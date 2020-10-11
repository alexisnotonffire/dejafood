using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
public class Contract : IButton
{
    Ledger ledger;
    public string Name {get;}
    public string Desc {get;}
    public int Value {get;}
    int Due {get;}
    public Dictionary<string,int> Crops {get;}
    public void OnClick()
    {
        ledger.AcceptContract(this);
    }
    public bool IsDue(int turn)
    {
        Debug.Log(turn + ": " + Due);
        return Due == turn;
    }
    string formatDesc()
    {
        string titleTemplate = "Value: {0} | Due: {1}";
        string cropTemplate = "{0}: {1}";
        List<string> lines = new List<string>();
        lines.Add(string.Format(titleTemplate, Value, Due));
        foreach (var crop in Crops)
        {
            lines.Add(string.Format(cropTemplate, crop.Key, crop.Value));
        }
        return string.Join("\n", lines);
    }
    public Contract(string n, int v, Dictionary<string,int> c, Ledger l, int due)
    {
        Name = n;
        Value = v;
        Crops = c;
        ledger = l;
        Desc = formatDesc();
        Due = due;
    }
}