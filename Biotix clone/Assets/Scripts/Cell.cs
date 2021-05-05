using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public int amountCells;
    public OwnerOfCell owner;
    [HideInInspector] public bool selected;

    [SerializeField] private int maxAmountCells;
    [SerializeField] private float plusOneCellRate;
    [SerializeField] private Color startColor;
    [Space(15)]
    [SerializeField] private TMP_Text AmountOfCellsText;
    [SerializeField] private Image cellCenter;
    [SerializeField] private Image selectedRing;


    private void Start()
    {
        selectedRing.enabled = false;
        cellCenter.color = startColor;
        UpdateValue();
    }


    public Cell SelectCell(Player player)
    {
        if (owner == player.owner)
        {
            selected = true;
            selectedRing.enabled = true;
            return this;
        }
        else return null;
    }


    public void UnSelecte()
    {
        selected = false;
        selectedRing.enabled = false;
    }


    public Vector2 SelectedAsTarget()
    {
        return new Vector2(transform.position.x, transform.position.y);
    }


    public void test()
        => print("test cell");


    public void Atack(Vector2 target)
    {
        // выпускает пацанов
    }


    public void TakeDamage()
    {
        // соприкосается с пацанами
    }


    private void CaptureCell()
    {
        StartCoroutine(PlusOneCellRoutine());
    }


    private void UnCaptureCell()
    {
        StartCoroutine(PlusOneCellRoutine());
    }


    private void UpdateValue()
    {
        if (amountCells == 0)
        {
            AmountOfCellsText.text = string.Empty;
            owner = OwnerOfCell.None;
            StopCoroutine(PlusOneCellRoutine());
            return;
        }
        else if (amountCells >= maxAmountCells)
            amountCells = maxAmountCells;

        AmountOfCellsText.text = amountCells.ToString();
    }


    private IEnumerator PlusOneCellRoutine()
    {
        yield return new WaitForSeconds(plusOneCellRate);

        if (owner != OwnerOfCell.None)
        {
            amountCells++;
            UpdateValue();
        }
        StartCoroutine(PlusOneCellRoutine());
    }
}
