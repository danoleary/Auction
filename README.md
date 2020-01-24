# Auction

## Run application
```bash
dotnet run -p src/Auction.WebApp/Auction.WebApp.csproj

(may need to run 'npm install' from within 'src/Auction.WebApp/ClientApp' first)
```

## Run unit tests
```bash
dotnet test test/Auction.WebApp.UnitTests/Auction.WebApp.UnitTests.csproj 
```

## Run integration tests
```bash
dotnet test test/Auction.WebApp.IntegrationTests/Auction.WebApp.IntegrationTests.csproj 
```

## Next Steps

### Validation and error handling
* Front end validation
* Front end error handling
* Additional back end validation

### Testing
* Add additional integration tests
* Add tests for the read model

### Features
* Authentication
* Real time updates in front end about bids placed, new auctions and auctions ending using Signalr
* Configurable start and end times for auctions
* Add images for auctions

### Architecture
* Persistent storage for events (including handling simultaneous requests on the same aggregate)
* Publish events to a queue / message bus. This would allow events to be processed asynchronously
* Create AuctionEnded event. A separate service (i.e. a process manager) could then consume AuctionCreated, and schedule a job to fire in an EndAuction command at the correct time. This would then allow other services to consume AuctionEnded and send notifications, update the front end etc
* Separate front end from .net project - could use something like Gatsby to enable deployment to a CDN