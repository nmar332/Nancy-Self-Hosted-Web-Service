set prompt=$G

if exist A8.exe del A8.exe

csc -out:A8.exe -r:DynamicLinqUoA.dll -r:Nancy.dll -r:Nancy.Hosting.Self.dll A8.cs
pause

start A8.exe

rem -H "Content-Type: application/json"

timeout 3

rem just a basic test, unrelated to a8

curl http://localhost:8081/
pause

type request1.txt
curl -X POST --data-binary @request1.txt http://localhost:8081/post/a8 > _response1.txt
fc _response1.txt response1.txt
echo ERRORLEVEL=%ERRORLEVEL%
pause

type request2.txt
curl -X POST --data-binary @request2.txt http://localhost:8081/post/a8 > _response2.txt
fc _response2.txt response2.txt
echo ERRORLEVEL=%ERRORLEVEL%
pause

type request3.txt
curl -X POST --data-binary @request3.txt http://localhost:8081/post/a8 > _response3.txt
fc _response3.txt response3.txt
echo ERRORLEVEL=%ERRORLEVEL%
pause

type request4.txt
curl -X POST --data-binary @request4.txt http://localhost:8081/post/a8 > _response4.txt
fc _response4.txt response4.txt
echo ERRORLEVEL=%ERRORLEVEL%
pause

type request5.txt
curl -X POST --data-binary @request5.txt http://localhost:8081/post/a8 > _response5.txt
fc _response5.txt response5.txt
echo ERRORLEVEL=%ERRORLEVEL%
pause

type request6.txt
curl -X POST --data-binary @request6.txt http://localhost:8081/post/a8 > _response6.txt
fc _response6.txt response6.txt
echo ERRORLEVEL=%ERRORLEVEL%
pause

echo.
pause

