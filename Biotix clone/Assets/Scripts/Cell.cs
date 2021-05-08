using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public int amountCells;
    public int maxAmountCells;
    public Player player;

    [SerializeField] private float offSetSpawnCells = 0.5f;
    [SerializeField] private float plusOneCellRate;
    [SerializeField] private OwnerOfCell startOwner = OwnerOfCell.None;
    [Space(15)]
    [SerializeField] private LineRenderer line;
    [SerializeField] private GameObject parentOfMiniCells;
    [SerializeField] private GameObject atackCells;
    [SerializeField] private TMP_Text amountOfCellsText;
    [SerializeField] private Image cellCenter;
    [SerializeField] private Image selectedRing;


    private void Start()
    {
        player = PlayerManager.Instance.GetPlayer(startOwner);
        cellCenter.color = player.color;

        UpdateValue();
        UnSelecte();

        if (player.owner != OwnerOfCell.None)
            StartCoroutine(PlusOneCellRoutine());

        CellManager.OnCellCreate.Invoke(this);
    }


    public Cell SelectCell(Player player)
    {
        if (this.player.owner == player.owner)
        {
            selectedRing.enabled = true;
            return this;
        }
        else return null;
    }


    public void UnSelecte()
    {
        selectedRing.enabled = false;
        Offline();
    }


    public Vector3 SelectedAsTarget()
        => transform.position;


    public void Offline()
        => line.enabled = false;


    // Выпускает пацанов
    public void Atack(Vector3 target)
    {
        int cells = (int)Mathf.Round((float)amountCells / 2);

        for (int i = 0; i < cells; i++)
        {
            Vector3 randomPos = new Vector3(transform.position.x + Random.Range(-offSetSpawnCells, offSetSpawnCells), 
                transform.position.y + Random.Range(-offSetSpawnCells, offSetSpawnCells), transform.position.z);

            GameObject miniCell = Instantiate(atackCells, randomPos, Quaternion.identity, parentOfMiniCells.transform);

            miniCell.GetComponent<MiniCell>().atack(target, player);
        }
        amountCells -= cells;
        UpdateValue();
    }


    // Cоприкaсается с пацанамиs
    public void TakeCells(int amount, Player player)
    {
        if (player.owner == this.player.owner)
            amountCells += amount;
        else
        {
            amountCells -= amount;
            if (amountCells < 0)
            {
                CaptureCell(player);
            }
        }
        UpdateValue();
    }


    private void CaptureCell(Player player)
    {
        StartCoroutine(PlusOneCellRoutine());
        CellManager.OnCellOwnerChanged?.Invoke(this, player.owner);

        this.player = player;
        cellCenter.color = this.player.color;
    }


    private void UpdateValue()
    {
        if (amountCells == 0)
        {
            CellManager.OnCellOwnerChanged?.Invoke(this, OwnerOfCell.None);
            player = PlayerManager.nonePlayer;

            cellCenter.color = player.color;
            amountOfCellsText.text = string.Empty;

            StopCoroutine(PlusOneCellRoutine());
            return;
        }
        else if (amountCells >= maxAmountCells)
            amountCells = maxAmountCells;
        else if (amountCells < 0)
            amountCells = 1;

        amountOfCellsText.text = amountCells.ToString();
    }


    private IEnumerator PlusOneCellRoutine()
    {
        yield return new WaitForSeconds(plusOneCellRate);

        if (player.owner != OwnerOfCell.None)
        {
            amountCells++;
            UpdateValue();
        }
        StartCoroutine(PlusOneCellRoutine());
    }


    public void DrawLine(Vector3 touchPos)
    {
        line.enabled = true;
        Vector3[] positions = new Vector3[] { new Vector3(transform.position.x, transform.position.y), touchPos };
        line.SetPositions(positions);
    }
}
