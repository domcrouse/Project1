using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Dimitrios Martin - MAR16003880
Games Technologies
Code For Project Re-Imagined*/
public class KillOnTouch : MonoBehaviour {

    //This is used to destroy the targeted game object at the end of the atting animation
    //and adds 1 to the scoreboard
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            ScoreBoard.score += 1;
        }
    }
    
}
