using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipBody : MonoBehaviour
{
    Rigidbody car;
    float lastTimeChecked;
    // Start is called before the first frame update
    void Start()
    {
        car = this.GetComponent<Rigidbody>();
    }
    void Reposition()
    {
        this.transform.position += Vector3.up;
        this.transform.rotation = Quaternion.LookRotation(this.transform.forward);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.up.y > 0.5f || car.velocity.magnitude > 1)
        {
            lastTimeChecked = Time.time;
        }
        if (lastTimeChecked + 4 < Time.time)
        {
            Reposition();
        }
    }
}
