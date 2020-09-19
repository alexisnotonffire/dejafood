﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class Farm : MonoBehaviour
{
    int turn = 1;
    Text turnCount;
    Tilemap tmap;
    Ledger ledger;
    Trader trader;
    Dictionary<Vector3Int, Field> fields = new Dictionary<Vector3Int, Field>();
    public void NextTurn()
    {
        if (turnCount != null)
        {
            turn++;
            turnCount.text = turn.ToString();
        }
    }
    GameObject InitDialog(string name, IUIButtonLister buttonLister)
    {
        GameObject dialogObj = GameObject.Find(name);
        if (dialogObj == null)
        {
            Debug.LogError("missing dialog: " + name);
            return null;
        }
        UIDialog dialog = dialogObj.GetComponent<UIDialog>();
        if (buttonLister == null || buttonLister.Buttons == null)
        {
            Debug.Log(name + "Dialog: buttons not found");
        } else {
            dialog.buttonLister = buttonLister;
        }
        return dialogObj;
    }
    void OnTileSelect()
    {
        if (!Input.GetMouseButtonDown(0) || !EventSystem.current.IsPointerOverGameObject()) { return; }
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePos = Vector3Int.RoundToInt(pos);
        Field f;
        fields.TryGetValue(tilePos, out f);
        if (f == null) { 
            f = new Field(InitDialog("Field", null));
        }
        f.OnClick();
    }
    void Awake()
    {
        ledger = new Ledger();
        trader = new Trader(ledger);
        InitDialog("Ledger", ledger);
        InitDialog("Trader", trader);
    }

    void Start()
    {
        GameObject tmObject = GameObject.Find("Farm/FarmTilemap");
        tmap = tmObject.GetComponent<Tilemap>();

        GameObject tcObject = GameObject.Find("GameInfo/TurnCount/Text");
        turnCount = tcObject.GetComponent<Text>();
    }

    void Update()
    {
        OnTileSelect();
    }
}
