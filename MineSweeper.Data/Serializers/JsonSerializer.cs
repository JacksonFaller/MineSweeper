using Newtonsoft.Json;

namespace MineSweeper.Data.Serializers
{
    public class JsonSerializer : ISerializer
    {
        public T Deserialize<T>(string obj) => JsonConvert.DeserializeObject<T>(obj);

        public string Serialize(object obj) => JsonConvert.SerializeObject(obj);
    }
}
