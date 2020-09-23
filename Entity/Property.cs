
using NJsonSchema.Annotations;

using System;
using System.Text.Json.Serialization;

namespace Vedma_backend.Entity
{
    public class Property
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public long CharSheetId { get; set; }
        [JsonIgnore]
        public CharSheet CharSheet { get; set; }
    }
}
