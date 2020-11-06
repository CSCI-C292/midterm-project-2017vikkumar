using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceStart : MonoBehaviour
{

    public GameObject[] startItems;
    public GameObject restart;
    public static bool raceStart = false;
    // Start is called before the first frame update
    void Start()
    {
        CheckPoint.laps = 0;
        restart.SetActive(false);
        for (int i = 0; i < startItems.Length; i++)
        {
            startItems[i].SetActive(false);
        }
        StartCoroutine(CountDown());
    }
    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < startItems.Length; i++)
        {
            startItems[i].SetActive(true);
            yield return new WaitForSeconds(1);
            startItems[i].SetActive(false);
        }
        raceStart = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
