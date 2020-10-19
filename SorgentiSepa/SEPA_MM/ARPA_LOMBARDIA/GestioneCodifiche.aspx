<%@ Page Title="" Language="VB" MasterPageFile="OpenPage.master" AutoEventWireup="false"
    CodeFile="GestioneCodifiche.aspx.vb" Inherits="ARPA_LOMBARDIA_GestioneCodifiche" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label ID="lblTitolo" runat="server" Text=""></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPMenu" runat="Server">
    <br />
    <telerik:RadButton ID="btnSalva" runat="server" Text="Salva" ToolTip="Salva Codifiche">
    </telerik:RadButton>
    <telerik:RadButton ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" OnClientClicking="closeRadWindowGestCodifiche"
        AutoPostBack="false">
    </telerik:RadButton>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPContenuto" runat="Server">
    <br />
    <telerik:RadGrid ID="RadGridGestioneCodifiche" runat="server" AllowSorting="false"
        AutoGenerateColumns="False" AllowFilteringByColumn="false" EnableLinqExpressions="False"
        IsExporting="False" Width="97%">
        <MasterTableView CommandItemDisplay="None" NoMasterRecordsText="Nessun dato da visualizzare."
            ShowHeadersWhenNoRecords="true" TableLayout="Auto" ClientDataKeyNames="ID" DataKeyNames="ID">
            <Columns>
                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false">
                </telerik:GridBoundColumn>
                <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Left" />
                </telerik:GridBoundColumn>
                <telerik:GridTemplateColumn HeaderText="CODIFICA" AllowFiltering="false">
                    <ItemTemplate>
                        <telerik:RadComboBox ID="ddlVoceGestione" runat="server" Width="300px" ResolvedRenderMode="Classic"
                            HighlightTemplatedItems="true" EnableLoadOnDemand="true">
                        </telerik:RadComboBox>
                    </ItemTemplate>
                    <HeaderStyle HorizontalAlign="Center" />
                    <ItemStyle HorizontalAlign="Center" />
                </telerik:GridTemplateColumn>
                <telerik:GridBoundColumn DataField="COD_ARPA" HeaderText="COD_ARPA" Visible="false">
                </telerik:GridBoundColumn>
            </Columns>
            <SortExpressions>
                <telerik:GridSortExpression FieldName="DESCRIZIONE" SortOrder="Ascending" />
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
    <asp:HiddenField ID="HFHeightGriglia" runat="server" Value="50" ClientIDMode="Static" />
</asp:Content>
