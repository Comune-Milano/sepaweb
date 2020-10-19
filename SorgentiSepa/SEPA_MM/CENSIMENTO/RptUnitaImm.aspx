<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RptUnitaImm.aspx.vb" Inherits="CENSIMENTO_RptUnitaImm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Report Unità Immobiliare</title>
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
<body style="background-image: url('../NuoveImm/SfondoMascheraContratti.jpg'); background-repeat: no-repeat">
    <form id="form1" runat="server">
    <div style="width: 780px; height: 600px">
        <table width="100%" cellpadding="0" cellspacing="0">
            <tr style="height: 28px; vertical-align: bottom;">
                <td style="width: 2%;">
                </td>
                <td style="width: 98%;">
                    <asp:Label ID="lbltitolo" runat="server" Text="Ricerca Report Unità Immobiliari"
                        Font-Bold="True" ForeColor="#801F1C" Font-Size="14" Font-Names="Arial"></asp:Label>
                </td>
            </tr>
            <tr style="height: 20px">
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
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:Label ID="lblcomplesso" runat="server" Text="Complesso" Font-Names="Arial" Font-Size="8"></asp:Label>
                            </td>
                            <td colspan="7">
                                <asp:DropDownList ID="ddlcomplesso" runat="server" Font-Names="Arial" Font-Size="8"
                                    Width="510" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lbledificio" runat="server" Text="Edificio" Font-Names="Arial" Font-Size="8"></asp:Label>
                            </td>
                            <td colspan="7">
                                <asp:DropDownList ID="ddledificio" runat="server" Font-Names="Arial" Font-Size="8"
                                    Width="510" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblindirizzo" runat="server" Text="Indirizzo" Font-Size="8" Font-Names="Arial"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlindirizzo" runat="server" Font-Names="Arial" Font-Size="8"
                                    Width="250" AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblcivico" runat="server" Text="Civico" Font-Names="Arial" Font-Size="8"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlcivico" runat="server" Font-Names="Arial" Font-Size="8"
                                    Width="70px" AutoPostBack="True" Height="16px">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblscala" runat="server" Text="Scala" Font-Names="Arial" Font-Size="8"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlscala" runat="server" Font-Names="Arial" Font-Size="8" Width="70"
                                    AutoPostBack="True">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="lblinterno" runat="server" Text="Interno" Font-Names="Arial" Font-Size="8"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlinterno" runat="server" Font-Names="Arial" Font-Size="8"
                                    Width="70">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblQuartiere" runat="server" Text="Quartiere" Font-Size="8" Font-Names="Arial"></asp:Label>
                            </td>
                            <td colspan="7">
                                <asp:DropDownList ID="ddlquartieri" runat="server" Font-Names="arial" Font-Size="8"
                                    Width="250px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 10%">
                                            <asp:Label ID="lblascensore" runat="server" Text="Ascensore" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                        <td style="width: 15%">
                                            <asp:DropDownList ID="cmbAscensore" runat="server" Font-Names="Arial" Font-Size="8"
                                                Width="70px" AutoPostBack="True" Height="16px">
                                                <asp:ListItem Value="-1">- - -</asp:ListItem>
                                                <asp:ListItem Value="1">SI</asp:ListItem>
                                                <asp:ListItem Value="0">NO</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Label ID="lblcondominio" runat="server" Text="Condominio" Font-Names="Arial"
                                                Font-Size="8"></asp:Label>
                                        </td>
                                        <td style="width: 15%">
                                            <asp:DropDownList ID="cmbcondominio" runat="server" Font-Names="Arial" Font-Size="8"
                                                Width="70px" AutoPostBack="True" Height="16px">
                                                <asp:ListItem Value="-1">- - -</asp:ListItem>
                                                <asp:ListItem Value="1">SI</asp:ListItem>
                                                <asp:ListItem Value="0">NO</asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td style="width: 10%">
                                            <asp:Label ID="lblstruttura" runat="server" Text="Struttura" Font-Names="Arial" Font-Size="8"></asp:Label>
                                        </td>
                                        <td style="width: 40%">
                                            <asp:DropDownList ID="ddlstruttura" runat="server" Font-Size="8" Font-Names="Arial"
                                                Width="350px">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 34%;">
                                            <center>
                                                <asp:Label ID="lbltipologia" runat="server" Text="Tipologia" Font-Size="8" Font-Names="Arial"></asp:Label>
                                            </center>
                                        </td>
                                        <td style="width: 33%;">
                                            <center>
                                                <asp:Label ID="lbldisponibilita" runat="server" Text="Disponibilità" Font-Size="8"
                                                    Font-Names="Arial"></asp:Label>
                                            </center>
                                        </td>
                                        <td style="width: 33%;">
                                            <center>
                                                <asp:Label ID="lbldestuso" runat="server" Text="Dest. Uso" Font-Size="8" Font-Names="Arial"></asp:Label>
                                            </center>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div style="width: 100%; height: 175px; overflow: auto;">
                                                <asp:CheckBoxList ID="cbltipologia" runat="server" Font-Names="Arial" Font-Size="8pt">
                                                </asp:CheckBoxList>
                                            </div>
                                        </td>
                                        <td>
                                            <div style="width: 100%; height: 175px; overflow: auto;">
                                                <asp:CheckBoxList ID="cbldisponibilita" runat="server" Font-Names="Arial" Font-Size="8pt">
                                                </asp:CheckBoxList>
                                            </div>
                                        </td>
                                        <td>
                                            <div style="width: 100%; height: 175px; overflow: auto;">
                                                <asp:CheckBoxList ID="cbldestuso" runat="server" Font-Names="Arial" Font-Size="8pt">
                                                </asp:CheckBoxList>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <center>
                                                <asp:Button ID="btntipologia" runat="server" Text="Seleziona/Deseleziona" Font-Names="Arial"
                                                    Font-Size="8" /></center>
                                        </td>
                                        <td>
                                            <center>
                                                <asp:Button ID="btndisponibilita" runat="server" Text="Seleziona/Deseleziona" Font-Names="Arial"
                                                    Font-Size="8" /></center>
                                        </td>
                                        <td>
                                            <center>
                                                <asp:Button ID="btndestuso" runat="server" Text="Seleziona/Deseleziona" Font-Names="Arial"
                                                    Font-Size="8" /></center>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 9%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 14%">
                                            <asp:CheckBox ID="Cbchiusi" runat="server" Text="Chiusi" Font-Names="Arial" Font-Size="8pt"
                                                AutoPostBack="True" />
                                        </td>
                                        <td style="width: 77%">
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 100px">
                                                        <asp:TextBox ID="txtdatachiusi" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Width="80px" Visible="False"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBoxList ID="cblstatochiusi" runat="server" Font-Names="arial" 
                                                            Font-Size="8pt" RepeatDirection="Horizontal" Visible="False">
                                                            <asp:ListItem Value="1">Regolari</asp:ListItem>
                                                            <asp:ListItem Value="2">Abusivi</asp:ListItem>
                                                            <asp:ListItem Value="3">Senza Titolo</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblContratti" runat="server" Text="Contratti" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="cbaperti" runat="server" Font-Names="Arial" Font-Size="8" Text="Aperti"
                                                AutoPostBack="True" />
                                        </td>
                                        <td>
                                            <table width="100%" cellpadding="0" cellspacing="0">
                                                <tr>
                                                    <td style="width: 100px">
                                                        <asp:TextBox ID="txtdataaperti" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Width="80px" Visible="False"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:CheckBoxList ID="cblstatoaperti" runat="server" Font-Names="arial" 
                                                            Font-Size="8pt" RepeatDirection="Horizontal" Visible="False">
                                                            <asp:ListItem Value="1">Regolari</asp:ListItem>
                                                            <asp:ListItem Value="2">Abusivi</asp:ListItem>
                                                            <asp:ListItem Value="3">Senza Titolo</asp:ListItem>
                                                        </asp:CheckBoxList>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:CheckBox ID="cbbozza" runat="server" Font-Names="Arial" Font-Size="8" Text="In Bozza"
                                                AutoPostBack="True" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtdatabozza" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                Visible="False" Width="80px"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td colspan = "2">
                                            <asp:CheckBox ID="cbsolofiltrati" runat="server" Font-Names="Arial" 
                                                Font-Size="8pt" Text="Visualizza tutti i contratti" Font-Bold="True" />
                                        </td>
                                        <td>
                                            &nbsp;</td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                <table width="100%" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td style="width: 50%;">
                                            &nbsp;</td>
                                        <td style="width: 50%;">
                                            &nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="width: 50%;">
                                            <center>
                                                <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="../NuoveImm/Img_AvviaRicerca.png" />
                                            </center>
                                        </td>
                                        <td style="width: 50%;">
                                            <center>
                                                <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../NuoveImm/Img_Home.png" />
                                            </center>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="selecttipologia" runat="server" Value="0" />
                    <asp:HiddenField ID="selectdisponibilita" runat="server" Value="0" />
                    <asp:HiddenField ID="selectdestuso" runat="server" Value="0" />
                    <asp:HiddenField ID="Controllo" runat="server" Value="0" />
                </td>
            </tr>
        </table>
    </div>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
    </form>
</body>
</html>
