# FishMarket Uygulamas�

FishMarket, bir .NET Core API ve .NET Core MVC projelerini i�eren bir �rnek uygulamad�r. Bu uygulama, bal�k pazar� i�lemlerini sim�le eder ve API �zerinden veri sa�lar. 

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

## Katk�da Bulunma

Bu projeye katk�da bulunmak isterseniz, l�tfen GitHub deposuna bir �ekme iste�i g�nderin. Her t�rl� geri bildirimi, �neriyi veya sorun bildirimini memnuniyetle kar��l�yoruz.
