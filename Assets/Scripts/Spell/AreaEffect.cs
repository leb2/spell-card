using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffect : SpellEffect {

    public override void Cast(Spell spell, Vector3 target) {
        this.spell = spell;
        transform.position = target;
        transform.position += Vector3.forward * 5;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Entity>().TakeDamage(spell.damage);
            Destroy(gameObject);
        }
    }
}
