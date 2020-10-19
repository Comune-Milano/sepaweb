<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ModuloRimborsoDC.aspx.vb" Inherits="Contratti_ModuloRimborsoDC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <title>Rimborso Crediti</title>
</head>
<body style="background-image: url('../NuoveImm/SfondoStatoDomanda.jpg'); background-repeat: no-repeat; background-attachment: fixed">
    <form id="form1" runat="server">
        <div>
            <br />
            <table style="width: 95%;">
                <tr>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Rimborso 
                Deposito Cauzionale</span></strong></td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                        <asp:Label ID="lblCodice" runat="server" Font-Names="arial"
                            Font-Size="10pt" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;&nbsp;</td>
                    <td>
                        <table style="width: 99%;">
                            <tr>
                                <td>
                                    <asp:Label ID="Label9" runat="server" Font-Names="arial" Font-Size="8pt"
                                        Text="Intestatario/i:"></asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblIntestatario" runat="server" Font-Names="arial"
                                        Font-Size="8pt" Font-Bold="True"></asp:Label>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="8pt"
                                        Text="Importo Credito"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCredito" runat="server" Font-Names="Arial" Font-Size="9pt"
                                        Width="80px" TabIndex="1" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    <asp:Label ID="Label8" runat="server" Font-Names="arial" Font-Size="8pt"
                                        Text="Importo Interessi" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtInteressi" runat="server" Font-Names="Arial" Font-Size="9pt"
                                        Width="80px" TabIndex="2" BorderStyle="Solid" BorderWidth="1px" Enabled="False"
                                        Visible="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="8pt"
                                        Text="Data Operazione"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDataOp" runat="server" BorderColor="Black"
                                        BorderStyle="Solid" BorderWidth="1px" TabIndex="3" Width="80px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    <asp:Label ID="Label5" runat="server" Font-Names="arial" Font-Size="8pt"
                                        Text="Tipo Mod.Restituzione"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbTipo" runat="server" TabIndex="4" AutoPostBack="True"
                                        CausesValidation="True" Width="239px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    <asp:Label ID="lblEstremi" runat="server" Font-Names="arial" Font-Size="8pt"
                                        Text="Estremi" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtEstremi" runat="server" BorderColor="Black"
                                        BorderStyle="Solid" BorderWidth="1px" TabIndex="5" Width="239px"
                                        Visible="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>&nbsp;</td>
                                <td>
                                    <asp:Label ID="lblAvviso" runat="server" Font-Names="arial" Font-Size="8pt"
                                        ForeColor="#000099" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr valign="top">
                                <td>
                                    <asp:Label ID="Label7" runat="server" Font-Names="arial" Font-Size="8pt"
                                        Text="Dati utili per il pagamento"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNote" runat="server" Height="37px" TabIndex="6"
                                        TextMode="MultiLine" Width="243px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblerrore" runat="server" Font-Names="arial" Font-Size="8pt"
                                        ForeColor="#CC0000" Visible="False" Font-Bold="True"></asp:Label>
                                </td>
                                <td>&nbsp; &nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">&nbsp;</td>
                    <td style="text-align: right">
                        <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                            ToolTip="Procedi" TabIndex="7"
                            OnClientClick="window.returnValue = '1';document.getElementById('btnProcedi').style.visibility = 'hidden';"
                            Style="height: 20px" />&nbsp;&nbsp;
                <img onclick="window.returnValue = '0';self.close();" alt="Home" src="../NuoveImm/Img_Home.png"
                    style="cursor: pointer;" title="Torna alla pagine Home" /></td>
                </tr>
            </table>

        </div>
        <script type="text/javascript">

            function ControllaCampiObb() {
                var Credito;
                var dataOp;
                var errore;

                errore = '0';
                document.getElementById('errore').value = '0';
                Credito = document.getElementById('txtCredito').value
                dataOp = document.getElementById('txtDataOp').value

                if (Credito == '') {
                    alert('Inserire il credito!');
                    errore = '1';
                }

                if (dataOp == '') {
                    alert('Inserire la data operazione!');
                    errore = '1';
                }

                document.getElementById('errore').value = errore;

            }


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
        <asp:HiddenField ID="DECORRENZA" runat="server" />
        <asp:HiddenField ID="INTERESSI" runat="server" />
        <asp:HiddenField ID="IDC" runat="server" />
        <asp:HiddenField ID="IDG" runat="server" />
        <asp:HiddenField ID="HFIdVocePF" runat="server" Value="-1" />
        <asp:HiddenField ID="HFidFornitore" runat="server" Value="-1" />
        <asp:HiddenField ID="HFidStruttura" runat="server" Value="-1" />
        <asp:HiddenField ID="HFidVoceInteressi" runat="server" Value="-1" />
        <asp:HiddenField ID="HFdocRestCred" runat="server" Value="-1" />
    </form>
</body>
</html>
