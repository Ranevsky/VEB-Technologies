FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Catalog.Presentation/Catalog.Presentation.csproj", "Catalog.Presentation/"]
COPY ["Catalog.Infrastructure/Catalog.Infrastructure.csproj", "Catalog.Infrastructure/"]
COPY ["Catalog.Application/Catalog.Application.csproj", "Catalog.Application/"]
COPY ["Catalog.Domain/Catalog.Domain.csproj", "Catalog.Domain/"]
RUN dotnet restore "Catalog.Presentation/Catalog.Presentation.csproj"
COPY . .
WORKDIR "/src/Catalog.Presentation"
RUN dotnet build "Catalog.Presentation.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Catalog.Presentation.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS="http://+:5000;https://+:5001"
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Catalog.Presentation.dll"]
