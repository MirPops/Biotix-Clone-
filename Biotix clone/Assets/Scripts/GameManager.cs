using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    void Start()
    {
        Cursor.visible = false;
    }

    
    void Update()
    {
        
    }
}

public enum OwnerOfCell
{
    Player1,
    AIBot,
    None
}

public struct Player
{
    public OwnerOfCell owner;
    public Color color;
}
