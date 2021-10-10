using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gameManager : MonoBehaviour
{
    public int gold  = 100;
    public int day = 0;

    public Text GoldValue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GoldValue.text = gold.ToString();
    }

    public int returnGold() {
        return gold;
    }
    public void giveGold(int amount)
    {
        gold += amount;
        
    }
}
