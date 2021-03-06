
 # BUILDING CATALOG USRV

* ASP.NET core WEB API application
* REST API principles, CRUD operation
* MONGODB noSql database connector on docker
* N-Layer implementation
* Repository Design Pattern
* Swagger Open API implementation
* DockerFile implementation

>CATALOG API
 * `GET /api/v1/Catalog`
 * `POST /api/v1/Catalog`
 * `PUT /api/v1/Catalog`
 * `GET /api/v1/Catalog/{id}`
 * `DELETE /api/v1/Catalog/{id}`
 * `GET /api/v1/Catalog/GetProductByCategory/{category}`

 ### Installing MongoDb Dockerhub

 * `docker pull mongo`
 * `docker run -d -p 27017:27017 --name aspnetrun-mongo mongo`
 * `docker exec -it aspnetrun-mongo /bin/bash` | enter to mongo

### Installing Swagger - NuGet
* Swashbuckle.AspNetCore 6.0.7

## Docker Operation for Catalog Microservices
* Dockerfile creation
* Dockerfile commands
* Docker-compose file creation
* Docker-compose file commands


### Mongo CLI **commands on docker**

Command | Explanation
-----------|-----------
docker exec -it aspnetrun-mongo /bin/bash |  execute (-it) internal terminal for this image and open the terminal, and start whit the bash commands
`...:/# mongo`| inside mongo `root@6c48eb827707:/# mongo`, execute mongo commands in her
`> show dbs` | list all database in mongo
`> use CatalogoDb` | create a new DB
`> db.createCollection(`Productos`)` | create a new DB
`> db.Products.insertMany(...)` | inside folder PRACTICA, there is a file with it

______________________________________

# BUILDING Basket USRV

* ASP.NET core WEB API application
* REST API principles, CRUD operation
* REDIS DB noSql database connector on docker
* N-Layer implementation
* Repository Design Pattern
* Swagger Open API implementation
* DockerFile implementation

### REDIS - [Docker hub](https://hub.docker.com/_/redis)
* `docker pull redis`
* `docker run -p 6379:6379 --name aspnetrun-redis -d redis` 
* `docker exec -it aspnetrun-redis /bin/bash`
* `regis-cli` | after execute redis
* `set key algunaClave` | generate a new key
* `get key` | get a key

### Redis NuGet
* StackExchange.Redis

### Run Application on Docker 

* docker stop f4 | stop container with CONTAINER_ID ex: 4e6e1481e4ca `> docker stop 4e`

______________________________________


![Tux, the Linux mascot](https://www.rabbitmq.com/img/logo-rabbitmq.svg)



______________________________________
# Run final application

# `docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d`
* Cone the repository
* Run command on top of **docker-compose.yml** files; 
`docker-compose -f docker-compose.yml   -f docker-compose.override.yml up -d`

* RabbitMq -> `http://localhost:15672`
* Catalog API -> http://localhost:8000/swagger/index.html
    * `docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d`
* Basket API -> http://localhost:8001/swagger/index.html
* Order API -> http://localhost:8002/swagger/index.html
* API Gateway -> http://localhost:7000/orders?username+swn
* Shopping Web UI -> http//localhost:8003



## Docker commands

* `docker -version `
* `docker pull <imane name>`
* `docker run -it -d <image name>` | this command list all the locally stored docker image 
* `docker images` | this command lists all the locally stored docker images 
* `docker rm <container id>` | this command is used to delete a stopped container
* `docker rmi <image-id>` | this command is used to delete an image for local storage
* `docker ps` | this command is used to ist the running containers
* `docker ps -a` | this command is used to list the running containers
* `docker exec -it <container id> bash ` | this command is used to access the running container
* `docker start <container id>`
* `docker stop <container id>`
* `docker restart <container id>`
* `docker info `
* `docker logs <container id> `

* `docker volume create`
* `dockervolume ls `
* `docker build <path to docker file>`  | this command is used to build an image from a specified docker file
* `docker compose up` | this command run multiple container

> Example Bonus Example docker hub pull
* `docker run -d --hostname swn-rabbit --name swn-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3-management`

> Single container
For aspNetCore app after adding docker file -- for single container add container add docker file build and run == create new container
    `$ docker build -t aspnetapp`
    `$ docker run -d -p 8080:80 --name myapp aspnetapp`

> Multiple Container - **docker-compose.yml**
    docker-compose up

`docker-compose -f docker-compose.yml   -f docker-compose.override.yml up -d` 

## Redis in Use
> We can say that REDIS is an Open Source NoSql database and that data origin holds on the memory. As mentioned in its own documentation, that this is not just a simple key-value server. It is more than the key-value directory server
> the key-value directory server because it has a lot of abilities like storing high-level data structure could be the list, map, sett heap and the json objects and redis is very good performance and in simplicity and automatic manipulation data structures
> when we compare with the traditional relational database is it provider us a god advantage for using redis, also we can say that redis is using in+memory database, which means that redis store your data on the ram and you can certain the same into your disk
> it has certain list so you can ensure that data consistency under advantage offer, redis is keeping your data in memory
> 
> It is very important because it can reduce to read and write operation up to milliseconds, if you compare it to performance, its normal relational databases for this property, it's really good advantage for redis
> When you use the right data structure, you can reduce your costs accessing data by up to 10%














