    public class MainState : MenuBaseState
    {
        public override void EnterState(MainMenuManager contextMenu)
        {
            contextMenu.mainMenuCanvas.gameObject.SetActive(true);
            contextMenu.mainMenuVCam.Priority += 1;
        }

        public override void ExitState(MainMenuManager contextMenu)
        {
            contextMenu.mainMenuCanvas.gameObject.SetActive(false);
            contextMenu.mainMenuVCam.Priority -= 1;

        }
    }
