# Domain Driven Design ERP


## Overview

This repository is an ERP (Enterprise Resource Planning) Web API developed with Domain Driven Design principles and Clean Architecture in mind. It aims to provide a scalable, maintainable, and organized codebase for managing enterprise resources.

## Project Structure

### 1. Domain Layer

The [Domain](./Domain) layer houses the core domain entities, domain validations, value objects, and aggregate roots. It represents the heart of the application, encapsulating business logic and rules.

### 2. Application Layer

[Application](./Application) contains application-specific logic, including use cases, domain services, and application services. It acts as an intermediary between the domain layer and the infrastructure layer.

### 3. Infrastructure Layer

[Infrastructure](./Infrastructure) encompasses implementation details that are external to the application, such as data access, external services, and infrastructure concerns like logging and caching.

### 4. Identity Layer

The [Identity](./Identity) layer is responsible for user identity and authentication functionalities, managing user-related operations.

### 5. Persistence Layer

[Persistence](./Persistence) focuses on data storage and retrieval, utilizing technologies like Entity Framework Core for interacting with the database.

### 6. API Layer

[API](./API) serves as the entry point for the Web API application, handling incoming HTTP requests, and coordinating actions across different layers of the application.


## Features and Patterns

- **Domain Validations & Value Objects**: Ensuring data integrity and consistency within the domain layer.
- **Aggregate Root**: Defining aggregate roots to maintain consistency boundaries within the domain.
- **Domain Events**: Utilizing domain events for asynchronous and decoupled communication between domain entities.
- **CQRS (Command Query Responsibility Segregation)**: Separating command and query responsibilities for improved scalability and performance.
- **MediatR**: Implementing MediatR for mediation and handling of commands, queries, and domain events.
- **Fluent Validation**: Integrating Fluent Validation for robust input validation and error handling.
- **Resiliency with Polly**: Enhancing application resilience with Polly for transient fault handling and retries.
- **Idempotency with MediatR**: Ensuring idempotent command handling for reliability and consistency.
- **Repository Pattern**: Implementing the repository pattern for data access and abstraction over data storage.
- **Specification Design Pattern**: Utilizing the specification pattern for expressing query criteria in a composable way.
- **Distributed Caching (Redis)**: Leveraging Redis for distributed caching to improve application performance and scalability.
- **Logging with Serilog**: Incorporating Serilog for structured logging and log analysis.
- **Docker-Compose**: Containerizing the application components for easy deployment and scalability using Docker Compose.
- **Static Code Analysis**: Applying StyleCop.Analyzers and SonarAnalyzer.CSharp for enforcing code quality standards.
- **Authentication & Authorization**: Implementing authentication and authorization mechanisms for securing the application.
- **Unit Testing with XUnit, Moq, and FluentAssertions**: Writing unit tests for command handlers and application services using XUnit, Moq, and FluentAssertions.

## Usage

This ERP Web API provides a foundation for building enterprise resource planning applications with Domain Driven Design principles and Clean Architecture. Customize and extend it according to your specific business requirements.

## Learn More

- **Books**:
  - [Domain-Driven Design: Tackling Complexity in the Heart of Software](https://www.amazon.com/Domain-Driven-Design-Tackling-Complexity-Software/dp/0321125215) by Eric Evans
  - [Clean Architecture: A Craftsman's Guide to Software Structure and Design](https://www.amazon.com/Clean-Architecture-Craftsmans-Software-Structure/dp/0134494164) by Robert C. Martin

- **Articles and Blogs**:
  - [Exploring Clean Architecture in ASP.NET Core](https://docs.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures#clean-architecture) on Microsoft Docs
  - [The Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) by Robert C. Martin

- **Community and Forums**:
  - [Clean Coders](https://cleancoders.com/)
  - [Stack Overflow - Clean Architecture](https://stackoverflow.com/questions/tagged/clean-architecture)

### Docker

Run the application in Docker containers using Docker Compose:

1. **Build and run the Docker containers:**

    ```bash
    docker-compose up -d
    ```

   This command will build and start the containers defined in the `docker-compose.yml` file. It will create containers for the API, Redis, SQL Server, and Seq, and establish the necessary network connections between them.

2. **Access the Web API:**

   - The API will be accessible at [http://localhost:8080](http://localhost:8080).

3. **Access Seq for Logging:**

   - Seq, the structured log server, will be accessible at [http://localhost:5341](http://localhost:5341).

4. **Access Redis for Distributed Caching:**

   - Redis, the distributed cache server, will be accessible at [http://localhost:6379](http://localhost:6379).

5. **Access SQL Server:**

   - SQL Server will be available for database operations. You can connect to it using your preferred SQL client with the following details:
     - **Server:** `localhost,14330`
     - **Username:** `sa`
     - **Password:** `P@ssw0rd123`

6. **Stop the containers:**

    ```bash
    docker-compose down
    ```

   This command will stop and remove the containers while preserving data volumes.

**Related Repositories:**
- [Docker Documentation](https://docs.docker.com/)


## Testing

Testing is an essential aspect of software development, ensuring that the application behaves as expected and meets the specified requirements. In this project, we utilize unit tests to validate the behavior of various components, including command handlers, repositories, and domain services.

### Unit Testing

#### Purpose

Unit tests verify the correctness of individual units of code, such as methods and functions, in isolation from the rest of the application. These tests focus on specific scenarios and edge cases to ensure robustness and reliability.

#### Tools and Frameworks

- **xUnit**: We use xUnit as the unit testing framework for writing and executing unit tests in C#.
- **Moq**: Moq is a popular mocking library used to create mock objects and define their behavior during unit testing.
- **FluentAssertions**: FluentAssertions provides a fluent syntax for asserting the results of tests, making test assertions more readable and expressive.

## Usage

This Web API provides a basic structure adhering to Clean Architecture. Customize it based on your specific application needs.

## License

This project is licensed under the MIT License. See the [LICENSE](https://github.com/mohamedelareeg/DomainDrivenERP/blob/master/LICENSE) file for details.
