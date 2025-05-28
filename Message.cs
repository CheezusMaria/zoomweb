using System;

namespace EventMessagingSystem
{
  
    /// Mesaj sınıfı - Her mesajın temel bilgilerini tutar
 
    public class Message
    {
        // Properties: Mesajın meta verileri
        public string Content { get; set; }        // Mesajın içeriği ("Merhaba!")
        public DateTime Timestamp { get; set; }    // Gönderilme zamanı (otomatik eklenir)
        public string MessageType { get; set; }    // Kategori/tür ("Haber", "Spor")
        public string SenderName { get; set; }     // Gönderen kanal ("Haber TV")

        public Message(string content, string messageType, string senderName)
        {
            Content = content;           // Kullanıcıdan gelen içerik
            MessageType = messageType;   // Mesaj kategorisi
            SenderName = senderName;     // Publisher'ın adı
            Timestamp = DateTime.Now;    // Şu anki zaman - otomatik timestamping
        }

        /// <summary>
        /// ToString override: Mesajı okunabilir formata çevirir
        /// Console çıktıları ve logging için kullanılır
        /// Format: [15:30:45] Haber TV (Haber): Bugün güneşli!
        /// </summary>
        public override string ToString()
        {
            return $"[{Timestamp:HH:mm:ss}] {SenderName} ({MessageType}): {Content}";
            //        ↑ Saat:Dakika:Saniye format
        }
    }
}