using System;
using System.IO;
using UnityEngine;

public static class CSVWriter
{
    private static StreamWriter writer;

    private static string Filepath
    {
        get
        {
            string fileName = DateTime.Now.ToString("yyyyMMddTHHmmss") + ".csv";
#if UNITY_EDITOR
            return Application.dataPath + "/Data/" + fileName;
#elif UNITY_ANDROID
        return Application.persistentDataPath + fileName;
#elif UNITY_IPHONE
        return Application.persistentDataPath + "/" + fileName;
#else
        return Application.dataPath + "/" + fileName;
#endif
        }
    }

    public static void Write(float time, string macAddress, Vector3 position)
    {
        if (writer == null)
        {
            writer = new StreamWriter(Filepath);
            writer.WriteLine("elapsed time,x-position,y-position,MAC address");
        }

        writer.WriteLine(time.ToString() + "," + position.x + "," + position.z + "," + macAddress);
    }

    public static void Close()
    {
        writer?.Flush();
        writer?.Close();
    }
}
