using UnityEngine;

[CreateAssetMenu(fileName = "DialogueCharacter", menuName = "DialogueSystem/Character")]
public class DialogueCharacter: ScriptableObject
{
    public Localization.LocalizedString CharacterName;
    public Sprite Portrait;
}