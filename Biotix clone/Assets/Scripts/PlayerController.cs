using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Color colorOfPlayer;
    private Player player;
    private List<Cell1> selectedCells;


    private void Start()
    {
        player = new Player { owner = OwnerOfCell.Player1, color = colorOfPlayer };
        selectedCells = new List<Cell1>();
    }


    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 pos = Camera.main.ScreenToWorldPoint(touch.position);

            RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);

            if (selectedCells.Count > 0)
            {
                for (int i = 0; i < selectedCells.Count; i++)
                {
                    selectedCells[i].DrawLine(pos);
                }
            }


            if (hit.transform != null)
            {
                Cell1 selectedCell = hit.transform.GetComponent<Cell1>();

                if ((touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved) && selectedCell.player.owner == player.owner && !selectedCells.Contains(selectedCell))
                {
                    selectedCells.Add(selectedCell.SelectCell(player));
                    return;
                }
                else if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Began) && selectedCell.player.owner != player.owner)
                {
                    Vector3 target = selectedCell.SelectedAsTarget();
                    target.z = 0;

                    foreach (Cell1 cell in selectedCells)
                    {
                        cell.Atack(target);
                    }
                    UnSelect();
                }
            }
            //else if (selectedCells.Count > 0)
            //{
            //    for (int i = 0; i < selectedCells.Count; i++)
            //    {
            //        Vector3 pos = new Vector3(touch.position.x, touch.position.y, 0);
            //        selectedCells[i].DrawLine(pos);
            //    }
            //}
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
                selectedCells[i].OffLine();
            }
        }
    }

    private void UnSelect()
    {
        foreach (Cell1 cell in selectedCells)
        {
            cell.UnSelecte();
        }
        selectedCells.Clear();
    }
}
