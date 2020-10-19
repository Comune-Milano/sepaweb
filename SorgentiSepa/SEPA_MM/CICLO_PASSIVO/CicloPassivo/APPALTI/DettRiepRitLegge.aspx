<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DettRiepRitLegge.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_APPALTI_DettRiepRitLegge" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <base target="_self" />
    <script type="text/javascript" language="javascript">
        window.name = "modal";
        var Selezionato;
        var OldColor;
        var SelColo;
        function ChiediConfermaUscita() {
            if (document.getElementById('HiddenFieldModifica').value == '1') {
                var chiedi = window.confirm('Sono state effettuate delle modifiche.\nVuoi uscire senza salvare?');
                if (chiedi == true) {
                    document.getElementById('HiddenFieldEsci').value = '1';
                    CancelEdit();
                } else {
                    document.getElementById('HiddenFieldEsci').value = '0';
                };

            } else {
                document.getElementById('HiddenFieldEsci').value = '1';
                CancelEdit();
            };
        };
    </script>
    <title>Riepilogo Rit.Legge Appalto</title>
    <style type="text/css">
        #form1 {
            width: 794px;
            height: 385px;
        }

        .style4 {
            width: 707px;
            text-align: right;
            font-family: Arial;
            font-weight: bold;
            color: #0000FF;
        }

        .style5 {
            font-family: Arial;
            font-size: 10pt;
        }
    </style>
</head>
<body>
    <!-- Da mettere subito dopo l'apertura del tag <body> -->
    <%--    <div id="splash" style="border: thin dashed #000066; position: absolute; z-index: 500;
        text-align: center; font-size: 10px; width: 760px; height: 300px; visibility: hidden;
        vertical-align: top; line-height: normal; top: 10px; left: 12px; background-color: #FFFFFF;">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <img src='../../../CONDOMINI/Immagini/load.gif' alt='caricamento in corso' /><br />
        <br />
        caricamento in corso...<br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        &nbsp;
    </div>--%>
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadFormDecorator Skin="Web20" ID="FormDecorator1" runat="server" DecoratedControls="Buttons" />
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server">
        </telerik:RadWindowManager>
        <table style="width: 98%; position: absolute; top: 21px; left: 10px;">
            <tr>
                <td style="vertical-align: top; text-align: left" class="style4">
                    <asp:Label ID="lblTitolo" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                 <asp:Button ID="ImgSalva" runat="server" Text="Salva" Style="cursor:pointer;" />
                                
                            </td>
                            <td>
                                <asp:Button ID="ImageButtonEsci" runat="server" Text="Esci" Style="cursor:pointer;"
                                    OnClientClick="ChiediConfermaUscita();return false;" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="font-family: Arial; font-size: 5pt">&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <div style="border: 2px solid #ccccff; left: 0px; vertical-align: top; overflow: auto; width: 99%; top: 0px; height: 220px; text-align: left">
                        <asp:DataGrid ID="dgvRitLegge" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="Gray" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            PageSize="1" Style="z-index: 105; left: 8px; top: 32px" Width="97%" BorderWidth="1px">
                            <HeaderStyle BackColor="White" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="#0000C0" />
                            <Columns>
                                <asp:BoundColumn DataField="ODL" HeaderText="ODL/ANNO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DESC_VOCE_PF" HeaderText="DESC. VOCE P.F."></asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_PRENOTAZIONE" HeaderText="DATA PREN."></asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_CONSUNTIVAZIONE" HeaderText="DATA CONS."></asp:BoundColumn>
                                <asp:BoundColumn DataField="IVA" HeaderText="ALIQUOTA IVA">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Width="80px" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="RITENUTA_LEGGE" HeaderText="IMPORTO LORDO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="ALIQUOTA IVA MODIFICATA" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownListAliquota" runat="server" OnSelectedIndexChanged="DropDownListAliquota_SelectedIndexChanged"
                                            Font-Names="Arial" Font-Size="8pt" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="IMPORTO_LORDO_MODIFICATO" HeaderText="IMPORTO LORDO MODIFICATO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="IMPORTO_LORDO_MODIFICATO_DIFF" HeaderText="IMPORTO LORDO MODIFICATO"
                                    Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_VOCE_PF_NEW" HeaderText="VOCE" Visible="false"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="false"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="LOTTO" ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="DropDownListLotto" runat="server" Font-Names="Arial" Font-Size="8pt">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:TemplateColumn>
                            </Columns>
                            <PagerStyle Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                        </asp:DataGrid>
                    </div>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width: 96%;" cellpadding="0" cellspacing="0">
                        <tr>
                            <td>
                                <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                                    Font-Bold="True" Font-Names="Arial" Font-Size="10pt" MaxLength="100" ReadOnly="True"
                                    Style="z-index: 2; left: 13px; top: 528px" Width="400px">Nessuna Selezione</asp:TextBox>
                               
                            </td>
                            <td style="text-align: right" class="style5">
                                <strong>Totale €.</strong>
                            </td>
                            <td style="text-align: right" width="15%">
                                <asp:TextBox ID="txtTotale" runat="server" Width="85px" Font-Bold="True" Font-Names="Arial"
                                    Font-Size="8pt" ReadOnly="True" Style="text-align: right"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="HiddenFieldModifica" runat="server" Value="0" />
        <asp:HiddenField ID="HiddenFieldEsci" runat="server" Value="0" />
        <asp:HiddenField ID="solaLettura" runat="server" />
    </form>
    <script type="text/javascript">
        function CloseAndRefresh(args) {
            GetRadWindow().BrowserWindow.refreshPage(args);
            GetRadWindow().close();
        };
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        };
        function CancelEdit() {
            GetRadWindow().close();
        };

    </script>
</body>
<%--<script type="text/javascript" language="javascript">
    document.getElementById('splash').style.visibility = 'hidden';

</script>
--%></html>
