using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace dc.net.automation.webservice.gls.model
{
    public class Parcel
    {
        [Required]
        public int CodiceContrattoGls { get; set; } = 1465;

        [Required]
        [StringLength(35)]
        public string RagioneSociale { get; set; }

        [Required]
        [StringLength(35)]
        public string Indirizzo { get; set; }

        [Required]
        [StringLength(30)]
        public string Localita { get; set; }

        [Required]
        public int Zipcode { get; set; }

        [Required]
        [StringLength(2)]
        public string Provincia { get; set; }

        [StringLength(11)]
        public string Bda { get; set; }

        [Required]
        public int Colli { get; set; }

        [Required]
        public decimal PesoReale { get; set; }

        public decimal ImportoContrassegno { get; set; }

        [StringLength(40)]
        public string NoteSpedizione { get; set; }

        [StringLength(1)]
        public string TipoPorto { get; set; }

        public decimal Assicurazione { get; set; }

        public decimal PesoVolume { get; set; }

        [StringLength(600)]
        public string RiferimentoCliente { get; set; }

        [StringLength(40)]
        public string NoteAggiuntive { get; set; }

        [StringLength(30)]
        public string CodiceClienteDestinatario { get; set; }

        [Required]
        public int TipoCollo { get; set; }

        [StringLength(70)]
        public string Email { get; set; }

        public double Cellulare1 { get; set; }

        [StringLength(50)]
        public string ServiziAccessori { get; set; }

        [StringLength(4)]
        public string ModalitaIncasso { get; set; }

        [StringLength(6)]
        public string DataPrenotazioneGDO { get; set; }

        [StringLength(40)]
        public string OrarioNoteGDO { get; set; }

        [Required]
        public int GeneraPdf { get; set; }

        [StringLength(2)]
        public string FormatoPdf { get; set; }

        public double ContatoreProgressivo { get; set; }

        public int NumDayListSped { get; set; }

        [StringLength(12)]
        public string IdentPIN { get; set; }

        [StringLength(1)]
        public string AssicurazioneIntegrativa { get; set; }

        [StringLength(1)]
        public string TipoSpedizione { get; set; }

        [StringLength(20)]
        public string IdReso { get; set; }

        [StringLength(80)]
        public string AFMIRagioneSocialeMittente { get; set; }

        [StringLength(30)]
        public string AFMIIndirizzoMittente { get; set; }

        [StringLength(30)]
        public string AFMILocalitaMittente { get; set; }

        [StringLength(2)]
        public string AFMIProvinciaMittente { get; set; }

        [StringLength(5)]
        public string AFMIZipCode { get; set; }

        [StringLength(70)]
        public string AFMIEmailMittente { get; set; }

        [StringLength(4)]
        public string SedeMittenteAFMI { get; set; }

        [StringLength(1)]
        public string FermoDeposito { get; set; }

        [StringLength(4)]
        public string SiglaSedeFermoDeposito { get; set; }

        [StringLength(1)]
        public string ResaContrassegno { get; set; }

        public Parcel()
        {

        }
    }
}
