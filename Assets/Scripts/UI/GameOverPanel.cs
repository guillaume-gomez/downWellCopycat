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
        stats.Add("level", LevelManager.instance.Level.ToString());
        stats.Add("score", LevelManager.instance.Score.ToString());
        stats.Add("max combo", LevelManager.instance.MaxCombo.ToString());
        stats.Add("nb killed", LevelManager.instance.NbKilled.ToString());
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
