<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DatiRegistrazioneS1.aspx.vb" Inherits="Contratti_DatiRegistrazioneS1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Ricerca Partite Gestionali</title>
    <style type="text/css">
        .bordiCombo
        {
            border: 1px solid #000000;
        }
    </style>
</head>
<body style="background-image: url('../NuoveImm/SfondoMascheraContratti.jpg'); background-repeat: no-repeat;
    background-attachment: fixed">
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table width="100%">
        <tr>
            <td>
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Ricerca 
                Dati Agenzia Entrate </span></strong>
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
                            <asp:Label ID="Label5" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Cod. Contratto</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtCodContr" runat="server" Width="354px" TabIndex="1"></asp:TextBox>
                        </td>
                        <td>
                            <img id="imgFumetto" alt="Ricerca approssimata" style="display: none;"
                                src="../ImmMaschere/alert2_ricercad.gif" />
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
                            <asp:Label ID="lblTipoUI" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Tipologia Unità</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbTipoUI" runat="server" Width="360px" TabIndex="2" Style="border: 1px solid black;"
                                AutoPostBack="True" Font-Names="arial" Font-Size="9pt" 
                                CssClass="bordiCombo">
                            </asp:DropDownList>
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
                            <asp:Label ID="lblTipoContr" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Tipologia Contratto</asp:Label>
                        </td>
                        <td style="height:15px;">
                            <asp:DropDownList ID="cmbTipoContr" runat="server" Width="360px" TabIndex="3" 
                                Style="border: 1px solid black;" Font-Names="arial" Font-Size="9pt" 
                                CssClass="bordiCombo">
                            </asp:DropDownList>
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
                            <asp:Label ID="lblTipoContr0" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Tipologia Posizione</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbTipoPosizione" runat="server" Width="360px" 
                                TabIndex="5" Style="border: 1px solid black;" Font-Names="arial" 
                                Font-Size="9pt" CssClass="bordiCombo">
                            </asp:DropDownList>
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
                            <asp:Label ID="lblTipoContr1" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Modalità Pagamento</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbModoPagamento" runat="server" Width="360px" 
                                TabIndex="6" Style="border: 1px solid black;" Font-Names="arial" 
                                Font-Size="9pt" CssClass="bordiCombo">
                            </asp:DropDownList>
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
                            <asp:Label ID="lblTipoContr2" runat="server" Font-Size="8pt" Font-Names="Arial" Font-Bold="False"
                                TabIndex="-1">Note</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNote" runat="server" Width="354px" TabIndex="1"></asp:TextBox>
                        </td>
                        <td>
                            <asp:CheckBox ID="ChTutti" runat="server" Font-Names="arial" Font-Size="8pt" 
                                Text="Qualsiasi valore inserito" />
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
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
                        <td align="right">
                            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="../NuoveImm/Img_AvviaRicerca.png"
                                ToolTip="Avvia Ricerca" TabIndex="7"  />
                        </td>
                        <td>
                            <img onclick="document.location.href='pagina_home.aspx';" alt="Home" src="../NuoveImm/Img_Home.png"
                                style="cursor: pointer;" title="Torna alla pagine Home" />
                        </td>
                        <td>
                            &nbsp
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
    <asp:HiddenField ID="DivVisible" runat="server" />
    <asp:HiddenField ID="errore" runat="server" Value="0" />
    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
        ForeColor="Red" Style="z-index: 10; left: 17px; position: absolute; top: 451px;
        height: 13px; width: 442px;" Text="Label" Visible="False"></asp:Label>
    </form>
    <script type="text/javascript">

        


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

        function RendiVisibile() {
            document.getElementById('imgFumetto').style.display = 'block';
        }
        function RendiNonVisibile() {
            document.getElementById('imgFumetto').style.display = 'none';
        }
    </script>
</body>
</html>
