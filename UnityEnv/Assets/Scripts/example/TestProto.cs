using System;
using System.IO;
using XNet;
using UnityEngine;
using Google.Protobuf;

public class TestProto : MonoBehaviour
{
    private string basePath;
    private CodedInputStream iStream;

    void Start()
    {
        basePath = Path.GetDirectoryName(Application.dataPath);
        Debug.Log("Start: " + basePath);
    }
    
    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 100, 60), "Save"))
        {
            WritePeopleBuf();
            //WriteTestBuf();
        }
        if (GUI.Button(new Rect(20, 140, 100, 60), "Load"))
        {
            Byte[] bytes = File.ReadAllBytes(basePath + "/data/people.bytes");
            ReadProtoBuf(bytes, bytes.Length);
        }
        if (GUI.Button(new Rect(20, 260, 100, 60), "GC"))
        {
                var bytes = File.ReadAllBytes(basePath + "/data/test.bytes");
                iStream = new CodedInputStream(bytes);
                //gcBuff = ByteString.CopyFrom(bytes);
        }
    }

    private void Update()
    {
        if (iStream != null)
        {
           Test.Parser.ParseFrom(iStream);
        }
    }

    
    private void WriteTestBuf()
    {
        Test proto = new Test();
        proto.Index = UnityEngine.Random.Range(100000, 2000000);
        proto.Age = UnityEngine.Random.Range(20, 100);
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

    public void ReadProtoBuf(Byte[] pBuffer, int nSize)
    {
        if (null == pBuffer || 0 >= nSize)
        {
            return;
        }
        // 反序列化
        using (MemoryStream ms = new MemoryStream(pBuffer, 0, nSize))
        {
            People proto = People.Parser.ParseFrom(pBuffer);
            Debug.Log(proto.Email + " snip cnt: " + proto.Snip.Count);
        }
    }

}
