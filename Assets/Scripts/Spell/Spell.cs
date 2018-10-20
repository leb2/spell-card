using System.Collections.Generic;
using UnityEngine;
using System;

public class Spell {
    public float damage;

    public ElementType elementType;
    public int magnitude; // number of element cards that are stacked
    public ShapeType shape;
    public Modifier modifier;


    public Spell(List<ElementType> elementTypes, ShapeType shape, Modifier modifier) {
        this.elementType = elementTypes[0];
        this.magnitude = elementTypes.Count;
        this.shape = shape;
        this.modifier = modifier;

        // Base damage values for each element
        Dictionary<ElementType, float> damages = new Dictionary<ElementType, float>
        {
            {ElementType.FIRE, 10},
            {ElementType.ICE, 5},
            {ElementType.ROT, 5}
        };


        // Set damage of spell. Rot spells scale differently because they deal damage over time.
        switch (this.elementType)
        {
            case ElementType.FIRE:
            case ElementType.ICE:
                this.damage = damages[this.elementType] * this.magnitude;
                break;
            case ElementType.ROT:
                this.damage = damages[this.elementType] + (2 * (this.magnitude - 1));
                break;
            default:
                throw new Exception("Spell of unknown element encountered: " + elementType);
        }
    }

    public void ApplyEffect(Collider2D target) {
        Entity entity = target.gameObject.GetComponent<Entity>();

        int baseDuration;
        switch (elementType)
        {
            case ElementType.FIRE:
                entity.TakeDamage(damage);
                break;
            case ElementType.ICE:
                float baseSpeedMod = (float)(0.5); // slow down by 50%
                baseDuration = 150; // ~5 seconds
                entity.ModifySpeed(baseSpeedMod, baseDuration * magnitude);
                break;
            case ElementType.ROT:
                baseDuration = 90; // ~3 seconds
                entity.TakeDamage(damage, baseDuration);
                break;
            default:
                throw new Exception("Spell of unknown element encountered: " + elementType);
        }

    }

  
    public override string ToString()
    {
        return elementType.ToString() + " " + shape.ToString() + " (" + magnitude + ")";
    }
}
