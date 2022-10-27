    public class SettingsState : MenuBaseState
    {
        public override void EnterState(MainMenuManager contextMenu)
        {
            contextMenu.settingsMenuCanvas.gameObject.SetActive(true);
            contextMenu.settingsMenuVCam.Priority += 1;
        }

        public override void ExitState(MainMenuManager contextMenu)
        {
            contextMenu.settingsMenuCanvas.gameObject.SetActive(false);
            contextMenu.settingsMenuVCam.Priority -= 1;
        }
    }
