using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ShapeCard : Card {
    public ShapeType shape;

    public ShapeCard(ShapeType shape) {
        this.shape = shape;
        cardType = CardType.SHAPE;

        Dictionary<ShapeType, Color> shapeColors = new Dictionary<ShapeType, Color>
        {
            {ShapeType.CIRCLE, Color.magenta},
            {ShapeType.LINE, Color.blue},
            {ShapeType.PROJECTILE, Color.green},
            {ShapeType.CONE, Color.cyan},

        };
        color = shapeColors[shape];
    }

    public override void AddToInventory(Inventory inv)
    {
        inv.shapeCards[shape] += 1;
    }
}
