<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatoassegnaStrutturaSUA.aspx.vb"
    Inherits="AMMSEPA_OperatoreSUA_RisultatoassegnaStrutturaSUA" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>RisultatoRicercaD</title>
    <script type="text/javascript">
        var Uscita;
        Uscita = 0;
        var Selezionato;
        function IMG1_onclick() {
        }
    </script>
    <script type="text/javascript">
        function Conferma() {
            var chiediConferma
            chiediConferma = window.confirm("Attenzione... Cambiare la Sede Territoriale per gli utenti selezionati?");
            if (chiediConferma == true) {
                document.getElementById('scelta2').value = '1';
            }
            else {
                document.getElementById('scelta2').value = '0';
            }
        }
        </script>
</head>
<body style="background-color: #f2f5f1">
    <form id="form1" runat="server">
    <div style="position: relative; left: -12px">
        <table style="width: 100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 1%; height: 42px;">
                </td>
                <td style="width: 99%">
                    <asp:Label ID="Label1" runat="server" Text="Elenco Operatori" Style="font-size: 24pt;
                        color: #722615; font-family: Arial; font-weight: 700;"></asp:Label>
                    &nbsp; <span style="font-size: 24pt; color: #801f1c; font-family: Arial"><strong>
                        <asp:Label ID="Label10" runat="server" Font-Names="ARIAL" Font-Size="12pt" Text=""></asp:Label></strong></span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <img alt="sfondo" src="../Immagini/SfondoHome.jpg" height="75px" width="100%" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <table width="100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td style="width: 100%;">
                                <table width="100%">
                                    <tr>
                                        <td style="width: 30%">
                                            <div style="width: 100%; overflow: auto; height: 480px;">
                                                <asp:CheckBoxList ID="ckbutenti" runat="server" CellPadding="5" CellSpacing="5" RepeatColumns="1"
                                                    Font-Names="Arial" Font-Size="10pt">
                                                </asp:CheckBoxList>
                                            </div>
                                        </td>
                                        <td style="width: 70%; vertical-align: top;">
                                            &nbsp;&nbsp;
                                            <asp:Label ID="Label8" runat="server" Font-Names="arial" Font-Size="10pt" Style="position: static"
                                                Text="Nuova Struttura" Width="100px"></asp:Label>
                                            <br />
                                            <span style="font-size: 10pt; font-family: Arial">&nbsp;
                                                <asp:DropDownList ID="cmbFiliale" runat="server" Font-Names="arial" Font-Size="10pt"
                                                    Width="450px" TabIndex="9">
                                                </asp:DropDownList>
                                            <br />
                                            <br />
&nbsp;
                                            </span>
                                            <asp:CheckBox ID="CheckBox1" runat="server" AutoPostBack="True" 
                                                Font-Names="Arial" Font-Size="10pt" Text="Seleziona Tutti" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 30px;">
                                &nbsp;
                                <span style="font-size: 24pt; color: #801f1c; font-family: Arial"><strong>
                        <asp:Label ID="Label11" runat="server" Font-Names="ARIAL" Font-Size="12pt" 
                                    Visible="False"></asp:Label></strong></span>
                            </td>
                            <td style="height: 30px;">
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%">
                                    <tr>
                                        <td style="width: 20%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 20%">
                                            <center>
                                                <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_SalvaGrande.png"
                                                    ToolTip="Visualizza" onclientclick="Conferma()" />
                                            </center>
                                        </td>
                                        <td style="width: 20%">
                                            <center>
                                                <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="../../NuoveImm/Img_NuovaRicerca.png"
                                                    ToolTip="Nuova Ricerca" /></center>
                                        </td>
                                        <td style="width: 20%">
                                            <center>
                                                <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../../NuoveImm/Img_Home.png"
                                                    ToolTip="Home" /></center>
                                        </td>
                                        <td style="width: 20%">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
    <asp:HiddenField ID="scelta2" runat="server" Value="0" />
    </form>
</body>
</html>
