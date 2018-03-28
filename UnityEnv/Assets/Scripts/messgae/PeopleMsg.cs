using System;
using UnityEngine;

namespace XNet
{
    class PeopleMsg : Proto
    {

        public PeopleMsg()
        {
            id = 1001;
        }
        

        public override void OnReply(object proto)
        {
            People p = proto as People;
            Debug.Log("email:" + p.email + " snip cnt: " + p.snip.Count);
        }

        public override void OnTimeout()
        {
        }

        public override Type GetProtoType()
        {
            return typeof(People);
        }

    }

}