<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaRpt.aspx.vb" Inherits="CALL_CENTER_RicercaRpt" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>

<head id="Head1" runat="server">
    <title>Ricerca Segnalazioni</title>

    <style type="text/css">
        #form1
        {
            width: 781px;
        }
    </style>
    <script type = "text/javascript" language ="javascript" >
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

        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\,]/g
        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
        }
    </script>
    </head>
<body  style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat:no-repeat;">
    <form id="form1" runat="server" >
    <table style="width: 100%;">
        <tr>
            <td>
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                Criteri di ricerca per Report sulle Segnalazioni</strong></span>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" 
                                Font-Size="8pt" Text="Data Inizio" Width="100px"></asp:Label>
                        </td>
                        <td style="width: 120px">
                            <asp:TextBox ID="txtInizio" runat="server" MaxLength="10" Width="70px" onfocus="this.select()" ></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" 
                                Font-Size="8pt" Text="Data Fine" Width="80px"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFine" runat="server" MaxLength="10" Width="70px" onfocus="this.select()" ></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" 
                                Font-Size="8pt" Text="Tipologia Intervento" Width="100px"></asp:Label>
                        </td>
                        <td>

                    <asp:DropDownList 
                        ID="cmbTipo" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="8pt" Height="20px" 
                Style="border: 1px solid black;    width: 300px;" 
                TabIndex="1">
            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" 
                                Font-Size="8pt" Text="Operatore" Width="100px"></asp:Label>
                        </td>
                        <td>

                    <asp:DropDownList 
                        ID="cmbOpSegnalante" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="8pt" Height="20px" 
                Style="border: 1px solid black; width: 300px;" 
                TabIndex="1">

            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" 
                                Font-Size="8pt" Text="Struttura" Width="100px"></asp:Label>
                        </td>
                        <td>

                    <asp:DropDownList 
                        ID="cmbStruttura" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="8pt" Height="20px" 
                Style="border: 1px solid black; width: 300px;" 
                TabIndex="1">

            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblStato" runat="server" Font-Bold="False" Font-Names="Arial" 
                                Font-Size="8pt" Text="Stato Segnalazione" Width="100px"></asp:Label>
                        </td>
                        <td>

                    <asp:DropDownList 
                        ID="cmbstato" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="8pt" Height="20px" 
                Style="border: 1px solid black; width: 300px;" 
                TabIndex="1">

            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblApertoDa" runat="server" Font-Bold="False" Font-Names="Arial" 
                                Font-Size="8pt" Text="Aperto Da" Width="100px"></asp:Label>
                        </td>
                        <td>

                            <asp:TextBox ID="txtApertoDa" runat="server" MaxLength="10" Width="70px" 
                                onfocus="this.select()" ></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td>
                            &nbsp;</td>
                        <td style="text-align: right">
        <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png" 
            ToolTip="Avvia Ricerca" TabIndex="4" />
                        </td>
                        <td>
                            <img alt="" src="../NuoveImm/Img_Home.png" onclick ="document.location.href='pagina_home.aspx'" style ="cursor:pointer" /></td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
    </table>
    </form>

</body>
</html>
