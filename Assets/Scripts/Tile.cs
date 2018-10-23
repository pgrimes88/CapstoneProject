﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class Tile : MonoBehaviour {

    public enum TileColor { standard, move, attack, ally }
    public enum TileType { normal, asteroid, debris}

    private const int gridSize = 10;

    private Vector2Int gridPos;

    [SerializeField] Unit unitOnTile;
    [SerializeField] TileType type;


    [Header("Material Colors")]
    [SerializeField] Color baseColor;
    [SerializeField] Color moveRangeColor;
    [SerializeField] Color attackRangeColor;
    [SerializeField] Color enemyColor;
    [SerializeField] Color allyColor;

    //[Header("Tile Type Prefabs")]
    //[SerializeField] GameObject normalTilePrefab;
    //[SerializeField] GameObject asteroidTilePrefab;
    //[SerializeField] GameObject debrisTilePrefab;


    Renderer[] childrenRenderers;

    //MOVED TO MAP
    ////TODO Move this to the search itself
    //bool visted = false;

    //public bool Visted
    //{
    //    get
    //    {
    //        return visted;
    //    }

    //    set
    //    {
    //        visted = value;
    //    }
    //}

    public static int GridSize
    {
        get
        {
            return gridSize;
        }
    }

    public Unit UnitOnTile
    {
        get
        {
            return unitOnTile;
        }

        set
        {
            unitOnTile = value;
        }
    }

    public TileType Type
    {
        get
        {
            return type;
        }

        set
        {
            type = value;
        }
    }

    //TODO Clean up coordianate systems
    private void Awake()
    {
        UnitOnTile = null;
        gridPos = GetGridPos();
    }

    // Use this for initialization
    void Start () {
        childrenRenderers = GetComponentsInChildren<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {

    }

    //GRID FUNCTIONS

    //Returns Position within the grid (0,0) (3,2) etc
    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            (int)Mathf.Floor(transform.position.x / gridSize),
            (int)Mathf.Floor(transform.position.z / gridSize)
        );
    }

    //TODO Possibly removed this and multiply GetGridPos by 10 any time actual coords are needed
    //Returns Unity Coordidates of the tile (0,0) (30,20) etc
    public Vector2Int GetCoords()
    {
        return new Vector2Int(
            (int)Mathf.Floor(transform.position.x / gridSize) * gridSize,
            (int)Mathf.Floor(transform.position.z / gridSize) * gridSize
        );
    }

    //TODO Convert to SendMessageUpwards
    void OnMouseDown(){
        BattleManager.instance.TileClicked(this);
	}



    public void ResetTileColor()
    {
        SetColor(baseColor);
    }


    //COLOR FUNCTIONS
    public void SetTileColor(TileColor color)
    {
        switch (color)
        {
            case TileColor.standard:
                SetColor(baseColor);
                break;
            case TileColor.move:
                SetColor(moveRangeColor);
                break;
            case TileColor.attack:
                SetColor(attackRangeColor);
                break;
            case TileColor.ally:
                SetColor(allyColor);
                break;
            default:
                SetColor(baseColor);
                break;
        }
    }

    public void SetColor(Color color)
    {
        Queue<Color> colors = new Queue<Color>();
        foreach (Renderer r in childrenRenderers)
        {
            r.material.color = color;
            colors.Enqueue(color);
        }
        SendMessage("UpdateBaseColorQueue", colors);
    }


    //HIGHLIGHT FUNCTIONS
    private void StartHighlightAnimation()
    {

    }

    private void StopHighlightAnimation()
    {

    }

    //DEPRECIATED
    //public void ResetTileSearchFields()
    //{
    //    visted = false;
    //    exploredFrom = null;
    //}

    //DEPRECIATED Use SetTileColor instead
    //Update all faces material
    //public void UpdateMaterial(Material material)
    //{
    //    foreach (Renderer renderer in childrenRenderers)
    //    {
    //        renderer.material = material;
    //    }
    //}
}
