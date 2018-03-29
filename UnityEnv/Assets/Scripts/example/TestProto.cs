using System;
using System.IO;
using XNet;
using UnityEngine;
using Google.Protobuf;

public class TestProto : MonoBehaviour
{
    private string basePath;

	void Start()
	{
        basePath = Path.GetDirectoryName(Application.dataPath);
		Debug.Log("Start: "+basePath);
	}


    private void OnGUI()
    {
        if(GUI.Button(new Rect(20, 20, 100, 60), "Save"))
        {
            WriteProtoBuf();
        }
        if(GUI.Button(new Rect(20,140,100,60),"Load"))
        {
            Byte[] bytes = File.ReadAllBytes(basePath + "/data/data.bytes");
            ReadProtoBuf(bytes, bytes.Length);
        }
    }
    


	public void WriteProtoBuf()
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
            File.WriteAllBytes(basePath + "/data/data.bytes", pBuffer);
        }
    }

	public void ReadProtoBuf(Byte[] pBuffer,int nSize)
	{
        if (null == pBuffer || 0 >= nSize)
        {
            return;
        }
        // 反序列化
        using (MemoryStream ms = new MemoryStream(pBuffer, 0, nSize))
        {
            People proto = People.Parser.ParseFrom(pBuffer);// new PBMessageSerializer().Deserialize(ms, null, typeof(People)) as People;
            Debug.Log(proto.Email + " snip cnt: " + proto.Snip.Count);
        }
    }
}
