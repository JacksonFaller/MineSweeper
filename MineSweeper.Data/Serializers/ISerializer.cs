namespace MineSweeper.Data.Serializers
{
    public interface ISerializer
    {
        string Serialize(object obj);
        T Deserialize<T>(string obj);
    }
}
