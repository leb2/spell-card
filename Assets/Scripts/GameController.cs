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

    public List<RoundInfo> rounds = new List<RoundInfo>{
        new RoundInfo(7, 0, 0),
        new RoundInfo(8, 0, 0),
        new RoundInfo(7, 2, 0),
        new RoundInfo(3, 4, 0),
        new RoundInfo(4, 6, 0),
        new RoundInfo(6, 6, 0),
        new RoundInfo(8, 2, 0),
        new RoundInfo(8, 3, 0),
        new RoundInfo(16, 0, 0),
        new RoundInfo(10, 0, 0),
        new RoundInfo(12, 2, 0),
        new RoundInfo(10, 3, 0),
        new RoundInfo(7, 0, 0),
        new RoundInfo(4, 3, 0),
        new RoundInfo(6, 2, 1),
        new RoundInfo(3, 2, 2),
        new RoundInfo(5, 2, 1),
        new RoundInfo(7, 0, 5),
        new RoundInfo(5, 4, 3)
    };


    //[Serializable]
    public struct RoundInfo
    {
        public int numZombies, numArchers, numTanks;

        //Constructor
        public RoundInfo(int z, int a, int t)
        {
            this.numZombies = z;
            this.numArchers = a;
            this.numTanks = t;
        }
    }

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
