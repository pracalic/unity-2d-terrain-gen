using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public enum TERRAIN_GEN_TYPE
{
    SIMPLE_RECT,
    ADVANCE_RECT,
    ONE_ISLAND,
    FEW_ISLANDS,
}
[Serializable]
public class ObstacleObiect
{
    public GameObject gob;
    public int size;

}
public class TerrainTileGeneratorAssets : MonoBehaviour
{
    public bool startFromCenter = false;
    public bool fenceGeneration = false;
    public TERRAIN_GEN_TYPE genType = TERRAIN_GEN_TYPE.SIMPLE_RECT;
    public int width = 100;
    public int height = 100;
    public TileBase[] tileBase;
    public TileBase[] fanceTileBase;
    public TileBase[] seaTileBase;
    public int[] cornerFanceTilesIndexes = { 1, 3, 9, 11 };
    public int[] sidFanceTileIndexes = { 14, 4 };
    public int[] cornerGrassTilesIndexes = { 0, 2, 20, 22 };
    public int[] sideGrassTilesIndexes = { 1, 12, 21, 10 };
    public Tilemap tileMap;
    public Tilemap tileMapBorders;
    public Tilemap colliderTileMap;
    public Tilemap seaTileMap;
    //indeks bazowego tile generacji
    public int tileBaseIndex = 11;
    //szansa na zast¹pienie bazowego indeksu jednym z indeksów dodatkowych
    public float chanceForChoseTileFromTilesBasePlus = 0.3f;
    //indeksy dodakowych tile (to równie¿ bazowe tile)
    public int[] tilesBasePlusIndexes = { 52, 53, 54, 55, 56, 57, 62, 63, 64, 65, 66, 67 };
    public ObstacleObiect[] obstaleObiect;
    public float obstaclesOnMap = 0.2f;
    public Transform obstacleParent;

}
