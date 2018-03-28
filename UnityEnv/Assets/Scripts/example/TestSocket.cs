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
        if (GUI.Button(new Rect(20, 60, 100, 60), "Person-proto"))
        {
            People p = new People();
            p.name = "hugx";
            p.id = 12345;
            p.email = "penghuailiang@126.com";
            p.snip.Add(2);
            XNetworkMgr.sington.Send(new PeopleMsg(p));
        }
        if (GUI.Button(new Rect(20, 160, 100, 60), "Student"))
        {
            Student s = new Student();
            s.age = 12;
            s.name = "hug";
            s.num = 1002;
            XNetworkMgr.sington.Send(new StudentMgr(s));
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
