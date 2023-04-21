using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseMenu : UICanvas
{
    public void ReplayButton()
    {
        NewUIManager.GetInstance().OpenUI<PlayingMenu>();
        LevelManager.GetInstance().Replay();
        Close(0);
    }
    public void QuitButton()
    {
        NewUIManager.GetInstance().OpenUI<MainMenu>();
        Close(0);
    }
}
