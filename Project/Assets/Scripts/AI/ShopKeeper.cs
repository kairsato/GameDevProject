using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class ShopKeeper : MonoBehaviour
{
    public GameObject menu;
    private GameObject GameManager;
    public float shopRange = 10;
    private Transform playerPosition;
    private GameObject player;
    private bool isShowing;

    public Text gold;

    private gameManager gm;
    private EquippedItems ei;

    void Start()
    {
        GameManager = GameObject.FindWithTag("GameManager");
        player = GameObject.FindWithTag("Player");
        playerPosition = player.transform;
        menu.SetActive(false);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        ei = player.GetComponent<EquippedItems>();
        gm = GameManager.GetComponent<gameManager>();
        
        if (Vector3.Distance(playerPosition.position, transform.position) < shopRange)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {

                EnterShop();

            }
            
            
        }
        if (isShowing)
        {
            Time.timeScale = 0f;
        }
        else {
            Time.timeScale = 1f;
        }
    }
    public void EnterShop() {
        
        gold.text = "Current Gold: "+ gm.gold.ToString();
        isShowing = !isShowing;
       
        menu.SetActive(isShowing);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;


        player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = !isShowing;


    }

    public void BuyItem(string item)
    {

        Debug.Log(gm.gold);
        //Debug.Log();
        switch (item)
        {
            case "HealthPoition":
                if (gm.gold >= 200) {
                    gm.gold -= 200;

                }
                break;
            case "SpeedPoition":
                if (gm.gold >= 500)
                {
                    gm.gold -= 500;
                }
                break;
            case "Axe":
                if (gm.gold >= 150)
                {
                    gm.gold -= 150;
                    ei.updateWeapons("Axe","");
                   
                }
                break;
            case "Bow":
                if (gm.gold >= 200)
                {
                    gm.gold -= 200;
                    ei.updateWeapons("", "Bow");
                }
                break;
            case "CrossBow":
                if (gm.gold >= 250)
                {
                    gm.gold -= 250;
                    ei.updateWeapons("", "Crossbow");
                }
                break;
        }
        gold.text = "Current Gold: " + gm.gold.ToString();
        Debug.Log(gm.gold);
        // Wandering State
    }

}
