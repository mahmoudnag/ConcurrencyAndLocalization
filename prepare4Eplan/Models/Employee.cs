using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace prepare4Eplan.Models
{
    public class Employee
    {
        public int Id { get; set; } 
        public string ?Name { get; set; }
        public string ?Email { get; set; }
   
        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}
