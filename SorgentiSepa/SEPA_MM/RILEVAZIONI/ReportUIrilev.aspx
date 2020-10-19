<%@ Page Title="" Language="VB" MasterPageFile="~/RILEVAZIONI/HomePage.master" AutoEventWireup="false"
    CodeFile="ReportUIrilev.aspx.vb" Inherits="RILEVAZIONI_ReportUIrilev" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    Ricerca Unità
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:Button ID="btnAvvia" runat="server" CssClass="bottone" Text="Avvia ricerca" />
    <asp:Button ID="btnEsci" runat="server" CssClass="bottone" Text="Esci" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table style="width: 100%;">
        <tr style="height: 60%">
            <td>
                <table align="center">
                    <tr>
                        <td style="font-family: Arial; font-size: 9pt;">
                            Tipo Unità
                        </td>
                        <td style="font-family: Arial; font-size: 9pt;">
                            <asp:DropDownList ID="cmbTipoUI" runat="server" Width="200px" ClientIDMode="Static"
                                AutoPostBack="True">
                                <asp:ListItem Value="1">RILEVATE</asp:ListItem>
                                <asp:ListItem Value="0">NON RILEVATE</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td id="lblMotivaz" style="font-family: Arial; font-size: 9pt;">
                            Motivazione
                        </td>
                        <td id="cmbMotivaz" style="font-family: Arial; font-size: 9pt;">
                            <asp:DropDownList ID="cmbMotivazione" runat="server" Width="200px">
                                <asp:ListItem Value="-1">- - -</asp:ListItem>
                                <asp:ListItem Value="S">SCHEDA NON CARICATA</asp:ListItem>
                                <asp:ListItem Value="L">LASTRATURA</asp:ListItem>
                                <asp:ListItem Value="O">OCCUPATO</asp:ListItem>
                                <asp:ListItem Value="M">MURATO</asp:ListItem>
                                <asp:ListItem Value="C">ASSENZA CHIAVI</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="0" cellspacing="0" width="97%">
                    <tr>
                        <td style="width: 50%;" valign="top">
                            <fieldset style="border: thin solid #CCCCCC;">
                                <legend>SEDE TERRITORIALE</legend>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 100%;">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkSedeTerr" runat="server" AutoPostBack="True" BackColor="#507CD1"
                                                            Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White" Text="SEDE TERRITORIALE"
                                                            Width="100%" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="PanelFiliali" runat="server" ClientIDMode="Static" Style="border: 1px solid #507CD1;
                                                            height: 200px; overflow: auto" onscroll="ScrollPosFiliali(this);">
                                                            <asp:CheckBoxList ID="chkListSedeTerr" runat="server" Font-Names="Arial" Font-Size="7pt"
                                                                Width="90%" AutoPostBack="True">
                                                            </asp:CheckBoxList>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            &nbsp
                        </td>
                        <td>
                            &nbsp
                        </td>
                        <td>
                            &nbsp
                        </td>
                        <td style="width: 50%;" valign="top">
                            <fieldset style="border: thin solid #CCCCCC;">
                                <legend>PARAMETRI COMPLESSO</legend>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 20%">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox Text="COMPLESSO" runat="server" ID="chkComplesso" BackColor="#507CD1"
                                                            Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White" Width="100%"
                                                            AutoPostBack="True" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="PanelComplessi" runat="server" Style="border: 1px solid #507CD1; height: 200px;
                                                            overflow: auto" onscroll="ScrollPosComplessi(this);" ClientIDMode="Static">
                                                            <asp:CheckBoxList ID="chkListComplesso" runat="server" Font-Names="Arial" Font-Size="7pt"
                                                                Width="90%" AutoPostBack="True">
                                                            </asp:CheckBoxList>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 50%;" valign="top">
                            <fieldset style="border: thin solid #CCCCCC;">
                                <legend>PARAMETRI EDIFICIO</legend>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 20%">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox Text="EDIFICIO" runat="server" ID="chkEdificio" BackColor="#507CD1"
                                                            Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White" Width="100%"
                                                            AutoPostBack="True" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="PanelEdifici" runat="server" Style="border: 1px solid #507CD1; height: 200px;
                                                            overflow: auto" onscroll="ScrollPosEdifici(this);" ClientIDMode="Static">
                                                            <asp:CheckBoxList ID="chkListEdificio" runat="server" Font-Names="Arial" Font-Size="7pt"
                                                                Width="90%" AutoPostBack="True">
                                                            </asp:CheckBoxList>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td>
                            &nbsp
                        </td>
                        <td>
                            &nbsp
                        </td>
                        <td>
                            &nbsp
                        </td>
                        <td style="width: 50%;" valign="top">
                            <fieldset style="border: thin solid #CCCCCC;">
                                <legend>STUDIO PROFESSIONALE</legend>
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tr>
                                        <td style="width: 20%;">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td>
                                                        <asp:CheckBox ID="chkStudioProfess" runat="server" AutoPostBack="True" BackColor="#507CD1"
                                                            Font-Bold="True" Font-Names="Arial" Font-Size="10pt" ForeColor="White" Text="STUDIO PROFESSIONALE"
                                                            Width="100%" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Panel ID="PanelStudioProf" runat="server" ClientIDMode="Static" Style="border: 1px solid #507CD1;
                                                            height: 200px; overflow: auto;" onscroll="ScrollPosStudioProf(this);">
                                                            <asp:CheckBoxList ID="chkListStudioProfess" runat="server" Font-Names="Arial" Font-Size="7pt"
                                                                Width="90%" AutoPostBack="True">
                                                            </asp:CheckBoxList>
                                                        </asp:Panel>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="idSelezionato" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="yPosFiliali" runat="server" Value="0" ClientIDMode="Static" />
                <asp:HiddenField ID="yPosComplessi" runat="server" Value="0" ClientIDMode="Static" />
                <asp:HiddenField ID="yPosEdifici" runat="server" Value="0" ClientIDMode="Static" />
                <asp:HiddenField ID="yPosStudioProf" runat="server" Value="0" ClientIDMode="Static" />
            </td>
        </tr>
    </table>
    <script src="Funzioni.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        visibleMotivazioni();
        function ScrollPosFiliali(obj) {
            document.getElementById('yPosFiliali').value = obj.scrollTop;
        };
        function ScrollPosComplessi(obj) {
            document.getElementById('yPosComplessi').value = obj.scrollTop;
        };
        function ScrollPosEdifici(obj) {
            document.getElementById('yPosEdifici').value = obj.scrollTop;
        };
        function ScrollPosStudioProf(obj) {
            document.getElementById('yPosStudioProf').value = obj.scrollTop;
        };
        function visibleMotivazioni() {
            if (document.getElementById('cmbTipoUI').value == '0') {
                document.getElementById('lblMotivaz').style.visibility = 'visible';
                document.getElementById('cmbMotivaz').style.visibility = 'visible';
            } else {
                document.getElementById('lblMotivaz').style.visibility = 'hidden';
                document.getElementById('cmbMotivaz').style.visibility = 'hidden';
            }
        };
    </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:Label ID="lblErrore" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
</asp:Content>
