using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(Camera))]
[RequireComponent(typeof(TechtonicsController))]
[RequireComponent(typeof(SpawnerData))]
[RequireComponent(typeof(CreatureController))]
public class Spawner : MonoBehaviour
{
    public GameObject[,] TilePlate;
    public GameObject[,] TileVert;
    public GameObject[,] TileEdgeX;
    public GameObject[,] TileEdgeY;

    Camera cam;
    TechtonicsController techtonicsController;
    SpawnerData spawnerData;
    CreatureController creatureController;

    void Start()
    {
        cam = GetComponent<Camera>();
        techtonicsController = GetComponent<TechtonicsController>();
        spawnerData = GetComponent<SpawnerData>();
        creatureController = GetComponent<CreatureController>();

        SetUpCam();

        TilePlate = spawnerData.TilePlate;
        TileEdgeX = spawnerData.TileEdgeX;
        TileEdgeY = spawnerData.TileEdgeY;
        TileVert = spawnerData.TileVert;

        techtonicsController.InitialisePlates(TilePlate, TileVert, TileEdgeX, TileEdgeY);
        creatureController.InitialiseCreatures();
    }

    void SetUpCam()
    {
        cam.transform.position = new Vector3(0, math.sqrt((spawnerData.Size.x * 1.5f * spawnerData.Size.x * 1.5f) + (spawnerData.Size.y * 1.5f * spawnerData.Size.y * 1.5f)) - 1, 0);
        cam.transform.rotation = Quaternion.Euler(45, 45, 0);

        cam.orthographic = true;

        cam.orthographicSize = math.sqrt((spawnerData.Size.x * 1.5f * spawnerData.Size.x * 1.5f) + (spawnerData.Size.y * 1.5f * spawnerData.Size.y * 1.5f));
        cam.backgroundColor = Color.black;
    }

    public void ClearSelection()
    {
        for (int i = 0; i < spawnerData.Size.y; i++)
        {
            for (int j = 0; j < spawnerData.Size.x; j++)
            {
                TilePlate[i, j].GetComponent<Selector>().isSelected = false;
                TilePlate[i, j].GetComponent<Animation>().SelectedDeHighlight();
            }
        }
    }
}
