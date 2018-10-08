using System;
using System.Collections;
using System.Collections.Generic;

public class Inventory {
    public List<Spell> spells = new List<Spell>(); // List of all unequipped spells
    public List<Spell> equippedSpells = new List<Spell>();
    public List<Modifier> modifiers = new List<Modifier>();

    public Dictionary<ElementType, int> elementCards = new Dictionary<ElementType, int>();
    public Dictionary<ShapeType, int> shapeCards = new Dictionary<ShapeType, int>();

    public Inventory()
    {
        // Initialize inventory counts to 0
        foreach (ElementType element in Enum.GetValues(typeof(ElementType))) {
            elementCards[element] = 5;
        }
        foreach (ShapeType shape in Enum.GetValues(typeof(ShapeType))) {
            shapeCards[shape] = 5;
        }
        modifiers.Add(new Modifier());
        equippedSpells.Add(new Spell(new List<ElementType> { ElementType.FIRE }, ShapeType.PROJECTILE, null));

    }
}
