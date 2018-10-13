using System.Collections.Generic;
using UnityEngine;

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

        Dictionary<ElementType, float> damages = new Dictionary<ElementType, float>
        {
            {ElementType.FIRE, 10},
            {ElementType.ICE, 5},
            {ElementType.ROT, 5}
        };

        this.damage = damages[this.elementType] * this.magnitude;
    }

    public void ApplyEffect(Collider2D target) {
        if (target.gameObject.CompareTag("Enemy"))
        {
            target.gameObject.GetComponent<Entity>().TakeDamage(damage);
            Debug.Log(elementType);

            // apply slowing effect if applicable
            if (elementType == ElementType.ICE) {
                float baseSpeedMod = (float)(0.5);
                int baseDuration = 150;
                target.gameObject.GetComponent<Entity>().ModifySpeed(baseSpeedMod, baseDuration * magnitude);
            }
        }
    }


    public override string ToString()
    {
        return shape.ToString();
    }
}
