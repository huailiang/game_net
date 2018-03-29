@echo off

echo --del old cs and clean env

del /q PBMessage.cs
del /q Game.cs

echo --gen proto message to Game.cs 

protoc --csharp_out ./ game.proto

echo --rename Game.cs to PBMessage.cs

copy /y Game.cs PBMessage.cs

echo --copy cs to unity project 

copy PBMessage.cs ..\..\UnityEnv\Assets\Scripts\netlib\

echo job done, bye!

pause

exit
