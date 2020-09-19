using System.Collections.Generic;
using UnityEngine;
public class Field
{
    GameObject dialogObj;
    UIDialog dialog;
    Crop crop;
    List<IButton> actions;
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
        dialog.ShowPanel(actions);
    }
    public Field(GameObject dialogObj)
    {
        this.dialogObj = dialogObj;
        this.dialog = this.dialogObj.GetComponent<UIDialog>();
    }
}