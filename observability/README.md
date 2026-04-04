# Observability

The stack includes:

- Prometheus at `http://localhost:9090`
- Grafana at `http://localhost:3000`
- ASP.NET metrics at `http://localhost:8080/metrics`
- pre-provisioned dashboard: `MvpShop / MvpShop Overview`

## Start

```bash
docker compose up -d --build
```

## Grafana login

Defaults:

- user: `admin`
- password: `admin`

Override with:

- `GRAFANA_ADMIN_USER`
- `GRAFANA_ADMIN_PASSWORD`

## Scrape target

Prometheus scrapes the `web` container on:

```text
http://web:8080/metrics
```
