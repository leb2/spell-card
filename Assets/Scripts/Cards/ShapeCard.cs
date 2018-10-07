using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShapeCard : Card {
    public ShapeType shape;


	// Use this for initialization
	void Start () {

        Dictionary<ShapeType, Color> shapeColors = new Dictionary<ShapeType, Color>
        {
            {ShapeType.CIRCLE, Color.magenta},
            {ShapeType.LINE, Color.blue},
            {ShapeType.PROJECTILE, Color.green},
            {ShapeType.CONE, Color.cyan},

        };
        gameObject.GetComponent<Image>().color = shapeColors[shape];
    }
}
