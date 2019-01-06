FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 27017

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["TaskApi/TaskList.TaskApi.csproj", "TaskApi/"]
RUN dotnet restore "TaskApi/TaskList.TaskApi.csproj"
COPY . .
WORKDIR "/src/TaskApi"
RUN dotnet build "TaskList.TaskApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "TaskList.TaskApi.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TaskList.TaskApi.dll"]