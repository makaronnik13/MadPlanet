
[System.Serializable]
public class DialoguePhrase
{
    public enum PortraitSide
    {
        Left,
        Right
    }

    public Localization.LocalizedString Text;
    public DialogueCharacter Char;
    public PortraitSide Side;
}