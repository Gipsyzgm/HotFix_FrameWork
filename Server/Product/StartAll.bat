@echo off

echo 开始启动游戏服务进程
set path=%~dp0

echo 当前路径path：%path%

cd %path%CenterServer\net5.0\
echo 启动CenterServer.exe
start CenterServer.exe
rem 延时1秒
C:\Windows\system32\choice.exe /t 1 /d y /n > nul

cd %path%GameServer\net5.0\
echo 启动GameServer.exe
start GameServer.exe
rem 延时1秒
C:\Windows\system32\choice.exe /t 1 /d y /n > nul

cd %path%GameServer\net5.0\
echo 启动GameServer.exe
start GameServer.exe
rem 延时1秒
C:\Windows\system32\choice.exe /t 1 /d y /n > nul

cd %path%LoginServer\net5.0\
echo 启动LoginServer.exe
start LoginServer.exe
rem 延时1秒
C:\Windows\system32\choice.exe /t 1 /d y /n > nul


echo 已启动所有游戏服务进程

rem @pause