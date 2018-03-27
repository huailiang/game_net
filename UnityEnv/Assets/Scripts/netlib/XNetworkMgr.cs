using System;
using System.IO;

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

        public void Init()
        {
            handle = new TcpClientHandler();
            handle.InitSocket();
        }
        
        ~XNetworkMgr()
        {
            Dispose();
        }

        public void Send(Proto proto)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                new PBMessageSerializer().Serialize(ms, proto.pb);
                byte[] pBuffer = ms.ToArray();

                byte[] pId = BitConverter.GetBytes(proto.id);
                byte[] buff = new byte[pBuffer.Length + pId.Length];
                pId.CopyTo(buff, 0);
                pBuffer.CopyTo(buff, pId.Length);
                Util.PrintBytes(pId);
                Send(buff);
            }
        }

        public void Send(byte[] buff)
        {
            Util.PrintBytes(buff);
            handle.Send(buff);
        }


        public void Dispose()
        {
            handle.Quit();
        }


    }
}
