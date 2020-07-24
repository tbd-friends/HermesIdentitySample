#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src

COPY "Hermes.sln" "Hermes.sln"

COPY "src/Services/Identity/Identity.API/Identity.API.csproj" "src/Services/Identity/Identity.API/"
COPY "src/Services/Ordering/Ordering.API/Ordering.API.csproj" "src/Services/Ordering/Ordering.API/"
COPY "src/Services/Ordering/Ordering.SignalrHub/Ordering.SignalrHub.csproj" "src/Services/Ordering/Ordering.SignalrHub/"
COPY "src/Web/Dashboard/Dashboard.csproj" "src/Web/Dashboard/"

RUN dotnet restore "Hermes.sln"

COPY . .
WORKDIR "/src/src/Services/Identity/Identity.API"
RUN dotnet build "Identity.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Identity.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.API.dll"]