using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    static public PlayerManager Instance { get; private set; }
    static public Player nonePlayer { get; private set; }

    [SerializeField] private Color colorOfPlayer1;
    [SerializeField] private Color colorOfBotAi;
    [SerializeField] private Color colorOfNonePlayer;
    private Dictionary<OwnerOfCell, Color> ColorOfPlayers;


    public Player GetPlayer(OwnerOfCell owner)
        => new Player { owner = owner, color = ColorOfPlayers[owner] };


    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);

        ColorOfPlayers = new Dictionary<OwnerOfCell, Color>()
        {
            { OwnerOfCell.Player1, colorOfPlayer1 },
            { OwnerOfCell.AIBot, colorOfBotAi },
            { OwnerOfCell.None, colorOfNonePlayer }
        };
        nonePlayer = new Player { owner = OwnerOfCell.None, color = colorOfNonePlayer };
    }
}
