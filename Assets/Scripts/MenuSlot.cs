using System.Diagnostics;
using UnityEngine.UIElements;

public class MenuSlot
{
    public HinnyButton hinnyButton;
    internal Button button;

    public MenuSlot(HinnyButton _hinnyButton, VisualTreeAsset template) {
        TemplateContainer hinnyButtonContainer = template.Instantiate();
        button = hinnyButtonContainer.Q<Button>();
        button.text = _hinnyButton.name;
        this.hinnyButton = _hinnyButton;

        button.RegisterCallback<ClickEvent>(OnClick);
    }

    public void OnClick(ClickEvent evt)
    {
        hinnyButton.ButtonClicked();
    }
}