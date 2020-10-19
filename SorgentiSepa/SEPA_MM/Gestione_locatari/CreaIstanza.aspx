<%@ Page Title="" Language="VB" MasterPageFile="~/Gestione_locatari/MasterGLocat.master"
    AutoEventWireup="false" CodeFile="CreaIstanza.aspx.vb" Inherits="Gestione_locatari_CreaIstanza" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) oWindow = window.radWindow;
            else if (window.frameElement.radWindow) oWindow = window.frameElement.radWindow;
            return oWindow;
        };
        function CancelEdit() {
            GetRadWindow().close();
        };

        function InserisciIstanza(btnToClik) {
            var tipoPres = $find("MasterPage_CPContenuto_RadComboModPres");
            var idtipoPres = tipoPres.get_value();
            if (idtipoPres != -1) {
                if (document.getElementById('MasterPage_CPContenuto_RadDatePresentaz').value != '') {
                    if (document.getElementById('MasterPage_CPContenuto_RadDateEvento').value != '') {
                       
                            apriConfirm('Sei sicuro di proseguire con la creazione dell\'istanza?', function callbackFn(arg) { if (arg == true) { clickElimina(btnToClik.id) } }, 300, 150, MessaggioTitolo.Attenzione, null);
                        
                     }
                     else {
                        apriAlert('Selezionare la data evento!', 300, 150, MessaggioTitolo.Attenzione, null, '../StandardTelerik/Immagini/Messaggi/alert.png');
                    }
                }
                else {
                    apriAlert('Selezionare la data di presentazione!', 300, 150, MessaggioTitolo.Attenzione, null, '../StandardTelerik/Immagini/Messaggi/alert.png');
                }
            }
            else {
                apriAlert('Selezionare la modalità di presentazione!', 300, 150, MessaggioTitolo.Attenzione, null, '../StandardTelerik/Immagini/Messaggi/alert.png');
            }
        };
        function ApriAnagrafeSIPO(btnToClik) {
            apriConfirm('Stai per connetterti al servizio SIPO per la verifica dei codici fiscali. Continuare?', function callbackFn(arg) { if (arg == true) { clickElimina(btnToClik.id) } }, 300, 150, MessaggioTitolo.Attenzione, null);
        };
        function CorreggiDaAnagrafeSIPO(btnToClik) {
            apriConfirm('Stai per aggiornare l\'anagrafica con i dati ricavati da SIPO. Continuare?', function callbackFn(arg) { if (arg == true) { clickElimina(btnToClik.id) } }, 300, 150, MessaggioTitolo.Attenzione, null);
        };
      
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Crea Istanza"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:Button ID="btnProcedi" runat="server" Text="Procedi" ToolTip="Procedi" Visible="False"
        OnClientClick="InserisciIstanza(this);return false;" />
    <asp:Button ID="btnAggiornaSipo" runat="server" Text="Aggiorna da SIPO" OnClientClick="CorreggiDaAnagrafeSIPO(this);return false;" CausesValidation="False" Visible="false" >
    </asp:Button>
    <asp:Button ID="btnAnagrafeSipo" runat="server" Text="Verifica SIPO" OnClientClick="ApriAnagrafeSIPO(this);return false;"
        CausesValidation="False"></asp:Button>
    <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" CausesValidation="False"
        OnClientClick="CancelEdit();" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <fieldset style="width: 97%;">
        <legend>&nbsp;&nbsp;&nbsp;<strong>Info Generali</strong>&nbsp;&nbsp;&nbsp;</legend>
        <div style="clear: both; float: left;">
            <table>
                <tr>
                    <td style="width: 150px;" class="tdNoWrapWidthBlock">
                        Data di presentazione:
                    </td>
                    <td style="width: 5px;">
                        &nbsp;
                    </td>
                    <td style="width: 185px;">
                        <telerik:RadDatePicker ID="RadDatePresentaz" runat="server" WrapperTableCaption=""
                            MinDate="01/01/1000" MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}"
                            ShowPopupOnFocus="true">
                            <DateInput ID="DateInput2" runat="server" EmptyMessage="gg/mm/aaaa">
                                <ClientEvents OnKeyPress="CompletaDataTelerik" />
                                <EmptyMessageStyle Font-Italic="True" ForeColor="#A8BCD9" />
                            </DateInput>
                            <Calendar ID="Calendar1" runat="server">
                                <SpecialDays>
                                    <telerik:RadCalendarDay Repeatable="Today">
                                        <ItemStyle Font-Bold="true" BackColor="LightSkyBlue" />
                                    </telerik:RadCalendarDay>
                                </SpecialDays>
                            </Calendar>
                        </telerik:RadDatePicker>
                    </td>
                </tr>
            </table>
        </div>
          <div style="float: left;">
            <table>
                <tr>
                    <td style="width: 150px;" class="tdNoWrapWidthBlock">
                        Data evento:
                    </td>
                    <td style="width: 5px;">
                        &nbsp;
                    </td>
                    <td style="width: 185px;">
                        <telerik:RadDatePicker ID="RadDateEvento" runat="server" WrapperTableCaption=""
                            MinDate="01/01/1000" MaxDate="01/01/9999" Width="110px" DataFormatString="{0:dd/MM/yyyy}"
                            ShowPopupOnFocus="true">
                            <DateInput ID="DateInput1" runat="server" EmptyMessage="gg/mm/aaaa">
                                <ClientEvents OnKeyPress="CompletaDataTelerik" />
                                <EmptyMessageStyle Font-Italic="True" ForeColor="#A8BCD9" />
                            </DateInput>
                            <Calendar ID="Calendar2" runat="server">
                                <SpecialDays>
                                    <telerik:RadCalendarDay Repeatable="Today">
                                        <ItemStyle Font-Bold="true" BackColor="LightSkyBlue" />
                                    </telerik:RadCalendarDay>
                                </SpecialDays>
                            </Calendar>
                        </telerik:RadDatePicker>
                    </td>
                </tr>
            </table>
        </div>
        <div style="float: left;">
            <table>
                <tr>
                    <td style="width: 150px;" class="tdNoWrapWidthBlock">
                        Modalità di presentazione:
                    </td>
                    <td style="width: 5px;">
                        &nbsp;
                    </td>
                    <td style="width: 185px;">
                        <telerik:RadComboBox ID="RadComboModPres" runat="server" Culture="it-IT" EnableLoadOnDemand="true"
                            Filter="None" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                            ResolvedRenderMode="Classic">
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <fieldset style="width: 97%;">
        <legend>&nbsp;&nbsp;&nbsp;<strong>Ultimo Nucleo Valido</strong>&nbsp;&nbsp;&nbsp;</legend>
        <div style="clear: both; float: left;">
            <table>
                <tr>
                    <td width="100%" style="text-align: justify">
                        <asp:Label ID="Label1" runat="server" Text="Scegli il dichiarante solo tra un componente maggiorenne"
                            Font-Names="arial" Font-Size="8pt" Width="100%" Font-Italic="True"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:RadioButtonList ID="ListaInt" runat="server" Font-Names="Arial" Font-Size="10pt">
                        </asp:RadioButtonList>
                    </td>
                    <td style="width: 5px;">
                        &nbsp
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    <fieldset style="width: 97%;">
        <legend>&nbsp;&nbsp;&nbsp;<strong>Info Import Dati</strong>&nbsp;&nbsp;&nbsp;</legend>
        <div style="clear: both; float: left;">
            <table width="100%">
                <tr>
                    <td width="100%" style="text-align: justify">
                        <asp:Label ID="lblMsgImport" runat="server" Text="" Font-Names="arial" Font-Size="9pt"
                            Width="100%"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </fieldset>
    
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:HiddenField ID="HFBtnToClick" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFBlockExit" runat="server" Value="1" ClientIDMode="Static" />
    <asp:HiddenField ID="frmModify" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="idcont" runat="server" ClientIDMode="Static" Value="0" />
    <asp:HiddenField ID="idMotivoIstanza" runat="server" ClientIDMode="Static" Value="0" />
    <asp:HiddenField ID="lIdDichiarazione" runat="server" ClientIDMode="Static" Value="0" />
    <asp:HiddenField ID="new_id_dom" runat="server" ClientIDMode="Static" Value="0" />
    <asp:HiddenField ID="new_idDichia" runat="server" ClientIDMode="Static" Value="0" />
    <asp:HiddenField ID="id_intest" runat="server" ClientIDMode="Static" Value="0" />
    <asp:HiddenField ID="codFisc" runat="server" ClientIDMode="Static" Value="0" />
    <asp:HiddenField ID="CodContratto" runat="server" ClientIDMode="Static" Value="" />
    <asp:HiddenField ID="intestatario" runat="server" ClientIDMode="Static" Value="" />
    <asp:HiddenField ID="id_bando" runat="server" ClientIDMode="Static" Value="-1" />
    <asp:HiddenField ID="tipoDomImportata" runat="server" ClientIDMode="Static" Value="-1" />
    <asp:HiddenField ID="hfCF" runat="server" ClientIDMode="Static" Value="" />
    <asp:HiddenField ID="hfCognome" runat="server" ClientIDMode="Static" Value="" />
    <asp:HiddenField ID="hfNome" runat="server" ClientIDMode="Static" Value="" />
    <asp:HiddenField ID="hfSesso" runat="server" ClientIDMode="Static" Value="" />
    <asp:HiddenField ID="hfDataNascita" runat="server" ClientIDMode="Static" Value="" />
    <asp:HiddenField ID="hfComuneNascita" runat="server" ClientIDMode="Static" Value="" />
    <asp:HiddenField ID="hfProvinciaNascita" runat="server" ClientIDMode="Static" Value="" />
    <asp:HiddenField ID="hfNazioneNascita" runat="server" ClientIDMode="Static" Value="" />
    <asp:HiddenField ID="hfIndirizzo" runat="server" ClientIDMode="Static" Value="" />
    <asp:HiddenField ID="hfCittadinanza" runat="server" ClientIDMode="Static" Value="" />
    <asp:HiddenField ID="hfNumDocId" runat="server" ClientIDMode="Static" Value="" />
    <asp:HiddenField ID="hfDataDocId" runat="server" ClientIDMode="Static" Value="" />
    <asp:HiddenField ID="hfComuneRilascio" runat="server" ClientIDMode="Static" Value="" />
    <asp:HiddenField ID="lista" runat="server" ClientIDMode="Static" Value="" />
</asp:Content>

