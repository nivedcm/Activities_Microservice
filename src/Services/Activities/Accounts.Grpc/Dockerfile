#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Activities/Accounts.Grpc/Accounts.Grpc.csproj", "Services/Activities/Accounts.Grpc/"]
COPY ["Services/Activities/Domain/Domain.csproj", "Services/Activities/Domain/"]
COPY ["Services/Activities/Application/Application.csproj", "Services/Activities/Application/"]
COPY ["Services/Activities/Persistence/Persistence.csproj", "Services/Activities/Persistence/"]
RUN dotnet restore "Services/Activities/Accounts.Grpc/Accounts.Grpc.csproj"
COPY . .
WORKDIR "/src/Services/Activities/Accounts.Grpc"
RUN dotnet build "Accounts.Grpc.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Accounts.Grpc.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Accounts.Grpc.dll"]
