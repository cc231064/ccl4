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
    [SerializeField] GameObject PlateIsland;
    private DialogueManager dialogue;

    private GameObject PlateModel;

    public LayerMask flameableLayer;

    private string AKMountain = "Mountain";
    private string AKVolcano = "Volcano";
    private string AKOcean = "Ocean";
    //private string AKTree = "Tree";
    //private string AKTreeBurning = "Treeburning";
    //private string AKWaves = "Waves";

    void Start()
    {
        gameObject.AddComponent<AkGameObj>();
        SnappedCoord = transform.position;
        techtonicsController = GetComponent<TechtonicsController>();
        dialogue = FindAnyObjectByType<DialogueManager>();

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
            //AkSoundEngine.PostEvent(AKTreeBurning, gameObject);
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
            dialogue.ShowDialogue("Rift", "When less dense Plates collide they buckle[br]and form Mountains!");
            RemoveForest();
        }

        if (PlateType == "Ocean")
        {
            PlateModel = Instantiate(PlateOcean, transform.position, Quaternion.LookRotation(new Vector3(0, 0, 1)));
            PlateModel.transform.SetParent(transform);
            RemoveForest();
            //AkSoundEngine.PostEvent(AKWaves, gameObject);
        }


        if (PlateType == "Volcanoe")
        {
            PlateModel = Instantiate(PlateVolcanoe, transform.position, Quaternion.LookRotation(new Vector3(0, 0, 1)));
            PlateModel.transform.SetParent(transform);

            Collider[] hits = Physics.OverlapSphere(transform.position, 2, flameableLayer); Debug.Log(hits.Length);

            foreach (Collider col in hits)
            {
                Debug.Log(col);
                col.gameObject.GetComponent<AnimationLib>().BurnDowwn();
            }

            RemoveForest();
        }

        if (PlateType == "Land")
        {
            PlateModel = Instantiate(PlateLand, transform.position, Quaternion.LookRotation(new Vector3(0, 0, 1)));
            PlateModel.transform.SetParent(transform);
            RemoveForest();
            GenerateForest();
        
        }

        if (PlateType == "Island")
        {
            PlateModel = Instantiate(PlateIsland, transform.position, Quaternion.LookRotation(new Vector3(0, 0, 1)));
            PlateModel.transform.SetParent(transform);
            RemoveForest();
        }

        if (PlateType == "Deep")
        {
            PlateModel = Instantiate(PlateRift, transform.position + new Vector3(0,-0.15f, 0), Quaternion.LookRotation(new Vector3(0, 0, 1)));
            PlateModel.transform.SetParent(transform);
            RemoveForest();
        }
    }

    public void PlateConverge(GameObject inputPlate)
    {
        dialogue.ShowDialogue("Converge", "When Plates move together we call that Converging");

        if (PlateType == "Land")
        {
            if (inputPlate.GetComponent<PlateData>().PlateType == "Land")
            {
                Debug.Log("Become Mountain");
                dialogue.ShowDialogue("Mountain", "When less dense Plates collide they buckle[br]and form Mountains!");
                PlateType = "Mountain";
                AkSoundEngine.PostEvent(AKMountain, gameObject);
            }

            if (inputPlate.GetComponent<PlateData>().PlateType == "Ocean")
            {
                Debug.Log("Become Volcanoe");
                dialogue.ShowDialogue("SubductionVolcanoe", "When a more dense Plate,[br]-like the Ocean-[br]collides with a less dense Plate,[br]-like land-[br]the dense one goes under the lighter one and melts, bubbling up as a Volcanoe!");
                RemoveForest();
                PlateType = "Volcanoe";
                AkSoundEngine.PostEvent(AKVolcano, gameObject);
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
                dialogue.ShowDialogue("Deep", "When Oceanic Plates converge they both bend down, forming Deep Ocean Trenches");
                PlateType = "Deep";
            }
        }

        if (PlateType == "Island")
        {
            if (inputPlate.GetComponent<PlateData>().PlateType == "Land")
            {
                Debug.Log("Become Major Volcanic landmass");
                dialogue.ShowDialogue("SubductionVolcanoe", "When a more dense Plate,[br]-like the Ocean-[br]collides with a less dense Plate,[br]-like land-[br]the dense one goes under the lighter one and melts, bubbling up as a Volcanoe!");
                RemoveForest();
                PlateType = "Volcanoe";
            }

            if (inputPlate.GetComponent<PlateData>().PlateType == "Ocean")
            {
                Debug.Log("Become Oceanic floor");
                PlateType = "Ocean";
                AkSoundEngine.PostEvent(AKOcean, gameObject);
            }
        }

        UpdateLandType();
    }

    public void PlateGraze(GameObject inputPlate)
    {
        dialogue.ShowDialogue("Earthquake", "When Plates move past eachother they can get caught, and suddenly...[br]Release![br]this is an Earthquake.");
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
                dialogue.ShowDialogue("Rift", "When Plates Move apart they form rifts, usually they fill with rock,[br]but they can have lave sometimes.");
                PlateType = "Rift";
            }

            if (inputPlate.GetComponent<PlateData>().PlateType == "Ocean")
            {
                Debug.Log("Become Coastal island chain");
                dialogue.ShowDialogue("DivergeChain", "When Plates part in the Ocean, the Mantle,[br]-Magma Under the surface-[br]Comes up and makes islands");
                PlateType = "Island";
                AkSoundEngine.PostEvent(AKOcean, gameObject);
            }
        }

        if (PlateType == "Ocean")
        {
            if (inputPlate.GetComponent<PlateData>().PlateType == "Land")
            {
                Debug.Log("Do basically nothing");
                AkSoundEngine.PostEvent(AKOcean, gameObject);
            }

            if (inputPlate.GetComponent<PlateData>().PlateType == "Ocean")
            {
                Debug.Log("Become mid Ocean Ridge");
                dialogue.ShowDialogue("DivergeChain", "When Plates part in the Ocean, the Mantle,[br]-Magma Under the surface-[br]Comes up and makes islands");
                PlateType = "Island";
                AkSoundEngine.PostEvent(AKOcean, gameObject);
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