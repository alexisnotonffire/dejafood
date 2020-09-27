using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class Farm : MonoBehaviour
{
    int turn = 1;
    int cash = 1000;
    int turnsInYear = 5;
    Text turnCount;
    public Grid grid;
    public Tilemap tmap;
    Ledger ledger;
    Trader trader;
    public GameObject fieldDialogObj;
    Dictionary<string, int> harvestedCrops = new Dictionary<string, int>();
    Dictionary<Vector3Int, Field> fields = new Dictionary<Vector3Int, Field>();
    int causalityBreaches = 0;
    public void NextTurn()
    {
        print("turn end cash: " + cash);
        int turnCausalityBreaches = 0;
        if (turnCount != null)
        {
            turn++;
            turnCount.text = turn.ToString();
        }
        foreach (var field in fields)
        {
            field.Value.NextTurn();
            if (field.Value.HarvestedCrop != null)
            {
                if (!harvestedCrops.ContainsKey(field.Value.HarvestedCrop.Name))
                {
                    harvestedCrops.Add(field.Value.HarvestedCrop.Name, 0);
                }
                harvestedCrops[field.Value.HarvestedCrop.Name]++;
                print(field.Value.HarvestedCrop.Name + ": " + harvestedCrops[field.Value.HarvestedCrop.Name]);
            }
        }
        Debug.Log("Aged all field crops");
        foreach (var crop in harvestedCrops)
        {
            print(crop.Key + ": " + crop.Value);
        }

        turnCausalityBreaches = validateCropFutures();
        print("causal breaches: " + turnCausalityBreaches.ToString());
        print("accepted contracts: " + ledger.AcceptedContracts.Count);
        List<Contract> dueContracts = ledger.NextTurn(turn);
        print("due contracts: " + dueContracts.Count);
        validateDueContracts(dueContracts);
        print("turn start cash: " + cash);

    }
    void validateDueContracts(List<Contract> dueContracts)
    {
        // print("validating");
        foreach (var contract in dueContracts)
        {
            // print("contract: " + contract.Name);
            bool valid = true;
            foreach (var crop in contract.Crops)
            {
                // print("crop: " + crop.Key);
                int harvestedCropCount;
                if (harvestedCrops.TryGetValue(crop.Key, out harvestedCropCount))
                {
                    // print("found: " + crop.Key);
                    harvestedCropCount -= crop.Value;
                    if (harvestedCropCount >= 0)
                    {
                        harvestedCrops[crop.Key] = harvestedCropCount;
                        continue;
                    }
                }
                else
                {
                    print("contract: " + contract.Name);
                    print("missing: " + crop.Key);
                    valid = false;
                    break;
                }
            }
            if (valid) { 
                print("redeemed contract: " + contract.Value);
                cash += contract.Value; 
            }
            else { 
                cash -= (int)(contract.Value * 1.1f);
                ledger.CancelContract(contract);
            }
        }
    }
    int validateCropFutures()
    {
        foreach (Field field in fields.Values)
        {
            var causalityBreaches = 0;
            var resolvedFields = new List<Field>();
            var resolution = field.GetCausalityBreach(turn - turnsInYear);
            switch (resolution)
            {
                case 1:
                    causalityBreaches++;
                    break;
                case 0:
                    field.DeleteCrop();
                    break;
            }
        }
        return causalityBreaches;
    }
    GameObject InitDialog(string name, IButtonLister buttonLister)
    {
        GameObject dialogObj = GameObject.Find(name);
        if (dialogObj == null)
        {
            // Debug.LogError("missing dialog: " + name);
            return null;
        }
        UIDialog dialog = dialogObj.GetComponent<UIDialog>();
        if (buttonLister != null && buttonLister.Buttons != null)
        {
            dialog.buttonLister = buttonLister;
        }
        return dialogObj;
    }
    void OnTileSelect()
    {
        if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1)) { return; }
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // Debug.Log("offtile click: " + Input.GetMouseButtonDown(0).ToString() + EventSystem.current.IsPointerOverGameObject());
            return;
        }
        Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3Int tilePos = grid.WorldToCell(pos);
        // Debug.Log("tile click: " + tilePos.ToString());
        if (tmap.GetTile(tilePos) == null || tmap.GetSprite(tilePos).name == "grass")
        {
            Debug.Log("no farm tile at: " + tilePos.ToString());
            return;
        }
        else
        {
            Field f;
            if (!fields.TryGetValue(tilePos, out f))
            {
                f = new Field(fieldDialogObj, tmap, tilePos);
                fields.Add(tilePos, f);
                // Debug.Log("registered fields: " + fields.Count);
            }
            Debug.Log("clicked field: " + tilePos);
            f.OnClick();
        }
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
        GameObject tcObject = GameObject.Find("GameInfo/TurnCount/Text");
        turnCount = tcObject.GetComponent<Text>();
    }
    void Update()
    {
        OnTileSelect();
    }
}
