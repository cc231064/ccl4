using UnityEngine;

public class PlateData : MonoBehaviour
{
    public string PlateType;
    public Vector3 SnapDistance = new Vector3(1, 1, 1);
    public Vector3 SnapOffset;
    public Vector3 SnappedCoord;
    public TechtonicsController techtonicsController;
    [SerializeField] public string Territory; 
    private GameObject[] Foliage;
    [SerializeField] GameObject? Tree;

    [SerializeField] GameObject PlateLand;
    [SerializeField] GameObject PlateOcean;
    [SerializeField] GameObject PlateMountain;
    [SerializeField] GameObject PlateRift;
    [SerializeField] GameObject PlateVolcanoe;

    private GameObject PlateModel;
    private string AkMountain = "Mountain";
    private string AkVolcano = "Volcano";
    private string AkOcean = "Ocean";

    void Start()
    {
        gameObject.AddComponent<AkGameObj>();
        SnappedCoord = transform.position;
        techtonicsController = GetComponent<TechtonicsController>();

        UpdateSnap();
        GenerateForest();

        UpdateLandType();
    }

    void UpdateSnap()
    {
        transform.position = SnapOffset + new Vector3(SnappedCoord.x * SnapDistance.x, SnappedCoord.y * SnapDistance.y, SnappedCoord.z * SnapDistance.z);
    }

    void RemoveForest()
    {
        for (int i = 0; i < Foliage.Length; i++)
        {
            Destroy(Foliage[i]);
        }
    }

    void GenerateForest()
    {
        Foliage = new GameObject[100];
        for (int i = 0; i < Foliage.Length; i++)
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

    void UpdateLandType()
    {
        Destroy(PlateModel);
        if (PlateType == "Mountain")
        {
            PlateModel = Instantiate(PlateMountain, transform.position, Quaternion.LookRotation(new Vector3(0, 0, 1)));
            PlateModel.transform.SetParent(transform);
            RemoveForest();
            //GenerateForest();
        }

        if (PlateType == "Rift")
        {
            PlateModel = Instantiate(PlateRift, transform.position, Quaternion.LookRotation(new Vector3(0, 0, 1)));
            PlateModel.transform.SetParent(transform);
            RemoveForest();
            //GenerateForest();
        }

        if (PlateType == "Ocean")
        {
            PlateModel = Instantiate(PlateOcean, transform.position, Quaternion.LookRotation(new Vector3(0, 0, 1)));
            PlateModel.transform.SetParent(transform);
            RemoveForest();
            //GenerateForest();
        }

        if (PlateType == "Volcanoe")
        {
            PlateModel = Instantiate(PlateVolcanoe, transform.position, Quaternion.LookRotation(new Vector3(0, 0, 1)));
            PlateModel.transform.SetParent(transform);
            RemoveForest();
            //GenerateForest();
        }
    }

    public void PlateConverge(GameObject inputPlate)
    {
        if (PlateType == "Land")
        {
            if (inputPlate.GetComponent<PlateData>().PlateType == "Land")
            {
                Debug.Log("Become Mountain");
                PlateType = "Mountain";
                AkSoundEngine.PostEvent(AkMountain, gameObject);
            }

            if (inputPlate.GetComponent<PlateData>().PlateType == "Ocean")
            {
                Debug.Log("Become Volcanoe");
                RemoveForest();
                PlateType = "Volcanoe";
                AkSoundEngine.PostEvent(AkVolcano, gameObject);
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
                RemoveForest();
                PlateType = "Volcanoe";
            }

            if (inputPlate.GetComponent<PlateData>().PlateType == "Ocean")
            {
                Debug.Log("Become Oceanic floor");
                PlateType = "Ocean";
                AkSoundEngine.PostEvent(AkOcean, gameObject);
            }
        }

        UpdateLandType();
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

        UpdateLandType();
    }

    public void PlateDiverge(GameObject inputPlate)
    {
        if (PlateType == "Land")
        {
            if (inputPlate.GetComponent<PlateData>().PlateType == "Land")
            {
                Debug.Log("Become Rift");
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
                RemoveForest();
                PlateType = "Volcanoe";
            }

            if (inputPlate.GetComponent<PlateData>().PlateType == "Ocean")
            {
                Debug.Log("Trigger Small Tsunami");
            }
        }

        UpdateLandType();
    }

    public void Earthquake(int Power)
    {
        if (Power != 0)
        {
            
        }
    }
}