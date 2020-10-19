<%@ Page Title="" Language="VB" MasterPageFile="PageModal.master" AutoEventWireup="false"
    CodeFile="AggiungiSpesaConsuntivi.aspx.vb" Inherits="SPESE_REVERSIBILI_AggiungiSpesaConsuntivi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
      
        //funzioni nuove

        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) {
                oWindow = window.radWindow;
            } else {
                if (window.frameElement) {
                    if (window.frameElement.radWindow) {
                        oWindow = window.frameElement.radWindow;
                    };
                };
            };
            return oWindow;
        };
        function refreshPage(btnToClik) {

            if (document.getElementById(btnToClik)) {
                var attr;
                attr = $('#' + btnToClik).attr('onclick');
                $('#' + btnToClik).attr('onclick', '');
                document.getElementById(btnToClik).click();
                $('#' + btnToClik).attr('onclick', attr);

            };
        };
        function CloseAndRefresh(pulsante) {
            GetRadWindow().close();
            GetRadWindow().BrowserWindow.refreshPage(pulsante);
        };


        //fine
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok" ClientIDMode="Static" 
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server">
    </telerik:RadFormDecorator>
    <br />
    <table border="0" cellpadding="2" cellspacing="2">
        <tr>
            <td colspan="2">
                <asp:Button ID="ButtonAggiungiSpesa" runat="server" Text="Aggiungi" ToolTip="Aggiungi spesa" />
                <asp:Button ID="ButtonEsci" runat="server" Text="Esci" ToolTip="Esci" OnClientClick="CloseAndNextJS(document.getElementById('HFBtnToClick').value);return false;" />
            </td>
        </tr>
        <tr>
            <td style="width: 25%">
                Tipologia spesa
            </td>
            <td style="width: 75%">
                <telerik:RadComboBox ID="DropDownListTipologiaSpesa" runat="server" AppendDataBoundItems="true"
                    AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                    Width="90%">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Descrizione spesa
            </td>
            <td>
                <asp:TextBox ID="TextBoxDescrizioneSpesa" runat="server" Width="90%"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Categoria
            </td>
            <td>
                <telerik:RadComboBox ID="DropDownListCategoria" runat="server" AppendDataBoundItems="true"
                    AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                    ToolTip="Categoria" Width="90%">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Importo
            </td>
            <td>
                <asp:TextBox ID="TextBoxImporto" runat="server" Width="100px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                Tipologia di divisione
            </td>
            <td>
                <telerik:RadComboBox ID="DropDownListTipologiaDivisione" runat="server" AppendDataBoundItems="true"
                    AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                    ToolTip="Tipologia divisione" Width="90%" Enabled="False">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Criterio di ripartizione
            </td>
            <td>
                <telerik:RadComboBox ID="DropDownListCriterioRipartizione" runat="server" AppendDataBoundItems="true"
                    AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                    ToolTip="Criterio ripartizione" Width="90%">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Complesso
            </td>
            <td>
                <telerik:RadComboBox ID="DropDownListComplesso" runat="server" AppendDataBoundItems="true"
                    AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                    ToolTip="Complesso" Width="90%">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Lotto
            </td>
            <td>
                <telerik:RadComboBox ID="DropDownListLotto" runat="server" AppendDataBoundItems="true"
                    AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                    ToolTip="Lotto" Width="90%">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Edificio
            </td>
            <td>
                <telerik:RadComboBox ID="DropDownListEdificio" runat="server" AppendDataBoundItems="true"
                    AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                    ToolTip="Edificio" Width="90%">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Scala
            </td>
            <td>
                <telerik:RadComboBox ID="DropDownListScala" runat="server" AppendDataBoundItems="true"
                    AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                    ToolTip="Scala" Width="90%">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Impianto
            </td>
            <td>
                <telerik:RadComboBox ID="DropDownListImpianti" runat="server" AppendDataBoundItems="true"
                    AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                    ToolTip="Impianti" Width="90%">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Aggregazione
            </td>
            <td>
                <telerik:RadComboBox ID="DropDownListAggregazione" runat="server" AppendDataBoundItems="true"
                    AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                    ToolTip="Aggregazione" Width="90%">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td>
                Tabella millesimale
            </td>
            <td>
                <telerik:RadComboBox ID="DropDownListTabellaMillesimale" runat="server" AppendDataBoundItems="true"
                    AutoPostBack="false" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                    ToolTip="Tabella millesimale" Width="90%">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 50px;">
                <br />
                <asp:Label ID="LabelErrore" runat="server" Font-Names="Arial" Font-Size="8pt" ForeColor="Maroon" />
                <br />
            </td>
        </tr>
    </table>
    <script type="text/javascript">
        function CloseAndRefresh(pulsante) {
            GetRadWindow().close();
            GetRadWindow().BrowserWindow.refreshPage(pulsante);
        };
    </script>
    <asp:HiddenField ID="HFBtnToClick" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="idSpesa" runat="server" Value="-1" ClientIDMode="Static" />
    <asp:HiddenField ID="idMilles" runat="server" Value="-1" ClientIDMode="Static" />
</asp:Content>
