using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace DataObjects
{
    /// <summary>
    ///     Created by    : Francis Mingomba
    ///     Date          : 2019/02/23
    ///     Summary       : Used for deep copy of classes by
    ///                     serializing data object byte by byte
    ///     Accessibility : Can be used by all data objects
    ///     Usage         : Add [Serializable] data annotation to
    ///                     data object class
    ///
    /// </summary>
    public static class ExtensionMethods
    {
        // Deep clone
        public static T DeepClone<T>(this T a)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, a);
                stream.Position = 0;
                return (T)formatter.Deserialize(stream);
            }
        }
    }
}