using System.IO;
using UnityEngine;
using XNet;
using System.Text;

public class TestSocket : MonoBehaviour
{

    TcpClientHandler handle;

    void Start()
    {
        XNetworkMgr.sington.Init();
    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 100, 60), "Hello World"))
        {
            if (handle != null)
            {
                byte[] bytes = Encoding.ASCII.GetBytes("hello world!");
                XNetworkMgr.sington.Send(bytes);
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
                XNetworkMgr.sington.Send(pBuffer);
            }
        }
        if (GUI.Button(new Rect(20, 260, 100, 60), "Person-proto"))
        {
            People p = new People();
            p.name = "hugx";
            p.id = 12345;
            p.email = "penghuailiang@126.com";
            p.snip.Add(2);
            XNetworkMgr.sington.Send(new PeopleMsg(p));
        }
    }


    private void OnApplicationQuit()
    {
        if (handle != null)
        {
            handle.Quit();
        }
    }

}
