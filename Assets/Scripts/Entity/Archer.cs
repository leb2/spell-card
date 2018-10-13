using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy {
    private GameObject player;
    public GameObject projectile;
    public float damage;

    public float timeBetweenShots;


    // Use this for initialization
    public new void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ShootPlayer());
    }

    IEnumerator ShootPlayer() {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        while (true) {
            yield return new WaitForSeconds(timeBetweenShots);
            ShootPlayerSingle();
        }

    }

    private void ShootPlayerSingle() {
        Vector3 direction = (player.transform.position - transform.position).normalized;
        GameObject projectileObj = Instantiate(projectile, transform.position + direction * 1.5F, Quaternion.identity);
        projectileObj.GetComponent<Rigidbody2D>().velocity = direction * 10;
        projectileObj.GetComponent<Projectile>().targetTag = "Player";
        projectileObj.GetComponent<Projectile>().damage = damage;
    }

    // Update is called once per frame
    public override void Update () {
        base.Update();

        Rigidbody2D body = GetComponent<Rigidbody2D>();
        Vector2 dirVector = new Vector2(player.transform.position.x - transform.position.x,
                                        player.transform.position.y - transform.position.y);
        if (dirVector.magnitude > 4) {
            body.velocity = dirVector.normalized * speed * speedModifier * Time.deltaTime;
        } else {
            body.velocity = Vector2.zero;
        }
    }   
}
