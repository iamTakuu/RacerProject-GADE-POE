using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[CreateAssetMenu(fileName = "New Character", menuName = "Scriptable Object/CharacterINF", order = 1)]

//Holds the info of any possible character that will speak.
public class CharacterInfo : ScriptableObject
{
    [Header("Sprite required for Portrait")]
    public Sprite Portrait;
    [Header("Character Name")]
    public string Name;
}
