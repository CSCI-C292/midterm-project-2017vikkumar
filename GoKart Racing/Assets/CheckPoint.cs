using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    // have we been triggered?
    bool triggered;
    public static int laps = 0;
    void Awake()
    {
        triggered = false;
    }
    // called whenever another collider enters our zone (if layers match)
    void OnTriggerEnter(Collider collider)
    {
        Trigger();
    }
    void Trigger()
    {
        laps = laps + 1;
        Debug.Log(laps);
        triggered = true;
    }

}