using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLevelManager : MonoBehaviour
{
    public GameObject endLevelPanel;
    public GameObject gameOverPanel;


    public void OpenEndLevelPanel()
    {
        endLevelPanel.SetActive(true);
        EndLevelPanel panel = endLevelPanel.GetComponent<EndLevelPanel>();
        panel.SetLevel(LevelManager.instance.Level.ToString());
    }

    public void CloseEndLevelPanel()
    {
        endLevelPanel.SetActive(false);
    }

    public void OpenGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void CloseGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }
}
