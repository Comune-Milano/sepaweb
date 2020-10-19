<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatiUnitaAssFuoriMI.aspx.vb"
    Inherits="Contratti_RisultatiUnitaAssFuoriMI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body style="background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat: no-repeat;
    vertical-align: top;">
    <div id="caricamento" style="margin: 0px; background-color: #C0C0C0; width: 100%;
        height: 100%; position: fixed; top: 0px; left: 0px; filter: alpha(opacity='75');
        opacity: 0.75; background-color: #eeeeee; z-index: 500">
        <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
            margin-top: -48px; background-image: url('Immagini/sfondo2.png');">
            <table style="width: 100%; height: 100%">
                <tr>
                    <td valign="middle" align="center">
                        <img alt="Caricamento" src="Immagini/load.gif" />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="center">
                        Caricamento . . .
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%">
        <tr style="height: 35px; vertical-align: bottom">
            <td colspan="2">
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>Elenco risultati
                    - Trovate
                    <asp:Label ID="lblNumTot" runat="server"></asp:Label>
                    assegnazioni per alloggi Fuori Milano </strong></span>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <div style="overflow: auto; width: 100%; height: 350px;">
                    <asp:DataGrid ID="dgvElencoPratiche" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        PageSize="14" Width="100%">
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Mode="NumericPages" Wrap="False" />
                        <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                            ForeColor="#0000C0" Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="INTESTATARIO" HeaderText="ITESTATARI" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_DOMANDA" HeaderText="ID_DOMANDA" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_UNITA" HeaderText="ID_UNITA" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_DICHIARAZIONE" HeaderText="ID_DICHIARAZIONE" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PROVENIENZA" HeaderText="PROVENIENZA" ReadOnly="True"
                                Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_UNITA_IMMOBILIARE" HeaderText="COD.UNITA'"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="CF_PIVA" HeaderText="COD.FISCALE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_ASSEGNAZIONE" HeaderText="DATA ACCETTAZIONE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtmia" runat="server" BorderColor="White" BorderStyle="None" Font-Bold="True"
                    Font-Names="Arial" Font-Size="10pt" MaxLength="500" ReadOnly="True" Style="z-index: 500;
                    background-color: transparent;" Width="100%">Nessuna Selezione</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%">
                    <tr>
                        <td style="width: 60%">
                            &nbsp;
                        </td>
                        <td>
                            <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Conferma.png"
                                OnClientClick="caricamentoincorso();" ToolTip="Visualizza" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnNewRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
                                OnClientClick="caricamentoincorso();location.href='NuovoContrattoFM.aspx';return false;"
                                ToolTip="Nuova Ricerca" />
                        </td>
                        <td>
                            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="../NuoveImm/Img_Home.png"
                                OnClientClick="caricamentoincorso();location.href='pagina_home.aspx';return false;"
                                ToolTip="Annulla" />
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="idUnita" runat="server" Value="0" />
                <asp:HiddenField ID="idDich" runat="server" Value="0" />
                <asp:HiddenField ID="prov" runat="server" Value="0" />
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript" language="javascript">
        var Selezionato;
        var OldColor;
        var SelColo;

        function caricamentoincorso() {
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate();
                if (Page_IsValid) {
                    if (document.getElementById('caricamento') != null) {
                        document.getElementById('caricamento').style.visibility = 'visible';
                    };
                }
                else {
                    alert('ATTENZIONE! Ci sono delle incongruenze nei dati della pagina!');
                };
            }
            else {
                if (document.getElementById('caricamento') != null) {
                    document.getElementById('caricamento').style.visibility = 'visible';
                };
            };
        };
        initialize();
        function initialize() {
            window.focus();
            document.getElementById('caricamento').style.visibility = 'hidden';
        };
    </script>
</body>
</html>
