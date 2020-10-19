<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiLegali.aspx.vb" Inherits="MOROSITA_RisultatiReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<script type="text/javascript" src="Funzioni.js">
<!--
var Uscita1;
Uscita1=1;
// -->
</script>

<head id="Head1" runat="server">
    <title>RISULTATI RICERCA LEGALI MOROSITA</title>
</head>
<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat:no-repeat;">
    <form id="form1" runat="server">
    <div>
    <table style="left: 0px;">
            <tr>
                <td style="width: 800px; height: 51px; left: 0px; position: absolute; top: 0px;">
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <br />
                        &nbsp;&nbsp; Risultati Ricerca n. <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</span></strong><br />
                    <br />
                    <table >
                        <tbody>
                            <tr>
                                <td>
                                    <div style="left: 8px; overflow: auto; width: 776px; top: 56px; height: 320px">
                                        <asp:DataGrid ID="DataGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                            BackColor="White" BorderColor="#000099" BorderWidth="1px" Font-Bold="False" Font-Italic="False"
                                            Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                                            Font-Underline="False" ForeColor="Black" PageSize="1" Style="table-layout: auto;
                                            z-index: 101; left: 0px; clip: rect(auto auto auto auto); direction: ltr;
                                            top: 8px; border-collapse: separate; width: 100%;" Width="1px">
                                            <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                                ForeColor="#0000C0" Wrap="False" />
                                            <Columns>
                                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                                    <HeaderStyle Width="0%" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="COGNOME" HeaderText="COGNOME">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" Width="10%" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="NOME" HeaderText="NOME">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" Width="10%" Wrap="False" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" Width="30%" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="COMUNE" HeaderText="COMUNE">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" Width="10%" Wrap="False" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="CAP" HeaderText="CAP">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Center" Width="5%" Wrap="False" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="TEL_1" HeaderText="TELEFONO 1">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" Width="5%" Wrap="False" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="TEL_2" HeaderText="TELEFONO 2">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" Width="5%" Wrap="False" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="CELL" HeaderText="CELLULARE">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" Width="5%" Wrap="False" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="FAX" HeaderText="FAX">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" Width="5%" Wrap="False" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="EMAIL" HeaderText="E-MAIL">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" Width="5%" Wrap="False" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:BoundColumn DataField="NOTE" HeaderText="NOTE">
                                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" Width="10%" Wrap="False" />
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                                </asp:BoundColumn>
                                                <asp:TemplateColumn HeaderText="Selezione" Visible="False">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                                                            Text="Modifica">Seleziona</asp:LinkButton>
                                                    </ItemTemplate>
                                                    <EditItemTemplate>
                                                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna"></asp:LinkButton><asp:LinkButton
                                                            ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel"
                                                            Text="Annulla"></asp:LinkButton>
                                                    </EditItemTemplate>
                                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" Wrap="False" />
                                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                        Font-Underline="False" Wrap="False" />
                                                </asp:TemplateColumn>
                                            </Columns>
                                            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Position="TopAndBottom" Visible="False" Wrap="False" />
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                                        Font-Bold="True" Font-Names="Arial" Font-Size="12pt" MaxLength="100" ReadOnly="True"
                                        Style="left: 16px; top: 392px" Width="768px">Nessuna Selezione</asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td>
                                                    <img alt="Elenco2" src="Immagini/alert_elencoLegali.gif" style="z-index: 109; left: 216px;
                                                        top: 408px; background-color: white" /></td>
                                                <td>
                                                    <br />
                                                    <br />
                                                    <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
                                                        Style="z-index: 106; left: 216px; top: 480px" ToolTip="Visualizza" /></td>
                                                <td>
                                                    <br />
                                                    <br />
                                                    <asp:ImageButton ID="btnStampa" runat="server" ImageUrl="~/MOROSITA/Immagini/Img_Stampa2.png"
                                                        Style="z-index: 103; left: 360px; top: 480px" ToolTip="Stampa lista risultato" /></td>
                                                <td>
                                                    <br />
                                                    <br />
                                                    <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_Export_XLS.png"
                                                        Style="z-index: 102; right: 931px; left: 400px; top: 480px" ToolTip="Esporta in Excel" /></td>
                                                <td>
                                                    <br />
                                                    <br />
                                                    <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
                                                        Style="z-index: 103; left: 560px; top: 480px" ToolTip="Nuova Ricerca" /></td>
                                                <td>
                                                    <br />
                                                    <br />
                                                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                                                        Style="z-index: 103; left: 448px; top: 480px" ToolTip="Home" /></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:HiddenField ID="txtNomeFile" runat="server" /><asp:HiddenField ID="txtID" runat="server" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
        </table>
    </div>
    </form>

   <script  language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
    
</body>
    
</html>

