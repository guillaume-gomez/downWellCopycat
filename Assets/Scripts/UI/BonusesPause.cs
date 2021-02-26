using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class BonusesPause : MonoBehaviour
{
  public GameObject[] bonusItems;

  void Start()
  {
    if(GameManager.instance != null)
    {
      // initial bonuses
      foreach(Bonus bonus in GameManager.instance.LevelSystemRun.bonuses)
      {
        RenderBonus(bonus);
      }
    }
  }

  void RenderBonus(Bonus bonus)
  {
    Transform itemsParent = GameObject.Find("Bonuses").transform;
    Vector3 position = new Vector3(0f, 0f, 0f);
    GameObject obj = Instantiate(bonusItems[GetBonusIndex(bonus)], position, transform.rotation);
    obj.transform.SetParent(itemsParent, false);
    obj.GetComponent<Button>().interactable = false;
  }

  int GetBonusIndex(Bonus bonus)
  {
    switch(bonus)
    {
      case Bonus.ExtraAmmo:
        return 0;
      case Bonus.ExtraLife:
        return 1;
      case Bonus.JumpTakeOff:
        return 2;
      case Bonus.MaxSpeed:
        return 3;
      case Bonus.Protect:
        return 4;
      default:
        return 0;
    }
  }
}