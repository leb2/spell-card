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
    }

    public override void Collect (Player p) {
        p.inventory.elementCards[elementType] += 1;
    }
}
