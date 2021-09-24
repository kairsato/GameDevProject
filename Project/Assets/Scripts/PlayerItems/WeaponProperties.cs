using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponProperties : MonoBehaviour
{

    public float attackSpeed = 1f;
    public float damage = 35f;
    public string weaponName;
    protected float timeElapsed;

   // public AnimationClip attackanim;

    void Start()
    {
        timeElapsed = attackSpeed;



        //damage = 35 * currentday;
    }

   
    void Update()
    {

        timeElapsed += Time.deltaTime;

        if ((Input.GetKey(KeyCode.Mouse0)) && (timeElapsed >= attackSpeed))
        {
            
        }

    }

   

}
