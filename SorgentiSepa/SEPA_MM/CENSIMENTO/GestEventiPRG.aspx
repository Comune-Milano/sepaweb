<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestEventiPRG.aspx.vb" Inherits="CENSIMENTO_GestEventiPRG" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Programmazione Interventi</title>
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
        }
    </style>
</head>
<body style="width: 800px; left: 0px; background-repeat: no-repeat; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');">
    <form id="form1" runat="server">
    <table>
        <tr>
            <td style="height: 10px">
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                Gestione Programmazione interventi</strong></span>
            </td>
        </tr>
    </table>
    <div>
        <table style="width: 96%; margin-left: 10px;">
            <tr>
                <td style="height: 20px">
                </td>
            </tr>
            <tr>
                <td style="width: 80%" valign="top">
                    <div style="overflow: auto; height: 400px; z-index: 100;">
                        <asp:DataGrid ID="DataGrDocGest" runat="server" Font-Names="Arial" AutoGenerateColumns="False"
                            Font-Size="8pt" Width="100%" PageSize="15" BackColor="White" Font-Bold="False"
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                            GridLines="None" BorderColor="Navy" BorderStyle="Solid" BorderWidth="1px" CellPadding="3">
                            <HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="False" BackColor="#F2F5F1"
                                Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="Navy" Wrap="False"></HeaderStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Width="20%" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </asp:BoundColumn>
                            </Columns>
                            <AlternatingItemStyle BackColor="Gainsboro" />
                        </asp:DataGrid>
                    </div>
                </td>
                <td style="width: 20%; height: 20%;" valign="top">
                    <img alt="Aggiungi Tipologia" src="../NuoveImm/Img_Aggiungi.png" style="cursor: pointer;"
                        id="btnAggiungi" onclick="  document.getElementById('InseriMotivazione').style.visibility = 'visible'; document.getElementById('Modificato').value='1';" />
                    <br /><br />
                    <asp:ImageButton ID="btnModifica" runat="server" ImageUrl="../NuoveImm/Img_Modifica.png"
                        ToolTip="Modifica Tipologia" OnClientClick="   if (document.getElementById('LBLID').value != 0) { document.getElementById('InseriMotivazione').style.visibility = 'visible';  document.getElementById('Modificato').value='2';} else{  alert('Nessuna riga selezionata!!'); document.getElementById('Modificato').value='0';}" />
                    <br /><br />
                    <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="../NuoveImm/Img_Elimina.png"
                        ToolTip="Elimina Tipologia" OnClientClick="DeleteConfirm();" />
                </td>
            </tr>
            <tr>
                <td style="width: 80%; height: 30px;" valign="bottom">
                    <asp:TextBox ID="txtmia" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="11pt"
                        Width="624px" BackColor="#F2F5F1" BorderStyle="None" BorderWidth="0px">Nessuna Selezione</asp:TextBox>
                </td>
                <td style="width: 20%" valign="top">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td style="width: 90%;" valign="bottom" align="right">
                    <asp:ImageButton ID="btnHome" runat="server" ImageUrl="../NuoveImm/Img_Home.png"
                        ToolTip="Home" />
                </td>
                <td style="width: 20%" valign="top">
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div id="InseriMotivazione" 
        style="margin: 0px; width: 795px; background-image: url('../NuoveImm/sfondo_grigio.png');
        height: 962px; position: fixed; top: -383px; left: 5px; z-index: 200; visibility: visible;">
        <div style="position: fixed; top: 50%; left: 50%; width: 492px; height: 320px; margin-left: -246px;
            margin-top: -160px; background-image: url('../ImmDiv/SfondoDim1.jpg'); z-index: 300;"
            align="center">
            <table width="460px" style="height: 250px; text-align: center; margin-left: 10px;
                z-index: 400;" align="center">
                <tr>
                    <td style="height: 19px; text-align: left" align="center" valign="middle" colspan="2">
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="height: 40px; text-align: left" align="center" valign="top" colspan="2">
                        <asp:Label ID="lbl_titMotiv" runat="server" Text="Nuova Tipologia" Font-Bold="True"
                            Font-Names="Arial" Font-Size="12pt" Width="386px"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        <span style="font-size: 10pt; font-family: Arial">Descrizione</span>
                    </td>
                    <td style="vertical-align: top; text-align: left;">
                        <asp:TextBox ID="txtMotivazione" runat="server" Font-Names="Arial" Font-Size="10pt"
                            Width="309px" MaxLength="400" TabIndex="1" Height="60px" Font-Bold="True" CssClass="CSSmaiuscolo"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top;">
                        
                    </td>
                    <td style="vertical-align: top; text-align: left;">
                       
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp
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
    <asp:HiddenField ID="LBLID" runat="server" Value="0" />
    <asp:HiddenField ID="Modificato" runat="server" />
    <asp:HiddenField ID="ConfElimina" runat="server" Value="0" />
    <script language="javascript" type="text/javascript">
        if ((document.getElementById('Modificato').value != '2') && (document.getElementById('Modificato').value != '1')) {
            document.getElementById('InseriMotivazione').style.visibility = 'hidden';
        } else {
            document.getElementById('InseriMotivazione').style.visibility = 'visible';
        }
    </script>
    </form>
</body>
</html>

