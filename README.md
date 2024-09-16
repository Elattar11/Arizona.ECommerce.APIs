<h1>Arizona E-Commerce Web API</h1>
<h2>Overview</h2>
<p>This project is designed using the Onion Architecture Pattern to ensure a scalable, maintainable, and loosely coupled solution. It is structured to follow the principles of dependency injection and clean architecture, keeping the core of the system independent from external concerns like data access, services, and UI.</p>

<p>The system uses SQL Server with Entity Framework Core (Code First) for data management, and integrates several modern design patterns and technologies to provide a robust solution. Below is a detailed overview of the key features implemented in this project.</p>

<h2>Key Features</h2>
<h3>1. Onion Architecture Pattern</h3>
<p>The system follows the Onion Architecture, promoting dependency inversion and a clear separation of concerns. Core business logic is isolated from external dependencies.</p>

<h3>2. Dependency Injection (DI)</h3>
<p>The project is developed against interfaces, not concrete classes, using the Dependency Injection Technique. This ensures flexibility and testability by decoupling implementations from abstractions.</p>

<h3>3. Entity Framework Core & LINQ</h3>
<p>SQL Server is used as the database, and Entity Framework Core (Code First) is implemented for data persistence. All database interactions are handled using LINQ operators.</p>

<h3>4. Data Transfer Objects (DTOs) & AutoMapper</h3>
<p>The system leverages DTOs to transfer data between layers, ensuring separation of concerns. Mapping between domain models and DTOs is facilitated by the AutoMapper package.</p>

<h3>5. Design Patterns</h3>
<p><strong>Unit of Work: </strong> Manages transactions and ensures that a group of operations are executed in a single transaction.</p>
<p><strong>Specification Pattern: </strong> Encapsulates query logic into reusable and composable specifications.</p>
<p><strong>Generic Repository: </strong> Provides a generic interface for data access operations, abstracting data handling logic from the service layer.</p>

<h3>6. Security & Authentication</h3>
<p>Implemented Identity for user authentication and authorization. Identity is managed in a separate DbContext to maintain clean separation of concerns.</p>
<p><strong>JWT Tokens: </strong> Token-based authentication is implemented using the JWT Package, enabling secure API access.</p>

<h3>7. Redis Integration</h3>
<p><Strong>Basket Module: </Strong> Implemented using Redis as an in-memory database to handle shopping basket operations.</p>
<p><strong>Caching Service: </strong> Used Redis for efficient caching of frequently accessed data to improve system performance.</p>

<h3>8. Payment Service (Stripe)</h3>
<p>Integrated Stripe for payment processing. The payment flow is completed using a Webhook Endpoint that listens for Stripe events, ensuring real-time payment status updates.</p>

<h3>9. Admin Dashboard</h3>
<p>Built an Admin Dashboard using ASP.NET MVC that provides administrative capabilities, including user role management and CRUD operations on the database.</p>

<h3>10. API Documentation & Testing</h3>
<p>All endpoints are documented and tested using the Postman tool, ensuring clear and comprehensive API documentation for client-side consumption.</p>

<h3>11. Client-Side Implementation (Angular)</h3>
<p>A client-side project built with Angular is provided, which consumes the backend APIs and delivers a responsive user experience.</p>

<h3>12. Deployment</h3>
<p>The APIs and Angular project are deployed locally using IIS (Internet Information Services) for hosting and serving the applications.</p>

<h2>Tools</h2>
<ul>
  <li>.NET 8.0 SDK</li>
  <li>SQL Server</li>
  <li>Redis Server</li>
  <li>Angular CLI</li>
  <li>Stripe Account for Payments</li>
</ul>

<h3>LinkedIn Post:</h3>
<p align="left">
<a href="https://www.linkedin.com/posts/mohamed-al-attar-13765918b_dear-all-i-am-so-happy-to-share-my-latest-activity-7213912274422362113-tQbL?utm_source=share&utm_medium=member_desktop" target="blank"><img align="center" src="https://raw.githubusercontent.com/rahuldkjain/github-profile-readme-generator/master/src/images/icons/Social/linked-in-alt.svg" alt="https://www.linkedin.com/in/mohamed-al-attar-13765918b/" height="30" width="40" /></a>
</p>
