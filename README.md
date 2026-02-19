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
