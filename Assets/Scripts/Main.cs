using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Timers;

public class Main : MonoBehaviour
{
    //call check ray trace
    //request screenshots
    //process json
    //raytrace to vectors
    //create shape
    //make traversable

    Camera camera;
    CameraHandler cameraHandler = new CameraHandler();
    GetScreenshots getScreenshots = new GetScreenshots();
    MeshHandler meshHandler = new MeshHandler();
    RayTrace rayTrace = new RayTrace();
    List<PixelInfo> ratioedPixelList;
    List<Vector3> vectorList = new List<Vector3>();

    private void Start()
    {
        GameObject cameraObject = GameObject.Find("/[CameraRig]/Camera");
        camera = cameraObject.GetComponent(typeof(Camera)) as Camera;
        noTimer();
    }

    public void runScreenshotTimer()
    {
        var startTime = System.TimeSpan.Zero;
        var periodTime = System.TimeSpan.FromSeconds(30);
        //for (int i = 0; i < 100; i++)
        //{
            var timer = new System.Threading.Timer((e) =>
            {
                getScreenshots.RequestScreenshots(1);
                ratioedPixelList = cameraHandler.SetPixelInfo(camera);
                rayTrace.SendRay(camera, ratioedPixelList, ref vectorList);
                if (vectorList.Count > 0)
                    meshHandler.CreateShape(vectorList);

            }, null, startTime, periodTime);
        //}
        
    }

    public void noTimer()
    {
        //getScreenshots.RequestScreenshots(1);
        ratioedPixelList = cameraHandler.SetPixelInfo(camera);
        rayTrace.SendRay(camera, ratioedPixelList, ref vectorList);
        if (vectorList.Count > 0)
            meshHandler.CreateShape(vectorList);
    }
}
