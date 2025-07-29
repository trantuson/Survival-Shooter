using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum StatType
{
    Health,
    Damage,
    Speed,
    FireRate,
    AddGun,
    Upgrade,
    Skill
}

[System.Serializable]
public class LevelUpChoice
{
    public Sprite icon; // Hình ảnh của lựa chọn
    public string description; // Mô tả lựa chọn
    public StatType statType;   // Loại chỉ số cần tăng
    public float amount;        // Số lượng tăng

    public List<StatModifier> modifiers; // ds các chỉ số bị ảnh hưởng
}
[System.Serializable]
public class StatModifier
{
    public StatType statType;
    public float amount; // số ấm để giảm
}
