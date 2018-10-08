using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    public bool roundOver = false;

    [Serializable]
    public struct RoundInfo
    {
        public int numZombies;
        public int numArchers;
    }
    public List<RoundInfo> rounds;


    // Use this for initialization
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        List<GameObject> enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        if (enemies.Count == 0 && !roundOver) {
            roundOver = true;
            SceneManager.LoadScene("MenuScene", LoadSceneMode.Additive);
        }
	}
}
