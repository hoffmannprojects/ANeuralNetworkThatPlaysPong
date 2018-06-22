using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;


public class UIDisplayManager : MonoBehaviour 
{
    [SerializeField] private TextMeshProUGUI _paddle1ScoreText;
    [SerializeField] private TextMeshProUGUI _paddle2ScoreText;

    // Use this for initialization
    void Start () 
	{
        _paddle1ScoreText.text = "0";
        _paddle2ScoreText.text = "0";
    }

    #region PUBLIC METHODS
    public void UpdateScore (int player1Score, int player2Score)
    {
        _paddle1ScoreText.text = player1Score.ToString();
        _paddle2ScoreText.text = player2Score.ToString();
    }
    #endregion
}
