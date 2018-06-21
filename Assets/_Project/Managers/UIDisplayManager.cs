using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;


public class UIDisplayManager : MonoBehaviour 
{
    [SerializeField] private TextMeshProUGUI _paddle1ScoreText;
    [SerializeField] private TextMeshProUGUI _paddle2ScoreText;
    private Brain _paddle1Brain;
    private Brain _paddle2Brain;

    // Use this for initialization
    void Start () 
	{
        _paddle1Brain = GameObject.FindGameObjectWithTag("Paddle1").GetComponent<Brain>();
        Assert.IsNotNull(_paddle1Brain);

        _paddle2Brain = GameObject.FindGameObjectWithTag("Paddle2").GetComponent<Brain>();
        Assert.IsNotNull(_paddle2Brain);

        _paddle1ScoreText.text = "1";
        _paddle2ScoreText.text = "2";
    }

    // Update is called once per frame
    void Update () 
	{
		
	}
}
