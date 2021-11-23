using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour
{
    private int score = 0;
    public Text scoreText;

    void Start()
    {
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int Score()
    {
        return this.score;
    }
    //suma puntaje
    public void SumarScore(int var)
    {
        this.score += var;
       scoreText.text = "Score: " + score;
    }
}
