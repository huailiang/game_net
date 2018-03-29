<p align="center">
	 <a href="https://huailiang.github.io/">
	    <img src="https://huailiang.github.io/img/cpp.jpeg" width="100" height="100">
    	</a>
	<a href="https://unity3d.com/cn/">
	    <img src="https://huailiang.github.io/img/unity.jpeg" width="200" height="100">
	</a>
    	<a href="https://huailiang.github.io/">
    	<img src="https://huailiang.github.io/img/avatar-Alex.jpg" width="120" height="100">
   	</a>
</p>

在tools/ProtobufTool/目录中：

客户端是Unity, 采用c#语言，服务器端使用的是c++。

客户端采取的protobuf版本是:3.0.0 服务器对应的版本3.1.0

由于谷歌官方给的版本对.net要求比较高，而且需要jit功能(ios被苹果禁止)，所以我们找到了第三方编译的版本。

关于unity protobuf的信息可以参考：https://github.com/google/protobuf/issues/644

客户端版本来源：https://github.com/bitcraftCoLtd/protobuf3-for-unity


tools目录：

运行build.bat 会生成cs proto文件，并将拷贝到对应的unity目录中

运行build_server.bat 会生成.h .cc文件， 并拷贝到Server目录



请将项目需要的.proto 全部定义在game.proto文件中，自动生成的代码全部保存在PBMessage.cs中

运行build—server.bat 会生成服务器端对应的代码。

服务端采用的protobuf版本是：protobuf-cpp-3.1.0， 对应的下载地址：https://github.com/google/protobuf/releases/download/v3.1.0/protobuf-cpp-3.1.0.zip

服务器用到的protobuf编译生成的libprotocd.lib、libprotobufd.lib、protoc.exe由于文件太大， 没有上传。
读者自行生成，可以参照csdn这篇paper: https://blog.csdn.net/program_anywhere/article/details/77365876  

服务器socket测试只支持windows平台，其他平台（linux）没有测试


运行效果如下图：
<img src="tools/img/show.gif">