using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Field
{
    UIDialog dialog;
    Crop crop;
    List<IButton> actions;
    IButtonLister actionLister;
    Action updateTile;
    Vector3Int pos;
    Tilemap tilemap;
    class ActionLister : IButtonLister 
    {
        public List<IButton> Buttons { get; set; }
        public ActionLister(List<IButton> actions)
        {
            Buttons = actions;
        }
    }
    void UpdateActions()
    {
        Debug.Log("updating actions");
        if (crop == null)
        {
            Shop shop = new Shop(this);
            Debug.Log("new shop: " + shop.ToString());
            actions = shop.Buttons;
            Debug.Log("actions count: " + actions.Count);
        }
        actionLister = new ActionLister(actions);
    }
    public void OnClick()
    {
        UpdateActions();
        dialog.buttonLister = actionLister;
        Debug.Log("actions: " + actionLister.Buttons.Count);
        Debug.Log("dialog: " + dialog.buttonLister.Buttons.Count);
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