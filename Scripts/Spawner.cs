using Unity.Mathematics;
using UnityEngine;
using Rng = UnityEngine.Random;

[RequireComponent(typeof(Camera))]
public class Spawner : MonoBehaviour
{
    [SerializeField] public Vector2 Size;
    public int[,] Plates;
    public int[,] EdgesX;
    public int[,] EdgesY;
    public int[,] Verts;
    [SerializeField] GameObject Plate1;
    [SerializeField] GameObject Vert1;
    [SerializeField] GameObject EdgeX1;
    [SerializeField] GameObject EdgeY1;

    public GameObject[,] TilePlate;
    public GameObject[] TileVert;
    public GameObject[] TileEdgeX;
    public GameObject[] TileEdgeY;

    public GameObject Selected;

    Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();

        SetUpCam();

        Plates = new int[(int)Size.x, (int)Size.y];
        EdgesX = new int[(int)Size.x, ((int)Size.y + 1)];
        EdgesY = new int[(int)Size.x, (int)Size.y];
        Verts = new int[(int)Size.x, (int)Size.y];

        TilePlate = new GameObject[((int)Size.x), ((int)Size.y)];

        for (int i = 0; i < Size.x; i++)
        {
            for (int j = 0; j < Size.y; j++)
            {
                Plates[i, j] = Rng.Range(1, 2);
                EdgesX[i, j] = Rng.Range(1, 2);
                EdgesY[i, j] = Rng.Range(1, 2);
                Verts[i, j] = Rng.Range(1, 2);

                if (Plates[i, j] == 1)
                {
                    TilePlate[i, j] = Instantiate(Plate1, new Vector3(i + (2 / 3) - Size.x * 0.5f, 0, j + (2 / 3) - Size.y * 0.5f), Quaternion.LookRotation(new Vector3(0, 0, 1)));
                    TilePlate[i, j].GetComponent<Selector>().Instantiator = GetComponent<Spawner>();
                    TilePlate[i, j].GetComponent<Animation>().positionOriginal = TilePlate[i, j].transform.position * 3.0f;
                }

                if (Verts[i, j] == 1)
                {
                    Instantiate(Vert1, new Vector3(i + (2 / 3) - Size.x * 0.5f, 0, j + (2 / 3) - Size.y * 0.5f), Quaternion.LookRotation(new Vector3(0, 0, 1)));
                }

                if (EdgesX[i, j] == 1)
                {
                    Instantiate(EdgeX1, new Vector3(i + (2 / 3) - Size.x * 0.5f, 0, j + (2 / 3) - Size.y * 0.5f), Quaternion.LookRotation(new Vector3(0, 0, 1)));
                }

                if (EdgesY[i, j] == 1)
                {
                    Instantiate(EdgeY1, new Vector3(i + (2 / 3) - Size.x * 0.5f, 0, j + (2 / 3) - Size.y * 0.5f), Quaternion.LookRotation(new Vector3(0, 0, 1)));
                }
            }
        }
    }

    void SetUpCam()
    {
        cam.transform.position = new Vector3(-Size.x * 1.5f, math.sqrt((Size.x * 1.5f * Size.x * 1.5f) + (Size.y * 1.5f * Size.y * 1.5f)) - 1, -Size.y * 1.5f);
        cam.transform.rotation = Quaternion.Euler(45, 45, 0);

        cam.orthographic = true;

        cam.orthographicSize = math.sqrt((Size.x * 1.5f * Size.x * 1.5f) + (Size.y * 1.5f * Size.y * 1.5f));
        cam.backgroundColor = Color.black;
    }

    public void ClearSelection()
    {
        for (int i = 0; i < Size.x * Size.y; i++)
        {
            TilePlate[(int)(i % Size.x), (int)(i / Size.x)].GetComponent<Selector>().isSelected = false;
            TilePlate[(int)(i % Size.x), (int)(i / Size.x)].GetComponent<Animation>().SelectedDeHighlight();
        }
    }
}
