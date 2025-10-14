# Etapa 1: build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia tudo e restaura dependências
COPY . .
RUN dotnet restore "./api-questoes.csproj"

# Publica a aplicação
RUN dotnet publish "./api-questoes.csproj" -c Release -o /app/publish

# Etapa 2: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia a build publicada da etapa anterior
COPY --from=build /app/publish .

# Define a porta que o Railway usa
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "api-questoes.dll"]
