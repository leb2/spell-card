using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
    public float maxHp;
    public float hp;
    public float speed;
    public float speedModifier = 1;
    public int frozenFramesRemaining = 0; // time until speedModifier is reset

    public virtual bool TakeDamage(float damage) {
        hp -= damage;
        if (hp <= 0) {
            Die();
            Destroy(this.gameObject);
            return true;
        }
        return false;
    }

    public virtual void ModifySpeed(float modifier, int duration) {
        speedModifier = modifier;
        frozenFramesRemaining = duration;
        Debug.Log("speedModifier: " + speedModifier);
        Debug.Log("frozenFrames: " + frozenFramesRemaining);
    }

    public virtual void Die () {
        return;
    }

	// Use this for initialization
    public void Start () {
        hp = maxHp;
	}
	
	// Update is called once per frame
	public virtual void Update () {
        if (frozenFramesRemaining < 1) {
            speedModifier = 1;
        } else if (frozenFramesRemaining > 0) {
            frozenFramesRemaining--;
        }
    }
}
