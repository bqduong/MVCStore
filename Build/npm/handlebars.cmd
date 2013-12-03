@IF EXIST "%~dp0\node.exe" (
  "%~dp0\node.exe"  "%~dp0\node_modules\handlebars\bin\handlebars" %*
) ELSE (
  node  "%~dp0\node_modules\handlebars\bin\handlebars" %*
)