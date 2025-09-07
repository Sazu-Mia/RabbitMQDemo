# Employee Payroll Management System

A .NET 8 microservices example using **MassTransit** and **RabbitMQ**
with separate **Producer** and **Consumer** projects, and a shared
**message contract library**.

------------------------------------------------------------------------

## **Table of Contents**

1.  [Project Structure](#project-structure)
2.  [Technologies Used](#technologies-used)
3.  [Prerequisites](#prerequisites)
4.  [Setup Instructions](#setup-instructions)
5.  [Running the Consumer](#running-the-consumer)
6.  [Running the Producer](#running-the-producer)
7.  [How it Works](#how-it-works)
8.  [Key Points](#key-points)
9.  [Testing](#testing)
10. [License](#license)
11. [Author](#author)

------------------------------------------------------------------------

## **Project Structure**

    Solution
    │
    ├── Shared.Messages       # Shared DTOs / Contracts
    │   └── EmployeeUpdateDto.cs
    │
    ├── Producer              # Publishes EmployeeUpdate messages
    │
    └── Consumer              # Consumes EmployeeUpdate messages and updates database

------------------------------------------------------------------------

## **Technologies Used**

-   .NET 8
-   MassTransit
-   RabbitMQ
-   Entity Framework Core
-   C#

------------------------------------------------------------------------

## **Prerequisites**

-   .NET 8 SDK\
-   RabbitMQ running on localhost:5672 (Management UI:
    http://localhost:15672)\
-   SQL Server / Database for Consumer project

> Run RabbitMQ via Docker:

``` bash
docker run -d --hostname my-rabbit --name some-rabbit -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

------------------------------------------------------------------------

## **Setup Instructions**

1.  Clone the repository:

``` bash
git clone https://github.com/yourusername/employee-messaging-system.git
```

2.  Restore packages:

``` bash
dotnet restore
```

3.  Build **Shared.Messages**:

``` bash
cd Shared.Messages
dotnet build
```

4.  Reference **Shared.Messages** in Producer & Consumer:
    -   Right-click project → Add → Project Reference → select
        `Shared.Messages`
5.  Update RabbitMQ configuration in `appsettings.json` or `Program.cs`
    if needed.

------------------------------------------------------------------------

## **Running the Consumer**

``` bash
cd Consumer
dotnet run
```

-   Listens to RabbitMQ queue for `EmployeeUpdateDto` messages\
-   Inserts data into the configured database

------------------------------------------------------------------------

## **Running the Producer**

``` bash
cd Producer
dotnet run
```

-   Publishes `EmployeeUpdateDto` message\
-   Consumer will receive and process the message automatically

------------------------------------------------------------------------

## **How it Works**

1.  `Shared.Messages` defines the **EmployeeUpdateDto** contract.\
2.  Producer publishes the DTO to RabbitMQ using MassTransit.\
3.  Consumer subscribes and processes the message, updating the
    database.\
4.  Logging confirms successful processing.

------------------------------------------------------------------------

## **Key Points**

-   Shared DTO ensures **message type consistency** between projects.\
-   `cfg.ConfigureEndpoints(context)` automatically creates queues.\
-   Use **Publish** for pub/sub messages.

------------------------------------------------------------------------

## **Testing**

1.  Start **Consumer** project.\
2.  Run **Producer** project.\
3.  Verify Consumer logs or database entries.\
4.  Use RabbitMQ Management UI to monitor queues.

------------------------------------------------------------------------

## **License**

MIT License

------------------------------------------------------------------------

## **Author**

Mohammad Sazu Mia\
- Email: sazuhasan50@gmail.com\
- GitHub: <https://github.com/Sazu-Mia>\
- LinkedIn: <https://www.linkedin.com/in/mohammad-sazu-mia/>
