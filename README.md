# PetCare - Backend

## Description

The application manages the server-side of the PetCare system.  
PetCare is a system designed to assist pet owners in organizing care for their pets.  
It enables scheduling veterinary visits, setting reminders for vaccinations, monitoring pets' diet and activity, and (in the future) integration with external veterinary systems.

### Warning
Application requires appsettings.json to run

## Routes

### Auth

```http
POST /api/Auth/login
POST /api/Auth/register
POST /api/Auth/refresh
POST /api/Auth/verify
```

### Pets

```http
GET /api/Pets
POST /api/Pets
PUT /api/Pets/{id}
DELETE /api/Pets/{id}
```

### Profile

```http
POST /api/Profile/{id}
PUT /api/Profile/{id}
```

### Reminders

```http
GET /api/Reminders
POST /api/Reminders
PUT /api/Reminders/{id}
DELETE /api/Reminders/{id}
```

### Users

```http
GET /api/Users
POST /api/Users
PUT /api/Users/{id}
DELETE /api/Users/{id}
```

### Visits

```http
GET /api/Visits
POST /api/Visits
PUT /api/Visits?id=<query_parameter>
DELETE /api/Visits/{id}
```
