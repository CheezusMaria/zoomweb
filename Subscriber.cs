using System;
using System.Collections.Generic;

namespace EventMessagingSystem
{
    /// <summary>
    /// Subscriber sınıfı - Observer Pattern'in Observer rolü
    /// Publisher'ları dinler ve sadece ilgilendiği mesajları alır
    /// Publisher/Subscriber Pattern ile filtreleme yapar
    /// </summary>
    public class Subscriber
    {
        // Private fields: Subscriber'ın internal state'i
        private string _subscriberName;                      // Subscriber'ın adı
        private List<Message> _receivedMessages;             // Alınan mesajların koleksiyonu
        private List<string> _interestedMessageTypes;        // İlgi filtreleri

        /// <summary>
        /// Constructor: Subscriber nesnesi oluştururken çağrılır
        /// Tüm koleksiyonlar boş olarak başlatılır
        /// </summary>
        public Subscriber(string name)
        {
            _subscriberName = name;
            _receivedMessages = new List<Message>();       // Boş mesaj listesi
            _interestedMessageTypes = new List<string>();  // Boş filtre listesi
        }

        /// <summary>
        /// İlgi alanı ekleme - Message filtering için
        /// Duplicate check yapılır (aynı türü iki kez eklemeyi önler)
        /// </summary>
        public void AddInterest(string messageType)
        {
            // Duplicate control: Aynı türü tekrar ekleme
            if (!_interestedMessageTypes.Contains(messageType))
            {
                _interestedMessageTypes.Add(messageType);
                Console.WriteLine($"{_subscriberName} artık '{messageType}' mesajlarını dinliyor");
            }
        }

        /// <summary>
        /// Publisher'a abone olma - Observer Pattern registration
        /// Event delegation: Publisher'ın event'ine kendi handler'ını bağlar
        /// += operator ile multi-cast delegate oluşur
        /// </summary>
        public void SubscribeTo(Publisher publisher)
        {
            // ⚡ KRITIK: Event'e method bağlama (Observer registration)
            // += multicast delegate oluşturur
            // Publisher mesaj gönderdiğinde OnMessageReceived otomatik çağrılır
            publisher.MessagePublished += OnMessageReceived;
            
            Console.WriteLine($"{_subscriberName}, {publisher.GetType().Name}'a abone oldu");
        }

        /// <summary>
        /// Abonelikten çıkma - Observer Pattern deregistration
        /// -= operator ile event'ten method'u çıkarır
        /// </summary>
        public void UnsubscribeFrom(Publisher publisher)
        {
            // Event'ten method çıkarma (Observer deregistration)
            publisher.MessagePublished -= OnMessageReceived;
            Console.WriteLine($"{_subscriberName} abonelikten çıktı");
        }

        /// <summary>
        /// Event Handler - Observer Pattern'in reaction method'u
        /// Publisher event tetiklediğinde otomatik çağrılır
        /// Content-based filtering yapar
        /// </summary>
        private void OnMessageReceived(Message message)
        {
            // Filtreleme logic: Publisher/Subscriber Pattern'in core'u
            // 1. Hiç filtre yoksa tüm mesajları al VEYA
            // 2. Bu mesaj türü ilgi listesinde var mı kontrol et
            if (_interestedMessageTypes.Count == 0 || 
                _interestedMessageTypes.Contains(message.MessageType))
            {
                // Mesajı kabul et ve koleksiyona ekle
                _receivedMessages.Add(message);
                Console.WriteLine($"📩 {_subscriberName} mesaj aldı: {message}");
            }
            // İlgilenmiyorsa silent ignore (mesaj görmezden gelinir)
        }

        /// <summary>
        /// Alınan mesajları görüntüleme - UI/Reporting method
        /// Empty collection check yapılır
        /// </summary>
        public void ShowReceivedMessages()
        {
            Console.WriteLine($"\n=== {_subscriberName} Alınan Mesajlar ===");
            
            // Empty collection check
            if (_receivedMessages.Count == 0)
            {
                Console.WriteLine("Henüz mesaj alınmadı.");
                return; // Early return pattern
            }

            // Collection iteration ile mesajları listele
            foreach (var message in _receivedMessages)
            {
                Console.WriteLine($"  {message}"); // 2 space indentation
            }
        }

        /// <summary>
        /// İlgi alanlarını görüntüleme - Debug/Info amaçlı
        /// string.Join ile array'i formatted string'e çevirir
        /// </summary>
        public void ShowInterests()
        {
            // Array to string conversion
            Console.WriteLine($"{_subscriberName} ilgilenilen türler: {string.Join(", ", _interestedMessageTypes)}");
        }
    }
}