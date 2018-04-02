using UnityEngine;
using XNet;
using System.Collections;

public class TestSocket : MonoBehaviour
{
    void Start()
    {
        IEnumerator itor;
        XNetworkMgr.sington.Init(out itor);
        StartCoroutine(itor);
    }
    


    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 60, 100, 60), "Person-proto"))
        {
            People p = new People();
            p.Name = "hugx";
            p.Id = 12345;
            p.Email = "penghuailiang@126.com";
            p.Snip.Add(2);
            XNetworkMgr.sington.Send(p);
        }
        if (GUI.Button(new Rect(20, 160, 100, 60), "Student"))
        {
            Student s = new Student();
            s.Age = 12;
            s.Name = "hug";
            s.Num = 1002;
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
        p.Name = "hugx";
        p.Id = 12345;
        p.Email = "penghuailiang@126.com";
        p.Snip.Add(2);
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
