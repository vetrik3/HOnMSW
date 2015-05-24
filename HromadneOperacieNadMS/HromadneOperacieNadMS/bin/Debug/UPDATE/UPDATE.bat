taskkill /im HONMSW_CLIENT.vshost.exe /F
cd..
xcopy "%~dp0\UPDATE\HONMSW_CLIENT.exe" "%~dp0HONMSW_CLIENT.exe"
"%~dp0HONMSW_CLIENT.exe"