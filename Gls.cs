using System;
using System.Collections.Generic;
using dc.net.automation.webservice.gls.model;
using dc.net.automation.webservice.gls.GlsLabelService;
using System.Web.Services;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using static System.Net.Mime.MediaTypeNames;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Diagnostics.Eventing.Reader;
using System.Diagnostics;

namespace dc.net.automation.webservice.gls
{
    public class Gls
    {
        static void Main(string[] args)
        {

        }
        public Result ListSped()
        {
            Result myResult = new Result();
            try
            {
                /* Convalida i dati ricevuti dal client attraverso l'istanza <myClientParcel> */
                /* Valorizza l'istanza <myClientParcel> con i dati di autenticazione e 
                 * i valori ricevuti dal client */

                ListSped myListSpedRequest = new ListSped();
                myListSpedRequest.SedeGls = "R3";
                myListSpedRequest.CodiceClienteGls = "119563";
                myListSpedRequest.PasswordClienteGls = "R314651122";

                /* Crea l'istanza del Web Service Proxy */
                IlsWebService myGlsWebService = new IlsWebService();

                /* Invoca il metodo ListSped */
                XmlNode myListSpedResult = myGlsWebService.ListSped(
                    myListSpedRequest.SedeGls,
                    myListSpedRequest.CodiceClienteGls,
                    myListSpedRequest.PasswordClienteGls
                    );

                /* Verifica la risposta ricevuta dal Web Service */
                if (myListSpedResult.Name.Equals("DescrizioneErrore"))
                {
                    myResult.Status = "ERR";
                    myResult.Errors.Add(myListSpedResult.InnerText);
                }
                else
                {
                    myResult.Status = "OK";
                    myResult.Data.Add("<ListParcel>" + myListSpedResult.InnerXml + "</ListParcel>");
                }

                /* Restituisce la risposta del Web Service al Client */
                return myResult;

            }
            catch (Exception e)
            {
                myResult.Status = "ERR";
                myResult.Errors.Add(e.Message.ToString());
                return myResult;

            }
        }

        public Result AddParcel(List<Parcel> myClientParcels)
        {
            Result myResult = new Result();

            try
            {
                /* Convalida i dati ricevuti dal client attraverso l'istanza <myClientParcel> */
                List<ValidationResult> results = new List<ValidationResult>();

                foreach (Parcel parcel in myClientParcels)
                {
                    if (!Validator.TryValidateObject(parcel, new ValidationContext(parcel), results, validateAllProperties: true))
                    {
                        foreach (ValidationResult err in results)
                        {
                            myResult.Errors.Add(err.ErrorMessage);
                        }
                        myResult.Status = "ERR";
                    }
                }
                if (myResult.Status == "ERR")
                {
                    return myResult;
                }
                else
                {
                    /* Valorizza l'istanza <myClientParcel> con i dati di autenticazione e 
                     * i valori ricevuti dal client */

                    Info myInfoRequest = new Info();
                    myInfoRequest.SedeGls = "R3";
                    myInfoRequest.CodiceClienteGls = "119563";
                    myInfoRequest.PasswordClienteGls = "R314651122";
                    myInfoRequest.AddParcelResult = "S";

                    /* Serializza l'istanza <myClientParcel> per trasformarla in un oggetto XML  */
                    XmlSerializer xmlSerializer = new XmlSerializer(myInfoRequest.GetType());
                    StringWriter stringWriter = new System.IO.StringWriter();
                    xmlSerializer.Serialize(stringWriter, myInfoRequest);
                    XmlDocument xmlInfoDocument = new XmlDocument();
                    xmlInfoDocument.LoadXml(stringWriter.ToString());

                    foreach (Parcel parcel in myClientParcels) {

                        XmlDocumentFragment parcelFragment = xmlInfoDocument.CreateDocumentFragment();

                        XmlSerializer xmlParcelSerializer = new XmlSerializer(parcel.GetType());
                        StringWriter stringWriterParcel = new System.IO.StringWriter();
                        xmlParcelSerializer.Serialize(stringWriterParcel, parcel);
                        XmlDocument xmlParcelDocument = new XmlDocument();
                        xmlParcelDocument.LoadXml(stringWriterParcel.ToString());
                        parcelFragment.InnerXml = "<Parcel>" + xmlParcelDocument.DocumentElement.InnerXml + "</Parcel>";

                        xmlInfoDocument.DocumentElement.AppendChild(parcelFragment);

                    }
                    XmlNode xmlInfoNode = xmlInfoDocument.SelectSingleNode("Info");

                    string XMLInfoParcelRequest = xmlInfoNode.OuterXml;

                    /* Crea l'istanza del Web Service Proxy */
                    IlsWebService myGlsWebService = new IlsWebService();

                    /* Invoca il metodo AddParcel */ 
                    XmlNode myInfoLabelResult = myGlsWebService.AddParcel(XMLInfoParcelRequest);

                    /* Verifica la risposta ricevuta dal Web Service */
                    if (myInfoLabelResult.Name.Equals("DescrizioneErrore"))
                    {
                        myResult.Status = "ERR";
                        myResult.Errors.Add(myInfoLabelResult.InnerText);
                    } 
                    else
                    {
                        /* Carica la risposta ricevuta dal Web Service in un XML esplorabile */
                        XmlDocument XMLInfoLabelResult = new XmlDocument();
                        XMLInfoLabelResult.LoadXml("<InfoLabel>" + myInfoLabelResult.InnerXml + "</InfoLabel>");

                        /* Verifica tutte le spedizioni contenute nella risposta */
                        foreach (XmlNode parcel in XMLInfoLabelResult.ChildNodes.Item(0))
                        {
                            /* Verifica se c'è un errore gestito dal Web Service GLS*/
                            if (parcel.Attributes.Count.Equals(0))
                            {
                                myResult.Status = "ERR";
                                myResult.Errors.Add(parcel.SelectNodes("NoteSpedizione").Item(0).InnerText);
                            } else
                            {
                                if (parcel.Attributes["Result"].Value == "Success")
                                {
                                    /* Verifica se l'indirizzo di spedizione è riconosciuto nello stradario GLS */
                                    if (parcel.SelectNodes("DescrizioneSedeDestino").Item(0).InnerText.Equals("GLS Check"))
                                    {
                                        myResult.Status = "ERR";
                                        myResult.Errors.Add("Indirizzo specificato non conforme a stradario GLS.");

                                        /* Elimina la spedizione con indirizzo Errato */
                                        XmlNode myDeleteSpedResult = myGlsWebService.DeleteSped(
                                            myInfoRequest.SedeGls,
                                            myInfoRequest.CodiceClienteGls,
                                            myInfoRequest.PasswordClienteGls,
                                            parcel.SelectNodes("NumeroSpedizione").Item(0).InnerText
                                            );
                                        /* Verifica la risposta ricevuta dal Web Service */
                                        if (myDeleteSpedResult.InnerText.Contains("Eliminazione della spedizione") == false)
                                        {
                                            myResult.Errors.Add(myDeleteSpedResult.InnerText);
                                        }
                                    }
                                    else {
                                        myResult.Status = "OK";
                                        myResult.Data.Add(parcel.OuterXml);
                                    }
                                }
                                else
                                {
                                    myResult.Status = "ERR";
                                    myResult.Errors.Add(XMLInfoLabelResult.DocumentElement.Attributes["WarningDescription"].Value);
                                }
                            }
                        }
                    }
                    /* TODO: Aggiungere pacchetto Request e Result alla risposta inoltrata al Client */
                    
                    
                    /* Restituisce la risposta del Web Service al Client */
                    return myResult;

                }

            } catch(Exception e)
            {
                myResult.Status = "ERR";
                myResult.Errors.Add(e.Message.ToString());
                return myResult;
                
            }
        }
        public Result CloseWorkDayByShipmentNumber(List<CloseWorkDayByShipmentNumber> myClientCloseWorkDayByShipmentNumber)
        {
            Result myResult = new Result();

            try
            {
                /* Convalida i dati ricevuti dal client attraverso l'istanza <myClientParcel> */
                List<ValidationResult> results = new List<ValidationResult>();

                foreach (CloseWorkDayByShipmentNumber parcel in myClientCloseWorkDayByShipmentNumber)
                {
                    if (!Validator.TryValidateObject(parcel, new ValidationContext(parcel), results, validateAllProperties: true))
                    {
                        foreach (ValidationResult err in results)
                        {
                            myResult.Errors.Add(err.ErrorMessage);
                        }
                        myResult.Status = "ERR";
                    }
                }
                if (myResult.Status == "ERR")
                {
                    return myResult;
                }
                else
                {
                    /* Valorizza l'istanza <myClientCloseWorkDayByShipmentNumber> con i 
                     * dati di autenticazione e i valori ricevuti dal client */

                    Info myInfoRequest = new Info();
                    myInfoRequest.SedeGls = "R3";
                    myInfoRequest.CodiceClienteGls = "119563";
                    myInfoRequest.PasswordClienteGls = "R314651122";

                    /* Serializza l'istanza <myClientParcel> per trasformarla in un oggetto XML  */
                    XmlSerializer xmlSerializer = new XmlSerializer(myInfoRequest.GetType());
                    StringWriter stringWriter = new System.IO.StringWriter();
                    xmlSerializer.Serialize(stringWriter, myInfoRequest);
                    XmlDocument xmlInfoDocument = new XmlDocument();
                    xmlInfoDocument.LoadXml(stringWriter.ToString());

                    foreach (CloseWorkDayByShipmentNumber parcel in myClientCloseWorkDayByShipmentNumber)
                    {

                        XmlDocumentFragment parcelFragment = xmlInfoDocument.CreateDocumentFragment();

                        XmlSerializer xmlParcelSerializer = new XmlSerializer(parcel.GetType());
                        StringWriter stringWriterParcel = new System.IO.StringWriter();
                        xmlParcelSerializer.Serialize(stringWriterParcel, parcel);
                        XmlDocument xmlParcelDocument = new XmlDocument();
                        xmlParcelDocument.LoadXml(stringWriterParcel.ToString());
                        parcelFragment.InnerXml = "<Parcel>" + xmlParcelDocument.DocumentElement.InnerXml + "</Parcel>";

                        xmlInfoDocument.DocumentElement.AppendChild(parcelFragment);

                    }
                    XmlNode xmlInfoNode = xmlInfoDocument.SelectSingleNode("Info");

                    string XMLInfoParcelRequest = xmlInfoNode.OuterXml;

                    /* Crea l'istanza del Web Service Proxy */
                    IlsWebService myGlsWebService = new IlsWebService();

                    /* Invoca il metodo AddParcel */
                    XmlNode myInfoLabelResult = myGlsWebService.CloseWorkDayByShipmentNumber(XMLInfoParcelRequest);

                    /* Verifica la risposta ricevuta dal Web Service */
                     if (myInfoLabelResult.FirstChild.InnerText.Equals("OK"))
                    {
                        /* Carica la risposta ricevuta dal Web Service in un XML esplorabile */
                        XmlDocument XMLInfoLabelResult = new XmlDocument();
                        XMLInfoLabelResult.LoadXml("<InfoLabel>" + myInfoLabelResult.InnerXml + "</InfoLabel>");

                        /* Verifica il nodo parcel con l'esito della risposta */
                        foreach(XmlNode parcel in XMLInfoLabelResult.GetElementsByTagName("Parcel"))
                        {
                            if (parcel.SelectNodes("esito").Item(0).InnerText.Equals("OK"))
                            {
                                myResult.Status = "OK";
                                myResult.Data.Add(XMLInfoLabelResult.OuterXml);
                            } else
                            {
                                myResult.Status = "ERR";
                                myResult.Errors.Add(parcel.SelectNodes("esito").Item(0).InnerText);
                            }
                        }
                    } else
                    {
                        myResult.Status = "ERR";
                        myResult.Errors.Add(myInfoLabelResult.FirstChild.InnerText);
                    }

                    /* Restituisce la risposta del Web Service al Client */
                    return myResult;                    
                }

            }
            catch (Exception e)
            {
                myResult.Status = "ERR";
                myResult.Errors.Add(e.Message.ToString());
                return myResult;

            }
        }

        public Result GetPdf(int myContatoreProgressivo)
        {
            Result myResult = new Result();
            try
            {
                /* Convalida i dati ricevuti dal client attraverso l'istanza <myClientParcel> */
                /* Valorizza l'istanza <myClientParcel> con i dati di autenticazione e 
                 * i valori ricevuti dal client */

                GetPdf myGetPdfRequest = new GetPdf();
                myGetPdfRequest.SedeGls = "R3";
                myGetPdfRequest.CodiceCliente = 119563;
                myGetPdfRequest.Password = "R314651122";
                myGetPdfRequest.CodiceContratto = 1465;
                myGetPdfRequest.ContatoreProgressivo = myContatoreProgressivo ;

                /* Crea l'istanza del Web Service Proxy */
                IlsWebService myGlsWebService = new IlsWebService();

                /* Invoca il metodo ListSped */
                Byte[] myGetPdfResult = myGlsWebService.GetPdf(
                    myGetPdfRequest.SedeGls,
                    myGetPdfRequest.CodiceCliente,
                    myGetPdfRequest.Password,
                    myGetPdfRequest.CodiceContratto,
                    myGetPdfRequest.ContatoreProgressivo
                    );

                /* Verifica la risposta ricevuta dal Web Service */
                if (myGetPdfResult != null)
                {
                    myResult.Status = "OK";
                    myResult.PDF = myGetPdfResult;
                }
                else
                {
                    myResult.Status = "ERR";
                    myResult.Errors.Add("Il valore del campo ContatoreProgressivo non risulta valido.");
                }

                /* Restituisce la risposta del Web Service al Client */
                return myResult;

            }
            catch (Exception e)
            {
                myResult.Status = "ERR";
                myResult.Errors.Add(e.Message.ToString());
                return myResult;

            }
        }

        public Result GetZpl(int myContatoreProgressivo)
        {
            Result myResult = new Result();
            try
            {

                GetPdf myGetZplRequest = new GetPdf();
                myGetZplRequest.SedeGls = "R3";
                myGetZplRequest.CodiceCliente = 119563;
                myGetZplRequest.Password = "R314651122";
                myGetZplRequest.CodiceContratto = 1465;
                myGetZplRequest.ContatoreProgressivo = myContatoreProgressivo;

                /* Crea l'istanza del Web Service Proxy */
                IlsWebService myGlsWebService = new IlsWebService();

                /* Invoca il metodo ListSped */
                XmlNode myGetZplResult = myGlsWebService.GetZpl(
                    myGetZplRequest.SedeGls,
                    myGetZplRequest.CodiceCliente,
                    myGetZplRequest.Password,
                    myGetZplRequest.CodiceContratto,
                    myGetZplRequest.ContatoreProgressivo
                    );

                /* Verifica la risposta ricevuta dal Web Service */
                if (myGetZplResult.Name.Equals("DescrizioneErrore"))
                {
                    myResult.Status = "ERR";
                    myResult.Errors.Add(myGetZplResult.InnerText);
                }
                else
                {
                    if (myGetZplResult.InnerXml.Equals(string.Empty))
                    {
                        myResult.Status = "ERR";
                        myResult.Errors.Add("Il valore del campo ContatoreProgressivo non risulta valido.");
                    } else
                    {
                        myResult.Status = "OK";
                        myResult.Data.Add("<Zpl>" + myGetZplResult.InnerXml + "</Zpl>");
                    }
                }

                /* Restituisce la risposta del Web Service al Client */
                return myResult;

            }
            catch (Exception e)
            {
                myResult.Status = "ERR";
                myResult.Errors.Add(e.Message.ToString());
                return myResult;

            }
        }

    }
}
