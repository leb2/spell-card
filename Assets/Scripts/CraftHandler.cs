using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftHandler : MonoBehaviour {

    public Inventory inventory;

    public GameObject elementCardPrefab;
    public GameObject elementCardPanel;

    public List<Button> buttons = new List<Button>();
    public Button elementSlot;
    public Button shapeSlot;
    public Button modifierSlot;

    private List<Button> _selectedElements = new List<Button>();
    private Button _selected;
    private List<Button> _slots;
    private Player _player;

    void Start()
    {
        //_player = GameObject.FindWithTag("Player").GetComponent<Player>();
        //_slots = new List<Button>(new Button[] { elementSlot, shapeSlot, modifierSlot });

        //foreach (Button button in _slots)
        //{
        //    button.onClick.AddListener(() => slotClicked(button));
        //}
        foreach (Button button in buttons) {
            button.onClick.AddListener(() => elementClicked(button));
        }

        // Create a card for each 
        foreach(KeyValuePair<ElementType, int> entry in inventory.elementCards) {
            GameObject newButton = Instantiate(elementCardPrefab) as GameObject;
            newButton.transform.SetParent(elementCardPanel.transform, false);

            Vector3 offset = new Vector3(30, 30, -30);
            newButton.transform.position = newButton.transform.position + offset;

        }
    }

    private void elementClicked(Button button) {
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
