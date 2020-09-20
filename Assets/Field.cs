using System.Collections.Generic;
using UnityEngine;
public class Field
{
    UIDialog dialog;
    Crop crop;
    List<IButton> actions;
    IButtonLister actionLister;
    class ActionLister : IButtonLister 
    {
        public List<IButton> Buttons { get; set; }
        public ActionLister(List<IButton> actions)
        {
            Buttons = actions;
        }
    }
    void UpdateActions()
    {
        Debug.Log("updating actions");
        if (crop == null)
        {
            Shop shop = new Shop(this);
            Debug.Log("new shop: " + shop.ToString());
            actions = shop.Buttons;
            Debug.Log("actions count: " + actions.Count);
        }
        actionLister = new ActionLister(actions);
    }
    public void OnClick()
    {
        UpdateActions();
        dialog.buttonLister = actionLister;
        Debug.Log("actions: " + actionLister.Buttons.Count);
        Debug.Log("dialog: " + dialog.buttonLister.Buttons.Count);
        dialog.ShowPanel();
    }
    public void AddCrop(Crop crop)
    {
        this.crop = crop;
    }
    public Field(GameObject dialogObj)
    {
        this.dialog = dialogObj.GetComponent<UIDialog>();
    }
}