# Manifesto
A tool for searching information about multiple files' manifests. 
The tool can assist especially by searching for files with `autoElevate` field enabled.  

# Overview

Some processes contain manifest [file](https://docs.microsoft.com/en-us/windows/desktop/sbscs/application-manifests) that affects the application at start time. Interesting fileds in the manifest file are `autoElevate` and `Level` which determine how the application will work with UAC. Number of [UAC bypasses](https://medium.com/tenable-techblog/uac-bypass-by-mocking-trusted-directories-24a96675f6e) were searching for files with `autoElevate` field enabled in order to bypass UAC.  
This tool provides simple GUI that provide better visuality when searching for multiple files and it also have PowerShell version.  

# Usage

## GUI  
Run the executable and you will get GUI with all the options.
<img src="https://github.com/g3rzi/Manifesto/blob/assets/manifesto_gui.PNG" width="260">  

##  PowerShell
Import the module
```powershell
Import-Module Invoke-Manifesto
```
Run it like that for all the options:  
```powershell
Invoke-Manifesto -FolderPath "C:\Windows\system32"
```  
For all the other switches, check the PowerShell code.  
<img src="https://github.com/g3rzi/Manifesto/blob/assets/manifesto_powershell.PNG" width="260">  
