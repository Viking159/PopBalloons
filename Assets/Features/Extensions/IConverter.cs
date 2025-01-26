namespace Features.Extensions
{
    /// <summary>
    /// Interface for converting objects differnt types
    /// </summary>
    public interface IConverter<T, G>
    {
        /// <summary>
        /// Convert from T to G
        /// </summary>
        G ConvertTo(T converting);

        /// <summary>
        /// Convert from G to T
        /// </summary>
        T ConvertFrom(G converting);
    }
}
