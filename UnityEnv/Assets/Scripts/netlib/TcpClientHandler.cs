using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using XNet;

public class TcpClientHandler
{

    const ushort len_head = 2;
    const ushort max_buff = 1024;
    const string sock_ip = "127.0.0.1";
    const int sock_port = 6000;

    Socket serverSocket;
    IPAddress ip;
    IPEndPoint ipEnd;
    string recvStr; //接收的字符串
    byte[] recvData = new byte[max_buff];
    byte[] sendData = new byte[max_buff];
    int recvLen; //接收的数据长度
    Thread connectThread;


    public void InitSocket()
    {
        //定义服务器的IP和端口，端口与服务器对应
        ip = IPAddress.Parse(sock_ip); //可以是局域网或互联网ip，此处是本机
        ipEnd = new IPEndPoint(ip, sock_port); //服务器端口号

        //开启一个线程连接，必须的，否则主线程卡死
        connectThread = new Thread(new ThreadStart(Receive));
        connectThread.Start();
    }

    void SocketConnet()
    {
        if (serverSocket != null) serverSocket.Close();
        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        serverSocket.Connect(ipEnd);
        Array.Clear(recvData, 0, max_buff);
        recvLen = serverSocket.Receive(recvData);
    }

    public void Send(string sendStr)
    {
        //清空发送缓存
        sendData = new byte[max_buff];

        sendData = Encoding.ASCII.GetBytes(sendStr);
        serverSocket.Send(sendData, sendData.Length, SocketFlags.None);
    }

    public void Send(byte[] bytes)
    {
        serverSocket.Send(bytes, bytes.Length, SocketFlags.None);
    }

    private void Receive()
    {
        try
        {
            SocketConnet();

            while (true)
            {
                if (recvLen == 0)
                {
                    SocketConnet();
                }
                recvStr = Encoding.ASCII.GetString(recvData, 0, recvLen);
                byte[] head = new byte[len_head];
                Array.Copy(recvData, 0, head, 0, len_head);
                ushort uid = BitConverter.ToUInt16(head, 0);
                byte[] buff = new byte[recvLen - len_head];
                Array.Copy(recvData, len_head, buff, 0, recvLen-len_head);
                XNetworkMgr.sington.OnProcess(uid, buff);
                recvLen = 0;
            }
        }
        catch (Exception e)
        {
            Debug.Log("err:" + e.Message + " stack:" + e.StackTrace);
        }
    }

    public string GetRecvStr()
    {
        string returnStr;
        //加锁防止字符串被改
        lock (this)
        {
            returnStr = recvStr;
        }
        return returnStr;
    }

    public void Quit()
    {
        if (serverSocket != null)
        {
            //serverSocket.Disconnect(false);
            serverSocket.Close();
        }
        if (connectThread != null)
        {
            connectThread.Interrupt();
            connectThread.Abort();
        }
        
        Debug.Log("diconnect");
    }

}