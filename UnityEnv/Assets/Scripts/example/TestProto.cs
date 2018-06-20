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
            Student proto = Student.Parser.ParseFrom(sharedStream);
            Debug.Log("name: " + proto.Name + " age: " + proto.Age);
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
            Student.Parser.ParseFrom(sharedStream);
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
        Student proto = new Student();
        proto.Name = "";
        proto.Age = 64;
        using (MemoryStream ms = new MemoryStream())
        {
            proto.WriteTo(ms);
            Byte[] pBuffer = ms.ToArray();
            Util.PrintBytes(pBuffer);
            File.WriteAllBytes(basePath + "/data/student.bytes", pBuffer);
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
