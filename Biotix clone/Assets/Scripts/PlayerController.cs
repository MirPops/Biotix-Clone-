using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (Input.touchCount > 0)
        {
            // Первоначальный план
            //Touch touch = Input.GetTouch(0);
            //Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);
            //RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            // Альтернатива для большей точности касания
            Vector2 pos = Vector2.zero;                              
            RaycastHit2D hit = new RaycastHit2D();
            Touch touch = Input.GetTouch(0);
            for (int i = 0; i < Input.touchCount; i++)
            {
                touch = Input.GetTouch(i);
                pos = mainCamera.ScreenToWorldPoint(touch.position);
                hit = Physics2D.Raycast(pos, Vector2.zero);
                if (hit.transform != null)
                    break;
            }

            if (selectedCells.Count > 0)
            {
                for (int i = 0; i < selectedCells.Count; i++)
                {
                    selectedCells[i].DrawLine(pos);
                }
            }


            if (hit.transform != null)
            {
                Cell selectedCell = hit.transform.GetComponent<Cell>();
                if (selectedCell == null) return;

                if ((touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved) && selectedCell.player.owner == player.owner && !selectedCells.Contains(selectedCell))
                {
                    selectedCells.Add(selectedCell.SelectCell(player));
                    return;
                }
                else if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Began) && selectedCell.player.owner != player.owner)
                {
                    Vector3 target = selectedCell.SelectedAsTarget();
                    target.z = 0;

                    foreach (Cell cell in selectedCells)
                    {
                        cell.Atack(target);
                    }
                    UnSelect();
                }
            }
            else if (touch.phase == TouchPhase.Began)
            {
                if (selectedCells.Count > 0)
                {
                    UnSelect();
                }
            }
        }
        else if (selectedCells.Count > 0)
        {
            for (int i = 0; i < selectedCells.Count; i++)
            {
                selectedCells[i].Offline();
            }
        }
    }

    private void UnSelect()
    {
        foreach (Cell cell in selectedCells)
        {
            cell.UnSelecte();
        }
        selectedCells.Clear();
    }
}
