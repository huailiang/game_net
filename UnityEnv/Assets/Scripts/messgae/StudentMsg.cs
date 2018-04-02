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
        
        public override void OnProcess(byte[] pBuffer)
        {
            Student p = Student.Parser.ParseFrom(pBuffer);
            Debug.Log("age:" + p.Age + " num: " + p.Num + " name: " + p.Name);

            GameObject go = new GameObject(p.Name);
            go.transform.position = Vector3.zero;
        }

        public override Type GetProtoType()
        {
            return typeof(Student);
        }

        public override void OnTimeout()
        {
        }

    }

}
