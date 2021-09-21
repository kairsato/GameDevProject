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

    public float meleerate = 0.8f;

    // Start is called before the first frame update
    void Start()
    {
        timeElapsed = meleerate;
        // Get the hitbox prefab for use when required
        Instantiate(hitBoxMelee, new Vector3(0, 0, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {

        timeElapsed += Time.deltaTime;

        if (Input.GetKey(KeyCode.Mouse0))
        {
            createHitBox();
            
        }


    }

    void createHitBox()
    {
        if (timeElapsed >= meleerate)
        {
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
    /*
    void OnTriggerEnter(Collider otherEntity)
    {
        Debug.Log("Test");
        if(otherEntity.gameObject.CompareTag("Enemy")) {
            Debug.Log("Hit Enemy");
        }
        else
        {
            Debug.Log("Missed");
        }
    } */
}