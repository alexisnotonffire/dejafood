using System.Collections;
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
    public GameObject fieldDialogObj;
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
        if (!Input.GetMouseButtonDown(0)) { return; } 
        if (EventSystem.current.IsPointerOverGameObject()) { 
            Debug.Log("offtile click: " + Input.GetMouseButtonDown(0).ToString() + EventSystem.current.IsPointerOverGameObject());
            return; 
        }
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePos = Vector3Int.RoundToInt(pos);
        Debug.Log("tile click: " + tilePos.ToString());
        // if (tmap.GetTile(tilePos) == null) { 
        //     Debug.Log("no tile at: " + tilePos.ToString());
        //     return; 
        // }

        Field f;
        if (!fields.TryGetValue(tilePos, out f)) { 
            f = new Field(fieldDialogObj);
            fields.Add(tilePos, f);
            Debug.Log("registered fields: " + fields.Count);
        }
        Debug.Log("clicked field: " + f.ToString());
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
