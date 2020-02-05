using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GraniteHouse.Models
{
    public class TestTypes
    {

        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(1,100)]
        public int Code { get; set; }
        public int SpecialTag { get; set; }

    }
}
