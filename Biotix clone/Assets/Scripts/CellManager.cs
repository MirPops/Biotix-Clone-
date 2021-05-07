using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellManager : MonoBehaviour
{
    public static List<Cell> noneCells;
    public static List<Cell> Player1Cells;
    public static List<Cell> AIBotCells;
    public static System.Action<Cell> OnCellCreate;
    public static System.Action<Cell, OwnerOfCell> OnCellOwnerChanged;

    private void Awake()
    {
        noneCells = new List<Cell>();
        Player1Cells = new List<Cell>();
        AIBotCells = new List<Cell>();
        OnCellCreate += PlusCell;
        OnCellOwnerChanged += CellOwnerChanged;
    }

    private void CellOwnerChanged(Cell cell,  OwnerOfCell newOwner)
    {
        GetCorrectArr(cell.player.owner).Remove(cell);
        GetCorrectArr(newOwner).Add(cell);
    }

    private List<Cell> GetCorrectArr(OwnerOfCell owner)
    {
        switch (owner)
        {
            case OwnerOfCell.Player1:
                return Player1Cells;
            case OwnerOfCell.AIBot:
                return AIBotCells;
            case OwnerOfCell.None:
                return noneCells;
            default:
                {
                    print("Warrior!!! UnOwned Cell, be carefull!!!");
                    return null;
                }
        }
    }

    private void PlusCell(Cell cell)
        => GetCorrectArr(cell.player.owner).Add(cell);
}
