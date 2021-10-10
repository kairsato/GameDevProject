using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour
{

    public static string currenttag;
    public static bool col;
    public float dmg;



    // Start is called before the first frame update
    void Start()
    {
        dmg = 22;
    }

    // Update is called once per frame
    void Update()
    {
        



    }


    void OnTriggerEnter(Collider otherEntity)
    {

        if (otherEntity.gameObject.tag == "Player") // If hit enemy - deal damage based on the current item equipped
        {

            Debug.Log("Hit Player");
            currenttag = otherEntity.gameObject.tag;
            col = true;
            otherEntity.gameObject.SendMessage("giveDamage", dmg);
        }
       /* else if (otherEntity.gameObject.tag == "Player")
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
        }*/


    }


}
