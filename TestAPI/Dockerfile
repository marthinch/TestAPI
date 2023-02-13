#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TestAPI.csproj", "./"]
RUN dotnet restore "./TestAPI.csproj"
COPY . .
RUN dotnet build "TestAPI.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TestAPI.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TestAPI.dll"]
