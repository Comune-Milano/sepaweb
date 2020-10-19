<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisUtenza.aspx.vb" Inherits="CENSIMENTO_RisultatiUI2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<script type ="text/javascript">var Selezionato</script>
    <title>Risultati Ricerca</title>
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
                        Inquilini Trovati&nbsp; <asp:Label ID="LnlNumeroRisultati" runat="server" 
                            Text="Label"></asp:Label>
                    </strong></span><br />
                    <br />
                    <div style="z-index: 150; overflow: auto; width: 776px; position: absolute; height: 412px; left: 11px; top: 56px;">
                        <asp:DataGrid style="z-index: 105" AutoGenerateColumns="False" 
                            BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" 
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                            Font-Underline="False" GridLines="None" ID="DataGrid1" PageSize="22" 
                            runat="server" Width="150%" AllowPaging="True" >
                            <PagerStyle Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                            <Columns>
                                <asp:BoundColumn DataField="ID_ANAGRAFICA" HeaderText="ID_ANAGRAFICA" 
                                    ReadOnly="True" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CFIVA" HeaderText="CFIVA" ReadOnly="True" 
                                    Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO" 
                                    Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_CONTRATTO" HeaderText="IDCONTRATTO" 
                                    Visible="False"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="COD. CONTRATTO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COD_CONTRATTO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INTESTATARIO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INTESTATARIO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DATA NASCITA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.DATA_NASCITA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CF/P.IVA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.CFIVA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="SESSO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.SESSO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="TELEFONO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.TELEFONO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="RESIDENZA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.RESIDENZA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="COMUNE RES.">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COMUNE_RESIDENZA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="PR. RES.">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.PROVINCIA_RESIDENZA") %>'></asp:Label>
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
                    <asp:HiddenField ID="CFIVA" runat="server" />
                    <asp:HiddenField ID="IdContratto" runat="server" />

                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
            Style="z-index: 106; left: 606px; position: absolute; top: 507px" 
            ToolTip="Nuova Ricerca" />
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 107; left: 731px; position: absolute; top: 507px" 
            ToolTip="Home" />
    
    </div>
        &nbsp;<asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
            
        Style="z-index: 102; left: 484px; position: absolute; top: 507px; " 
        ToolTip="Visualizza" />
        &nbsp;
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
    </form>
    <script  language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
