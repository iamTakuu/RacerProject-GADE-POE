using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private float scaleMultiplier = 1.1f;
        private Vector3 defaultScale;
        [SerializeField]private SoundEffectsHandler sfxHandler;

        private void Start()
        {
            defaultScale = transform.localScale;
            //sfxHandler = FindObjectOfType<SoundEffectsHandler>();
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.localScale *= scaleMultiplier;
            sfxHandler.PlayEffect("Hover");
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = defaultScale;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            transform.localScale = defaultScale;
            sfxHandler.PlayEffect("Click");
        }
    }
