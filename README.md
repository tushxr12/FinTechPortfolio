# FinTechPortfolio : Fintech Valuation Engine

A high-performance **Fintech Backend Engine** designed to track investment portfolios, fetch real-time market data, and provide instant profit/loss analytics. Built with a focus on **Clean Architecture** and scalability using .NET 8.

---

## 🚀 Overview

This project serves as the core engine for a portfolio management system. It automates the heavy lifting of financial calculations by integrating with external market providers and maintaining a robust local data store for transaction history.

### Key Features
* **Real-time Valuation Engine:** Automatically calculates Total Portfolio Value and unrealized Profit/Loss.
* **External API Integration:** Seamlessly fetches live stock prices via Alpha Vantage.
* **Performance Optimized:** Implements **In-Memory Caching** to minimize external API latency and manage rate limits effectively.
* **Persistence:** Solid data management using Entity Framework Core with SQLite.
* **Developer Friendly:** Fully documented API endpoints via Swagger/OpenAPI.

## 🛠 Tech Stack

* **Framework:** .NET 8 (Web API)
* **Architecture:** Clean Architecture (Domain-Driven approach)
* **Database:** SQLite / Entity Framework Core
* **Patterns:** Repository Pattern, Dependency Injection, DTO Mapping
* **Third-Party APIs:** Alpha Vantage REST API

## Architecture

The project follows **Clean Architecture** principles to ensure the business logic remains independent of external frameworks:

1.  **Domain:** Entities and core logic (The "Engine").
2.  **Application:** Interfaces, DTOs, and Business logic/Services.
3.  **Infrastructure:** Data persistence and external API clients.
4.  **API:** Controllers and Middleware (The Entry Point).

## Getting Started

### Prerequisites
* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* An API Key from [Alpha Vantage](https://www.alphavantage.co/support/#api-key)

### Installation

1.  **Clone the repository:**
    ```bash
    git clone https://github.com/tushxr12/FinTechPortfolio
    ```

2.  **Navigate to the API project directory:**
    ```bash
    cd Portfolio.API
    ```

3.  **Configure API Keys:**
    Update your `appsettings.json` with your Alpha Vantage API Key:
    ```json
    "AlphaVantage": {
      "ApiKey": "YOUR_KEY_HERE",
      "BaseUrl": "AlphaVantageBaseUrl"
    }
    ```

4.  **Run the application:**
    ```bash
    dotnet run
    ```
