FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
ARG PROJECT_PORT
WORKDIR /app
EXPOSE $PROJECT_PORT

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["AeroLinker.Core/AeroLinker.Core.WebAPI/AeroLinker.Core.WebAPI.csproj", "AeroLinker.Core/AeroLinker.Core.WebAPI/"]
COPY ["AeroLinker.Core/AeroLinker.Core.BLL/AeroLinker.Core.BLL.csproj", "AeroLinker.Core/AeroLinker.Core.BLL/"]
COPY ["AeroLinker.Core/AeroLinker.Core.DAL/AeroLinker.Core.DAL.csproj", "AeroLinker.Core/AeroLinker.Core.DAL/"]
COPY ["AeroLinker.Core/AeroLinker.Core.Common/AeroLinker.Core.Common.csproj", "AeroLinker.Core/AeroLinker.Core.Common/"]

RUN dotnet restore "AeroLinker.Core/AeroLinker.Core.WebAPI/AeroLinker.Core.WebAPI.csproj"
COPY . .
WORKDIR "/src/AeroLinker.Core/AeroLinker.Core.WebAPI"
RUN dotnet build "AeroLinker.Core.WebAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AeroLinker.Core.WebAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY AeroLinker.Core/AeroLinker.Core.WebAPI/Resources/AeroLinkerSetup.exe /app/Resources/
COPY AeroLinker.Core/AeroLinker.Core.WebAPI/Resources/AeroLinkerSetup-osx-x64.zip /app/Resources/
COPY --from=publish /app/publish .
ENV ASPNETCORE_URLS http://*:5050
ENTRYPOINT ["dotnet", "AeroLinker.Core.WebAPI.dll"]
