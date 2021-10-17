# DockerProject
#Create Dockerfile like so
#Get base sdk image from microsoft
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env

#Make work directory as app
WORKDIR /app
#Copy csproj file and restore any dependencies via NUGET

COPY *.csproj ./

#Restoring nuget packages
RUN dotnet restore


#Copy project files and build our release

COPY . ./

#building release
RUN dotnet publish -c Release -o out

#Generate Runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0

#Make work directory as app
WORKDIR /app

#Expose to 80 port
#EXPOSE 80

#Copy to app/out
COPY --from=build-env /app/out .

ENTRYPOINT ["dotnet", "MicroServiceExamplePlatformService.dll"]