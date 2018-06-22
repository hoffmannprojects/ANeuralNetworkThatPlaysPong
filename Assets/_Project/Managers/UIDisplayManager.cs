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
        _paddle1ScoreText.text = "1";
        _paddle2ScoreText.text = "2";
    }

    // Update is called once per frame
    void Update () 
	{
		
	}
}
