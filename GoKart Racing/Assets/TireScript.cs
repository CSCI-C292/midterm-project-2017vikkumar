using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TireScript : MonoBehaviour
{
    public WheelCollider[] WCs;
    public GameObject[] Wheels;

    public AudioSource skidSound;
    public Transform SkidTrailPrefab;
    Transform[] skidTrails = new Transform[4];

    public ParticleSystem smokePrefab;
    ParticleSystem[] skidSmoke = new ParticleSystem[4];
    public GameObject brakeLight;

    public Rigidbody rb;
    public float currentSpeed { get { return rb.velocity.magnitude * 3; } }

    

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            skidSmoke[i] = Instantiate(smokePrefab);
            skidSmoke[i].Stop();
        }
        brakeLight.SetActive(false);
    }

    public void CheckForSkid()
    {
        int numSkidding = 0;
        for (int i = 0; i < 4; i++)
        {
            WheelHit wheelHit;
            WCs[i].GetGroundHit(out wheelHit);
            if (Mathf.Abs(wheelHit.forwardSlip) >= 0.4f || Mathf.Abs(wheelHit.sidewaysSlip) >= 0.4f)
            {
                numSkidding++;
                if (!skidSound.isPlaying)
                {
                    skidSound.Play();
                }
                if (skidTrails[i] == null)
                {
                    skidTrails[i] = Instantiate(SkidTrailPrefab);
                }
                skidTrails[i].parent = WCs[i].transform;
                skidTrails[i].localRotation = Quaternion.Euler(90, 0, 0);
                skidTrails[i].localPosition = -Vector3.up * WCs[i].radius;
                skidSmoke[i].transform.position = WCs[i].transform.position - WCs[i].transform.up * WCs[i].radius;
                skidSmoke[i].Emit(1);
            }
            else
            {
                if (skidTrails[i] == null)
                    return;
                Transform holder = skidTrails[i];
                skidTrails[i] = null;
                holder.parent = null;
                holder.rotation = Quaternion.Euler(90, 0, 0);
                Destroy(holder.gameObject, 30);
            }
        }
        if (numSkidding == 0 && skidSound.isPlaying)
        {
            skidSound.Stop();
        }
    }

    public void Go(float a, float turn, float deceleration)
    {
        if(!RaceStart.raceStart) return;
        a = Mathf.Clamp(a, -1, 1);
        
        

        if (deceleration != 0)
            brakeLight.SetActive(true);
        else
            brakeLight.SetActive(false);

       

        for (int i = 0; i < 4; i++)
        {
            WCs[i].motorTorque = a * 200;;

            if (i < 2)
                WCs[i].steerAngle = Mathf.Clamp(turn, -1, 1) * 30;
            else
                WCs[i].brakeTorque = Mathf.Clamp(deceleration, 0, 1) * 500;

            Quaternion quat;
            Vector3 position;
            WCs[i].GetWorldPose(out position, out quat);
            Wheels[i].transform.position = position;
            Wheels[i].transform.localRotation = quat;
        }
    }

    // Update is called once per frame

}
