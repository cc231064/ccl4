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
        TileEdgeY = TileEdgeYIn;
    }

    public GameObject? NorthOfSelected;
    public GameObject? SouthOfSelected;
    public GameObject? EastOfSelected;
    public GameObject? WestOfSelected;

    public void DetermineCollide(Vector3 vector3)
    {
        EastOfSelected = TileEdgeX[(int)Selected.GetComponent<PlateData>().SnappedCoord.x, (int)Selected.GetComponent<PlateData>().SnappedCoord.z];
        if ((int)Selected.GetComponent<PlateData>().SnappedCoord.x > 0) WestOfSelected = TileEdgeX[(int)Selected.GetComponent<PlateData>().SnappedCoord.x - 1, (int)Selected.GetComponent<PlateData>().SnappedCoord.z];
        NorthOfSelected = TileEdgeY[(int)Selected.GetComponent<PlateData>().SnappedCoord.x, (int)Selected.GetComponent<PlateData>().SnappedCoord.z];
        if ((int)Selected.GetComponent<PlateData>().SnappedCoord.z > 0) SouthOfSelected = TileEdgeY[(int)Selected.GetComponent<PlateData>().SnappedCoord.x, (int)Selected.GetComponent<PlateData>().SnappedCoord.z - 1];

        if (vector3.x != 0)
        {
            if (vector3.x == 1)
            {
                //East
                if (EastOfSelected) EastOfSelected.GetComponent<PlateData>().PlateConverge(Selected); 
                if (WestOfSelected) WestOfSelected.GetComponent<PlateData>().PlateDiverge(Selected);
                if (NorthOfSelected) NorthOfSelected.GetComponent<PlateData>().PlateGraze(Selected);
                if (SouthOfSelected) SouthOfSelected.GetComponent<PlateData>().PlateGraze(Selected);
            }
            else
            {
                //West
                if (WestOfSelected) WestOfSelected.GetComponent<PlateData>().PlateConverge(Selected);
                if (EastOfSelected) EastOfSelected.GetComponent<PlateData>().PlateDiverge(Selected);
                if (NorthOfSelected) NorthOfSelected.GetComponent<PlateData>().PlateGraze(Selected);
                if (SouthOfSelected) SouthOfSelected.GetComponent<PlateData>().PlateGraze(Selected);
            }
        }
        else
        {
            if (vector3.z == 1)
            {
                //North
                if (NorthOfSelected) NorthOfSelected.GetComponent<PlateData>().PlateConverge(Selected);
                if (SouthOfSelected) SouthOfSelected.GetComponent<PlateData>().PlateDiverge(Selected);
                if (EastOfSelected) EastOfSelected.GetComponent<PlateData>().PlateGraze(Selected);
                if (WestOfSelected) WestOfSelected.GetComponent<PlateData>().PlateGraze(Selected);
            }
            else
            {
                //South
                if (SouthOfSelected) SouthOfSelected.GetComponent<PlateData>().PlateConverge(Selected);
                if (NorthOfSelected) NorthOfSelected.GetComponent<PlateData>().PlateDiverge(Selected);
                if (EastOfSelected) EastOfSelected.GetComponent<PlateData>().PlateGraze(Selected);
                if (WestOfSelected) WestOfSelected.GetComponent<PlateData>().PlateGraze(Selected);
            }
        }
    }
}