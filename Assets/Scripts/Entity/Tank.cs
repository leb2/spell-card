using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Enemy {

    private Rigidbody2D rb2d;
    private GameObject player;
    private Vector2 spellTargetLocation; // used so that spell localtion does not change after indicator
    public GameObject areaEffect;
    public GameObject areaEffectIndicator;
    public float timeBetweenShots;

    // Use this for initialization
    public new void Start()
    {
        base.Start();
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        spellTargetLocation = player.transform.position;
        timeBetweenShots = timeBetweenShots + Random.value;
        StartCoroutine(ShootPlayer());
    }

    IEnumerator ShootPlayer()
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenShots + 1);
            TargetPlayerSingle();
            yield return new WaitForSeconds(timeBetweenShots - 0.5F - (timeBetweenShots - 1F));
            ShootPlayerSingle();
        }

    }

    private void TargetPlayerSingle()
    {
        ModifySpeed(0, 300); // stop tank movement while attacking
        spellTargetLocation = (Vector2)player.transform.position + Random.insideUnitCircle * 2;
        GameObject areaEffectIndicatorObj = Instantiate(areaEffectIndicator, spellTargetLocation, Quaternion.identity);
    }

    private void ShootPlayerSingle()
    {
        GameObject areaEffectObj = Instantiate(areaEffect, spellTargetLocation, Quaternion.identity);

        List<ElementType> elementTypes = new List<ElementType>();
        elementTypes.Add(ElementType.FIRE);
        areaEffectObj.GetComponent<AreaEffect>().spell = new Spell(elementTypes, ShapeType.CIRCLE, null);
        areaEffectObj.GetComponent<AreaEffect>().targetTag = "Player";
        ModifySpeed(1, 0); // resume tank movement after attacking
    }

    // Update is called once per frame
    public override void Update () {
        base.Update();

        Vector2 dirVector = new Vector2(player.transform.position.x - transform.position.x,
                                        player.transform.position.y - transform.position.y);
        if (dirVector.magnitude > 5)
        {
            Debug.Log(speedModifier);
            Debug.Log(dirVector.normalized * speed * speedModifier * Time.deltaTime);
            rb2d.velocity = dirVector.normalized * speed * speedModifier * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().TakeDamage(damage);
        }
    }
}
