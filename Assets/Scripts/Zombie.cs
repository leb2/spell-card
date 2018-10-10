using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy {

    public float speed;
    public float damage = 10;

    private Rigidbody2D rb2d;
    private GameObject player;

    // Use this for initialization
    public new void Start()
    {
        base.Start();
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        Vector2 dirVector = new Vector2(player.transform.position.x - transform.position.x, player.transform.position.y - transform.position.y);
        rb2d.velocity = dirVector.normalized * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
