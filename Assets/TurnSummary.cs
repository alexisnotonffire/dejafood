using System.Collections.Generic;
using UnityEngine;
public class TurnSummary : IButtonLister
{
    public List<IButton> Buttons {get;} = new List<IButton>();
    public TurnSummary(int cash, int causalityPoints)
    {
        this.Buttons.Add(new TurnItem("Current Cash", cash.ToString()));
        this.Buttons.Add(new TurnItem("Causality Points", causalityPoints.ToString()));
    }
}