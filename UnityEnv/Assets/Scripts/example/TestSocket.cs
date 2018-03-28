using UnityEngine;
using XNet;
using System.Collections;

public class TestSocket : MonoBehaviour
{


    void Start()
    {
        XNetworkMgr.sington.Init();
    }

    private void Update()
    {
      //  XNetworkMgr.sington.Update();
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
            XNetworkMgr.sington.Send(p);
        }
        if (GUI.Button(new Rect(20, 160, 100, 60), "Student"))
        {
            Student s = new Student();
            s.age = 12;
            s.name = "hug";
            s.num = 1002;
            XNetworkMgr.sington.Send(s);
        }
    }

    /// <summary>
    /// 测试连发
    /// </summary>
    /// <returns></returns>
    IEnumerator YieldPersonMsg()
    {
        People p = new People();
        p.name = "hugx";
        p.id = 12345;
        p.email = "penghuailiang@126.com";
        p.snip.Add(2);
        XNetworkMgr.sington.Send(p);
        yield return new WaitForSeconds(0.1f);
        XNetworkMgr.sington.Send(p);
        yield return new WaitForSeconds(0.1f);
        XNetworkMgr.sington.Send(p);
        yield return new WaitForSeconds(0.1f);
    }

    private void OnApplicationQuit()
    {
        XNetworkMgr.sington.Dispose();
    }

}
