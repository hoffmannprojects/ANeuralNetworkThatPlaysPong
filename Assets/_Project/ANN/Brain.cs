using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brain : MonoBehaviour
{
    [SerializeField]
    private GameObject _paddle;
    [SerializeField]
    private GameObject _ball;
    private Rigidbody2D _ballRigidbody2D;
    private ANN _ann;

    [SerializeField]
    private float ballsSaved = 0; //TODO: Why float?
    [SerializeField]
    private float ballsMissed = 0;

    private float yVelocity;
    private float paddleMinY = 8.8f;
    private float paddleMaxY = 17.4f;
    private float paddleMaxSpeed = 15f;


    // Use this for initialization
    private void Start()
    {
        _ann = new ANN(6, 1, 1, 4, 0.11);
        _ballRigidbody2D = _ball.GetComponent<Rigidbody2D>();
    }

    private List<double> Run(
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
        var outputs = new List<double>();
        inputs.Add(ballXPosition);
        inputs.Add(ballYPosition);
        inputs.Add(ballXVelocity);
        inputs.Add(ballYVelocity);
        inputs.Add(paddleXPosition);
        inputs.Add(paddleYPosition);
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

    }
}
