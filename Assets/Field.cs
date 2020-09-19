using System.Collections.Generic;
using UnityEngine;
public class Field : IUIButtonLister
{
    GameObject dialogObj;
    UIDialog dialog;
    Crop crop;
    List<IButton> actions;
    public List<IButton> Buttons { get { return actions; } }

    void UpdateActions()
    {
        if (crop == null)
        {
            actions = new Shop().Buttons;
        }
    }
    public void OnClick()
    {
        UpdateActions();
        dialog.buttonLister = this;
        dialog.ShowPanel();
    }
    public Field(GameObject dialogObj)
    {
        this.dialogObj = dialogObj;
        this.dialog = this.dialogObj.GetComponent<UIDialog>();
    }
}