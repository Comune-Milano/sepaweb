<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ResultAmministratore.aspx.vb" Inherits="Condomini_ResultAmministratore" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Pagina senza titolo</title>
    <script type ="text/javascript" >
        var Selezionato;
    </script>

</head>
<body  style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg)">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblContratto" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="16pt"
            ForeColor="#660000" Text="Elenco Condomini Trovati" Width="264px"></asp:Label>
        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
            <asp:Label ID="LnlNumeroRisultati" runat="server" Text="Label"></asp:Label>
            <asp:HiddenField ID="txtid" runat="server" />
        </span></strong>
    
    </div>
        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                
        Style="z-index: 107; left: 722px; position: absolute; top: 547px; height: 20px;" 
        ToolTip="Home" />
            <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
                Style="z-index: 106; left: 590px; position: absolute; top: 547px" 
        ToolTip="Nuova Ricerca" />
            <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
                Style="z-index: 102; left: 392px; position: absolute; top: 547px" 
        ToolTip="Visualizza" />
            <asp:Label ID="LblErrore" runat="server" Font-Bold="True" 
        Font-Names="Arial" Font-Size="8pt"
                ForeColor="Red" Style="left: 6px; position: absolute; top: 538px" Text="Label"
                Visible="False" Width="624px"></asp:Label>
            <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                Font-Bold="True" Font-Names="Arial" Font-Size="12pt" MaxLength="100" ReadOnly="True"
                Style="z-index: 2; left: 7px; position: absolute; top: 498px" Width="632px">Nessuna Selezione</asp:TextBox>
            <div style="left: 8px; overflow: auto; width: 781px; position: absolute; top: 51px;
                height: 446px">
                <asp:DataGrid ID="DataGridAmminist" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="Black"
                    GridLines="None" PageSize="24" 
                    Style="z-index: 105; left: 193px; top: 54px" Width="762px">
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" ReadOnly="True" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="CONDOMINIO" HeaderText="CODNOMINIO" Visible="False"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="COD CONDOMINIO">
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.COD_CONDOMINIO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="CONDOMINIO">
                            <ItemTemplate>
                                <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CONDOMINIO") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="AMMINIST.">
                            <ItemTemplate>
                                <asp:Label runat="server" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.AMMINIST") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" />
                </asp:DataGrid>
            </div>
        </span></strong>
    
    </form>
</body>
</html>
