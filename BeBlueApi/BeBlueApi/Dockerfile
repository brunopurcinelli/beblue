FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["BeBlueApi.csproj", "BeBlueApi/"]
RUN dotnet restore "BeBlueApi/BeBlueApi.csproj"
COPY . .
WORKDIR "/src/BeBlueApi"
RUN dotnet build "BeBlueApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BeBlueApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BeBlueApi.dll"]