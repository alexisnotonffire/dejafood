using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Field
{
    UIDialog dialog;
    Crop crop;
    public Crop HarvestedCrop;
    Dictionary<int, string> futureCrops = new Dictionary<int, string>();
    List<IButton> actions;
    IButtonLister actionLister;
    Action updateTile;
    Vector3Int pos;
    Tilemap tilemap;
    int turn = 1;
    class ActionLister : IButtonLister 
    {
        public List<IButton> Buttons { get; set; }
        public ActionLister(List<IButton> actions)
        {
            Buttons = actions;
        }
    }
    public int GetCausalityBreach(int turn)
    {
        // Return Codes
        // 1: Breach
        // 0: Resolved
        // -1: No futures
        string expectedCrop;
        if (!futureCrops.TryGetValue(turn, out expectedCrop))
        {
            if (crop == null || expectedCrop != crop.Name)
            {
                return 1;
            }
            return 0;
        }
        return -1;
    }
    FieldMenu getFieldMenu()
    {
        FieldMenu fieldMenu = null;
        if (crop == null && Input.GetMouseButtonDown(0))
        {
            fieldMenu = new Shop(this);
            // Debug.Log("new shop: " + fieldMenu.ToString());
        } else if (Input.GetMouseButtonDown(1))
        {
            fieldMenu = new Harvest(this, turn);
            // Debug.Log("new harvest: " + fieldMenu.ToString());
        }
        return fieldMenu;
    }
    void UpdateActions()
    {
        // Debug.Log("updating actions");
        var fieldMenu = getFieldMenu();
        if (fieldMenu != null)
        {
            actions = fieldMenu.Buttons;
        }
        // Debug.Log("actions count: " + actions.Count);
        actionLister = new ActionLister(actions);
    }
    public void HarvestCrop(int turn, Crop crop)
    {
        futureCrops.Add(turn, crop.Name);
        HarvestedCrop = crop;
    }
    public string GetCropName(){
        return crop.Name;
    }
    public void DeleteCrop()
    {
        crop = null;
    }
    public void OnClick()
    {
        UpdateActions();
        dialog.buttonLister = actionLister;
        // Debug.Log("actions: " + actionLister.Buttons.Count);
        // Debug.Log("dialog: " + dialog.buttonLister.Buttons.Count);
        dialog.ShowPanel();
    }
    public void AddCrop(Crop crop)
    {
        this.crop = crop;
        UpdateSprite(crop.GetSprite());
    }
    void UpdateSprite(Sprite sprite)
    {
        Tile tile = ScriptableObject.CreateInstance<Tile>();
        tile.sprite = sprite;
        tilemap.SetTile(pos, tile);
    }
    public void NextTurn()
    {
        turn++;
        if (crop == null) { return; }
        Sprite sprite = crop.NextTurn();
        UpdateSprite(sprite);
    }
    public Field(GameObject dialogObj, Tilemap tilemap, Vector3Int pos)
    {
        this.dialog = dialogObj.GetComponent<UIDialog>();
        this.tilemap = tilemap;
        this.pos = pos;
    }
}