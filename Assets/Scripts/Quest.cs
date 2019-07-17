
using com.armatur.common.flags;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quest")]
public class Quest: ScriptableObject
{
    public Localization.LocalizedString Title;
    public int MaxValue;
    public Sprite Icon;
}