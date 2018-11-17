using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementCard : Card {
    public ElementType elementType;

	// Use this for initialization
    public ElementCard(ElementType elementType) {
        cardType = CardType.ELEMENT;
        this.elementType = elementType;

        //Dictionary<ElementType, Color> elementColors = new Dictionary<ElementType, Color>
        //{
        //    {ElementType.FIRE, Color.red},
        //    {ElementType.ICE, Color.blue},
        //    {ElementType.ROT, Color.green}
        //};
        //color = elementColors[elementType];
    }

    public override void AddToInventory (Inventory inv) {
        inv.elementCards[elementType] += 1;
    }
}
