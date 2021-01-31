#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#grunt cssmin
#grunt uglify
FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0.102-ca-patch-buster-slim AS build
WORKDIR /src
COPY ["Presentation/Presentation.csproj", "/Presentation/"]
RUN dotnet restore "/Presentation/Presentation.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "Presentation/Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Presentation/Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Presentation.dll"]
