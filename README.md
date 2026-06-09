# exampleProject - ASP.NET Core MVC Admin Panel

Bu proje; katmanlı mimari (N-Tier Architecture), Generic Repository ve esnek bir dinamik yetkilendirme (Permission-Based Authorization) altyapısı kullanılarak geliştirilmiş modern bir yönetim paneli uygulamasıdır. Arayüz giydirmesi olarak **AdminLTE 3** şablonu entegre edilmiştir.

---

## 🚀 Öne Çıkan Özellikler

* **Çok Katmanlı Mimari (N-Tier):** Core, Data ve Web katmanları arasında tamamen gevşek bağımlı (Loosely Coupled) yapı.
* **Generic Repository Pattern:** Temel CRUD işlemleri için kod tekrarını önleyen merkezi veri erişim katmanı.
* **Permission-Based Authorization:** Kod içerisine statik roller (`[Authorize(Roles="Admin")]`) gömmek yerine, tamamen veritabanı kontrollü, talebe bağlı (`Policy-Based / Claim-Based`) dinamik yetkilendirme motoru.
* **Dinamik Policy Provider:** Her yeni yetki için `Program.cs` dosyasına manuel kural ekleme zahmetini ortadan kaldıran otomatik politika sağlayıcı sistemi.
* **Data Seeding:** Uygulama ilk kez ayağa kalktığında veritabanını varsayılan roller (`SuperAdmin`), kullanıcılar ve yetkilerle otomatik olarak besleyen mekanizma.

---

## 🏗️ Proje Mimarisi

Proje 3 ana katmandan oluşmaktadır:

1.  **`exampleProject.Core`**
    * Entity (Varlık) tanımlamaları (`Category`, `Identity` modelleri).
    * Interface (Arayüz) sözleşmeleri (`IGenericRepository`).
    * Sistem genelindeki sabitler ve yetki tanımlamaları (`PermissionConsts`).
    * Yetkilendirme gereksinimleri (`PermissionRequirement`).

2.  **`exampleProject.Data`**
    * Veritabanı bağlamı (`AppDbContext` - `IdentityDbContext` entegreli).
    * Repository implementasyonları (`GenericRepository`).
    * Veritabanı göç dosyaları (Migrations).
    * İlk veri besleme sınıfı (`DataSeeder`).

3.  **`exampleProject.Web`**
    * Kullanıcı arayüzü (AdminLTE 3, Razor Views, ViewModels).
    * Denetleyiciler (`HomeController`, `AccountController`).
    * Dinamik yetki doğrulama motorları (`PermissionHandler`, `PermissionPolicyProvider`).
    * Uygulama konfigürasyonları ve Dependency Injection tanımlamaları (`Program.cs`).

---

## 🛠️ Kurulum ve Çalıştırma

### Gereksinimler
* .NET 8.0 SDK veya üzeri
* MS SQL Server & SQL Server Management Studio (SSMS)
* Visual Studio 2022 (v17.8+)

### Adım Adım Ayaklar Altına Alma

1.  **Projeyi Klonlayın veya Açın:**
    Çözüm dosyasını (`exampleProject.sln`) Visual Studio ile açın.

2.  **Veritabanı Bağlantı Dizesini Güncelleyin:**
    `exampleProject.Web` projesi altındaki `appsettings.json` dosyasını açarak `DefaultConnection` alanını kendi yerel SQL Server bilgilerinize göre düzenleyin:
    ```json
    "ConnectionStrings": {
      "DefaultConnection": "Server=YOUR_SERVER_NAME;Database=ExampleAdminDb;Trusted_Connection=True;TrustServerCertificate=True;"
    }
    ```

3.  **Statik Dosyaları Kontrol Edin:**
    AdminLTE şablonuna ait `dist` ve `plugins` klasörlerinin `exampleProject.Web/wwwroot/` dizini altında doğrudan yer aldığından emin olun.

4.  **Veritabanı Migration İşlemlerini Çalıştırın:**
    Visual Studio menüsünden **Tools > NuGet Package Manager > Package Manager Console** penceresini açın.
    * *Default project* açılır menüsünden **`exampleProject.Data`** projesini seçin.
    * Sırasıyla şu komutları koşturun:
    ```powershell
    Add-Migration InitialCreate
    Update-Database
    ```

5.  **Başlangıç Projesini Ayarlayın:**
    `exampleProject.Web` projesine sağ tıklayıp **"Set as Startup Project"** seçeneğini seçin.

6.  **Projeyi Çalıştırın:**
    `F5` veya `Start` butonuna basarak projeyi ayağa kaldırın.

---

## 🔐 İlk Giriş Bilgileri

Uygulama ilk kez çalıştığında `DataSeeder` mekanizması arka planda devreye girer ve test etmeniz için aşağıdaki yetkili hesabı oluşturur:

* **E-posta:** `cemre@example.com`
* **Şifre:** `Cemre123!`
* **Rol:** `SuperAdmin` (Kategoriler modülüne ait View, Create, Update, Delete yetkilerinin tamamına sahiptir.)

> **Not:** `HomeController/Index` (Kategori Listesi) sayfası `[Authorize(Policy = PermissionConsts.Category.View)]` ile korunmaktadır. Giriş yapmadan bu sayfaya erişmeye çalışırsanız sistem sizi otomatik olarak `/Account/Login` sayfasına yönlendirecektir.