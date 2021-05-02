FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["ElevatorApp/ElevatorApp.csproj", "./"]
RUN dotnet restore "./ElevatorApp.csproj"
COPY ./ElevatorApp .
RUN dotnet build "ElevatorApp.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ElevatorApp.csproj" -c Release -o /app/publish

FROM base AS final
ENV WAIT_SCRIPT_VERSION=2.7.3
ADD https://github.com/ufoscout/docker-compose-wait/releases/download/${WAIT_SCRIPT_VERSION}/wait /wait
RUN chmod +x /wait
WORKDIR /app
COPY --from=publish /app/publish .
CMD /wait && dotnet ElevatorApp.dll
