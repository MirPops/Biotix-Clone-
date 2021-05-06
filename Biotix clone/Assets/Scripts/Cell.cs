using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public int amountCells;
    public OwnerOfCell startOwner;

    public Player player;

    [SerializeField] private int maxAmountCells;
    [SerializeField] private float plusOneCellRate;
    [SerializeField] private Color startColor;
    [Space(15)]
    [SerializeField] private LineRenderer line;
    [SerializeField] private GameObject parentOfMiniCells;
    [SerializeField] private GameObject atackCells;
    [SerializeField] private TMP_Text amountOfCellsText;
    [SerializeField] private Image cellCenter;
    [SerializeField] private Image selectedRing;


    private void Start()
    {
        OffLine();

        selectedRing.enabled = false;
        cellCenter.color = startColor;
        UpdateValue();

        player = new Player { owner = startOwner, color = startColor };

        if (player.owner != OwnerOfCell.None)
            StartCoroutine(PlusOneCellRoutine());
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
        line.enabled = false;
    }


    public Vector3 SelectedAsTarget()
    {
        return transform.position;
    }


    // Выпускает пацанов
    public void Atack(Vector3 target)
    {
        Player a = new Player { color = Color.blue, owner = OwnerOfCell.Player1 };    ///////////////////////// test


        int cells = (int)Mathf.Round((float)amountCells / 2);

        for (int i = 0; i < cells; i++)
        {
            Vector3 randomPos = new Vector3(transform.position.x + Random.Range(-0.5f, 0.5f), transform.position.y + Random.Range(-0.5f, 0.5f), transform.position.z);

            GameObject miniCell = Instantiate(atackCells, randomPos, Quaternion.identity, parentOfMiniCells.transform);
            //miniCell.transform.localPosition = new Vector3(miniCell.transform.localPosition.x, miniCell.transform.localPosition.y, 0);

            miniCell.GetComponent<MiniCell>().atack(target, a);
        }
        amountCells -= cells;
        UpdateValue();
    }


    // Cоприкaсается с пацанами
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
        this.player = player;
        cellCenter.color = this.player.color;
    }


    private void UpdateValue()
    {
        if (amountCells == 0)
        {
            amountOfCellsText.text = string.Empty;
            player = new Player { owner = OwnerOfCell.None, color = Color.white };
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


    public void OffLine()
        => line.enabled = false;
}
