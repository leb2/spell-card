using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
    public float maxHp;
    public float hp;

    public bool TakeDamage(float damage) {
        hp -= damage;
        if (hp < 0) {
            Destroy(this.gameObject);
            return true;
        }
        return false;
    }

	// Use this for initialization
    public void Start () {
        hp = maxHp;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
