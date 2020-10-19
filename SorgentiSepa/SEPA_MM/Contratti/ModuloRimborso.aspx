<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ModuloRimborso.aspx.vb" Inherits="Contratti_ModuloRimborso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
                        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Rimborso crediti</span></strong></td>
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
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="8pt"
                                        Text="Importo Credito"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCredito" runat="server" Font-Names="Arial" Font-Size="9pt"
                                        Width="80px" TabIndex="1" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtCredito"
                                        ErrorMessage="Errore (specificare i decimali, separati da ,)" Font-Bold="True"
                                        Font-Names="ARIAL" Font-Size="8pt"
                                        ValidationExpression="^(-?)\b\d*,\d{2}\b" Font-Overline="False"
                                        Font-Strikeout="False" ForeColor="#CC0000"></asp:RegularExpressionValidator>
                                    <asp:Label ID="Label6" runat="server" Font-Names="arial" Font-Size="8pt"
                                        ForeColor="#CC0000" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" runat="server" Font-Names="arial" Font-Size="8pt"
                                        Text="Data Operazione"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDataOp" runat="server" BorderColor="Black"
                                        BorderStyle="Solid" BorderWidth="1px" TabIndex="2" Width="80px"></asp:TextBox>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                        ControlToValidate="txtDataOp" Display="Dynamic" ErrorMessage="Errore (gg/mm/aaaa)"
                                        Font-Bold="True" Font-Names="arial" Font-Size="8pt" ForeColor="#CC3300"
                                        Style="height: 14px;" TabIndex="-1"
                                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" runat="server" Font-Names="arial" Font-Size="8pt"
                                        Text="Tipo Mod.Restituzione"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbTipo" runat="server" TabIndex="3" AutoPostBack="True"
                                        CausesValidation="True" Width="239px">
                                    </asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
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


                            <tr>
                                <td>&nbsp;</td>
                                <td>
                                    <asp:Label ID="lblAvviso" runat="server" Font-Names="arial" Font-Size="8pt"
                                        ForeColor="#000099" Visible="False"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblerrore" runat="server" Font-Names="arial" Font-Size="8pt"
                                        ForeColor="#CC0000" Visible="False"></asp:Label>
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
                            ToolTip="Procedi" TabIndex="4"
                            OnClientClick="ControllaCampiObb();" />&nbsp;&nbsp;
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
                if (errore == '0') {
                    window.returnValue = '1';
                    document.getElementById('btnProcedi').style.visibility = 'hidden';
                }
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
        <asp:HiddenField ID="errore" runat="server" />
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
