# FishMarket Uygulamasý

FishMarket, bir .NET Core API ve .NET Core MVC projelerini içeren bir uygulamadýr. Bu uygulama, balýk pazarý iþlemlerini simüle eder ve API üzerinden veri saðlar. 

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

## Mimari

FishMarket uygulamasý, Onion mimarisi kullanýlarak tasarlanmýþtýr. Katmanlar, birbirinden olabildiðince baðýmsýz olarak geliþtirilmiþtir ve aþaðýdaki gibi sýralanmýþtýr:

- **Domain**: Sadece Domain Entity 'leri burada tanýmlanýr.  
- **Data**: DbContext ve veritabaný schema tanýmlarý burada bulunur.
- **Core**: Uygulamanýn genel yapýlandýrma ve temel hizmetleri bu katmanda yer alýr. Baðýmlýlýklarý yönetir ve uygulama boyunca kullanýlan çekirdek iþlevleri saðlar.
- **Repository**: Veritabaný ile iletiþimi yöneten katmandýr. Veri tabaný iþlemleri bu katman aracýlýðýyla gerçekleþtirilir.
- **Service**: Uygulamanýn iþ mantýðýný uygulayan hizmetlerin bulunduðu katmandýr. API katmanýndan gelen istekleri iþler ve tüm iþ mantýðý burada yürütülür.
- **API**: Service katmanýnda yazýlan tüm süreçlerin kullanýlabilmesini saðlayan servisler saðlar. 
- **MVC**: Web Katmaný, API katmanýna Http istekleri yaparak merkezi olarak 
- **DTO**: Veri transfer nesnelerinin (DTO'larýn) tanýmlandýðý katmandýr. Veri tabanýndan alýnan veya API'den döndürülen verilerin taþýnmasýnda kullanýlýr. MVC uygulamasý da ayný DTO 'larý kullanýr.
Bu katmanlar arasýnda net bir sýnýrlama ve iliþki saðlanarak, uygulamanýn bakýmý, geniþletilmesi ve test edilmesi kolaylaþtýrýlmýþtýr. 

