using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

public class OptionMenu : MonoBehaviour
{
    public GameObject goBackButton;
    public GameObject firstSelectedButton;

   public void SetVolume(System.Single vol)
   {
        SoundManager.instance.SetMusicVolume((float) vol);
   }

   public void SetVFX(System.Single vol)
   {
        SoundManager.instance.SetVFXVolume((float) vol);
   }

    void OnDisable()
    {
       EventSystem.current.SetSelectedGameObject(goBackButton);
    }

    void OnEnable()
    {
        EventSystem.current.SetSelectedGameObject(firstSelectedButton);
    }
}
