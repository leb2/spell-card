using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameController : MonoBehaviour {

    [HideInInspector]
    public bool roundOver = false;

    public GameObject zombie;
    public GameObject archer;

    private int _currRound = 0;


    [Serializable]
    public struct RoundInfo
    {
        public int numZombies;
        public int numArchers;
    }
    public List<RoundInfo> rounds;


    // Use this for initialization
    void Start () {
        StartNextRound();
	}


    public void StartNextRound() {
        RoundInfo info = rounds[_currRound];

        // Spawn zombies
        for (int i = 0; i < info.numZombies; i++) {
            Vector3 position = Random.insideUnitCircle.normalized;
            position *= Random.Range(7, 14);
            Instantiate(zombie, position, Quaternion.identity);
        }

        // Spawn archers
        for (int i = 0; i < info.numArchers; i++)
        {
            Vector3 position = Random.insideUnitCircle.normalized;
            position *= Random.Range(7, 10);
            Instantiate(archer, position, Quaternion.identity);
        }
        GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().position = Vector3.zero;
        roundOver = false;
    }

    // Update is called once per frame
    void Update () {
        List<GameObject> enemies = new List<GameObject>(GameObject.FindGameObjectsWithTag("Enemy"));
        if (enemies.Count == 0 && !roundOver) {
            roundOver = true;
            _currRound = _currRound == rounds.Count - 1 ? rounds.Count - 1 : _currRound + 1;
            SceneManager.LoadScene("MenuScene", LoadSceneMode.Additive);
        }
	}
}
