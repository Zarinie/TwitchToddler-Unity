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
        // Set up
        GameObject cameraObject = GameObject.Find("/[CameraRig]/Camera");
        camera = cameraObject.GetComponent(typeof(Camera)) as Camera;
        meshFound = false;
    }

    private void Update()
    {
        // On every tick this is run
        checkRay();
    }

    // Another way to run the code with a timer, was not functional but could still be useful
    public void runScreenshotTimer()
    {
        var startTime = System.TimeSpan.Zero;
        var periodTime = System.TimeSpan.FromSeconds(30);
        var timer = new System.Threading.Timer((e) =>
        {
            getScreenshots.RequestScreenshots(1);
            ratioedPixelList = cameraHandler.SetPixelInfo(camera);
            rayTrace.SendRay(camera, ratioedPixelList, ref vectorList);
            if (vectorList.Count > 0)
            {
                meshHandler.CreateShape(vectorList);
            }
        }, null, startTime, periodTime);
    }

    // Version without the timer element
    public void noTimer()
    {
        // Get the pixel co-ordinates after having been ratioed to the camera view in unity
        ratioedPixelList = cameraHandler.SetPixelInfo(camera); 

        //ratioedPixelList = new List<PixelInfo>{new PixelInfo(0, 0), new PixelInfo(0, 1440), new PixelInfo(2560, 1440), new PixelInfo(2560, 0)};
        //Run raytracing using the pixel list
        rayTrace.SendRay(camera, ratioedPixelList, ref vectorList);
        
        // To make a satisfactory shape, need at least 4 vectors and restict to less than the total pixel co-ordinates
        if (vectorList.Count > 3 && vectorList.Count <= ratioedPixelList.Count)
        {
            meshHandler.CreateShape(vectorList);
            meshFound = true;
            Debug.Log(vectorList);
        }   
    }

    //Quick check to make sure the code needs called, only if the main camera is looking at the plane
    public void checkRay()
    {
        ray = camera.ScreenPointToRay(new Vector3(0, 0, 0));
        Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);

        // Requires an impact with the plane to run
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Plane")
            {
                noTimer();
            }
        }
        vectorList = new List<Vector3>();
    }

    // Test code
    public void createMesh()
    {
        List<Vector3> temp = new List<Vector3> { new Vector3(1.3F, 0F, 1.1F), new Vector3(1.2F, 0F, 1.2F), new Vector3(1.1F, 0F, 1.3F) };
        meshHandler.CreateShape(temp);
    }
}
