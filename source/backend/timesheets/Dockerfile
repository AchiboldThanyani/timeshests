# syntax=docker/dockerfile:1

# Set .NET version from csproj
ARG DOTNET_VERSION=8.0

# Builder stage
FROM mcr.microsoft.com/dotnet/sdk:${DOTNET_VERSION} AS builder
WORKDIR /app

# Copy everything and build
COPY --link . ./
RUN --mount=type=cache,target=/root/.nuget/packages \
    --mount=type=cache,target=/root/.cache/msbuild \
    dotnet publish timesheets.csproj -c Release -o /app/publish

# Final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:${DOTNET_VERSION} AS final
WORKDIR /app

# Create non-root user
RUN addgroup --system appgroup && adduser --system --ingroup appgroup appuser
USER appuser

# Copy published output from builder
COPY --link --from=builder /app/publish .

# Expose default ASP.NET port
EXPOSE 80

# Entry point
ENTRYPOINT ["dotnet", "timesheets.dll"]
