using System;
using UnityEngine;

namespace XNet
{
    class PeopleMsg : Proto
    {

        public PeopleMsg(object _pb)
        {
            id = 12;
            pb = _pb;
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