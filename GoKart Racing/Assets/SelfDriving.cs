using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDriving : MonoBehaviour
{
    public BestLine raceTrack;
    
    TireScript ds;
    public float steerForce = 0.05f;
    
    Vector3 requiredCheckpoint;
    Vector3 nextCheckpoint;
    int indexTarget = 0;
    float totalDistanceToTarget;

    GameObject guide;
    int currentIndexTarget = 0;
    float lookAhead = 10;


    // Start is called before the first frame update
    void Start()
    {
        
        ds = this.GetComponent<TireScript>();
        requiredCheckpoint = raceTrack.dfs[indexTarget].transform.position;
        nextCheckpoint = raceTrack.dfs[indexTarget + 1].transform.position;
        totalDistanceToTarget = Vector3.Distance(requiredCheckpoint, ds.rb.gameObject.transform.position);

        guide = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        guide.GetComponent<MeshRenderer>().enabled = false;
        DestroyImmediate(guide.GetComponent<Collider>());
        guide.transform.position = ds.rb.gameObject.transform.position;
        guide.transform.rotation = ds.rb.gameObject.transform.rotation;
    }


    

  
    void Update()
    {
        if(!RaceStart.raceStart)
        {
            return;
        }
        Debug.DrawLine(ds.rb.gameObject.transform.position, guide.transform.position);

        if (Vector3.Distance(ds.rb.gameObject.transform.position, guide.transform.position) > lookAhead) return;

        guide.transform.LookAt(raceTrack.dfs[currentIndexTarget].transform.position);
        guide.transform.Translate(0, 0, 1.0f);

        if (Vector3.Distance(guide.transform.position, raceTrack.dfs[currentIndexTarget].transform.position) < 1)
        {
            currentIndexTarget++;
            if (currentIndexTarget >= raceTrack.dfs.Length)
                currentIndexTarget = 0;
        }
        Vector3 localTarget = ds.rb.gameObject.transform.InverseTransformPoint(guide.transform.position);
        float targetAngle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

        ds.Go(0.5f, Mathf.Clamp(targetAngle * steerForce, -1, 1) * Mathf.Sign(ds.currentSpeed), 0);

        ds.CheckForSkid();
        
    }
}