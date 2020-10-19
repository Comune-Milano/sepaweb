<%@ Page Title="" Language="VB" MasterPageFile="~/Gestione_locatari/MasterGLocat.master"
    AutoEventWireup="false" CodeFile="ScegliIstanza.aspx.vb" Inherits="Gestione_locatari_ScegliIstanza" %>

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

        function apriIstanza() {

            if (document.getElementById('MasterPage_CPContenuto_cmbTipoProcesso')) {
                var tipoProcesso = $find("MasterPage_CPContenuto_cmbTipoProcesso");
                var idtipoProcesso = tipoProcesso.get_value();
            }

            var tipoNorma = $find("MasterPage_CPContenuto_cmbQuadroNormativo");
            var idtipoNorma = tipoNorma.get_value();

            if (idtipoNorma == 2) {
                if (idtipoProcesso != '') {
                    //GetRadWindow().close();
                    //var oWnd = $find('"MasterPage_RadWindow1');
                    var oWnd = GetRadWindow();

                    oWnd.setUrl('../Gestione_locatari/CreaIstanza.aspx?IDM=' + idtipoProcesso + '&IDC=' + document.getElementById('idcont').value);
                    oWnd.setSize(650, 550);
                    oWnd.show();

                    // window.open('../Gestione_locatari/CreaIstanza.aspx?IDM=' + idtipoProcesso + '&IDC=' + document.getElementById('idcont').value);

                }
                else {
                    apriAlert('Selezionare il tipo di processo!', 300, 150, 'Attenzione', null, '../StandardTelerik/Immagini/Messaggi/alert.png');
                }
            }
            else {
                //GetRadWindow().close();
                window.open('../VSA/Locatari/nuova_domanda.aspx?ID=' + document.getElementById('idcont').value + '&COD=' + document.getElementById('CodContratto').value + '&INTEST=' + document.getElementById('id_intest').value, 'Nuova_domanda', 'height=570,top=0,left=0,width=820,scrollbars=no');

            }


        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Gestione Locatari"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:Button ID="btnProcedi" runat="server" Text="Procedi" ToolTip="Procedi" OnClientClick="apriIstanza();" />
    <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" CausesValidation="False"
        OnClientClick="CancelEdit();" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="PanelNorma">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PanelNorma" LoadingPanelID="RadAjaxLoadingPanelNorma" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanelNorma" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <div style="clear: both; float: left;">
        <asp:Panel runat="server" ID="PanelNorma">
            <table>
                <tr>
                    <td class="tdNoWrapWidthBlock">
                        Quadro Normativo:
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmbQuadroNormativo" runat="server" Culture="it-IT" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" LoadingMessage="Caricamento..." ResolvedRenderMode="Classic"
                            AutoPostBack="True" Width="200px">
                            <Items>
                                <telerik:RadComboBoxItem Text="R.R. 1/2004 e s.m.i." Value="1" />
                                <telerik:RadComboBoxItem Text="R.R. 4/2017 e s.m.i." Value="2" />
                            </Items>
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblTipoProcesso" runat="server" Text="Tipo Processo" Font-Names="arial"
                            Font-Size="9pt" Visible="false"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="cmbTipoProcesso" runat="server" Culture="it-IT" EnableLoadOnDemand="true"
                            HighlightTemplatedItems="true" LoadingMessage="Caricamento..." ResolvedRenderMode="Classic"
                            Visible="false" Width="200px">
                            
                        </telerik:RadComboBox>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </div>
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
</asp:Content>
