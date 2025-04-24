using Shared.Extensions;
using System.ComponentModel;
using System.Reflection;

namespace Shared.Extensions
{
    public static class EnumExtensions
    {
        public static T ToEnumName<T, U>(this U value) where T : Enum where U : Type
        {
            var dict = Enum.GetValues(typeof(T))
                .Cast<T>().ToDictionary(e => e.Convert<T, U>(), e => e);
            return dict.TryGetValue(value, out var name) ? name : throw new ArgumentException($"Invalid value: {value}");
        }

        public static U ToEnumValue<T, U>(this T name) where T : Enum where U : Type
        {
            var dict = Enum.GetValues(typeof(T))
                .Cast<T>().ToDictionary(e => e, e => e.Convert<T, U>());
            return dict.TryGetValue(name, out var value) ? value : throw new ArgumentException($"Invalid name: {name}");
        }

        private static U Convert<T, U>(this T enumValue) where T : Enum where U : Type
        {
            var field = enumValue.GetType().GetField(enumValue.ToString());
            var attribute = field?.GetCustomAttribute<AmbientValueAttribute>();
            return attribute?.Value as U ?? throw new ArgumentException($"Invalid enum conversion from: {enumValue}");
        }
    }
}
