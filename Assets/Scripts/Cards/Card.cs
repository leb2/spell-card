using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Card : MonoBehaviour {
    public CardType cardType;
    public abstract void AddToInventory(Inventory inv);
}