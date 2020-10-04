using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.U2D;
public class Field
{
    Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
    UIDialog dialog;
    Crop crop;
    public Crop HarvestedCrop;
    Dictionary<int, string> futureCrops = new Dictionary<int, string>();
    List<IButton> actions;
    IButtonLister actionLister;
    Action updateTile;
    Vector3Int pos;
    Tilemap tilemap;
    Action<int> Charge;
    int turn = 1;
    class ActionLister : IButtonLister 
    {
        public List<IButton> Buttons { get; set; }
        public ActionLister(List<IButton> actions)
        {
            Buttons = actions;
        }
    }
    enum fieldMenuEnum
    {
        Shop,
        Harvest,
        None,
    }
    public int GetCausalityBreach(int turn)
    {
        // Return Codes
        // 1: Breach
        // 0: Resolved
        // -1: No futures
        string expectedCrop;
        if (futureCrops.TryGetValue(turn, out expectedCrop))
        {
            if (crop == null || expectedCrop != crop.Name)
            {
                return 1;
            }
            return 0;
        }
        return -1;
    }
    FieldMenu getFieldMenu(fieldMenuEnum fieldMenuType)
    {
        FieldMenu fieldMenu = null;
        switch (fieldMenuType)
        {
            case fieldMenuEnum.Shop: 
                fieldMenu = new Shop(this);
                break;
            case fieldMenuEnum.Harvest:
                fieldMenu = new Harvest(this, turn);
                break;
        }
        return fieldMenu;
    }
    void UpdateUIDialog(FieldMenu fieldMenu)
    {
        if (fieldMenu != null)
        {
            actions = fieldMenu.Buttons;
            dialog.buttonLister = new ActionLister(fieldMenu.Buttons);
            dialog.SetTitle(fieldMenu.Name);        
        }
    }
    public void HarvestCrop(int turn, Crop crop)
    {
        futureCrops.Add(turn, crop.Name);
        Debug.Log("resolve one year from: " + turn);
        HarvestedCrop = crop;
    }
    public string GetCropName(){
        return crop.Name;
    }
    public void DeleteCrop()
    {
        crop = null;
        UpdateSprite(sprites["empty_field"]);
    }
    public void OnClick()
    {
        var fieldMenuType = fieldMenuEnum.None;
        if (crop == null && Input.GetMouseButtonDown(0))
        {
            fieldMenuType = fieldMenuEnum.Shop;
            // Debug.Log("new shop: " + fieldMenu.ToString());
        } else if (HarvestedCrop == null && Input.GetMouseButtonDown(1))
        {
            fieldMenuType = fieldMenuEnum.Harvest;
            // Debug.Log("new harvest: " + fieldMenu.ToString());
        }
        FieldMenu fieldMenu = getFieldMenu(fieldMenuType);
        if (fieldMenu != null)
        {
            UpdateUIDialog(fieldMenu);
            dialog.ShowPanel();
        }
    }
    public void AddCrop(Crop crop)
    {
        Charge(crop.Cost);
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
        HarvestedCrop = null;
        if (crop == null) { return; }
        Sprite sprite = crop.NextTurn();
        UpdateSprite(sprite);
    }
    public Field(GameObject dialogObj, Tilemap tilemap, Vector3Int pos, Farm farm)
    {
        this.dialog = dialogObj.GetComponent<UIDialog>();
        this.tilemap = tilemap;
        this.pos = pos;
        this.Charge = farm.Charge;

        SpriteAtlas atlas = Resources.Load<SpriteAtlas>("game-sprites");
        sprites.Add("empty_field", atlas.GetSprite("empty_field"));
    }
}