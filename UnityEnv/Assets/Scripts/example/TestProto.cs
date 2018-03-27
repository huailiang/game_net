using System;
using System.IO;
using XNet;
using UnityEngine;

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
		People proto =new People();
		proto.email = "rectvv@gmail.com";
	    proto.id = 10086;
	    proto.name = "Rect";
        proto.snip.Add(2);
        proto.snip.Add(4);
	    // 序列化
	    using (MemoryStream ms = new MemoryStream())
	    {
	        new PBMessageSerializer().Serialize(ms, proto);
	        Byte[] pBuffer = ms.ToArray();
	        File.WriteAllBytes(basePath+"/data/data.bytes",pBuffer);
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
	        People proto =  new PBMessageSerializer().Deserialize(ms, null, typeof(People)) as People;
	        Debug.Log(proto.email+" snip cnt: "+proto.snip.Count);
	    }
	}
}
