using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellCard : Card
{
    public Spell spell;

    // Use this for initialization
    void Start()
    {
        cardType = CardType.SPELL;
    }
}
