using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public class SoundEffectsHandler : MonoBehaviour
    {
        [SerializeField]private SoundEffectsSO _effectsSo;
        [SerializeField]private AudioSource _source;

        public static SoundEffectsHandler Instance{ get; private set; }
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
            _effectsSo.Initialize();
            DontDestroyOnLoad(this.gameObject);
        }

        public void PlayEffect(string effectID)
        {
            _source.clip = _effectsSo.GetClip(effectID);
            _source.Play();
        }
    }
