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
        int childCount = tTGA.obstacleParent.childCount;
        GameObject[] obToDestroy = new GameObject[childCount];
        for (int i = 0; i < childCount; i++)
        {
            obToDestroy[i] = tTGA.obstacleParent.GetChild(i).gameObject;
        }
        for (int i = 0; i<obToDestroy.Length; i++)
        {
            DestroyImmediate(obToDestroy[i]);
        }
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
                    GenerateObstacles();
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


    void GenerateObstacles()
    {
        if (tTGA.width >= 3 && tTGA.height >= 3)
        {
            int howMany = (int)Mathf.Floor(tTGA.width * tTGA.height * tTGA.obstaclesOnMap);
            Debug.Log(howMany);
            int[] randomPlaces;
            ObstacleObiect[] randObstacles = new ObstacleObiect[howMany];
            randomPlaces = new int[howMany];
            bool[] drawItem = new bool[howMany];
            for (int i = 0; i < howMany; i++)
            {
                int randWidth = Random.Range(1, tTGA.width);
                int randHeight = Random.Range(1, tTGA.height);

               // Debug.Log(randWidth + "   " + randHeight);
                randomPlaces[i] = randWidth + randHeight*tTGA.width;
               // Debug.Log(randomPlaces[i]);
                randObstacles[i] = tTGA.obstaleObiect[Random.Range(0, tTGA.obstaleObiect.Length)];
                drawItem[i] = true;
            }

           

            for (int i = 0; i < howMany; i++)
            {
                for (int j = 1; j < howMany; j++)
                {
                    if (i != j && drawItem[i] && drawItem[j])
                    {
                        if (randomPlaces[i] == randomPlaces[j])
                        {
                            drawItem[j] = false;
                        }

                        if(drawItem[j]==true)
                        if (randObstacles[i].size > 0 || randObstacles[j].size > 0)
                        {
                           Vector2 pos1 = new Vector2(randomPlaces[i]%tTGA.width, Mathf.Floor(randomPlaces[i]/tTGA.width));
                            Vector2 pos2 = new Vector2(randomPlaces[j] % tTGA.width, Mathf.Floor(randomPlaces[j] / tTGA.width));

                                //Debug.Log(pos1 +"   " + pos2);
                                bool draw = true;
                            if (pos1.x < pos2.x)
                            {
                                   
                                    if (pos1.x + randObstacles[i].size >= pos2.x - randObstacles[j].size)
                                    {
                                        if (pos1.y < pos2.y)
                                        {
                                            if (pos1.y + randObstacles[i].size >= pos2.y - randObstacles[j].size)
                                                draw = false;
                                        }
                                        else
                                        {
                                            if (pos2.y + randObstacles[j].size >= pos1.y - randObstacles[i].size)
                                                draw = false;
                                        }
                                    }
                                    drawItem[j] = draw;
                            }
                            else
                            {
                                    if (pos2.x + randObstacles[j].size >= pos1.x - randObstacles[i].size)
                                    {
                                        if (pos1.y < pos2.y)
                                        {
                                            if (pos1.y + randObstacles[i].size >= pos2.y - randObstacles[j].size)
                                                draw = false;
                                        }
                                        else
                                        {
                                            if (pos2.y + randObstacles[j].size >= pos1.y - randObstacles[i].size)
                                                draw = false;
                                        }
                                    }
                                    drawItem[j] = draw;
                                
                            }

                              
                        }
                    }
                }

            }

            for (int i = 1; i < tTGA.width - 1; i++)
            {
                for (int j = 1; j < tTGA.height - 1; j++)
                {
                    for (int k = 0; k < howMany; k++)
                    {
                        if (randomPlaces[k] == i + j*tTGA.width)
                        {
                            if (drawItem[k])
                            {
                                Instantiate(randObstacles[k].gob, startPos + new Vector3Int(i, j)+new Vector3(0.5f, 0.5f,0), Quaternion.identity, tTGA.obstacleParent);
                            }
                        }
                    }
                }
            }
        }
    }
}
