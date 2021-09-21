using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{

    public int playerHealth;
    public Text healthText;


    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 100;
    }

    // Update is called once per frame
    void Update()
    {
        // Update current health
        healthText.text = "Health:" + playerHealth.ToString();

        
    }

    public void giveDamage(int damage) // If hit - apply damage
    {
        playerHealth -= damage;

    }

}

