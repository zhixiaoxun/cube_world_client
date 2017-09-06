@ECHO OFF

SET protoSrcDir=..\..\..\mcworld_server\gameserver\common\proto
SET protoDstDir=..\..\mcworld\Assets\Core\Scripts\GameLogic\Network\Proto

CALL :buildProto proto_common.proto
CALL :buildProto proto_client.proto
CALL :buildProto proto_server.proto

DEL %cd%\proto_client.proto
DEL %cd%\proto_server.proto
DEL %cd%\proto_common.proto

GOTO :EOF

:buildProto
SET protoFile=%~1
ECHO building %protoFile%
ECHO %protoSrcDir%\%protoFile%
COPY /Y %protoSrcDir%\%protoFile% %cd%
protogen.exe -i:%protoFile% -o:%protoDstDir%\%protoFile%.cs -q
EXIT /B %ERRORLEVEL%
