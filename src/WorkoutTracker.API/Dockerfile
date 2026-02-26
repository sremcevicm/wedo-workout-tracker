FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY ["src/WorkoutTracker.API/WorkoutTracker.API.csproj", "src/WorkoutTracker.API/"]
COPY ["src/WorkoutTracker.Application/WorkoutTracker.Application.csproj", "src/WorkoutTracker.Application/"]
COPY ["src/WorkoutTracker.Infrastructure/WorkoutTracker.Infrastructure.csproj", "src/WorkoutTracker.Infrastructure/"]
COPY ["src/WorkoutTracker.Domain/WorkoutTracker.Domain.csproj", "src/WorkoutTracker.Domain/"]
RUN dotnet restore "src/WorkoutTracker.API/WorkoutTracker.API.csproj"

COPY . .
RUN dotnet publish "src/WorkoutTracker.API/WorkoutTracker.API.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8080
ENTRYPOINT ["dotnet", "WorkoutTracker.API.dll"]
