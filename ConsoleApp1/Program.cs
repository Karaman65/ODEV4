// Ürün sınıfı, her ürün için ID, ad ve miktar bilgilerini tutar
class Urun
{
    public string UrunId { get; set; } // Ürün ID'si
    public string Ad { get; set; } // Ürün adı
    public int Miktar { get; set; } // Ürün miktarı

    // Ürün sınıfının yapıcı metodu, ürünün ID'sini, adını ve miktarını alır ve bu değerleri atar
    public Urun(string id, string ad, int miktar)
    {
        UrunId = id;
        Ad = ad;
        Miktar = miktar;
    }
}

// Programın ana sınıfı
class Program
{
    // Ana programın çalıştığı metod
    static void Main()
    {
        // Kullanıcıdan kuyruk mu, yoksa stack mi seçtiğini soruyoruz
        Console.WriteLine("Kuyruk (1) veya Stack (2) seçin:");
        int secim = int.Parse(Console.ReadLine()); // Kullanıcının cevabını alıyoruz

        // LinkedList oluşturuyoruz (bu, ürünleri tutacak veri yapısı olacak)
        LinkedList<Urun> veriYapisi = new LinkedList<Urun>();
        bool kuyruk_Mu = secim == 1;  // Eğer kullanıcı "1" seçerse, kuyruk olacak; "2" seçerse stack olacak

        // Sonsuz bir döngüde işlemleri tekrarlayacağız
        while (true)
        {
            // Kullanıcıya seçenekler sunuluyor: ekle, sil, ara, listele, sırala
            Console.WriteLine("\nEkle(1), Sil(2), Ara(3), Listele(4), Sırala(5):");
            int islem = int.Parse(Console.ReadLine()); // Kullanıcının yaptığı işlem seçimi

            if (islem == 1) // Eğer kullanıcı "Ekle" seçerse
            {
                // Ürün bilgilerini alıyoruz
                Console.Write("Ürün ID: ");
                string id = Console.ReadLine();
                Console.Write("Ürün Adı: ");
                string ad = Console.ReadLine();
                Console.Write("Miktar: ");
                int miktar = int.Parse(Console.ReadLine());

                // Yeni bir Urun nesnesi oluşturuyoruz
                var urun = new Urun(id, ad, miktar);

                // Eğer kuyruk seçildiyse, ürünü kuyruğa ekliyoruz
                if (kuyruk_Mu)
                    veriYapisi.AddLast(urun); // Kuyrukta sonuna ekleme
                else
                    veriYapisi.AddFirst(urun); // Stack'te başına ekleme
            }
            else if (islem == 2) // Eğer kullanıcı "Sil" seçerse
            {
                // Liste boş değilse
                if (veriYapisi.Count > 0)
                {
                    // Kuyruk başı (ilk eleman) veya Stack sonu (son eleman) silinecek
                    var silinenUrun = kuyruk_Mu ? veriYapisi.First : veriYapisi.Last;
                    Console.WriteLine($"Silinen: {silinenUrun.Value.UrunId} - {silinenUrun.Value.Ad}");
                    veriYapisi.Remove(silinenUrun); // Seçilen ürünü listeden çıkarıyoruz
                }
                else
                    Console.WriteLine("Liste boş!"); // Eğer liste boşsa kullanıcıya bilgi veriyoruz
            }
            else if (islem == 3) // Eğer kullanıcı "Ara" seçerse
            {
                // Aramak istenen ürünün ID'sini alıyoruz
                Console.Write("Aradığınız Ürün ID: ");
                string arananId = Console.ReadLine();
                // Ürün arama fonksiyonunu çağırıyoruz
                var bulunanUrun = UrunAra(veriYapisi, arananId);
                // Eğer ürün bulunduysa, bilgilerini gösteriyoruz
                if (bulunanUrun != null)
                    Console.WriteLine($"Bulunan: {bulunanUrun.UrunId} - {bulunanUrun.Ad} - {bulunanUrun.Miktar}");
                else
                    Console.WriteLine("Ürün bulunamadı."); // Eğer ürün bulunmazsa mesaj gösteriyoruz
            }
            else if (islem == 4) // Eğer kullanıcı "Listele" seçerse
            {
                // Listede bulunan tüm ürünleri sırasıyla yazdırıyoruz
                foreach (var urun in veriYapisi)
                    Console.WriteLine($"{urun.UrunId} - {urun.Ad} - {urun.Miktar}");
            }
            else if (islem == 5) // Eğer kullanıcı "Sırala" seçerse
            {
                // LinkedList'i bir List'e dönüştürüyoruz, çünkü sıralama List üzerinden yapılacak
                var urunlerListesi = new List<Urun>(veriYapisi);

                // Bubble Sort algoritmasıyla sıralama yapıyoruz
                for (int i = 0; i < urunlerListesi.Count - 1; i++)
                {
                    // Her iki öğeyi karşılaştırıyoruz ve yanlış sıradaysa takas yapıyoruz
                    for (int j = 0; j < urunlerListesi.Count - 1 - i; j++)
                    {
                        if (urunlerListesi[j].Miktar > urunlerListesi[j + 1].Miktar)
                        {
                            // Takas işlemi (iki öğeyi yer değiştiriyoruz)
                            var temp = urunlerListesi[j];
                            urunlerListesi[j] = urunlerListesi[j + 1];
                            urunlerListesi[j + 1] = temp;
                        }
                    }
                }

                // Sıralı listeyi ekrana yazdırıyoruz
                foreach (var urun in urunlerListesi)
                    Console.WriteLine($"{urun.UrunId} - {urun.Ad} - {urun.Miktar}");
            }

            // Kullanıcıya işlem sonrası devam etmek isteyip istemediğini soruyoruz
            Console.Write("Devam etmek ister misiniz? (E/H): ");
            if (Console.ReadLine().ToLower() != "e") break; // Kullanıcı "E" girerse devam ediyor, yoksa çıkıyor
        }
    }

    // Ürün arama fonksiyonu: Liste içinde ürün ID'sine göre arama yapıyoruz
    static Urun UrunAra(LinkedList<Urun> veriYapisi, string urunId)
    {
        // Her ürünü kontrol ediyoruz
        foreach (var urun in veriYapisi)
        {
            // Eğer ürün ID'si eşleşiyorsa, o ürünü döndürüyoruz
            if (urun.UrunId == urunId)
                return urun;
        }
        // Ürün bulunamazsa, null döndürüyoruz
        return null;
    }
}
