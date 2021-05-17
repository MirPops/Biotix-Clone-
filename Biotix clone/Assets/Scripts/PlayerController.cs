using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private OwnerOfCell owner;
    [SerializeField] private Camera mainCamera;
    private Player player;
    private List<Cell> selectedCells;


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
            return;
        }

        Touch touch = Input.GetTouch(0);
        Vector2 pos = mainCamera.ScreenToWorldPoint(touch.position);
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

        if (hit.transform != null)
        {
            Cell selectedCell = hit.transform.GetComponent<Cell>();
            if (selectedCell == null) return;

            //if ((touch.phase == TouchPhase.Began || isTouchMoved(touch)) && selectedCell.player.owner == player.owner && !selectedCells.Contains(selectedCell))
            //{
            //    selectedCells.Add(selectedCell.SelectCell(player));
            //    return;
            //}
            //else if (selectedCells.Count > 0 && ((touch.phase == TouchPhase.Began && !isTouchMoved(touch)) || (touch.phase == TouchPhase.Ended && isTouchMoved(touch))))
            //{
            //    Atack(selectedCell);
            //}

            // Свайп
            if (touch.deltaPosition.magnitude > 0f)
            {
                if (touch.phase == TouchPhase.Moved && selectedCell.player.owner == player.owner && !selectedCells.Contains(selectedCell))
                    selectedCells.Add(selectedCell.SelectCell(player));
                else if (touch.phase == TouchPhase.Ended && selectedCells.Count > 0)
                    Atack(selectedCell);

            }
            // Просто тач
            else if (touch.phase == TouchPhase.Ended)
            {
                if (selectedCell.player.owner == player.owner && !selectedCells.Contains(selectedCell))
                    selectedCells.Add(selectedCell.SelectCell(player));
                else if (selectedCells.Count > 0)
                    Atack(selectedCell);
            }
        }
        else if (touch.phase == TouchPhase.Ended && touch.deltaPosition.magnitude == 0)
        {
            if (selectedCells.Count > 0)
                UnSelect();
        }

        if (selectedCells.Count > 0)
        {
            for (int i = 0; i < selectedCells.Count; i++)
            {
                selectedCells[i].DrawLine(pos);
            }
        }
    }

    private void UnSelect()
    {
        foreach (Cell cell in selectedCells)
            cell.UnSelecte();

        selectedCells.Clear();
    }

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
            selectedCells[i].Atack(target);
        UnSelect();
    }

    private bool isTouchMoved(Touch touch)
        => touch.deltaPosition.magnitude > 1.5f;
}
