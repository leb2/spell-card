using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Archer : Entity {
    private GameObject _playerObj;

    public float timeBetweenShots = 1;


    // Use this for initialization
    public new void Start()
    {
        base.Start();
        _playerObj = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ShootPlayer());
    }

    IEnumerator ShootPlayer() {
        Rigidbody2D body = GetComponent<Rigidbody2D>();
        yield return new WaitForSeconds(timeBetweenShots);
    }

	// Update is called once per frame
	void Update () {
		
	}
}
