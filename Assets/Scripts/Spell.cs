using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element { FIRE, ICE, ROT };
public enum Shape { LINE, CONE, CIRCLE, PROJECTILE };

public class Spell : MonoBehaviour {
    public int damage;
    public Element element;
    public Shape shape;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
