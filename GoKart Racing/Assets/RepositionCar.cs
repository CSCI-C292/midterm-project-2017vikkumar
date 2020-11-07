using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepositionCar : MonoBehaviour
{
    Rigidbody car;
    float lastUpdate;
    Vector3 wayPointRequired;
    Vector3 nextWaypoint;
    int indexTarget = 0;
    float totalDistanceToTarget;
    // Start is called before the first frame update
    void Start()
    {
        car = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.up.y > 0.5f || car.velocity.magnitude > 1)
        {
            lastUpdate = Time.time;
        }
        if (lastUpdate + 1 < Time.time)
        {
            this.transform.position += Vector3.up;
            this.transform.rotation = Quaternion.LookRotation(this.transform.forward);
        }
    }
}
