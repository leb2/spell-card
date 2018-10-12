using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCard : Card
{
    public Spell spell;

    // Use this for initialization
    public SpellCard(Spell spell) 
    {
        this.spell = spell;
        this.cardType = CardType.SPELL;
    }

    public override void AddToInventory(Inventory inv)
    {
        inv.spells.Add(spell);
    }


}
