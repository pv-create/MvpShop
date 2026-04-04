# Observability

The stack includes:

- Prometheus at `http://localhost:9090`
- Grafana at `http://localhost:3000`
- Loki at `http://localhost:3100`
- ASP.NET metrics at `http://localhost:8080/metrics`

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

## Logs in Grafana

Add a Loki data source manually in Grafana:

- Type: `Loki`
- URL: `http://loki:3100`

Then you can query logs, for example:

```text
{container="mvp-shop-web"}
```
