using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHitboxP : MonoBehaviour
{


    public static string currenttag;
    public static bool col;
    public float dmg;
    public GameObject player;

  

    //external script values
    private playerAttack damageValues;

    // Start is called before the first frame update
    void Start()
    {
        //weapon = GameObject.FindWithTag("Weapon");
        //melee = weapon.GetComponent<Melee>();

        player = GameObject.FindWithTag("Player");
        damageValues = player.GetComponent<playerAttack>();

        //Debug.Log("Test");
        col = false;
    }

    // Update is called once per frame
    void Update()
    {
        dmg = damageValues.weaponDMG;
    }

    void getdmg(int dmggiven)
    {
        dmg = dmggiven;
    }

    
    void OnTriggerEnter(Collider otherEntity)
    {
        
        //update dmg values
        //dmg = damageValues.weaponDMG;

        if (otherEntity.gameObject.tag == "Enemy") // If hit enemy - deal damage based on the current item equipped
        {
           
            Debug.Log("Hit Enemy");
            currenttag = otherEntity.gameObject.tag;
            col = true;
            otherEntity.gameObject.SendMessage("giveDamage", dmg);
        }
        else if (otherEntity.gameObject.tag == "Player")
        {
            Debug.Log("Hit Player");
            currenttag = otherEntity.gameObject.tag;
            col = true;
        }
        else
        {
            //Debug.Log("Called");
            currenttag = "Nothing";
            col = true;
        }
        

    }
}
