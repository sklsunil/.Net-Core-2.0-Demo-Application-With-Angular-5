#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/aspnetcore:2.0-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0-nanoserver-1803 AS build
WORKDIR /src
COPY ["Demo.API/Demo.API.csproj", "Demo.API/"]
COPY ["Demo.BLL/Demo.BLL.csproj", "Demo.BLL/"]
COPY ["Demo.DAL/Demo.DAL.csproj", "Demo.DAL/"]
COPY ["Demo.DomainModels/Demo.DomainModels.csproj", "Demo.DomainModels/"]
RUN dotnet restore "Demo.API/Demo.API.csproj"
COPY . .
WORKDIR "/src/Demo.API"
RUN dotnet build "Demo.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Demo.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Demo.API.dll"]