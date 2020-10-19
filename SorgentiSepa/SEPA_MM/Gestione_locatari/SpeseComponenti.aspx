<%@ Page Title="" Language="VB" MasterPageFile="~/Gestione_locatari/MasterGLocat.master"
    AutoEventWireup="false" CodeFile="SpeseComponenti.aspx.vb" Inherits="Gestione_locatari_SpeseComponenti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Spese Componenti Nucleo"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:Button ID="btnSalva" runat="server" Text="Salva" ToolTip="Salva" OnClientClick="document.getElementById('frmModify').value='0';" />
    <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" CausesValidation="False"
        OnClientClick="ChiudiFinestra(document.getElementById('HFBtnToClick').value);" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table style="width: 80%;">
        <tr>
            <td>
                Descrizione:
            </td>
            <td>
                <telerik:RadTextBox ID="txtDescrizione" runat="server">
                </telerik:RadTextBox>
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
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:HiddenField ID="HFBtnToClick" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="idSpesa" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="operazione" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="frmModify" runat="server" Value="0" ClientIDMode="Static" />
</asp:Content>
