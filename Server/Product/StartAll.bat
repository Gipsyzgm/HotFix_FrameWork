@echo off

echo ��ʼ������Ϸ�������
set path=%~dp0

echo ��ǰ·��path��%path%

cd %path%CenterServer\
echo ����CenterServer.exe
start CenterServer.exe
rem ��ʱ1��
C:\Windows\system32\choice.exe /t 1 /d y /n > nul

cd %path%GameServer\
echo ����GameServer.exe
start GameServer.exe
rem ��ʱ1��
C:\Windows\system32\choice.exe /t 1 /d y /n > nul

cd %path%GameServer\
echo ����GameServer.exe
start GameServer.exe
rem ��ʱ1��
C:\Windows\system32\choice.exe /t 1 /d y /n > nul

cd %path%LoginServer\
echo ����LoginServer.exe
start LoginServer.exe
rem ��ʱ1��
C:\Windows\system32\choice.exe /t 1 /d y /n > nul


echo ������������Ϸ�������

rem @pause