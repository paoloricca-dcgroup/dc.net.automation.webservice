using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dc.net.automation.webservice.gls.model
{
    public class ListSped
    {
        [Required]
        [StringLength(2)]
        public string SedeGls { get; set; }

        [Required]
        [StringLength(6)]
        public string CodiceClienteGls { get; set; }

        [Required]
        [StringLength(10)]
        public string PasswordClienteGls { get; set; }

        public ListSped()
        {

        }
    }
}
