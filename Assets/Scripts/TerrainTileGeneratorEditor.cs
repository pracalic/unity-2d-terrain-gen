using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.Tilemaps;

[CustomEditor(typeof(TerrainTileGenerator))]
public class TerrainTileGeneratorEditor : Editor
{

    //[SerializeField]
    //GridPalette gridPallete;
    [SerializeField]
    Tilemap tileMap;


    //[SerializeField]
    //TerrainTileGeneratorAssets tTGA;
    public override void OnInspectorGUI()
    {
        TerrainTileGenerator myTarget = (TerrainTileGenerator)target;

        //myTarget.experience = EditorGUILayout.IntField("Experience", myTarget.experience);

        myTarget.sizeX = EditorGUILayout.IntField("Terrain width", myTarget.sizeX);
        myTarget.sizeY = EditorGUILayout.IntField("Terrain height", myTarget.sizeY);

        

        myTarget.startFromCenter = EditorGUILayout.Toggle("Start from center", myTarget.startFromCenter);
       // myTarget.tileMap = EditorGUILayout.

      //  myTarget.tileMap = EditorGUILayout.
        //EditorGUILayout.LabelField("Level", myTarget.sizeX.ToString());
        if (GUILayout.Button("Generuj teren"))
        {
            //Debug.Log("Generuje teren");
            myTarget.DrawTerrain();
        }

        if (GUILayout.Button("Usun teren"))
        {
            //Debug.Log("Generuje teren");
            myTarget.ClearTerrain();
        }
    }
}