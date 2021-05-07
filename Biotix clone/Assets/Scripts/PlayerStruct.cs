public enum OwnerOfCell
{
    Player1,
    AIBot,
    None
}

public struct Player
{
    public OwnerOfCell owner;
    public UnityEngine.Color color;
}
