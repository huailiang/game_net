using UnityEngine;

public class TestSocket : MonoBehaviour
{

    TcpClientHandler handle;

    // Use this for initialization
    void Start()
    {
        handle = new TcpClientHandler();
        handle.InitSocket();

    }

    private void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 100, 60), "Send"))
        {
            if(handle!=null)
            {
                handle.SocketSend("hell world!");
            }
        }
        if(GUI.Button(new Rect(20,140,100,60),"Test"))
        {
            handle.SocketSend("OK, Sync!");
        }
    }


    private void OnApplicationQuit()
    {
        if(handle!=null)
        {
            handle.SocketQuit();
        }
    }


}
