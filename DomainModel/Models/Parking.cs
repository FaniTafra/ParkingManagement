using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainModel.Models
{
    public class Parking
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Neighborhood { get; set; }
        public string OwnerId { get; set; }
        public int UserId { get; set; }
        public User? user  { get; set; }   
        public DateTime ParkingFrom { get; set; }
        public DateTime ParkingTo { get; set; }
        public ParkingStatus Status { get; set; }

    }
}
