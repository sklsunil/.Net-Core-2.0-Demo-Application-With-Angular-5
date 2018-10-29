FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
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