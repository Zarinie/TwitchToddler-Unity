using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft;
using Newtonsoft.Json;

public class CameraHandler : MonoBehaviour
{

    public Camera camera;
    List<PixelInfo> ratioPixelList;
    //List<Vector3> vectorList = new List<Vector3>();
    public GameObject sphere;
    public GameObject sphereColour;
    //MeshHandler meshHandler = new MeshHandler();
    //RayTrace rayTrace = new RayTrace();

    // Start is called before the first frame update
    void Start()
    {
        //GameObject cameraObject = GameObject.Find("/[CameraRig]/Camera");
        //camera = cameraObject.GetComponent(typeof(Camera)) as Camera;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //int index = 0;
        //foreach (PixelInfo pixelPair in ratioPixelList)
        //{
        //    rayTrace.SendRay(index, ratioPixelList, ref vectorList);
        //    index++;
        //}
        ////SendRay(index);
        //if(vectorList.Count > 0)
        //    meshHandler.CreateShape(vectorList);
        //vectorList = new List<Vector3>();
    }

    public List<PixelInfo> SetPixelInfo(Camera camera)
    {
        PixelInfo cameraRes = new PixelInfo
        {
            x = camera.pixelWidth,
            y = camera.pixelHeight
        };
        string file = System.IO.File.ReadAllText(@"C:\FinalYearProject\Python\Output\json\coords0.json");
        Debug.Log(file);

        List<PixelInfo> pixelList = JsonConvert.DeserializeObject<List<PixelInfo>>(file);
        PixelInfo imageRes = pixelList[0];
        pixelList.RemoveAt(0);

        double ratioX = cameraRes.x / imageRes.x;
        double ratioY = cameraRes.y / imageRes.y;
        ratioPixelList = new List<PixelInfo>();
        int index = 0;

        foreach (PixelInfo pixelPair in pixelList)
        {
            PixelInfo temp = new PixelInfo
            {
                x = (int)(pixelPair.x * ratioX),
                y = (int)(pixelPair.y * ratioY)
            };
            ratioPixelList.Add(temp);
            index++;
        }
        return ratioPixelList;
    }
}
