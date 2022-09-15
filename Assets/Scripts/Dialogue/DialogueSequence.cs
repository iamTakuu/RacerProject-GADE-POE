using UnityEngine;

[CreateAssetMenu(fileName = "New Dialogue Sequence", menuName = "Scriptable Object/Dialogue Sequence", order = 0)]

//This class holds all the DialogueSO that allow characters to speak
public class DialogueSequence : ScriptableObject
{
    public Phrase[] Phrases;
}
