using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dc.net.automation.webservice.gls.model
{
    public class CloseWorkDayByShipmentNumber
    {

        [Required]
        public int NumeroDiSpedizioneGLSDaConfermare { get; set; }

        public CloseWorkDayByShipmentNumber()
        {

        }
    }
}
