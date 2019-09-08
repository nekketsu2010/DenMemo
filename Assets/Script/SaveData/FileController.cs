using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class FileController : MonoBehaviour
{

    public static string Save<T>(T serializableObject)
    {
        MemoryStream memoryStream = new MemoryStream();
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(memoryStream, serializableObject);

        string tmp = System.Convert.ToBase64String(memoryStream.ToArray());
        return tmp;
    }

    public static T Load<T>(string serializedData)
    {
        BinaryFormatter bf = new BinaryFormatter();

        MemoryStream dataStream = new MemoryStream(System.Convert.FromBase64String(serializedData));
        T deserializedObject = (T)bf.Deserialize(dataStream);

        return deserializedObject;
    }

}