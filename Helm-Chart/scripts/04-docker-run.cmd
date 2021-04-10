REM Executes an image in a container and exposes internal port 80 to 8080, http://localhost:8080/swagger

docker run -it --rm -e "ASPNETCORE_ENVIRONMENT=Development" --name my-example -p 8080:80 registry.gitlab.com/netninja2202/project:latest 

REM -it =    interactive + tty
REM --rm =   Automatically remove the container when it exits
REM -e =     Set environment variables
REM --name = Give docker container a name, default is a random name
REM -p =     Publish port