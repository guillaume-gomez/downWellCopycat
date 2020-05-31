using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPanel : MonoBehaviour
{

   public GameObject panel;
   public GameObject line;

    // Start is called before the first frame update
    void Start()
    {
        string[] test = new string[5]{"maxime", "jonathan", "bastien", "nicolas", "pascal"};
        for(int i = 0; i < test.Length; ++i)
        {
            Vector3 position = new Vector3(0f, 0f, 0f);
            GameObject obj = Instantiate(line, position, transform.rotation);
            obj.transform.SetParent(panel.transform);

            ScoreLine scoreLine = obj.GetComponent<ScoreLine>();
            scoreLine.SetStatsName(test[i]);
            scoreLine.SetStatsScore(test[i]);
        }
    }

}
