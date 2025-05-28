using System;

namespace EventMessagingSystem
{
    /// <summary>
    /// Main program - Event-based messaging system demonstration
    /// Observer ve Publisher/Subscriber pattern'lerini test eder
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("🚀 Event-Based Messaging System Demo\n");

            // ========== PHASE 1: SYSTEM SETUP ==========
            
            /// <summary>
            /// Publisher'lar oluştur - Subject objects (Observer Pattern)
            /// Her publisher farklı kanal/domain'i temsil eder
            /// </summary>
            var haberKanali = new Publisher("Haber TV");
            var sporKanali = new Publisher("Spor Kanalı");

            /// <summary>
            /// Subscriber'lar oluştur - Observer objects
            /// Her subscriber farklı kullanıcıyı temsil eder
            /// </summary>
            var ali = new Subscriber("Ali");
            var ayse = new Subscriber("Ayşe");
            var mehmet = new Subscriber("Mehmet");

            // ========== PHASE 2: INTEREST CONFIGURATION ==========
            
            /// <summary>
            /// İlgi alanları belirleme - Content filtering setup
            /// Publisher/Subscriber pattern'in filtering mekanizması
            /// </summary>
            Console.WriteLine("--- İlgi Alanları Belirleniyor ---");
            ali.AddInterest("Haber");        // Ali sadece haber istiyor
            
            ayse.AddInterest("Spor");        // Ayşe sadece spor istiyor
            ayse.AddInterest("Haber");       // Ayşe ayrıca haber de istiyor
            
            mehmet.AddInterest("Teknoloji"); // Mehmet sadece teknoloji istiyor

            Console.WriteLine("\n" + new string('-', 50));

            // ========== PHASE 3: SUBSCRIPTION SETUP ==========
            
            /// <summary>
            /// Observer registration - Event delegation setup
            /// Subscriber'lar Publisher'lara abone oluyor
            /// += operator ile event handler'lar bağlanıyor
            /// </summary>
            Console.WriteLine("--- Abonelikler Kuruluyor ---");
            ali.SubscribeTo(haberKanali);      // Ali haber kanalına abone
            
            ayse.SubscribeTo(haberKanali);     // Ayşe haber kanalına abone
            ayse.SubscribeTo(sporKanali);      // Ayşe spor kanalına da abone
            
            mehmet.SubscribeTo(haberKanali);   // Mehmet haber kanalına abone
                                               // (ama teknoloji ilgisi var, filtreleme yapılacak)

            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("📡 MESAJ YAYINLAMA TEST SENARYOSu");
            Console.WriteLine(new string('=', 60));

            // ========== PHASE 4: EVENT PUBLISHING ==========
            
            /// <summary>
            /// Event firing test - Observer Pattern action
            /// Her PublishMessage() çağrısı:
            /// 1. Message objesi oluşturur
            /// 2. Event'i tetikler (MessagePublished?.Invoke)
            /// 3. Tüm subscriber'ların OnMessageReceived metodunu çağırır
            /// 4. Her subscriber kendi filtresine göre mesajı kabul/red eder
            /// </summary>

            // Test 1: Haber mesajı - Ali ve Ayşe almalı, Mehmet almamalı
            Console.WriteLine("\n🔸 Test 1: Haber Mesajı");
            haberKanali.PublishMessage("Seçim sonuçları açıklandı!", "Haber");

            // Test 2: Spor mesajı - Sadece Ayşe almalı
            Console.WriteLine("\n🔸 Test 2: Spor Mesajı");
            sporKanali.PublishMessage("Fenerbahçe şampiyon oldu!", "Spor");

            // Test 3: Teknoloji mesajı - Kimse almamalı (ilgilenen yok)
            Console.WriteLine("\n🔸 Test 3: Teknoloji Mesajı (filtrelenecek)");
            haberKanali.PublishMessage("Yeni iPhone çıktı!", "Teknoloji");

            // ========== PHASE 5: RESULTS ANALYSIS ==========
            
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("📊 SONUÇ ANALİZİ");
            Console.WriteLine(new string('=', 60));

            /// <summary>
            /// Her subscriber'ın aldığı mesajları göster
            /// Filtreleme sonuçlarını doğrular
            /// </summary>
            ali.ShowReceivedMessages();      // Sadece haber mesajı
            ayse.ShowReceivedMessages();     // Haber + spor mesajları
            mehmet.ShowReceivedMessages();   // Mesaj yok (filtrelendi)

            // ========== PHASE 6: AUDIT TRAIL ==========
            
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("📝 PUBLISHER GEÇMİŞİ (Audit Trail)");
            Console.WriteLine(new string('=', 60));

            /// <summary>
            /// Publisher'ların mesaj geçmişini göster
            /// System audit ve logging amaçlı
            /// </summary>
            haberKanali.ShowHistory();
            sporKanali.ShowHistory();

            // ========== PATTERN ANALYSIS ==========
            
            Console.WriteLine("\n" + new string('=', 60));
            Console.WriteLine("🔍 PATTERN ANALİZİ");
            Console.WriteLine(new string('=', 60));
            Console.WriteLine("✅ Observer Pattern: Publisher değiştiğinde subscriber'lar otomatik bilgilendirildi");
            Console.WriteLine("✅ Publisher/Subscriber: Mesaj filtreleme başarıyla çalıştı");
            Console.WriteLine("✅ Event-Driven Architecture: Loose coupling sağlandı");
            Console.WriteLine("✅ Multicast Delegate: Birden fazla subscriber aynı event'i dinledi");

            Console.WriteLine("\n✅ Demo tamamlandı! Herhangi bir tuşa basın...");
            Console.ReadKey();
        }
    }
}