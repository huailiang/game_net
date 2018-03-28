using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace XNet
{
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

        public void Init()
        {
            handle = new TcpClientHandler();
            handle.InitSocket();
            id_mp = new Dictionary<ushort, Proto>();
            ty_mp = new Dictionary<int, Proto>();

            Regist();
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
        }

        public void Send(object obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                int hash = obj.GetType().GetHashCode();
                new PBMessageSerializer().Serialize(ms, obj);
                byte[] pBuffer = ms.ToArray();
                byte[] pId = BitConverter.GetBytes(ty_mp[hash].id);
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
            handle.Quit();
        }

        public void OnProcess(ushort uid,byte[] pb,int size)
        {
            if(id_mp.ContainsKey(uid))
            {
                id_mp[uid].OnProcess(pb, size);
            }
            else
            {
                Debug.LogError("proto not regist uid: " + uid);
            }
        }
        
    }
}
