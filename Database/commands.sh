docker build -t my-postgres-db ./
docker run -d --name my-postgresdb-container -p 8001:5432 my-postgres-db