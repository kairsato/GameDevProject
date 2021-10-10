using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItems : MonoBehaviour
{

    public GameObject equippedMelee;
    public GameObject equippedRanged;
    public GameObject currentEquipped;

    private GameObject MC;

    private playerAttack p;

    public float timeElapsed;

    private WeaponProperties WP;

    // Start is called before the first frame update
    void Start()
    {
        // Defines where the weapons will be located in the hierarchy
        MC = GameObject.FindWithTag("MainCamera");
        // Defines starting Melee and Ranged weapons
        equippedMelee = GameObject.FindWithTag("Sword");
        equippedRanged = GameObject.FindWithTag("Bow");
        // Create the melee weapon as the starting equipped weapon
        currentEquipped = Instantiate(equippedMelee, MC.transform);
        currentEquipped.tag = "CurrentEquip";
        WP = currentEquipped.GetComponent<WeaponProperties>();

    }

    // Update is called once per frame
    void Update()
    {

        timeElapsed += Time.deltaTime;
        //currentEquipped = GameObject.FindWithTag("Weapon");
        WP = currentEquipped.GetComponent<WeaponProperties>();
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {


            //currentEquipped = equippedMelee;
            Destroy(currentEquipped);
            currentEquipped = Instantiate(equippedMelee, MC.transform);
            currentEquipped.tag = "CurrentEquip";
            timeElapsed = 0;
           //p.setWeapon();

            //removeRangedWeapon();
            //Instantiate(equippedMelee, MC.transform);




        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {


            //currentEquipped = equippedRanged;
            Destroy(currentEquipped);
            currentEquipped = Instantiate(equippedRanged, MC.transform);
            currentEquipped.tag = "CurrentEquip";
            // p.setWeapon();
            //p.weapon = currentEquippedObj;
            //removeMeleeWeapon();
            //Instantiate(equippedRanged, MC.transform);
            timeElapsed = 0;

        }

        if (timeElapsed >= WP.attackSpeed)
        {

            if (WP.weaponName == "bow")
            {
                Destroy(currentEquipped);
                currentEquipped = Instantiate(GameObject.FindWithTag("BowArrow"), MC.transform);
                currentEquipped.tag = "CurrentEquip";
                timeElapsed = 0;
            }

            if (WP.weaponName == "crossbow")
            {
                Destroy(currentEquipped);
                currentEquipped = Instantiate(GameObject.FindWithTag("CrossbowArrow"), MC.transform);
                currentEquipped.tag = "CurrentEquip";
                timeElapsed = 0;
            }

        }

        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (WP.weaponName == "bowarrow")
            {
                Destroy(currentEquipped);
                currentEquipped = Instantiate(GameObject.FindWithTag("Bow"), MC.transform);
                currentEquipped.tag = "CurrentEquip";
                timeElapsed = 0;
            }

            if (WP.weaponName == "crossbowarrow")
            {
                Destroy(currentEquipped);
                currentEquipped = Instantiate(GameObject.FindWithTag("Crossbow"), MC.transform);
                currentEquipped.tag = "CurrentEquip";
                timeElapsed = 0;
            }

        }



    }
    public void updateWeapons(string melee, string ranged)
    {
        Destroy(currentEquipped);
        if (melee.Length != 0) {
            equippedMelee = GameObject.FindWithTag(melee);
        }
        if (ranged.Length != 0)
        {
            equippedRanged = GameObject.FindWithTag(ranged);
        }
       
        
        
        // Create the melee weapon as the starting equipped weapon
        currentEquipped = Instantiate(equippedMelee, MC.transform);
        currentEquipped.tag = "CurrentEquip";
        WP = currentEquipped.GetComponent<WeaponProperties>();
    }
    /*
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

    }*/

}