using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    [SerializeField] private GameObject _paddle;
    [SerializeField] private GameObject _ball;
    private Rigidbody2D _ballRigidbody2D;
    private ANN _ann;

    [SerializeField] private float _ballsSaved = 0; //TODO: Why float?
    [SerializeField] private float _ballsMissed = 0;
    private float _yVelocity;
    private float _paddleMinY = 8.8f;
    private float _paddleMaxY = 17.4f;
    private float _paddleMaxSpeed = 15f;


    // Use this for initialization
    private void Start()
    {
        _ann = new ANN(6, 1, 1, 4, 0.005);
        _ballRigidbody2D = _ball.GetComponent<Rigidbody2D>();
    }

    private List<double> Run (
        double ballXPosition,
        double ballYPosition,
        double ballXVelocity,
        double ballYVelocity,
        double paddleXPosition,
        double paddleYPosition,
        double paddleVelocity,
        bool train)
    {
        var inputs = new List<double>();
        inputs.Add(ballXPosition);
        inputs.Add(ballYPosition);
        inputs.Add(ballXVelocity);
        inputs.Add(ballYVelocity);
        inputs.Add(paddleXPosition);
        inputs.Add(paddleYPosition);

        var outputs = new List<double>();
        outputs.Add(paddleVelocity);

        if (train)
        {
            return _ann.Train(inputs, outputs);
        }
        else
        {
            return _ann.CalcOutput(inputs, outputs);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        float yPosition = Mathf.Clamp(_paddle.transform.position.y + (_yVelocity * _paddleMaxSpeed * Time.deltaTime), _paddleMinY, _paddleMaxY);
        _paddle.transform.position = new Vector2(_paddle.transform.position.x, yPosition);

        // Layermask for backwall (layer 9).
        int layerMask = 1 << 9;
        RaycastHit2D hit = Physics2D.Raycast(_ball.transform.position, _ballRigidbody2D.velocity, 1000, layerMask);

        var output = new List<double>();

        if (hit.collider)
        {
            float deltaY = hit.point.y - _paddle.transform.position.y;

            output = Run(
                _ball.transform.position.x,
                _ball.transform.position.y,
                _ballRigidbody2D.velocity.x,
                _ballRigidbody2D.velocity.y,
                _paddle.transform.position.x,
                _paddle.transform.position.y,
                deltaY,
                true);

            _yVelocity = (float)output[0];
        }
        else
        {
            _yVelocity = 0;
        }
    }
}
