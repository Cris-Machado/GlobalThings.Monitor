using System.Text.Json.Serialization;

namespace GlobalThings.Domain.Models.Base
{
    public abstract class ModelBase
    {
        [JsonRequired]
        public string Id { get; set; }
        [JsonIgnore]
        public bool Active { get; set; } = true;
    }
}
