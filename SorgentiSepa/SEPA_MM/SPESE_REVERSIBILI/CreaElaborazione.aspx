<%@ Page Title="" Language="VB" MasterPageFile="~/SPESE_REVERSIBILI/HomePage.master"
    AutoEventWireup="false" CodeFile="CreaElaborazione.aspx.vb" Inherits="SPESE_REVERSIBILI_CreaElaborazione" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok" ClientIDMode="Static" 
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <table border="0" cellpadding="2" cellspacing="2" width="100%">
        <tr>
            <td>Selezione del piano finanziario
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadComboBox ID="cmbPiano" runat="server" AutoPostBack="true" Filter="Contains"
                    LoadingMessage="Caricamento..." Width="400px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>Descrizione dell'elaborazione
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="TextBoxNoteElaborazione" runat="server" Height="100%"
                    MaxLength="4000" Rows="20"
                    TextMode="MultiLine" Width="90%"></asp:TextBox>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:Button ID="ButtonCreaElaborazione" runat="server" Text="Crea nuova elaborazione"
        ToolTip="Crea nuova elaborazione" OnClientClick="caricamento(1);" />
</asp:Content>
