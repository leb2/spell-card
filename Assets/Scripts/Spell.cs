using System.Collections.Generic;

public class Spell {
    public double damage;

    public ElementType elementType;
    public ShapeType shape;
    public Modifier modifier;


    public Spell(List<ElementType> elementTypes, ShapeType shape, Modifier modifier) {
        this.elementType = elementTypes[0];
        this.shape = shape;
        this.modifier = modifier;

        Dictionary<ElementType, double> damages = new Dictionary<ElementType, double>
        {
            {ElementType.FIRE, 10},
            {ElementType.ICE, 5},
            {ElementType.ROT, 5}
        };

        this.damage = damages[this.elementType];
    }
}
