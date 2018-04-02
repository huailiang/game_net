using System;
using System.Net;
using System.Net.Sockets;
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
    int recvLen; //接收的数据长度

    public void InitSocket()
    {
        //定义服务器的IP和端口，端口与服务器对应
        ip = IPAddress.Parse(sock_ip); //可以是局域网或互联网ip，此处是本机
        ipEnd = new IPEndPoint(ip, sock_port); //服务器端口号
    }

    void Connect()
    {
        if (serverSocket != null) serverSocket.Close();
        serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        serverSocket.Connect(ipEnd);
    }

    void TryReceive()
    {
        Array.Clear(recvData, 0, max_buff);
        serverSocket.BeginReceive(recvData, 0, max_buff, SocketFlags.None, OnReceiveDataComplete, null);
    }

    
    private void OnReceiveDataComplete(IAsyncResult ar)
    {
        recvLen = serverSocket.EndReceive(ar);
        byte[] head = new byte[len_head];
        Array.Copy(recvData, 0, head, 0, len_head);
        ushort uid = BitConverter.ToUInt16(head, 0);
        byte[] buff = new byte[recvLen - len_head];
        Array.Copy(recvData, len_head, buff, 0, recvLen - len_head);
        XNetworkMgr.sington.OnProcess(uid, buff);
        if (serverSocket != null) serverSocket.Close();
        recvLen = 0;
    }

    public void Send(byte[] bytes)
    {
        Connect();
        serverSocket.Send(bytes, bytes.Length, SocketFlags.None);
        TryReceive();
    }

    
    public void Quit()
    {
        if (serverSocket != null)
        {
            serverSocket.Close();
        }

        Debug.Log("diconnect");
    }

}