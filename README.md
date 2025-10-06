# RenartAPI

A RESTful API for Renart Jewelry products built with .NET 9.

[For testing — RenartAPI](https://renart-tahafurkanteke-ergbd4drcjaja7f5.francecentral-01.azurewebsites.net/index.html)

## Features

-  RESTful API endpoints for product management
-  Dynamic price calculation based on real-time gold prices
-  Product filtering by price and popularity
-  CORS enabled for mobile/web clients
-  Swagger documentation
-  Docker container support

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
- Fallback cached price: $125.00/gram

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
- All origins in development mode

### Network Access
- Configured for both localhost and local network access
- Mobile/tablet compatible
- Firewall: Ensure ports 5223 and 7133 are open for local network access

---

# RenartAPI

.NET 9 ile geliştirilmiş Renart Jewelry ürünleri için RESTful API.

[Test için — RenartAPI](https://renart-tahafurkanteke-ergbd4drcjaja7f5.francecentral-01.azurewebsites.net/index.html)

## Özellikler

-  Ürün yönetimi için RESTful API uç noktaları
-  Gerçek zamanlı altın fiyatlarına göre dinamik fiyat hesaplama
-  Fiyata ve popülerliğe göre ürün filtreleme
-  Mobil/web istemciler için CORS etkin
-  Swagger dokümantasyonu
-  Docker konteyner desteği

## API Uç Noktaları

### Ürünleri Getir
```
GET /api/products
```

#### Sorgu Parametreleri
- `minPrice` (isteğe bağlı): Minimum fiyat filtresi
- `maxPrice` (isteğe bağlı): Maksimum fiyat filtresi  
- `minPopularity` (isteğe bağlı): Minimum popülerlik puanı filtresi

#### Örnek Yanıt
```json
[
  {
    "id": 1,
    "name": "Nişan Yüzüğü 1",
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

## Fiyat Hesaplama

Dinamik fiyatlandırma formülü:
```
Fiyat = (popularityScore + 1) * ağırlık * altınFiyatı
```

- Altın fiyatı gerçek zamanlı API'den alınır: `https://api.metals.live/v1/spot/gold`
- Fiyatlar USD cinsinden hesaplanır
- Yedeklenmiş önbellek fiyatı: $125.00/gram

## Uygulamayı Çalıştırma

### Geliştirme
```bash
dotnet run
```

### Docker ile
```bash
docker build -t renartapi .
docker run -p 8080:8080 renartapi
```

### API'ye Erişim
- Geliştirme: `http://localhost:5223`
- Yerel Ağ: `http://192.168.0.14:5223`
- Konteyner: `http://localhost:8080`
- Swagger UI: Geliştirme modunda kök URL üzerinden erişilir

## Teknoloji Yığını

- .NET 9
- ASP.NET Core Web API
- Newtonsoft.Json
- Swagger/OpenAPI
- Docker Desteği

## Proje Yapısı

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

## Yapılandırma

### CORS Ayarları
API aşağıdaki adreslerden gelen isteklere açıktır:
- localhost:3000-3002 (HTTP/HTTPS)
- Geliştirme modunda tüm kaynaklara açık

### Ağ Erişimi
- Hem localhost hem yerel ağ erişimi için yapılandırıldı
- Mobil/tablet uyumlu
- Güvenlik duvarı: Yerel ağ erişimi için 5223 ve 7133 portlarının açık olduğundan emin olun
