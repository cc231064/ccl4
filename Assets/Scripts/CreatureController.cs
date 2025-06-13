using System;
using UnityEngine;

public class CreatureController : MonoBehaviour
{
    public GameObject[,] Map;
    public string[,] Territory;
    SpawnerData spawner;

    public void InitialiseCreatures()
    {
        spawner = GetComponent<SpawnerData>();
        Debug.Log("a"); 
        Debug.Log(spawner.Size);
        Map = new GameObject[(int)spawner.Size.x * 2, (int)spawner.Size.y * 2];
        Territory = new string[Map.GetLength(0), Map.GetLength(1)];
        Territory[0, 0] = "Fox Origin";

        GenerateMap();
        GenerateTerritory();

        int count = 0;
        for (int i = 0; i < Map.GetLength(0); i++)
        {
            for (int j = 0; j < Map.GetLength(1); j++)
            {
                if (Map[i, j].GetComponent<PlateData>().PlateType != "Ocean") count++;
            }
        }
        Debug.Log(count);
    }

    public void GenerateMap()
    {
        for (int i = 0; i < Map.GetLength(0); i += 2)
        {
            for (int j = 0; j < Map.GetLength(1); j += 2)
            {
                Debug.Log(i / 2 + " " + j/2);
                Map[i, j] = spawner.TilePlate[i / 2, j / 2];
                Map[i + 1, j] = spawner.TileEdgeX[i / 2, j / 2];
                Map[i, j + 1] = spawner.TileEdgeY[i / 2, j / 2];
                Map[i + 1, j + 1] = spawner.TileVert[i / 2, j / 2];
            }
        }
    }

    void ClearTerritory()
    {
        for (int i = 0; i < Map.GetLength(0); i++)
        {
            for (int j = 0; j < Map.GetLength(1); j++)
            {
                if (Territory[i, j] != "Fox Origin")
                {
                    Territory[i, j] = "";
                }
            }
        }
    }

    void GenerateTerritory()
    {
        ClearTerritory();
        for (int i = 0; i < Map.GetLength(0); i++)
        {
            for (int j = 0; j < Map.GetLength(1); j++)
            {
                //Debug.Log(Map[i, j].GetComponent<PlateData>().PlateType);
                if (Territory[i, j] == "Fox Origin" || Territory[i, j] == "Fox")
                {
                    if (Map[i, j].GetComponent<PlateData>().PlateType != "Ocean")
                    {
                        if (i > 0 && j > 0) Territory[i - 1, j - 1] = "Fox";
                        if (i > 0 && j < Map.GetLength(1) - 1) Territory[i - 1, j + 1] = "Fox";
                        if (i < Map.GetLength(0) - 1 && j > 0) Territory[i + 1, j - 1] = "Fox";
                        if (i < Map.GetLength(0) - 1 && j < Map.GetLength(1) - 1) Territory[i + 1, j + 1] = "Fox";
                    }
                }
            }
        }
    }
}
