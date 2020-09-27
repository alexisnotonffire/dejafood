using System.Collections.Generic;
using System;
using UnityEngine;
public class FieldMenu : IButtonLister
{
    Field field;
    public List<IButton> Buttons {get; protected set;} = new List<IButton>();
    public FieldMenu(Field field)
    {
        this.field = field;
    }
}