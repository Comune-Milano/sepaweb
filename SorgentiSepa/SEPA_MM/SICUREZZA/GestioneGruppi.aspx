<%@ Page Title="" Language="VB" MasterPageFile="~/SICUREZZA/HomePage.master" AutoEventWireup="false"
    CodeFile="GestioneGruppi.aspx.vb" Inherits="SICUREZZA_GestioneGruppi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    Gestione Gruppi
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <asp:Button ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <table style="width: 100%;" cellpadding="2" cellspacing="2">
        <tr valign="top">
            <td width="85%">
                <telerik:RadGrid ID="RadGridGruppi" runat="server" AllowSorting="True" GroupPanelPosition="Top"
                    ResolvedRenderMode="Classic" AutoGenerateColumns="False" PageSize="100" Culture="it-IT"
                    RegisterWithScriptManager="False" Font-Size="8pt" Font-Names="Arial" MasterTableView-HeaderStyle-Wrap="true"
                    Width="100%">
                    <MasterTableView EnableHierarchyExpandAll="true" NoMasterRecordsText="Nessun gruppo presente"
                        HierarchyLoadMode="Client" ShowHeadersWhenNoRecords="true">
                        <Columns>
                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NOME_GRUPPO" HeaderText="NOME GRUPPO">
                                <HeaderStyle Width="100%" />
                                <ItemStyle Width="100%" />
                            </telerik:GridBoundColumn>
                        </Columns>
                        <PagerStyle AlwaysVisible="True"></PagerStyle>
                        <HeaderStyle Wrap="True" />
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true">
                        <Selecting AllowRowSelect="true" />
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                    </ClientSettings>
                </telerik:RadGrid>
                <asp:TextBox ID="txtGruppoSelected" runat="server" Font-Bold="True" Font-Names="Arial"
                    Font-Size="8" BorderStyle="None" BackColor="Transparent" BorderColor="Transparent"
                    ClientIDMode="Static" Width="80%"></asp:TextBox>
            </td>
            <td width="15%" valign="top">
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td>
                            <telerik:RadButton ID="btnAggiungi" runat="server" Text="Aggiungi" Width="84px" AutoPostBack="false"
                                OnClientClicking="function(sender, args){document.getElementById('NavigationMenu').style.visibility = 'hidden';document.getElementById('CPContenuto_TextBox1').value='1';document.getElementById('txtDenominazione').value='';openWindow(sender, args, 'MasterPage_CPFooter_RadWindowAggiungi');}">
                            </telerik:RadButton>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btnModifica" runat="server" Text="Modifica" Width="84px" AutoPostBack="false"
                                OnClientClicking=" function(sender, args){document.getElementById('NavigationMenu').style.visibility = 'hidden';if (document.getElementById('LBLID').value != '') {document.getElementById('CPContenuto_TextBox1').value='2';document.getElementById('txtDenominazione').value=document.getElementById('Denominazione').value;openWindow(sender, args, 'MasterPage_CPFooter_RadWindowAggiungi');}else{  apriAlert(Messaggio.Elemento_No_Selezione, 300, 150, Messaggio.Titolo_Conferma, null, null); document.getElementById('CPContenuto_TextBox1').value='0';document.getElementById('NavigationMenu').style.visibility = 'visible';}}">
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
    <asp:HiddenField ID="TextBox1" runat="server" Value="" />
    <asp:HiddenField ID="HfContenteDivHeight" runat="server" ClientIDMode="Static" Value="150" />
    <asp:HiddenField runat="server" ID="Denominazione" ClientIDMode="Static" />
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <script type="text/javascript">
        validNavigation = false;
    </script>
    <telerik:RadWindow ID="RadWindowAggiungi" runat="server" CenterIfModal="true" Modal="true"
        Width="300px" Height="180px" VisibleStatusbar="false" Behaviors="Pin,Close" RestrictionZoneID="RestrictionZoneID"
        Title="Gruppo">
        <ContentTemplate>
            <table style="width: 100%;" class="tblDiv">
                <tr style="text-align: left">
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Denominazione" Font-Names="arial" Font-Size="8pt"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:TextBox ID="txtDenominazione" runat="server" MaxLength="100" Width="222px" CssClass="CssMaiuscolo"
                            ClientIDMode="Static"></asp:TextBox>
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
                                    <asp:Button ID="btnSalvaDen" runat="server" Text="Salva" />
                                </td>
                                <td>
                                    <telerik:RadButton ID="RadButtonEsci" runat="server" Text="Esci" AutoPostBack="false"
                                        OnClientClicking="function(sender, args){document.getElementById('NavigationMenu').style.visibility = 'visible';closeWindow(sender, args, 'MasterPage_CPFooter_RadWindowAggiungi', '');}">
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
