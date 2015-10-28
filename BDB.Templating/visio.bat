rem %1 - tempFile
rem %2 - tempDir
rem %3 - Xsl

del %2\report.vbs
del %2\report.vsd
msxsl %1 %3.xsl -o %2\report.vbs -t
wscript.exe %2\report.vbs %2
runner %2\report.vsd
