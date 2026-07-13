# ==========================
# Build Stage
# ==========================
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src

# Copy project files
COPY ["Restaurant/Restaurant.csproj", "Restaurant/"]
COPY ["BLL/BLL.csproj", "BLL/"]
COPY ["DAL/DAL.csproj", "DAL/"]

# Restore packages
RUN dotnet restore "Restaurant/Restaurant.csproj"

# Copy all source files
COPY . .

# Build
WORKDIR /src/Restaurant
RUN dotnet build "Restaurant.csproj" -c Release -o /app/build

# Publish
RUN dotnet publish "Restaurant.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ==========================
# Runtime Stage
# ==========================
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

ENTRYPOINT ["dotnet", "Restaurant.dll"]
