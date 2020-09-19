using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDialog : MonoBehaviour
{
    public GameObject pfButton;
    public GameObject list;
    public GameObject closeButton;
    public Text closeText;
    public Text title;
    public IUIButtonLister buttonLister {get; set;}
    public void HidePanel()
    {
        gameObject.SetActive(false);
    }
    
    public void ShowPanel(List<IButton> buttons)
    {
        SetButtons(buttons ?? buttonLister.Buttons);
        gameObject.SetActive(true);
    }

    void Start()
    {
        HidePanel();
    }
    public void SetButtons(List<IButton> lib)
    {
        if (lib == null) { return; }

        foreach (Transform child in list.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (IButton button in lib)
        {
            GameObject b = Instantiate(pfButton);
            Text t = b.transform.Find("Text").GetComponent<Text>();
            t.text = button.Name;

            Button cButton = b.GetComponent<Button>();
            cButton.onClick.AddListener(() => button.OnClick());
            cButton.onClick.AddListener(() => HidePanel());
            
            b.transform.SetParent(list.transform);
        }
    }
}
