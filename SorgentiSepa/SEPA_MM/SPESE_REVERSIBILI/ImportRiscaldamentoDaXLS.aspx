<%@ Page Title="" Language="VB" MasterPageFile="~/SPESE_REVERSIBILI/HomePage.master"
    AutoEventWireup="false" CodeFile="ImportRiscaldamentoDaXLS.aspx.vb" Inherits="SPESE_REVERSIBILI_ImportRiscaldamentoDaXLS" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function Conferma() {
            if (document.getElementById('cmbCriterioRipartizione').value == '' || document.getElementById('cmbTipologiaDivisione').value == ''
                || document.getElementById('cmbTipologiaCaratura').value == '' || document.getElementById('cmbCategoriaCarature').value == ''
                || document.getElementById('FileUpload1').value == '') {
                message('Attenzione', 'Valorizzare tutti i campi!');
                return false;
            }
            else {
                return true;
            };
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok" ClientIDMode="Static"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <table>
        <tr>
            <td>
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td style="vertical-align: middle; text-align: center">
                            <img src="../Images/Telerik/Information-icon.png" alt="info" height="16" width="16" />
                        </td>
                        <td style="vertical-align: middle">
                            <asp:Label ID="LabelJolly" runat="server" class="TitoloH1"><i>L'operazione di importazione cancella preventivamente le precedenti spese di riscaldamento importate nel prospetto.</i>
                            <i>Il file da importare deve essere composto da 2 colonne: nella prima indicare il codice identificativo dell'elemento, nella seconda l'importo annuo della spesa.</i></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td style="width: 10%">Criterio di ripartizione
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cmbCriterioRipartizione" Width="30%" AppendDataBoundItems="true"
                                ClientIDMode="Static" Filter="Contains" runat="server" AutoPostBack="false" ResolvedRenderMode="Classic"
                                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%">Tipologia di divisione
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cmbTipologiaDivisione" Width="30%" AppendDataBoundItems="true"
                                ClientIDMode="Static" Filter="Contains" runat="server" AutoPostBack="false" ResolvedRenderMode="Classic"
                                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%">Tipologia valore millesimale
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cmbTipologiaCaratura" Width="30%" AppendDataBoundItems="true"
                                ClientIDMode="Static" Filter="Contains" runat="server" AutoPostBack="TRUE" ResolvedRenderMode="Classic"
                                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%">Categoria
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cmbCategoriaCarature" Width="30%" AppendDataBoundItems="true"
                                ClientIDMode="Static" Filter="Contains" runat="server" AutoPostBack="false" ResolvedRenderMode="Classic"
                                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%">Seleziona un file
                        </td>
                        <td>
                            <asp:FileUpload ID="FileUpload1" runat="server" Width="500px" ClientIDMode="Static" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <tr>
        <td style="width: 10%">
            <asp:Button ID="btnAllega" runat="server" Text="Upload" OnClientClick="if (!Conferma()){return false;};" />
            <asp:Button ID="ButtonEsci" runat="server" Text="Esci" OnClientClick="tornaHome();return false;" />
        </td>
    </tr>
</asp:Content>
