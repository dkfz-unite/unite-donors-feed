# Donors Data Feed Service

## General
Donors data feed service provides the following functionality:
- [Donors data feed web API](Docs/api.md) - REST API for uploading clinical and treatment data to the portal (including input data validation).
- Donors data indexing service - background service responsible for donor-centric data index creation.

## Dependencies
- [SQL](https://github.com/dkfz-unite/unite-environment/tree/main/programs/postgresql) - SQL server with domain data and user identity data.
- [Elasticsearch](https://github.com/dkfz-unite/unite-environment/tree/main/programs/elasticsearch) - Elasticsearch server with indices of domain data.

## Access
Environment|Address|Port
-----------|-------|----
Host|http://localhost:5100|5100
Docker|http://feed.donors.unite.net|80

## Configuration
To configure the application, change environment variables in either docker or [launchSettings.json](Unite.Donors.Feed.Web/Properties/launchSettings.json) file (if running locally):

- `ASPNETCORE_ENVIRONMENT` - ASP.NET environment (`Release`).
- `UNITE_API_KEY` - API key for decription of JWT token and user authorization.
- `UNITE_ELASTIC_HOST` - Elasticsearch service host (`es.unite.net:9200`).
- `UNITE_ELASTIC_USER` - Elasticsearch service user.
- `UNITE_ELASTIC_PASSWORD` - Elasticsearch service password.
- `UNITE_SQL_HOST` - SQL server host (`sql.unite.net`).
- `UNITE_SQL_PORT` - SQL server port (`5432`).
- `UNITE_SQL_USER` - SQL server user.
- `UNITE_SQL_PASSWORD` - SQL server password.
- `UNITE_INDEXING_BUCKET_SIZE` - Indexing bucket size (`100`).

## Installation

### Docker Compose
The easiest way to install the application is to use docker-compose:
- Environment configuration and installation scripts: https://github.com/dkfz-unite/unite-environment
- Donors data feed service configuration and installation scripts: https://github.com/dkfz-unite/unite-environment/tree/main/applications/unite-donors-feed

### Docker
The image of the service is available in our [registry](https://github.com/dkfz-unite/unite-donors-feed/pkgs/container/unite-donors-feed) for the following environments:
- `linux/amd64`

[Dockerfile](Dockerfile) is used to build an image of the application.
To build an image run the following command:
```
docker build -t unite.donors.feed:latest .
```

All application components should run in the same docker network.
To create common docker network if not yet available run the following command:
```bash
docker network create unite
```

To run application in docker run the following command:
```bash
docker run \
--name unite.donors.feed \
--restart unless-stopped \
--net unite \
--net-alias feed.donors.unite.net \
-p 127.0.0.1:5100:80 \
-e ASPNETCORE_ENVIRONMENT=Release \
-e UNITE_API_KEY=[unite_api_key] \
-e UNITE_ELASTIC_HOST=http://es.unite.net:9200 \
-e UNITE_ELASTIC_USER=[elasticsearch_user] \
-e UNITE_ELASTIC_PASSWORD=[elasticsearch_password] \
-e UNITE_SQL_HOST=sql.unite.net \
-e UNITE_SQL_PORT=5432 \
-e UNITE_SQL_USER=[sql_user] \
-e UNITE_SQL_PASSWORD=[sql_password] \
-e UNITE_INDEXING_BUCKET_SIZE=100 \
-d \
unite.donors.feed:latest
```
