@echo off

echo 开始拷贝proto到Server
copy game.proto ..\..\Server\Server\

echo 进入到Server所在的目录
cd ..\..\Server\Server\

echo 开始编译.h和.cc
protoc --cpp_out ./ game.proto

echo 操作完成，再见!
pause
exit