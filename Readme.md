# FishMarket Uygulamasý

FishMarket, bir .NET Core API ve .NET Core MVC projelerini içeren bir örnek uygulamadýr. Bu uygulama, balýk pazarý iþlemlerini simüle eder ve API üzerinden veri saðlar. 

## Kurulum

1. Bu projenin kaynak kodlarýný almak için aþaðýdaki komutu kullanarak GitHub deposunu klonlayýn:
    ```
    git clone https://github.com/mehmetyagci/FishMarket.git
    ```

2. Ardýndan, klonladýðýnýz dizine geçin:
    ```
    cd FishMarket
    ```

3. FishMarket.sln solution dosyasýný Visual Studio veya baþka bir geliþtirme ortamýnda açýn.

4. API projesi, SQL Server veritabaný kullanmaktadýr. Veritabanýnýn oluþturulmasý ve iliþkilendirilmesi gerekmektedir. Bunun için, API projesinin `appsettings.json` dosyasýnda veritabaný baðlantý bilgilerini ayarlayýn ve ardýndan veritabanýný Code First Migration kullanarak oluþturun. Bu adýmý gerçekleþtirmek için aþaðýdaki komutlarý sýrasýyla kullanabilirsiniz:
    ```
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```

5. FishMarket.API ve FishMarket.MVC projelerini Startup project olarak belirleyin ve Visual Studio'da çalýþtýrýn.

6. API ve MVC projelerini baþlatýn ve FishMarket uygulamasýný kullanmaya baþlayýn.

## Katkýda Bulunma

Bu projeye katkýda bulunmak isterseniz, lütfen GitHub deposuna bir çekme isteði gönderin. Her türlü geri bildirimi, öneriyi veya sorun bildirimini memnuniyetle karþýlýyoruz.
