# BlockchainSQL

BlockchainSQL gives you the blockchain as an SQL database. Easily build your analytics and BI platform without ever worrying about protocols again.

This repository contains the source code for the BlockchainSQL application.

## Why BlockchainSQL?

Blockchain data is stored in complex binary formats and requires specialized knowledge to parse and query. BlockchainSQL solves this by:

- **SQL Access**: Query blockchain data using standard SQL syntax you already know
- **Real-Time Sync**: Keep your database synchronized with the blockchain as new blocks arrive
- **Analytics Ready**: Build dashboards, reports, and BI solutions on top of relational data
- **Protocol Abstraction**: No need to understand low-level blockchain protocols

## Features

Based on the product description on [https://sphere10.com/products/blockchainsql](https://sphere10.com/products/blockchainsql):

- **Blockchain as SQL Database**: Access all blockchain data through standard SQL queries
- **Block Explorer**: Built-in web interface for exploring blocks, transactions, and addresses
- **Custom SQL Queries**: Write and save custom queries for your specific analytics needs
- **Real-Time Synchronization**: Automatically sync with the blockchain as new blocks are mined
- **Multiple Database Support**: Works with SQL Server and other database backends
- **Web Interface**: Browser-based UI for querying and exploring blockchain data
- **Address Tracking**: Track balances and transaction history for any address
- **Script Analysis**: Analyze transaction scripts and smart contract data

## Screenshots

### GUI Scanner

[![BlockchainSQL Scanner UI](https://sphere10.com/files/037f687f-8e3c-459b-ad40-f69ed166a1fa/Untitled.png)](https://sphere10.com/products/blockchainsql)

### Web Explorer

[![BlockchainSQL Web Explorer](https://sphere10.com/files/78f27e7c-48fd-40d2-be9f-922ecf632f68/Untitled.png)](https://sphere10.com/products/blockchainsql)

## Documentation & Resources

Sphere10 provides additional end-user documentation and resources:

- [Product Page](https://sphere10.com/products/blockchainsql)
- [BlockchainSQL Manual](https://sphere10.com/products/blockchainsql/manual)

## Build From Source

### Prerequisites

- Windows (Windows Forms UI components require Windows)
- **.NET SDK 8.0+**
- Visual Studio 2022+ (recommended)
- SQL Server (for the blockchain database)

> **Note:** This repository includes projects targeting **.NET 8**. Install the .NET 8 SDK to build the full solution.

### Build (CLI)

From the repository root:

```bash
# Restore dependencies
dotnet restore

# Debug build
dotnet build -c Debug

# Release build
dotnet build -c Release
```

### Build (Visual Studio)

1. Open the solution (`BlockchainSQL.sln`) in Visual Studio.
2. Set `BlockchainSQL.Server` as the startup project.
3. Build with **Build > Build Solution**.
4. Run with **Debug > Start Without Debugging**.

## Project Structure

The solution contains the following projects:

| Project | Description |
|---------|-------------|
| **BlockchainSQL.Server** | Main Windows application with GUI and service |
| **BlockchainSQL.Web** | ASP.NET Core web interface for querying and exploring |
| **BlockchainSQL.Processing** | Blockchain parsing and synchronization logic |
| **BlockchainSQL.DataAccess** | Database access layer |
| **BlockchainSQL.DataAccess.NHibernate** | NHibernate ORM mappings |
| **BlockchainSQL.DataObjects** | Domain objects and data models |
| **BlockchainSQL.Web.DataAccess** | Web-specific data access |
| **BlockchainSQL.Web.DataObjects** | Web-specific data models |
| **BlockchainSQL.NUnit** | Unit tests |

## Architecture

BlockchainSQL consists of several components:

- **Block Stream**: Reads blockchain data from block files or network nodes
- **Block Processor**: Parses blocks and transactions into relational format
- **Database Layer**: Stores processed data in SQL Server
- **Web Interface**: Provides query and exploration capabilities
- **Service**: Runs as a Windows service for continuous synchronization

## Troubleshooting

- **Database connection errors**: Verify SQL Server is running and connection string is correct.
- **Sync issues**: Check that block files are accessible and not corrupted.
- **Build failures**: Verify SDK install with `dotnet --info`, then run `dotnet restore` and `dotnet build`.
- **Performance issues**: Ensure database indexes are enabled after initial sync.

## Contributing

Contributions are welcome!

- Keep changes small and focused.
- Follow formatting rules from `.editorconfig`.
- Add/update tests where applicable.

See [CONTRIBUTING.md](CONTRIBUTING.md) for more details.

## License

This project is licensed under the **GNU GPL v3.0** (or later).

- See [`LICENSE`](LICENSE)
- Copyright details: [`COPYRIGHT`](COPYRIGHT)

## Credits

**Author:** Herman Schoenfeld (<***REDACTED_EMAIL***>)

**Website:** [https://sphere10.com/products/localnotion](https://sphere10.com/products/localnotion)

**Copyright:** © Herman Schoenfeld 2018 - Present. All rights reserved.