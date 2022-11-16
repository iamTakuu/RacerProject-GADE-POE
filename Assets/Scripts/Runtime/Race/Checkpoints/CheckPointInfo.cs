using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ChPt", menuName = "Scriptable Object/Checkpoint", order = 0)]
public class CheckPointInfo : ScriptableObject
{
   public bool isTarget = false;
   [Header("Checkpoint Time Value")]
   public int timeToAdd;
}
