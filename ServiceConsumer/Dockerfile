#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["ServiceConsumer/ServiceConsumer.csproj", "ServiceConsumer/"]
COPY ["Shared.Models/Shared.Models.csproj", "Shared.Models/"]
RUN dotnet restore "ServiceConsumer/ServiceConsumer.csproj"
COPY . .
WORKDIR "/src/ServiceConsumer"
RUN dotnet build "ServiceConsumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServiceConsumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServiceConsumer.dll"]