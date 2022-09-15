using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Phrase", menuName = "Scriptable Object/Phrase", order = 2)]
//Simply the words the character will say.
public class Phrase : ScriptableObject
{
    [Header("Character SO")]
    public CharacterInfo Character;
    [Header("Words to say")]
    public string Sentence;
}
