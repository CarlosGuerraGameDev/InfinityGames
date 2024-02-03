namespace Mechanics.SaveSystem
{
    public interface ISerializer<T>
    {
        string Serialize(T data);
        T Deserialize(string serializedData);
    }
}