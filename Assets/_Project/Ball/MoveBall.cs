using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBall : MonoBehaviour
{
    #region FIELDS
    [SerializeField] private AudioSource _blip;
    [SerializeField] private AudioSource _blop;

    [SerializeField] private float _speed;

    private Vector3 _ballStartPosition;
    private Rigidbody2D _rigidBody; 
    #endregion

    #region EVENT PUBLISHING
    public event System.EventHandler<BallHitWallEventArgs> HitBackWall;

    protected virtual void OnHitBackWall (GameObject otherGameObject)
    {
        // Check that subscribers are present.
        HitBackWall?.Invoke(this, new BallHitWallEventArgs()
        {
            SenderPosition = transform.position,
            OtherGameObject = otherGameObject
        });
    }
    #endregion

    #region PRIVATE METHODS
    // Use this for initialization
    private void Start ()
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

            // Raise event.
            OnHitBackWall(collision.gameObject);
        }
        else
        {
            _blip.Play();
        }
    } 
    #endregion

    #region PUBLIC METHODS
    public void ResetBall ()
    {
        transform.position = _ballStartPosition;
        _rigidBody.velocity = Vector3.zero;
        Vector2 direction = new Vector2(Random.Range(-100f, 100f), Random.Range(-100f, 100f)).normalized;

        _rigidBody.AddForce(direction * _speed);
    }
    #endregion
}