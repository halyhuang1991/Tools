rd /s /q  BI
mkdir BI

del /Q BI\*
copy /y D:\NewWork\WinFormSystem\BI_Report\BogartRpt\bin\BogartMis.exe BI
set "filename=BI%date:~0,4%%date:~5,2%%date:~8,2%"
echo %filename%  
cd BI

if exist  %cd%\%filename%.exe ( del %filename%.exe  ) else (echo "")
ren BogartMis.exe %filename%.exe
cd ../
del /Q BI.zip
zip -r -q -o BI.zip  BI
rem ../  上级
rem 复制到下级目录，改名打包