<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AnnulloPagamento.aspx.vb" Inherits="Contratti_Pagamenti_AnnulloPagamento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Annullo dell'ultimo Pagamento Manuale Effettuato</title>
    <style type="text/css">
        .style1
        {
            font-family: ARIAL;
            font-weight: bold;
            font-size: 12pt;
            color: #FFFF66;
            background-color: #990000;
            text-align: center;
        }
        </style>
        <script type="text/javascript" language ="javascript" >
            window.name = "modal";

            function TastoInvio(e) {
                sKeyPressed1 = e.which;
                if (sKeyPressed1 == 13) {

                    e.preventDefault();
                }
            }

            function $onkeydown() {
                if (event.keyCode == 13) {
                    event.keyCode = '9';
                }
            }    
        </script>
</head>
<body>
        <!-- Da mettere subito dopo l'apertura del tag <body> -->
          <div id="splash"      
        
        style="border: thin dashed #000066; position :absolute; z-index :500; text-align:center; font-size:10px; width: 940px; height: 375px; visibility :hidden; vertical-align: top; line-height: normal; 
        top: 70px; left: 12px; background-color:#FFFFFF;">
                    <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <img src='../../CONDOMINI/Immagini/load.gif' alt='caricamento in corso'/><br/><br/>
            caricamento in corso...<br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            &nbsp;
        </div>
                <script type="text/javascript">
                    if (navigator.appName == 'Microsoft Internet Explorer') {
                        document.onkeydown = $onkeydown;
                    }
                    else {
                        window.document.addEventListener("keydown", TastoInvio, true);
                    }
        </script>
    <form id="form1" runat="server" target ="modal">
    <table width="100%">
        <tr>
            <td class="style1">
                ELENCO PAGAMENTI LIQUIDATI IN BASE ALL&#39;ULTIMA OPERAZIONE EFFETTUATA</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td align = "center"  >
    <div style="width: 98%; height: 233px; overflow: auto;">
        <asp:DataGrid ID="DgvUltimaOperazione" runat="server" AllowSorting="True" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="1px" Font-Bold="False" 
        Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
            PageSize="1" Style="table-layout: auto; z-index: 101; left: 16px; clip: rect(auto auto auto auto);
            direction: ltr; top: 200px; border-collapse: separate" Width="99%" 
        CellPadding="1" CellSpacing="1">
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
                <asp:BoundColumn DataField="OPERATORE" HeaderText="OPERATORE"></asp:BoundColumn>
                <asp:BoundColumn DataField="COD_CAF" HeaderText="ENTE"></asp:BoundColumn>
                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DATA_ORA" HeaderText="DATA ORA"></asp:BoundColumn>
                <asp:BoundColumn DataField="MOTIVAZIONE" HeaderText="DESCRIZIONE"></asp:BoundColumn>
                <asp:BoundColumn DataField="TIPO" HeaderText="TIPO"></asp:BoundColumn>
                <asp:BoundColumn DataField="MOTIVO_PAGAMENTO" HeaderText="MOTIVO">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="ID_OPERATORE" HeaderText="ID_OPERATORE" Visible="False"></asp:BoundColumn>
                <asp:BoundColumn DataField="COD_EVENTO" HeaderText="COD_EVENTO" Visible="False"></asp:BoundColumn>
<asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO €.">
    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
        Wrap="False" />
    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
        Wrap="False" />
                </asp:BoundColumn>
            </Columns>
            <HeaderStyle BackColor="#C5DCF5" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="#0000C0" Wrap="False" />
        </asp:DataGrid>
</div>
            </td>
        </tr>
        <tr>
            <td>
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" 
                        Text="Label" Visible="False" Width="580px"></asp:Label>
    
                            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblTotale" runat="server" Font-Names="Arial" Font-Size="10pt" 
                    Font-Bold="True" ForeColor="Blue"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                <table style="width:100%;">
                    <tr>
                        <td style="text-align: left">
                                <img id="exit" alt="Esci" src="../../NuoveImm/Img_Esci_Grande.png" 
                                title="Esci e chiudi" style="cursor :pointer" 
                                onclick="self.close();"/></td>
                        <td>
                            &nbsp;</td>
                        <td style="text-align: right">
                                <asp:ImageButton ID="btnProcedi" runat="server" 
                                    ImageUrl="~/NuoveImm/Img_Conferma.png" 
                                    OnClientClick="document.getElementById('splash').style.visibility = 'visible';"
                                    ToolTip="Conferma eliminazione dell'ultima operazione effettuata" />
                            </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="TotAnnullo" runat="server" />

    <script type ="text/javascript" language ="javascript" >
        document.getElementById('splash').style.visibility = 'hidden';

    
    </script>
    </form>
</body>
</html>
