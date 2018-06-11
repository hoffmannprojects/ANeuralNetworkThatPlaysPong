using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    [SerializeField]
    private AudioSource _blip;
    [SerializeField]
    private AudioSource _blop;
    private Vector3 _ballStartPosition;
    private Rigidbody2D _rigidBody;

    [SerializeField]
    private float _speed;

	// Use this for initialization
	void Start ()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        _ballStartPosition = transform.position;
        ResetBall();
	}

    private void Update ()
    {
        if (Input.GetKeyDown("space"))
        {
            ResetBall();
        }
    }

    private void OnCollisionEnter2D (Collision2D collision)
    {
        if (collision.gameObject.tag == "backwall")
        {
            _blop.Play();
        }
        else
        {
            _blip.Play();
        }
    }

    #region PUBLIC METHODS
    public void ResetBall ()
    {
        transform.position = _ballStartPosition;
        _rigidBody.velocity = Vector3.zero;
        Vector2 direction = new Vector2(Random.Range(100f, 300f), Random.Range(-100f, 100f)).normalized;

        _rigidBody.AddForce(direction * _speed);
    } 
    #endregion
}
