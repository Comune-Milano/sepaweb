<%@ Page Title="Report Bollette Gestionali" Language="VB" MasterPageFile="~/Contratti/Spalmatore/HomePage.master" AutoEventWireup="false" CodeFile="RptBollGestionali.aspx.vb" Inherits="Contratti_Spalmatore_RptBollGestionali" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function apriMaschera() {
            location.replace('VisualizzazioneRpt.aspx');
        };

        function confermaExport(btnToClik) {
            MultiConfermaJS(btnToClik, 'Attenzione', 'Vuoi esportare tutte le scritture gestionali presenti a sistema?');

        };
        function procedi(sender, args) {

            if (sender == true) {
                document.getElementById('btnExport').click();
            }
            else {
                location.href = 'SpalmatoreHome.aspx';
            }
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:Button runat="server" ID="btnExport" Text="" Style="display: none;"
        ClientIDMode="Static" />
    <script type="text/javascript">
        //document.getElementById("btnExport").click();
    </script>
</asp:Content>

