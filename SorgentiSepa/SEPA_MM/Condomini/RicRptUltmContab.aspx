<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicRptUltmContab.aspx.vb"
    Inherits="Condomini_RicRptUltmContab" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ricerca Ultima Contabilità</title>
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
    </script>
</head>
<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
    background-repeat: no-repeat;">
    <form id="form1" runat="server">
    <div style="width: 790px; height: 600px">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr style="height: 30px">
                <td style="vertical-align: bottom">
                    <asp:Label ID="lbltitle" runat="server" Text="Ricerca Ultima Contabilità" Font-Names="Arial"
                        Font-Size="14pt" Font-Bold="True" ForeColor="#801F1C"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButtonList ID="rdbLstScelta" runat="server" Font-Names="Arial" Font-Size="8pt"
                        Width="100%" AutoPostBack="True">
                        <asp:ListItem Value="0">Ultima contabilità (solo dati statistici senza importi)</asp:ListItem>
                        <asp:ListItem Value="1">Ultima contabilità (con importi)</asp:ListItem>
                        <asp:ListItem Value="2">Contabilità presenti in un anno di gestione senza importi</asp:ListItem>
                        <asp:ListItem Value="3">Contabilità presenti in un anno di gestione con importi</asp:ListItem>
                        <asp:ListItem Value="4">Contabilità Rendicontate e non Pagate</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <div id="dateRicerca">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="width: 5%">
                                    &nbsp;
                                </td>
                                <td style="width: 20%">
                                    <asp:Label ID="lbldatadal" runat="server" Text="Anno dal" Font-Names="Arial" Font-Size="8pt"
                                        Visible="False"></asp:Label>
                                </td>
                                <td style="width: 20%">
                                    <asp:TextBox ID="txtdatadal" runat="server" Font-Names="Arial" Font-Size="8pt" Width="60px"
                                        MaxLength="4" Visible="False"></asp:TextBox>
                                </td>
                                <td style="width: 20%; text-align: center;">
                                    <asp:Label ID="lbldataal" runat="server" Text="al" Font-Names="Arial" Font-Size="8pt"
                                        Visible="False"></asp:Label>
                                </td>
                                <td style="width: 60%">
                                    &nbsp;
                                    <asp:TextBox ID="txtdataal" runat="server" Font-Names="Arial" Font-Size="8pt" Width="60px"
                                        MaxLength="4" Visible="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 5%">
                                    &nbsp;
                                </td>
                                <td style="width: 10%">
                                    &nbsp;
                                </td>
                                <td style="width: 25%">
                                    &nbsp;
                                </td>
                                <td style="width: 25%; text-align: center;">
                                    &nbsp;
                                </td>
                                <td style="width: 60%">
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 50%">
                                &nbsp; &nbsp;
                            </td>
                            <td style="width: 50%">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 50%">
                                <center>
                                    &nbsp;<asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="../NuoveImm/Img_AvviaRicerca.png" />
                                </center>
                            </td>
                            <td style="width: 50%">
                                <center>
                                    <asp:ImageButton ID="btnHome" runat="server" ImageUrl="~/NuoveImm/Img_Home.png" />
                                    &nbsp;</center>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
