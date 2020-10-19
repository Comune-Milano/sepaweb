<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Anagrafe4.aspx.vb" Inherits="Anagrafe4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Interrogazione Banca Dati Anagrafe</title>
    <style type="text/css">
        .style1
        {
            height: 23px;
        }
        .style2
        {
            height: 19px;
        }
    </style>
</head>
<body bgcolor="#f2f4f1">
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
            <tr bgcolor="Maroon">
                <td align="center">
                    <strong><span style="font-family: Arial; color: #FFFFFF; background-color: #800000;">
                        INTERROGAZIONE ANAGRAFE&nbsp; SIPO - DATI COMPLETI</span></strong>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr>
                            <td style="text-align: left" width="50%">
                                <asp:Label ID="lblOperatore" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt"></asp:Label>
                            </td>
                            <td style="text-align: right" width="50%">
                                <asp:Label ID="lblData" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="9pt"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Label ID="lblCF" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"></asp:Label>
                    </td>
            </tr>
            <tr>
                <td>
                    <table style="width:100%;">
                        <tr>
                            <td>
                                <asp:Label ID="lblI2" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Numero di matricola anagrafica</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati1" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI21" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Comune di matrimonio</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati20" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblI3" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Numero di famiglia</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati2" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI22" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Provincia di matrimonio</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati21" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblI4" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Cognome</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati3" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI23" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Cognome e nome del coniuge</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati22" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblI5" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Nome</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati4" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI24" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Data Emigrazione</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati23" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblI6" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Sesso</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati5" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI25" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Comune di emigrazione</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati24" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblI7" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Data di Nascita</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati6" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI26" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Provincia di emigrazione</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati25" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblI8" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Paternità</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati7" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI27" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Data di Decesso</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati26" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblI9" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Maternità</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati8" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI28" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Comune di Decesso</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati27" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style1">
                                <asp:Label ID="lblI10" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Comune di nascita</asp:Label>
                            </td>
                            <td class="style1">
                                <asp:Label ID="lblDati9" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                            <td class="style1">
                                <asp:Label ID="lblI29" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Provincia di Decesso</asp:Label>
                            </td>
                            <td class="style1">
                                <asp:Label ID="lblDati28" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblI11" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Provincia di nascita</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati10" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI30" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Ecopass</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati29" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblI12" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Nazione di nascita</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati11" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI31" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Data di ingresso nella famiglia</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati30" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblI13" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Codice fiscale</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati12" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI32" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Cap</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati31" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblI14" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Flag di validità del codice fiscale</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati13" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI33" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Status Anagrafico</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati32" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="style2">
                                <asp:Label ID="lblI15" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Indirizzo (Via – nome via – civico)</asp:Label>
                            </td>
                            <td class="style2">
                                <asp:Label ID="lblDati14" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                            <td class="style2">
                                <asp:Label ID="lblI36" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Codice della via</asp:Label>
                            </td>
                            <td class="style2">
                                <asp:Label ID="lblDati35" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblI16" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Stato civile</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati15" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI37" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Numero civico della via</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati36" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblI17" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Rapporto di parentela (rispetto al capo famiglia)</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati16" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI38" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Numero civico barrato della via</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati37" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblI18" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Cittadinanza</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati17" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI39" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Status anagrafico secondo la vecchia codifica</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati38" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblI40" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Data Cittadinanza</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati39" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI41" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">INA/SAIA</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati40" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblI19" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Data di matrimonio</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati18" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblI20" runat="server" Font-Bold="False" Font-Names="arial" 
                                    Font-Size="9pt">Data di cessazione del matrimonio</asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblDati19" runat="server" Font-Bold="True" Font-Names="arial" 
                                    Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                &nbsp;</td>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
            <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                        ForeColor="#990000" Visible="False"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="chiamante" runat="server" />
    </form>
</body>
</html>

