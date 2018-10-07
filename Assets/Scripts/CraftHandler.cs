using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftHandler : MonoBehaviour {

    public Inventory inventory;

    public GameObject elementCardPrefab;
    public GameObject shapeCardPrefab;

    public GameObject elementCardPanel;
    public GameObject shapeCardPanel;
    public GameObject modifierCardPanel;

    public Button craftButton;
    public Button elementSlot;
    public Button shapeSlot;
    public Button modifierSlot;

    private List<Button> _selectedElements = new List<Button>();
    private Button _selectedShape = null;
    private Button _selected;
    private List<Button> _slots;
    private Player _player;

    void Start()
    {

        Debug.Log("ASDF");
        craftButton.onClick.AddListener(CraftSelection);
        elementSlot.onClick.AddListener(CraftSelection);

        // Spawn each element card centered 
        int i = 0;
        foreach (KeyValuePair<ElementType, int> entry in inventory.elementCards) {
            ElementCard elementCard = InitializeButton(i, inventory.elementCards.Count, elementCardPanel, elementCardPrefab) as ElementCard;
            Debug.Log(elementCard);
            elementCard.elementType = entry.Key;
            elementCard.cardType = CardType.ELEMENT;
            i += 1;
        }
        // Spawn each shape card centered
        i = 0;
        foreach (KeyValuePair<ShapeType, int> entry in inventory.shapeCards)
        {
            ShapeCard shapeCard = InitializeButton(i, inventory.shapeCards.Count, shapeCardPanel, shapeCardPrefab) as ShapeCard;
            shapeCard.shape = entry.Key;
            shapeCard.cardType = CardType.SHAPE;
            i += 1;
        }
    }

    private void CraftSelection() {
        Debug.Log("TESTING");
        if (_selectedElements.Count != 0 && _selectedShape != null) {

        }
    }

    private Card InitializeButton(int i, int numCards, GameObject panel, GameObject prefab) {
        GameObject newButton = Instantiate(prefab) as GameObject;
        newButton.transform.SetParent(panel.transform, false);
        newButton.GetComponent<RectTransform>().localPosition = GetCenteredPosition(i, numCards);

        // Add event listener
        Button buttonComponent = newButton.GetComponent<Button>();
        buttonComponent.onClick.AddListener(() => elementClicked(buttonComponent));

        return newButton.GetComponents<Card>()[0];
    }

    private Vector3 GetCenteredPosition(int i, int numCards) {
        int cardSpacing = 70;
        Vector3 startVector = new Vector3(-(numCards - 1) * cardSpacing / 2, 0, 0);
        Vector3 offset = new Vector3(i * cardSpacing, 0, -30);
        return startVector + offset;
    }

    private void elementClicked(Button button) {
        Card card = button.gameObject.GetComponents<Card>()[0];
        if (card.cardType == CardType.ELEMENT) {
            Debug.Log("I am an element");
        }
        if (card.cardType == CardType.SHAPE) {
            Debug.Log("I am a shape");
        }

        if (_selectedElements.Contains(button)) {
            _selectedElements.Remove(button);
        } else {
            _selectedElements.Add(button);
        }
    }

    private void slotClicked(Button button) {
        if (_selectedElements.Contains(button)) {
            _selectedElements.Remove(button);
        }
    }
}
