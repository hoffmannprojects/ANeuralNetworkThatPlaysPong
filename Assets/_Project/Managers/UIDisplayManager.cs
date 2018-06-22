using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;


public class UIDisplayManager : MonoBehaviour 
{
    [SerializeField] private TextMeshProUGUI _paddle1ScoreText;
    [SerializeField] private TextMeshProUGUI _paddle2ScoreText;

    private Animator _paddle1ScoreAnimator;
    private Animator _paddle2ScoreAnimator;

    private int _player1LastScore = 0;
    private int _player2LastScore = 0;

    // Use this for initialization
    void Start () 
	{
        _paddle1ScoreText.text = "0";
        _paddle2ScoreText.text = "0";

        _paddle1ScoreAnimator = _paddle1ScoreText.GetComponent<Animator>();
        Assert.IsNotNull(_paddle1ScoreAnimator);

        _paddle2ScoreAnimator = _paddle2ScoreText.GetComponent<Animator>();
        Assert.IsNotNull(_paddle2ScoreAnimator);
    }

    #region PUBLIC METHODS
    public void UpdateScore (int player1Score, int player2Score)
    {
        int playerScored = (player1Score > _player1LastScore) ? 1 : 2;

        _paddle1ScoreText.text = player1Score.ToString(); 
        _paddle2ScoreText.text = player2Score.ToString();

        if (playerScored == 1)
        {
            _paddle1ScoreAnimator.SetTrigger("Scored");
        }
        else
        {
            _paddle2ScoreAnimator.SetTrigger("Scored");
        }

        _player1LastScore = player1Score;
        _player2LastScore = player2Score;
    }
    #endregion
}
