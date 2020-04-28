using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionMenu : MonoBehaviour
{
   public void SetVolume(System.Single vol)
   {
        SoundManager.instance.SetMusicVolume((float) vol);
   }

   public void SetVFX(System.Single vol)
   {
        SoundManager.instance.SetVFXVolume((float) vol);
   }
}
