using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public int amountCells = -1;
    public int maxAmountCells;
    public Player player;

    [SerializeField] private float offSetSpawnCells = 0.5f;
    [SerializeField] private float plusOneCellRate = 1.5f;
    [SerializeField] private float waitTimeRoutine = 1f;
    [SerializeField] private float minusCellRate = 0.8f;
    [SerializeField] private int minusForOverload = 2;
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

        FirstUpdateValue();
        UnSelecte();

        CellManager.OnCellCreate.Invoke(this);
    }


    // ����� ������
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


    // ��������� ���� �������
    public void Atack(Vector3 target, Player player)
    {
        if (player.owner != this.player.owner)
            return;

        int cells = (int)Mathf.Round((float)amountCells / 2);

        for (int i = 0; i < cells; i++)
        {
            Vector3 randomPos = new Vector3(transform.position.x + Random.Range(-offSetSpawnCells, offSetSpawnCells),
                transform.position.y + Random.Range(-offSetSpawnCells, offSetSpawnCells), transform.position.z);

            GameObject miniCell = Instantiate(atackCells, randomPos, Quaternion.identity, parentOfMiniCells.transform);

            miniCell.GetComponent<MiniCell>().Atack(target, player);
        }
        amountCells -= cells;
        UpdateValue();
    }


    // ��������� ���� �������
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


    // ������ ����� �� ������ � �������(����)
    public void DrawLine(Vector3 touchPos, Player player)
    {
        if (player.owner != this.player.owner) 
            return;

        line.enabled = true;
        Vector3[] positions = new Vector3[] { new Vector3(transform.position.x, transform.position.y), touchPos };
        line.SetPositions(positions);
    }


    // ������ ������
    private void CaptureCell(Player player)
    {
        CellManager.OnCellOwnerChanged?.Invoke(this, player.owner);

        this.player = player;
        cellCenter.color = this.player.color;
        StartCoroutine(PlusOneCellRoutine());
    }


    // ������ ���������� ������(��� �������)
    private void FirstUpdateValue()
    {
        cellCenter.color = player.color;
        if (player.owner != OwnerOfCell.None)
            StartCoroutine(PlusOneCellRoutine());

        if (amountCells == -1)
            amountCells = maxAmountCells / 2;
        else if (amountCells > maxAmountCells)
            amountCells = maxAmountCells;
        else if (amountCells == 0 || amountCells < 0)
        {
            amountOfCellsText.text = string.Empty;
            return;
        }

        amountOfCellsText.text = amountCells.ToString();
    }


    // ���������� ������
    private void UpdateValue()
    {
        if (amountCells == 0)
        {
            CellManager.OnCellOwnerChanged?.Invoke(this, OwnerOfCell.None);
            player = PlayerManager.NonePlayer;

            UnSelecte();
            cellCenter.color = player.color;
            amountOfCellsText.text = string.Empty;

            StopCoroutine(PlusOneCellRoutine());
            return;
        }
        else if (amountCells < 0)
            amountCells = 1;

        amountOfCellsText.text = amountCells.ToString();
    }


    // �� �������� ��������� �� 1 ������� ������ ��� �������� �� 2 ���� �������� ������ ������������� 
    private IEnumerator PlusOneCellRoutine()
    {
        yield return new WaitForSeconds(waitTimeRoutine);

        while (player.owner != OwnerOfCell.None)
        {
            if (amountCells < maxAmountCells)
            {
                amountCells++;
                UpdateValue();
                yield return new WaitForSeconds(plusOneCellRate);
            }
            else if (amountCells > maxAmountCells)
            {
                amountCells -= minusForOverload;
                UpdateValue();
                yield return new WaitForSeconds(minusCellRate);
            }
            else yield return new WaitForSeconds(waitTimeRoutine);
        }
    }
}
