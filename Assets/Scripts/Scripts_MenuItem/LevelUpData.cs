using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelUpData", menuName = "Game/LevelUpData")]
public class LevelUpData : ScriptableObject
{
    public List<LevelData> levelUpChoices;

    public List<LevelUpChoice> GetRandomChoicesForLevel(int level, int count = 3)
    {
        foreach (var data in levelUpChoices)
        {
            if (data.level == level)
            {
                List<LevelUpChoice> shuffled = new List<LevelUpChoice>(data.choices);
                for (int i = 0; i < shuffled.Count; i++)
                {
                    int randIndex = Random.Range(i, shuffled.Count);
                    var temp = shuffled[i];
                    shuffled[i] = shuffled[randIndex];
                    shuffled[randIndex] = temp;
                }
                return shuffled.GetRange(0, Mathf.Min(count, shuffled.Count));
            }
        }
        return null;
    }
}
