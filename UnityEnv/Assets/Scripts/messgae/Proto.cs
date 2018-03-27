using System;
using System.IO;

namespace XNet
{
    public abstract class Proto : IDisposable
    {
        public short id;
        

        public object pb;

        public abstract Type GetProtoType();

        public abstract void OnReply(object proto);

        public virtual void OnTimeout() { }

        public void OnRecv(byte[] pBuffer, int nSize)
        {
            using (MemoryStream ms = new MemoryStream(pBuffer, 0, nSize))
            {
                Type type= GetProtoType();
                var proto = new PBMessageSerializer().Deserialize(ms, null, type);
                OnReply(proto);
            }
        }


        public virtual void Dispose()
        {
            id = 0;
            GC.SuppressFinalize(this);
        }


    }

}