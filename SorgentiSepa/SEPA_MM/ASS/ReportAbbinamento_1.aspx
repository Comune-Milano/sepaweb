<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportAbbinamento_1.aspx.vb" Inherits="ASS_ReportAbbinamento_1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Report</title>
</head>
<body style="text-align: left">
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr>
                <td style="text-align: center">
                    <img src="../IMG/logoComune.bmp" 
                        style="z-index: 100; left: 0px; position: static; top: 0px" /></td>
                <td style="text-align: center">
                    <img src="../IMG/MM_113_84.png" 
                        style="z-index: 100; left: 0px; position: static; top: 0px" /></td>
            </tr>
            <tr>
                <td style="font-family: Arial; font-size: 8pt; font-weight: normal; text-align: center;">
                    Direzione Centrale Casa
                    <br />
                    Settore Assegnazione Alloggi di ERP
                    <br />
                    Servizio Assegnazione Alloggi e Controlli
                    <br />
                    Ufficio Assegnazioni<br />
                    Tel. 02/884.64435 Fax 02/884.66991</td>
                <td style="font-family: Arial; font-size: 8pt; font-weight: normal; text-align: center;" 
                    valign="top">
                    &nbsp;</td>
            </tr>
            </table>
    
    </div>
        <br />
        <table width="100%">
            <tr>
                <td style="text-align: center" width="100%">
                    <strong><span style="font-size: 14pt">
                        <asp:Label ID="Label2" runat="server" Style="z-index: 100; left: 0px; position: static;
                            top: 0px" Text="Label" Width="595px" BorderStyle="Solid" BorderWidth="1px"></asp:Label></span></strong></td>
            </tr>
        </table>
        <br />
        <table style="z-index: 100; left: 0px; position: static; top: 0px" width="100%">
            <tr>
                <td style="height: 21px" width="20%">
                    <span style="font-size: 10pt">CODICE:</span></td>
                <td style="height: 21px" width="30%">
                    <asp:Label ID="lblCodice" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
                <td style="height: 21px" width="15%">
                    <span style="font-size: 10pt">GESTORE</span></td>
                <td style="height: 21px" width="35%">
                    <asp:Label ID="lblGestore" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
            </tr>
            <tr>
                <td width="20%">
                    <span style="font-size: 10pt">PROPRIETA'</span></td>
                <td width="30%">
                    <asp:Label ID="lblProprieta" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
                <td width="15%">
                    <span style="font-size: 10pt">STATO</span></td>
                <td width="35%">
                    <asp:Label ID="lblStato" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
            </tr>
            <tr>
                <td width="20%">
                    <span style="font-size: 10pt">ZONA</span></td>
                <td width="30%">
                    <asp:Label ID="lblZona" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
                <td width="15%">
                </td>
                <td width="35%">
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <span style="font-size: 10pt">TIPOLOGIA</span></td>
                <td width="30%">
                    <asp:Label ID="lblTipologia" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
                <td width="15%">
                </td>
                <td width="35%">
                </td>
            </tr>
            <tr>
                <td style="height: 21px" width="20%">
                    <span style="font-size: 10pt">DATA COMUNICAZIONE</span></td>
                <td style="height: 21px" width="30%">
                    <asp:Label ID="lblComunicazione" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
                <td style="height: 21px" width="15%">
                    <span style="font-size: 10pt">DATA DISPONIBILITA'</span></td>
                <td style="height: 21px" width="35%">
                    <asp:Label ID="lblDisponibile" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
            </tr>
            <tr>
                <td width="20%">
                    <span style="font-size: 10pt">N° LOCALI</span></td>
                <td width="30%">
                    <asp:Label ID="lblLocali" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
                <td width="15%">
                    <span style="font-size: 10pt">SUPERFICIE</span></td>
                <td width="35%">
                    <asp:Label ID="lblSup" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
            </tr>
            <tr>
                <td style="height: 21px" width="20%">
                    <span style="font-size: 10pt">POR</span></td>
                <td style="height: 21px" width="30%">
                    <asp:Label ID="lblCanone" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
                <td style="height: 21px" width="15%">
                </td>
                <td style="height: 21px" width="35%">
                </td>
            </tr>
            <tr>
                <td width="20%">
                    <span style="font-size: 10pt">INDIRIZZO</span></td>
                <td width="30%">
                    <asp:Label ID="lblIndirizzo" runat="server" Style="z-index: 100; left: 0px; position: relative;
                        top: 0px" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
                <td width="15%">
                    <span style="font-size: 10pt">N° ALLOGGIO</span></td>
                <td width="35%">
                    <asp:Label ID="lblAlloggio" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
            </tr>
            <tr>
                <td style="height: 21px" width="20%">
                    <span style="font-size: 10pt">PIANO</span></td>
                <td style="height: 21px" width="30%">
                    <asp:Label ID="lblPiano" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
                <td style="height: 21px" width="15%">
                    <span style="font-size: 10pt">SCALA</span></td>
                <td style="height: 21px" width="35%">
                    <asp:Label ID="lblScala" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
            </tr>
            <tr>
                <td style="height: 21px" width="20%">
                    <span style="font-size: 10pt">ASCENSORE</span></td>
                <td style="height: 21px" width="30%">
                    <asp:Label ID="lblAscensore" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
                <td style="height: 21px" width="15%">
                </td>
                <td style="height: 21px" width="35%">
                </td>
            </tr>
            <tr>
                <td style="height: 21px" width="20%">
                    <span style="font-size: 10pt">CONDIZIONE</span></td>
                <td style="height: 21px" width="30%">
                    <asp:Label ID="lblCondizione" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
                <td style="height: 21px" width="15%">
                    <span style="font-size: 10pt">IDONEO HANDICAP MOTORIO</span></td>
                <td style="height: 21px" width="35%">
                    <asp:Label ID="lblMotorio" runat="server" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Font-Names="arial" Font-Size="10pt"></asp:Label></td>
            </tr>
        </table>
        <br />
        <table width="100%" style="z-index: 100; left: 0px; position: static; top: 0px">
            <tr>
                <td style="text-align: center" width="100%">
                    <strong><span style="font-size: 14pt">DATI OPERATIVI OFFERTA/ASSEGNAZIONE</span></strong></td>
            </tr>
        </table>
        <br />
        <table width="100%">
            <tr>
                <td style="height: 21px" width="20%">
                    PG DOMANDA</td>
                <td style="height: 21px" width="70%">
                    <asp:Label ID="LBLPG" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="30%"></asp:Label></td>
            </tr>
            <tr>
                <td style="height: 21px" width="20%">
                    <span style="font-size: 10pt">NOMINATIVO</span></td>
                <td style="height: 21px" width="70%">
                    <asp:Label ID="LBLNominativo" runat="server" Font-Names="arial" Font-Size="10pt"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Width="94%"></asp:Label></td>
            </tr>
            <tr>
                <td width="20%">
                    <span style="font-size: 10pt">DATA OFFERTA</span></td>
                <td width="70%">
                    <asp:Label ID="lblDataOfferta" runat="server" Font-Names="arial" Font-Size="10pt"
                        Style="z-index: 100; left: 0px; position: static; top: 0px" Width="30%"></asp:Label></td>
            </tr>
            <tr>
                <td width="20%">
                    <span style="font-size: 10pt">SEZIONE</span></td>
                <td width="70%" style="font-size: 12pt">
                    <asp:Label ID="Label5" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="30%">BANDI</asp:Label></td>
            </tr>
        </table>
        <br />
        <table width="100%" style="font-size: 12pt; z-index: 100; left: 0px; position: static; top: 0px">
            <tr>
                <td style="text-align: left" width="100%">
                    <strong><span>
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                            Style="z-index: 100; left: 0px; position: static; top: 0px" Width="98%"></asp:Label></span></strong></td>
            </tr>
        </table>
        <br /><table width="100%" style="font-size: 12pt; z-index: 100; left: 0px; position: static; top: 0px">
            <tr>
                <td style="text-align: center; height: 18px;" width="100%">
                    <strong><span>
                        <br />
                        <span style="font-size: 16pt">ACCETTA [<asp:Label ID="lblAccetta" runat="server"
                            Font-Names="arial" Font-Size="10pt" Style="z-index: 100; left: 0px; position: static;
                            top: 0px" Width="4%"></asp:Label>] &nbsp; NON ACCETTA [<asp:Label ID="lblNonAccetta"
                                runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100; left: 0px;
                                position: static; top: 0px" Width="4%"></asp:Label>]*</span></span></strong></td>
            </tr>
            <tr>
                <td style="height: 18px; text-align: left" width="100%">
                    <br />
        * Motivazione del rifiuto: 
                    <asp:Label ID="lblMotivo" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="72%"></asp:Label><br />
                    <br />
                    Eventuali annotazioni:
                    <asp:Label ID="lblNote" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                        left: 0px; position: static; top: 0px" Width="74%"></asp:Label><br />
                    <br />
                    <br />
            L'Alloggio proposto di cui alla presente offerta.</td>
            </tr>
        </table>
        <br />
        <table width="100%">
            <tr>
                <td style="text-align: left; width: 50%;">
                    <strong><span>
                        <br />
                        Data
                        <asp:Label ID="lblData" runat="server" Font-Names="arial" Font-Size="10pt" Style="z-index: 100;
                            left: 0px; position: static; top: 0px" Width="4%"></asp:Label></span></strong></td>
                <td width="50%" style="text-align: center">
                    Firma</td>
            </tr>
        </table>
    </form>
</body>
</html>
