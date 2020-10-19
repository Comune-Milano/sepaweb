<%@ Page Title="" Language="VB" MasterPageFile="~/SICUREZZA/HomePage.master" AutoEventWireup="false"
    CodeFile="Eventi.aspx.vb" Inherits="SICUREZZA_Eventi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label Text="Eventi segnalazione" runat="server" ID="lblTitolo" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:Button ID="imgIndietro" runat="server" Text="Indietro"  ToolTip="Indietro" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <asp:DataGrid runat="server" ID="DataGridEventi" AutoGenerateColumns="True" CellPadding="2"
        Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
        GridLines="None" Width="98%" CellSpacing="2" PageSize="50">
        <ItemStyle BackColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
            Position="TopAndBottom" />
        <AlternatingItemStyle BackColor="Gainsboro" />
        <EditItemStyle BackColor="White" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
            Font-Underline="False" HorizontalAlign="Center" />
        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
    </asp:DataGrid>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:HiddenField runat="server" ID="idSegnalazione" ClientIDMode="Static" />
    <script type="text/javascript">
        validNavigation = false;
    </script>
</asp:Content>
