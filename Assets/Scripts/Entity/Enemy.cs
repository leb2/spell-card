using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity {

    public GameObject collectible;
    public float damage;

    public GameController.RoundDropWeightInfo dropWeights = new GameController.RoundDropWeightInfo(1, 1, 1, 1, 1, 1, 1);

    public override void Update() {
        base.Update();
    }

    public void SetMaxHp(float newHp) { maxHp = hp = newHp; }

    public override void Die()
    {
        //Debug.Log("here");
        //Object[] cards = Resources.LoadAll("Prefab/Collectibles");

        int randomNum = Random.Range(1, 101); // has value in range [1, 100]

        if (1 <= randomNum && randomNum < 26) {
            // spawn health drops
        } else {
            // Generate random card according to weights and level
            Dictionary<Card, int> choices = new Dictionary<Card, int>
            {
                {new ElementCard(ElementType.FIRE), dropWeights.fireWeight},
                {new ElementCard(ElementType.ICE), dropWeights.iceWeight},
                {new ElementCard(ElementType.ROT), dropWeights.rotWeight},
                {new ShapeCard(ShapeType.CIRCLE), dropWeights.circleWeight},
                {new ShapeCard(ShapeType.CONE), dropWeights.coneWeight},
                {new ShapeCard(ShapeType.LINE), dropWeights.lineWeight},
                {new ShapeCard(ShapeType.PROJECTILE), dropWeights.projectileWeight}
            };

            GameObject clone = Instantiate(collectible, transform.position, Quaternion.identity) as GameObject;
            clone.GetComponent<Collectible>().card = RandomWeightedPicker(choices);
        }

        return;
    }

    /*
     * This function takes in a dictionary of keys mapped to integers, which
     * represent weights associated to each key. A random key is chosen
     * with the weights taken in account;
     */
    public E RandomWeightedPicker<E>(Dictionary<E, int> choices) {
        int totalWeight = 0;
        foreach (E choice in choices.Keys)
        {
            totalWeight += choices[choice];
        }

        List<E> list = new List<E>();
        foreach (E choice in choices.Keys)
        {
            for (int i = 0; i < choices[choice]; i++) {
                list.Add(choice);
            }
        }

        return list[Random.Range(0, totalWeight)];
    }
}

