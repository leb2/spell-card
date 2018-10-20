using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Enemy {
    private GameObject player;
    public GameObject projectile;
    public float dashSpeed;
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
            yield return new WaitForSeconds(timeBetweenShots);
            DashOrthogonal();
        }

    }

    private void DashOrthogonal() {
        Vector2 playerDirection = player.transform.position - transform.position;
        Vector2 moveDirection = Vector2.Perpendicular(playerDirection);
        int leftOrRight = (Random.value > 0.5f) ? -1 : 1;
        GetComponent<Rigidbody2D>().velocity = moveDirection * dashSpeed * leftOrRight;
    }

    private void ShootPlayerSingle() {
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

        Rigidbody2D body = GetComponent<Rigidbody2D>();
        Vector2 dirVector = new Vector2(player.transform.position.x - transform.position.x,
                                        player.transform.position.y - transform.position.y);
        if (dirVector.magnitude > 4) {
            body.position += dirVector.normalized * speed;
        }
    }   
}
