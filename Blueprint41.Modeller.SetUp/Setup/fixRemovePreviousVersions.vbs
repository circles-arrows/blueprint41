Dim objInstaller
Dim objDatabase
Dim objView
Dim objResult

Dim strPathMsi 

If WScript.Arguments.Count <> 1 Then
    WScript.Echo "Usage: cscript fixRemovePreviousVersions.vbs <path to MSI>"
    WScript.Quit -1
End If

strPathMsi = WScript.Arguments(0)

Set objInstaller = CreateObject("WindowsInstaller.Installer")
Set objDatabase = objInstaller.OpenDatabase(strPathMsi, 1)
Set objView = objDatabase.OpenView("UPDATE InstallExecuteSequence SET Sequence=1450 WHERE Action='RemoveExistingProducts'")

WScript.Echo "Patching install sequence: UPDATE InstallExecuteSequence SET Sequence=1450 WHERE Action='RemoveExistingProducts'"
objView.Execute
objDatabase.Commit

WScript.Quit 0