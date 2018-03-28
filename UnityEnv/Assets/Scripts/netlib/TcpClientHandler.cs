using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using XNet;

public class TcpClientHandler
{
    Socket serverSocket;
    IPAddress ip;
    IPEndPoint ipEnd;
    string recvStr; //接收的字符串
    byte[] recvData = new byte[1024];
    byte[] sendData = new byte[1024];
    int recvLen; //接收的数据长度
    Thread connectThread;
    const ushort len_head = 2;
    const ushort max_buff = 1024;


    public void InitSocket()
    {
        //定义服务器的IP和端口，端口与服务器对应
        ip = IPAddress.Parse("127.0.0.1"); //可以是局域网或互联网ip，此处是本机
        ipEnd = new IPEndPoint(ip, 6000); //服务器端口号

        //开启一个线程连接，必须的，否则主线程卡死
        connectThread = new Thread(new ThreadStart(Receive));
        connectThread.Start();
    }

    void SocketConnet()
    {
        if (serverSocket != null) serverSocket.Close();
        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        serverSocket.Connect(ipEnd);
        recvLen = serverSocket.Receive(recvData);
    }

    public void Send(string sendStr)
    {
        //清空发送缓存
        sendData = new byte[1024];

        sendData = Encoding.ASCII.GetBytes(sendStr);
        serverSocket.Send(sendData, sendData.Length, SocketFlags.None);
    }

    public void Send(byte[] bytes)
    {
        serverSocket.Send(bytes, bytes.Length, SocketFlags.None);
    }

    private void Receive()
    {
        SocketConnet();

        while (true)
        {
            recvData = new byte[1024];
            recvLen = serverSocket.Receive(recvData);
            if (recvLen == 0)
            {
                SocketConnet();
            }

            try
            {
                recvStr = Encoding.ASCII.GetString(recvData, 0, recvLen);
                byte[] head = new byte[len_head];
                head[0] = recvData[0];
                head[1] = recvData[1];
                ushort uid = BitConverter.ToUInt16(head, 0);
                Debug.Log("uid: " + uid + " len: " + recvLen + " data: " + recvData.Length);
                byte[] buff = new byte[max_buff - len_head];
                Array.Copy(recvData, len_head, buff, 0, buff.Length);
                XNetworkMgr.sington.OnProcess(uid, buff, recvLen - len_head);
            }
            catch (Exception e)
            {
                Debug.Log("err:" + e.Message + " stack:" + e.StackTrace);
            }
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
        if (connectThread != null)
        {
            connectThread.Interrupt();
            connectThread.Abort();
        }
        if (serverSocket != null)
            serverSocket.Close();
        Debug.Log("diconnect");
    }

}