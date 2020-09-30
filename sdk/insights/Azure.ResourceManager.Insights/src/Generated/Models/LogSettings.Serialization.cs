// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Azure.ResourceManager.Insights.Models
{
    public partial class LogSettings : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            if (Optional.IsDefined(Category))
            {
                writer.WritePropertyName("category");
                writer.WriteStringValue(Category);
            }
            writer.WritePropertyName("enabled");
            writer.WriteBooleanValue(Enabled);
            if (Optional.IsDefined(RetentionPolicy))
            {
                writer.WritePropertyName("retentionPolicy");
                writer.WriteObjectValue(RetentionPolicy);
            }
            writer.WriteEndObject();
        }

        internal static LogSettings DeserializeLogSettings(JsonElement element)
        {
            Optional<string> category = default;
            bool enabled = default;
            Optional<RetentionPolicy> retentionPolicy = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("category"))
                {
                    category = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("enabled"))
                {
                    enabled = property.Value.GetBoolean();
                    continue;
                }
                if (property.NameEquals("retentionPolicy"))
                {
                    retentionPolicy = RetentionPolicy.DeserializeRetentionPolicy(property.Value);
                    continue;
                }
            }
            return new LogSettings(category.Value, enabled, retentionPolicy.Value);
        }
    }
}