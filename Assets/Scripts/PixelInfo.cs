using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using System.Text.Json;
using System;
using Newtonsoft;
using Newtonsoft.Json;
//using com.unity.nuget.newtonsoft-json

public class PixelInfo
{

    public int x { get; set; }
    public int y { get; set; }

    public PixelInfo(int xCoord, int yCoord)
    {
        x = xCoord;
        y = yCoord;
    }

}
