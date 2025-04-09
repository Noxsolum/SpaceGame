using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEditor;

public class DebuggingScript : MonoBehaviour
{
    public StreamWriter writer;
    private string path;

    private void Start()
    {
        //OpenFile();
    }

    void OpenFile()
    {
        path = "Assets/Resources/DebugOutput.txt";
        writer = new StreamWriter(path, true);
        //Log("File Started");
    }

    //public void Log(object output)
    //{
    //    writer.WriteLine(output);
    //    Debug.Log(output);
    //}

    //private void OnApplicationQuit()
    //{
    //    writer.Close();
    //}

    #region Basic Functions
    /// <summary>
    /// 
    /// </summary>

    static void WriteString()
    {
        string path = "Assets/Resources/DebugOutput.txt";

        StreamWriter writer = new StreamWriter(path, true);
        writer.WriteLine("Test");
        writer.WriteLine("Oh boi, here's another test");
        writer.Close();

        //AssetDatabase.Refresh();
        //AssetDatabase.ImportAsset(path);
        TextAsset asset = Resources.Load<TextAsset>("DebugOutput");

        Debug.Log(asset.text);

    }

    static void ReadString()
    {
        string path = "Assets/Resources/test.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        Debug.Log(reader.ReadToEnd());
        reader.Close();
    }

    #endregion
}
