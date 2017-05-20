#include <idp.iss>

#define MyAppName "NetShare"
#define MyAppVersion "1.0.0.130"
#define MyAppTextVersion "v1.00 build 130"
#define MyAppPublisher "Vitaliy Zabrodin"
#define MyAppCopyright "Copyright (c) 2017 Vitaliy Zabrodin"
#define MyAppExeName "NetShare.exe"

[Setup]
AppId={{2D9B2BEC-0435-4FD2-A803-0846964A3D58}
AppName={#MyAppName}
AppVersion={#MyAppVersion}
AppPublisher={#MyAppPublisher}
DefaultDirName={pf}\{#MyAppName}
OutputDir=bin
OutputBaseFilename=NetShareSetup
SolidCompression=yes
UsePreviousAppDir=yes
UninstallDisplayName={#MyAppName}
SetupIconFile=resourses\install-icon.ico
MinVersion=6.1
AllowRootDirectory=yes
CreateUninstallRegKey=yes
DisableProgramGroupPage=auto
DefaultGroupName={#MyAppName}
AlwaysShowGroupOnReadyPage=yes
AlwaysShowDirOnReadyPage=yes
AllowNoIcons=yes
AlwaysUsePersonalGroup=yes
CloseApplications=no
AppCopyright={#MyAppCopyright}
VersionInfoProductName={#MyAppName}
VersionInfoProductVersion={#MyAppVersion}
VersionInfoProductTextVersion={#MyAppTextVersion}
VersionInfoVersion={#MyAppVersion}
VersionInfoCompany={#MyAppPublisher}
VersionInfoTextVersion={#MyAppTextVersion}
VersionInfoCopyright={#MyAppCopyright}
ShowTasksTreeLines=yes
LicenseFile=..\LICENSE

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"; LicenseFile: "..\LICENSE"

[CustomMessages]
english.ClosingApplications=Closing applications...
english.InstallingService=Installing service...
english.DotNetRequired={#MyAppName} requires Microsoft .NET Framework 4.0 Full Profile.'#13#13'The installer will attempt to install it
english.DotNetInstalling=Microsoft Framework 4.0 is installing. Please wait...

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"

[Files]
Source: "..\NetShare.Cilent\bin\Debug\NetShare.Cilent.exe"; DestDir: "{app}"; DestName: "{#MyAppExeName}"; Flags: ignoreversion
Source: "..\NetShare.Service\bin\Debug\NetShare.Service.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\NetShare.Host\bin\Debug\NetShare.Host.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\NetShare.ICS\bin\Debug\NetShare.ICS.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\NetShare.WLAN\bin\Debug\NetShare.Wlan.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\LICENSE"; DestDir: "{app}"; DestName: "license.txt"; Flags: ignoreversion
Source: "..\changelog.txt"; DestDir: "{app}"; Flags: ignoreversion

[Run]
Filename: "taskkill"; Parameters: "/f /im NetShare.Service.exe"; Flags: runhidden
Filename: "taskkill"; Parameters: "/f /im {#MyAppExeName}"; Flags: runhidden
Filename: "{app}\NetShare.Service.exe"; Parameters: "/install"; Flags: runhidden
Filename: "{app}\{#MyAppExeName}"; Flags: postinstall skipifsilent shellexec; Description: "Run {#MyAppName}"

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\{#MyAppExeName}";
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[UninstallDelete]
Type: files; Name: "{app}\{#MyAppExeName}"
Type: files; Name: "{app}\NetShare.Service.exe"
Type: files; Name: "{app}\NetShare.Host.dll"
Type: files; Name: "{app}\NetShare.ICS.dll"
Type: files; Name: "{app}\NetShare.Wlan.dll"
Type: files; Name: "{app}\license.txt"
Type: files; Name: "{app}\changelog.txt"
Type: filesandordirs; Name: "{app}"

[UninstallRun]
Filename: "taskkill"; Parameters: "/f /im NetShare.Service.exe"; Flags: runhidden
Filename: "taskkill"; Parameters: "/f /im {#MyAppExeName}"; Flags: runhidden
Filename: "{app}\NetShare.Service.exe"; Parameters: "/uninstall"; Flags: runhidden

[Code]
#include "scripts\dotnet.pas"
#include "scripts\uninstall_before.pas"
                                  
procedure InitializeWizard();
begin
  if not IsDotNetDetected('v4.0 Full Profile', 0) then
  begin
    idpAddFile('http://download.microsoft.com/download/9/5/A/95A9616B-7A37-4AF6-BC36-D6EA96C8DAAE/dotNetFx40_Full_x86_x64.exe', ExpandConstant('{tmp}\NetFrameworkInstaller.exe'));
    idpDownloadAfter(wpReady);
  end;
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
  case CurStep of
    ssInstall:
    begin
      if (IsUpgrade()) then
      begin
        UnInstallOldVersion();
      end;
      if not IsDotNetDetected('v4.0 Full Profile', 0) then
      begin
        InstallFramework();
      end;
    end;
  end;
end;