using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FoodLabel : MonoBehaviour
{
    [SerializeField] RuntimeData _runTimeData;

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = _runTimeData.CurrentFoodMousedOver;
    }
}
