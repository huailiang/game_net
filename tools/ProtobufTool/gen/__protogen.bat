@echo off

cd ..\
protobuf-net\ProtoGen\protogen.exe ^
-i:game.proto ^
-o:PBMessage\PBMessage.cs -ns:PBMessage
cd gen
