FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["BeBlueApi.WebApi/BeBlueApi.WebApi.csproj", "BeBlueApi.WebApi/"]
RUN dotnet restore "BeBlueApi.WebApi/BeBlueApi.WebApi.csproj"
COPY . .
WORKDIR "/src/BeBlueApi.WebApi"
RUN dotnet build "BeBlueApi.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "BeBlueApi.WebApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "BeBlueApi.WebApi.dll"]