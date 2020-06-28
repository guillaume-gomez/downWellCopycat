using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLevelManager : MonoBehaviour
{
    public GameObject endLevelPanel;
    public GameObject gameOverPanel;

    void Awake()
    {
        if(LevelManager.instance)
        {
            LevelManager.instance.OnWin += OpenEndLevelPanel;
            LevelManager.instance.OnLose += OpenGameOverPanel;

        }
    }

    public void OpenEndLevelPanel(object sender, System.EventArgs e)
    {
        endLevelPanel.SetActive(true);
        EndLevelPanel panel = endLevelPanel.GetComponent<EndLevelPanel>();
        panel.SetLevel(GameManager.instance.LevelSystem.level.ToString());
        Invoke("CloseEndLevelPanel", 2.0f);
    }

    public void CloseEndLevelPanel()
    {
        endLevelPanel.SetActive(false);
    }

    public void OpenGameOverPanel(object sender, System.EventArgs e)
    {
        gameOverPanel.SetActive(true);
        Invoke("CloseGameOverPanel", 5.0f);
    }

    public void CloseGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }
}
