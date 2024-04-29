using UnityEngine;
using UnityEngine.Tilemaps;

public enum TERRAIN_GEN_TYPE
{
    SIMPLE_RECT,
    ADVANCE_RECT,
    ONE_ISLAND,
    FEW_ISLANDS,
}

public class TerrainTileGeneratorAssets : MonoBehaviour
{
    [SerializeField]
    public bool startFromCenter = false;
    [SerializeField]
    public bool fenceGeneration = false;
    [SerializeField]
    public TERRAIN_GEN_TYPE genType = TERRAIN_GEN_TYPE.SIMPLE_RECT;
    [SerializeField]
    public int width = 100;
    [SerializeField]
    public int height = 100;
    [SerializeField]
    public TileBase[] tileBase;
    [SerializeField]
    public TileBase[] fanceTileBase;
    [SerializeField]
    public TileBase[] seaTileBase;

    [SerializeField]
    public int[] cornerFanceTilesIndexes = { 1, 3, 9, 11 };
    public int[] sidFanceTileIndexes = { 14, 4 };
    public int[] cornerGrassTilesIndexes = { 0, 2, 20, 22 };
    public int[] sideGrassTilesIndexes = { 1, 12, 21, 10 };

   //[SerializeField]
   // Tile tile;
   // [SerializeField]
   // Tile
    [SerializeField]
    public Tilemap tileMap;
    [SerializeField]
    public Tilemap tileMapBorders;
    [SerializeField]
    public Tilemap colliderTileMap;
    [SerializeField]
    public Tilemap seaTileMap;
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
