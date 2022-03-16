using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.Windows;
using System.Threading.Tasks;

public class GetScreenshots : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //RequestScreenshots(10);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RequestScreenshots(int amount)
    {
        int ExitCode;
        ProcessStartInfo ProcessInfo;
        Process Process;
        string command = "\"cd C:/FinalYearProject/Python ; python pythonHandler.py " + amount.ToString() + "\"";
        //command = "-NoExit - Command " + command;

        ProcessInfo = new ProcessStartInfo("powershell.exe", "" + command);
        ProcessInfo.CreateNoWindow = true;
        //ProcessInfo.UseShellExecute = false;

        Process = Process.Start(ProcessInfo);
        Process.WaitForExit();

        ExitCode = Process.ExitCode;
        Process.Close();

        //MessageBox.Show("ExitCode: " + ExitCode.ToString(), "ExecuteCommand");
        System.Console.WriteLine("check Screenshots");
    }
}
