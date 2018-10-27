using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffect : SpellEffect {

    public string targetTag = "Enemy";
    private int framesRemaining = 5;

    private void Update()
    {
        framesRemaining -= 1;
        if (framesRemaining > 5)
        {
            //Debug.Log("in Circle update method");
        }
        else if (framesRemaining <= -30) {
            Destroy(gameObject);
        }
    }

    public override void Cast(Spell spell, Vector3 target) {
        this.spell = spell;
        transform.position = target;
        transform.position += Vector3.forward * 5;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (framesRemaining >= 0 && framesRemaining <= 5) {
            if (other.gameObject.CompareTag(targetTag))
            {
                spell.ApplyEffect(other);
                framesRemaining = 0;
            }
        }
    }
}
