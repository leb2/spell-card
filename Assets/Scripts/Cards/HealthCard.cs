using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthCard : Card
{
    public float value;

    // Use this for initialization
    public HealthCard(float value)
    {
        this.value = value;
    }

    public override void Collect(Player p)
    {
        p.hp += this.value;
        if (p.hp > p.maxHp) {
            p.hp = p.maxHp;
        }
        p.HPBar.value = p.hp;
    }
}
