<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NuovaTipPagamentoRuolo.aspx.vb" Inherits="Contabilita_Report_NuovaTipPagamentoRuolo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestione Tipologia Pagamento</title>
</head>
<body  style="background-image: url(../../NuoveImm/SfondoMascheraContratti.jpg);">
   <form id="form1" runat="server">
    <div>
        <div style="height: 10px;">
        </div>
        <table border="0" cellpadding="0" cellspacing="0">
            <tr>
                <td>
                    <asp:Label ID="Label1" Text="Gestione Tipologie Pagamenti Ruolo" runat="server" Font-Bold="True"
                        Font-Names="Arial" Font-Size="14pt" ForeColor="Maroon" />
                </td>
            </tr>
        </table>
        <div style="height: 15px;">
        </div>
        <div style="height: 450px; vertical-align: top">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td style="width: 20%">
                        <asp:Label ID="Label2" Text="Descrizione tipologia" runat="server" Font-Names="Arial"
                            Font-Size="10pt" />
                    </td>
                    <td style="width: 80%">
                        <asp:TextBox ID="TextBoxDescrizione" runat="server" Width="250px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
        <table border="0" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="width: 80%">
                </td>
                <td style="width: 10%">
                <asp:ImageButton ID="ImageButtonNuova" runat="server" ToolTip="Aggiungi"
                        ImageUrl="../../NuoveImm/NewAggiungi.png" />
                   <asp:ImageButton ID="ImageButtonModifica" runat="server" ToolTip="Modifica" 
                        ImageUrl="../../NuoveImm/NewModifica.png" />
                </td>
                <td style="width: 10%">
                    <asp:ImageButton ID="ImageButtonEsci" runat="server" ToolTip="Esci" 
                        ImageUrl="../../NuoveImm/newEsci.png" />
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
