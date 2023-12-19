using System.Text.Encodings.Web;
using System.Text.Json;

namespace Diamond.Utils
{
    public static class JsonConvertor
    {
        public static ICollection<T> ReadJsonData<T>(string fileName, string path)
        {
            var filePath = Path.Combine(path, $"{fileName}.json");
            if (File.Exists(filePath))
            {
                var content = File.ReadAllText(filePath);

                return JsonSerializer.Deserialize<ICollection<T>>(content, new JsonSerializerOptions()
                {
                    ReadCommentHandling = JsonCommentHandling.Skip
                });
            }

            return null;
        }

        public static void WriteJsonData<T>(List<T> data, string fileName, string path)
        {
            var filePath = Path.Combine(path, $"{fileName}.json");

            var json = JsonSerializer.Serialize(data, new JsonSerializerOptions()
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            });

            File.WriteAllText(filePath, json);
        }
    }
}
