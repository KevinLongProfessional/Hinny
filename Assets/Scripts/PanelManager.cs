using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PanelManager : MonoBehaviour
{
    UIDocument uiPanel;
    public VisualTreeAsset itemButttonTemplate;

    // Start is called before the first frame update
    private void OnEnable()
    {
        uiPanel = GetComponent<UIDocument>();

        TemplateContainer itemButtonContainer = itemButttonTemplate.Instantiate();
        var itemRow = uiPanel.rootVisualElement.Q("ItemRow");
        itemRow.Add(itemButtonContainer);

        Button button = itemButtonContainer.Q<Button>();

        button.RegisterCallback<ClickEvent>(OnClick);
    }

    public void OnClick(ClickEvent evt)
    {
        Debug.Log("!!!!!!!!!!!!!!!!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
