# Based on microsofts example dockerfile @ https://learn.microsoft.com/en-us/dotnet/core/docker/build-container?tabs=windows&pivots=dotnet-10-0
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /App

# Copy csproj
COPY communication.csproj ./
# Restore as distinct layers
RUN dotnet restore communication.csproj
# Copy everything else
COPY . ./
# Build and publish a release
RUN dotnet publish communication.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /App
EXPOSE 5139
COPY --from=build /App/out .
ENV ASPNETCORE_URLS=http://+:5139
ENTRYPOINT ["dotnet", "communication.dll"]