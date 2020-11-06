using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public Circuit raceTrack;
    public float brakeCoefficient = 3f;
    TireScript ds;
    public float steerCoefficient = 0.01f;
    public float accelCoefficient = 0.3f;
    Vector3 wayPointRequired;
    Vector3 nextWaypoint;
    int indexTarget = 0;
    float totalDistanceToTarget;

    GameObject tracker;
    int currentIndexTarget = 0;
    float lookAhead = 10;


    // Start is called before the first frame update
    void Start()
    {
        
        ds = this.GetComponent<TireScript>();
        wayPointRequired = raceTrack.waypoints[indexTarget].transform.position;
        nextWaypoint = raceTrack.waypoints[indexTarget + 1].transform.position;
        totalDistanceToTarget = Vector3.Distance(wayPointRequired, ds.rb.gameObject.transform.position);

        tracker = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        DestroyImmediate(tracker.GetComponent<Collider>());
        tracker.transform.position = ds.rb.gameObject.transform.position;
        tracker.transform.rotation = ds.rb.gameObject.transform.rotation;
    }


    void BestLine()
    {
        Debug.DrawLine(ds.rb.gameObject.transform.position, tracker.transform.position);

        if (Vector3.Distance(ds.rb.gameObject.transform.position, tracker.transform.position) > lookAhead) return;

        tracker.transform.LookAt(raceTrack.waypoints[currentIndexTarget].transform.position);
        tracker.transform.Translate(0, 0, 1.0f);

        if (Vector3.Distance(tracker.transform.position, raceTrack.waypoints[currentIndexTarget].transform.position) < 1)
        {
            currentIndexTarget++;
            if (currentIndexTarget >= raceTrack.waypoints.Length)
                currentIndexTarget = 0;
        }

    }

  
    void Update()
    {
        if(!RaceStart.raceStart)
        {
            return;
        }
        BestLine();
        Vector3 localTarget = ds.rb.gameObject.transform.InverseTransformPoint(tracker.transform.position);
        float targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;
        float steer = Mathf.Clamp(targetAngle * steerCoefficient, -1, 1) * Mathf.Sign(ds.currentSpeed);

        float brake = 0;
        float accel = 0.5f;

        ds.Go(accel, steer, brake);

        ds.CheckForSkid();
        
    }
}