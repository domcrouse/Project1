using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/*Dimitrios Martin - MAR16003880
Games Technologies
Code For Project Re-Imagined*/

public class ScoreBoard : MonoBehaviour
{
    public static int score;
    Text text;

    //sets the score to null
    void Start()
    {
        text = GetComponent<Text>();
        score = 0;
    }
    //updates the score for every emy that has been killed
    void Update()
    {
        text.text = "Score:" + score;
    }
}
