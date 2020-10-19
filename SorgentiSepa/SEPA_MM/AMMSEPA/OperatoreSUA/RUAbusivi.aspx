<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RUAbusivi.aspx.vb" Inherits="AMMSEPA_OperatoreSUA_RUAbusivi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Rapporti Utenza Abusivi</title>
    <script type="text/javascript">
        var Uscita;
        Uscita = 0;
        var Selezionato;
        function IMG1_onclick() {
        }
        function AggiungiRU() {
            window.showModalDialog('AggRUAbusivo.aspx', window, 'status:no;dialogWidth:500px;dialogHeight:300px;dialogTop:230;dialogLeft:470;Hide:true;help:no;scroll:no');
        }
        function AggiungiRUDaFile() {
            window.showModalDialog('AggRUAbusivoDaFile.aspx', window, 'status:no;dialogWidth:800px;dialogHeight:600px;dialogTop:230;dialogLeft:470;Hide:true;help:no;scroll:no');
        }
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
                    <asp:Label ID="Label1" runat="server" Text="Rapporti Utenza Abusivi" Style="font-size: 24pt;
                        color: #722615; font-family: Arial; font-weight: 700;"></asp:Label>
                    &nbsp; <span style="font-size: 24pt; color: #801f1c; font-family: Arial"><strong>
                        <asp:Label ID="lblrecord" runat="server" Font-Names="ARIAL" 
                        Font-Size="12pt"></asp:Label></strong></span>
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
                                        <td style="width: 80%">
                                            <div style="width: 100%; overflow: auto; height: 480px;">
                                                <asp:DataGrid ID="dgvruabusivi" runat="server" Font-Names="Arial" AutoGenerateColumns="False"
                                                    Font-Size="8pt" Width="100%" PageSize="25" BackColor="White" Font-Bold="False"
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
                                                        <asp:BoundColumn DataField="CODICE" HeaderText="CODICE CONTRATTO">
                                                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                                Font-Strikeout="False" Font-Underline="False" Width="15%" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                                Font-Strikeout="False" Font-Underline="False" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO">
                                                            <HeaderStyle Width="70%" />
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn DataField="ELABORATO" HeaderText="ELABORATO">
                                                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                                                Width="15%" />
                                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                                                        </asp:BoundColumn>
                                                    </Columns>
                                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" Wrap="False" />
                                                    <AlternatingItemStyle BackColor="Gainsboro" />
                                                    <PagerStyle Mode="NumericPages" />
                                                </asp:DataGrid>
                                            </div>
                                        </td>
                                        <td style="width: 20%; vertical-align: top;">
                                            <asp:ImageButton ID="btnaggiungi" runat="server" ImageUrl="../../NuoveImm/Img_Aggiungi.png"
                                                ToolTip="Aggiungi Rapporto di Utenza Abusivo" OnClientClick="AggiungiRU();" />
                                            <br />
                                            <br />
                                            <asp:ImageButton ID="btnaggiungidafile" runat="server" ImageUrl="../../NuoveImm/AggiungiDaFile.png"
                                                ToolTip="Aggiungi Rapporto di Utenza Abusivo Da File" OnClientClick="AggiungiRUDaFile();" />
                                            <br />
                                            <br />
                                            <asp:ImageButton ID="btnelimina" runat="server" 
                                                ImageUrl="../../NuoveImm/Img_Elimina.png" onclientclick="DeleteConfirm();" />
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
    <asp:HiddenField ID="LBLID" runat="server" Value="0" />
    <asp:HiddenField ID="ConfElimina" runat="server" Value="0" />
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
    </form>
</body>
</html>
