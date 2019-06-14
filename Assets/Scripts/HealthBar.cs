using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthBar : MonoBehaviour
{
    public float CurrentHealth;
    public float MaxHealth;
    public float CurrentArmour;
    public float MaxArmour;
    public float nextDamage;
    public SpriteRenderer rend;

    public Slider healthBar;
    public Slider ShieldBar;

    // Start is called before the first frame update
    void Start()
    {
        CurrentHealth = MaxHealth;
        healthBar.value = CalculateHealth();

        CurrentArmour = MaxArmour;
        ShieldBar.value = CalculateArmour();

        rend = GetComponent<SpriteRenderer>();
    }

    void Update(){
        if(Time.time < nextDamage){
            rend.color = new Color(rend.color.r,rend.color.g,rend.color.b,Mathf.Sin(nextDamage-Time.time*15.0f)/2 + 0.5f);
        }
        else if(rend.color.a != 1.0f){
            rend.color = new Color(rend.color.r,rend.color.g,rend.color.b,1.0f);
        }
    }

    void DealingDamage(float DamageVal)
    {
        nextDamage = Time.time + 2;
        if(CurrentArmour <= 0){
            CurrentHealth -= DamageVal;
        }
        else{
            CurrentArmour -= DamageVal;
        }
        healthBar.value = CalculateHealth();
        ShieldBar.value = CalculateArmour();

        //kill the player when no health is left
        if (CurrentHealth <= 0)
            Die();
    }

    float CalculateArmour()
    {
        return CurrentArmour / MaxArmour;
    }

    float CalculateHealth()
    {
        return CurrentHealth / MaxHealth;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if(Time.time > nextDamage){
            if (collision.gameObject.tag == "Enemy")
            {
                DealingDamage(10);
                Debug.Log("DamageDealt to player");
            }
            else if (collision.gameObject.tag == "Player")
            {
               // DealingDamage(0);
                Debug.Log("Collision with Enemy");
            }
            else if (collision.gameObject.tag == "Healing")
            {
                DealingDamage(-10);
                Destroy(GameObject.FindGameObjectWithTag("Healing"));
                Debug.Log("10HP");
            }
            else if (collision.gameObject.tag == "Healing2035")
            {
                DealingDamage(-25);
                Destroy(GameObject.FindGameObjectWithTag("Healing2035"));
                Debug.Log("25HP");
            }
        }
    }

    void Die()
    {
        SceneManager.LoadScene("Menu");
    }
}
