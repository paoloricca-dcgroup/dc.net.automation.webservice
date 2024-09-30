using dc.net.automation.webservice.gls.model;
using dc.net.automation.webservice.gls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace dc.net.automation.webservice.glsClient
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void TestAddParcel_Click(object sender, EventArgs e)
        {
            /* Crea l'istanza della richiesta da inviare al Web Service GLS */
            List<Parcel> myAddParcelsList = new List<Parcel>();

            gls.model.Parcel myAddParcelRequest1 = new gls.model.Parcel();

            /* Proprietà obbligatorie  
             * 
                <CodiceContrattoGls> { Valore fisso = 1465 }
                <RagioneSociale>
                <Indirizzo>
                <Localita>
                <Zipcode>
                <Provincia>
                <PesoReale>

             */

            // Valore dinamico Da DDT vendite
            myAddParcelRequest1.RagioneSociale = "Giuseppe Brutti";
            // Valore dinamico Da DDT vendite
            myAddParcelRequest1.Provincia = "RM";
            // Valore dinamico DDT vendite
            myAddParcelRequest1.Localita = "Roma";
            // Valore dinamico DDT vendite
            myAddParcelRequest1.Indirizzo = "Via XYZD,500";
            // Valore dinamico da DDT vendite
            myAddParcelRequest1.Zipcode = 00100;
            // Numero del DDT: Valore dinamico da DDT Vendite
            myAddParcelRequest1.Bda = "0000012347";
            // Numero univoco: Valore dinamico Da DDT vendite
            myAddParcelRequest1.ContatoreProgressivo = 000012346;
            // Fisso
            myAddParcelRequest1.Colli = 1;
            // Fisso
            myAddParcelRequest1.PesoReale = 5;
            // Fisso
            myAddParcelRequest1.PesoVolume = 5;
            // Fisso
            myAddParcelRequest1.TipoCollo = 0;
            // Valore dinamico Da DDT Vendite: totale su singolo collo o suddiviso in parti uguali per ogni collo
            myAddParcelRequest1.ImportoContrassegno = (decimal)25; 
            // Codice Cliente da DDT Vendite
            myAddParcelRequest1.NoteSpedizione = "Test primo invio collo 1";
            // Da DDT Vendite
            myAddParcelRequest1.Cellulare1 = 3480000001;
            // Fisso
            myAddParcelRequest1.ModalitaIncasso = "CONT";
            // Fisso
            myAddParcelRequest1.GeneraPdf = 0;
            // Fisso per spedizioni Nazionali
            myAddParcelRequest1.TipoSpedizione = string.Empty;
            // Fisso
            myAddParcelRequest1.TipoPorto = "F";
            // Fisso
            myAddParcelRequest1.Assicurazione = 0;

            //myAddParcelRequest1.RiferimentoCliente = String.Empty;
            //myAddParcelRequest1.NoteAggiuntive = String.Empty;
            //myAddParcelRequest1.CodiceClienteDestinatario = String.Empty;            
            //myAddParcelRequest1.Email = String.Empty;
            //myAddParcelRequest1.ServiziAccessori = String.Empty;
            //myAddParcelRequest1.DataPrenotazioneGDO = string.Empty;
            //myAddParcelRequest1.OrarioNoteGDO = string.Empty;
            //myAddParcelRequest1.FormatoPdf = string.Empty;
            //myAddParcelRequest1.NumDayListSped = 0;
            //myAddParcelRequest1.IdentPIN = string.Empty;
            //myAddParcelRequest1.AssicurazioneIntegrativa = string.Empty;
            //myAddParcelRequest1.IdReso = string.Empty;

            myAddParcelsList.Add(myAddParcelRequest1);


            gls.model.Parcel myAddParcelRequest2 = new gls.model.Parcel();

            // Valore dinamico Da DDT vendite
            myAddParcelRequest2.RagioneSociale = "Giuseppe Brutti";
            // Valore dinamico Da DDT vendite
            myAddParcelRequest2.Provincia = "RM";
            // Valore dinamico DDT vendite
            myAddParcelRequest2.Localita = "Roma";
            // Valore dinamico DDT vendite
            myAddParcelRequest2.Indirizzo = "Via XYZ,500";
            // Valore dinamico da DDT vendite
            myAddParcelRequest2.Zipcode = 00100;
            // Numero del DDT: Valore dinamico da DDT Vendite
            myAddParcelRequest2.Bda = "0000012347";
            // Numero univoco: Valore dinamico Da DDT vendite
            myAddParcelRequest2.ContatoreProgressivo = 000012347;
            // Fisso
            myAddParcelRequest2.Colli = 1;
            // Fisso
            myAddParcelRequest2.PesoReale = 5;
            // Fisso
            myAddParcelRequest2.PesoVolume = 5;
            // Fisso
            myAddParcelRequest2.TipoCollo = 0;
            // Valore dinamico Da DDT Vendite: totale su singolo collo o suddiviso in parti uguali per ogni collo
            myAddParcelRequest2.ImportoContrassegno = (decimal)25;
            // Codice Cliente da DDT Vendite
            myAddParcelRequest2.NoteSpedizione = "Test primo invio collo 2";
            // Da DDT Vendite
            myAddParcelRequest2.Cellulare1 = 3480000001;
            // Fisso
            myAddParcelRequest2.ModalitaIncasso = "CONT";
            // Fisso
            myAddParcelRequest2.GeneraPdf = 0;
            // Fisso per spedizioni Nazionali
            myAddParcelRequest2.TipoSpedizione = string.Empty;
            // Fisso
            myAddParcelRequest2.TipoPorto = "F";

            myAddParcelRequest2.Assicurazione = 0;

            //myAddParcelsList.Add(myAddParcelRequest2);


            /* Crea l'istanza dell'oggetto result che riceverà i valori di ritorno 
             * provenienti dal Web Service GLS */
            gls.model.Result myAddParcelResult = new gls.model.Result();

            /* Crea l'istanza del connettore GLS */
            gls.Gls myGlsClient = new gls.Gls();

            /* Valorizza i dati ricevuti con la risposta del Web Service GLS*/
            myAddParcelResult = myGlsClient.AddParcel(myAddParcelsList);

            /* Verifica la risposta del Web Service GLS 
             * 
                myAddParcelResult.Status = "OK"   ==> la richiesta è stata inviata e ricevuta correttamente
                myAddParcelResult.Status = "ERR"  ==> la richiesta ha restituito un errore
                myAddParcelResult.Errors = { espone una lista <List> di errori con la relativa descrizione}
                
             */

            Console.WriteLine(myAddParcelResult.Status);
        }

        private void TestListSped_Click(object sender, EventArgs e)
        {
            /* Crea l'istanza della richiesta da inviare al Web Service GLS */
            gls.model.ListSped myListSpedRequest = new gls.model.ListSped();

            /* Crea l'istanza dell'oggetto result che riceverà i valori di ritorno 
             * provenienti dal Web Service GLS */
            gls.model.Result myListSpedResult = new gls.model.Result();

            /* Crea l'istanza del connettore GLS */
            gls.Gls myGlsClient = new gls.Gls();

            /* Valorizza i dati ricevuti con la risposta del Web Service GLS*/
            myListSpedResult = myGlsClient.ListSped();

            /* Verifica la risposta del Web Service GLS 
             * 
                myAddParcelResult.Status = "OK"   ==> la richiesta è stata inviata e ricevuta correttamente
                myAddParcelResult.Status = "ERR"  ==> la richiesta ha restituito un errore
                myAddParcelResult.Errors = { espone una lista <List> di errori con la relativa descrizione}

             */

            Console.WriteLine(myListSpedResult.Status);
        }

        private void TestGetPdf_Click(object sender, EventArgs e)
        {
            /* Crea l'istanza della richiesta da inviare al Web Service GLS */
            gls.model.GetPdf myGetPdfRequest = new gls.model.GetPdf();

            /* Crea l'istanza dell'oggetto result che riceverà i valori di ritorno 
             * provenienti dal Web Service GLS */
            gls.model.Result myGetPdfResult = new gls.model.Result();

            /* Crea l'istanza del connettore GLS */
            gls.Gls myGlsClient = new gls.Gls();

            int myContatoreProgressivo = 0002223;

            /* Valorizza i dati ricevuti con la risposta del Web Service GLS*/
            myGetPdfResult = myGlsClient.GetPdf(myContatoreProgressivo);

            /* Verifica la risposta del Web Service GLS 
             * 
                myAddParcelResult.Status = "OK"   ==> la richiesta è stata inviata e ricevuta correttamente
                myAddParcelResult.Status = "ERR"  ==> la richiesta ha restituito un errore
                myAddParcelResult.Errors = { espone una lista <List> di errori con la relativa descrizione}

             */

            Console.WriteLine(myGetPdfResult.Status);

        }

        private void TestGetZpl_Click(object sender, EventArgs e)
        {
            /* Crea l'istanza della richiesta da inviare al Web Service GLS */
            gls.model.GetZpl myGetZplRequest = new gls.model.GetZpl();

            /* Crea l'istanza dell'oggetto result che riceverà i valori di ritorno 
             * provenienti dal Web Service GLS */
            gls.model.Result myGetZplResult = new gls.model.Result();

            /* Crea l'istanza del connettore GLS */
            gls.Gls myGlsClient = new gls.Gls();

            int myContatoreProgressivo = 000222340;

            /* Valorizza i dati ricevuti con la risposta del Web Service GLS*/
            myGetZplResult = myGlsClient.GetZpl(myContatoreProgressivo);

            /* Verifica la risposta del Web Service GLS 
             * 
                myAddParcelResult.Status = "OK"   ==> la richiesta è stata inviata e ricevuta correttamente
                myAddParcelResult.Status = "ERR"  ==> la richiesta ha restituito un errore
                myAddParcelResult.Errors = { espone una lista <List> di errori con la relativa descrizione}

             */

            Console.WriteLine(myGetZplResult.Status);
        }

        private void TestCloseWorkDay_Click(object sender, EventArgs e)
        {
            /* Crea l'istanza della richiesta da inviare al Web Service GLS */
            List<CloseWorkDayByShipmentNumber> myCloseWorkDayByShipmentNumberList = 
                new List<CloseWorkDayByShipmentNumber>();

            gls.model.CloseWorkDayByShipmentNumber myCloseWorkDayByShipmentNumberRequest1 = 
                new gls.model.CloseWorkDayByShipmentNumber();

            myCloseWorkDayByShipmentNumberRequest1.NumeroDiSpedizioneGLSDaConfermare = 840015550;

            myCloseWorkDayByShipmentNumberList.Add(myCloseWorkDayByShipmentNumberRequest1);

            /* Crea l'istanza dell'oggetto result che riceverà i valori di ritorno 
             * provenienti dal Web Service GLS */
            gls.model.Result myCloseWorkDayByShipmentNumberResult = new gls.model.Result();

            /* Crea l'istanza del connettore GLS */
            gls.Gls myGlsClient = new gls.Gls();

            /* Valorizza i dati ricevuti con la risposta del Web Service GLS*/
            myCloseWorkDayByShipmentNumberResult = myGlsClient.CloseWorkDayByShipmentNumber(myCloseWorkDayByShipmentNumberList);

            /* Verifica la risposta del Web Service GLS 
             * 
                myAddParcelResult.Status = "OK"   ==> la richiesta è stata inviata e ricevuta correttamente
                myAddParcelResult.Status = "ERR"  ==> la richiesta ha restituito un errore
                myAddParcelResult.Errors = { espone una lista <List> di errori con la relativa descrizione}

             */

            Console.WriteLine(myCloseWorkDayByShipmentNumberResult.Status);

        }
    }
}
