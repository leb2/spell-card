using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SpellEffect : MonoBehaviour {
    public Spell spell;
    public abstract void Cast(Spell spell, Vector3 target);
}
