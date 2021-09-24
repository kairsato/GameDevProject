using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItems : MonoBehaviour
{

    public GameObject equippedMelee;
    public GameObject equippedRanged;
    public GameObject currentEquipped;

    public GameObject MC;

    // Start is called before the first frame update
    void Start()
    {
        MC = GameObject.FindWithTag("MainCamera");
        equippedMelee = GameObject.FindWithTag("Weapon");
    }

    // Update is called once per frame
    void Update()
    {
        currentEquipped = GameObject.FindWithTag("Weapon");

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            removeCurrentWeapon();


            Instantiate(equippedMelee, MC.transform);


        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            removeCurrentWeapon();


            Instantiate(equippedRanged, MC.transform);


        }

    }


    void removeCurrentWeapon()
    {
        GameObject[] things = GameObject.FindGameObjectsWithTag("Weapon");
        for (int i = 0; i < things.Length; i++)
        {
            Destroy(things[i]);
        }
    }

}