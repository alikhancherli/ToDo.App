
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["ToDo.App.Api/ToDo.App.Api.csproj", "ToDo.App.Api/"]
COPY ["ToDo.App.Infrastructure/ToDo.App.Infrastructure.csproj", "ToDo.App.Infrastructure/"]
COPY ["ToDo.App.Application/ToDo.App.Application.csproj", "ToDo.App.Application/"]
COPY ["ToDo.App.Domain/ToDo.App.Domain.csproj", "ToDo.App.Domain/"]
COPY ["ToDo.App.Shared/ToDo.App.Shared.csproj", "ToDo.App.Shared/"]
COPY ["ToDo.App.Test/ToDo.App.Test.csproj", "./test/ToDo.App.Test/"]

RUN dotnet restore "ToDo.App.Api/ToDo.App.Api.csproj"
COPY . .
WORKDIR "/src/ToDo.App.Api"
RUN dotnet build "ToDo.App.Api.csproj" -c Release -o /app/build


 WORKDIR "/src/test/ToDo.App.Test"
 RUN dotnet test --logger:trx

WORKDIR "/src/ToDo.App.Api"
FROM build AS publish
RUN dotnet publish "ToDo.App.Api.csproj" -c Release -o /app/publish


FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5125
EXPOSE 7112

ENTRYPOINT ["dotnet", "ToDo.App.Api.dll"]