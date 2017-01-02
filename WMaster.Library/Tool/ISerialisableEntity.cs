namespace WMaster.Tool
{
    using System.Xml.Linq;

    /// <summary>
    /// Provide support for serialisation / deserialisation.
    /// </summary>
    public interface ISerialisableEntity
    {
        /// <summary>
        /// Serialise instance into XElement.
        /// </summary>
        /// <param name="data"><see cref="XElement"/> of data to serialise.</param>
        /// <returns><b>True</b> if all data was serialized without error.</returns>
        bool Serialise(XElement data);
        /// <summary>
        /// Deserialise <see cref="XElement"/> <paramref name="data"/> into this instance.
        /// <remarks>
        ///     <para>Property not present in <see cref="XElement"/> <paramref name="data"/> keep their default value.</para>
        ///     <para>
        ///         Calling multiple time this function with different data alow multiple source loading.
        ///         Order of call was important because last data set overrite old value.
        ///     </para>
        /// </remarks>
        /// </summary>
        /// <param name="data"><see cref="XElement"/> of data to deserialiser.</param>
        /// <returns><b>True</b> if all data was set without error.</returns>
        bool Deserialise(XElement data);
    }
}
