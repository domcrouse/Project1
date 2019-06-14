using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*Dimitrios Martin - MAR16003880
Games Technologies
Code For Project Re-Imagined*/
public class Player_Controller : MonoBehaviour
{
    Rigidbody2D Player;
    Animator Player_Animation;
    float Player_Speed;
    float Xaxis;
    bool Facing_Right;
    Vector3 Local_Scale;
    public bool isPlaying;
    public GameObject KillBox;



    //sets the player speed and also retireves the animations for the player movement
    void Start()
    {
        Player = GetComponent<Rigidbody2D>();
        Player_Animation = GetComponent<Animator>();
        Local_Scale = transform.localScale;
        Player_Speed = 5f;

    }
    //used in order to kill the enmy at the end of the animation which enables a box collider
    IEnumerator Die()
    {
        yield return new WaitForSeconds(0.25f);
        GameObject killBox =  Instantiate(KillBox, transform.position , Quaternion.identity, transform);
        yield return new WaitForSeconds(0.1f);
        if (killBox != null)
        {
            Destroy(killBox);
        }
        Player_Animation.SetBool("isAttacking", false);

    }

    //used in order to actually move the player with the animation used for the character
    void Update()
    {
        Xaxis = Input.GetAxisRaw("Horizontal") * Player_Speed;
        Player.velocity = new Vector2(Xaxis, Player.velocity.y);


        if (Input.GetMouseButtonDown(0))
        {
            if (!Player_Animation.GetBool("isAttacking"))
            {
                Player_Animation.SetBool("isAttacking", true);
                StartCoroutine(Die());
            }
        }

        if (Input.GetButtonDown("Jump") && Player.velocity.y == 0)
        { Player.AddForce(Vector2.up * 100f); }



        if (Mathf.Abs(Input.GetAxis("Horizontal")) > 0.1f && Player.velocity.y == 0)
        { Player_Animation.SetBool("isWalking", true); }
        else
        { Player_Animation.SetBool("isWalking", false); }



        if (Player.velocity.y == 0)
        {
            Player_Animation.SetBool("isJumping", false);
            Player_Animation.SetBool("isFalling", false);
        }

        if (Player.velocity.y > 0)
            Player_Animation.SetBool("isJumping", true);

        if (Player.velocity.y < 0)
        {
            Player_Animation.SetBool("isJumping", false);
            Player_Animation.SetBool("isFalling", true);
        }


        if (Xaxis > 0)
            Facing_Right = true;
        else if (Xaxis < 0)
            Facing_Right = false;

        if (((Facing_Right) && (Local_Scale.x < 0)) || ((!Facing_Right) && (Local_Scale.x > 0)))
            Local_Scale.x *= -1;


        transform.localScale = Local_Scale;

    }

}
/**Dan - 
Pros: You can jump.You can walk.Animations are nice.
Cons: Dont place code in void start.

**/