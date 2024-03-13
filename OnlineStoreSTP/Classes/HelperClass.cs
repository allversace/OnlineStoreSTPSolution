using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace OnlineStoreSTP.Classes
{
    public class HelperClass
    {
        public static bool CheckLetter(string text)
        {
            return text.Any(char.IsLetter);
        }

        public static bool CheckNumber(string text)
        {
            return text.Any(char.IsDigit);
        }
    }
}
