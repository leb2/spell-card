using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Enemy : Entity {

    public GameObject collectible;
    public float damage;

    public override void Update() {
        base.Update();
    }

    public void SetMaxHp(float newHp) { maxHp = hp = newHp; }

    public override void Die()
    {
        //Debug.Log("here");
        //Object[] cards = Resources.LoadAll("Prefab/Collectibles");
        GameObject clone = Instantiate(collectible, transform.position, Quaternion.identity) as GameObject;
        clone.GetComponent<Collectible>().card = new ElementCard(ElementType.FIRE);
        return;
    }
}
