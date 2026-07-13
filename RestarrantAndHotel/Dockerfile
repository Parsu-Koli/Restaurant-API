FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["RestaurantAndHotel/RestaurantAndHotel.csproj", "RestaurantAndHotel/"]
COPY ["BLL/BLL.csproj", "BLL/"]
COPY ["DAL/DAL.csproj", "DAL/"]

RUN dotnet restore "RestaurantAndHotel/RestaurantAndHotel.csproj"

COPY . .

WORKDIR /src/RestaurantAndHotel

RUN dotnet publish "RestaurantAndHotel.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080

EXPOSE 8080

ENTRYPOINT ["dotnet", "RestaurantAndHotel.dll"]
