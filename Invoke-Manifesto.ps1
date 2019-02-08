
function Invoke-Manifesto
{
	<#
		.SYNOPSIS
			Get information from filest with Manifest
			Author: Eviatar Gerzi (@g3rzi)
			License: Free
			Required Dependencies: None
			Optional Dependencies: None
			
			Version 1.1: 9.2.2019
				- Added more examples and usage information

			Version 1.0: 4.3.2018
			
		.DESCRIPTION

		.PARAMETER FolderPath
			The folder where to search for the files' manifests

		.PARAMETER Recursive
			Search recursively inside the subfolders
		
		.PARAMETER asInvoker
			Search for manifests with Level = asInvoker
			
		.PARAMETER requireAdministrator
			Search for manifests with Level = requireAdministrator
			
		.PARAMETER highestAvailable
			Search for manifests with Level = highestAvailable
		
		.PARAMETER uiAccessTrue
			Search for manifests with uiAccess = True
			
		.PARAMETER uiAccessFalse
			Search for manifests with uiAccess = False
			
		.PARAMETER autoElevateTrue
			Search for manifests with uiAccess = True
				
		.PARAMETER autoElevateFalse
			Search for manifests with uiAccess = False
				
		.PARAMETER dpiAwareTrue
			Search for manifests with uiAccess = True
				
		.PARAMETER dpiAwareFalse
			Search for manifests with uiAccess = False
		
		.EXAMPLE 
			Invoke-Manifesto -FolderPath "C:\Windows\system32" -uiAccessTrue

		.EXAMPLE 
			Invoke-Manifesto -FolderPath "C:\Windows\system32" -uiAccessTrue -Recursive
		#>

    [CmdletBinding()]
	param
	(
		[Parameter(
			Mandatory = $true,
			ParameterSetName = 'FolderPath')
		]
        [string]$FolderPath,
		[switch]$Recursive = $false,
        [switch]$asInvoker,
        [switch]$requireAdministrator,
        [switch]$highestAvailable,
        [switch]$uiAccessTrue,
        [switch]$uiAccessFalse,
        [switch]$autoElevateTrue,
        [switch]$autoElevateFalse,
        [switch]$dpiAwareTrue,
        [switch]$dpiAwareFalse
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
        $manifestInfo = $null
        try
        {
            $bytes = [ManifestoPowershell.ResourceManager]::GetResourceFromExecutable($filePath, [ManifestoPowershell.NativeAPI]::CREATEPROCESS_MANIFEST_RESOURCE_ID, [ManifestoPowershell.NativeAPI]::RT_MANIFEST)
            # Some files were UTF8 BOM. Trying to encode it without BOM
            # $enc= New-Object System.Text.UTF8Encoding $False
            # Didn't help.
            # Solved it by removing the first three bytes: 239, 187, 191
            if($bytes[0] -eq 239)
            {
                $first, $second, $third, $bytes = $bytes
            }

            
            if($bytes -ne $null)
            {
                $enc = [system.Text.Encoding]::UTF8


                $encodedString = $enc.GetString($bytes)
                try{

                    $manifestInfo = New-Object psobject
                    # " to type "System.Xml.XmlDocument". Error: "'asmv3' is an undeclared prefix. Line 15, position 6."
                    # C:\Windows\system32\changepk.exe
                    $xml = [xml]$encodedString

                    $requestedExecutionLevel = $xml.GetElementsByTagName("requestedExecutionLevel")

                    <#$properties = @{'level'= "";
                            'uiAccess'= "";
                            'autoElevate'= "";
                            'dpiAware'=""}
                    $manifestInfo = New-Object psobject –Prop $properties
            #>

                    $manifestInfo | Add-Member -MemberType NoteProperty -Name "FileName" -Value $filePath
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
        }
        catch
        {
            # No manifest resource
        }

        return $manifestInfo
    }

    function Count-FindingInfo($flag, $flagName, $infoField, [ref]$found)
    {
        if($flag -and $infoField -ne $null -and $infoField.ToLower() -eq $flagName.ToLower())
        {
            $found.value++
        }
    }
    
    function Is-Filtered($info)
    {
        $isAllowed = $false
        $shouldFound = 0
        $found = 0

        if($asInvoker -or $requireAdministrator -or $highestAvailable)
        {
            $shouldFound++
            Count-FindingInfo $asInvoker "asInvoker" $info.Level ([ref]$found)
            Count-FindingInfo $requireAdministrator "requireAdministrator" $info.Level ([ref]$found)
            Count-FindingInfo $highestAvailable "highestAvailable" $info.Level ([ref]$found)
        }

        if($uiAccessTrue -or $uiAccessFalse)
        {
            $shouldFound++
            Count-FindingInfo $uiAccessTrue "true" $info.uiAccess ([ref]$found)
            Count-FindingInfo $uiAccessFalse "false" $info.uiAccess ([ref]$found)
        }

        if($autoElevateTrue -or $autoElevateFalse)
        {
            $shouldFound++
            Count-FindingInfo $autoElevateTrue "true" $info.autoElevate ([ref]$found)
            Count-FindingInfo $autoElevateFalse "false" $info.autoElevate ([ref]$found)
        }

        if($dpiAwareTrue -or $dpiAwareFalse)
        {
            $shouldFound++
            Count-FindingInfo $dpiAwareTrue "true/PM" $info.dpiAware ([ref]$found)
            Count-FindingInfo $dpiAwareTrue "true" $info.dpiAware ([ref]$found)
            Count-FindingInfo $dpiAwareFalse "false" $info.dpiAware ([ref]$found)
        }

        if($shouldFound -eq $found){
            $isAllowed = $true
        }

        return $isAllowed 
    }

    function Main($file)
    {
         $info = Get-ManifestInfo $file

         if(($info -ne $null) -and ([System.String]::Empty -ne ($info.Level + $info.uiAccess + $info.autoElevate + $info.dpiAware)))
         {
            if(Is-Filtered $info)
            {
                $info | fl *
            }
         }
    }

    if($Recursive)
    {
        Get-ChildItem $FolderPath -Filter *.exe -Recurse -ErrorAction SilentlyContinue | ForEach-Object {
            try
            {
                Main $_.FullName
            }
            catch
            {
                $a = 3
                
            }
        }
    }
    else
    {
        Get-ChildItem $FolderPath -Filter *.exe | ForEach-Object {
            Main $_.FullName
        }
    }
}