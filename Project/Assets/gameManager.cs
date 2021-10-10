using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class gameManager : MonoBehaviour
{
    public int gold = 100;
    public int day = 0;
    public Text dayHUD;

    public float range = 10.0f;

    //1440 minutes in a day
    public float timeOfDay = 0;
    public float timeSpeed = 5;
    public bool isDay = true;
    public Text GoldValue;

    public GameObject Sun;
    public GameObject Moon;

    public GameObject player;

    public GameObject enemyUnit;
    public bool end = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
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
        else
        {
            Sun.transform.eulerAngles = new Vector3(0, 0, 0);
            Sun.SetActive(false);
            Moon.SetActive(true);
            Moon.transform.Rotate(timeSpeed * Time.deltaTime, 0, 0);
        }

        if (Sun.transform.localRotation.eulerAngles.x >= 180 || Moon.transform.localRotation.eulerAngles.x >= 180)
        {
            Sun.transform.eulerAngles = new Vector3(0, 0, 0);
            Moon.transform.eulerAngles = new Vector3(0, 0, 0);



            isDay = !isDay;
            if (isDay)
            {
                day += 1;
            }

            Vector3 point;
            if (RandomPoint(player.transform.position, range, out point))
            {
                Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f);
            }
            Debug.Log(point);
            Instantiate(enemyUnit, point,player.transform.rotation);

        }
        GoldValue.text = gold.ToString();
        if (day >= 5 && !end)
        {
            end = true;
            //teleport player etc.
        }
    }

    public int returnGold()
    {
        return gold;
    }
    public void giveGold(int amount)
    {
        gold += amount;

    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 randomPoint = center + Random.insideUnitSphere * range;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                result = hit.position;
                return true;
            }
        }
        result = Vector3.zero;
        return false;
    }

   
}
