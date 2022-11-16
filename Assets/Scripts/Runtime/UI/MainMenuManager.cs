using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
        [Header("Cameras For Settings")] 
        public CinemachineVirtualCamera mainMenuVCam;
        public CinemachineVirtualCamera settingsMenuVCam;

        [Space] [Header("Canvas To Use")] [SerializeField]
        public Canvas mainMenuCanvas; 
        public Canvas settingsMenuCanvas;
        
        [Space] [Header("Buttons")] 
        public Button settingsBack;
        public Button settingsGo;
        
        private MenuBaseState currentState;
        public MainState MainState = new MainState();
        public SettingsState SettingsState = new SettingsState();

        private void Start()
        {
            settingsBack.onClick.AddListener((() => SwitchState(MainState)));
            settingsGo.onClick.AddListener((() => SwitchState(SettingsState)));
            
            currentState = MainState;
            currentState.EnterState(this);
        }

        private void SwitchState(MenuBaseState state)
        {
            currentState.ExitState(this);
            currentState = state;
            state?.EnterState(this);
        }
    }
