﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "New RunTimeData")]
public class RuntimeData : ScriptableObject
{
    public string CurrentFoodMousedOver;
    public GameplayState CurrentGameplayState;

}
