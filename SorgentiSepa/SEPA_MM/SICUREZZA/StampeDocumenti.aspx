<%@ Page Title="" Language="VB" MasterPageFile="~/SICUREZZA/HomePage.master" AutoEventWireup="false" CodeFile="StampeDocumenti.aspx.vb" Inherits="SICUREZZA_StampeDocumenti" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" Runat="Server">
<asp:HiddenField ID="idIntervento" runat="server" Value="0" ClientIDMode="Static" />
<asp:HiddenField ID="tipoIntervento" runat="server" Value="0" ClientIDMode="Static" />
<asp:HiddenField ID="statoIntervento" runat="server" Value="0" ClientIDMode="Static" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" Runat="Server">
</asp:Content>

