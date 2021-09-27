using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{

    public GameObject hitBoxMelee;
    public float hitBoxLifeTime = 0.1f;
    public Camera entityface;
    public string hitboxtag;
    public bool coll;
    protected float timeElapsed;
    protected float dmg;

    public GameObject weapon;

    public float weaponDMG;
    private WeaponProperties damageValues;

    public float swingSpeed = 0.05f;
    public bool swinging = false;

   

    private float rof;

    // Start is called before the first frame update
    void Start()
    {
       /* weapon = GameObject.FindWithTag("Weapon");
        weapon2 = GameObject.FindWithTag("RangedWeapon");

        damageValues = weapon.GetComponent<WeaponProperties>();
        damageValues2 = weapon2.GetComponent<WeaponProperties>();

        rof = damageValues.attackSpeed;

        timeElapsed = rof; */

        weapon = GameObject.FindWithTag("CurrentEquip");

        damageValues = weapon.GetComponent<WeaponProperties>();

        rof = damageValues.attackSpeed;

        timeElapsed = rof;


        // Get the hitbox prefab for use when required
        Instantiate(hitBoxMelee, new Vector3(0, 0, 0), Quaternion.identity); 
    }

    // Update is called once per frame
    void Update()
    {

        if ((Input.GetKeyDown(KeyCode.Alpha1)) || (Input.GetKeyDown(KeyCode.Alpha2))) // Update these values on weapon change
        {

            rof = damageValues.attackSpeed;
            timeElapsed = rof;

        }

            // Update these values all the time
            weapon = GameObject.FindWithTag("CurrentEquip");
            damageValues = weapon.GetComponent<WeaponProperties>();
            weaponDMG = damageValues.damage;

        timeElapsed += Time.deltaTime;

        if ((Input.GetKey(KeyCode.Mouse0)) && (timeElapsed >= rof))
        {

            if ((damageValues.weaponName == "sword") || (damageValues.weaponName == "axe") || (damageValues.weaponName == "Sword") || (damageValues.weaponName == "Axe"))
            {
                weapon.GetComponent<Animation>().Play();
                createMeleeHitBox();

                //weapon.transform.rotation = new Quaternion(0,0,0,0);
                //StartCoroutine(meleeAnimation());


            }

            if ((damageValues.weaponName == "bow") || (damageValues.weaponName == "Bow") || (damageValues.weaponName == "crossbow") || (damageValues.weaponName == "Crossbow"))
            {

                createRangedHitBox();


            }

        }


    }


    void createMeleeHitBox()
    {
        if (timeElapsed >= rof)
        {
            //  hitBoxMelee.sendmessage("",dmg);
            // Create melee hit box
            //Instantiate(hitBoxMelee, transform.position + (transform.forward * 2), transform.rotation);

            var hitBox = (GameObject)Instantiate(hitBoxMelee, entityface.transform.position + (entityface.transform.forward * 2), entityface.transform.rotation);

            // Check for collision - if yes run collision function
            hitboxtag = PHitboxP.currenttag;
            coll = PHitboxP.col;

            // Delete box collider
            Destroy(hitBox, hitBoxLifeTime);
            timeElapsed = 0;
        }
        
    }

    void createRangedHitBox()
    {
        if (timeElapsed >= rof)
        {
            //  hitBoxMelee.sendmessage("",dmg);
            // Create melee hit box
            //Instantiate(hitBoxMelee, transform.position + (transform.forward * 2), transform.rotation);

            var hitBox = (GameObject)Instantiate(hitBoxMelee, entityface.transform.position + (entityface.transform.forward * 2), entityface.transform.rotation);

            // Check for collision - if yes run collision function
            hitboxtag = PHitboxP.currenttag;
            coll = PHitboxP.col;

            // Delete box collider
            Destroy(hitBox, hitBoxLifeTime);
            timeElapsed = 0;
        }
    }


}
