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
    public IButtonLister buttonLister {get; set;}
    void Awake()
    {
        print(title.text + ": awake");
        var button = transform.Find("Panel/CloseButton").GetComponent<Button>();
        button.onClick.AddListener(() => HidePanel());
    }
    void Start()
    {
        print(title.text + ": start");
    }
    public void HidePanel()
    {
        print(title + ": disabled");
        gameObject.SetActive(false);
    }
    public void SetDialog(string dialog)
    {
        GameObject dialogList = transform.Find("Panel").Find("List").gameObject;
        GameObject dialogTextObj = new GameObject();
        dialogTextObj.transform.SetParent(dialogList.transform, false);
        Text dialogText = dialogTextObj.AddComponent<Text>();
        dialogText.text = dialog; 
        dialogText.color = Color.black;  
        dialogText.font = Resources.GetBuiltinResource<Font>("Arial.ttf");

    }
    public void ShowPanel()
    {
        if (buttonLister != null)
        {
            SetButtons(buttonLister.Buttons);
        }
        gameObject.SetActive(true);
    }
    public void SetTitle(string title)
    {
        this.title.text = title;
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
            if (button.Desc != null && button.Desc != "")
            {
                t.text += "\n" + button.Desc;
            }

            Button cButton = b.GetComponent<Button>();
            cButton.onClick.AddListener(() => button.OnClick());
            cButton.onClick.AddListener(() => HidePanel());
            
            b.transform.SetParent(list.transform);
        }
    }
}
