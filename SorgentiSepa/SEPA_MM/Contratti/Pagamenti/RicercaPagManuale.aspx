<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaPagManuale.aspx.vb"
    Inherits="Contratti_Pagamenti_RicercaPagManuale" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript">
<!--
    var Uscita1;
    Uscita1 = 1;
// -->
</script>
<head id="Head1" runat="server">
    <title>RICERCA</title>
    <style type="text/css">
        #form1
        {
            width: 780px;
        }
    </style>
</head>
<body style="background-image: url('../../NuoveImm/SfondoMascheraContratti.jpg');
    background-repeat: no-repeat; background-attachment: fixed">
    <form id="form1" runat="server" defaultbutton="btnCerca">
    <table style="width: 100%;">
        <tr>
            <td>
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Ricerca Inquilino
                    per procedura Pagamento Manuale <asp:Label ID="lblTitolo" runat="server" Font-Size="14pt"
                        Font-Names="Arial" Font-Bold="True" ForeColor="#801F1C" Text =""></asp:Label></span></strong>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 80%;">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblCognome" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Cognome</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCognome" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Style="" Font-Names="arial" Font-Size="10pt" Width="241px" TabIndex="1"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblCognome1" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Nome</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNome" runat="server" BorderStyle="Solid" BorderWidth="1px" Style=""
                                Font-Names="arial" Font-Size="10pt" Width="241px" TabIndex="2"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblCognome2" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Cod. Fiscale</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCf" runat="server" BorderStyle="Solid" BorderWidth="1px" Style=""
                                Font-Names="arial" Font-Size="10pt" Width="241px" TabIndex="3"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblCognome3" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Rag. Sociale</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRagSociale" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Style="" Font-Names="arial" Font-Size="10pt" Width="241px" TabIndex="4"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblCognome4" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">P. IVA</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtPiva" runat="server" BorderStyle="Solid" BorderWidth="1px" Style=""
                                Font-Names="arial" Font-Size="10pt" Width="241px" TabIndex="5"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblCognome5" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Cod. Contratto</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodContratto" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Style="" Font-Names="arial" Font-Size="10pt" Width="241px" TabIndex="6"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:Label ID="lblCognome6" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Codice U.I.</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodUi" runat="server" BorderStyle="Solid" BorderWidth="1px" Style=""
                                Font-Names="arial" Font-Size="10pt" Width="241px" TabIndex="7"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                                ToolTip="Avvia Ricerca" TabIndex="14" />
                        </td>
                        <td>
                            <img onclick="document.location.href='../pagina_home.aspx';" alt="Home" src="../../NuoveImm/Img_Home.png"
                                style="cursor: pointer;" title="Torna alla pagine Home" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    &nbsp;<br />
    &nbsp;<br />
    <asp:HiddenField ID="DivVisible" runat="server" />
    <asp:HiddenField ID="tipoPagamanto" runat="server" />
    <br />
    <br />
    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
        ForeColor="Red" Style="z-index: 10; left: 13px; position: absolute; top: 324px;
        height: 13px; width: 442px;" Text="Label" Visible="False"></asp:Label>
    <br />
    <br />
    <br />
    &nbsp;
    <br />
    <br />
    <br />
    </form>
</body>
</html>
