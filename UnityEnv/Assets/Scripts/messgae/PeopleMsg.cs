using System;
using UnityEngine;

namespace XNet
{
    class PeopleMsg : Proto
    {
        private People cache;

        public PeopleMsg()
        {
            id = 1001;
        }

        public override void OnProcess(byte[] pBuffer)
        {
            People p = People.Parser.ParseFrom(pBuffer);
            Debug.Log("email:" + p.Email + " snip cnt: " + p.Snip.Count+" phone:"+p.Phones[0].Number+" type: "+p.Phones[0].Type);
        }

        public override Type GetProtoType()
        {
            return typeof(People);
        }

        public override void OnTimeout()
        {
        }

    }

}