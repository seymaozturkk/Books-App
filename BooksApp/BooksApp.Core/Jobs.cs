using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksApp.Core
{
    public static class Jobs
    {
        public static string GetUrl(string text)
        {
            #region Açıklamalar
            /*
             * Bu metot kendisine gönderilen text değişkeni içindeki;
             * 1) Latin alfabesine çevrilirken sorun yaratma ihtimali olan İ-i, ı-I gibi değerleri dönüştürecek.
             * 2) Diğer Türkçe karakterlerin yerine latin alfabesindeki karşılıklarını koyacak. Ör: ş->s, ö->o
             * 3) Boşlukların yerine - işareti koyacak.
             * 4) Nokta(.), Slash(/) gibi karakterleri de yok edecek.
             */
            #endregion

            #region Sorunlu Türkçe Karakterler Düzeltiliyor
            text = text.Replace("I", "i");
            text = text.Replace("İ", "i");
            text = text.Replace("ı", "i");
            #endregion
            #region Küçük Harfe Dönüştürülüyor
            text = text.ToLower();
            #endregion
            #region Türkçe Karakterler Düzeltiliyor
            text = text.Replace("ö", "o");
            text = text.Replace("ü", "u");
            text = text.Replace("ş", "s");
            text = text.Replace("ç", "c");
            text = text.Replace("ğ", "g");
            #endregion
            #region Sorunlu Karakterler Düzeltiliyor
            text = text.Replace(".", "");
            text = text.Replace("/", "");
            text = text.Replace("\\", "");
            text = text.Replace("'", "");
            text = text.Replace("`", "");
            text = text.Replace("\"", "");
            text = text.Replace("(", "");
            text = text.Replace(")", "");
            text = text.Replace("{", "");
            text = text.Replace("}", "");
            text = text.Replace("[", "");
            text = text.Replace("]", "");
            text = text.Replace("?", "");
            text = text.Replace(",", "");
            text = text.Replace("-", "");
            text = text.Replace("_", "");
            text = text.Replace("$", "");
            text = text.Replace("&", "");
            text = text.Replace("%", "");
            text = text.Replace("^", "");
            text = text.Replace("#", "");
            text = text.Replace("+", "");
            text = text.Replace("!", "");
            text = text.Replace("=", "");
            text = text.Replace(";", "");
            text = text.Replace(">", "");
            text = text.Replace("<", "");
            text = text.Replace("|", "");
            text = text.Replace("*", "");

            #endregion
            #region Boşluklar Tire İle Değiştiriliyor
            text = text.Replace(" ", "-");
            #endregion
            return text;
        }
        public static string UploadImage(IFormFile image)
        {
            var extension = Path.GetExtension(image.FileName);
            var randomName = $"{Guid.NewGuid()}{extension}";
            //Şimdi de resmi sunucuya yüklüyoruz
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/books", randomName);
            using (var stream = new FileStream(path, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return randomName;
        }
        public static string CreateMessage(string title, string message, string alertType)
        {
            AlertMessage msg = new AlertMessage
            {
                Title = title,
                Message = message,
                AlertType = alertType
            };
            string result = JsonConvert.SerializeObject(msg);
            return result;
        }
    }
}