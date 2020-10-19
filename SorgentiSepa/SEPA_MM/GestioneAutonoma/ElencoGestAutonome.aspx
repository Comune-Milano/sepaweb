<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoGestAutonome.aspx.vb" Inherits="GestioneAutonoma_ElencoGestAutonome" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Anagrafica Condomini</title>
    <script type="text/javascript">
        function ApriAutogestione() {
            if (document.getElementById('txtid').value != 0) {
                parent.main.location.replace('GestioneAutonoma.aspx?IdAutogest=' + document.getElementById('txtid').value + '&IDESERC=' + document.getElementById('txtidEsercizio').value + '&CALL=ElencoGestAutonome');
            }
            else {
                alert('Selezionare una Gestione Autonoma da visualizzare!');
            }
        }
    </script>

</head>
<body  style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat:no-repeat;">
    <form id="form1" runat="server">
    <div style="width: 787px">
        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">Elenco Autogestioni 
        Trovate
            <asp:Label ID="LnlNumeroRisultati" runat="server" Text="Label" 
            Visible="False"></asp:Label>
            <div style="left: 8px; overflow: auto; width: 781px; position: absolute; top: 51px;
                height: 400px">
                <asp:DataGrid ID="DataGridCondom" runat="server" AutoGenerateColumns="False"
                    BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                    GridLines="None" PageSize="24" 
                    Style="z-index: 105; left: 193px; top: 54px" Width="762px">
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <ItemStyle ForeColor="Black" />
                    <Columns>
                        <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ID_ESERCIZIO" HeaderText="ID_ESERCIZIO" 
                            Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="DENOMINAZIONE" HeaderText="DENOMINAZIONE">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="DATA_COSTITUZIONE" HeaderText="DATA COST.">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ESERCIZIO" HeaderText="ESERCIZIO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="CC_POSTALE" HeaderText="C/C POSTALE">
                        </asp:BoundColumn>
                        <asp:BoundColumn></asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" />
                </asp:DataGrid>
            </div>
            <asp:TextBox ID="txtmia" runat="server" BackColor="White" 
            BorderColor="White" BorderStyle="None"
                Font-Bold="True" Font-Names="Arial" Font-Size="12pt" MaxLength="100" ReadOnly="True"
                Style="z-index: 2; left: 7px; position: absolute; top: 458px" 
            Width="632px">Nessuna Selezione</asp:TextBox>
            <asp:Label ID="LblErrore" runat="server" Font-Bold="True" 
            Font-Names="Arial" Font-Size="8pt"
                ForeColor="Red" Style="left: 11px; position: absolute; top: 479px" Text="Label"
                Visible="False" Width="624px"></asp:Label>
            &nbsp;
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 107; left: 722px; position: absolute; top: 501px" 
            ToolTip="Home" />
            <asp:HiddenField ID="txtid" runat="server" Value="0" />
    
            <asp:HiddenField ID="txtidEsercizio" runat="server" Value="0" 
            EnableViewState="False" />
    
            <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/Condomini/Immagini/Img_Export_Grande.png"
                Style="z-index: 107; left: 9px; position: absolute; top: 501px; right: 1246px;" 
        ToolTip="Esporta in Excel" />
        <img alt="" src="../NuoveImm/Img_Visualizza.png" 
            style="position: absolute; top: 501px; left: 581px; cursor: pointer;"
            onclick="ApriAutogestione();" title="Visualizza" /></span></strong></div>
    </form>
                   <script  language="javascript" type="text/javascript">
                       document.getElementById('dvvvPre').style.visibility = 'hidden';
               </script>

</body>
</html>
