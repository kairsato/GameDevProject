using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class player : MonoBehaviour
{

    public float playerHealth;
    public Image health;
    public GameObject DeadScreen;
    private float maxWidth = 200;

    // Start is called before the first frame update
    void Start()
    {
        playerHealth = 100;
        DeadScreen.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Update current health
        //healthText.text = "Health:" + playerHealth.ToString();
        health.rectTransform.sizeDelta = new Vector2((float)maxWidth * (playerHealth / 100), 25);

        if (playerHealth <= 0)
        {
            dead();
        }
    }

    public void giveDamage(int damage) // If hit - apply damage
    {
        playerHealth -= damage;

    }
    public void dead()
    {
        DeadScreen.SetActive(true);
        Time.timeScale = 0f;
        //health.SetActive(true);

    }

    
}
