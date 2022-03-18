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
    Ray ray;
    RaycastHit hit;
    bool meshFound;

    private void Start()
    {
        GameObject cameraObject = GameObject.Find("/[CameraRig]/Camera");
        camera = cameraObject.GetComponent(typeof(Camera)) as Camera;
        meshFound = false;
        //createMesh();
        //noTimer();
    }

    private void Update()
    {
        //if(!meshFound)
            checkRay();
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
        //vectorList = new List<Vector3>();
        
        if (vectorList.Count > 3 && vectorList.Count <= ratioedPixelList.Count)
        {
            meshHandler.CreateShape(vectorList);
            meshFound = true;
        }   
    }

    public void checkRay()
    {
            ray = camera.ScreenPointToRay(new Vector3(0, 0, 0));
            Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Plane")
                {
                    noTimer();
                }
            }
        vectorList = new List<Vector3>();
    }

    public void createMesh()
    {
        List<Vector3> temp = new List<Vector3> { new Vector3(1.3F, 0F, 1.1F), new Vector3(1.2F, 0F, 1.2F), new Vector3(1.1F, 0F, 1.3F) };
        meshHandler.CreateShape(temp);
    }
}
