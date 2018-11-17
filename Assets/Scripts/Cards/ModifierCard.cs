using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierCard : Card {
    public Modifier modifier;

    public ModifierCard(Modifier modifier) {
        this.modifier = modifier;
        cardType = CardType.MODIFIER;

    }

    public override void Collect(Player p)
    {
        p.inventory.modifiers.Add(modifier);
    }
}
