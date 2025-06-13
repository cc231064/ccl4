using UnityEngine;
using Rng = UnityEngine.Random;

public class SpawnerData : MonoBehaviour
{
    public Vector2 Size = new Vector2(3,3);
    public string[,] Plates = new string[3,3]{
        { "Land","Land","Land"},
        { "Land","Ocean","Land"},
        { "Land","Land","Land"}};
    public string[,] EdgesX = new string[3,3]{{"Land","Land","Land"},{"Land","Land","Land"},{"Land","Land","Land"}};
    public string[,] EdgesY = new string[3,3]{{"Land","Land","Land"},{"Land","Land","Land"},{"Land","Land","Land"}};
    public string[,] Verts = new string[3,3]{{"Land","Land","Land"},{"Land","Land","Land"},{"Land","Land","Land"}};
    [SerializeField] GameObject PlateLand;
    [SerializeField] GameObject VertLand;
    [SerializeField] GameObject EdgeXLand;
    [SerializeField] GameObject EdgeYLand;
    [SerializeField] GameObject PlateOcean;
    [SerializeField] GameObject VertOcean;
    [SerializeField] GameObject EdgeXOcean;
    [SerializeField] GameObject EdgeYOcean;
    [SerializeField] GameObject PlateIsland;
    [SerializeField] GameObject VertIsland;
    [SerializeField] GameObject EdgeXIsland;
    [SerializeField] GameObject EdgeYIsland;

    public GameObject[,] TilePlate;
    public GameObject[,] TileVert;
    public GameObject[,] TileEdgeX;
    public GameObject[,] TileEdgeY;

    void Awake()
    {
        TilePlate = new GameObject[((int)Size.x), ((int)Size.y)];
        TileEdgeX = new GameObject[((int)Size.x), ((int)Size.y)];
        TileEdgeY = new GameObject[((int)Size.x), ((int)Size.y)];
        TileVert = new GameObject[((int)Size.x), ((int)Size.y)];

        for (int i = 0; i < Size.x; i++)
        {
            for (int j = 0; j < Size.y; j++)
            {
                if (Plates[i, j] == "Land")
                {
                    TilePlate[i, j] = Instantiate(PlateLand, new Vector3(i, 0, j), Quaternion.LookRotation(new Vector3(0, 0, 1)));
                    TilePlate[i, j].GetComponent<Selector>().Instantiator = GetComponent<Spawner>();
                }
                if (Plates[i, j] == "Ocean")
                {
                    TilePlate[i, j] = Instantiate(PlateOcean, new Vector3(i, -0.1f, j), Quaternion.LookRotation(new Vector3(0, 0, 1)));
                    TilePlate[i, j].GetComponent<Selector>().Instantiator = GetComponent<Spawner>();
                }

                if (Verts[i, j] == "Land")
                {
                    TileVert[i, j] = Instantiate(VertLand, new Vector3(i, 0, j), Quaternion.LookRotation(new Vector3(0, 0, 1)));
                }

                if (EdgesX[i, j] == "Land")
                {
                    TileEdgeX[i, j] = Instantiate(EdgeXLand, new Vector3(i, 0, j), Quaternion.LookRotation(new Vector3(0, 0, 1)));
                }

                if (EdgesY[i, j] == "Land")
                {
                    TileEdgeY[i, j] = Instantiate(EdgeYLand, new Vector3(i, 0, j), Quaternion.LookRotation(new Vector3(0, 0, 1)));
                }
                TilePlate[i, j].GetComponent<Animation>().positionOriginal = TilePlate[i, j].transform.position * 3.0f;
                TilePlate[i, j].GetComponent<PlateData>().PlateType = Plates[i, j];
                // Debug.Log(TilePlate[i, j].GetComponent<PlateData>().PlateType);
            }
        }
    }
}
