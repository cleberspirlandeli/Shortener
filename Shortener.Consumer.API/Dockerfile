#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Shortener.Consumer.API/Shortener.Consumer.API.csproj", "Shortener.Consumer.API/"]
RUN dotnet restore "Shortener.Consumer.API/Shortener.Consumer.API.csproj"
COPY . .
WORKDIR "/src/Shortener.Consumer.API"
RUN dotnet build "Shortener.Consumer.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shortener.Consumer.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shortener.Consumer.API.dll"]