FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY Azor.sln ./

COPY src/Azor.Domain/Azor.Domain.csproj src/Azor.Domain/
COPY src/Azor.Application/Azor.Application.csproj src/Azor.Application/
COPY src/Azor.Infrastructure/Azor.Infrastructure.csproj src/Azor.Infrastructure/
COPY src/Azor.Api/Azor.Api.csproj src/Azor.Api/

RUN dotnet restore Azor.sln

COPY . .

RUN dotnet publish src/Azor.Api/Azor.Api.csproj -c Release -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

EXPOSE 8080
ENV ASPNETCORE_HTTP_PORTS=8080

COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Azor.Api.dll"]