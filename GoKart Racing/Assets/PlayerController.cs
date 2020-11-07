using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    TireScript scriptDrive;
    public WheelCollider[] wheels;
    public GameObject[] Wheels;
    public GameObject restartButton;
    // Start is called before the first frame update
    void Start()
    {
        scriptDrive = this.GetComponent<TireScript>();
    }
    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckPoint.laps == 5) {
            GameOver();
        }
        
        float acceleration = Input.GetAxis("Vertical");
        if(!RaceStart.raceStart) acceleration = 0;
        float turn = Input.GetAxis("Horizontal");
        float jump = Input.GetAxis("Jump");

        scriptDrive.Go(acceleration, turn, jump);
        scriptDrive.CheckForSkid();

    }
}
