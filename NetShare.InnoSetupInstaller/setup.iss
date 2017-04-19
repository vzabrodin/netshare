#define MyAppName "NetShare"
#define MyAppVersion "1.0.0.127"
#define MyAppTextVersion "v1.00 build 127"
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
UninstallDisplayName=NetShare
UninstallDisplayIcon={app}\NetShare.exe
SetupIconFile=.\install.ico
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
VersionInfoProductName=NetShare
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

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"

[Files]
Source: "..\NetShare.Cilent\bin\Debug\NetShare.Cilent.exe"; DestDir: "{app}"; DestName: "NetShare.exe"; Flags: ignoreversion
Source: "..\NetShare.Service\bin\Debug\NetShare.Service.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\NetShare.Host\bin\Debug\NetShare.Host.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\NetShare.ICS\bin\Debug\NetShare.ICS.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\NetShare.WLAN\bin\Debug\NetShare.Wlan.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "..\LICENSE"; DestDir: "{app}"; DestName: "license.txt"; Flags: ignoreversion
Source: "..\changelog.txt"; DestDir: "{app}"; Flags: ignoreversion

[Run]
Filename: "taskkill"; Parameters: "/f /im NetShare.Service.exe"; Flags: runhidden
Filename: "taskkill"; Parameters: "/f /im NetShare.exe"; Flags: runhidden
Filename: "{app}\NetShare.Service.exe"; Parameters: "/install"; Flags: runhidden
Filename: "{app}\NetShare.exe"; Flags: postinstall skipifsilent shellexec; Description: "Run {#MyAppName}"

[Icons]
Name: "{group}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\{#MyAppExeName}";
Name: "{group}\{cm:UninstallProgram,{#MyAppName}}"; Filename: "{uninstallexe}"
Name: "{commondesktop}\{#MyAppName}"; Filename: "{app}\{#MyAppExeName}"; IconFilename: "{app}\{#MyAppExeName}"; Tasks: desktopicon

[UninstallDelete]
Type: files; Name: "{app}\NetShare.exe"
Type: files; Name: "{app}\NetShare.Service.exe"
Type: files; Name: "{app}\NetShare.Host.dll"
Type: files; Name: "{app}\NetShare.ICS.dll"
Type: files; Name: "{app}\NetShare.Wlan.dll"

[UninstallRun]
Filename: "taskkill"; Parameters: "/f /im NetShare.Service.exe"; Flags: runhidden
Filename: "taskkill"; Parameters: "/f /im NetShare.exe"; Flags: runhidden
Filename: "{app}\NetShare.Service.exe"; Parameters: "/uninstall"; Flags: runhidden

[Code]
function GetTextAppVersion(Version: String): String; forward;
procedure Explode(var Dest: TArrayOfString; Text: String; Separator: String);
var
  i, p: Integer;
begin
  i := 0;
  repeat
    SetArrayLength(Dest, i+1);
    p := Pos(Separator,Text);
    if p > 0 then begin
      Dest[i] := Copy(Text, 1, p-1);
      Text := Copy(Text, p + Length(Separator), Length(Text));
      i := i + 1;
    end else begin
      Dest[i] := Text;
      Text := '';
    end;
  until Length(Text)=0;
end;

function GetTextAppVersion(Version: String): String;
var
  aStr: TArrayOfString;
begin
  Explode(aStr, Version, '.');
  Result:= 'v' + aStr[0] + '.' + aStr[1] + aStr[2] + ', build ' + aStr[3];
end;

function GetUninstallString(): String;
var
  sUnInstPath: String;
  sUnInstallString: String;
begin
  sUnInstPath := ExpandConstant('Software\Microsoft\Windows\CurrentVersion\Uninstall\{#emit SetupSetting("AppId")}_is1');
  sUnInstallString := '';
  if not RegQueryStringValue(HKLM, sUnInstPath, 'UninstallString', sUnInstallString) then
    RegQueryStringValue(HKCU, sUnInstPath, 'UninstallString', sUnInstallString);
  Result := sUnInstallString;
end;

function IsUpgrade(): Boolean;
begin
  Result := (GetUninstallString() <> '');
end;

function UnInstallOldVersion(): Integer;
var
  sUnInstallString: String;
  iResultCode: Integer;
begin
  // Return Values:
  // 1 - uninstall string is empty
  // 2 - error executing the UnInstallString
  // 3 - successfully executed the UnInstallString

  // default return value
  Result := 0;

  // get the uninstall string of the old app
  sUnInstallString := GetUninstallString();
  if sUnInstallString <> '' then begin
    sUnInstallString := RemoveQuotes(sUnInstallString);
    if Exec(sUnInstallString, '/SILENT /NORESTART /SUPPRESSMSGBOXES','', SW_HIDE, ewWaitUntilTerminated, iResultCode) then
      Result := 3
    else
      Result := 2;
  end else
    Result := 1;
end;

procedure CurStepChanged(CurStep: TSetupStep);
begin
  if (CurStep=ssInstall) then
  begin
    if (IsUpgrade()) then
    begin
      UnInstallOldVersion();
    end;
  end;
end;