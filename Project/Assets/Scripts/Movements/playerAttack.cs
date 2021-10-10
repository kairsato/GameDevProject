using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{

    public GameObject hitBoxMelee;
    public GameObject hitBoxRanged;
    
    public float hitBoxLifeTime = 0.1f;
    public float arrowLifeTime = 5f;
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
        Instantiate(hitBoxRanged, new Vector3(0, 0, 0), Quaternion.identity);
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

        if (Input.GetKey(KeyCode.Mouse0))
        {

            if ((damageValues.weaponName == "sword") || (damageValues.weaponName == "axe") || (damageValues.weaponName == "Sword") || (damageValues.weaponName == "Axe"))
            {
           
                createMeleeHitBox();

            }

            if ((damageValues.weaponName == "bowarrow") || (damageValues.weaponName == "crossbowarrow"))
            {

                createRangedHitBox();


            }

        }


    }


    void createMeleeHitBox()
    {
        if (timeElapsed >= rof)
        {
            weapon.GetComponent<Animation>().Play();
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
        //Quaternion cameraAngles = entityface.transform.rotation;
        //cameraAngles.x += 90;


        //hitBoxRanged.transform.Rotate(90, 180, 90);
     
        
        

       
        var hitBox = (GameObject)Instantiate(hitBoxRanged, entityface.transform.position + (entityface.transform.forward * 2), entityface.transform.rotation);

        hitBox.transform.Rotate(90, 0, 0);
      
        
        Rigidbody arrowRB = hitBox.GetComponent<Rigidbody>();
        arrowRB.velocity = entityface.transform.forward * 30;
        

        // Check for collision - if yes run collision function
        hitboxtag = PHitboxP.currenttag;
        coll = PHitboxP.col;

        Destroy(hitBox, arrowLifeTime);
        timeElapsed = 0;

        // Check for collision - if yes run collision function
        // hitboxtag = PHitboxP.currenttag;
        //coll = PHitboxP.col;

        /*
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
        }*/
    }


}
