using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Melee : MonoBehaviour
{

    public float attackSpeed;
    public int damage;
    public string weaponType;
    public GameObject pObj;
    public int currentday = 1;

    // Start is called before the first frame update
    void Start()
    {
        damage = 35 * currentday;
    }

    // Update is called once per frame
    void Update()
    {
        //pObj.SendMessage("getdmg", damage);
    }

    public void SwordDamage()
    {
        pObj.SendMessage("getdmg", damage);

    }

}
