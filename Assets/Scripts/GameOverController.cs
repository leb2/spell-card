using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Do nothing
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene("ArenaScene", LoadSceneMode.Single);
        }
    }
}
