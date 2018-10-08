using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour {
    public double maxHp;
    public double hp;

    public bool TakeDamage(double damage) {
        hp -= damage;
        if (hp < 0) {
            Destroy(this.gameObject);
            return true;
        }
        return false;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
