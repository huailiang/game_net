using System;
using System.IO;
using XNet;
using UnityEngine;
using Google.Protobuf;

public class TestProto : MonoBehaviour
{
    private string basePath;
    private CodedInputStream sharedStream;
    private bool parse = false;

    void Start()
    {
        basePath = Path.GetDirectoryName(Application.dataPath);
        Debug.Log("Start: " + basePath);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 100, 60), "SavePeople"))
        {
            WritePeopleBuf();
        }
        if (GUI.Button(new Rect(140, 20, 100, 60), "SaveTest"))
        {
            WriteTestBuf();
        }
        if (GUI.Button(new Rect(20, 140, 100, 60), "LoadPeople"))
        {
            Byte[] bytes = File.ReadAllBytes(basePath + "/data/people.bytes");
            FillInputStream(bytes);
            People proto = People.Parser.ParseFrom(sharedStream);
            Debug.Log(proto.Email + " snip cnt: " + proto.Snip.Count);
        }
        if (GUI.Button(new Rect(140, 140, 100, 60), "LoadTest"))
        {
            Byte[] bytes = File.ReadAllBytes(basePath + "/data/test.bytes");
            FillInputStream(bytes);
            Test proto = Test.Parser.ParseFrom(sharedStream);
            Debug.Log("index: " + proto.Index + " age: " + proto.Age);
        }
        if (GUI.Button(new Rect(20, 260, 100, 60), "GC"))
        {
            var bytes = File.ReadAllBytes(basePath + "/data/test.bytes");
            FillInputStream(bytes);
            parse = true;
        }
    }

    private void Update()
    {
        if (parse)
        {
            Test.Parser.ParseFrom(sharedStream);
        }
    }

    private void FillInputStream(byte[] bytes)
    {
        if (sharedStream == null)
        {
            sharedStream = new CodedInputStream(bytes);
        }
        else
        {
            sharedStream.Set(bytes);
        }
    }


    private void WriteTestBuf()
    {
        Test proto = new Test();
        proto.Index = 1024;
        proto.Age = 64;
        using (MemoryStream ms = new MemoryStream())
        {
            proto.WriteTo(ms);
            Byte[] pBuffer = ms.ToArray();
            Util.PrintBytes(pBuffer);
            File.WriteAllBytes(basePath + "/data/test.bytes", pBuffer);
            ms.SetLength(0);
        }
    }

    private void WritePeopleBuf()
    {
        People proto = new People();
        proto.Email = "rectvv@gmail.com";
        proto.Id = 10086;
        proto.Name = "Rect";
        proto.Snip.Add(2);
        proto.Snip.Add(4);
        // 序列化
        using (MemoryStream ms = new MemoryStream())
        {
            proto.WriteTo(ms);
            Byte[] pBuffer = ms.ToArray();
            Util.PrintBytes(pBuffer);
            File.WriteAllBytes(basePath + "/data/people.bytes", pBuffer);
            ms.SetLength(0);
        }
    }

}
