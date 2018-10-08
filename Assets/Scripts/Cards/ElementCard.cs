using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElementCard : Card {
    public ElementType elementType;

	// Use this for initialization
	void Start () {
        cardType = CardType.ELEMENT;

        Dictionary<ElementType, Color> elementColors = new Dictionary<ElementType, Color>
        {
            {ElementType.FIRE, Color.red},
            {ElementType.ICE, Color.blue},
            {ElementType.ROT, Color.green}
        };
        gameObject.GetComponent<Image>().color = elementColors[elementType];
    }
}
