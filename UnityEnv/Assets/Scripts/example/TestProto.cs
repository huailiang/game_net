using System;
using System.IO;
using XNet;
using UnityEngine;
using Google.Protobuf;

public class TestProto : MonoBehaviour
{
    private string basePath;
    private CodedInputStream sharedInStream;
    private CodedOutputStream sharedOutStream;
    private bool parse = false;
    private bool write = false;
    private MemoryStream ms;
    private Student stu;

    void Start()
    {
        basePath = Path.GetDirectoryName(Application.dataPath);
        ms = new MemoryStream();
        sharedInStream = new CodedInputStream(ms);
        sharedOutStream = new CodedOutputStream(ms);
        Debug.Log("Start: " + basePath);
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 100, 60), "SavePeople"))
        {
            WritePeopleBuf();
        }
        if (GUI.Button(new Rect(140, 20, 100, 60), "SaveStudent"))
        {
            WriteStudentBuf();
        }
        if (GUI.Button(new Rect(20, 140, 100, 60), "LoadPeople"))
        {
            Byte[] bytes = File.ReadAllBytes(basePath + "/data/people.bytes");
            ms.SetLength(bytes.Length);
            ms.Write(bytes, 0, bytes.Length);
            ms.Seek(0, SeekOrigin.Begin);
            People proto = People.Parser.ParseFrom(ms);
            Debug.Log(proto.Email + " snip cnt: " + proto.Snip.Count);
            ms.Position = 0;
            proto = People.Parser.ParseFrom(ms);
            Debug.Log(proto.Email + " snip cnt: " + proto.Snip.Count);
            ms.Position = 0;
            sharedInStream.Set(ms);
            proto = People.Parser.ParseFrom(sharedInStream);
            Debug.Log(proto.Email + " snip cnt: " + proto.Snip.Count);
        }
        if (GUI.Button(new Rect(140, 140, 100, 60), "LoadStudent"))
        {
            Byte[] bytes = File.ReadAllBytes(basePath + "/data/student.bytes");
            ms.SetLength(bytes.Length);
            ms.Write(bytes, 0, bytes.Length);
            ms.Position = 0;
            sharedInStream.Set(ms);
            Student proto = Student.Parser.ParseFrom(sharedInStream);
            Debug.Log("num: " + proto.Num + " age: " + proto.Age);
        }
        if (GUI.Button(new Rect(20, 260, 100, 60), "GC-Parse"))
        {
            Byte[] bytes = File.ReadAllBytes(basePath + "/data/student.bytes");
            sharedInStream.Set(ms);
            ms.SetLength(bytes.Length);
            ms.Write(bytes, 0, bytes.Length);
            parse = true;
            write = false;
        }
        if (GUI.Button(new Rect(140, 260, 100, 60), "GC-Write"))
        {
            stu = new Student();
            stu.Age = 32;
            stu.Num = 1002;
            ms.SetLength(0);
            sharedOutStream.Set(ms);

            write = true;
            parse = false;
        }
    }

    private void Update()
    {
        if (parse)
        {
            ms.Position = 0;
            sharedInStream.Set(ms);
            Student.Parser.ParseFrom(sharedInStream);
        }
        if (write)
        {
            stu.WriteTo(sharedOutStream);
        }
    }


    private void WriteStudentBuf()
    {
        Student proto = new Student();
        proto.Age = 64;
        proto.Num = 1024;
        ms.SetLength(0);
        sharedOutStream.Set(ms);
        proto.WriteToSharedStream(sharedOutStream);
        Util.PrintBytes(ms.ToArray());
        File.WriteAllBytes(basePath + "/data/student.bytes", ms.ToArray());
    }

    private void WritePeopleBuf()
    {
        People proto = new People();
        proto.Email = "rectvv@gmail.com";
        proto.Id = 10086;
        proto.Name = "Rect";
        proto.Snip.Add(2);
        proto.Snip.Add(4);
        ms.SetLength(0);
        sharedOutStream.Set(ms);
        proto.WriteToSharedStream(sharedOutStream);
        Util.PrintBytes(ms.ToArray());
        File.WriteAllBytes(basePath + "/data/people.bytes", ms.ToArray());
    }
}
