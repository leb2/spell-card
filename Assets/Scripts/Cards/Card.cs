using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card {
    public CardType cardType;
    public abstract void Collect(Player p);
    public Color color = Color.white;
}