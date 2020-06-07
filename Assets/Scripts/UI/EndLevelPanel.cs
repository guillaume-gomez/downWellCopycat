using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelPanel : MonoBehaviour
{
  public TextMeshProUGUI levelText;

  public void SetLevel(string level)
  {
    levelText.text = "You reach level " + level; //level + " - 125";
  }

}
