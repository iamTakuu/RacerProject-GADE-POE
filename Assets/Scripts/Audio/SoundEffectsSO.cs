using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityExtensions;

    [CreateAssetMenu(fileName = "NewSoundEffect", menuName = "Audio/New Sound Effect")]
    public class SoundEffectsSO : ScriptableObject
    {
        //[SerializeField] private SerializableDictionary<string, AudioClip> AudioLibrary;
        [SerializeField] private List<AudioClip> AudioLibrary;
        private CustomHashMap<string, AudioClip> audioHashMap;

        /// <summary>
        /// Used to add all the required AudioClips to the HashMap.
        /// </summary>
        public void Initialize()
        {
            audioHashMap = new CustomHashMap<string, AudioClip>();
            foreach (var clip in AudioLibrary)
            {
                audioHashMap.Add(clip.name, clip);
            }
        }

        public AudioClip GetClip(string clipID)
        {
            return audioHashMap.GetValue(clipID);
        }
    }
