using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class CraftHandler : MonoBehaviour {

    public GameObject cardPrefab;

    public GameObject elementCardPanel;
    public GameObject shapeCardPanel;
    public GameObject modifierCardPanel;
    public GameObject spellCardPanel;
    public GameObject equippedSpellsPanel;

    public Button craftButton;
    public Button finishButton;
    public Button elementSlot;
    public Button shapeSlot;
    public Button modifierSlot;

    private Inventory inventory;
    private List<Button> _selectedElements = new List<Button>();
    private Button _selectedShape = null;
    private Button _selectedModifier = null;
    private Button _selected;
    private List<Button> _slots;
    private Player _player;
    private List<GameObject> inventorySpellCards = new List<GameObject>();
    private List<Button> _equippedSpells = new List<Button>();

    private List<CardUI> elementCardUIs = new List<CardUI>();
    private List<CardUI> shapeCardUIs = new List<CardUI>();
    private List<CardUI> modifierCardUIs = new List<CardUI>();

    void Start()
    {
        craftButton.onClick.AddListener(CraftSelection);
        finishButton.onClick.AddListener(FinishCrafting);

        elementSlot.onClick.AddListener(() =>
        {
            _selectedElements.Clear();
            elementSlot.GetComponent<Image>().color = Color.white;
        });
        shapeSlot.onClick.AddListener(() =>
        {
            _selectedShape = null;
            shapeSlot.GetComponent<Image>().color = Color.white;
        });
        modifierSlot.onClick.AddListener(() =>
        {
            _selectedModifier = null;
            modifierSlot.GetComponent<Image>().color = Color.white;
        });

        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().inventory;
        Debug.Log(inventory);

        // Spawn each element card centered 
        int i = 0;
        foreach (KeyValuePair<ElementType, int> entry in inventory.elementCards) {
            CardUI cardUI = InitializeButton(i, inventory.elementCards.Count, 
                                             elementCardPanel, new ElementCard(entry.Key)) as CardUI;
            elementCardUIs.Add(cardUI);
            GameObject text = cardUI.gameObject.transform.GetChild(0).gameObject;
            text.GetComponent<Text>().text = entry.Key.ToString();
            i += 1;
        }
        // Spawn each shape card centered
        i = 0;
        foreach (KeyValuePair<ShapeType, int> entry in inventory.shapeCards)
        {
            CardUI cardUI = InitializeButton(i, inventory.shapeCards.Count, 
                                             shapeCardPanel, new ShapeCard(entry.Key)) as CardUI;
            shapeCardUIs.Add(cardUI);
            GameObject text = cardUI.gameObject.transform.GetChild(0).gameObject;
            text.GetComponent<Text>().text = entry.Key.ToString();
            i += 1;
        }

        // Spawn each modifier card centered
        i = 0;
        foreach(Modifier modifier in inventory.modifiers) {
            CardUI cardUI = InitializeButton(i, inventory.modifiers.Count, 
                                             modifierCardPanel, new ModifierCard(modifier)) as CardUI;
            modifierCardUIs.Add(cardUI);
            i += 1;
        }
        RefreshInventorySpells();
        UpdateCardCounts();
    }

    private void UpdateCardCounts() {
        foreach(CardUI elementCardUI in elementCardUIs) {
            ElementType elementType = (elementCardUI.card as ElementCard).elementType;
            elementCardUI.SetCount(inventory.elementCards[elementType]);
        }
        foreach (CardUI shapeCardUI in shapeCardUIs)
        {
            ShapeType shapeType = (shapeCardUI.card as ShapeCard).shape;
            shapeCardUI.SetCount(inventory.shapeCards[shapeType]);
        }
    }


    private void ClearSelection() {
        _selectedElements.Clear();
        _selectedModifier = null;
        _selectedShape = null;
        UpdateColors();
    }

    private void CraftSelection() {
        if (_selectedElements.Count != 0 && _selectedShape != null)
        {
            List<ElementType> elementTypes = new List<ElementType>();

            foreach (Button button in _selectedElements)
            {
                ElementType elementType = (button.gameObject.GetComponent<CardUI>().card as ElementCard).elementType;
                elementTypes.Add(elementType);
                inventory.elementCards[elementType] -= 1;
            }
            ShapeType shape = (_selectedShape.gameObject.GetComponent<CardUI>().card as ShapeCard).shape;
            Modifier modifier = (_selectedModifier.gameObject.GetComponent<CardUI>().card as ModifierCard).modifier;

            inventory.shapeCards[shape] -= 1;
            inventory.modifiers.Remove(modifier);

            Spell spell = new Spell(elementTypes, shape, modifier);
            inventory.spells.Add(spell);
            RefreshInventorySpells();
            ClearSelection();
            UpdateCardCounts();
        }
    }

    private void RefreshInventorySpells() {
        foreach (GameObject button in inventorySpellCards) {
            Destroy(button);
        }

        int i = 0;
        foreach (Spell spell in inventory.spells)
        {
            CardUI spellCard = InitializeButton(i, inventory.spells.Count, 
                                                spellCardPanel, new SpellCard(spell)) as CardUI;
            spellCard.card = new SpellCard(spell);

            GameObject text = spellCard.gameObject.transform.GetChild(0).gameObject;
            text.GetComponent<Text>().text = spell.ToString();
            inventorySpellCards.Add(spellCard.gameObject);
            i += 1;
        }
        i = 0;
        foreach (Spell spell in inventory.equippedSpells) {
            CardUI spellCard = InitializeButton(i, inventory.equippedSpells.Count, 
                                                equippedSpellsPanel, new SpellCard(spell)) as CardUI;
            GameObject text = spellCard.gameObject.transform.GetChild(0).gameObject;
            text.GetComponent<Text>().text = spell.ToString();
            inventorySpellCards.Add(spellCard.gameObject);
            i += 1;
        }
    }

    private CardUI InitializeButton(int i, int numCards, GameObject panel, Card card) {
        GameObject newButton = Instantiate(cardPrefab) as GameObject;
        newButton.transform.SetParent(panel.transform, false);
        newButton.GetComponent<RectTransform>().localPosition = GetCenteredPosition(i, numCards);

        // Add event listener
        Button buttonComponent = newButton.GetComponent<Button>();
        buttonComponent.onClick.AddListener(() => elementClicked(buttonComponent));
        CardUI cardUI = newButton.GetComponents<CardUI>()[0];
        cardUI.card = card;
        return cardUI;
    }

    private Vector3 GetCenteredPosition(int i, int numCards) {
        int cardSpacing = 70;
        Vector3 startVector = new Vector3(-(numCards - 1) * cardSpacing / 2, 0, 0);
        Vector3 offset = new Vector3(i * cardSpacing, 0, -30);
        return startVector + offset;
    }

    private void elementClicked(Button button) {
        CardUI cardUI = button.gameObject.GetComponents<CardUI>()[0];
        Card card = cardUI.card;

        if (card.cardType == CardType.ELEMENT) {
            if (_selectedElements.Contains(button))
            {
                _selectedElements.Remove(button);
                if (_selectedElements.Count == 0) {
                    elementSlot.GetComponent<Image>().color = Color.white;
                }
            }
            else
            {
                _selectedElements.Add(button);
                elementSlot.GetComponent<Image>().color = Color.green;

            }
        }
        if (card.cardType == CardType.SHAPE) {
            if (_selectedShape == button) {
                _selectedShape = null;
            }
            else {
                _selectedShape = button;
            }
        }
        if (card.cardType == CardType.MODIFIER) {
            if (_selectedModifier == button)
            {
                _selectedModifier = null;
            }
            else
            {
                _selectedModifier = button;
            }
        }
        if (card.cardType == CardType.SPELL) {
            Spell spell = ((SpellCard)card).spell;
            if (inventory.equippedSpells.Contains(spell)) {
                inventory.equippedSpells.Remove(spell);
                inventory.spells.Add(spell);
            } else if (inventory.equippedSpells.Count < 2) {
                inventory.equippedSpells.Add(spell);
                inventory.spells.Remove(spell);
            }
            RefreshInventorySpells();
        }
        UpdateColors();
    }

    private void UpdateColors() {
        elementSlot.GetComponent<Image>().color = _selectedElements.Count == 0 ? Color.white :
            Color.green;
        shapeSlot.GetComponent<Image>().color = _selectedShape == null ? Color.white : Color.green;
        modifierSlot.GetComponent<Image>().color = _selectedModifier == null ? Color.white : Color.green;
    }

    private void FinishCrafting() {
        SceneManager.UnloadSceneAsync("MenuScene");
        GameController gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        gameController.StartNextRound();
    }

    private void slotClicked(Button button) {
        if (_selectedElements.Contains(button)) {
            _selectedElements.Remove(button);
        }
    }
}
