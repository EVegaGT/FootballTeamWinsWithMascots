#Football Team Wins With Mascots

This repository contains a full-stack solution built with **ASP.NET Core 8** (Backend API) and **React + TypeScript** (Frontend).  
The application allows users to search football teams and view their records dynamically, following **Domain-Driven Design (DDD)** and **CQRS** principles using **MediatR**.

---

## Project Overview

- **Backend:** ASP.NET Core 8 Web API  
- **Frontend:** React + TypeScript (Vite + Material UI)  
- **Database:** SQLite with Entity Framework Core  
- **Architecture:** DDD + CQRS with MediatR  
- **Auto-generated API client:** swagger-typescript-api  

---

## Running the Project (Visual Studio)

The solution includes a preconfigured **Visual Studio launch profile** that runs both the **API** and **React WebApp** automatically.

### To run:
1. Open the solution in **Visual Studio 2022 or later**.
2. Set the startup profile to **“Multiple startup projects”**.
3. Make sure both are marked as:
   - **FootballTeamWinsWithMascots.Api** → *Start*
   - **FootballTeamWinsWithMascots.WebApp** → *Start without debugging*
4. Press **F5** (or click “Run”).

Visual Studio will:
- Launch the API on **https://localhost:7138**
- Launch the React app on **http://localhost:5138**

The frontend is configured with a **Vite proxy**, so all `/api/...` calls are redirected to the backend automatically (no CORS configuration required).

---

## Alternative Manual Run

If the Visual Studio profile fails for any reason, you can start both applications manually.

###  Start the API
```bash
cd FootballTeamWinsWithMascots.Api
dotnet run

Runs on:
https://localhost:7138

###  Start the WebApp
```bash
cd FootballTeamWinsWithMascots.WebApp
npm install
npm run dev

