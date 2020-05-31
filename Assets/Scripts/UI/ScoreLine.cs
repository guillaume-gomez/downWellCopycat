using TMPro;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreLine : MonoBehaviour
{
    public TextMeshProUGUI statsName;
    public TextMeshProUGUI statsScore;

    public void SetStatsName(string value)
    {
        statsName.text = value;
    }

    public void SetStatsScore(string value)
    {
        statsScore.text = value;
    }
}
