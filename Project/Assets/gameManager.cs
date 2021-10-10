using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class gameManager : MonoBehaviour
{
    public int gold  = 100;
    public int day = 0;
    public Text dayHUD;

    //1440 minutes in a day
    public float timeOfDay = 0;
    public float timeSpeed = 5;
    public bool isDay = true;
    public Text GoldValue;

    public GameObject Sun;
    public GameObject Moon;


    // Start is called before the first frame update
    void Start()
    {
      
        isDay = true;
    }

    // Update is called once per frame
    void Update()
    {
        dayHUD.text = "Day: " + day.ToString();

        if (isDay)
        {
            Moon.transform.eulerAngles = new Vector3(0, 0, 0);
            Sun.SetActive(true);
            Moon.SetActive(false);
           
            Sun.transform.Rotate(timeSpeed * Time.deltaTime, 0, 0);
        }
        else {
            Sun.transform.eulerAngles = new Vector3(0,0,0);
            Sun.SetActive(false);
            Moon.SetActive(true);
            Moon.transform.Rotate(timeSpeed * Time.deltaTime, 0, 0);
        }

        if (Sun.transform.localRotation.eulerAngles.x >= 180 || Moon.transform.localRotation.eulerAngles.x >= 180)
        {
            Sun.transform.eulerAngles = new Vector3(0, 0,0);
            Moon.transform.eulerAngles = new Vector3(0, 0, 0);

            Debug.Log(RandomNavmeshLocation(10));

            isDay = !isDay;
            if (isDay) {
                day += 1;
            }
        }
        GoldValue.text = gold.ToString();
    }

    public int returnGold() {
        return gold;
    }
    public void giveGold(int amount)
    {
        gold += amount;
        
    }
    public Vector3 RandomNavmeshLocation(float radius)
    {
        Vector3 randomDirection = Random.insideUnitSphere * radius;
        randomDirection += transform.position;
        NavMeshHit hit;
        Vector3 finalPosition = Vector3.zero;
        if (NavMesh.SamplePosition(randomDirection, out hit, radius, 1))
        {
            finalPosition = hit.position;
        }
        return finalPosition;
    }
}
