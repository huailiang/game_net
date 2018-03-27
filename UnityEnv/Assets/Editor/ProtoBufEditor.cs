using UnityEditor;
using System.Diagnostics;
using System;

public class ProtoBufEditor : Editor
{

    [MenuItem("Tools/protobuf/make-dll")]
    public static void ToMakeProto()
    {
#if UNITY_EDITOR_WIN
        Process proc = null;
        try
        {
            string path = UnityEngine.Application.dataPath;
            path = path.Remove(path.IndexOf( "UnityEnv"));
            UnityEngine.Debug.Log(path);
            
            
            string targetDir = path + "tools/ProtobufTool";
            proc = new Process();
            proc.StartInfo.WorkingDirectory = targetDir;
            proc.StartInfo.FileName = "build.bat";
            proc.StartInfo.Arguments = string.Format("10");//this is argument
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;//这里设置DOS窗口不显示，经实践可行
            proc.Start();
            proc.WaitForExit();
            EditorUtility.DisplayDialog("message","protobuf dll done","ok");
        }
        catch (Exception ex)
        {
            EditorUtility.DisplayDialog("error", string.Format("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString()), "ok");
        }
#else
         EditorUtility.DisplayDialog("warn", "Do this step in windows editor!", "ok");
#endif
    }


}
