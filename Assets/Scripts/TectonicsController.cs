using Unity.Mathematics;
using UnityEngine;

public class TechtonicsController : MonoBehaviour
{
    public GameObject[,] TilePlate;
    public GameObject[,] TileVert;
    public GameObject[,] TileEdgeX;
    public GameObject[,] TileEdgeY;

    public GameObject Selected;

    public void InitialisePlates(GameObject[,] TilePlateIn, GameObject[,] TileVertIn, GameObject[,] TileEdgeXIn, GameObject[,] TileEdgeYIn)
    {
        TilePlate = TilePlateIn;
        TileVert = TileVertIn;
        TileEdgeX = TileEdgeXIn;
        TileEdgeX = TileEdgeYIn;
    }

    public void DetermineCollide(Vector3 vector3)
    {
        if (Selected.GetComponent<PlateData>().PlateType == "Land")
        {
            if (math.abs(vector3.x) < 0)
            {
                if (vector3.x < 0)
                {
                    //RUp
                }
                else
                {
                    //LDown
                }
            }
            else
            {
                if (math.abs(vector3.y) < 0)
                {
                    //LUp
                }
                else
                {
                    //RDown
                }
            }
        }        
    }

    public void LtLcollide()
    {

    }

    public void LtLDrift()
    {

    }

    public void LtLSeperate()
    {

    }

    public void OtLcollide()
    {

    }

    public void OtLDrift()
    {

    }

    public void OtLSeperate()
    {

    }
    
    public void OtOcollide()
    {

    }

    public void OtODrift()
    {

    }

    public void OtOSeperate()
    {

    }
}