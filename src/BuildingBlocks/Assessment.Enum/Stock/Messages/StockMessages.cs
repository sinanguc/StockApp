using Assessment.Enum.Common.Messages;

namespace Assessment.Enum.Stock.Messages
{
    public class StockMessages : GenericMessages
    {
        public const string KullaniciZatenKayitli = "Kullanıcı zaten kayıtlı";
        public const string KullaniciAdiveyaSifreHatali = "Kullanıcı Adı veya Şifre hatalı";
        public const string EskiSifreHatali = "Eski şifre hatalı";
        public const string AccessTokenBasariylaOlusturuldu = "Access token başarıyla oluşturuldu.";
        public const string IslemiYapmayaYetkinizYok = "Bu işlem için yetkiniz yok.";
        public const string TokenOlusturulamadi = "Token oluşturulurken hata oluştu";
        public const string ZatenKayitli = "Kayıt etmek istenilen kayıt zaten kayıtlı";

        public const string UstMenuKendisiOlamaz = "Üst menü kendisi olamaz";
    }
}
