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
    public GameObject tank;

    private int _currRound = 0;

    public List<RoundEnemyInfo> rounds = new List<RoundEnemyInfo>{
        new RoundEnemyInfo(7, 1, 1),
        new RoundEnemyInfo(8, 0, 0),
        new RoundEnemyInfo(7, 2, 0),
        new RoundEnemyInfo(3, 4, 0),
        new RoundEnemyInfo(4, 6, 0),
        new RoundEnemyInfo(6, 6, 0),
        new RoundEnemyInfo(8, 2, 0),
        new RoundEnemyInfo(8, 3, 0),
        new RoundEnemyInfo(16, 0, 0),
        new RoundEnemyInfo(10, 0, 0),
        new RoundEnemyInfo(12, 2, 0),
        new RoundEnemyInfo(10, 3, 0),
        new RoundEnemyInfo(7, 0, 0),
        new RoundEnemyInfo(4, 3, 0),
        new RoundEnemyInfo(6, 2, 1),
        new RoundEnemyInfo(3, 2, 2),
        new RoundEnemyInfo(5, 2, 1),
        new RoundEnemyInfo(7, 0, 5),
        new RoundEnemyInfo(5, 4, 3)
    };

    public List<RoundDropWeightInfo> dropWeights = new List<RoundDropWeightInfo>
    {
        new RoundDropWeightInfo(1, 0, 0, 0, 0, 0, 1),
        new RoundDropWeightInfo(1, 0, 0, 0, 0, 0, 1),
        new RoundDropWeightInfo(1, 0, 0, 0, 0, 0, 1),
        new RoundDropWeightInfo(1, 0, 0, 0, 0, 0, 1),
        new RoundDropWeightInfo(1, 0, 0, 0, 0, 0, 1),
        // After level 5
        new RoundDropWeightInfo(2, 0, 1, 0, 0, 0, 1),
        new RoundDropWeightInfo(2, 0, 1, 0, 0, 0, 1),
        new RoundDropWeightInfo(2, 0, 1, 0, 0, 0, 1),
        // After level 8
        new RoundDropWeightInfo(1, 0, 1, 0, 1, 0, 1),
        new RoundDropWeightInfo(1, 0, 1, 0, 1, 0, 1),
        new RoundDropWeightInfo(1, 0, 1, 0, 1, 0, 1),
        new RoundDropWeightInfo(1, 0, 1, 0, 1, 0, 1),
        // After level 12
        new RoundDropWeightInfo(3, 0, 5, 0, 4, 5, 3),
        new RoundDropWeightInfo(3, 0, 5, 0, 4, 5, 3),
        new RoundDropWeightInfo(3, 0, 5, 0, 4, 5, 3),
        // After level 15
        new RoundDropWeightInfo(3, 5, 5, 0, 2, 3, 2),
        new RoundDropWeightInfo(3, 0, 5, 0, 4, 5, 3),
        // After level 17
        new RoundDropWeightInfo(3, 4, 3, 4, 2, 2, 1),
        new RoundDropWeightInfo(3, 4, 3, 4, 2, 2, 1)
    };


    public struct RoundEnemyInfo
    {
        public int numZombies, numArchers, numTanks;

        //Constructor
        public RoundEnemyInfo(int zombies, int archers, int tanks)
        {
            numZombies = zombies;
            numArchers = archers;
            numTanks = tanks;
        }
    }

    public struct RoundDropWeightInfo
    {
        public int fireWeight, iceWeight, rotWeight;
        public int circleWeight, coneWeight, lineWeight, projectileWeight;

        //Constructor
        public RoundDropWeightInfo(int fire, int ice, int rot, 
                                   int circle, int cone, int line, int projectile)
        {
            fireWeight = fire;
            iceWeight = ice;
            rotWeight = rot;
            circleWeight = circle;
            coneWeight = cone;
            lineWeight = line;
            projectileWeight = projectile;
        }
    }

    // Use this for initialization
    void Start () {
        StartNextRound();
	}


    public void StartNextRound() {
        RoundEnemyInfo info = rounds[_currRound];

        // Spawn zombies
        for (int i = 0; i < info.numZombies; i++) {
            Vector3 position = Random.insideUnitCircle.normalized;
            position *= Random.Range(7, 8);
            Enemy clone = Instantiate(zombie, position, Quaternion.identity).GetComponent<Enemy>();
            clone.dropWeights = dropWeights[_currRound];
            clone.SetMaxHp(15 + _currRound * (float)7.5);
        }

        // Spawn archers
        for (int i = 0; i < info.numArchers; i++)
        {
            Vector3 position = Random.insideUnitCircle.normalized;
            position *= Random.Range(7, 8);
            Enemy clone = Instantiate(archer, position, Quaternion.identity).GetComponent<Enemy>();
            clone.dropWeights = dropWeights[_currRound];
            clone.SetMaxHp(10 + _currRound * 5);
        }

        // Spawn tanks
        for (int i = 0; i < info.numTanks; i++)
        {
            Vector3 position = Random.insideUnitCircle.normalized;
            position *= Random.Range(7, 8);
            Enemy clone = Instantiate(tank, position, Quaternion.identity).GetComponent<Enemy>();
            clone.dropWeights = dropWeights[_currRound];
            clone.SetMaxHp(30 + _currRound * 15);
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
