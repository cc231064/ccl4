using UnityEngine;
using AK.Wwise;

public class PlateData : MonoBehaviour
{
    
    public string PlateType;
    public Vector3 SnapDistance = new Vector3(1, 1, 1);
    public Vector3 SnapOffset;
    public Vector3 SnappedCoord;
    public TechtonicsController techtonicsController;

    private string AKEventButton = "Mountain";

    private GameObject[] Foliage;
    [SerializeField] GameObject? Tree;
    void Start()
    {
        gameObject.AddComponent<AkGameObj>();
        SnappedCoord = transform.position;
        techtonicsController = GetComponent<TechtonicsController>();
        Foliage = new GameObject[(int)(transform.localScale.x * transform.localScale.y)*20];

        UpdateSnap();

        for (int i = 0; i < Foliage.Length; i++)
        {
            if (Tree && PlateType == "Land")
            {
                Foliage[i] = Instantiate(Tree, transform.position + new Vector3(Random.Range(-transform.localScale.x / 2, transform.localScale.x / 2), 1, Random.Range(-transform.localScale.z / 2, transform.localScale.z / 2)), Quaternion.LookRotation(new Vector3(0, 0, 1)));
                for (int j = 0; j < i; j++)
                {
                    if ((Foliage[i].transform.position - Foliage[j].transform.position).magnitude < 0.3)
                    {
                        Destroy(Foliage[i]);
                    }
                }
            }
        }
    }

    void UpdateSnap()
    {
        transform.position = SnapOffset + new Vector3(SnappedCoord.x * SnapDistance.x, SnappedCoord.y * SnapDistance.y, SnappedCoord.z * SnapDistance.z);
    }

    public void PlateConverge(GameObject inputPlate)
    {
        if (PlateType == "Land")
        {
            if (inputPlate.GetComponent<PlateData>().PlateType == "Land")
            {
                Debug.Log("Become Mountain");
                transform.localScale += new Vector3(0, 1, 0);
                PlateType = "Mountain";
                AkSoundEngine.PostEvent(AKEventButton, gameObject);
            }

            if (inputPlate.GetComponent<PlateData>().PlateType == "Ocean")
            {
                Debug.Log("Become Volcanoe");
                PlateType = "Volcanoe";
            }
        }

        if (PlateType == "Ocean")
        {
            if (inputPlate.GetComponent<PlateData>().PlateType == "Land")
            {
                Debug.Log("Become Land and fill cell behind");
                PlateType = "Land";
            }

            if (inputPlate.GetComponent<PlateData>().PlateType == "Ocean")
            {
                Debug.Log("Become Oceanic Rift");
                PlateType = "Deep";
            }
        }

        if (PlateType == "Island")
        {
            if (inputPlate.GetComponent<PlateData>().PlateType == "Land")
            {
                Debug.Log("Become Major Volcanic landmass");
                PlateType = "Volcanoe";
            }

            if (inputPlate.GetComponent<PlateData>().PlateType == "Ocean")
            {
                Debug.Log("Become Oceanic floor");
                PlateType = "Ocean";
            }
        }
    }

    public void PlateGraze(GameObject inputPlate)
    {
        if (PlateType == "Land")
        {
            if (inputPlate.GetComponent<PlateData>().PlateType == "Land")
            {
                Debug.Log("Trigger Earthquake");
                Earthquake(1);
            }

            if (inputPlate.GetComponent<PlateData>().PlateType == "Ocean")
            {
                Debug.Log("Trigger Small Earthquake");
                Earthquake(0);
            }
        }

        if (PlateType == "Ocean")
        {
            if (inputPlate.GetComponent<PlateData>().PlateType == "Land")
            {
                Debug.Log("Trigger Tsunami");
                Earthquake(1);
            }

            if (inputPlate.GetComponent<PlateData>().PlateType == "Ocean")
            {
                Debug.Log("Trigger Small Tsunami");
                Earthquake(0);
            }
        }

        if (PlateType == "Island")
        {
            if (inputPlate.GetComponent<PlateData>().PlateType == "Land")
            {
                Debug.Log("Trigger Medium Tsunami");
                Earthquake(1);
            }

            if (inputPlate.GetComponent<PlateData>().PlateType == "Ocean")
            {
                Debug.Log("Trigger Small Tsunami");
                Earthquake(0);
            }
        }
    }

    public void PlateDiverge(GameObject inputPlate)
    {
        if (PlateType == "Land")
        {
            if (inputPlate.GetComponent<PlateData>().PlateType == "Land")
            {
                Debug.Log("Become Rift");
                transform.localScale += new Vector3(0, -0.5f, 0);
                PlateType = "Rift";
            }

            if (inputPlate.GetComponent<PlateData>().PlateType == "Ocean")
            {
                Debug.Log("Become Coastal island chain");
                PlateType = "Island";
            }
        }

        if (PlateType == "Ocean")
        {
            if (inputPlate.GetComponent<PlateData>().PlateType == "Land")
            {
                Debug.Log("Do basically nothing");
            }

            if (inputPlate.GetComponent<PlateData>().PlateType == "Ocean")
            {
                Debug.Log("Become mid Ocean Ridge");
                PlateType = "Island";
            }
        }

        if (PlateType == "Island")
        {
            if (inputPlate.GetComponent<PlateData>().PlateType == "Land")
            {
                Debug.Log("Exagerate the volcanic activity of the islands");
                PlateType = "Volcanoe";
            }

            if (inputPlate.GetComponent<PlateData>().PlateType == "Ocean")
            {
                Debug.Log("Trigger Small Tsunami");
            }
        }
    }

    public void Earthquake(int Power)
    {
        if (Power != 0)
        {
            
        }
    }
}