<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Zone.aspx.vb" Inherits="AMMSEPA_Zone" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Tabella Zone</title>
    <script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
    <script type="text/javascript" src="../Contratti/moo.fx.js"></script>
    <script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>
    <script type="text/javascript">
        var Uscita;
        Uscita = 0;
        var Selezionato;

        function DeleteConfirm() {
            if (document.getElementById('LBLID').value != 0) {
                var Conferma;
                Conferma = window.confirm('Attenzione...Confermi di voler eliminare il dato selezionato?');
                if (Conferma == false) {
                    document.getElementById('ConfElimina').value = '0';
                }
                else {
                    document.getElementById('ConfElimina').value = '1';
                }
            }
            else {
                alert('Nessuna riga selezionata!!');
            }
        }
        function disabilitaMinore(e) {
            var key;
            if (window.event)
                key = window.event.keyCode;     //IE
            else
                key = e.which;     //firefox

            if (key == 226)
                return false;
            else
                return true;
        }
    </script>
</head>
<body style="background-color: #f2f5f1">
    <form id="form1" runat="server">
    <div>
        <table style="width: 100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="width: 1%; height: 42px;">
                </td>
                <td style="width: 99%">
                    <asp:Label ID="Label1" runat="server" Text="Tabella Zone" Style="font-size: 24pt;
                        color: #722615; font-family: Arial; font-weight: 700;"></asp:Label>
                    &nbsp; <span style="font-size: 24pt; color: #801f1c; font-family: Arial"><strong>
                        <asp:Label ID="lblrecord" runat="server" Font-Names="ARIAL" Font-Size="12pt"></asp:Label></strong></span>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <img alt="sfondo" src="Immagini/SfondoHome.jpg" height="75px" width="100%" />
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
                                        <td style="width: 80%">
                                            <div style="width: 100%; overflow: auto; height: 440px;">
                                                <asp:DataGrid ID="DataGrZone" runat="server" Font-Names="Arial" AutoGenerateColumns="False"
                                                    BackColor="#F2F5F1" BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" PageSize="100"
                                                    Width="100%" BorderColor="Navy" BorderStyle="Solid" CellPadding="2">
                                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" Wrap="False" />
                                                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" Wrap="False" />
                                                    <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" Wrap="False" />
                                                    <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" Mode="NumericPages" Wrap="False" />
                                                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                                    <Columns>
                                                        <asp:BoundColumn Visible="False" DataField="COD" HeaderText="COD">
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" Wrap="False" />
                                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" Wrap="False" />
                                                            <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                                ForeColor="#0000C0" Wrap="False" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="ZONA" HeaderText="ZONA">
                                                            <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                                ForeColor="#0000C0" Wrap="False" HorizontalAlign="Left" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="NOMINATIVO" HeaderText="DETTAGLI">
                                                            <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                                ForeColor="#0000C0" Wrap="False" HorizontalAlign="Left" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                                Font-Underline="False" />
                                                        </asp:BoundColumn>
                                                    </Columns>
                                                    <PagerStyle Mode="NumericPages" />
                                                </asp:DataGrid>
                                            </div>
                                        </td>
                                        <td style="vertical-align: top;">
                                            <img alt="Aggiungi Zona" onclick="document.getElementById('TextBox1').value='1';myOpacity.toggle();document.getElementById('btnAggiungi').disabled = 'disabled';document.getElementById('btnModifica').disabled = 'disabled';document.getElementById('btnElimina').disabled = 'disabled';"
                                                src="../NuoveImm/Img_Aggiungi.png" style="cursor: pointer;" id="btnAggiungi" />
                                            <br />
                                            <br />
                                            <asp:ImageButton ID="btnModifica" runat="server" ImageUrl="../NuoveImm/Img_Modifica.png"
                                                ToolTip="Modifica Zona" OnClientClick="document.getElementById('TextBox1').value='2';" />
                                            <br />
                                            <br />
                                            <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="../NuoveImm/Img_Elimina.png"
                                                ToolTip="Elimina Zona" OnClientClick="DeleteConfirm();" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblerrore" runat="server" Font-Bold="True" Font-Names="arial"
                                                Font-Size="9pt" ForeColor="Red"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 80%">
                                            <asp:TextBox ID="txtmia" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
                                                Width="777px" BackColor="#F2F5F1" BorderStyle="None" BorderWidth="0px">Nessuna Selezione</asp:TextBox>
                                        </td>
                                        <td style="width: 20%; vertical-align: top;">
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 30px;">
                                &nbsp;
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
                                            &nbsp;
                                        </td>
                                        <td style="width: 20%">
                                            &nbsp;
                                        </td>
                                        <td style="width: 20%">
                                            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../NuoveImm/Img_Home.png"
                                                ToolTip="Home" />
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
    <div id="InseriZona" style="z-index: 300; position: absolute; top: 143px; left: 255px;
        background-image: url('../ImmDiv/SfondoDim1.jpg'); width: 490px; height: 320px;
        background-repeat: no-repeat;">
        <table border="0" cellpadding="1" cellspacing="1">
            <tr>
                <td width="60px">
                    &nbsp
                </td>
            </tr>
            <tr>
                <td width="60px">
                    &nbsp
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
                <td>
                    <table>
                        <tr style="width: 435px; height: 208px;">
                            <td style="width: 52px; height: 19px; text-align: left">
                                <strong><span style="font-family: Arial">Nuova</span></strong>
                            </td>
                            <td style="width: 274px; height: 19px; text-align: left">
                                <strong><span style="font-family: Arial">Zona</span></strong>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 52px; height: 19px; text-align: left">
                                <span style="font-size: 10pt; font-family: Arial">Nome</span>
                            </td>
                            <td style="width: 274px; height: 19px; text-align: left">
                                <asp:TextBox ID="txtNomeZona" runat="server" Font-Names="Arial" Font-Size="9pt" Width="250px"
                                    TabIndex="1"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="!!"
                                    Display="Dynamic" ControlToValidate="txtNomeZona" ValidationExpression="\d+"
                                    Font-Size="9pt" Font-Bold="True" ForeColor="Red"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr style="font-size: 12pt; font-family: Times New Roman">
                            <td style="width: 52px; height: 19px; text-align: left">
                                <span style="font-size: 10pt; font-family: Arial">Dettagli</span>
                            </td>
                            <td style="width: 274px; height: 19px; text-align: left">
                                <asp:TextBox ID="txtDescrizione" runat="server" Font-Names="Arial" Font-Size="9pt"
                                    Width="250px" TabIndex="1" TextMode="MultiLine" onkeydown="return disabilitaMinore(event)"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 52px; height: 19px">
                            </td>
                            <td align="right" style="width: 274px; height: 19px; text-align: right">
                                <table border="0" cellpadding="1" cellspacing="1" style="width: 110%">
                                    <tr>
                                        <td align="left">
                                            &nbsp
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: right">
                                            <asp:ImageButton ID="img_InserisciSchema" runat="server" ImageUrl="~/NuoveImm/Img_InserisciVal.png"
                                                TabIndex="2" Style="height: 16px" />&nbsp;<asp:ImageButton ID="img_ChiudiSchema"
                                                    runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png" OnClientClick="document.getElementById('InseriZona').style.visibility='hidden';" />
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
    <asp:HiddenField ID="LBLID" runat="server" />
    <asp:HiddenField ID="TextBox1" runat="server" />
    <asp:HiddenField ID="ConfElimina" runat="server" Value="0" />
    <script language="javascript" type="text/javascript">
        myOpacity = new fx.Opacity('InseriZona', { duration: 200 });

        if (document.getElementById('TextBox1').value != '1' && document.getElementById('TextBox1').value != '2') {
            myOpacity.hide();
        }
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
    </form>
</body>
</html>
