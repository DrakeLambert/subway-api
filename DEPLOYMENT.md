# Subway API Docker Deployment

1. `docker build -t subway-api .`
2. `docker run -it --rm -p 5001:80 --name subway-api subway-api`
3. Browse `http://localhost:5001/swagger`
