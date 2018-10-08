using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpell : MonoBehaviour {
    public string targetTag = "Enemy";
    public float damage;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        // Destroy if out of bounds;
        if (Mathf.Abs(transform.position.y) >= 10 || Mathf.Abs(transform.position.x) >= 10)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("projectile hit");
        if (other.gameObject.CompareTag(targetTag))
        {
            other.gameObject.GetComponent<Entity>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
