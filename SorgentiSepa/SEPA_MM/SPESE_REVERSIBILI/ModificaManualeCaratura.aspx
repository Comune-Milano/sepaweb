<%@ Page Title="Modifica manuale carature" Language="VB" MasterPageFile="HomePage.master"
    AutoEventWireup="false" CodeFile="ModificaManualeCaratura.aspx.vb" Inherits="SPESE_REVERSIBILI_ModificaManualeCaratura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
 <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok" ClientIDMode="Static" 
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <table width="90%" cellspacing="5" cellpadding="3">
    <tr>
            <td style="width: 15%">
                Codice UI
            </td>
            <td style="width: 85%">
                <asp:TextBox runat="server" ID="TextBoxUI" MaxLength="17" Width="200px" />
            </td>
        </tr>
        <tr>
            <td style="width: 15%">
                Complesso
            </td>
            <td style="width: 85%">
                <telerik:RadComboBox ID="DrLComplesso" runat="server" AppendDataBoundItems="true"
                    AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                    ToolTip="Complesso" Width="400px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Edificio
            </td>
            <td>
                <telerik:RadComboBox ID="cmbEdificio" runat="server" AppendDataBoundItems="true"
                    AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                    ToolTip="Edificio" Width="400px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Indirizzo
            </td>
            <td>
                <telerik:RadComboBox ID="cmbIndirizzo" runat="server" AppendDataBoundItems="true"
                    AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                    ToolTip="Indirizzo" Width="400px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Civico
            </td>
            <td>
                <telerik:RadComboBox ID="cmbCivico" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                    Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                    ToolTip="Civico" Width="100px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Scala
            </td>
            <td>
                <telerik:RadComboBox ID="cmbScala" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                    Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                    ToolTip="Scala" Width="100px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Interno
            </td>
            <td>
                <telerik:RadComboBox ID="cmbInterno" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                    Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                    ToolTip="Interno" Width="100px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Ascensore
            </td>
            <td>
                <telerik:RadComboBox ID="cmbAscensore" runat="server" AppendDataBoundItems="true"
                    AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                    ToolTip="Ascensore" Width="100px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Tipologie
            </td>
            <td>
                <div style="height: 250px;width:95%;overflow: auto;">
                    <asp:CheckBoxList ID="chkListTipologie" runat="server" 
                        ToolTip="Tipologie unità immobiliari" RepeatColumns="3" Width="100%">
                    </asp:CheckBoxList>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                
            </td>
            <td>
                <asp:HiddenField ID="HiddenFieldSelezionaTutti" runat="server" Value="0" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:Button ID="ButtonAvviaricerca" runat="server" Text="Avvia ricerca" OnClientClick="caricamento(1);" />
    <asp:Button ID="btnSelezionaTutto" runat="server" Text="Sel./desel. tipologie" ToolTip="Seleziona/deseleziona tipologie" OnClientClick="caricamento(1);" />
    <asp:Button ID="ButtonEsci" runat="server" Text="Esci" OnClientClick="tornaHome();return false;" />
</asp:Content>
