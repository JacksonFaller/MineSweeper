using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace MineSweeper.Data.Serializers
{
    public class Base64Serializer : ISerializer
    {
        public T Deserialize<T>(string obj)
        {
            byte[] b = Convert.FromBase64String(obj);
            using var stream = new MemoryStream(b);
            var formatter = new BinaryFormatter();
            stream.Seek(0, SeekOrigin.Begin);
            return (T)formatter.Deserialize(stream);
        }

        public string Serialize(object obj)
        {
            using var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, obj);
            stream.Flush();
            stream.Position = 0;
            return Convert.ToBase64String(stream.ToArray());
        }
    }
}
