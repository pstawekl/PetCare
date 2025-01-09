# PetCare - Backend

## Opis aplikacji

Aplikacja odpowiada za stronę serwerową systemu PetCare. 
PetCare to system wspomagający właścicieli zwierząt domowych w organizacji opieki nad nimi. 
Umożliwia planowanie wizyt u weterynarza, przypomnienia o szczepieniach, 
monitorowanie diety i aktywności zwierząt oraz (w przyszłości) integrację z zewnętrznymi systemami weterynaryjnymi.

## Kontrolery

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
## Klasy w aplikacji

### Auth

```plaintext
LoginDto
RegisterDto
RefreshTokenDto
```

### Pets

```plaintext
PetCreateDto
```

### Profile

```plaintext
UpdateProfileDto
```

### Reminders

```plaintext
ReminderCreateDto
```

### Users

```plaintext
UserCreateDto
```

### Visits

```plaintext
VisitCreateDto
```
