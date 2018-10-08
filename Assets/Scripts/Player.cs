using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public Inventory inventory;

    public int hp;
    public float speed;

    public GameObject projectile;

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

        // Cast Spells
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            UseSpell(0, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        if (Input.GetKeyDown(KeyCode.Mouse1)) {
            UseSpell(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void UseSpell(int spellIndex, Vector3 worldPoint) {
        Spell spell = inventory.equippedSpells[spellIndex];
        Vector3 direction = (worldPoint - transform.position).normalized;
        GameObject projectileObj = Instantiate(projectile, transform.position + direction * 1.2F, Quaternion.identity);
        projectileObj.GetComponent<Rigidbody2D>().velocity = direction * 10;
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
