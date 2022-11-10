using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Newtonsoft;
using Newtonsoft.Json;

public struct PixelInfo 
{

    public int x { get; set; }
    public int y { get; set; }

    public PixelInfo(int xCoord, int yCoord)
    {
        x = xCoord;
        y = yCoord;
    }
}