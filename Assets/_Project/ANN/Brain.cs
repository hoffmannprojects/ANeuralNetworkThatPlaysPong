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
    private void Start () 
	{
        _ann = new ANN(6, 1, 1, 4, 0.11);
        _ballRigidbody2D = _ball.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	private void Update () 
	{
		
	}
}
