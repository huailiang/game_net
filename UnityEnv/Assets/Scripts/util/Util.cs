using System;
using UnityEngine;

public class Util
{

    public static void PrintBytes(byte[] bytes)
    {
        Debug.Log(BitConverter.ToString(bytes));
    }
}
