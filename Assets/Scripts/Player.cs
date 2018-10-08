using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

    public Inventory inventory;
    public float speed;

    public GameObject projectile;
    public GameObject gameController;

    private Rigidbody2D rb2d;

    public new bool TakeDamage(float damage) {
        bool died = base.TakeDamage(damage);
        // TODO: Handle player death here
        return died;
    }

    // Use this for initialization
    public new void Start () {
        base.Start();
        rb2d = GetComponent<Rigidbody2D>();
        inventory = new Inventory();
    }
	
	// Update is called once per frame
	void Update () {
        if (gameController.GetComponent<GameController>().roundOver) {
            return;
        }

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
        GameObject projectileObj = Instantiate(projectile, transform.position + direction * 1.5F, Quaternion.identity);
        projectileObj.GetComponent<Rigidbody2D>().velocity = direction * 50;
        projectileObj.GetComponent<ProjectileSpell>().damage = spell.damage;
    }
}
