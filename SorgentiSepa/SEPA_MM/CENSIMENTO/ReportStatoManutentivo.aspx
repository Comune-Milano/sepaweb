<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportStatoManutentivo.aspx.vb" Inherits="CENSIMENTO_ReportStatoManutentivo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Stato Manutentivo</title>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-weight: bold;
        }
        .style2
        {
            font-family: Arial;
        }
        .style3
        {
            width: 297px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table align="center" style="width: 90%;">
        <tr>
            <td class="style1">
                STATO MANUTENTIVO UNITA&#39;&nbsp;
                </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label11" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;</td>
        </tr>
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td class="style3">
                                                Quartiere</td>
                        <td class="style1">
                            <asp:Label ID="Label22" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                                                &nbsp;</td>
                        <td class="style1">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style3">
                                                Zona Decentramento</td>
                        <td class="style1">
                            <asp:Label ID="Label15" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                                                Quartiere</td>
                        <td class="style1">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style3">
                                                Tipologia</td>
                        <td class="style1">
                            <asp:Label ID="Label16" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                                                Numero Locali</td>
                        <td class="style1">
                            <asp:Label ID="Label17" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                                                Numero Servizi</td>
                        <td class="style1">
                            <asp:Label ID="Label18" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                                                Data Sopralluogo di PRE-SLOGGIO</td>
                        <td class="style1">
                            <asp:Label ID="Label1" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                    Data Sopralluogo di SLOGGIO"
                        </td>
                        <td class="style1">
                                                        <asp:Label ID="Label2" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                    Rilevazione Sopralluogo
                        </td>
                        <td class="style1">
                            <asp:Label ID="Label3" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                    Alloggio Riassegnabile
                        </td>
                        <td class="style1">
                            <asp:Label ID="Label4" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                    Porta Blindata
                        </td>
                        <td class="style1">
                            <asp:Label ID="Label5" runat="server"></asp:Label>
&nbsp;<br />
                            <asp:Label ID="Label13" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                    destinata a portatori di Handicap
                        </td>
                        <td class="style1">
                            <asp:Label ID="Label6" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            Motivazioni</td>
                        <td>
                            <asp:Label ID="Label14" runat="server" 
                                style="font-weight: 700; font-family: Arial"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                    Elenco Interventi Manutenzione Programmati
                        </td>
                        <td class="style1">
                            <asp:Label ID="Label9" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            Data Consegna Chiavi alla ditta incaricata</td>
                        <td>
                            <asp:Label ID="Label19" runat="server" 
                                style="font-family: Arial; font-weight: 700"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            Chiavi Consegnate a</td>
                        <td>
                            <asp:Label ID="Label20" runat="server" 
                                style="font-family: Arial; font-weight: 700"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            Data Ripresa Chiavi</td>
                        <td>
                            <asp:Label ID="Label21" runat="server" 
                                style="font-family: Arial; font-weight: 700"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                    Data Presunta di Fine Lavori
                        </td>
                        <td class="style1">
                            <asp:Label ID="Label8" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3">
                            &nbsp;&nbsp;</td>
                        <td>
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style3" >
                    Note
                        </td>
                        <td class="style1">
                            <asp:Label ID="Label7" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="style3" >
                            &nbsp;&nbsp;</td>
                        <td class="style1">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td class="style3" >
                            Milano, li
                            <asp:Label ID="Label12" runat="server"></asp:Label>
                        </td>
                        <td class="style1">
                            &nbsp;</td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </div>
    </form>
</body>
</html>
