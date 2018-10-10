using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

    public GameObject collectible;

    public override void Die()
    {
        //Debug.Log("here");
        //Object[] cards = Resources.LoadAll("Prefab/Collectibles");
        GameObject clone = Instantiate(collectible, transform.position, Quaternion.identity) as GameObject;
        clone.GetComponent<Collectible>().card = new ElementCard();
        return;
    }
}
