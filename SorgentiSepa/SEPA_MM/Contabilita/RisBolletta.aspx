<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisBolletta.aspx.vb" Inherits="Contabilita_RisBolletta" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Anteprima</title>
</head>
<body bgcolor="white">
    <form id="form1" runat="server" >
    <div>
        
        <table style="left: 0px; BACKGROUND-IMAGE: url(../NuoveImm/SfondoMascheraContratti.jpg); WIDTH: 798px;
            position: absolute; top: 0px">
            <tr>
                <td style="width: 706px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco 
                    Bollette Trovate&nbsp; <asp:Label ID="LnlNumeroRisultati" runat="server" 
                            Text="Label"></asp:Label>
                    </strong></span><br />
                    <br />
                    <div style="z-index: 150; overflow: auto; width: 776px; position: absolute; height: 297px; left: 11px; top: 56px;">
                        <asp:DataGrid style="z-index: 105" AutoGenerateColumns="False" 
                            BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" 
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                            Font-Underline="False" GridLines="None" ID="DataGridBollette" PageSize="15" 
                            runat="server" Width="99%" AllowPaging="True" >
                            <PagerStyle Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <Columns>
                                <asp:BoundColumn HeaderText="ID_BOLLETTA" Visible="False" 
                                    DataField="ID_BOLLETTA"></asp:BoundColumn>
                                <asp:BoundColumn HeaderText="ID_ANAGRAFICA" 
                                    Visible="False" DataField="ID_ANA"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_CONTRATTO" HeaderText="idContratto" 
                                    Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="NUM. BOLLETTA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.ID_BOLLETTA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="STATO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.STATO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INTESTATARIO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.INTESTATARIO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="RIFERIMENTO DAL">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.RIFERIMENTO_DA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="RIFERIMENTO AL">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.RIFERIMENTO_A") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="IMPORTO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="IMPORTO PAGATO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO_PAGATO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                            <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="#0000C0" />
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
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:HiddenField ID="IdAnagrafica" runat="server" />
                    <asp:HiddenField ID="Idbolletta" runat="server" />
        <asp:ImageButton ID="btnAnteprima" runat="server" ImageUrl="~/Contabilita/IMMCONTABILITA/AnteprimaBoll.png"
            Style="z-index: 106; left: 469px; position: absolute; top: 402px" 
            ToolTip="Anteprima Bolletta" />
                    <asp:HiddenField ID="IdContratto" runat="server" />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
            Style="z-index: 106; left: 594px; position: absolute; top: 402px" 
            ToolTip="Nuova Ricerca" />
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 107; left: 715px; position: absolute; top: 402px; height: 20px;" 
            ToolTip="Home" />
    
    </div>
        &nbsp;&nbsp;
        &nbsp;
        <asp:TextBox ID="txtmia" runat="server" BackColor="White" 
        BorderColor="White" BorderStyle="None"
            Font-Bold="True" Font-Names="Arial" Font-Size="12pt" MaxLength="100" ReadOnly="True"
            Style="z-index: 2; left: 11px; position: absolute; top: 477px" 
        Width="632px">Nessuna Selezione</asp:TextBox>
        &nbsp;&nbsp;
        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" 
        Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Style="left: 13px; position: absolute; top: 526px" Text="Label"
            Visible="False" Width="624px"></asp:Label>

    <p>
        ~/NuoveImm/Img_AnteprimaModelli.png</p>
    <p>
        <asp:ImageButton ID="btnVisualizzaDettaglio" runat="server" ImageUrl="~/Contabilita/IMMCONTABILITA/RiepilogoUtente.png"
            Style="z-index: 106; left: 330px; position: absolute; top: 402px; height: 20px;" 
            ToolTip="Riepilogo Contabilità Utente" />
        </p>
    </form>
    </body>
</html>
