<%@ Page Title="" Language="VB" MasterPageFile="~/Gestione_locatari/MasterGLocat.master"
    AutoEventWireup="false" CodeFile="PatrimonioImmobComponenti.aspx.vb" Inherits="Gestione_locatari_PatrimonioImmobComponenti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Patrimonio Immobiliare"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:Button ID="btnSalva" runat="server" Text="Salva" ToolTip="Salva" OnClientClick="document.getElementById('frmModify').value='0';" />
    <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" CausesValidation="False"
        OnClientClick="ChiudiFinestra(document.getElementById('HFBtnToClick').value);" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <telerik:RadAjaxManagerProxy ID="RadAjaxManagerProxy1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="cmbTipo">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbTipoImm" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnCalcolaICI">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="txtValore" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManagerProxy>
    <table style="width: 100%;">
        <tr>
            <td>
                Componente:
            </td>
            <td>
                <telerik:RadComboBox ID="cmbComponente" runat="server" Culture="it-IT" EnableLoadOnDemand="true"
                    HighlightTemplatedItems="true" LoadingMessage="Caricamento..." ResolvedRenderMode="Classic"
                    Width="200px">
                </telerik:RadComboBox>
            </td>
            <td>
                Tipologia F.:
            </td>
            <td>
                <telerik:RadComboBox ID="cmbTipo" runat="server" Culture="it-IT" EnableLoadOnDemand="true"
                    HighlightTemplatedItems="true" LoadingMessage="Caricamento..." ResolvedRenderMode="Classic"
                    Width="200px" AutoPostBack="True">
                    <Items>
                        <telerik:RadComboBoxItem Text="FABBRICATI" Value="0" Selected="true" />
                        <telerik:RadComboBoxItem Text="TERRENI" Value="1" />
                        <telerik:RadComboBoxItem Text="AREE EDIFICABILI" Value="2" />
                    </Items>
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                % Proprietà:
            </td>
            <td>
                <telerik:RadNumericTextBox ID="txtPerc" runat="server" Width="100px" NumberFormat-DecimalDigits="2"
                    MinValue="0" EnabledStyle-HorizontalAlign="Right">
                </telerik:RadNumericTextBox>
            </td>
            <td>
                Tipo Proprietà:
            </td>
            <td>
                <telerik:RadComboBox ID="cmbTipoPropr" runat="server" Culture="it-IT" EnableLoadOnDemand="true"
                    HighlightTemplatedItems="true" LoadingMessage="Caricamento..." ResolvedRenderMode="Classic"
                    Width="200px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Rendita Cat.:
            </td>
            <td>
                <telerik:RadNumericTextBox ID="txtRendita" runat="server" Width="100px" NumberFormat-DecimalDigits="2"
                    MinValue="0" EnabledStyle-HorizontalAlign="Right">
                </telerik:RadNumericTextBox>
                <asp:ImageButton ID="btnCalcolaICI" runat="server" ImageUrl="~/NuoveImm/Img_Calcolatrice.png"
                    ToolTip="Calcola Valore IMU" Width="30px" Height="30px" Style="vertical-align: bottom;" />
            </td>
            <td>
                Valore IMU:
            </td>
            <td>
                <telerik:RadNumericTextBox ID="txtValore" runat="server" Width="100px" NumberFormat-DecimalDigits="2"
                    MinValue="0" EnabledStyle-HorizontalAlign="Right">
                </telerik:RadNumericTextBox>
            </td>
        </tr>
        <tr>
            <td>
                Mutuo:
            </td>
            <td>
                <telerik:RadNumericTextBox ID="TxtMutuo" runat="server" Width="100px" NumberFormat-DecimalDigits="2"
                    MinValue="0" EnabledStyle-HorizontalAlign="Right">
                </telerik:RadNumericTextBox>
            </td>
            <td>
                Cat.Catastale:
            </td>
            <td>
                <telerik:RadComboBox ID="cmbTipoImm" runat="server" Culture="it-IT" EnableLoadOnDemand="true"
                    HighlightTemplatedItems="true" LoadingMessage="Caricamento..." ResolvedRenderMode="Classic"
                    Width="200px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Comune:
            </td>
            <td>
                <telerik:RadComboBox ID="cmbComune" runat="server" Culture="it-IT" EnableLoadOnDemand="true"
                    HighlightTemplatedItems="true" LoadingMessage="Caricamento..." ResolvedRenderMode="Classic"
                    Width="200px">
                </telerik:RadComboBox>
            </td>
            <td>
                Num.Vani:
            </td>
            <td>
                <telerik:RadNumericTextBox ID="txtNumVani" runat="server" Width="100px" NumberFormat-DecimalDigits="2"
                    MinValue="0" EnabledStyle-HorizontalAlign="Right">
                </telerik:RadNumericTextBox>
            </td>
        </tr>
        <tr>
            <td>
                Sup.Utile:
            </td>
            <td>
                <telerik:RadNumericTextBox ID="txtSupUtile" runat="server" Width="100px" NumberFormat-DecimalDigits="2"
                    MinValue="0" EnabledStyle-HorizontalAlign="Right">
                </telerik:RadNumericTextBox>
            </td>
            <td>
                Valore di mercato:
            </td>
            <td>
                <telerik:RadNumericTextBox ID="txtValoreMercato" runat="server" Width="100px" NumberFormat-DecimalDigits="2"
                    MinValue="0" EnabledStyle-HorizontalAlign="Right">
                </telerik:RadNumericTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:HiddenField ID="HFBtnToClick" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="iddich" runat="server" Value="-1" ClientIDMode="Static" />
    <asp:HiddenField ID="idImmob" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="operazione" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="frmModify" runat="server" Value="0" ClientIDMode="Static" />
</asp:Content>
