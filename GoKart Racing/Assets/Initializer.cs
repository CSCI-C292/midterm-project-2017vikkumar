using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] RuntimeData _runTimeData;
    [SerializeField] Dialogue _startingDialogue;
    // Start is called before the first frame update
    void Awake()
    {
        _runTimeData.CurrentGameplayState = GameplayState.InDialog;
        _runTimeData.CurrentFoodMousedOver = "";

    }
    void Start()
    {
        GameEvents.InvokeDialogInitiated(_startingDialogue);
    }

}
