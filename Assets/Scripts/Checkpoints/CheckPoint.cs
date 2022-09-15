using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Phrase", menuName = "Scriptable Object/Checkpoint", order = 0)]
public class CheckPoint : ScriptableObject
{
   [Header("Checkpoint Time Value")]
   public int timeToAdd;
}
