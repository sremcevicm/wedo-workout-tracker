WorkoutTracker

WorkoutTracker is a full-stack web application for tracking gym workouts. Users can log their workouts, track progress over time, and get a weekly activity overview for a selected month.

Tech Stack

Backend
- .NET 10 — Web API
- Clean Architecture (Domain / Application / Infrastructure / API)
- CQRS with MediatR
- Entity Framework Core + SQLite
- JWT authentication
- FluentValidation

Frontend
- React 19 (Vite)
- React Router
- Axios

Features

- Authentication — user registration and login with JWT token
- Workout logging — exercise type, duration, calories, workout intensity (1–10), fatigue (1–10), date and notes
- Exercise types — cardio, strength training, flexibility
- Monthly progress — weekly overview for a selected month: total duration, number of workouts, average intensity and fatigue

POKRETANJE -- DOCKER --

Potrebno: Docker Desktop

1. Klonirati repozitorijum
git clone "https://github.com/sremcevicm/wedo-workout-tracker.git"
cd wedo-workout-tracker

2. Kreirati .env fajl
copy .env.example .env

Otvoriti .env i postaviti JWT key:
JWT_KEY=your-secret-key-minimum-32-characters-long

3. Pokrenuti
docker compose up --build

Aplikacija je dostupna na http://localhost




POKRETANJE -- LOKALNO --

Potrebno: .NET 10 SDK, Node.js 22

1. Backend

cd src/WorkoutTracker.API

dotnet user-secrets set "Jwt:Key" "your-secret-key-minimum-32-characters-long"

dotnet run

API radi na http://localhost:5223

2. Frontend

cd src/WorkoutTracker.Web

npm install

npm run dev

App radi na http://localhost:5173