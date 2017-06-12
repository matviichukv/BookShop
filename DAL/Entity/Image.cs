using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    public class Image
    {
        public int ImageId { get; set; }
        [Required]
        public byte[] ImageFile { get; set; }
    }
}
