using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftHandler : MonoBehaviour {
    public List<Button> buttons = new List<Button>();
    public Button selectedElement = null;
    public Player player;

    void Start()
    {
        GameObject.FindWithTag("player");

        foreach (Button button in buttons) {
            button.onClick.AddListener(() => elementClicked(button));
        }
    }

    private void elementClicked(Button button) {
        Debug.Log("Clicked: " + button.GetComponent<ElementUI>().element);
        selectedElement = button;
    }
}
