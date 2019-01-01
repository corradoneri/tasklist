FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["TaskApi/TaskApi.csproj", "TaskApi/"]
RUN dotnet restore "TaskApi/TaskApi.csproj"
COPY . .
WORKDIR "/src/TaskApi"
RUN dotnet build "TaskApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TaskApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TaskApi.dll"]