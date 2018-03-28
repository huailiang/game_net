using System;
using UnityEngine;

namespace XNet
{
    class StudentMgr : Proto
    {

        public StudentMgr(object _pb)
        {
            id = 1002;
            pb = _pb;
        }

        public override void OnReply(object proto)
        {
            Student p = proto as Student;
            Debug.Log("age:" + p.age + " num: " + p.num + " name: " + p.name);
        }

        public override void OnTimeout()
        {
        }

        public override Type GetProtoType()
        {
            return typeof(Student);
        }
    }

}
