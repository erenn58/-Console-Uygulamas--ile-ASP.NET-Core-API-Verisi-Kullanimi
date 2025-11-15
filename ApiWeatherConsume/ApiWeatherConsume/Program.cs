#region Menü Başlangıcı
using System.Net;


using Newtonsoft.Json.Linq;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using System.Text;

Console.WriteLine("Api Consume İşlemine Hoş Geldiniz");
Console.WriteLine();
Console.WriteLine("## Yapılacak İşlem Türünü Seçiniz ##");
Console.WriteLine();
Console.WriteLine("1-Şehir Listesini Getir");
Console.WriteLine("2-Liste Getir");
Console.WriteLine("3-Yeni Şehir Ekle");
Console.WriteLine("4-Şehir Sil");
Console.WriteLine("5-ID'ye Göre Şehir Getir");
Console.WriteLine();
#endregion  
string number;
Console.Write("Tercihiniz: ");
number = Console.ReadLine();
Console.WriteLine();
if(number =="1")
{
    string url = "https://localhost:7003/api/Weathers";
    using (HttpClient client = new HttpClient()) 
    {
        HttpResponseMessage response=await client.GetAsync(url);
        string responseBody=await response.Content.ReadAsStringAsync();
        JArray jArray = JArray.Parse(responseBody);
        foreach(var item in jArray)
        {
            string cityName = item["cityName"].ToString();
            Console.WriteLine($"Şehir:{cityName}");
        }
    }
}
if (number == "2")
{
    string url = "https://localhost:7003/api/Weathers";
    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = await client.GetAsync(url);
        string responseBody = await response.Content.ReadAsStringAsync();
        JArray jArray = JArray.Parse(responseBody);
        foreach (var item in jArray)
        {
            string cityName = item["cityName"].ToString();
            string temp = item["temp"].ToString();
            string country = item["country"].ToString();
            Console.Write(cityName + "-" + country + "-->" + temp + "Derece");
           
                

        }
    }
}
if (number == "3")
{
    Console.WriteLine("Yeni Veri Girişi");
    Console.WriteLine();
    string cityName, country, detail;
    decimal temp;
    Console.Write("Şehir Adı:");
    cityName=Console.ReadLine();

    Console.Write("Ülke Adı:");
    country = Console.ReadLine();

    Console.Write("Durum Detayı::");
    detail = Console.ReadLine();

    Console.Write("Sıcaklık:");
    temp = decimal.Parse(Console.ReadLine());

    string url= "https://localhost:7003/api/Weathers";
    var newweatherCity = new
    {
        cityName = cityName,
        country = country,
        detail = detail,
        temp = temp
    };
    using (HttpClient client = new HttpClient())
    {
        string json = JsonConvert.SerializeObject(newweatherCity);
        StringContent content = new(json, Encoding.UTF8,"application/json" );
        HttpResponseMessage response=await client.PutAsync(url, content);
        response.EnsureSuccessStatusCode();
        
        
    }
}
if (number == "4")
{
    string url = "https://localhost:7003/api/Weathers=";
    Console.WriteLine("Silinecek ID Değeri:");
    int id=int.Parse(Console.ReadLine()) ;
    using(HttpClient client= new HttpClient())
    {
        HttpResponseMessage response=await client.DeleteAsync(url+id);
        response.EnsureSuccessStatusCode();
    }
}
if(number == "5")
{
    string url = "https://localhost:7003/api/Weathers/GetByIdWeatherCity?id=";
    Console.Write("Bilgilerini Getirmek İstediğiniz Id Değeri:");
    int id = int.Parse(Console.ReadLine());
    using (HttpClient client = new HttpClient())
    {
        HttpResponseMessage response = await client.GetAsync(url + id);
        response.EnsureSuccessStatusCode();
        string responseBody = await response.Content.ReadAsStringAsync();
        JObject weatherCityObject = JObject.Parse(responseBody);
        string cityName = weatherCityObject["cityName"].ToString();
        string detail = weatherCityObject["detail"].ToString();
        string country = weatherCityObject["country"].ToString();
        decimal temp = decimal.Parse(weatherCityObject["temp"].ToString());
        Console.WriteLine("Girilen ID'ye ait bilgiler");
        Console.WriteLine();
        Console.Write("Şehir:" + cityName + " Ülke:" + country + "Detay:" + detail + "Sıcaklık:" + temp);


    }
}
Console.Read();



