using System;
using System.Collections.Generic;

namespace EventMessagingSystem
{
 
    /// Publisher sınıfı - Observer Pattern'in Subject rolü
    /// Mesaj yayınlar ve tüm subscriber'ları otomatik bilgilendirir

    public class Publisher
    {
       
        /// Action<Message>: Message parametresi alan fonksiyon türü
        /// Subscriber'lar bu event'e kendi metodlarını bağlar
      
        public event Action<Message> MessagePublished;

        // Private fields: Encapsulation prensibi
        private string _publisherName;           // Yayıncının adı
        private List<string> _messageHistory;    // Gönderilen mesaj kayıtları

        /// <summary>
        /// Constructor: Publisher nesnesi oluştururken çağrılır
        /// </summary>
        public Publisher(string name)
        {
            _publisherName = name;                    // Publisher'a isim ver
            _messageHistory = new List<string>();     // Boş geçmiş listesi oluştur
        }

        /// <summary>
        /// Ana mesaj yayınlama metodu - Publisher/Subscriber Pattern implementasyonu
        /// 1. Message objesi oluştur
        /// 2. Geçmişe kaydet
        /// 3. Event tetikle (tüm subscriber'lara gönder)
        /// 4. Console'a log yaz
        /// </summary>
        public void PublishMessage(string content, string messageType)
        {
            // 1. Yeni mesaj objesi oluştur (Message constructor çağrılır)
            var message = new Message(content, messageType, _publisherName);
            
            // 2. String formatında geçmişe kaydet (audit trail)
            _messageHistory.Add(message.ToString());
            
            // 3. ⚡ KRITIK: Event'i tetikle - Observer Pattern action
            // Null-conditional operator (?.) kimse dinlemiyorsa hata önler
            // Invoke() tüm subscriber'ların OnMessageReceived metodunu çağırır
            MessagePublished?.Invoke(message);
            
            // 4. Publisher'ın kendi logunu yaz
            Console.WriteLine($"Publisher '{_publisherName}' mesaj yayınladı: {message}");
        }

        /// <summary>
        /// Mesaj geçmişini görüntüleme - Audit/logging amaçlı
        /// </summary>
        public void ShowHistory()
        {
            Console.WriteLine($"\n=== {_publisherName} Mesaj Geçmişi ===");
            foreach (var msg in _messageHistory)
            {
                Console.WriteLine(msg);
            }
        }
    }
}