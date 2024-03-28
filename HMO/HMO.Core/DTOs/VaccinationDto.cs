using HMO.Core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMO.Core.DTOs
{
    public class VaccinationDto
    {
        public int Id { get; set; }
        //public string Producer { get; set; }
        public DateTime Date { get; set; }
        public ManufacturerDto Manufacturer { get; set; }
        public int ManufacturerId { get; set; }
    }
}
