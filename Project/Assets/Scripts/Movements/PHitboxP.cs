using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PHitboxP : MonoBehaviour
{


    public static string currenttag;
    public static bool col;
    public int dmg;
    public GameObject weapon;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("Test");
        col = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void getdmg(int dmggiven)
    {
        dmg = dmggiven;
    }

    
    void OnTriggerEnter(Collider otherEntity)
    {
        
        if (otherEntity.gameObject.tag == "Enemy") // If hit enemy - deal damage based on the current item equipped
        {
           weapon.SendMessage("SwordDamage");
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
            Debug.Log("Called");
            currenttag = "Nothing";
            col = true;
        }


    }
}
