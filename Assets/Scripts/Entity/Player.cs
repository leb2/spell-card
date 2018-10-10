using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity {

    public Inventory inventory;
    public float speed;

    public GameObject projectile;
    public GameObject coneArea;
    public GameObject lineArea;
    public GameObject circleArea;

    public GameObject gameController;

    private Rigidbody2D rb2d;

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

        Dictionary<ShapeType, GameObject> shapeAreaPrefabs = new Dictionary<ShapeType, GameObject>
        {
                {ShapeType.CONE, coneArea},
                {ShapeType.CIRCLE, circleArea},
                {ShapeType.LINE, lineArea},
                {ShapeType.PROJECTILE, projectile}
        };
        //Debug.Log(gameObject.GetComponent<Rigidbody2D>().position);
        Debug.Log(transform.position);
        GameObject spellObj = Instantiate(shapeAreaPrefabs[spell.shape], transform.position, Quaternion.identity);
        spellObj.GetComponents<SpellEffect>()[0].Cast(spell, worldPoint);
    }
}
