<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RptMorEmesse.aspx.vb" Inherits="Condomini_RptMorEmesse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        #form1
        {
            width: 778px;
        }
    </style>
</head>
<body style="background-attachment: fixed; background-image: url(Immagini/SfondoMascheraContratti.jpg); background-repeat: no-repeat">
    <form id="form1" runat="server">



    <div>
        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
        Elenco delle Morosità Emesse<br />
        </span></strong>
    </div>
                   
                 <div style="left: 11px; overflow: auto; width: 768px; position: absolute; top: 57px;
            height: 452px">
            <asp:DataGrid ID="DgvMorEmesse" runat="server" AutoGenerateColumns="False"
                BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                PageSize="20" Style="z-index: 105; left: 193px; top: 54px" Width="99%" 
                    CellPadding="1" CellSpacing="1">
                <PagerStyle Mode="NumericPages" />
                <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                <ItemStyle ForeColor="Black" />
                <Columns>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" 
                        Visible="False">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_CONDOMINIO" HeaderText="ID_CONODMINIO" 
                        Visible="False">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DENOMINAZIONE" HeaderText="CONDOMINIO">
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="RIF_DA" HeaderText="RIFERIMENTO DAL">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                            Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                            Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="RIF_A" HeaderText="RIFERIMENTO AL">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                            Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                            Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="MOROSITA" HeaderText="IMP. MOROSITA'">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                            Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                            Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="BOLLETTATO" HeaderText="IMP. BOLLETTATO">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                            Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                            Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                            Wrap="False" />
                    </asp:BoundColumn>
                </Columns>
                <HeaderStyle BackColor="Lavender" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    ForeColor="#0000C0" />
            </asp:DataGrid>
        </div>
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 107; left: 715px; position: absolute; top: 520px" 
            ToolTip="Home" />
    
            <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial"
            Font-Size="8pt" ForeColor="Red" Height="18px" Style="z-index: 104; left: 121px;
            position: absolute; top: 524px; width: 580px;" Visible="False"></asp:Label>

                <strong>
    <span style="font-size: 14pt; color: #801f1c; font-family: Arial">
    
            <asp:ImageButton ID="btnExport" runat="server" ImageUrl="Immagini/Img_Export_Grande.png"
                Style="z-index: 10; left: 12px; position: absolute; top: 520px" 
        ToolTip="Esporta in Excel" />
        </span></strong>
    
        
    </form>
</body>
</html>
