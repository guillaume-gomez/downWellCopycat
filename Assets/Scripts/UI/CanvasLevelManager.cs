﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasLevelManager : MonoBehaviour
{
    public GameObject endLevelPanel;
    public GameObject gameOverPanel;
    public GameObject pickedUpPanel;

    void Start()
    {
        if(LevelManager.instance)
        {
            LevelManager.instance.OnWin += OpenEndLevelPanel;
            LevelManager.instance.OnLose += OpenGameOverPanel;
            LevelManager.instance.OnPickedUp += OpenPickedUpPanel;

        }
    }

    public void OpenEndLevelPanel(object sender, System.EventArgs e)
    {
        endLevelPanel.SetActive(true);
        EndLevelPanel panel = endLevelPanel.GetComponent<EndLevelPanel>();
        panel.SetLevel(GameManager.instance.LevelSystemRun.level.ToString());
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

    public void OpenPickedUpPanel(object sender, OnPickedEventArgs e)
    {
        pickedUpPanel.SetActive(true);
        PickedUpPanel panel = pickedUpPanel.GetComponent<PickedUpPanel>();
        panel.SetItemName(e.item);
        Invoke("ClosePickedUpPanel", 2.0f);
    }

    public void ClosePickedUpPanel()
    {
        pickedUpPanel.SetActive(false);
    }

}
