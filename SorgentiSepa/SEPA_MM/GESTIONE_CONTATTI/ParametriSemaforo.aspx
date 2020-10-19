<%@ Page Title="" Language="VB" MasterPageFile="~/GESTIONE_CONTATTI/HomePage.master"
    AutoEventWireup="false" CodeFile="ParametriSemaforo.aspx.vb" Inherits="GESTIONE_CONTATTI_ParametriSemaforo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="Label1" Text="Impostazione parametri per cambi di priorità" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table border="0" cellpadding="2" cellspacing="2">
        <tr>
            <td>
                <b>CAMBIO DI PRIORITA'</b>
            </td>
            <td>
                <b>TEMPO DI GIACENZA</b>
            </td>
            <td>
                <b>NUMERO DI NOTE</b>
            </td>
        </tr>
        <tr>
            <td>
                da VERDE a GIALLO
            </td>
            <td>
                <asp:TextBox ID="TextBoxGiorniGiacenza1" runat="server" MaxLength="3" Width="30px"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="TextBoxNote1" runat="server" MaxLength="3" Width="30px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                da GIALLO a ROSSO
            </td>
            <td>
                <asp:TextBox ID="TextBoxGiorniGiacenza2" runat="server" MaxLength="3" Width="30px"></asp:TextBox>
            </td>
            <td>
                <asp:TextBox ID="TextBoxNote2" runat="server" MaxLength="3" Width="30px"></asp:TextBox>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="giacenza1" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="giacenza2" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="note1" runat="server" ClientIDMode="Static" />
    <asp:HiddenField ID="note2" runat="server" ClientIDMode="Static" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPFooter" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:Button ID="ButtonSalva" runat="server" Text="Applica" ClientIDMode="Static"
                    CssClass="bottone" />
            </td>
            <td>
                <asp:Button ID="imgEsci" runat="server" Text="Esci" ClientIDMode="Static" CssClass="bottone" />
            </td>
        </tr>
    </table>
</asp:Content>
