﻿<%@ Page Title="" Language="VB" MasterPageFile="~/RILEVAZIONI/HomePage.master" AutoEventWireup="false"
    CodeFile="ReportUIriattam.aspx.vb" Inherits="RILEVAZIONI_ReportUIriattam" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    Ricerca Unità Valori Riattamento
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:Button ID="btnAvvia" runat="server" CssClass="bottone" Text="Avvia ricerca" />
    <asp:Button ID="btnEsci" runat="server" CssClass="bottone" Text="Esci" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table style="width: 100%;">
        <tr style="height: 60%">
            <td>
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
    </script>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <asp:Label ID="lblErrore" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
</asp:Content>
