//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations.Schema;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace HMO.Core.Entity
//{
//    public class Member
//    {
//        public int Id { get; set; }
//        public string Identity { get; set; }
//        public string Name { get; set; }
//        public City City { get; set; }
//        //[ForeignKey("CityId")]
//       public int CityId { get; set; }
//        public string Street { get; set; }
//        public int HouseNumber { get; set; }
//        public DateTime DateOfBirth { get; set; }
//        public string Phone { get; set; }
//        public string MobilePhone { get; set; }
//        public List<Vaccination> Vaccinations { get; set; }
//        public int NumOfVaccinations { get; set; }
//        public DateTime StartOfIll { get; set; }
//        public DateTime EndOfIll { get; set; }
//    }
//}
using HMO.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HMO.Core.Entity
{
    public class Member
    {
        public int Id { get; set; }
        public string Identity { get; set; }
        public string Name { get; set; }
        public int CityId { get; set; }
        public City City { get; set; } // CityId property instead of City property
        public string Street { get; set; }
        public int HouseNumber { get; set; }
        public string DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string MobilePhone { get; set; }
        public List<Vaccination> Vaccinations { get; set; }
        public int NumOfVaccinations { get; set; }
        public string StartOfIll { get; set; }
        public string EndOfIll { get; set; }
    }
}
