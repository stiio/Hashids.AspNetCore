using System.Collections;
using System.Text.Json;
using System.Text.Json.Serialization;
using HashidsNet.AspNetCore.Extensions;

namespace HashidsNet.AspNetCore.Json.SystemText;

// Add converter for enumerable items: https://stackoverflow.com/a/67082032
internal class HashidsJsonConverterFactory : JsonConverterFactory
{
    public override bool CanConvert(Type typeToConvert)
    {
        var targetType = GetTargetType(typeToConvert);
        return targetType == typeof(int)
               || targetType == typeof(int?)
               || targetType == typeof(long)
               || targetType == typeof(long?);
    }

    public override JsonConverter? CreateConverter(Type typeToConvert, JsonSerializerOptions options)
    {
        var converter = CreateConverterForType(typeToConvert);
        if (converter != null)
        {
            return converter;
        }

        var (itemType, isArray, isSet) = GetItemType(typeToConvert);
        if (itemType == null)
        {
            throw new NotImplementedException();
        }

        var itemConverter = CreateConverterForType(itemType);
        if (itemConverter == null)
        {
            throw new NotImplementedException();
        }

        if (isArray)
        {
            return (JsonConverter)Activator
                .CreateInstance(
                    typeof(ArrayItemConverterDecorator<>).MakeGenericType(itemType),
                    new object[] { itemConverter })!;
        }

        if (!typeToConvert.IsAbstract && !typeToConvert.IsInterface && typeToConvert.GetConstructor(Type.EmptyTypes) != null)
        {
            return (JsonConverter)Activator
                .CreateInstance(
                    typeof(ConcreteCollectionItemConverterDecorator<,,>).MakeGenericType(typeToConvert, typeToConvert, itemType),
                    new object[] { itemConverter })!;
        }

        if (isSet)
        {
            var setType = typeof(HashSet<>).MakeGenericType(itemType);
            if (typeToConvert.IsAssignableFrom(setType))
            {
                return (JsonConverter)Activator
                    .CreateInstance(
                        typeof(ConcreteCollectionItemConverterDecorator<,,>).MakeGenericType(setType, typeToConvert, itemType),
                        new object[] { itemConverter })!;
            }
        }
        else
        {
            var listType = typeof(List<>).MakeGenericType(itemType);
            if (typeToConvert.IsAssignableFrom(listType))
            {
                return (JsonConverter)Activator
                    .CreateInstance(
                        typeof(ConcreteCollectionItemConverterDecorator<,,>).MakeGenericType(listType, typeToConvert, itemType),
                        new object[] { itemConverter })!;
            }
        }

        // OK it's not an array and we can't find a parameter-less constructor for the type. We can serialize, but not deserialize.
        return (JsonConverter)Activator
            .CreateInstance(
                typeof(EnumerableItemConverterDecorator<,>).MakeGenericType(typeToConvert, itemType),
                new object[] { itemConverter })!;
    }

    private static JsonConverter? CreateConverterForType(Type typeToConvert)
    {
        if (typeToConvert == typeof(int))
        {
            return Activator.CreateInstance<HashidsIntJsonConverter>();
        }
        else if (typeToConvert == typeof(int?))
        {
            return Activator.CreateInstance<HashidsIntNullableJsonConverter>();
        }
        else if (typeToConvert == typeof(long))
        {
            return Activator.CreateInstance<HashidsLongJsonConverter>();
        }
        else if (typeToConvert == typeof(long?))
        {
            return Activator.CreateInstance<HashidsLongNullableJsonConverter>();
        }

        return null;
    }

    private static Type? GetTargetType(Type type)
    {
        var targetType = type;

        if (targetType.IsArray)
        {
            targetType = targetType.GetElementType();
        }

        Type? enumerableType = null;
        foreach (var iType in type.GetInterfacesAndSelf())
        {
            if (!iType.IsGenericType)
            {
                continue;
            }

            var genType = iType.GetGenericTypeDefinition();
            if (genType != typeof(IEnumerable<>))
            {
                continue;
            }

            var thisItemType = iType.GetGenericArguments()[0];
            if (enumerableType != null && enumerableType != thisItemType)
            {
                return null;
            }

            enumerableType = thisItemType;
        }

        targetType = enumerableType ?? targetType;

        return targetType!;
    }

    private static (Type? ItemType, bool IsArray, bool IsSet) GetItemType(Type type)
    {
        // Quick reject for performance
        // Dictionary is not implemented.
        if (type.IsPrimitive || type == typeof(string) || typeof(IDictionary).IsAssignableFrom(type))
        {
            return (null, false, false);
        }

        if (type.IsArray)
        {
            return type.GetArrayRank() == 1 ? (type.GetElementType(), true, false) : (null, false, false);
        }

        Type? itemType = null;
        bool isSet = false;
        foreach (var iType in type.GetInterfacesAndSelf())
        {
            if (iType.IsGenericType)
            {
                var genType = iType.GetGenericTypeDefinition();
                if (genType == typeof(ISet<>))
                {
                    isSet = true;
                }
                else if (genType == typeof(IEnumerable<>))
                {
                    var thisItemType = iType.GetGenericArguments()[0];
                    if (itemType != null && itemType != thisItemType)
                    {
                        return (null, false, false); // type implements multiple enumerable types simultaneously.
                    }

                    itemType = thisItemType;
                }
                else if (genType == typeof(IDictionary<,>))
                {
                    return (null, false, false);
                }
            }
        }

        return (itemType, false, isSet);
    }

    private abstract class CollectionItemConverterDecoratorBase<TEnumerable, TItem> : JsonConverter<TEnumerable>
        where TEnumerable : IEnumerable<TItem>
    {
        private readonly JsonConverter<TItem> innerConverter;

        protected CollectionItemConverterDecoratorBase(JsonConverter<TItem> converter)
        {
            this.innerConverter = converter;
        }

        public sealed override void Write(Utf8JsonWriter writer, TEnumerable value, JsonSerializerOptions options)
        {
            writer.WriteStartArray();
            foreach (var item in value)
            {
                this.innerConverter.Write(writer, item, options);
            }

            writer.WriteEndArray();
        }

        protected TCollection BaseRead<TCollection>(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            where TCollection : ICollection<TItem>, new()
        {
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException(); // Unexpected token type.  JsonTokenType.Null is handled by the framework, unless we set HandleNull => true (which we didn't).
            }

            var list = new TCollection();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    break;
                }

                var item = this.innerConverter.Read(ref reader, typeof(TItem), options);
                list.Add(item!);
            }

            return list;
        }
    }

    private sealed class ArrayItemConverterDecorator<TItem> : CollectionItemConverterDecoratorBase<TItem[], TItem>
    {
        public ArrayItemConverterDecorator(JsonConverter<TItem> converter)
            : base(converter)
        {
        }

        public override TItem[] Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => this.BaseRead<List<TItem>>(ref reader, typeToConvert, options).ToArray();
    }

    private sealed class ConcreteCollectionItemConverterDecorator<TCollection, TEnumerable, TItem> : CollectionItemConverterDecoratorBase<TEnumerable, TItem>
        where TCollection : ICollection<TItem>, TEnumerable, new()
        where TEnumerable : IEnumerable<TItem>
    {
        public ConcreteCollectionItemConverterDecorator(JsonConverter<TItem> converter)
            : base(converter)
        {
        }

        public override TEnumerable Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => this.BaseRead<TCollection>(ref reader, typeToConvert, options);
    }

    private sealed class EnumerableItemConverterDecorator<TEnumerable, TItem> : CollectionItemConverterDecoratorBase<TEnumerable, TItem>
        where TEnumerable : IEnumerable<TItem>
    {
        public EnumerableItemConverterDecorator(JsonConverter<TItem> converter)
            : base(converter)
        {
        }

        public override TEnumerable Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) => throw new NotImplementedException($"Deserialization is not implemented for type {typeof(TEnumerable)}");
    }
}