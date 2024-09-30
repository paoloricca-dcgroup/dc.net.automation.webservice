using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dc.net.automation.webservice.gls.model
{
    public class GetPdf
    {
        [Required]
        [StringLength(2)]
        public string SedeGls { get; set; }

        [Required]
        [StringLength(6)]
        public int CodiceCliente { get; set; }

        [Required]
        [StringLength(10)]
        public string Password { get; set; }

        [Required]
        [StringLength(4)]
        public int CodiceContratto { get; set; }

        [Required]
        [StringLength(9)]
        public int ContatoreProgressivo { get; set; }

        public GetPdf()
        {

        }
    }
}
