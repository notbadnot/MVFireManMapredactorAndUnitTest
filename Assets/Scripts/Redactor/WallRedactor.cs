using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;


[ExecuteInEditMode]
public class WallRedactor : MonoBehaviour
{
    public WallConfig walls;


    [SerializeField] GameObject wallFromScene;
    [SerializeField] string mapName = "defaultMapName";

    [SerializeField] private GameObject prefab;

    [SerializeField] int minX=-12;
    [SerializeField] int maxX=12;
    [SerializeField] int minZ = -12;
    [SerializeField] int maxZ = 12;


    public void Save()
    {
        if (walls == null || walls.name != mapName )
        {
            walls = ScriptableObject.CreateInstance<WallConfig>();
            AssetDatabase.CreateAsset(walls, $"Assets/Resources/Levels/{mapName}.asset");
        }

        walls.VecPositions = new List<Vector3>();
        Debug.Log(walls.name);
        for (int i = 0; i<wallFromScene.transform.childCount; i++)
        {
            walls.VecPositions.Add(wallFromScene.transform.GetChild(i).position);
        }
        EditorUtility.SetDirty(walls);
        AssetDatabase.SaveAssets();



    }

    [MenuItem("CONTEXT/WallRedactor/Save")]

    private static void Savestatic(MenuCommand command)
    {
        Debug.Log("Saving ");
        var wallRedactor = command.context as WallRedactor;
        wallRedactor.Save();
        //wallRedactor.Save();
    }

    [MenuItem("CONTEXT/WallRedactor/CreateCircleWalls")]
    private static void CreateCircleWalls (MenuCommand command)
    {
        var wallRedactor = command.context as WallRedactor;
        List<Vector3> allChildrenPosition = new List<Vector3>();
        for (int i=0; i < wallRedactor.wallFromScene.transform.childCount;i++)
        {
            allChildrenPosition.Add(wallRedactor.wallFromScene.transform.GetChild(i).position);
        }

        for (int ix = wallRedactor.minX;ix <= wallRedactor.maxX; ix++)
        {
            int iz = wallRedactor.minZ;
            for (int count = 0; count < 2; count++)
            {
                var pointToSpawn = new Vector3(ix, 1, iz);
                if (!allChildrenPosition.Contains(pointToSpawn))
                {
                    var prefab = PrefabUtility.InstantiatePrefab(wallRedactor.prefab, wallRedactor.wallFromScene.transform) as GameObject;
                    prefab.transform.position = pointToSpawn;
                    allChildrenPosition.Add(pointToSpawn);
                }
                iz = wallRedactor.maxZ;
            }
        }


        for (int iz = wallRedactor.minZ; iz <= wallRedactor.maxZ; iz++)
        {
            int ix = wallRedactor.minX;
            for (int count = 0; count < 2; count++)
            {
                var pointToSpawn = new Vector3(ix, 1, iz);
                if (!allChildrenPosition.Contains(pointToSpawn))
                {
                    var prefab = PrefabUtility.InstantiatePrefab(wallRedactor.prefab, wallRedactor.wallFromScene.transform) as GameObject;
                    prefab.transform.position = pointToSpawn;
                    allChildrenPosition.Add(pointToSpawn);
                }
                ix = wallRedactor.maxX;
            }
        }
    }
}
