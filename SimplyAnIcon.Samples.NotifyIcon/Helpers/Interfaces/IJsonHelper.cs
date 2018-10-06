namespace SimplyAnIcon.Samples.NotifyIcon.Helpers.Interfaces
{
    public interface IJsonHelper
    {
        T DeserializeFile<T>(string filepath);
        T Deserialize<T>(string json);
        void SerializeToFile<T>(T obj, string filepath);
        string Serialize<T>(T obj);
    }
}
