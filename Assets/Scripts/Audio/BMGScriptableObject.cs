using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityExtensions;

[CreateAssetMenu(fileName = "BMG_SO", menuName = "Audio/New BMG")]
public class BMGScriptableObject : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<int, AudioClip> SongLibrary;

        public int GetCount()
        {
            return SongLibrary.Count;
        }
        public AudioClip GetSong(int clipID)
        {
            return SongLibrary[clipID];
        }
    }
