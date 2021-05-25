using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBot : MonoBehaviour
{
    [Range(0.5f, 1f)]
    [SerializeField] private float timeStepFactor = 0.8f;
    [Range(0.5f, 1f)]
    [SerializeField] private float SelecteCellFactor = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float RandFactor = 0.5f;
    [SerializeField] private float startTimeStep = 2f;
    [SerializeField] private float minTimeStep = 0.5f;
    private Player player;


    private void Start()
    {
        player = PlayerManager.Instance.GetPlayer(OwnerOfCell.AIBot);
        StartCoroutine(AILogicRoutine());
    }


    private IEnumerator AILogicRoutine()
    {
        yield return new WaitForSeconds(1.5f);

        while (CellManager.AIBotCells.Count > 0)
        {
            yield return new WaitForSeconds(TimeStep());
            
            Cell cellForAtack = SelectCellForAtack();
            if (cellForAtack != null)
            {
                Cell targetCell;
                if (Random.value < RandFactor)
                    targetCell = FindRandom();
                else
                    targetCell = FindNearests(cellForAtack);

                if (targetCell != null)
                    cellForAtack.Atack(targetCell.SelectedAsTarget(), player);
            }
        }
    }


    // Ищет ближайщую не свою клетку от той что в параметрах
    private Cell FindNearests(Cell from)
    {
        int index = 0;
        List<Cell> cells = new List<Cell>();

        cells.AddRange(CellManager.NoneCells);
        cells.AddRange(CellManager.Player1Cells);

        if (cells.Count == 0) return null;

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


    // Ищет рандомную клетку
    private Cell FindRandom()
    {
        List<Cell> cells = new List<Cell>();

        cells.AddRange(CellManager.NoneCells);
        cells.AddRange(CellManager.Player1Cells);
        cells.AddRange(CellManager.AIBotCells);

        return cells[Random.Range(0, cells.Count)];
    }


    // Выбирает почти рандомно свою клетку для атаки, у которой больше запас клеток чем половина от максимального запаса(зависет от SelecteCellFactor)
    private Cell SelectCellForAtack()
    {
        List<int> indexes = new List<int>();
        for (int i = 0; i < CellManager.AIBotCells.Count; i++)
            indexes.Add(i);
        
        while (indexes.Count > 0)
        {
            int randIndex = Random.Range(0, indexes.Count);
            Cell cell = CellManager.AIBotCells[randIndex];

            float randFactor = Random.Range(0.5f, SelecteCellFactor);

            if (cell.amountCells >= (int)(cell.maxAmountCells * randFactor) / 2)
                return cell;
            else
                indexes.RemoveAt(randIndex);
        }
        return null;
    }


    // Чем больше клеток под контролем тем быстрее думает
    private float TimeStep()
    {
        float timeStep = startTimeStep;
        float cellsAmount = CellManager.AIBotCells.Count;
        for (int i = 0; i < cellsAmount; i++)
            timeStep *= timeStepFactor;

        return timeStep > minTimeStep ? timeStep : minTimeStep;
    }
}
