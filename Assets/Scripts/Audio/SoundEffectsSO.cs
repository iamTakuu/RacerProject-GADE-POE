using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityExtensions;

    [CreateAssetMenu(fileName = "NewSoundEffect", menuName = "Audio/New Sound Effect")]
    public class SoundEffectsSO : ScriptableObject
    {
        [SerializeField] private SerializableDictionary<string, AudioClip> AudioLibrary;
        
        public AudioClip GetClip(string clipID)
        {
            return AudioLibrary[clipID];
        }
    }
