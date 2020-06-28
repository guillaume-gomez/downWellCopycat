using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{

   public GameObject panel;
   public GameObject line;
   private Dictionary<string, string> stats;

    void Awake()
    {
        stats = new Dictionary<string, string>();
        // ... Add some keys and values.
        stats.Add("level", GameManager.instance.LevelSystem.level.ToString());
        stats.Add("score", GameManager.instance.LevelSystem.score.ToString());
        stats.Add("max combo", GameManager.instance.LevelSystem.maxCombo.ToString());
        stats.Add("nb killed", GameManager.instance.LevelSystem.nbKilled.ToString());
        stats.Add("money",
            GameManager.instance.GeneralStatistics.money + " + " +
            GameManager.instance.LevelSystem.money + " * " +
            GameManager.instance.GeneralStatistics.saveMoneyPercentage.ToString("#.00") );
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
    }

}
