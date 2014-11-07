REM ********************************************************************************************************************
REM * Hi-Jack the Auto Build Variables by QtAgent since this is injected after it has REM * setup
REM * Open the autogenerated qtREM * setup in the test run location of
REM * C:\Users\IntegrationTester\AppData\Local\VSEQT\QTAgent\...
REM * For example:
REM * set DeploymentDirectory=C:\Users\INTEGR~1\AppData\Local\VSEQT\QTAgent\54371B~1\RSAKLF~1\DEPLOY~1
REM * set TestRunDirectory=C:\Users\INTEGR~1\AppData\Local\VSEQT\QTAgent\54371B~1\RSAKLF~1
REM * set TestRunResultsDirectory=C:\Users\INTEGR~1\AppData\Local\VSEQT\QTAgent\54371B~1\RSAKLF~1\Results\RSAKLF~1
REM * set TotalAgents=5
REM * set AgentWeighting=100
REM * set AgentLoadDistributor=Microsoft.VisualStudio.TestTools.Execution.AgentLoadDistributor
REM * set AgentId=1
REM * set TestDir=C:\Users\INTEGR~1\AppData\Local\VSEQT\QTAgent\54371B~1\RSAKLF~1
REM * set ResultsDirectory=C:\Users\INTEGR~1\AppData\Local\VSEQT\QTAgent\54371B~1\RSAKLF~1\Results
REM * set DataCollectionEnvironmentContext=Microsoft.VisualStudio.TestTools.Execution.DataCollectionEnvironmentContext
REM * set TestLogsDir=C:\Users\INTEGR~1\AppData\Local\VSEQT\QTAgent\54371B~1\RSAKLF~1\Results\RSAKLF~1
REM * set ControllerName=rsaklfsvrtfsbld:6901
REM * set TestDeploymentDir=C:\Users\INTEGR~1\AppData\Local\VSEQT\QTAgent\54371B~1\RSAKLF~1\DEPLOY~1
REM * set AgentName=RSAKLFTST7X64-3
REM ********************************************************************************************************************

REM ** Kill The Warewolf ;) **
sc stop "Warewolf Server"
taskkill /im "Warewolf Server.exe"
taskkill /im "Warewolf Studio.exe"

REM  Wait 10 seconds ;)
ping -n 10 127.0.0.1 > nul

REM Init paths to Warewolf server under test
IF NOT EXIST %DeploymentDirectory% IF EXIST "%~dp0..\Dev2.Server\bin\Debug\Warewolf Server.exe" SET DeploymentDirectory=%~dp0..\Dev2.Server\bin\Debug
IF EXIST "%DeploymentDirectory%\Server\Warewolf Server.exe" SET DeploymentDirectory=%DeploymentDirectory%\Server
IF EXIST "%DeploymentDirectory%\ServerStarted" DEL "%DeploymentDirectory%\ServerStarted"

REM ** Start Warewolf server from deployed binaries **
IF NOT EXIST %TestRunDirectory%\..\..\..\nircmd.exe GOTO RegularStartup
%TestRunDirectory%\..\..\..\nircmd.exe elevate "%DeploymentDirectory%\Warewolf Server.exe"
GOTO WaitForServerStart
:RegularStartup
START "%DeploymentDirectory%\Warewolf Server.exe" /D "%DeploymentDirectory%\Server" "Warewolf Server.exe"

rem ping server until it responds
:WaitForServerStart
IF EXIST "%DeploymentDirectory%\ServerStarted" goto StartStudio 
rem wait for 10 seconds before trying again
@echo Waiting 10 seconds...
ping -n 10 127.0.0.1 > nul
goto WaitForServerStart 

:StartStudio
REM ** Start Warewolf studio from deployed binaries **
START "%DeploymentDirectory%\Studio\Warewolf Studio.exe" /D "%DeploymentDirectory%\Studio" "Warewolf Studio.exe"

REM  Wait 30 seconds ;)
ping -n 30 127.0.0.1 > nul

exit 0