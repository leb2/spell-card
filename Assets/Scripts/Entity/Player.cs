using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : Entity {

    public Inventory inventory;

    //instantiates all spell types 
    public GameObject fireProjectile;
    public GameObject iceProjectile;
    public GameObject rotProjectile;
    public GameObject fireCone;
    public GameObject iceCone;
    public GameObject rotCone;
    public GameObject fireLine;
    public GameObject iceLine;
    public GameObject rotLine;
    public GameObject fireCircle;
    public GameObject iceCircle;
    public GameObject rotCircle;

    public Slider HPBar;
    public int[] spellCooldowns = { 0, 0 };

    public Animator animator;

    public GameObject gameController;

    private Rigidbody2D rb2d;

    // Use this for initialization
    public new void Start () {
        base.Start();
        rb2d = GetComponent<Rigidbody2D>();
        HPBar.maxValue = maxHp;
        HPBar.value = hp;
        inventory = new Inventory();
    }
	
	// Update is called once per frame
	public override void Update () {
        base.Update();

        if (gameController.GetComponent<GameController>().roundOver) {
            return;
        }

        transform.position = new Vector3(transform.position.x + Input.GetAxisRaw("Horizontal") * speed,
            transform.position.y + Input.GetAxisRaw("Vertical") * speed, 0);
        //transform.position.y = transform.position.x + Input.GetAxisRaw("Vertical") * speed;
        //(new Vector2(Input.GetAxisRaw("Horizontal"),
        //    Input.GetAxisRaw("Vertical"))).normalized * speed * Time.deltaTime, ForceMode2D.Impulse);

        //update animator with position info
        animator.SetFloat("HorzSpeed", Input.GetAxisRaw("Horizontal") * speed);
        animator.SetFloat("VertSpeed", Input.GetAxisRaw("Vertical") * speed);
        animator.SetFloat("health", hp);

        // Cast Spells
        if (Input.GetKeyDown(KeyCode.Mouse0) && spellCooldowns[0] < 1) {
            UseSpell(0, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            spellCooldowns[0] = inventory.equippedSpells[0].cooldown;
        }
        if (Input.GetKeyDown(KeyCode.Mouse1) && spellCooldowns[1] < 1) {
            UseSpell(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));
            spellCooldowns[1] = inventory.equippedSpells[1].cooldown;
        }

        // decrement cooldowns
        for (int i = 0; i < spellCooldowns.Length; i++) {
            if (spellCooldowns[i] > 0){
                spellCooldowns[i]--;
            }
        }
    }

    private void UseSpell(int spellIndex, Vector3 worldPoint) {
        Spell spell = inventory.equippedSpells[spellIndex];

        Dictionary<ElementType, GameObject> conePrefabs = new Dictionary<ElementType, GameObject>
        {
                {ElementType.FIRE, fireCone},
                {ElementType.ICE, iceCone},
                {ElementType.ROT, rotCone},
        };

        Dictionary<ElementType, GameObject> circlePrefabs = new Dictionary<ElementType, GameObject>
        {
                {ElementType.FIRE, fireCircle},
                {ElementType.ICE, iceCircle},
                {ElementType.ROT, rotCircle},
        };

        Dictionary<ElementType, GameObject> linePrefabs = new Dictionary<ElementType, GameObject>
        {
                {ElementType.FIRE, fireLine},
                {ElementType.ICE, iceLine},
                {ElementType.ROT, rotLine},
        };

        Dictionary<ElementType, GameObject> projectilePrefabs = new Dictionary<ElementType, GameObject>
        {
                {ElementType.FIRE, fireProjectile},
                {ElementType.ICE, iceProjectile},
                {ElementType.ROT, rotProjectile},
        };

        Dictionary<ShapeType, Dictionary<ElementType, GameObject>> shapeAreaPrefabs = new Dictionary<ShapeType, Dictionary<ElementType, GameObject>>
        {
                {ShapeType.CONE, conePrefabs},
                {ShapeType.CIRCLE, circlePrefabs},
                {ShapeType.LINE, linePrefabs},
                {ShapeType.PROJECTILE, projectilePrefabs}
        };
        //Debug.Log(gameObject.GetComponent<Rigidbody2D>().position);
        //Debug.Log(transform.position);
        GameObject spellObj = Instantiate(shapeAreaPrefabs[spell.shape][spell.elementType], transform.position, Quaternion.identity);
        spellObj.GetComponents<SpellEffect>()[0].Cast(spell, worldPoint);
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            //Debug.Log((transform.position - other.transform.position) * 100.0f);
            rb2d.AddForce((transform.position - other.transform.position) * 30.0f, ForceMode2D.Impulse);
        }
    }

    public override bool TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        HPBar.value -= damage;
        return false; 
    }

    public override void Die()
    {
            base.Die();
            SceneManager.LoadScene("GameOverScene", LoadSceneMode.Single);
            return;
    }
}
