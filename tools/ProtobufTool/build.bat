@echo off

cd PBMessage

echo --delete all files in bin\Release\ [删除旧的生成文件]
del /q bin\Release\*

echo --delete all files in obj\Release\ [删除旧的生成文件]
del /q obj\Release\*

echo --gen proto message to PBMessage.cs [生成解析代码,全部放到一个cs源码里]
cd ..\gen
call __genbat.bat

echo --compile PBMessage.cs [编译源码 生成DLL]
cd ..\PBMessage
C:\Windows\Microsoft.NET\Framework\v4.0.30319\Csc.exe /noconfig /nowarn:1701,1702 /nostdlib+ /errorreport:prompt /warn:4 /define:TRACE /reference:C:\Windows\Microsoft.NET\Framework\v2.0.50727\mscorlib.dll /reference:protobuf-net.dll /reference:C:\Windows\Microsoft.NET\Framework\v2.0.50727\System.dll /debug:pdbonly /filealign:512 /optimize+ /out:obj\Release\PBMessage.dll /target:library /utf8output PBMessage.cs Properties\AssemblyInfo.cs

echo --output to bin\Release\ [设置DLL输出路径]
copy obj\Release\PBMessage.dll bin\Release\PBMessage.dll
copy obj\Release\PBMessage.pdb bin\Release\PBMessage.pdb
copy protobuf-net.dll bin\Release\protobuf-net.dll

echo --precompile PBMessage.dll [生成专门序列化的DLL文件]
cd bin\Release
..\..\..\protobuf-net\Precompile\precompile.exe PBMessage.dll -o:PBMessageSerializer.dll -t:PBMessageSerializer

: 复制文件到指定文件夹
echo --copy dlls to unity project [复制文件到指定文件夹Plugins]

copy PBMessage.dll ..\..\..\..\..\UnityEnv\Assets\Plugins\PBMessage.dll
copy PBMessageSerializer.dll ..\..\..\..\..\UnityEnv\Assets\Plugins\PBMessageSerializer.dll
copy protobuf-net.dll ..\..\..\..\..\UnityEnv\Assets\Plugins\protobuf-net.dll

echo 已经拷贝dll到Unity工程Plugins目录下！
rem pause
exit