using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PickedUpPanel : MonoBehaviour
{
  public TextMeshProUGUI pickedUpText;

  public void SetItemName(string item)
  {
    pickedUpText.text = "You picked " + item;
  }

}
