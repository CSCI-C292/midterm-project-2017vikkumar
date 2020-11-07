using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoRollCar : MonoBehaviour
{
    // Source https://stackoverflow.com/questions/62687708/how-to-set-center-of-gravity-on-prefab-child
    // Copy and Pasted Code from https://forum.unity.com/threads/how-to-make-a-physically-real-stable-car-with-wheelcolliders.50643/
    // to prevent car roll 
    // This script isnt critical to the game, but makes the car drive extremely stable
    
    public float antiRoll = 5000.0f;
    public WheelCollider LFront;
    public WheelCollider RFront;
    public WheelCollider LRear;
    public WheelCollider RRear;
    public GameObject centermass;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        rb.centerOfMass = centermass.transform.localPosition;
    }
    void preventRoll(WheelCollider leftSide, WheelCollider rightSide)
    {
        WheelHit hit;
        float travelL = 1.0f;
        float travelR = 1.0f;

        bool leftRoll = leftSide.GetGroundHit(out hit);
        if (leftRoll)
            travelL = (-leftSide.transform.InverseTransformPoint(hit.point).y - leftSide.radius) / leftSide.suspensionDistance;

        bool rightRoll = rightSide.GetGroundHit(out hit);
        if (rightRoll)
            travelR = (-rightSide.transform.InverseTransformPoint(hit.point).y - rightSide.radius) / rightSide.suspensionDistance;
        float antiRollForce = (travelL - travelR) * antiRoll;

        if (leftRoll)
            rb.AddForceAtPosition(leftSide.transform.up * -antiRollForce, leftSide.transform.position);
        if (rightRoll)
            rb.AddForceAtPosition(rightSide.transform.up * antiRollForce, rightSide.transform.position);

    }
    // Update is called once per frame
    void Update()
    {
        preventRoll(LFront, RFront);
        preventRoll(LRear, RRear);
    }
}
