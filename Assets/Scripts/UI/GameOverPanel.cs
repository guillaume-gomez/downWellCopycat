using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameOverPanel : MonoBehaviour
{

  public GameObject firstSelectedButton;
  public GameObject panel;
  public GameObject line;
  private Dictionary<string, string> stats;

  void Awake()
  {
    stats = new Dictionary<string, string>();
    // ... Add some keys and values.
    stats.Add("level", GameManager.instance.LevelSystemRun.level.ToString());
    stats.Add("score", GameManager.instance.LevelSystemRun.score.ToString());
    stats.Add("max combo", GameManager.instance.LevelSystemRun.maxCombo.ToString());
    stats.Add("nb killed", GameManager.instance.LevelSystemRun.nbKilled.ToString());
    stats.Add("money",
    
    GameManager.instance.LevelSystem.money.ToString("#.00") + " + " +
    GameManager.instance.LevelSystemRun.money.ToString("#.00") + " * " +
    GameManager.instance.LevelSystemRun.saveMoneyPercentage.ToString("#.00") );
  }

  void Start()
  {

    foreach (var pair in stats)
    {
        Vector3 position = new Vector3(0f, 0f, 0f);
        GameObject obj = Instantiate(line, position, transform.rotation);
        obj.transform.SetParent(panel.transform);

        ScoreLine scoreLine = obj.GetComponent<ScoreLine>();
        scoreLine.SetStatsName(pair.Key);
        scoreLine.SetStatsScore(pair.Value);
    }

    firstSelectedButton.GetComponent<Selectable>().OnSelect(null);
    EventSystem.current.SetSelectedGameObject(firstSelectedButton);
  }

}
