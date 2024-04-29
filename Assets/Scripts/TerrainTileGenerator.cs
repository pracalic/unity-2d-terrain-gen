using UnityEngine;
using UnityEngine.Tilemaps;

public class TerrainTileGenerator : MonoBehaviour
{
    [SerializeField]
    TerrainTileGeneratorAssets tTGA;
    private Vector3Int startPos;


    void DrawTileOnTilemap(TileBase tb, Tilemap tm, Vector3Int pos)
    {
        tm.SetTile(pos, tb);
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
        tTGA.seaTileMap.ClearAllTiles();
        tTGA.tileMapBorders.ClearAllTiles();
    }

    public void DrawTerrain()
    {
        ClearTerrain();

        if (!tTGA.startFromCenter)
        {
            startPos.x = -tTGA.width / 2;
            startPos.y = -tTGA.height / 2;
        }
        else
        {
            startPos.x = startPos.y = 0;
        }

        switch (tTGA.genType)
        {
            case TERRAIN_GEN_TYPE.SIMPLE_RECT:
                {
                    SimpleRectGeneration();
                    if(tTGA.fenceGeneration)    
                        SimpleFenceGenerator();
                    GenerateSeaAround();
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

    }


    bool SimpleCorenrsGenerator(int i, int j)
    {
        int ind = -1;
        if (i == 0 && j == 0)
            ind = tTGA.cornerGrassTilesIndexes[2];

        if (i == tTGA.width - 1 && j == 0)
            ind = tTGA.cornerGrassTilesIndexes[3];

        if (i == tTGA.width - 1 && j == tTGA.height - 1)
            ind = tTGA.cornerGrassTilesIndexes[1];

        if (i == 0 && j == tTGA.height - 1)
            ind = tTGA.cornerGrassTilesIndexes[0];

        if (ind >= 0)
        {
            DrawTileOnTilemap(tTGA.tileBase[ind], tTGA.tileMapBorders, startPos + new Vector3Int(i, j));
            return true;
        }
        return false;
    }


    bool SimpleSideGenerator(int i, int j)
    {
        int ind = -1;
        if (i == 0 && j > 0)
            ind = tTGA.sideGrassTilesIndexes[3];

        if (i > 0 && j == 0)
            ind = tTGA.sideGrassTilesIndexes[2];

        if (i > 0 && j == tTGA.height - 1)
            ind = tTGA.sideGrassTilesIndexes[0];

        if (i == tTGA.width - 1 && j > 0)
            ind = tTGA.sideGrassTilesIndexes[1];

        if (ind >= 0)
        {
            DrawTileOnTilemap(tTGA.tileBase[ind], tTGA.tileMapBorders, startPos + new Vector3Int(i, j));
            return true;
        }
    
        return false;
    }
    void SimpleRectGeneration()
    {
        for (int i = 0; i < tTGA.width; i++)
        {
            for (int j = 0; j < tTGA.height; j++)
            {

                if (SimpleCorenrsGenerator(i,j))
                    continue;

                if(SimpleSideGenerator(i,j))
                    continue;

                DrawTileOnTilemap(tTGA.tileBase[GetRandomBaseTile()], tTGA.tileMap, startPos + new Vector3Int(i, j));
            }
        }
    }

    void SimpleFenceGenerator()
    {
        for (int i = 0; i < tTGA.width; i++)
        {
            for (int j = 0; j < tTGA.height; j++)
            {
                DrawTileOnTilemap(GetFenceTile(i, j), tTGA.colliderTileMap, startPos + new Vector3Int(i, j));
            }
        }
    }

    void GenerateSeaAround()
    {
        int generationMultiplier = 4;
        for (int i = 0; i < tTGA.width* generationMultiplier; i++)
            for (int j = 0; j < tTGA.height* generationMultiplier; j++)
            {
                
                Vector3Int start = startPos;
                if (start == Vector3Int.zero)
                    start -= new Vector3Int(tTGA.width *generationMultiplier/ 2 - tTGA.width/2, tTGA.height * generationMultiplier/2 - tTGA.height/2);
                else
                    start *= generationMultiplier;

                Vector3Int to = start + new Vector3Int(i, j);
                DrawTileOnTilemap(GetSeaTile(), tTGA.seaTileMap, to);
            }
    }

    TileBase GetSeaTile()
    {
        return tTGA.seaTileBase[Random.Range(0, tTGA.seaTileBase.Length)];
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
