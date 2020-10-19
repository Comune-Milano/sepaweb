<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaSfitti.aspx.vb" Inherits="MANUTENZIONI_RicercaSfitti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >

<script type="text/javascript" src="Funzioni.js">
<!--
var Uscita1;
Uscita1=1;
// -->
</script>

<head id="Head1" runat="server">
    <title>RISULTATI RICERCA</title>
</head>
<body >
    <form id="form1" runat="server">
    <div>
        &nbsp;&nbsp;
            <table >
            <tr>
                <td style="width: 800px; height: 58px; left: 0px; background-image: url(../../../NuoveImm/SfondoMascheraContratti.jpg); position: absolute; top: 0px;">
                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <br />
                        &nbsp;&nbsp; Risultati Ricerca n.<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
&nbsp;</span></strong><br />
                    <br />
                    <br />
                    <br />
                    <div style="left: 8px; overflow: auto; width: 784px; position: absolute; top: 56px;
                        height: 320px">
                        <asp:DataGrid ID="DataGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            BackColor="White" BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="Black" Height="1px" PageSize="1" Style="table-layout: auto; z-index: 101;
                            left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                            TabIndex="35" Width="784px" BorderColor="#000099">
                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Position="TopAndBottom" Visible="False" Wrap="False" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <Columns>
                                <asp:BoundColumn DataField="ID_UNITA_IMMOBILIARI" HeaderText="ID" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="EDIFICIO" HeaderText="EDIFICIO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="SCALA" HeaderText="SCALA"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PIANO" HeaderText="PIANO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="INTERNO" HeaderText="INTERNO"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="SUB">
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME_SUB") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME_SUB") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="TIPOLOGIA_UNITA" HeaderText="TIPOLOGIA ALLOGGIO"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="Selezione" Visible="False">
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna"></asp:LinkButton><asp:LinkButton
                                            ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel"
                                            Text="Annulla"></asp:LinkButton>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                                            Text="Modifica">Seleziona</asp:LinkButton>
                                    </ItemTemplate>
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                            </Columns>
                            <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="#0000C0" Wrap="False" />
                        </asp:DataGrid></div>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp;<br />
                    <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                        Font-Names="Arial" Font-Size="12pt" MaxLength="100" Style="left: 16px; position: absolute;
                        top: 392px" Width="768px" ReadOnly="True" Font-Bold="True">Nessuna Selezione</asp:TextBox>
                    <br />
                    <br />
                    <img alt="Elenco2" src="Immagini/alert_elencoManutenzioni.gif" style="z-index: 109; left: 16px;
                        position: absolute; top: 424px; background-color: white" />
                    <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
                        Style="z-index: 103; left: 512px; position: absolute; top: 480px" ToolTip="Nuova Ricerca" Visible="False" />
                    <br />
                    <br />
                    <br /><asp:ImageButton ID="btnStampa" runat="server" ImageUrl="~/CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Img_Stampa2.png"
                        Style="z-index: 103; left: 368px; position: absolute; top: 480px" ToolTip="Stampa lista risultato" Visible="False" />
                </td>
            </tr>
        </table>
        &nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 107; left: 648px; position: absolute; top: 480px" ToolTip="Home" />
        &nbsp;&nbsp;
        <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
            Style="z-index: 106; left: 224px; position: absolute; top: 480px" ToolTip="Visualizza" />
        &nbsp;
        <asp:TextBox ID="txtid" runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None"
            MaxLength="100" Style="left: 544px; position: absolute; top: 576px" Width="152px"></asp:TextBox>
        &nbsp;
    
    </div>
    </form>
</body>
</html>

