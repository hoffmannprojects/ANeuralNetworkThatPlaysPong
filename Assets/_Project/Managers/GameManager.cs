using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class GameManager : MonoBehaviour 
{
    #region FIELDS
    private MoveBall _moveBall;
    #endregion

    #region PROPERTIES
    public int Player1Score { get; private set; } = 0;
    public int Player2Score { get; private set; } = 0;
    #endregion

    #region EVENT SUBSCRIPTIONS
    private void OnEnable ()
    {
        // Publishers.
        _moveBall = FindObjectOfType<MoveBall>();
        Assert.IsNotNull(_moveBall);

        // Subscribe.
        _moveBall.HitBackWall += OnBallHitBackWall;
    }

    private void OnDisable ()
    {
        // Unsubscribe.
        _moveBall.HitBackWall -= OnBallHitBackWall;
    }
    #endregion

    // Use this for initialization
    private void Start () 
	{
        
	}

    private void OnBallHitBackWall (object source, BallHitWallEventArgs e)
    {
        var ballPositionX = e.SenderPosition.x;
        var wallPositionX = e.OtherGameObject.transform.position.x;

        // Left backwall.
        if (wallPositionX < ballPositionX)
        {
            Debug.Log("Scoring: Left backwall hit.");
        }
        // Right backwall.
        else
        {
            Debug.Log("Scoring: Right backwall hit.");
        }
    }
}
