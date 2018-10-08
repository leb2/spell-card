using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierCard : Card {
    public Modifier modifier;

    void Start()
    {
        cardType = CardType.MODIFIER;
    }
}
