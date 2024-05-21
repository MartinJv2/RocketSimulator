using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

[CreateAssetMenu(fileName = "BestScore", menuName = "ScriptableObjects/BestScore", order = 1)]
public class BestScore : ScriptableObject
{
    private float bestscore;
    public string filepath;
    public floatscriptableobject actualscore;

    public void OnEnable()
    {
        bestscore = readfile();
        Debug.Log(bestscore);
    }

    private float readfile()
    {
        try
        {
            StreamReader sr = new StreamReader(filepath);
            string text = sr.ReadLine();
            return float.Parse(text);
        }
        catch (Exception e)
        {
            Console.WriteLine("File not found. Default best score is 0");
            return 0f;
        }
    }
    public void registerscore(float value)
    {
        if (value > bestscore)
        {
            addline(value.ToString());
            bestscore = value;
        }
    }

    private void addline(string text)
    {
        try
        {
            StreamWriter sw = new StreamWriter(filepath, false, Encoding.ASCII);
            sw.Write(text);
            sw.Close();
        }
        catch (Exception e)
        {
            Console.WriteLine("file do not exit. Creating a file");
            createfile();
            addline(text);
        }
    }

    private void createfile()
    {
        using (FileStream fs = File.Create(filepath)) fs.Close();
    }
}
