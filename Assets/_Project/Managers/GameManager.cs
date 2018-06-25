using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;


public class GameManager : MonoBehaviour 
{
    #region FIELDS
    [SerializeField] private UIDisplayManager _uIDisplayManager;
    [SerializeField] private float _delayAfterScore = 1;

    private int _player1Score = 0;
    private int _player2Score = 0;

    private MoveBall _moveBall;
    #endregion

    #region EVENT SUBSCRIPTIONS
    private void OnEnable ()
    {
        /// Publishers.
        _moveBall = FindObjectOfType<MoveBall>();
        Assert.IsNotNull(_moveBall);

        /// Subscribe to events.
        _moveBall.HitBackWall += OnBallHitBackWall;
    }

    private void OnDisable ()
    {
        /// Unsubscribe from events.
        _moveBall.HitBackWall -= OnBallHitBackWall;
    }
    #endregion

    // Use this for initialization
    private void Start () 
	{
        Assert.IsNotNull(_uIDisplayManager);
	}

    private void OnBallHitBackWall (object source, BallHitWallEventArgs e)
    {
        var ballPositionX = e.SenderPosition.x;
        var wallPositionX = e.OtherGameObject.transform.position.x;

        // Left backwall.
        if (wallPositionX < ballPositionX)
        {
            Debug.Log("Scoring: Left backwall hit.");
            _player2Score++;
        }
        // Right backwall.
        else
        {
            Debug.Log("Scoring: Right backwall hit.");
            _player1Score++;
        }
        _uIDisplayManager.UpdateScore(_player1Score, _player2Score);

        StartCoroutine(WaitAndResetBall(_delayAfterScore));
    }

    private IEnumerator WaitAndResetBall (float timeToWait)
    {
        _moveBall.gameObject.SetActive(false);
        yield return new WaitForSecondsRealtime(timeToWait);
        _moveBall.gameObject.SetActive(true);
        _moveBall.ResetBall();
    } 
}
