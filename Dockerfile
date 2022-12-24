FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

WORKDIR /src
COPY ["src/InstaShare.Web.Host/InstaShare.Web.Host.csproj", "src/InstaShare.Web.Host/"]
COPY ["src/InstaShare.Web.Core/InstaShare.Web.Core.csproj", "src/InstaShare.Web.Core/"]
COPY ["src/InstaShare.Application/InstaShare.Application.csproj", "src/InstaShare.Application/"]
COPY ["src/InstaShare.Core/InstaShare.Core.csproj", "src/InstaShare.Core/"]
COPY ["src/InstaShare.EntityFrameworkCore/InstaShare.EntityFrameworkCore.csproj", "src/InstaShare.EntityFrameworkCore/"]
WORKDIR "/src/src/InstaShare.Web.Host"
RUN dotnet restore 

WORKDIR /src
COPY ["src/InstaShare.Web.Host", "src/InstaShare.Web.Host"]
COPY ["src/InstaShare.Web.Core", "src/InstaShare.Web.Core"]
COPY ["src/InstaShare.Application", "src/InstaShare.Application"]
COPY ["src/InstaShare.Core", "src/InstaShare.Core"]
COPY ["src/InstaShare.EntityFrameworkCore", "src/InstaShare.EntityFrameworkCore"]
WORKDIR "/src/src/InstaShare.Web.Host"
RUN dotnet publish -c Release -o /publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:6.0
EXPOSE 80
WORKDIR /app
COPY --from=build /publish .
ENTRYPOINT ["dotnet", "InstaShare.Web.Host.dll"]
