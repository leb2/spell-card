﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : Enemy {

    private Rigidbody2D rb2d;
    private GameObject player;
    public GameObject projectile;
    public float timeBetweenShots;

    // Use this for initialization
    public new void Start()
    {
        base.Start();
        rb2d = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    IEnumerator ShootPlayer()
    {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenShots);
            ShootPlayerSingle();
        }

    }

    private void ShootPlayerSingle()
    {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        GameObject projectileObj = Instantiate(projectile, transform.position + direction * 1.5F, Quaternion.identity);

        List<ElementType> elementTypes = new List<ElementType>();
        elementTypes.Add(ElementType.FIRE);
        projectileObj.GetComponent<Projectile>().spell = new Spell(elementTypes, ShapeType.PROJECTILE, null);
        projectileObj.GetComponent<Rigidbody2D>().velocity = direction * 10;
        projectileObj.GetComponent<Projectile>().targetTag = "Player";
    }

    // Update is called once per frame
    public override void Update () {
        base.Update();

        Vector2 dirVector = new Vector2(player.transform.position.x - transform.position.x,
                                        player.transform.position.y - transform.position.y);
        rb2d.velocity = dirVector.normalized * speed * speedModifier * Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player.GetComponent<Player>().TakeDamage(damage);
        }
    }
}