using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class TileMenu : MonoBehaviour
{
    public static TileMenu instance;
    public bool show = false;
    public List<HinnyButton> buttons = new List<HinnyButton>();
    public VisualTreeAsset menuButtonTemplate;
    private VisualElement buttonContainer;
    private Tile SelectedTile;

    private void Awake()
    {
        instance = this;
    }

    private void OnEnable()
    {
        buttonContainer = GetComponent<UIDocument>().rootVisualElement.Q("ItemRow");

        foreach (var button in buttons)
        {
            MenuSlot newSlot =  new MenuSlot(button, menuButtonTemplate);
            button.buttonElement = newSlot.button;

            buttonContainer.Add(newSlot.button);
        }
        HideMenu();
    }

    public void ShowMenu()
    {
        foreach (var button in buttons)
        {
            //to do: You'll need to recreate the ui each time. Visibility isn't enough.
            if(button.Validate())
            {
                button.buttonElement.style.display = DisplayStyle.Flex;
            }
            else
            {
                button.buttonElement.style.display = DisplayStyle.None;
            }
        }
        buttonContainer.style.visibility = Visibility.Visible;
    }

    public void HideMenu()
    {
        buttonContainer.style.visibility = Visibility.Hidden;

    }

    private void OnDestroy()
    {
        
    }

    public void SelectTile(Tile tile)
    {
        if (SelectedTile)
        {
            SelectedTile.Deselect();
        }
        SelectedTile = tile;
        ShowMenu(); //to do: refactor to send a TileSelected event and have menu listen for it?
    }

    public Tile GetSelectedTile()
    {
        return SelectedTile;
    }

}