using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class ShopKeeper : MonoBehaviour
{
    public GameObject menu;
    public float shopRange = 10;
    private Transform playerPosition;
    private GameObject player;
    private bool isShowing;


    void Start()
    {
        player = GameObject.FindWithTag("Player");
        playerPosition = player.transform;
        menu.SetActive(false);
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {

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
        Debug.Log("aaa");
        
        isShowing = !isShowing;
        Debug.Log(isShowing);
        menu.SetActive(isShowing);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;


        player.GetComponent<UnityStandardAssets.Characters.FirstPerson.FirstPersonController>().enabled = !isShowing;


    }

    // Wandering State


}
