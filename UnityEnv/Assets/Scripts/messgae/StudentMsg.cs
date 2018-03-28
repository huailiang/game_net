using System;
using UnityEngine;

namespace XNet
{
    class StudentMsg : Proto
    {

        public StudentMsg()
        {
            id = 1002;
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
