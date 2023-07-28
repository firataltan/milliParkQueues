using System;
using System.Collections.Generic;
using System.IO;

class MilliPark
{
    public string MilliPark_Adı { get; set; }
    public List<string> İl_Adları { get; set; }
    public int İlan_Yılı { get; set; }
    public double Yüzölçümü { get; set; }

    public MilliPark(string ad, List<string> iller, int yıl, double yüzölçüm)
    {
        MilliPark_Adı = ad;
        İl_Adları = iller;
        İlan_Yılı = yıl;
        Yüzölçümü = yüzölçüm;
    }
}

class ÖncelikliKuyruk
{
    private List<MilliPark> milliParks;

    public ÖncelikliKuyruk()
    {
        milliParks = new List<MilliPark>();
    }

    public void Ekle(MilliPark milliPark)
    {
        milliParks.Add(milliPark);
    }

    public MilliPark SilEnKüçükYüzölçümlü()
    {
        if (milliParks.Count == 0)
            throw new InvalidOperationException("Kuyruk boş.");

        int enKüçükIndex = 0;
        for (int i = 1; i < milliParks.Count; i++)
        {
            if (milliParks[i].Yüzölçümü < milliParks[enKüçükIndex].Yüzölçümü)
                enKüçükIndex = i;
        }

        MilliPark enKüçük = milliParks[enKüçükIndex];
        milliParks.RemoveAt(enKüçükIndex);
        return enKüçük;
    }

    public bool BosMu()
    {
        return milliParks.Count == 0;
    }
}

class Program
{
    static void Main()
    {
        ÖncelikliKuyruk kuyruk = new ÖncelikliKuyruk();

        // Dosyadan verileri okuyarak MilliPark nesnelerini oluşturunuz.
        try
        {
            using (StreamReader sr = new StreamReader("milli.txt"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] parts = line.Split(';');
                    string ad = parts[0];
                    List<string> iller = new List<string>(parts[1].Split(','));
                    int yıl = int.Parse(parts[2]);
                    double yüzölçüm = double.Parse(parts[3]);

                    MilliPark milliPark = new MilliPark(ad, iller, yıl, yüzölçüm);
                    kuyruk.Ekle(milliPark);
                }
            }

            Console.WriteLine("***** Milli Parklar (Yüzölçümlerine göre küçükten büyüğe) *****");
            while (!kuyruk.BosMu())
            {
                MilliPark milliPark = kuyruk.SilEnKüçükYüzölçümlü();
                PrintMilliParkInfo(milliPark);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine("Dosya okuma hatası: " + e.Message);
        }

        Console.ReadKey();
    }

    static void PrintMilliParkInfo(MilliPark milliPark)
    {
        Console.WriteLine($"Milli Park Adı: {milliPark.MilliPark_Adı}");
        Console.WriteLine("Bulunduğu İller:");
        foreach (string il in milliPark.İl_Adları)
        {
            Console.WriteLine(il);
        }
        Console.WriteLine($"İlan Yılı: {milliPark.İlan_Yılı}");
        Console.WriteLine($"Yüzölçümü: {milliPark.Yüzölçümü} hektar");
        Console.WriteLine();
    }
}
