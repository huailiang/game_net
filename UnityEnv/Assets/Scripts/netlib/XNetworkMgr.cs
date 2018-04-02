using System;
using System.Collections;
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


        public void Init(out IEnumerator rcv)
        {
            handle = new TcpClientHandler();
            handle.InitSocket(out rcv);
            id_mp = new Dictionary<ushort, Proto>();
            ty_mp = new Dictionary<int, Proto>();

            Regist();
        }

        public void Update()
        {
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
        
        /// <summary>
        /// 这里是子线程调用 不能在方法里处理Unity API
        /// </summary>
        /// <param name="uid">消息id</param>
        /// <param name="pb">网络buff</param>
        public void OnProcess(ushort uid, byte[] pb)
        {
            Debug.Log("rcv pb with uid: " + uid);
            if (id_mp.ContainsKey(uid))
            {
                id_mp[uid].OnProcess(pb);
            }
            else
            {
                Debug.LogError("proto not regist uid: " + uid);
            }
        }
    }
}
