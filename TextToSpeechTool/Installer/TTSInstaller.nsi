; Text to Speech Tool installer
;
; This is currently an experiment. Do not use to build installer. Current installer is built using the 
; installer based on zip file option in the NSIS Main window.
;
;--------------------------------

; The name of the installer
Name "Text To Speech Tool"

; The file to write
OutFile "TTSInstaller.exe"

; Request application privileges for Windows Vista
RequestExecutionLevel user

; Build Unicode installer
Unicode True

; The default installation directory
InstallDir $ProgramFiles\TextToSpeechTool

;--------------------------------

; Pages

Page directory
Page instfiles

;--------------------------------

; The stuff to install
Section "" ;No components page, name is not important

  ; Set output path to the installation directory.
  SetOutPath $INSTDIR
  
  ; Put file there
  File example1.nsi
  
SectionEnd ; end the section
