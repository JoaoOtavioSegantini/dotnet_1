## Instalation

Build sql server image:

```zsh
    docker build -t sqlserver .  
```

Init sql server:

```zsh
    docker run --env-file .env -it --rm --name sqlserver -p 1433:1433 sqlserver
```

Install dependencies:

```c 
   dotnet restore
``` 

Run migrations:

```c
   dotnet ef database update
```

Run project:

```c
   dotnet run
```
