using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Color colorOfPlayer;
    private Player player;
    private List<Cell> selectedCells;


    private void Start()
    {
        player = new Player { owner = OwnerOfCell.Player1, color = colorOfPlayer };
        selectedCells = new List<Cell>();
    }


    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            RaycastHit2D hit = Physics2D.Raycast(touch.position, Vector2.zero);
            if (hit.transform != null)
            {
                Cell selectedCell = hit.transform.GetComponent<Cell>();

                if ((touch.phase == TouchPhase.Began || touch.phase == TouchPhase.Moved) && !selectedCells.Contains(selectedCell))
                {
                    selectedCells.Add(selectedCell);
                    selectedCell.SelectCell(player);

                    print("touch began or moved");
                    print(selectedCells.Count);
                }
                else if ((touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled) && selectedCell.owner != player.owner)
                {
                    print("touch ended or canceled");


                    Vector2 target = selectedCell.SelectedAsTarget();
                    foreach (Cell cell in selectedCells)
                    {
                        cell.Atack(target);
                    }
                }
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                print("delete selected");

                if (selectedCells.Count > 0)
                {
                    foreach (Cell cell in selectedCells)
                    {
                        cell.UnSelecte();
                    }
                    selectedCells.Clear();
                }
            }
        }
    }
}
