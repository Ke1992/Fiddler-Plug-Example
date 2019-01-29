; ֱ�Ӵӹ����������ַ������ҷ���
; https://nsis.sourceforge.io/StrContains
; StrContains
; This function does a case sensitive searches for an occurrence of a substring in a string. 
; It returns the substring if it is found. 
; Otherwise it returns null(""). 
; Written by kenglish_hi
; Adapted from StrReplace written by dandaman32
Var STR_HAYSTACK
Var STR_NEEDLE
Var STR_CONTAINS_VAR_1
Var STR_CONTAINS_VAR_2
Var STR_CONTAINS_VAR_3
Var STR_CONTAINS_VAR_4
Var STR_RETURN_VAR
 
Function StrContains
  Exch $STR_NEEDLE
  Exch 1
  Exch $STR_HAYSTACK
  ; Uncomment to debug
  ;MessageBox MB_OK 'STR_NEEDLE = $STR_NEEDLE STR_HAYSTACK = $STR_HAYSTACK '
    StrCpy $STR_RETURN_VAR ""
    StrCpy $STR_CONTAINS_VAR_1 -1
    StrLen $STR_CONTAINS_VAR_2 $STR_NEEDLE
    StrLen $STR_CONTAINS_VAR_4 $STR_HAYSTACK
    loop:
      IntOp $STR_CONTAINS_VAR_1 $STR_CONTAINS_VAR_1 + 1
      StrCpy $STR_CONTAINS_VAR_3 $STR_HAYSTACK $STR_CONTAINS_VAR_2 $STR_CONTAINS_VAR_1
      StrCmp $STR_CONTAINS_VAR_3 $STR_NEEDLE found
      StrCmp $STR_CONTAINS_VAR_1 $STR_CONTAINS_VAR_4 done
      Goto loop
    found:
      StrCpy $STR_RETURN_VAR $STR_NEEDLE
      Goto done
    done:
   Pop $STR_NEEDLE ;Prevent "invalid opcode" errors and keep the
   Exch $STR_RETURN_VAR  
FunctionEnd
 
!macro _StrContainsConstructor OUT NEEDLE HAYSTACK
  Push `${HAYSTACK}`
  Push `${NEEDLE}`
  Call StrContains
  Pop `${OUT}`
!macroend
 
!define StrContains '!insertmacro "_StrContainsConstructor"'

;����if������(�߼���)
!include "LogicLib.nsh"

;����
Name "Fiddler Example"
;������ļ�����
OutFile "FiddlerExample.exe"
;����exe�ļ���ͼ����ʽ
Icon "icon.ico"
;��װ��ʱ���Ƿ�Ҫ�����ԱȨ��
RequestExecutionLevel "admin"
;ָ��ѹ����ʽ
SetCompressor lzma
;��ʾ�ڵײ����İ�
BrandingText "Fiddler Example v1.0.0"
;���ð�װ·��
;$PROGRAMFILES�������ļ�Ŀ¼(ͨ��Ϊ C:\Program Files ��������ʱ����)
InstallDir "$PROGRAMFILES\Fiddler\Scripts\"
;��ȡע�����Fiddler�İ�װ·��(��ȡʧ�ܵ�����»�ʹ����һ����·��)
InstallDirRegKey HKCU "SOFTWARE\Microsoft\Fiddler2\InstallerSettings" "ScriptPath"

;������ҳ��
Page directory ;installation directory selection page
Page instfiles ;installation page where the sections are executed

;һ����������
Section "Main"
	;�ж��Ƿ������Ҫ�ֶ�
	${StrContains} $0 "Fiddler\Scripts" $INSTDIR
	;������Ϊ��(����������Ҫ�ֶ�)
	${If} $0 == ""
		MessageBox MB_OK|MB_ICONEXCLAMATION "��ѡ��Fiddler��װĿ¼�µ�Scripts�ļ���"
		;����Ŀ¼ѡ��ҳ
		SendMessage $HWNDPARENT 0x408 -1 0
		;��ִ�к���Ĵ���
		Abort
	${EndIf}
	;$INSTDIR���û�����Ľ�ѹ·��
	SetOutPath "$INSTDIR"
	;�Ƿ�����дģʽ
	SetOverwrite on
	;��Ҫ�����exe���ļ�
	File "FiddlerExample.dll"
	File "Newtonsoft.Json.dll"
	;�������־��
	DetailPrint "��װ·����$INSTDIR"
	DetailPrint "��װ�ɹ���"
	;ʹ��MessageBox����һ���Ի���
	MessageBox MB_OK|MB_ICONEXCLAMATION "��װ�ɹ�"
SectionEnd

;�Ƿ���ʾ��װ��־
ShowInstDetails show