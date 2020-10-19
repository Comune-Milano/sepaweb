<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElaborazioneInteressiDepCauz.aspx.vb"
    Inherits="Contratti_ElDepCauz" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Elenco Elaborazioni Depositi Cauzionali</title>
    <style type="text/css">
        /* ***** NEWS ***** */.RadForm.rfdButton input.rfdDecorated[type="submit"]
        {
            cursor: pointer;
        }
        .style1
        {
            height: 72px;
        }
    </style>
    <script language="javascript" type="text/jscript">
        function caricamentoincorso() {

            document.getElementById('caricamento').style.visibility = 'visible';

        };
    </script>
</head>
<body style="background-image: url('../NuoveImm/SfondoMascheraContratti.jpg'); background-repeat: no-repeat;
    width: 770px;">
    <div id="caricamento" style="margin: 0px; background-color: #C0C0C0; width: 100%;
        height: 100%; position: fixed; top: 0px; left: 0px; filter: alpha(opacity='75');
        opacity: 0.75; background-color: #eeeeee; z-index: 500">
        <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
            margin-top: -48px; background-image: url('Immagini/sfondo2.png');">
            <table style="width: 100%; height: 100%">
                <tr>
                    <td valign="middle" align="center" class="style1">
                        <img alt="Caricamento" src="Immagini/load.gif" />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="center" style="font-family: Arial; font-size: 10pt; font-weight: 700">
                        Caricamento . . .
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <form id="form1" runat="server">
    <table border="0" cellpadding="0" cellspacing="0" width="100%">
        <tr>
            <td style="width: 90%">
                <br />
                <asp:Label ID="Label1" Text="Elaborazioni interessi su deposito cauzionale" runat="server"
                    ForeColor="Maroon" Font-Size="14pt" Font-Bold="true" Font-Names="Arial" />
            </td>
            <td style="width: 10%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 90%">
                &nbsp;
            </td>
            <td style="width: 10%">
                <asp:ImageButton ID="ImageButton" runat="server" ImageUrl="../NuoveImm/Refresh.png"
                    AlternateText="Aggiorna" ToolTip="Aggiorna" Height="32px" Width="32px" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div style="width: 770px; height: 342px; overflow: auto;">
                    <asp:DataGrid ID="DataGridElenco" runat="server" CellPadding="2" Font-Bold="False"
                        Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" GridLines="None"
                        Width="100%" CellSpacing="2" ShowFooter="True" AutoGenerateColumns="False">
                        <ItemStyle BackColor="#EFF3FB" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="FROM_ID" HeaderText="FROM_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TO_ID" HeaderText="TO_ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_REST_INTERESSI" HeaderText="DATA CALCOLO REST.INTERESSI"
                                ItemStyle-HorizontalAlign="Left">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="INIZIO" HeaderText="INIZIO" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FINE" HeaderText="FINE" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ESITO" HeaderText="ESITO" ItemStyle-HorizontalAlign="Left">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PERCENTUALE" HeaderText="PERCENTUALE" ItemStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ERRORE" HeaderText="ANOMALIA">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                    Font-Strikeout="False" Font-Underline="False" />
                            </asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#CCCCCC" Font-Bold="True" ForeColor="Red" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                    <br />
                    <br />
                    <br />
                    
                    &nbsp;</div>
            </td>
        </tr>
        <tr>
            <td align="center" style="text-align: left">
                <asp:Label ID="LabelRis" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="9pt" />&nbsp;
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td align="center" style="text-align: right" colspan="2">
                <br />
                    <asp:ImageButton ID="btnReportDettagli" runat="server" ImageUrl="~/NuoveImm/Img_ReportDettagli.png" />
                    <asp:ImageButton ID="btnReportTotali" runat="server" ImageUrl="~/NuoveImm/Img_ReportTotali.png" /></td>
            
        </tr>
    </table>
    <asp:HiddenField ID="idSel" runat="server" Value="0" />
    <asp:HiddenField ID="Perc" runat="server" Value="0" />
    <script language="javascript" type="text/javascript">
        document.getElementById('caricamento').style.visibility = 'hidden';

        var Selezionato;

    </script>
    </form>
</body>
</html>
