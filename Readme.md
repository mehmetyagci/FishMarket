# FishMarket Uygulamas�

FishMarket, bir .NET Core API ve .NET Core MVC projelerini i�eren bir uygulamad�r. Bu uygulama, bal�k pazar� i�lemlerini sim�le eder ve API �zerinden veri sa�lar. 

## Kurulum

1. Bu projenin kaynak kodlar�n� almak i�in a�a��daki komutu kullanarak GitHub deposunu klonlay�n:
    ```
    git clone https://github.com/mehmetyagci/FishMarket.git
    ```

2. Ard�ndan, klonlad���n�z dizine ge�in:
    ```
    cd FishMarket
    ```

3. FishMarket.sln solution dosyas�n� Visual Studio veya ba�ka bir geli�tirme ortam�nda a��n.

4. API projesi, SQL Server veritaban� kullanmaktad�r. Veritaban�n�n olu�turulmas� ve ili�kilendirilmesi gerekmektedir. Bunun i�in, API projesinin `appsettings.json` dosyas�nda veritaban� ba�lant� bilgilerini ayarlay�n ve ard�ndan veritaban�n� Code First Migration kullanarak olu�turun. Bu ad�m� ger�ekle�tirmek i�in a�a��daki komutlar� s�ras�yla kullanabilirsiniz:
    ```
    dotnet ef migrations add InitialCreate
    dotnet ef database update
    ```

5. FishMarket.API ve FishMarket.MVC projelerini Startup project olarak belirleyin ve Visual Studio'da �al��t�r�n.

6. API ve MVC projelerini ba�lat�n ve FishMarket uygulamas�n� kullanmaya ba�lay�n.

## Mimari

FishMarket uygulamas�, Onion mimarisi kullan�larak tasarlanm��t�r. Katmanlar, birbirinden olabildi�ince ba��ms�z olarak geli�tirilmi�tir ve a�a��daki gibi s�ralanm��t�r:

- **Domain**: Sadece Domain Entity 'leri burada tan�mlan�r.  
- **Data**: DbContext ve veritaban� schema tan�mlar� burada bulunur.
- **Core**: Uygulaman�n genel yap�land�rma ve temel hizmetleri bu katmanda yer al�r. Ba��ml�l�klar� y�netir ve uygulama boyunca kullan�lan �ekirdek i�levleri sa�lar.
- **Repository**: Veritaban� ile ileti�imi y�neten katmand�r. Veri taban� i�lemleri bu katman arac�l���yla ger�ekle�tirilir.
- **Service**: Uygulaman�n i� mant���n� uygulayan hizmetlerin bulundu�u katmand�r. API katman�ndan gelen istekleri i�ler ve t�m i� mant��� burada y�r�t�l�r.
- **API**: Service katman�nda yaz�lan t�m s�re�lerin kullan�labilmesini sa�layan servisler sa�lar. 
- **MVC**: Web Katman�, API katman�na Http istekleri yaparak merkezi olarak 
- **DTO**: Veri transfer nesnelerinin (DTO'lar�n) tan�mland��� katmand�r. Veri taban�ndan al�nan veya API'den d�nd�r�len verilerin ta��nmas�nda kullan�l�r. MVC uygulamas� da ayn� DTO 'lar� kullan�r.
Bu katmanlar aras�nda net bir s�n�rlama ve ili�ki sa�lanarak, uygulaman�n bak�m�, geni�letilmesi ve test edilmesi kolayla�t�r�lm��t�r. 

