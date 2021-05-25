using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private OwnerOfCell owner;
    [SerializeField] private Camera mainCamera;
    private List<Cell> selectedCells;
    private Player player;
    private Vector2 startPos;
    private Vector2 endPos;

    private void Start()
    {
        player = PlayerManager.Instance.GetPlayer(owner);
        selectedCells = new List<Cell>();
    }

    void Update()
    {
        if (Input.touchCount == 0)
        {
            if (selectedCells.Count > 0)
                for (int i = 0; i < selectedCells.Count; i++)
                    selectedCells[i].Offline();
            startPos = Vector2.zero;
            endPos = Vector2.zero;
            return;
        }

        // Рейкаст по тачу
        Touch touch = Input.GetTouch(0);
        Vector2 pos = mainCamera.ScreenToWorldPoint(touch.position);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);


        // Запись координат при начале и конце тача
        if (touch.phase == TouchPhase.Began)
            startPos = touch.position;
        else if (touch.phase == TouchPhase.Ended)
            endPos = touch.position;


        if (hit.transform != null)
        {
            Cell selectedCell = hit.transform.GetComponent<Cell>();
            if (selectedCell == null) return;

            // Механика тача и свайпа
            if (selectedCell.player.owner == player.owner && !selectedCells.Contains(selectedCell))
            {
                if ((startPos != touch.position) || (startPos == endPos))
                {
                    selectedCells.Add(selectedCell.SelectCell(player));
                }
            }
            else if (touch.phase == TouchPhase.Ended && selectedCells.Count > 0)
            {
                Atack(selectedCell);
            }
        }
        // При таче не по клетке выбранные клетки анулируются
        else if (startPos == endPos && selectedCells.Count > 0)
        {
            UnSelect();
        }
        
        // Прорисовка линии за тачем от всех выбранных клеток
        if (startPos != touch.position && selectedCells.Count > 0)
        {
            for (int i = 0; i < selectedCells.Count; i++)
            {
                selectedCells[i].DrawLine(pos, player);
            }
        }
    }


    // Если аткует свою клетку, то таргет клетке не атакует сама себя
    private void Atack(Cell cell)
    {
        Vector3 target = cell.SelectedAsTarget();

        if (selectedCells.Contains(cell))
        {
            if (selectedCells.Count == 1)
                return;

            selectedCells.Remove(cell);
            cell.UnSelecte();
        }

        for (int i = 0; i < selectedCells.Count; i++)
            selectedCells[i].Atack(target, player);
        UnSelect();
    }


    // Анселектид все выбранные клетки
    private void UnSelect()
    {
        foreach (Cell cell in selectedCells)
            cell.UnSelecte();

        selectedCells.Clear();
    }
}