<%@ Page Title="Modifica manuale carature" Language="VB" MasterPageFile="HomePage.master"
    AutoEventWireup="false" CodeFile="RisultatiModificaManuale.aspx.vb" Inherits="SPESE_REVERSIBILI_RisultatiModificaManuale" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="DataGridUI">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="DataGridUI" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok" ClientIDMode="Static"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td style="vertical-align: middle; text-align: center">
                            <img src="../Images/Telerik/Information-icon.png" alt="info" height="16" width="16" />
                        </td>
                        <td style="vertical-align: middle">
                            <asp:Label ID="LabelJolly" runat="server" class="TitoloH1"><i>Tutti i CDR verranno memorizzati fino alla sesta cifra decimale.</i></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <br />
    <telerik:RadGrid ID="DataGridUI" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
        PagerStyle-AlwaysVisible="true" AllowPaging="true" AllowFilteringByColumn="true"
        EnableLinqExpressions="False" Width="97%" AllowSorting="True" IsExporting="False"
        ShowFooter="true"
        PageSize="100">
        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
            CommandItemDisplay="Top" Width="100%" AllowMultiColumnSorting="true">
            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                ShowRefreshButton="true" />
            <Columns>
                <telerik:GridBoundColumn DataField="ID_UNITA" HeaderText="ID_UNITA" Visible="False"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="CODICE_UNITA" HeaderText="CODICE UNITA' IMMOBILIARE" Exportable="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="COD_UNITA_EXPORT" HeaderText="CODICE UNITA' IMMOBILIARE" Visible="false" Exportable="TRUE">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="EDIFICIO" HeaderText="EDIFICIO"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="SCALA" HeaderText="SCALA"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="INTERNO" HeaderText="INTERNO"></telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="PIANO" HeaderText="PIANO"></telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="CDR SERVIZI COMPLESSO">
                    <ItemTemplate>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:TextBox ID="TextBoxServizi" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CARATURA_SERVIZI") %>'
                                        Font-Names="Arial" Font-Size="8pt" CssClass="numero" Width="70px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="CDR SERVIZI EDIFICIO">
                    <ItemTemplate>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:TextBox ID="TextBoxEdificio" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CARATURA_EDIFICIO") %>'
                                        Font-Names="Arial" Font-Size="8pt" CssClass="numero" Width="70px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="CDR RISCALDAMENTO">
                    <ItemTemplate>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:TextBox ID="TextBoxRiscaldamento" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CARATURA_RISCALDAMENTO") %>'
                                        Font-Names="Arial" Font-Size="8pt" CssClass="numero" Width="70px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="CDR ASCENSORE">
                    <ItemTemplate>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:TextBox ID="TextBoxAscensore" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CARATURA_ASCENSORE") %>'
                                        Font-Names="Arial" Font-Size="8pt" CssClass="numero" Width="70px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="CDR MONTASCALE">
                    <ItemTemplate>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:TextBox ID="TextBoxMontascale" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CARATURA_MONTASCALE") %>'
                                        Font-Names="Arial" Font-Size="8pt" CssClass="numero" Width="70px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
            </Columns>
            <CommandItemTemplate>
                <div style="float: right; padding: 4px;">
                    <asp:Button ID="ButtonRefresh" runat="server" OnClick="Refresh_Click" CommandName="Refresh"
                        CssClass="rgRefresh" OnClientClick="caricamento(2);" />
                    <asp:Button ID="ButtonExportExcel" Text="text" runat="server" OnClick="Esporta_Click"
                        CommandName="ExportToExcel" CssClass="rgExpXLS" OnClientClick="caricamento(2);" />
                </div>
            </CommandItemTemplate>
        </MasterTableView>
        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
        <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
            <Excel FileExtension="xls" Format="Xlsx" />
        </ExportSettings>
        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true"
            ClientEvents-OnCommand="onCommand">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true"></Scrolling>
            <Selecting AllowRowSelect="true" />
        </ClientSettings>
    </telerik:RadGrid>
    <asp:HiddenField ID="HFGriglia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightGriglia" runat="server" Value="350" ClientIDMode="Static" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:Button ID="ButtonImpostaCarature" runat="server" Text="Imposta valori" OnClientClick="caricamento(1);" />
    <asp:Button ID="ButtonRicarica" runat="server" Text="Ricarica" OnClientClick="caricamento(1);" />
    <asp:Button ID="ButtonNuovaRicerca" runat="server" OnClientClick="caricamento(2);"
        Text="Nuova ricerca" ToolTip="Nuova ricerca" />
    <asp:Button ID="ButtonEsci" runat="server" OnClientClick="tornaHome();return false;" Text="Esci"
        ToolTip="Esci" />
</asp:Content>
