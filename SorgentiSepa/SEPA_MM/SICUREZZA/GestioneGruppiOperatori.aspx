<%@ Page Title="" Language="VB" MasterPageFile="~/SICUREZZA/HomePage.master" AutoEventWireup="false"
    CodeFile="GestioneGruppiOperatori.aspx.vb" Inherits="SICUREZZA_GestioneGruppiOperatori" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    Gestione Operatori Gruppi
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table style="width: 100%;">
        <tr>
            <td style="width: 85%;">
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label33" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                                Text="Gruppo"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cmbGruppo" runat="server" Font-Bold="True" Font-Names="arial"
                                Font-Size="10pt" Style="margin-left: 0px" AutoPostBack="True" CausesValidation="True"
                                Width="250px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
            <td valign="top">
                &nbsp;
            </td>
        </tr>
        <tr valign="top">
            <td width="85%">
                <telerik:RadGrid ID="RadGridOperatori" runat="server" AllowSorting="True" GroupPanelPosition="Top"
                    ResolvedRenderMode="Classic" AutoGenerateColumns="False" PageSize="100" Culture="it-IT"
                    RegisterWithScriptManager="False" Font-Size="8pt" Font-Names="Arial" Width="100%"
                    IsExporting="False">
                    <MasterTableView NoMasterRecordsText="Nessun operatore presente" ShowHeadersWhenNoRecords="true">
                        <Columns>
                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_GRUPPO" HeaderText="ID_GRUPPO" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="OPERATORE" HeaderText="OPERATORE">
                                <HeaderStyle Width="33%" />
                                <ItemStyle Width="33%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="COGNOME" HeaderText="COGNOME">
                                <HeaderStyle Width="33%" />
                                <ItemStyle Width="33%" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NOME" HeaderText="NOME">
                                <HeaderStyle Width="33%" />
                                <ItemStyle Width="33%" />
                            </telerik:GridBoundColumn>
                        </Columns>
                        <PagerStyle AlwaysVisible="True"></PagerStyle>
                    </MasterTableView>
                    <GroupingSettings CollapseAllTooltip="Collapse all groups"></GroupingSettings>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="false" />
                </telerik:RadGrid>
                <asp:TextBox ID="txtOperatoreSelected" runat="server" Font-Bold="True" Font-Names="Arial"
                    Font-Size="8" BorderStyle="None" BackColor="Transparent" BorderColor="Transparent"
                    ClientIDMode="Static" Width="80%"></asp:TextBox>
            </td>
            <td width="15%" valign="top">
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td>
                            <telerik:RadButton ID="btnAggiungi" runat="server" Text="Aggiungi" Width="84px" AutoPostBack="false"
                                OnClientClicking="function(sender, args){openWindow(sender, args, 'MasterPage_CPFooter_RadWindowAggiungi');}">
                            </telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btnEliminaElemento" runat="server" Text="Elimina" OnClientClicking="function(sender, args){deleteElementTelerik(sender, args, 'LBLID');}"
                                Width="84px">
                            </telerik:RadButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="LBLID" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="TextBox1" runat="server" />
    <asp:HiddenField ID="HfContenteDivHeight" runat="server" ClientIDMode="Static" Value="150" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <script type="text/javascript">
        validNavigation = false;
    </script>
    <telerik:RadWindow ID="RadWindowAggiungi" runat="server" CenterIfModal="true" Modal="true"
        Width="300px" Height="180px" VisibleStatusbar="false" Behaviors="Pin,Close" RestrictionZoneID="RestrictionZoneID"
        Title="Assegna Operatore">
        <ContentTemplate>
            <table width="100%" cellpadding="0" cellspacing="0" class="tblDiv">
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Operatore" Font-Names="arial" Font-Size="8pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="cmbOperatore" runat="server" Font-Bold="False" Font-Names="arial"
                            Font-Size="8pt" Style="margin-left: 0px" Width="250px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp
                    </td>
                </tr>
                <tr align="right">
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSalvaOp" runat="server" Text="Salva" />
                                </td>
                                <td style="text-align: right">
                                    <telerik:RadButton ID="RadButtonEsci" runat="server" Text="Esci" AutoPostBack="false"
                                        OnClientClicking="function(sender, args){closeWindow(sender, args, 'MasterPage_CPFooter_RadWindowAggiungi', '');}">
                                    </telerik:RadButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </telerik:RadWindow>
</asp:Content>
