using UnityEngine;

public class CreatureController : MonoBehaviour
{
    public GameObject[,] Map;
    SpawnerData spawner;
    [SerializeField] GameObject Predator;
    [SerializeField] GameObject Prey;
    public GameObject[,] Animals;

    private string AkFox = "Fox";



    public void InitialiseCreatures()
    {
        gameObject.AddComponent<AkGameObj>();
        spawner = GetComponent<SpawnerData>();
        Map = new GameObject[(int)spawner.Size.x * 2, (int)spawner.Size.y * 2];
        Animals = new GameObject[(int)spawner.Size.x * 2, (int)spawner.Size.y * 2];

        Debug.Log("Map Instantiated");

        GenerateMap();

        Map[0, 0].GetComponent<PlateData>().Territory = Predator.name + "Origin";
        Animals[0, 0] = Instantiate(Predator, Map[0, 0].transform.position * 3 + new Vector3(0, 2, 0), Quaternion.LookRotation(new Vector3(0, 0, 1)));
        Debug.Log(Predator.name + " seeded");

        Map[3,3].GetComponent<PlateData>().Territory = Prey.name + "Origin";
        Animals[3,3] = Instantiate(Prey, Map[3,3].transform.position * 3 + new Vector3(0, 2, 0), Quaternion.LookRotation(new Vector3(0, 0, 1)));

        GenerateTerritory();
    }

    public void GenerateMap()
    {
        for (int i = 0; i < Map.GetLength(0); i += 2)
        {
            for (int j = 0; j < Map.GetLength(1); j += 2)
            {
                Map[i, j] = spawner.TilePlate[i / 2, j / 2];
                Map[i + 1, j] = spawner.TileEdgeX[i / 2, j / 2];
                Map[i, j + 1] = spawner.TileEdgeY[i / 2, j / 2];
                Map[i + 1, j + 1] = spawner.TileVert[i / 2, j / 2];
            }
        }
        Debug.Log("Map Generated");
    }

    void ClearTerritory()
    {
        for (int i = 0; i < Map.GetLength(0); i++)
        {
            for (int j = 0; j < Map.GetLength(1); j++)
            {
                if (!Map[i, j].GetComponent<PlateData>().Territory.Contains(Predator.name + "Origin"))
                {
                    Map[i, j].GetComponent<PlateData>().Territory = Map[i, j].GetComponent<PlateData>().Territory.Replace(Predator.name,"").Trim();
                }
            }
        }
        Debug.Log("Territory Cleared");
    }

    void GenerateTerritory()
    {
        ClearTerritory();
        for (int itter = 0; itter < Map.GetLength(0); itter++)
        {
            for (int i = 0; i < Map.GetLength(0); i++)
            {
                for (int j = 0; j < Map.GetLength(1); j++)
                {
                    if (Map[i, j].GetComponent<PlateData>().Territory.Contains(Predator.name))
                    {
                        if (Map[i, j].GetComponent<PlateData>().PlateType == "Land")
                        {
                            if (i > 0)
                            {
                                if (Map[i - 1, j].GetComponent<PlateData>().PlateType == "Land") if(!Map[i-1, j].GetComponent<PlateData>().Territory.Contains(Predator.name)) Map[i - 1, j].GetComponent<PlateData>().Territory += " " + Predator.name;
                            }
                            if (j > 0)
                            {
                                if (Map[i, j - 1].GetComponent<PlateData>().PlateType == "Land") if(!Map[i, j-1].GetComponent<PlateData>().Territory.Contains(Predator.name)) Map[i, j - 1].GetComponent<PlateData>().Territory += " " + Predator.name;
                            }
                            if (i < Map.GetLength(0) - 1)
                            {
                                if (Map[i + 1, j].GetComponent<PlateData>().PlateType == "Land") if(!Map[i+1, j].GetComponent<PlateData>().Territory.Contains(Predator.name)) Map[i + 1, j].GetComponent<PlateData>().Territory += " " + Predator.name;
                            }
                            if (j < Map.GetLength(1) - 1)
                            {
                                if (Map[i, j + 1].GetComponent<PlateData>().PlateType == "Land") if(!Map[i, j+1].GetComponent<PlateData>().Territory.Contains(Predator.name)) Map[i, j + 1].GetComponent<PlateData>().Territory += " " + Predator.name;
                            }
                        }
                    }
                }
            }
        }
        Debug.Log("Territory Made");
    }

    public void ShowTerritory()
    {
        GenerateTerritory();
        for (int i = 0; i < Map.GetLength(0); i++)
        {
            for (int j = 0; j < Map.GetLength(1); j++)
            {
                if (Animals[i, j] != null)
                {
                    Animals[i, j].GetComponent<Animator>().SetTrigger("trWalk");
                    Animals[i, j].GetComponent<AnimationLib>().JumpSpin();
                    Animals[i, j].GetComponent<AnimationLib>().Spinning = Random.Range(-4, 8);
                    if (Animals[i,j].name.Contains(Prey.name) && Map[i, j].GetComponent<PlateData>().Territory.Contains(Predator.name))
                    {
                        Animals[i, j].GetComponentInChildren<TintController>().FadeStart(new Color(1, 0, 0));
                    }
                }

                if (Map[i, j].GetComponent<PlateData>().Territory.Contains(Predator.name))
                {
                    Map[i, j].GetComponent<TintController>().FadeStart(new Color(1, 0, 0));
                    foreach (Transform item in Map[i, j].GetComponentsInChildren<Transform>())
                    {
                        if (item.GetComponent<TintController>() != null)
                        {
                            item.GetComponent<TintController>().FadeStart(new Color(1, 0, 0));
                        }
                    }
                    if (Map[i, j].GetComponent<AnimationLib>().wobbleKillTimer > 1) Map[i, j].GetComponent<AnimationLib>().DoAnimation(Map[i, j].GetComponent<AnimationLib>().Wobble());
                }
            }
        }
    }

    public void HideTerritory()
    {
        for (int i = 0; i < Map.GetLength(0); i++)
        {
            for (int j = 0; j < Map.GetLength(1); j++)
            {
                Map[i, j].GetComponent<TintController>().FadeEnd();
                foreach (Transform item in Map[i, j].GetComponentsInChildren<Transform>())
                    {
                        if (item.GetComponent<TintController>() != null)
                        {
                            item.GetComponent<TintController>().FadeEnd();
                        }
                    }
            }
        }
    }
}
