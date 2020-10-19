<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InserimMotivRifiutoAnnAss.aspx.vb"
    Inherits="ASS_InserimMotivRifiutoAnnAss" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Inserimento/Annullamento/Modifica Motivazioni ASS</title>
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



        function ControlloSelezione() {
            if (document.getElementById('LBLID').value = '0') {
                document.getElementById('Modificato').value = '0';
                document.getElementById('InseriMotivazione').style.visibility = 'hidden';
                alert('Nessuna riga selezionata!!');
            }
            else {

                document.getElementById('InseriMotivazione').style.visibility = 'visible'; document.getElementById('Modificato').value = '2';


            }
        }
        

    </script>
    <style type="text/css">
        .CSSmaiuscolo
        {
            text-transform: uppercase;
        }
        .style1
        {
            width: 365px;
        }
    </style>
</head>
<body style="background-image: url(../NuoveImm/SfondoMaschere.jpg); background-repeat: no-repeat;
    left: 0px; top: 0px; position: absolute; width: 660px; height: 540px;">
    <form id="form1" runat="server">
    <table>
        <tr>
            <td style="height: 30px" valign="bottom">
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>Modifica Motivazioni
                    Annullo/Rifiuto Abbinam. Alloggio</strong></span>
            </td>
        </tr>
    </table>
    <div id="InseriMotivazione" style="margin: 0px; width: 100%; background-image: url('../NuoveImm/sfondo_grigio.png');
        height: 100%; position: fixed; top: 5px; left: 5px; visibility: hidden;">
        <div style="position: fixed; top: 50%; left: 50%; width: 492px; height: 320px; margin-left: -246px;
            margin-top: -161px; background-image: url('../ImmDiv/SfondoDim1.jpg');" align="center">
            <table width="460px" style="height: 250px; text-align: center; margin-left: 10px;"
                align="center">
                <tr>
                    <td style="height: 19px; text-align: left" align="center" valign="middle" colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="height: 30px; text-align: left" align="center" valign="top" colspan="2">
                        <asp:Label ID="lbl_titMotiv" runat="server" Text="Nuova Motivazione" Font-Bold="True"
                            Font-Names="Arial" Font-Size="12pt" Width="386px"></asp:Label>
                    </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Times New Roman">
                    <td>
                        <span style="font-size: 10pt; font-family: Arial">Tipo</span>
                    </td>
                    <td style="text-align: left" height="60px">
                        <asp:RadioButtonList ID="rdbTipoMotivazioni" runat="server" Font-Names="Arial" Font-Size="10pt"
                            RepeatDirection="Vertical">
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <span style="font-size: 10pt; font-family: Arial">Motivazione</span>
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtMotivazione" runat="server" Font-Names="Arial" Font-Size="10pt"
                            Width="309px" MaxLength="400" TabIndex="1" Height="61px" TextMode="MultiLine"
                            Font-Bold="True" CssClass="CSSmaiuscolo"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp
                    </td>
                </tr>
                <tr style="font-size: 12pt; font-family: Times New Roman">
                    <td style="text-align: left">
                        &nbsp
                    </td>
                    <td align="left">
                        <asp:CheckBox ID="chkRilev" Text="Motivo Irrilevante" runat="server" Font-Names="Arial"
                            Font-Size="10pt" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left" height="50px">
                        &nbsp;
                    </td>
                    <td style="text-align: right">
                        <asp:ImageButton ID="btn_inserisci" runat="server" TabIndex="2" Style="height: 16px"
                            ImageUrl="../NuoveImm/Img_InserisciVal.png" />
                        <asp:ImageButton ID="btn_chiudi" runat="server" ImageUrl="../NuoveImm/Img_AnnullaVal.png"
                            OnClientClick="document.getElementById('InseriMotivazione').style.visibility='hidden';" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div>
        <table style="width: 96%; margin-left: 15px;">
            <tr>
                <td style="height: 20px">
                </td>
            </tr>
            <tr>
                <td style="width: 85%">
                    <div>
                        <asp:DataGrid ID="DataGrMotivazioni" runat="server" Font-Names="Arial" AutoGenerateColumns="False"
                            Font-Size="8pt" Width="97%" PageSize="8" BackColor="White" Font-Bold="False"
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                            GridLines="None" AllowPaging="True" ShowFooter="True" BorderColor="Navy" BorderStyle="Solid"
                            BorderWidth="1px">
                            <HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="False" BackColor="#F2F5F1"
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="Navy" Wrap="False"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn Visible="False" DataField="ID_TIPO" HeaderText="ID_TIPO">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MOTIVAZIONE" HeaderText="MOTIVAZIONE">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPO" HeaderText="TIPO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="RILEVANTE" HeaderText="IRRILEVANTE">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                            </Columns>
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <AlternatingItemStyle BackColor="Gainsboro" />
                            <PagerStyle Mode="NumericPages" />
                        </asp:DataGrid>
                    </div>
                </td>
                <td style="width: 15%" valign="top">
                    <img alt="Aggiungi Motivazione" src="../NuoveImm/Img_Aggiungi.png" style="cursor: pointer;"
                        id="btnAggiungi" onclick="  document.getElementById('InseriMotivazione').style.visibility = 'visible'; document.getElementById('Modificato').value='1';" />
                    <br />
                    <br />
                    <asp:ImageButton ID="btnModifica" runat="server" ImageUrl="../NuoveImm/Img_Modifica.png"
                        ToolTip="Modifica Motivazione" OnClientClick="   if (document.getElementById('LBLID').value != 0) { document.getElementById('InseriMotivazione').style.visibility = 'visible';  document.getElementById('Modificato').value='2';} else{  alert('Nessuna riga selezionata!!'); document.getElementById('Modificato').value='0';}" />
                    <br />
                    <br />
                    <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="../NuoveImm/Img_Elimina.png"
                        ToolTip="Elimina Motivazione" OnClientClick="DeleteConfirm();" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td style="width: 80%; height: 30px;" valign="bottom" colspan="2">
                    <asp:TextBox ID="txtmia" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="11pt"
                        Width="610px" BackColor="#F2F5F1" BorderStyle="None" BorderWidth="0px">Nessuna Selezione</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 80%; height: 60px;" valign="bottom" align="right">
                    <asp:ImageButton ID="btnHome" runat="server" ImageUrl="../NuoveImm/Img_Home.png"
                        ToolTip="Home" />
                </td>
                <td style="width: 20%" valign="top">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="LBLID" runat="server" Value="0" />
    <asp:HiddenField ID="Modificato" runat="server" />
    <asp:HiddenField ID="ConfElimina" runat="server" Value="0" />
    <script language="javascript" type="text/javascript">
        //        myOpacity = new fx.Opacity('InseriMotivazione', { duration: 200 });

        if ((document.getElementById('Modificato').value != '2') && (document.getElementById('Modificato').value != '1')) {
            document.getElementById('InseriMotivazione').style.visibility = 'hidden';

        } else {

            document.getElementById('InseriMotivazione').style.visibility = 'visible';

        }



        //        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
    </form>
</body>
</html>
