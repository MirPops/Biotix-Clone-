using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniCell : MonoBehaviour
{
    [SerializeField] private Image cellCenter;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    private Vector3 target;
    public Player player;
    public int amount = 1;



    public void atack(Vector2 target, Player player)
    {
        this.target = target;
        this.player = player;
        cellCenter.color = player.color;

        rb.velocity = (this.target - transform.position).normalized * speed;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Cell cell = collision.GetComponent<Cell>();

        if (cell.transform.position == target)
        {
            cell.TakeCells(amount, this.player);
            Destroy(gameObject);
        }
    }
}
