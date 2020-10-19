<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_RicercaINS_2.ascx.vb"
    Inherits="Tab_RicercaINS_2" %>
<script type="text/javascript" src="prototype.lite.js"></script>
<script type="text/javascript" src="moo.fx.js"></script>
<script type="text/javascript" src="moo.fx.pack.js"></script>
<link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
<table class="FontTelerik">
    <tr>
        <td style="vertical-align: middle; text-align: left" class="TitoloH1">
            Ricerca Ubicazione per:
        </td>
        <td>
            <asp:RadioButtonList ID="RBL1" runat="server" AutoPostBack="True" RepeatDirection="Horizontal"
                Width="200px">
                <asp:ListItem Selected="True">Complesso</asp:ListItem>
                <asp:ListItem Value="Edificio"></asp:ListItem>
            </asp:RadioButtonList>
        </td>
        <td style="width: 330px">
        </td>
    </tr>
</table>
<table style="width: 100%" class="FontTelerik">
    <tr>
        <td style="height: 20px; width: 10%">
        </td>
        <td style="height: 20px">
        </td>
        <td style="width: 40px; height: 20px">
        </td>
    </tr>
    <tr>
        <td style="height: 20px">
            <asp:Label ID="Label1" runat="server" Font-Bold="False" Width="130px">Esercizio Finanziario*</asp:Label>
        </td>
        <td style="height: 20px">
            <telerik:RadComboBox ID="cmbEsercizio" Width="40%" AppendDataBoundItems="true" Filter="Contains"
                runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                LoadingMessage="Caricamento...">
            </telerik:RadComboBox>
        </td>
        <td style="width: 40px; height: 20px">
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblIndirizzo" runat="server">Indirizzo*</asp:Label>
        </td>
        <td>
            <telerik:RadComboBox ID="cmbIndirizzo" Width="40%" AppendDataBoundItems="true" Filter="Contains"
                OnClientKeyPressing="ChangeToUpperCase" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
            </telerik:RadComboBox>
        </td>
        <td style="width: 40px;">
            <img id="Img1" alt="Aiuto Ricerca per Denominazione Edificio" onclick="document.getElementById('Tab_Ricerca2_TextBox1').value!='1';myOpacity.toggle();"
                src="Immagini/Search_24x24.png" style="left: 600px; cursor: pointer; visibility: hidden;
                top: 72px" />
        </td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblServizio" runat="server">Servizio</asp:Label>
        </td>
        <td>
            <telerik:RadComboBox ID="cmbServizio" Width="40%" AppendDataBoundItems="true" Filter="Contains"
                runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                LoadingMessage="Caricamento...">
            </telerik:RadComboBox>
        </td>
        <td style="width: 40px;">
        </td>
    </tr>
    <tr>
        <td style="height: 24px">
            <asp:Label ID="lblTipoServizioDett" runat="server">Voce DGR</asp:Label>
        </td>
        <td style="height: 24px">
            <telerik:RadComboBox ID="cmbServizioVoce" Width="40%" AppendDataBoundItems="true"
                Filter="Contains" runat="server" AutoPostBack="true" ResolvedRenderMode="Classic"
                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
            </telerik:RadComboBox>
        </td>
        <td style="width: 40px; height: 24px;">
        </td>
    </tr>
    <tr>
        <td style="height: 21px">
            <asp:Label ID="LblAppalto" runat="server">Num. Repertorio</asp:Label>
        </td>
        <td style="height: 21px">
            <telerik:RadComboBox ID="cmbAppalto" Width="40%" AppendDataBoundItems="true" Filter="Contains"
                runat="server" AutoPostBack="true" ResolvedRenderMode="Classic" HighlightTemplatedItems="true"
                LoadingMessage="Caricamento...">
            </telerik:RadComboBox>
        </td>
        <td style="height: 21px; width: 40px;">
        </td>
    </tr>
    <tr>
        <td>
        </td>
        <td>
        </td>
        <td style="width: 40px">
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top; text-align: left">
        </td>
        <td>
            <asp:Label ID="LblErrore" runat="server" Font-Bold="True" ForeColor="Red" Style="left: 152px;
                top: 304px" Text="Label" Visible="False" Width="300px"></asp:Label>
        </td>
        <td style="width: 40px">
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top; text-align: left">
        </td>
        <td>
            &nbsp;
        </td>
        <td style="width: 40px">
        </td>
    </tr>
    <tr>
        <td style="vertical-align: top; text-align: left; height: 97px;">
            &nbsp;
        </td>
        <td style="height: 97px">
            <div id="AiutoRicerca" style="z-index: 200; left: 408px; width: 306px; position: absolute;
                top: 112px; height: 227px; background-color: transparent">
                <br />
                <div style="width: 180px; height: 68px; background-color: silver">
                    <table style="width: 301px; height: 185px; background-color: silver">
                        <tr>
                            <td class="style1" style="vertical-align: top; height: 21px; text-align: left">
                                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 14px; top: 32px" Width="238px">Denominazione Indirizzo</asp:Label>
                            </td>
                            <td class="style2" style="vertical-align: baseline; height: 21px; text-align: left">
                            </td>
                        </tr>
                        <tr>
                            <td class="style3" style="vertical-align: top; text-align: left">
                                <asp:TextBox ID="TxtDescInd" runat="server" BorderStyle="Solid" BorderWidth="1px"
                                    Font-Names="arial" Font-Size="10pt" Style="z-index: 102; left: 14px; top: 51px"
                                    ToolTip="Approssimare la ricerca per questo indirizzo" Width="243px"></asp:TextBox>
                            </td>
                            <td class="style4" style="vertical-align: baseline; text-align: left">
                                <asp:ImageButton ID="BtnCerca" runat="server" ImageUrl="~/CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/text_view.png"
                                    Style="z-index: 111; left: 246px; top: 50px" ToolTip="Cerca Per Approssimazione" />
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 352px; height: 104px; text-align: left">
                                <asp:Label ID="LblNoResult" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Red" Style="z-index: 100; left: 14px; top: 73px" Visible="False" Width="97px">Nessun Risultato</asp:Label><br />
                                <div style="left: 5px; overflow: auto; width: 263px; top: 87px; height: 101px">
                                    <asp:RadioButtonList ID="ListEdifci" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Width="240px">
                                    </asp:RadioButtonList>
                                </div>
                            </td>
                            <td style="vertical-align: baseline; width: 27px; height: 104px; text-align: left">
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <asp:ImageButton ID="BtnConferma" runat="server" ImageUrl="~/CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Next_24x24.png"
                                    OnClientClick="myOpacity.toggle();" Style="z-index: 111; left: 268px; top: 190px"
                                    ToolTip="Conferma" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </td>
        <td style="width: 40px; height: 97px;">
        </td>
    </tr>
    <asp:HiddenField ID="TextBox1" runat="server"></asp:HiddenField>
    <asp:HiddenField ID="txtTIPO" runat="server"></asp:HiddenField>
</table>
<script type="text/javascript">

    myOpacity = new fx.Opacity('AiutoRicerca', { duration: 200 });
    //myOpacity.hide();

    if (document.getElementById('Tab_Ricerca2_TextBox1').value != '2') {
        myOpacity.hide(); ;
    }
        
</script>
