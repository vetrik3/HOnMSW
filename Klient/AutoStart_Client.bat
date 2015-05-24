REG ADD "HKLM\SOFTWARE\Microsoft\Windows\CurrentVersion\Run" /V "HONMSW_CLIENT" /t REG_SZ /F /D "%~dp0HONMSW_CLIENT.exe"
pause