using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniCell1 : MonoBehaviour
{
    [SerializeField] private SpriteRenderer cellCenter;
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
        Cell1 cell = collision.GetComponent<Cell1>();

        if (cell.transform.position == target)
        {
            cell.TakeCells(amount, this.player);
            Destroy(gameObject);
        }
    }
}
