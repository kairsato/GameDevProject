using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItems : MonoBehaviour
{

    public GameObject equippedMelee;
    public GameObject equippedRanged;
    public GameObject currentEquipped;

    public GameObject currentEquippedObj;

    public GameObject MC;

    public playerAttack p;

    // Start is called before the first frame update
    void Start()
    {
        MC = GameObject.FindWithTag("MainCamera");
        equippedMelee = GameObject.FindWithTag("Weapon");
        equippedRanged = GameObject.FindWithTag("RangedWeapon");
        currentEquipped = equippedMelee;
        currentEquippedObj = Instantiate(equippedMelee, MC.transform);
        currentEquippedObj.tag = "CurrentEquip";

    }

    // Update is called once per frame
    void Update()
    {
        //currentEquipped = GameObject.FindWithTag("Weapon");

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {


            currentEquipped = equippedMelee;
            Destroy(currentEquippedObj);
            currentEquippedObj = Instantiate(equippedMelee, MC.transform);
            currentEquippedObj.tag = "CurrentEquip";

           //p.setWeapon();

            //removeRangedWeapon();
            //Instantiate(equippedMelee, MC.transform);




        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {


            currentEquipped = equippedRanged;
            Destroy(currentEquippedObj);
            currentEquippedObj = Instantiate(equippedRanged, MC.transform);
            currentEquippedObj.tag = "CurrentEquip";
           // p.setWeapon();
            //p.weapon = currentEquippedObj;
            //removeMeleeWeapon();
            //Instantiate(equippedRanged, MC.transform);


        }

    }


    void removeMeleeWeapon()
    {
        GameObject[] things = GameObject.FindGameObjectsWithTag("Weapon");
        for (int i = 0; i < things.Length; i++)
        {
            Destroy(things[i]);
        }

    }

    void removeRangedWeapon()
    {
        GameObject[] things = GameObject.FindGameObjectsWithTag("RangedWeapon");
        for (int i = 0; i < things.Length; i++)
        {
            Destroy(things[i]);
        }

    }

}