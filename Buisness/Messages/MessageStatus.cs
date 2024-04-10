namespace Buisness.Messages
{
    public class MessageStatus
    {
        public const string ExceptionMessage = "Gözlənilməz xəta baş verdi!";
        public static string NotFoundMessage(string entityName) => $"Belə bir {entityName.ToLower()} yoxdur";
        public static string ExistMessage(string entityName) => $"Belə bir {entityName.ToLower()} mövcuddur";
        public static string NotNullMessage(string entityName) => $"{entityName} boş ola bilməz";
    }
}
