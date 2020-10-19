<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaServizi.aspx.vb"
    Inherits="SATISFACTION_RicercaServizi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="prototype.lite.js"></script>
<script type="text/javascript" src="moo.fx.js"></script>
<script type="text/javascript" src="moo.fx.pack.js"></script>
<script type="text/javascript">
    // Funzione javascript per l'inserimento in automatico degli slash nella data

    function CompletaData(e, obj) {
        // Check if the key is a number
        var sKeyPressed;

        sKeyPressed = (window.event) ? event.keyCode : e.which;

        if (sKeyPressed < 48 || sKeyPressed > 57) {
            if (sKeyPressed != 8 && sKeyPressed != 0) {
                // don't insert last non-numeric character
                if (window.event) {
                    event.keyCode = 0;
                }
                else {
                    e.preventDefault();
                }
            }
        }
        else {
            if (obj.value.length == 2) {
                obj.value += "/";
            }
            else if (obj.value.length == 5) {
                obj.value += "/";
            }
            else if (obj.value.length > 9) {
                var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                if (selText.length == 0) {
                    // make sure the field doesn't exceed the maximum length
                    if (window.event) {
                        event.keyCode = 0;
                    }
                    else {
                        e.preventDefault();
                    }
                }
            }
        }
    }
</script>
<head>
    <title>Ricerca Servizi</title>
</head>
<body style="background-attachment: fixed; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');
    background-repeat: no-repeat; width: 796px;">
    <form id="Form1" runat="server" defaultbutton="btnCerca">
    <asp:HiddenField ID="opID" runat="server" />
    <table>
        <tr>
            <td style="height: 5px">
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTitoloPagina" runat="server" Font-Bold="True" Font-Names="Arial"
                    Font-Size="12pt" ForeColor="Maroon" Text="Ricerca Servizi"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 20%">
                            <asp:Label ID="Label1" runat="server" Font-Size="9pt" Font-Names="Arial" Font-Bold="False"
                                Text="Servizi"></asp:Label>
                        </td>
                        <td style="width: 80%">
                            <asp:DropDownList ID="ddlServizi" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                AutoPostBack="True" Font-Names="arial" Font-Size="9pt" Width="550px">
                                <asp:ListItem Value="1">Servizi di pulizia</asp:ListItem>
                                <asp:ListItem Value="2">Servizi di portierato</asp:ListItem>
                                <asp:ListItem Value="3">Servizi di riscaldamento</asp:ListItem>
                                <asp:ListItem Value="4">Servizi di manutenzione del verde</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Font-Size="9pt" Font-Names="Arial" Font-Bold="False"
                                Text="Domande">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDomande" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="Arial" Font-Size="9pt" Width="550px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Font-Size="9pt" Font-Names="Arial" Font-Bold="False"
                                Text="Risposta">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRisposta" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="Arial" Font-Size="9pt" Width="180px">
                                <asp:ListItem Selected="True">---</asp:ListItem>
                                <asp:ListItem>SI</asp:ListItem>
                                <asp:ListItem Value="AB">AB - Abbastanza</asp:ListItem>
                                <asp:ListItem Value="PC">PC - Poco</asp:ListItem>
                                <asp:ListItem>NO</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Font-Size="9pt" Font-Names="Arial" Font-Bold="False"
                                Text="Valore">
                            </asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlValore" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="Arial" Font-Size="9pt" Width="180px">
                                <asp:ListItem Selected="True">---</asp:ListItem>
                                <asp:ListItem>1</asp:ListItem>
                                <asp:ListItem>2</asp:ListItem>
                                <asp:ListItem>3</asp:ListItem>
                                <asp:ListItem>4</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Font-Size="9pt" Font-Names="Arial" Font-Bold="False"
                                Text="Codice UI"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txt_codUI" runat="server" Width="174px" Font-Names="Arial" 
                                Font-Size="9pt"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label6" runat="server" Font-Size="9pt" Font-Names="Arial" Font-Bold="False"
                                Text="Indirizzo"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlIndirizzi" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="Arial" Font-Size="9pt" Width="300px" AutoPostBack="True">
                                <asp:ListItem Selected="True">---</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label7" runat="server" Font-Size="9pt" Font-Names="Arial" Font-Bold="False"
                                Text="Civico"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCivico" runat="server" BorderStyle="Solid"
                                BorderWidth="1px" Font-Names="Arial" Font-Size="9pt" Width="180px" AutoPostBack="True">
                                <asp:ListItem Selected="True">---</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label9" runat="server" Font-Size="9pt" Font-Names="Arial" Font-Bold="False"
                                Text="Data inserimento dal"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtData1" runat="server" Font-Size="9pt" Font-Names="Arial" Font-Bold="False"
                                Width="70px">
                            </asp:TextBox>
                            <asp:Label ID="Label10" runat="server" Font-Size="9pt" Font-Names="Arial" Font-Bold="False"
                                Style="width: 16px" Text="a"></asp:Label>
                            <asp:TextBox ID="txtData2" runat="server" Font-Size="9pt" Font-Names="Arial" Font-Bold="False"
                                Width="70px">
                            </asp:TextBox>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="erroreDate" runat="server" Font-Names="Arial" Font-Size="9pt" Text="Inserire le date correttamente nel formato gg/mm/aaaa"
                                ForeColor="#CC0000"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblOperatore" runat="server" Font-Size="9pt" Font-Names="Arial" Font-Bold="False"
                                Text="Operatore"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlOperatori" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                Font-Names="Arial" Font-Size="9pt" Width="180px">
                                <asp:ListItem Selected="True">---</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 800px">
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="text-align: right; width: 80%;">
                            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="../NuoveImm/Img_AvviaRicerca.png"
                                ToolTip="Avvia Ricerca" />
                        </td>
                        <td style="width: 20%;">
                            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../NuoveImm/Img_Home.png"
                                ToolTip="Home" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
