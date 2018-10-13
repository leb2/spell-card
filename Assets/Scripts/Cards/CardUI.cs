using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardUI : MonoBehaviour {
    public Card card;

	// Use this for initialization
	void Start () {
        gameObject.GetComponent<Image>().color = card.color;
        if (card.cardType == CardType.SPELL || card.cardType == CardType.MODIFIER) {
            gameObject.transform.GetChild(1).GetComponent<Text>().enabled = false;
        }
    }

    public void SetCount(int count) {
        GameObject countText = gameObject.transform.GetChild(1).gameObject;
        countText.GetComponent<Text>().text = count.ToString();
    }
}
