using System.Collections.Generic;
using UnityEngine;
public class Field
{
    UIDialog dialog;
    Crop crop;
    List<IButton> actions;
    IUIButtonLister actionLister;
    class ActionLister : IUIButtonLister 
    {
        public List<IButton> Buttons { get; set; }
        public ActionLister(List<IButton> actions)
        {
            Buttons = actions;
        }
    }
    void UpdateActions()
    {
        if (crop == null)
        {
            actions = new Shop().Buttons;
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
    public Field(GameObject dialogObj)
    {
        this.dialog = dialogObj.GetComponent<UIDialog>();
    }
}