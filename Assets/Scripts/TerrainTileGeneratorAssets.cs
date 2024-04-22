using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainTileGeneratorAssets : MonoBehaviour
{
    [SerializeField]
    public int width = 100;
    [SerializeField]
    public int height = 100;
    [SerializeField]
   public TileBase[] tileBase;
   //[SerializeField]
   // Tile tile;
   // [SerializeField]
   // Tile
    [SerializeField]
    public Tilemap tileMap;
    //indeks bazowego tile generacji
    [SerializeField]
    public int tileBaseIndex = 11;
    //szansa na zast¹pienie bazowego indeksu jednym z indeksów dodatkowych
    [SerializeField]
    public float chanceForChoseTileFromTilesBasePlus = 0.3f;
    //indeksy dodakowych tile (to równie¿ bazowe tile)
    [SerializeField]
    public int[] tilesBasePlusIndexes = { 52, 53, 54, 55, 56, 57, 62, 63, 64, 65, 66, 67 };

}
