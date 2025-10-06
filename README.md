# RenartAPI

A RESTful API for Renart Jewelry products built with .NET 9.

## Features

- ? RESTful API endpoints for product management
- ? Dynamic price calculation based on real-time gold prices
- ? Product filtering by price and popularity
- ? CORS enabled for mobile/web clients
- ? Swagger documentation
- ? Docker container support

## API Endpoints

### Get Products
```
GET /api/products
```

#### Query Parameters
- `minPrice` (optional): Minimum price filter
- `maxPrice` (optional): Maximum price filter  
- `minPopularity` (optional): Minimum popularity score filter

#### Example Response
```json
[
  {
    "id": 1,
    "name": "Engagement Ring 1",
    "popularityScore": 0.85,
    "weight": 2.1,
    "price": 294.525,
    "popularityRating": 4.25,
    "images": {
      "yellow": "https://cdn.shopify.com/...",
      "rose": "https://cdn.shopify.com/...",
      "white": "https://cdn.shopify.com/..."
    }
  }
]
```

## Price Calculation

Dynamic pricing formula:
```
Price = (popularityScore + 1) * weight * goldPrice
```

- Gold price is fetched from real-time API: `https://api.metals.live/v1/spot/gold`
- Prices are calculated in USD
- Fallback cached price: $75.00/gram

## Running the Application

### Development
```bash
dotnet run
```

### Using Docker
```bash
docker build -t renartapi .
docker run -p 8080:8080 renartapi
```

### Accessing the API
- Development: `http://localhost:5223`
- Local Network: `http://192.168.0.14:5223`
- Container: `http://localhost:8080`
- Swagger UI: Access root URL when running in development

## Technology Stack

- .NET 9
- ASP.NET Core Web API
- Newtonsoft.Json
- Swagger/OpenAPI
- Docker Support

## Project Structure

```
Renart/
??? Controllers/
?   ??? ProductsController.cs
??? Models/
?   ??? Product.cs
??? Services/
?   ??? GoldPriceService.cs
??? wwwroot/
?   ??? data/
?       ??? products.json
??? Properties/
?   ??? launchSettings.json
??? Program.cs
??? Renart.csproj
??? README.md
```

## Configuration

### CORS Settings
The API is configured to accept requests from:
- localhost:3000-3002 (HTTP/HTTPS)
- 192.168.0.14:3000 (for mobile/tablet access)
- All origins in development mode

### Network Access
- Configured for both localhost and local network access
- Mobile/tablet compatible
- Firewall: Ensure ports 5223 and 7133 are open for local network access

## License

This project is for educational/demonstration purposes.