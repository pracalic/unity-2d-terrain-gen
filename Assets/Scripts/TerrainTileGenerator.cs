using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainTileGenerator : MonoBehaviour
{
    public int sizeX = 100, sizeY = 100;
    public bool startFromCenter = false;
    Vector3Int startPos = Vector3Int.zero;
    public ScriptableObject sO;
    // public Tilemap tileMap;
    //public GridPalette gridPallete;
    [SerializeField]
    TerrainTileGeneratorAssets tTGA;

    [SerializeField]
    GridPalette gridPallete;
    [SerializeField]
    public Tilemap tileMap;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DrawTerrain(Tilemap tm, TileBase tb)
    {
        tm.SetTile(new Vector3Int(0, 0), tb);
        tm.SetTile(new Vector3Int(0, 0), tb);
    }

    void AsignnTerrainTileGeneratorAssets()
    {
        if (tTGA == null)
            tTGA = GetComponent<TerrainTileGeneratorAssets>();
    }

    public void ClearTerrain()
    {
        AsignnTerrainTileGeneratorAssets();
        tTGA.tileMap.ClearAllTiles();
        tTGA.colliderTileMap.ClearAllTiles();
    }

    public void DrawTerrain()
    {
        ClearTerrain();

        if (!startFromCenter)
        {
            startPos.x = -tTGA.width / 2;
            startPos.y = -tTGA.height / 2;
        }
        else
        {
            startPos.x = startPos.y = 0;
        }

        //Debug.Log("Rysuje na " + startPos);
        //if (tTGA.genType == TERRAIN_GEN_TYPE.SIMPLE_RECT)
        //{
        //    for (int i = 0; i < tTGA.width; i++)
        //    {
        //        for (int j = 0; j < tTGA.height; j++)
        //        {
        //            tTGA.tileMap.SetTile(startPos + new Vector3Int(i, j), tTGA.tileBase[GetRandomBaseTile()]);
        //        }
        //    }
        //}

        switch (tTGA.genType)
        {
            case TERRAIN_GEN_TYPE.SIMPLE_RECT:
                {
                    SimpleRectGeneration();
                    SimpleFenceGenerator();
                    break;
                }
            case TERRAIN_GEN_TYPE.ADVANCE_RECT:
                {
                    break;
                }
            case TERRAIN_GEN_TYPE.ONE_ISLAND:
                {
                    break;
                }

            case TERRAIN_GEN_TYPE.FEW_ISLANDS:
                {

                    break;
                }
            default:
                {
                    Debug.Log("Do nothing");
                    break;
                }

        }

        // tTGA.tileMap.SetTile(startPos, tTGA.tileBase[1]);

    }

    void SimpleRectGeneration()
    {
        for (int i = 0; i < tTGA.width; i++)
        {
            for (int j = 0; j < tTGA.height; j++)
            {
                tTGA.tileMap.SetTile(startPos + new Vector3Int(i, j), tTGA.tileBase[GetRandomBaseTile()]);
            }
        }
    }

    void SimpleFenceGenerator()
    {
        for (int i = 0; i < tTGA.width; i++)
        {
           // if (i == 0 || i ==tTGA.width-1)
            for (int j = 0; j < tTGA.height; j++)
            {
                tTGA.colliderTileMap.SetTile(startPos + new Vector3Int(i, j), GetFenceTile(i,j));
            }
        }

    }

    TileBase GetFenceTile(int i, int j)
    {
        if ((i == 0) && (j == 0))
            return tTGA.fanceTileBase[tTGA.cornerFanceTilesIndexes[2]];
        if (i == tTGA.width - 1 && j == 0)
            return tTGA.fanceTileBase[tTGA.cornerFanceTilesIndexes[3]];
        if (i == tTGA.width - 1 && j == tTGA.height-1)
            return tTGA.fanceTileBase[tTGA.cornerFanceTilesIndexes[1]];
        if (i == 0 && j == tTGA.height-1)
            return tTGA.fanceTileBase[tTGA.cornerFanceTilesIndexes[0]];

        if(i == 0 || i==tTGA.width-1 &&j>0)
            return tTGA.fanceTileBase[tTGA.sidFanceTileIndexes[1]];
        if(j==0 || j==tTGA.height -1 && i >0)
            return tTGA.fanceTileBase[tTGA.sidFanceTileIndexes[0]];
        return null;
    }

    int GetRandomBaseTile()
    {
        float rand = Random.Range(0f, 1f);
       // Debug.Log(rand);
        if (rand < tTGA.chanceForChoseTileFromTilesBasePlus)
            return tTGA.tilesBasePlusIndexes[Random.Range(0, tTGA.tilesBasePlusIndexes.Length)];
        else
            return tTGA.tileBaseIndex;
    }
    
}
