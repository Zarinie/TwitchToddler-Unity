using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json;
using System;

public class CameraHandler : MonoBehaviour
{

    public Camera camera;
    List<PixelInfo> ratioPixelList;
    public GameObject sphere;
    public GameObject sphereColour;

    public List<PixelInfo> SetPixelInfo(Camera camera)
    {
        PixelInfo cameraRes = new PixelInfo(camera.pixelWidth,camera.pixelHeight);
        string file = System.IO.File.ReadAllText(@"C:\FinalYearProject\Python\TwitchToddler-Python\Python\Output\json\coords0.json");

        List<PixelInfo> pixelList = JsonConvert.DeserializeObject<List<PixelInfo>>(file);
        // The first component of the json is the image resolution, which is then discarded
        PixelInfo imageRes = pixelList[0];
        pixelList.RemoveAt(0);

        // Image resolution must be compared to camera resolution so there are no descrepencies
        decimal ratioX = Math.Round((Decimal)imageRes.x / cameraRes.x, 2);
        decimal ratioY = Math.Round((Decimal)imageRes.y / cameraRes.y, 2);
        ratioPixelList = new List<PixelInfo>();
        int index = 0;

        //Math.Round((Decimal)cameraRes.x / imageRes.x, 2);

        // This is then applied to the co-ordinates
        foreach (PixelInfo pixelPair in pixelList)
        {
            PixelInfo temp = new PixelInfo
                ((int)(pixelPair.x * ratioX), (int)(pixelPair.y * ratioY));

            ratioPixelList.Add(temp);
            index++;
        }
        return ratioPixelList;
    }
}
