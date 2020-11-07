using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    float _rotationSpeed = 180f;
    // Start is called before the first frame update
    [SerializeField] RuntimeData _runTimeData;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseEnter()
    {
        transform.Find("Spot Light").gameObject.SetActive(true);
        _runTimeData.CurrentFoodMousedOver = name;
    }
    void OnMouseOver()
    {
        transform.Find("Food Mesh").RotateAround(transform.position, Vector3.up, _rotationSpeed * Time.deltaTime);
    }
    void OnMouseExit()
    {
        transform.Find("Spot Light").gameObject.SetActive(false);
        _runTimeData.CurrentFoodMousedOver = "";
    }
}
