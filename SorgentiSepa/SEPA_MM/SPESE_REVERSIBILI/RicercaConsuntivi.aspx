<%@ Page Title="Ricerca consuntivi annui" Language="VB" MasterPageFile="HomePage.master"
    AutoEventWireup="false" CodeFile="RicercaConsuntivi.aspx.vb" Inherits="SPESE_REVERSIBILI_RicercaConsuntivi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .RadComboBoxDropDown_Web20 {
            color: #000 !important;
            font-family: Arial !important;
            font-size: 9pt !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok" ClientIDMode="Static"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <table width="90%" cellspacing="5" cellpadding="3">
        <tr>
            <td style="width: 15%">Ricerca dettagliata
            </td>
            <td style="width: 85%">
                <asp:CheckBox runat="server" ID="chkRicercaDettagliata" Checked="false" />
            </td>
        </tr>
        <tr>
            <td style="width: 15%">Codice UI
            </td>
            <td style="width: 85%">
                <asp:TextBox runat="server" ID="txtCodiceUI" MaxLength="17" Width="200px" />
            </td>
        </tr>
        <tr>
            <td style="width: 15%">Intestatario
            </td>
            <td style="width: 85%">
                <telerik:RadTextBox ID="txtIntestatario" runat="server" EmptyMessage="Cognome e nome"  Width="200px" ></telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td style="width: 15%">Complesso
            </td>
            <td style="width: 85%">
                <telerik:RadComboBox ID="DrLComplesso" runat="server" AutoPostBack="true" Filter="Contains"
                    LoadingMessage="Caricamento..." Width="400px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>Edificio
            </td>
            <td>
                <telerik:RadComboBox ID="cmbEdificio" runat="server" AutoPostBack="true" Filter="Contains"
                    LoadingMessage="Caricamento..." Width="400px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>Indirizzo
            </td>
            <td>
                <telerik:RadComboBox ID="cmbIndirizzo" runat="server" AutoPostBack="true" Filter="Contains"
                    LoadingMessage="Caricamento..." Width="400px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>Civico
            </td>
            <td>
                <telerik:RadComboBox ID="cmbCivico" runat="server" AutoPostBack="true" Filter="Contains"
                    LoadingMessage="Caricamento..." Width="400px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>Scala
            </td>
            <td>
                <telerik:RadComboBox ID="cmbScala" runat="server" AutoPostBack="true" Filter="Contains"
                    LoadingMessage="Caricamento..." Width="100px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>Interno
            </td>
            <td>
                <telerik:RadComboBox ID="cmbInterno" runat="server" AutoPostBack="false" Filter="Contains"
                    LoadingMessage="Caricamento..." Width="100px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>Ascensore
            </td>
            <td>
                <telerik:RadComboBox ID="cmbAscensore" runat="server" AutoPostBack="false" Filter="Contains"
                    LoadingMessage="Caricamento..." Width="100px">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>Tipologie
            </td>
            <td>
                <div style="height: 180px; width: 100%; overflow: auto;">
                    <asp:CheckBoxList ID="chkListTipologie" runat="server" ToolTip="Tipologie unità immobiliari"
                        RepeatColumns="5">
                    </asp:CheckBoxList>
                </div>
            </td>
        </tr>
        <tr>
            <td></td>
            <td>
                <asp:HiddenField ID="HiddenFieldSelezionaTutti" runat="server" Value="0" />
            </td>
        </tr>
        <%--<tr>
            <td>
                Dettaglio voce di aggregazione
            </td>
            <td>
                <asp:CheckBox ID="CheckBox1" runat="server" Checked="false" />
            </td>
        </tr>--%>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:Button ID="ButtonAvviaricerca" runat="server" Text="Avvia ricerca" ToolTip="Avvia ricerca" OnClientClick="caricamento(1);" />
    <asp:Button ID="btnSelezionaTutto" runat="server" Text="Sel./desel. tipologie" ToolTip="Seleziona o deseleziona tutte le tipologie" OnClientClick="caricamento(1);" />
    <asp:Button ID="ButtonEsci" runat="server" Text="Esci" ToolTip="Esci" OnClientClick="tornaHome();return false;" />
</asp:Content>
