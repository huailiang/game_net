using System;

namespace XNet
{
    public abstract class Proto : IDisposable
    {
        public ushort id;

        public object pb;
        
        public virtual void OnTimeout() { }

        public abstract Type GetProtoType();

        public abstract void OnProcess(byte[] pBuffer);


        public virtual void Dispose()
        {
            id = 0;
            GC.SuppressFinalize(this);
        }
        
    }

}