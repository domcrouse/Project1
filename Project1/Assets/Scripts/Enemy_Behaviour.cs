using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Dimitrios Martin - MAR16003880
Games Technologies
Code For Project Re-Imagined*/


/*references and links for code used*/
/*https://www.youtube.com/watch?v=rhoQd6IAtDo used or the Zombie AI*/
/*https://docs.unity3d.com/ScriptReference/Physics2D.Raycast.html & https://www.youtube.com/watch?v=Hbo7vmsrABUused in order to understand raycasting more in depth*/
/*https://www.youtube.com/watch?v=AI8XNNRpTTw used in order to create an enmy spawner for the game*/

public class Enemy_Behaviour : MonoBehaviour {


    [SerializeField] private LayerMask Visible;
    private SpriteRenderer Enemy;
    public float Distance;
    public int scoreValue = 10;
    public float speed;
    private Transform target;


    // Use this for initialization
    void Start () {
        //finds the objects which are tagged with player
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
	}

    //called every frame to detect the player
    void Update() {
        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime); 
    }
}
