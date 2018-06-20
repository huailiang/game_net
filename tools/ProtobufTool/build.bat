@echo off

echo --del old cs and clean env

del /q PBMessage.cs
del /q GamePb3.cs

echo --gen proto message to GamePb3.cs 

protoc --csharp_out ./ game-pb3.proto

echo --rename GamePb3.cs to PBMessage.cs

copy /y GamePb3.cs PBMessage.cs

echo --copy cs to unity project 

copy PBMessage.cs ..\..\UnityEnv\Assets\Scripts\netlib\

echo job done, bye!

pause

exit
