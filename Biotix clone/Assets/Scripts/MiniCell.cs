using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiniCell : MonoBehaviour
{
    [SerializeField] private float speedFactor = 1.1f;
    [SerializeField] private float speed = 20f;
    [SerializeField] private Image cellCenter;
    [SerializeField] private Image cellRadius;
    [SerializeField] private Rigidbody2D rb;
    private Player player;
    private int amount = 1;
    private Vector2 target;



    public void atack(Vector2 target, Player player)
    {
        this.target = target;
        this.player = player;
        cellCenter.color = cellRadius.color = player.color;

        Vector2 pos = new Vector2(transform.position.x, transform.position.y);
        rb.velocity = (this.target - pos).normalized * (Random.Range(speed / speedFactor, speed * speedFactor));
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        Cell cell = collision.GetComponent<Cell>();

        if (cell.transform.position.x == target.x && cell.transform.position.y == target.y)
        {
            cell.TakeCells(amount, player);
            Destroy(gameObject);
        }
    }
}