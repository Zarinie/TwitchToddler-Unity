using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayTrace : MonoBehaviour
{
    public Camera cameraMain;
    public Ray ray;
    RaycastHit hit;
    public void SendRay(Camera cameraMain, List<PixelInfo> ratioPixelList, ref List<Vector3> vectorList)
    {
        foreach (PixelInfo pixelPair in ratioPixelList)
        {
            ray = cameraMain.ScreenPointToRay(new Vector3(pixelPair.x, pixelPair.y, 0));
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
            
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Plane")
                {
                    //sphereColour.transform.position = ray.GetPoint(Hit3D.distance);               
                    //transform.position.Set(Hit3D.point.x, 0.0F, Hit3D.point.z);
                    Vector3 vector = new Vector3(hit.point.x, 0.0F, hit.point.z);
                    //sphere.transform.position = vector;

                    vectorList.Add(vector);
                    Debug.Log(vector.x);
                }
            }
        }
        //Ray ray = Camera.ScreenPointToRay(new Vector3(ratioPixelList[index].x, ratioPixelList[index].y, 0));
        
    }


}
