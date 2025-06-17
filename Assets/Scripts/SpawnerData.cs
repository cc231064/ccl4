using UnityEngine;
using Rng = UnityEngine.Random;

public class SpawnerData : MonoBehaviour
{
    public Vector2 Size = new Vector2(3,3);
    public string[,] Plates = new string[3,3]{
        { "Land","Land","Land"},
        { "Ocean","Mountain","Ocean"},
        { "Land","Land","Land"}};
    public string[,] EdgesX = new string[3,3]{
        { "Land","Land","Land"},
        { "Land","Land","Ocean"},
        { "Land","Ocean","Land"}};
    public string[,] EdgesY = new string[3,3]{
        { "Land","Land","Land"},
        { "Land","Land","Ocean"},
        { "Land","Ocean","Land"}};
    public string[,] Verts = new string[3,3]{
        { "Ocean","Land","Mountain"},
        { "Land","Land","Land"},
        { "Land","Land","Ocean"}};
    [SerializeField] GameObject PlateLand;
    [SerializeField] GameObject VertLand;
    [SerializeField] GameObject EdgeXLand;
    [SerializeField] GameObject EdgeYLand;

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
                TilePlate[i, j] = Instantiate(PlateLand, new Vector3(i, 0, j), Quaternion.LookRotation(new Vector3(0, 0, 1)));
                TilePlate[i, j].GetComponent<Selector>().Instantiator = GetComponent<Spawner>();
                
                TileVert[i, j] = Instantiate(VertLand, new Vector3(i, 0, j), Quaternion.LookRotation(new Vector3(0, 0, 1)));
                
                TileEdgeX[i, j] = Instantiate(EdgeXLand, new Vector3(i, 0, j), Quaternion.LookRotation(new Vector3(0, 0, 1)));

                TileEdgeY[i, j] = Instantiate(EdgeYLand, new Vector3(i, 0, j), Quaternion.LookRotation(new Vector3(0, 0, 1)));


                TilePlate[i, j].GetComponent<AnimationLib>().positionOriginal = TilePlate[i, j].transform.position * 3.0f;
                TilePlate[i, j].GetComponent<PlateData>().PlateType = Plates[i, j];

                TileVert[i, j].GetComponent<AnimationLib>().positionOriginal = TileVert[i, j].transform.position * 3.0f;
                TileVert[i, j].GetComponent<PlateData>().PlateType = Verts[i, j];

                TileEdgeX[i, j].GetComponent<AnimationLib>().positionOriginal = TileEdgeX[i, j].transform.position * 3.0f;
                TileEdgeX[i, j].GetComponent<PlateData>().PlateType = EdgesX[i, j];

                TileEdgeY[i, j].GetComponent<AnimationLib>().positionOriginal = TileEdgeY[i, j].transform.position * 3.0f;
                TileEdgeY[i, j].GetComponent<PlateData>().PlateType = EdgesY[i, j];
            }
        }
    }
}
