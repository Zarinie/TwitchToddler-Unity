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
    Text title;
    GameObject sphere;

    public void SendRay(Camera cameraMain, List<PixelInfo> ratioPixelList, ref List<Vector3> vectorList)
    {
        GameObject textGenerator = GameObject.Find("/Canvas/Text");
        title = textGenerator.GetComponent(typeof(Text)) as Text;
        sphere = GameObject.Find("/SphereColour");
        //title = textGenerator.GetComponent(typeof()) as Text;
        int index = 0;
        foreach (PixelInfo pixelPair in ratioPixelList)
        {
            ray = cameraMain.ScreenPointToRay(new Vector3(pixelPair.y, pixelPair.x, 0));
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
            
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Plane")
                {
                    //sphereColour.transform.position = ray.GetPoint(Hit3D.distance);               
                    //transform.position.Set(Hit3D.point.x, 0.0F, Hit3D.point.z);
                    Vector3 vector = new Vector3(RoundFloat(hit.point.x), RoundFloat(hit.point.y), RoundFloat(hit.point.z));
                    //sphere.transform.position = vector;
                    //createClone(vector);
                    //drawSphere(vector);
                    if (!vectorList.Contains(vector))
                    {
                        vectorList.Add(vector);
                        //Debug.Log(vector.x);
                        title.text = vector.ToString();
                    }
                }
            }
            //Debug.Log(index);
            index++;
        }
        //Ray ray = Camera.ScreenPointToRay(new Vector3(ratioPixelList[index].x, ratioPixelList[index].y, 0));
        
    }
    public float RoundFloat(float f)
    {
        return Mathf.Round(f * 10.0f) * 0.1f;
    }

    public void createClone(Vector3 pos)
    {
        GameObject clone = Instantiate(sphere, pos, Quaternion.identity);
    }

}
