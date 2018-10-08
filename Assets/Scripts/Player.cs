using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Inventory inventory;

    public int hp;
    public float speed;

    private Rigidbody2D rb2d;


    // Use this for initialization
    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        inventory = new Inventory();
    }
	
	// Update is called once per frame
	void Update () {
        rb2d.velocity = (new Vector2(Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical"))).normalized * speed * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
            Debug.Log("success");
        }
    }
}
