
function Invoke-Manifesto
{
	<#
		.SYNOPSIS
			Get information from filest with Manifest
			Author: Eviatar Gerzi (@)
			License: Free
			Required Dependencies: None
			Optional Dependencies: None
			
			Version 1.0: 4.3.2018
			
		.DESCRIPTION

		.PARAMETER TargetComputerName
			The name of the computer name to detect for NTLM connections.

		.EXAMPLE 


		#>

    [CmdletBinding()]
	param
	(
		[Parameter(
			Mandatory = $true,
			ParameterSetName = 'FolderPath')
		]
        [string]$FolderPath,
		[switch]$Recursive = $false
	)		

    $code = @"
using System;
using System.Runtime.InteropServices;
using System.Text;
namespace ManifestoPowershell
{
    public class NativeAPI
    {
        [System.Flags]
        public enum LoadLibraryFlags : uint
        {
            DONT_RESOLVE_DLL_REFERENCES = 0x00000001,
            LOAD_IGNORE_CODE_AUTHZ_LEVEL = 0x00000010,
            LOAD_LIBRARY_AS_DATAFILE = 0x00000002,
            LOAD_LIBRARY_AS_DATAFILE_EXCLUSIVE = 0x00000040,
            LOAD_LIBRARY_AS_IMAGE_RESOURCE = 0x00000020,
            LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200,
            LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000,
            LOAD_LIBRARY_SEARCH_DLL_LOAD_DIR = 0x00000100,
            LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800,
            LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400,
            LOAD_WITH_ALTERED_SEARCH_PATH = 0x00000008
        }

        public const uint RT_MANIFEST = 24;
        public const uint CREATEPROCESS_MANIFEST_RESOURCE_ID = 1;


        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, LoadLibraryFlags dwFlags);

        [DllImport("Kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr LoadLibrary(string lpFileName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr FindResource(IntPtr hModule, uint lpName, uint lpType);
        //  public static extern IntPtr FindResource(IntPtr hModule, int lpName, uint lpType);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LoadResource(IntPtr hModule, IntPtr hResInfo);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern IntPtr LockResource(IntPtr hResData);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern uint SizeofResource(IntPtr hModule, IntPtr hResInfo);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool EnumResourceNames(IntPtr hModule, string lpType, IntPtr lpEnumFunc, IntPtr lParam);

    }

    public class ResourceManager
    {
        public static byte[] GetResourceFromExecutable(string lpFileName, uint lpName, uint lpType)
        {
            IntPtr hModule = NativeAPI.LoadLibraryEx(lpFileName, IntPtr.Zero, NativeAPI.LoadLibraryFlags.LOAD_LIBRARY_AS_DATAFILE);
            if (hModule != IntPtr.Zero)
            {
                IntPtr hResource = NativeAPI.FindResource(hModule, lpName, lpType);
                if (hResource != IntPtr.Zero)
                {
                    uint resSize = NativeAPI.SizeofResource(hModule, hResource);
                    IntPtr resData = NativeAPI.LoadResource(hModule, hResource);
                    if (resData != IntPtr.Zero)
                    {
                        byte[] uiBytes = new byte[resSize];
                        IntPtr ipMemorySource = NativeAPI.LockResource(resData);
                        Marshal.Copy(ipMemorySource, uiBytes, 0, (int)resSize);
                        return uiBytes;
                    }
                }
            }
            return null;
        }
    }

}
"@

    $location = [PsObject].Assembly.Location
    $compileParams = New-Object System.CodeDom.Compiler.CompilerParameters
    $assemblyRange = @("System.dll", $location)
    $compileParams.ReferencedAssemblies.AddRange($assemblyRange)
    $compileParams.GenerateInMemory = $True
    Add-Type -TypeDefinition $code -CompilerParameters $compileParams -passthru | Out-Null


    #$filePath = "C:\Windows\System32\calc.exe"
    function Get-ManifestInfo($filePath)
    {
        $bytes = [ManifestoPowershell.ResourceManager]::GetResourceFromExecutable($filePath, [ManifestoPowershell.NativeAPI]::CREATEPROCESS_MANIFEST_RESOURCE_ID, [ManifestoPowershell.NativeAPI]::RT_MANIFEST)
        $manifestInfo = $null
        if($bytes -ne $null)
        {
            $enc = [system.Text.Encoding]::UTF8
            $encodedString = $enc.GetString($bytes)
            try{

            $manifestInfo = New-Object psobject
            $xml = [xml]$encodedString

            $requestedExecutionLevel = $xml.GetElementsByTagName("requestedExecutionLevel")

        

            <#$properties = @{'level'= "";
                    'uiAccess'= "";
                    'autoElevate'= "";
                    'dpiAware'=""}
            $manifestInfo = New-Object psobject –Prop $properties
    #>

            foreach($attr in $requestedExecutionLevel.Attributes)
            {
                $manifestInfo | Add-Member -MemberType NoteProperty -Name $attr.Name -Value $attr.Value
            }

            $manifestInfo | Add-Member -MemberType NoteProperty -Name "autoElevate" -Value ($xml.GetElementsByTagName("autoElevate")).'#text'
            $manifestInfo | Add-Member -MemberType NoteProperty -Name "dpiAware" -Value ($xml.GetElementsByTagName("dpiAware")).'#text'
            }
            catch
            {
                $manifestInfo | Add-Member -MemberType NoteProperty -Name "autoElevate" -Value $encodedString
            }
        }

        return $manifestInfo
    }

    function Print-Info($info, $file)
    {
        Write-Host "[*] File: "$file
        $info | fl *
    }

    function Main($file)
    {
         $info = Get-ManifestInfo $file

         if(($info -ne $null) -and ([System.String]::Empty -ne ($info.Level + $info.uiAccess + $info.autoElevate + $info.dpiAware)))
         {
            Print-Info $info $file
         }
    }

    if($Recursive)
    {
        Get-ChildItem $FolderPath -Filter *.exe -Recurse | ForEach-Object {
            Main $_.FullName
        }
    }
    else
    {
        Get-ChildItem $FolderPath -Filter *.exe | ForEach-Object {
            Main $_.FullName
        }
    }
}

#Invoke-Manifesto -FolderPath "C:\Windows" 
