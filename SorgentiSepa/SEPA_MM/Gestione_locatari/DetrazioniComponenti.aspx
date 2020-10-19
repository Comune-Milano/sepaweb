<%@ Page Title="" Language="VB" MasterPageFile="~/Gestione_locatari/MasterGLocat.master" AutoEventWireup="false" CodeFile="DetrazioniComponenti.aspx.vb" Inherits="Gestione_locatari_DetrazioniComponenti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" Runat="Server">
<asp:Label ID="lblTitolo" runat="server" Text="Detrazioni Componenti"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" Runat="Server">
 <asp:Button ID="btnSalva" runat="server" Text="Salva" ToolTip="Salva" OnClientClick="document.getElementById('frmModify').value='0';">
    </asp:Button>
    <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" CausesValidation="False"
        OnClientClick="ChiudiFinestra(document.getElementById('HFBtnToClick').value);" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" Runat="Server">
<table style="width: 80%;">
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
        </tr>
        <tr>
            <td>
                Tipo Detrazione:
            </td>
            <td>
                <telerik:RadComboBox ID="cmbDetrazione" runat="server" Culture="it-IT" EnableLoadOnDemand="true"
                    HighlightTemplatedItems="true" LoadingMessage="Caricamento..." ResolvedRenderMode="Classic"
                    Width="200px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Importo:
            </td>
            <td>
                <telerik:RadNumericTextBox ID="txtImporto" runat="server" Width="100px" NumberFormat-DecimalDigits="2"
                    MinValue="0" EnabledStyle-HorizontalAlign="Right">
                </telerik:RadNumericTextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" Runat="Server">
 <asp:HiddenField ID="HFBtnToClick" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="idDetrazioni" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="iddich" runat="server" Value="-1" ClientIDMode="Static" />
    <asp:HiddenField ID="operazione" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="frmModify" runat="server" Value="0" ClientIDMode="Static" />
</asp:Content>

