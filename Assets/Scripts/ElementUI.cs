using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ElementUI : MonoBehaviour {
    public ElementType element;
    public ShapeType shape;
    public CardType cardType;

	// Use this for initialization
	void Start () {
        Dictionary<ElementType, Color> elementColors = new Dictionary<ElementType, Color>
        {
            {ElementType.FIRE, Color.red},
            {ElementType.ICE, Color.blue},
            {ElementType.ROT, Color.green}
        };
        Dictionary<ShapeType, Color> shapeColors = new Dictionary<ShapeType, Color>
        {
            {ShapeType.CIRCLE, Color.magenta},
            {ShapeType.LINE, Color.blue},
            {ShapeType.PROJECTILE, Color.green},
            {ShapeType.CONE, Color.cyan},

        };
        Image image = gameObject.GetComponent<Image>();
        if (cardType == CardType.ELEMENT) {
            image.color = elementColors[element];
        } else if (cardType == CardType.SHAPE) {
            image.color = shapeColors[shape];
        }
    }
}
