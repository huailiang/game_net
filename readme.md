<p align="center">
    <img src="https://raw.githubusercontent.com/sschmid/Entitas-CSharp/master/Readme/Images/MadeForUnity.png" width="200" height="100">
    <img src="http://sem.tanzhouedu.com/shiguang/it/iframe/img/C_C++.jpg" width="120" height="100">
    <img src="https://huailiang.github.io/img/avatar-Alex.jpg" width="120" height="100">
</p>

在tools/ProtobufTool/目录中：

客户端是Unity, 采用c#语言，服务器端使用的是c++。


tools目录：

运行build.bat 会生成三个dll到Unity工程的Plugins目录下，三个dll分别对应：

1. protobuf-net：protobuf核心工程，生成后的dll

2. protogen：用于将标准的protobuf定义文件“ * .proto”转换成“ * .cs”文件，这样就免去了重新定义协议。

3. precompile：用于生成protogen生成的文件所生成的dll所对应的序列化与反序列化dll。

请将项目需要的.proto 全部定义在game.proto文件中，自动生成的代码全部保存在PBMessage/PBMessage.cs中。



运行build—server.bat 会生成服务器端对应的代码。

服务端采用的protobuf版本是：protobuf-cpp-3.1.0， 对应的下载地址：https://github.com/google/protobuf/releases/download/v3.1.0/protobuf-cpp-3.1.0.zip

服务器用到的protobuf编译生成的libprotocd.lib、libprotobufd.lib、protoc.exe由于文件太大， 没有上传。
读者自行生成，可以参照csdn这篇paper: https://blog.csdn.net/program_anywhere/article/details/77365876  

服务器socket测试只支持windows平台，其他平台（linux）没有测试