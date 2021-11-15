set prompt=$G

if exist SampleModuleHost.exe del SampleModuleHost.exe

csc -out:SampleModuleHost.exe -r:Nancy.dll -r:Nancy.Hosting.Self.dll SampleModule.cs SampleHost.cs

start SampleModuleHost.exe

timeout 3

curl -i http://localhost:8081/greet/jbon007

curl -i http://localhost:8081/greet/

curl -i http://localhost:8081/

pause

curl -i -X POST --data @data.txt http://localhost:8081/post/text

rem useful for a8:

curl -i -X POST -H "Content-Type: application/json" --data-binary @data.txt http://localhost:8081/post/text

pause

curl -i -X POST -H "Content-Type: application/json" --data @json.txt http://localhost:8081/post/json

curl -i -X POST -H "Content-Type: text/xml" --data @xml.txt http://localhost:8081/post/xml

pause

curl -i -X POST -H "Content-Type: application/json" --data @json0.txt http://localhost:8081/post/model0

curl -i -X POST -H "Content-Type: text/xml" --data @xml0.txt http://localhost:8081/post/model0

pause

curl -i -X POST -H "Content-Type: application/json" --data @json.txt http://localhost:8081/post/modeljson

curl -i -X POST -H "Content-Type: text/xml" --data @xml.txt http://localhost:8081/post/modelxml

pause
