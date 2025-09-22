# timeshests
 Time Sheets Management

## Running with Docker

This project provides a Docker setup for the backend timesheets service using .NET 8.0. The default ASP.NET port 80 is exposed.

### Requirements
- Docker and Docker Compose installed
- No required environment variables by default (add a `.env` file if needed)
- .NET version: 8.0 (as specified in the Dockerfile)

### Build and Run

From the project root, run:

```sh
docker compose up --build
```

This will build and start the `csharp-timesheets` service, exposing it on port 80.

### Configuration
- The backend service is available at [http://localhost:80](http://localhost:80)
- If you need to add a database (e.g., PostgreSQL), uncomment and configure the relevant section in `docker-compose.yml` and update your connection strings accordingly.
- To use environment variables, create a `.env` file in `./source/backend/timesheets` and uncomment the `env_file` line in the compose file.

### Ports
- `csharp-timesheets`: 80 (default ASP.NET port)

