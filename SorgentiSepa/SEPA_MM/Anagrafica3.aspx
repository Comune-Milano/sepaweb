<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Anagrafica3.aspx.vb" Inherits="Anagrafica3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Interrogazione Banca Dati Anagrafe</title>
</head>
<body bgcolor="#f2f4f1">
<div id="caric" style="margin: 0px; background-color: #C0C0C0; width: 100%; height: 100%;
        position: fixed; top: 0px; left: 0px; filter: alpha(opacity='75'); opacity: 0.75;
        background-color: #eeeeee; z-index: 500">
        <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
            margin-top: -48px; background-image: url('Immagini/sfondo2.png');">
            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td valign="middle" align="center">
                        <asp:Image ID="Image2222" runat="server" ImageUrl="..\NuoveImm\load.gif" />
                        <br />
                        <br />
                        <asp:Label ID="lblcarica222" runat="server" Text="caricamento in corso..." Font-Names="Arial"
                            Font-Size="10pt"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;">
            <tr bgcolor="Maroon">
                <td align="center">
                    <strong><span style="font-family: Arial; color: #FFFFFF; background-color: #800000;">
                        INTERROGAZIONE ANAGRAFE - SIPO</span></strong>
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
                <td>
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lblCF" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr style="font-family: ARIAL, Helvetica, sans-serif; font-size: 9pt; font-weight: bold;
                            color: #FFFFFF; background-color: #000099;">
                            <td>
                                MATRICOLA
                            </td>
                            <td>
                                NUMERO FAMIGLIA
                            </td>
                            <td>
                                COGNOME
                            </td>
                            <td>
                                NOME
                            </td>
                            <td>
                                SESSO
                            </td>
                            <td>
                                DATA DI NASCITA
                            </td>
                            <td>
                                INDIRIZZO
                            </td>
                            <td>
                                STATO CIVILE
                            </td>
                            <td>
                                RAPPORTO DI PARENTELA CON IL CAPO FAM.
                            </td>
                            <td>
                                CIVICO
                            </td>
                            <td>
                                CIVICO BARRATO
                            </td>
                            <td>
                                DATI COMPLETI
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblI1" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI2" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI3" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI4" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI5" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI6" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI7" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI8" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI9" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI11" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="9pt"></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblI12" runat="server" Font-Bold="False" Font-Names="arial" Font-Size="9pt"></asp:Label>
                            </td>
                            <td align="center">
                               
                                        <table>
                                            <tr>
                                                <td style="text-align: center">
                                                    <asp:ImageButton ID="ImageButton1" runat="server" 
                                                        ImageUrl="~/NuoveImm/view_ico.png" Height="20px" Width="20px" />
                                                </td>
                                            </tr>
                                        </table>
                                   
                            </td>
                </tr> </table> </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="lblCF0" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt">Nucleo Familiare Completo</asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 100%;">
                        <tr style="font-family: ARIAL, Helvetica, sans-serif; font-size: 9pt; font-weight: bold;
                            color: #FFFFFF; background-color: #000099;">
                            <td>
                                MATRICOLA
                            </td>
                            <td>
                                NUMERO FAMIGLIA
                            </td>
                            <td>
                                COGNOME
                            </td>
                            <td>
                                NOME
                            </td>
                            <td>
                                COD. FISCALE</td>
                            <td>
                                SESSO
                            </td>
                            <td>
                                DATA DI NASCITA
                            </td>
                            <td>
                                STATO CIVILE
                            </td>
                            <td>
                                RAPPORTO DI PARENTELA CON IL CAPO FAM.
                            </td>
                            <td>
                                DICITURA
                            </td>
                            <td>
                                CARTA IDENTITA N.
                            </td>
                            <td>
                                DATA RILASCIO</td>
                            <td>
                                DATA SCADENZA</td>
                            <td>
                                DATA PROROGA</td>
                            <td>
                                COMUNE RILASCIO</td>
                        </tr>
                        <%=TestoFamiglia%>
                    </table>
                </td>
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
    <asp:HiddenField ID="HCfDaCercare" runat="server" />
    </form>
    <script type="text/javascript">
    
    if (document.getElementById('caric')) {
        document.getElementById('caric').style.visibility = 'hidden';
    }
    </script>
</body>
</html>
