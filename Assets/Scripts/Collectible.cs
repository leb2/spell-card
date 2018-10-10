using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour {

    public string targetTag = "Player";

    public Card card;

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collected Spell");
        if (other.gameObject.CompareTag(targetTag))
        {
            Inventory inv = other.gameObject.GetComponent<Player>().inventory;
            card.AddToInventory(inv);
            Destroy(gameObject);
        }
    }
}
