@echo off&setlocal enabledelayedexpansion


:: data_de_ontem
@echo off
set day=-1
echo >"%temp%\%~n0.vbs" s=DateAdd("d",%day%,now) : d=weekday(s)
echo>>"%temp%\%~n0.vbs" WScript.Echo year(s)^& right(100+month(s),2)^& right(100+day(s),2)
for /f %%a in ('cscript /nologo "%temp%\%~n0.vbs"') do set "result=%%a"
del "%temp%\%~n0.vbs"
set "YYYY=%result:~0,4%"
set "MM=%result:~4,2%"
set "DD=%result:~6,2%"
set "data=%dd%/%mm%/%yyyy%"


C:\CMDFI\Contabil_CS_CMD\FI.CONTABIL.CDC.CMD MTPFCONT DTREF=%data% DIRLOG=C:\CMDLOG ARQLOG=PREFECHAMENTO_LOG.TXT DIRERRO=C:\CMDLOG ARQERRO=PREFECHAMENTO_ERRO.TXT

ECHO  AGUARDANDO PRE FECHAMENTO FINALIZAR...
TIMEOUT 120