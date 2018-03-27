using System.IO;
using UnityEngine;
using XNet;

public class TestSocket : MonoBehaviour
{

    TcpClientHandler handle;

    void Start()
    {
        handle = new TcpClientHandler();
        handle.InitSocket();

    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 100, 60), "Hello World"))
        {
            if (handle != null)
            {
                handle.Send("hell world!");
            }
        }
        if (GUI.Button(new Rect(20, 140, 100, 60), "Person"))
        {
            People p = new People();
            p.name = "hug";
            p.id = 12345;
            p.email = "penghuailiang@126.com";
            p.snip.Add(2);
            using (MemoryStream ms = new MemoryStream())
            {
                new PBMessageSerializer().Serialize(ms, p);
                byte[] pBuffer = ms.ToArray();
                handle.Send(pBuffer);
            }
        }
    }


    private void OnApplicationQuit()
    {
        if (handle != null)
        {
            handle.SocketQuit();
        }
    }

}
