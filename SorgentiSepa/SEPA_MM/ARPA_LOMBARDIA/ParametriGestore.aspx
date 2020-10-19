<%@ Page Title="" Language="VB" MasterPageFile="HomePage.master" AutoEventWireup="false"
    CodeFile="ParametriGestore.aspx.vb" Inherits="ARPA_LOMBARDIA_ParametriGestore" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text="Gestione Parametri Gestore"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <telerik:RadButton ID="btnSalva" runat="server" Text="Salva" ToolTip="Salva">
    </telerik:RadButton>
    <telerik:RadButton ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" OnClientClicking="TornaHome"
        AutoPostBack="false">
    </telerik:RadButton>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <br />
    <telerik:RadGrid ID="RadGridGestore" runat="server" AllowSorting="false" AutoGenerateColumns="False"
        AllowFilteringByColumn="false" EnableLinqExpressions="False" IsExporting="False"
        Width="97%">
        <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="Nessun dato da visualizzare."
            ShowHeadersWhenNoRecords="true" TableLayout="Auto" ClientDataKeyNames="COD" DataKeyNames="COD">
            <Columns>
                <telerik:GridBoundColumn DataField="COD" HeaderText="COD" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DESCRIZIONE_PATRIMONIO" HeaderText="DESCRIZIONE">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="CODICE FISCALE" AllowFiltering="false">
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtCodiceFiscale" runat="server" Width="97%" Text='<%# DataBinder.Eval(Container, "DataItem.CODICE_FISCALE") %>'
                            MaxLength="16">
                        </telerik:RadTextBox>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="RAGIONE SOCIALE" AllowFiltering="false">
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtRagioneSociale" runat="server" Width="97%" Text='<%# DataBinder.Eval(Container, "DataItem.RAG_SOCIALE") %>'
                            MaxLength="100">
                        </telerik:RadTextBox>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridTemplateColumn HeaderText="DENOMINAZIONE" AllowFiltering="false">
                    <ItemTemplate>
                        <telerik:RadTextBox ID="txtDenominazione" runat="server" Width="97%" Text='<%# DataBinder.Eval(Container, "DataItem.DENOMINAZIONE") %>'
                            MaxLength="100">
                        </telerik:RadTextBox>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
            </Columns>
            <SortExpressions>
                <telerik:GridSortExpression FieldName="DESCRIZIONE_PATRIMONIO" SortOrder="Ascending" />
            </SortExpressions>
        </MasterTableView>
        <ClientSettings EnableRowHoverStyle="true">
            <Scrolling AllowScroll="True" UseStaticHeaders="True" SaveScrollPosition="true" />
        </ClientSettings>
    </telerik:RadGrid>
</asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="CPFooter" runat="Server">
    <telerik:RadNotification ID="RadNotificationNote" runat="server" Title="Sep@Com"
        Height="85px" Animation="Fade" EnableRoundedCorners="true" EnableShadow="true"
        AutoCloseDelay="3500" Position="BottomRight" OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
    </telerik:RadNotification>
    <asp:HiddenField ID="HFGriglia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightGriglia" runat="server" Value="70" ClientIDMode="Static" />
</asp:Content>
