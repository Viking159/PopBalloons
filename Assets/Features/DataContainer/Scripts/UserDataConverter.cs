using Features.Extensions;

namespace Features.DataContainer
{
    public sealed class UserDataConverter : IConverter<string, UserData>
    {
        public UserData ConvertTo(string converting) => new UserData() { Name = converting };

        public string ConvertFrom(UserData converting) => converting == null ? string.Empty : converting.Name;
    }
}
