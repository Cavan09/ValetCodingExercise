using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ValetCodingExercise.Entities
{
    public partial class User
    {
        public User()
        {
            Devices = new HashSet<Device>();
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [ForeignKey("DeviceId")]
        public virtual ICollection<Device> Devices { get; set; }
    }
}
