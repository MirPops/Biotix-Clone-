using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AIBot : MonoBehaviour
{
    public float timeStepFactor;
    [SerializeField] private OwnerOfCell owner;
    private Player player;
    private List<Cell> forAtackCells;


    private void Start()
    {
        forAtackCells = new List<Cell>();

        StartCoroutine(AILogicRoutine());
    }

    private IEnumerator AILogicRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        player = PlayerManager.Instance.GetPlayer(owner);    /////////////////////////


        while (CellManager.AIBotCells.Count > 0)
        {
            float wait = CellManager.AIBotCells.Count;
            for (int i = 0; i < wait; i++)
                wait *= timeStepFactor;

            yield return new WaitForSeconds(2f);

            Cell cellForAtack = SelectCellForAtack();
            if (cellForAtack != null)
            {
                Cell targetCell = FindNearests(cellForAtack);
                cellForAtack.Atack(targetCell.SelectedAsTarget());
            }

        }
    }

    private Cell FindNearests(Cell from)
    {
        int index = 0;
        List<Cell> cells = new List<Cell>();

        cells.AddRange(CellManager.noneCells);
        cells.AddRange(CellManager.Player1Cells);

        float min = (cells[index].transform.position - from.transform.position).magnitude;

        for (int j = 1; j < cells.Count; j++)
        {
            float magn = (cells[j].transform.position - from.transform.position).magnitude;
            if (magn < min)
            {
                min = magn;
                index = j;
            }
        }

        return cells[index];
    }

    private Cell SelectCellForAtack()
    {
        List<int> indexes = new List<int>();
        for (int i = 0; i < CellManager.AIBotCells.Count; i++)
            indexes.Add(i);

        while (indexes.Count > 0)
        {
            int rand = Random.Range(0, indexes.Count);
            Cell cell = CellManager.AIBotCells[rand];

            if (cell.amountCells >= cell.maxAmountCells / 2)
                return cell;
            else
                indexes.Remove(rand);
        }
        return null;
    }
}
