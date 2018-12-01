using System.Collections.Generic;
using UnityEngine;
using System;

public class Spell {
    public float damage;
    public int cooldown;

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
            {ElementType.ICE, 10},
            {ElementType.ROT, 8}
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
        if (this.elementType == ElementType.ICE)
        {
            Debug.Log("Spell damage: " + this.damage);
        }

        // Set cooldown of spell
        switch (this.shape)
        {
            case ShapeType.CIRCLE:
                this.cooldown = 5 * 30;
                break;
            case ShapeType.CONE:
                this.cooldown = 5 * 30;
                break;
            case ShapeType.LINE:
                this.cooldown = 2 * 30;
                break;
            case ShapeType.PROJECTILE:
                this.cooldown = (int)(0.3 * 30);
                break;
            default:
                throw new Exception("Spell of unknown shape encountered: " + shape);
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
                entity.TakeDamage(damage);
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
