using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace ValetCodingExercise.Entities
{
    public partial class Device
    {
        public int DeviceId { get; set; }
        public int UserId { get; set; }
        public string? Name { get; set; }
        public string? Status { get; set; } 
        public string? Mode { get; set; }

        [JsonIgnore]
        public virtual User? User { get; set; }
    }

    public enum DeviceStatus
    {
        On, Off
    }

    public enum DeviceMode
    {
        Enabled, Disabled
    }
}
