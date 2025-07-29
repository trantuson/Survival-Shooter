using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public int level; // Cấp độ
    public List<LevelUpChoice> choices; // Các lựa chọn của cấp độ này
}
