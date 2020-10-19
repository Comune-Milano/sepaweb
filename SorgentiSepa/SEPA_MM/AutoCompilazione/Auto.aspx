<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Auto.aspx.vb" Inherits="Auto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Compilazione domanda ERP</title>
</head>
<body background="Immagini/Sfondo.gif">
    <form id="form1" runat="server">
    <div>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="text-align: center; border-right: #cc0000 1px solid; width: 12%;">
                    <img src="Immagini/Milano.gif" /></td>
                <td style="border-bottom: #cc0000 1px solid; text-align: center; width: 91%; border-left-width: 1px; border-left-color: #cc0000;">
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center; border-right: #cc0000 1px solid; border-top: #cc0000 1px solid; width: 12%;" valign="top">
                    <table cellpadding="0" cellspacing="0" style="width: 59px">
                        <tr>
                            <td>
                                <img src="Immagini/LogoComune.gif" /></td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <table cellpadding="0" cellspacing="0" style="background-color: #dadada; width: 173px;">
                        <tr>
                            <td style="width: 545px; height: 19px">
                                <span style="font-size: 11pt; color: #cc0000; font-family: Arial"></span>
                            </td>
                            <td background="Immagini/TabAltoDx.gif" style="font-size: 12pt; width: 78px; font-family: Times New Roman;
                                height: 19px">
                                &nbsp;</td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 545px; height: 19px">
                                &nbsp;<strong><span style="font-size: 11pt; color: #cc0000; font-family: Arial">Link
                                    Utili</span></strong></td>
                            <td style="width: 78px; height: 19px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 545px; height: 19px">
                            </td>
                            <td style="width: 78px; height: 19px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 545px; text-align: left">
                                &nbsp;</td>
                            <td style="width: 78px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 545px; text-align: left">
                                &nbsp;</td>
                            <td style="width: 78px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 545px; text-align: left">
                                &nbsp;</td>
                            <td style="width: 78px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 545px; text-align: left">
                                &nbsp;</td>
                            <td style="width: 78px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 545px">
                                &nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td background="Immagini/TabBassoDx.gif" style="width: 78px">
                                &nbsp;</td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 545px; text-align: left">
                                <asp:HyperLink ID="HyperLink2" runat="server" Font-Names="arial" Font-Size="8pt"
                                    NavigateUrl="~/Portale.aspx" Target="_top">Torna alla pagina principale</asp:HyperLink></td>
                            <td background="Immagini/TabBassoDx.gif" style="width: 78px">
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 545px; text-align: left">
                                &nbsp;</td>
                            <td background="Immagini/TabBassoDx.gif" style="width: 78px">
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 91%; font-size: 12pt; font-family: Times New Roman; text-align: left;" align="center" valign="top">
                    <table width="100%">
                        <tr>
                            <td style="background-image: url(Immagini/BarraSfondo.gif); text-align: left;" valign="middle">
                                <span style="font-size: 14pt; color: #393a3a"> Compilazione Domanda
                                    di Bando E.R.P.<br>
                                    &nbsp;&nbsp;</span></td>
                        </tr>
                    </table>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
                    &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; 
                    <table style="text-align: center" width="98%">
                        <tr>
                            <td style="text-align: left">
                                Questo servizio permette
                                                la compilazione on line della domanda per partecipare al Bando ERP.
                        <br />
                        <br />
                                E’ possibile inserire
                                                tutte le informazioni relative al nucleo familiare richiedente e
                                        le motivazioni per le quali si presenta domanda per un alloggio ERP.
                        <br />
                        <br />
                                Terminato l’inserimento dei dati,
                                        modificabili e/o integrabili anche in un secondo momento direttamente
                                                presso
                                                    gli uffici comunali in fase di formalizzazione della domanda, sarà possibile stampare una ricevuta
                                                contenente i dati anagrafici del richiedente e il <span style="text-decoration: underline">numero di registrazione</span>. 
                        <br />
                        <br />
                                La ricevuta dovrà
                                                essere presentata agli sportelli del Comune di Milano al momento della validazione
                                        della domanda.
                                        In occasione della formalizzazione della domanda agli&nbsp;
                                                    sportelli comunali sarà possibile rettificare o integrare i dati precedentemente
                                    inseriti.
                        <br />
                        <br />
                                <strong>Il servizio è GRATUITO.<br />
                            <br />
                                </strong><span style="font-family: Arial"><span style="font-size: 10pt"><strong>&nbsp;<br />
                            <br />
                            Modalità di compilazione<br />
                            <br />
                                </strong>
                                <table width="100%" style="font-size: 10pt; font-weight: normal;">
                                    <tr>
                                        <td style="width: 39px">
                                            &nbsp;</td>
                                        <td>
                                        1-Inserire tutti i dati richiesti per la presentazione della domanda;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 39px; height: 18px">
                                            &nbsp;</td>
                                        <td style="height: 18px">
                                        2-Indicare l’indirizzo di posta elettronica presso il quale, alla fine del processo,
                                            ricevere da parte dell’Amministrazione una comunicazione di avvenuta ricezione dei
                                            dati inseriti, che conterrà inoltre le indicazioni operative necessarie per la successiva
                                            formalizzazione della domanda di Bando E.R.P.;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 39px">
                                            &nbsp;</td>
                                        <td>
                                            3-Stampare la ricevuta, che andrà successivamente esibita, contenente gli estremi
                                            del Richiedente ed il <span style="text-decoration: underline">numero di registrazione</span>
                                            e prendere nota di tutte le <span style="text-decoration: underline">indicazioni operative</span>
                                            in essa contenute;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 39px">
                                        </td>
                                        <td>
                                            4-Gli uffici preposti La contatteranno per fissare un appuntamento presso gli uffici
                                            del Comune di Milano, Piazzale Cimitero Monumentale, 14, durante il quale formalizzare la richiesta di partecipazione al bando.</td>
                                    </tr>
                                </table>
                        </span></span>
                                <br />
                                <span style="font-size: 10pt; font-family: Arial"><strong>NOTE<br />
                                </strong>
                                    <br />
                                    Il servizio sarà attivo nel periodo di apertura del bando. L’appuntamento
                            per la validazione potrà avvenire anche successivamente alla data di chiusura del 
                                bando.
                            <br />
                            <br />
                            Qualora il richiedente, successivamente all’inserimento dei dati, intendesse
                            rinunciare alla presentazione formale della domanda di partecipazione al bando ERP,
                            potrà cancellare&nbsp; i propri dati utilizzando la funzione “Cancella Domanda”
                            presente nella sezione “Funzioni”, inserendo il numero di registrazione ottenuto
                            in occasione dell’invio on-line della richiesta.
                            <br />
                            Le domande inserite on-line non formalizzate e non cancellate, verranno automaticamente
                            rimosse alla chiusura del bando. Dal servizio on-line di compilazione della domanda
                            Erp sono esclusi coloro i quali risultino già titolari di una domanda di accesso
                            all’E.R.P. e/o nel caso in cui il nucleo inserito abbia già presentato una domanda
                            di bando E.R.P..
                            <br />
                            <br />
                            <strong><span style="color: #ff3300">La richiesta è considerata priva di valore
                                se non formalizzata di persona da parte del richiedente.
                                <br />
                                <br />
                                <span style="color: #000000">Info: Comune di Milano – Sezione Bandi Erp 
                                    email casa.assegnazione@comune.milano.it</span></span></strong></span></td>
                        </tr>
                    </table>
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp; &nbsp; &nbsp; &nbsp;<br />
                            <table width="100%">
                                <tr>
                                    <td style="text-align: right">
                                <asp:ImageButton ID="imgProsegui" runat="server" ImageUrl="~/AutoCompilazione/Immagini/Procedi.jpg" /></td>
                                </tr>
                            </table>
                </td>
            </tr>
                                </table>
    
    </div>
        <br />
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    </form>
</body>
</html>
