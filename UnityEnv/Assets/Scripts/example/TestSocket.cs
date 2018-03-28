using UnityEngine;
using XNet;

public class TestSocket : MonoBehaviour
{

    TcpClientHandler handle;

    void Start()
    {
        XNetworkMgr.sington.Init();
    }

    private void OnGUI()
    {
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
