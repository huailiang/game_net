using System;
using System.Collections.Generic;
using System.IO;
using Google.Protobuf;
using UnityEngine;

namespace XNet
{
    struct NetSerial
    {
        public float time;
        public ushort uid;
    }

    class XNetworkMgr : IDisposable
    {
        static TcpClientHandler handle;

        private static XNetworkMgr _s;

        public static XNetworkMgr sington
        {
            get { if (_s == null) { _s = new XNetworkMgr(); } return _s; }
        }

        private Dictionary<ushort, Proto> id_mp;
        private Dictionary<int, Proto> ty_mp;

        private float timeout = 2f;
        private Queue<NetSerial> queue = new Queue<NetSerial>();

        public void Init()
        {
            handle = new TcpClientHandler();
            handle.InitSocket();
            id_mp = new Dictionary<ushort, Proto>();
            ty_mp = new Dictionary<int, Proto>();

            Regist();
        }

        public void Update()
        {
            while (queue.Count > 0)
            {
                NetSerial ser = queue.Peek();
                if (Time.time - ser.time >= timeout)
                {
                    Proto proto = id_mp[ser.uid];
                    proto.OnTimeout();
                    queue.Dequeue();
                }
            }
        }

        public void Regist()
        {
            Regist(new PeopleMsg());
            Regist(new StudentMsg());

            //add others here..
        }


        private void Regist(Proto p)
        {
            int hash = p.GetProtoType().GetHashCode();
            ty_mp.Add(hash, p);
            id_mp.Add(p.id, p);
            Debug.Log("regist type: " + p.GetProtoType());
        }

        public void Send(IMessage msg)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Debug.Log("type: " +msg.GetType());
                int hash = msg.GetType().GetHashCode();
                ushort uid = ty_mp[hash].id;

                msg.WriteTo(ms);
                byte[] pBuffer = ms.ToArray();
                byte[] pId = BitConverter.GetBytes(uid);
                byte[] buff = new byte[pBuffer.Length + pId.Length];
                pId.CopyTo(buff, 0);
                pBuffer.CopyTo(buff, pId.Length);
                queue.Enqueue(new NetSerial() { time = Time.time, uid = uid });
                Send(buff);
            }
        }

        public void Send(byte[] buff)
        {
            handle.Send(buff);
        }


        public void Dispose()
        {
            if (handle != null)
            {
                handle.Quit();
            }
        }
        

        public void OnProcess(ushort uid, byte[] pb)
        {
            if (id_mp.ContainsKey(uid))
            {
                if (queue.Count > 0 && queue.Peek().uid == uid)
                {
                    id_mp[uid].OnProcess(pb);
                    queue.Dequeue();
                }
                else
                {
                    Debug.Log("may be proto timeout!");
                }
            }
            else
            {
                Debug.LogError("proto not regist uid: " + uid);
            }
        }
    }
}
