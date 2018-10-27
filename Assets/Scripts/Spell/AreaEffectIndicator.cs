using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEffectIndicator : SpellEffect
{

    private int framesRemaining = 5;

    // Use this for initialization
    void Start () {
		
	}

    void Update()
    {
        framesRemaining -= 1;
        if (framesRemaining > 5)
        {
            //Debug.Log("in Circle update method");
        }
        else if (framesRemaining <= -30)
        {
            Destroy(gameObject);
        }
    }

    public override void Cast(Spell spell, Vector3 target)
    {
        this.spell = spell;
        transform.position = target;
    }
}
