using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using UnityEngine.EventSystems;

public class Farm : MonoBehaviour
{
    int cash = 1000;
    int causalityBreaches = 0;
    Dictionary<Vector3Int, Field> fields = new Dictionary<Vector3Int, Field>();
    public GameObject fieldDialogObj;
    public Grid grid;
    Dictionary<string, int> harvestedCrops = new Dictionary<string, int>();
    Ledger ledger;
    public Tilemap tmap;
    Trader trader;
    int turn = 1;
    Text turnCount;
    public GameObject turnDialogObj;
    int turnsInYear = 5;
    void Awake()
    {
        this.ledger = new Ledger();
        this.trader = new Trader(ledger);
    }
    GameObject InitDialog(string name, IButtonLister buttonLister)
    {
        GameObject dialogObj = GameObject.Find("/" + name);
        if (dialogObj == null)
        {
            Debug.LogError("missing dialog: " + name);
            return null;
        }
        print(dialogObj.name + ": " + dialogObj.activeSelf);
        UIDialog dialog = dialogObj.GetComponent<UIDialog>();
        if (dialog == null)
        {
            print(name + ": where's the fucking dialog????");
        } else if (buttonLister != null && buttonLister.Buttons != null)
        {
            dialog.buttonLister = buttonLister;
        }
        dialog.HidePanel();
        return dialogObj;
    }
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
            }
        }
        Debug.Log("aged crops");
        turnCausalityBreaches = validateCropFutures();
        causalityBreaches += turnCausalityBreaches;
        if (causalityBreaches >= 5)
        {
            SceneController.EndGame();
            return;
        }
        print("causal breaches: " + turnCausalityBreaches);
        print("accepted contracts: " + ledger.AcceptedContracts.Count);
        List<Contract> dueContracts = ledger.NextTurn(turn);
        print("due contracts: " + dueContracts.Count);
        validateDueContracts(dueContracts);
        print("turn start cash: " + cash);
        updateTurnSummary();
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
    void Start()
    {
        GameObject tcObject = GameObject.Find("GameInfo/TurnCount/Text");
        turnCount = tcObject.GetComponent<Text>();

        InitDialog("Trader", trader);
        InitDialog("Ledger", ledger);
        InitDialog("Field", null);
        InitDialog("Turn", null);
    }
    void Update()
    {
        OnTileSelect();
    }
    void updateTurnSummary()
    {
        UIDialog turnDialog = turnDialogObj.GetComponent<UIDialog>();
        turnDialog.title.text = string.Format("Year: {0:N0} | Week: {1:N0}", (turn / 15) + 1, turn % 15);
        turnDialog.buttonLister = new Turn(cash, causalityBreaches);
    }
    int validateCropFutures()
    {
        var causalityBreaches = 0;
        foreach (Field field in fields.Values)
        {
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
}
