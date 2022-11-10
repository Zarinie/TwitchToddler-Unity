using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using 

public class RayTrace : MonoBehaviour
{
    public Camera cameraMain;
    public Ray ray;
    RaycastHit hit;
    GameObject sphere;

    // Ray is sent and its point of impact with the plane is recorded
    public void SendRay(Camera cameraMain, List<PixelInfo> ratioPixelList, ref List<Vector3> vectorList)
    {
        int index = 0;
        foreach (PixelInfo pixelPair in ratioPixelList)
        {
            ray = cameraMain.ScreenPointToRay(new Vector3(pixelPair.x, pixelPair.y, 0));
            Debug.DrawRay(ray.origin, ray.direction *10, Color.green);
            
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Plane")
                {
                    // Rounding is required because otherwise the numbers become too complex to use in the mesh
                    Vector3 vector = new Vector3(RoundFloat(hit.point.x), RoundFloat(hit.point.y), RoundFloat(hit.point.z));
                    if (!vectorList.Contains(vector))
                    {
                        vectorList.Add(vector);
                    }
                }
            }
            index++;
        }
    }

    public float RoundFloat(float f)
    {
        return Mathf.Round(f * 10.0f) * 0.1f;
    }

    //test code
    public void createClone(Vector3 pos)
    {
        GameObject clone = Instantiate(sphere, pos, Quaternion.identity);
    }

}
