<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Anagrafe.aspx.vb" Inherits="Anagrafe" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Anagrafe Popolazione</title>
</head>
<body bgcolor="#f2f4f1">
    <form id="form1" runat="server">
    <div>
        <table width="100%">
            <tr bgcolor="Maroon">
                <td align="center">
                    <strong><span style="font-family: Arial; color: #FFFFFF; background-color: #800000;">
                        SCELTA INDIVIDUO</span></strong>
                </td>
            </tr>
            <tr>
                <td>
                <div style="height: 150px; overflow: auto">
                <asp:RadioButtonList ID="ListaOperatori" runat="server" Font-Names="ARIAL" Font-Size="9pt">
                    </asp:RadioButtonList>
                </div>
                    
                </td>
            </tr>
            <tr>
                <td style="text-align: right" valign="middle">
                    &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center; font-family: ARIAL; font-size: 8pt; font-weight: bold;">
                    <span style="font-size: 8pt; font-family: Arial; font-weight: bold;">Attenzione, il
                        sistema memorizzerà gli estremi delle richieste effettuate alla banca dati dell'anagrafe,
                        compreso il nome dell'operatore attualmente collegato.</span>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    &nbsp; &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    &nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    &nbsp; &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:ImageButton ID="btnAccetto" runat="server" ImageUrl="~/NuoveImm/Img_Accetto_Proseguo.png"
                        ToolTip="Accetto e Proseguo" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                        OnClientClick="window.close();" ToolTip="Annulla e chiudi" />
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="chiamante" runat="server" />
    <asp:HiddenField ID="chiusura" runat="server" />
    <asp:HiddenField ID="indice1" runat="server" />
    </form>
</body>
</html>
