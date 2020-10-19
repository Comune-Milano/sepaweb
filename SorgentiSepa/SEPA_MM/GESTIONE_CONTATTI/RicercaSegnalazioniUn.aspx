<%@ Page Title="" Language="VB" MasterPageFile="HomePage.master" AutoEventWireup="false"
    CodeFile="RicercaSegnalazioniUn.aspx.vb" Inherits="GESTIONE_CONTATTI_RicercaSegnalazioniUn" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="js/HGridScript.js"></script>
    <script type="text/javascript">
        var selezionato;
        var noCaricamento = 0;
        function Apri() {
            document.getElementById('CPFooter_btnVisualizza').click();
        };
    </script>
    <link rel="stylesheet" href="../AUTOCOMPLETE/cmbstyle/chosen.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPTitolo" runat="Server">
    <asp:Label Text="Ricerca Segnalazioni per aggregazione" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPContenuto" runat="Server">
    <asp:MultiView ID="MultiViewRicerca" runat="server" ActiveViewIndex="0">
        <asp:View ID="ViewParametriRicerca" runat="server">
            <table border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td style="width: 50%; vertical-align: top">
                        <table border="0" cellpadding="2" cellspacing="2" width="100%">
                            <tr>
                                <td style="width: 20%">
                                    <asp:Label ID="Label0" Text="Sede Territoriale" runat="server" Font-Names="Arial"
                                        Font-Size="8pt" />
                                </td>
                                <td style="width: 80%">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <div style="overflow: auto; width: 247px; border: 1px gray solid; background-color: White">
                                                    <asp:CheckBoxList ID="CheckBoxListSedi" runat="server" AutoPostBack="True">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label11" Text="Complesso" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbComplesso" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        CssClass="chzn-select" Width="450px" AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label22" Text="Edificio" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbEdificio" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        CssClass="chzn-select" Width="450px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label9" Text="Segnalante" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                </td>
                                <td>
                                    <asp:TextBox ID="txtSegnalante" runat="server" MaxLength="100" Font-Names="Arial"
                                        Font-Size="8pt" Width="350px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label7" Text="Fornitore" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbFornitori" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Width="450px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label19" Text="Numero" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                </td>
                                <td>
                                    <asp:TextBox ID="TextBoxNumero" runat="server" Font-Names="Arial" Font-Size="8pt"
                                        Width="100px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label20" Text="Dal" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                </td>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtDal" runat="server" ToolTip="gg/mm/aaaa" Width="68px" MaxLength="10"
                                                    Font-Names="Arial" Font-Size="8pt"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDal"
                                                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Size="8pt" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                                <asp:TextBox ID="TextBoxOreDal" runat="server" ToolTip="ore" Width="25px" Font-Names="Arial"
                                                    Font-Size="8pt"></asp:TextBox>
                                                <asp:Label ID="Label6" runat="server" Width="10px" TabIndex="-1" Font-Names="Arial"
                                                    Style="text-align: center" Font-Size="8pt">:</asp:Label>
                                                <asp:TextBox ID="TextBoxMinutiDal" runat="server" ToolTip="minuti" Width="25px" Font-Names="Arial"
                                                    Font-Size="8pt"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label21" Text="Al" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                </td>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtAl" runat="server" ToolTip="gg/mm/aaaa" Width="68px" Font-Names="Arial"
                                                    Font-Size="8pt"></asp:TextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtAl"
                                                    Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                                <asp:TextBox ID="TextBoxOreAl" runat="server" ToolTip="ore" Width="25px" Font-Names="Arial"
                                                    Font-Size="8pt"></asp:TextBox>
                                                <asp:Label ID="Label8" runat="server" Width="10px" TabIndex="-1" Font-Names="Arial"
                                                    Style="text-align: center" Font-Size="8pt">:</asp:Label>
                                                <asp:TextBox ID="TextBoxMinutiAl" runat="server" ToolTip="minuti" Width="25px" Font-Names="Arial"
                                                    Font-Size="8pt"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    Solo segnalazioni con ticket figli
                                </td>
                                <td>
                                    <asp:CheckBox ID="CheckBoxSegnalazioniFigli" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" Text="Ordina risultati per" runat="server" Font-Bold="True"
                                        Font-Names="Arial" Font-Size="8pt" Style="text-align: Left" Width="100%" Visible="False" />
                                </td>
                                <td>
                                    <%--<div style="overflow: auto; width: 247px; border: 1px gray solid; background-color: White">
                            <asp:RadioButtonList ID="RadioButtonListOrdine" runat="server" RepeatDirection="Vertical">
                                <asp:ListItem Value="0" Text="Stato Segnalazione" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="1" Text="Urgenza"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Tipo Segnalazione"></asp:ListItem>
                            </asp:RadioButtonList>
                        </div>--%>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 50%; vertical-align: top">
                        <table border="0" cellpadding="2" cellspacing="2" width="100%">
                            <tr>
                                <td>
                                    <asp:Label ID="Label16" Text="Stato Segnalazione" runat="server" Font-Names="Arial"
                                        Font-Size="8pt" />
                                </td>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <div style="overflow: auto; width: 247px; border: 1px gray solid; background-color: White">
                                                    <asp:CheckBoxList ID="CheckBoxListStato" runat="server">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label5" Text="Categoria segnalazione" runat="server" Font-Names="Arial"
                                        Font-Size="8pt" />
                                </td>
                                <td>
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td>
                                                <div style="overflow: auto; width: 247px; border: 1px gray solid; background-color: White">
                                                    <asp:CheckBoxList ID="CheckBoxListTipoSegnalazione" runat="server" AutoPostBack="True">
                                                    </asp:CheckBoxList>
                                                </div>
                                            </td>
                                            <td>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label17" Text="Categoria 1" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbTipoSegnalazioneLivello1" runat="server" AutoPostBack="True"
                                        Font-Names="Arial" Font-Size="8pt" Width="250px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" Text="Categoria 2" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbTipoSegnalazioneLivello2" runat="server" AutoPostBack="True"
                                        Font-Names="Arial" Font-Size="8pt" Width="250px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" Text="Categoria 3" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbTipoSegnalazioneLivello3" runat="server" AutoPostBack="True"
                                        Font-Names="Arial" Font-Size="8pt" Width="250px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label4" Text="Categoria 4" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="cmbTipoSegnalazioneLivello4" runat="server" Font-Names="Arial"
                                        Font-Size="8pt" Width="250px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label18" Text="Urgenza" runat="server" Font-Names="Arial" Font-Size="8pt" />
                                </td>
                                <td>
                                    <asp:DropDownList ID="DropDownListUrgenza" runat="server" BackColor="White" Font-Names="Arial"
                                        Font-Size="8pt" TabIndex="7" Width="90px" Enabled="False">
                                        <asp:ListItem style="background-color: White; color: Black;" Value="---">Tutti</asp:ListItem>
                                        <asp:ListItem style="background-color: Blue; color: Blue;" Value="Blu"></asp:ListItem>
                                        <asp:ListItem style="background-color: White; color: White;" Value="Bianco">&nbsp;</asp:ListItem>
                                        <asp:ListItem style="background-color: Green; color: Green;" Value="Verde"></asp:ListItem>
                                        <asp:ListItem style="background-color: Yellow; color: Yellow;" Value="Giallo"></asp:ListItem>
                                        <asp:ListItem style="background-color: Red; color: Red;" Value="Rosso"></asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <script src="../AUTOCOMPLETE/cmbscript/chosen.jquery.js" type="text/javascript"></script>
            <script type="text/javascript">
                $(".chzn-select").chosen({
                    disable_search_threshold: 10,
                    no_results_text: "Nessun risultato trovato!",
                    placeholder_text_single: "- - -",
                    width: "95%"
                });
            </script>
        </asp:View>
        <asp:View ID="ViewRisultatiRicerca" runat="server">
            <asp:Label Text="" runat="server" ID="lblRisultati" />
            <asp:TextBox runat="server" ID="TextBox1" Text="" BackColor="Transparent" BorderColor="Transparent"
                BorderWidth="0px" Font-Bold="True" Font-Names="arial" Font-Size="9pt" ForeColor="Black"
                Width="95%" ReadOnly="true" />
            <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
                <AjaxSettings>
                    <telerik:AjaxSetting AjaxControlID="RadGridSegnalazioni">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadGridSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1">
                            </telerik:AjaxUpdatedControl>
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                    <telerik:AjaxSetting AjaxControlID="RadButtonSelezionaTutti">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadGridSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1">
                            </telerik:AjaxUpdatedControl>
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                    <telerik:AjaxSetting AjaxControlID="RadButtonDeselezionaTutti">
                        <UpdatedControls>
                            <telerik:AjaxUpdatedControl ControlID="RadGridSegnalazioni" LoadingPanelID="RadAjaxLoadingPanel1">
                            </telerik:AjaxUpdatedControl>
                        </UpdatedControls>
                    </telerik:AjaxSetting>
                </AjaxSettings>
            </telerik:RadAjaxManager>
            <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
            </telerik:RadAjaxLoadingPanel>
            <div id="divOverContent" style="width: 100%; overflow: auto;">
                <telerik:RadGrid ID="RadGridSegnalazioni" runat="server" AllowPaging="True" AllowSorting="True"
                    GroupPanelPosition="Top" ResolvedRenderMode="Classic" AutoGenerateColumns="False"
                    PageSize="100" Culture="it-IT" RegisterWithScriptManager="False" Font-Size="8pt"
                    Font-Names="Arial" MasterTableView-HeaderStyle-Wrap="true" Width="97%" Height="500px"
                    ShowHeadersWhenNoRecords="False">
                    <MasterTableView EnableHierarchyExpandAll="true" NoMasterRecordsText="Nessuna segnalazione da visualizzare."
                        HierarchyLoadMode="Client" ShowHeadersWhenNoRecords="false">
                        <DetailTables>
                            <telerik:GridTableView Name="Dettagli" Width="100%" AllowPaging="false" BackColor="Azure"
                                HierarchyDefaultExpanded="true">
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false" EmptyDataText="">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NUM" HeaderText="N°" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TIPO" HeaderText="" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TIPO_INT" HeaderText="CRIT." ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Width="3%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TIPO0" HeaderText="CATEGORIA" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="7%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TIPO1" HeaderText="CATEGORIA 1" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TIPO2" HeaderText="CATEGORIA 2" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TIPO3" HeaderText="CATEGORIA 3" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TIPO4" HeaderText="CATEGORIA 4" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="6%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="RICHIEDENTE" HeaderText="RICHIEDENTE" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="CODICE_RU" HeaderText="CODICE CONTRATTO" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="8%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INS." ItemStyle-HorizontalAlign="Center">
                                        <HeaderStyle Width="4%" />
                                        <ItemStyle HorizontalAlign="Center" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ItemStyle-HorizontalAlign="Left"
                                        ItemStyle-Wrap="true">
                                        <HeaderStyle Width="17%" />
                                        <ItemStyle HorizontalAlign="Left" Wrap="True" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FILIALE" HeaderText="SEDE TERRITORIALE" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NOTE_C" HeaderText="NOTA DI CHIUSURA" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FIGLI2" HeaderText="" Visible="false">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FIGLI" HeaderText="TICKET FIGLI" ItemStyle-HorizontalAlign="Left">
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ID_PERICOLO_SEGNALAZIONE" HeaderText="ID_PERICOLO_SEGNALAZIONE"
                                        Visible="false" EmptyDataText="">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ID_SEGNALAZIONE_PADRE" HeaderText="N° SEGN. PADRE"
                                        EmptyDataText="">
                                        <HeaderStyle Width="5%" />
                                        <ItemStyle HorizontalAlign="Left" />
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <PagerStyle AlwaysVisible="True" />
                            </telerik:GridTableView>
                        </DetailTables>
                        <RowIndicatorColumn Visible="False">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn Created="True">
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridTemplateColumn HeaderText="">
                                <HeaderStyle Width="2%" />
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# DataBinder.Eval(Container,"DataItem.CHECK1") %>'
                                        AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged1" />
                                </ItemTemplate>
                            </telerik:GridTemplateColumn>
                            <telerik:GridBoundColumn DataField="NUM" HeaderText="N°" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="3%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO" HeaderText="" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO_INT" HeaderText="CRIT." ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="3%" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO0" HeaderText="CATEGORIA" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="7%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO1" HeaderText="CATEGORIA 1" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO2" HeaderText="CATEGORIA 2" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO3" HeaderText="CATEGORIA 3" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="TIPO4" HeaderText="CATEGORIA 4" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="STATO" HeaderText="STATO" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="6%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="RICHIEDENTE" HeaderText="RICHIEDENTE" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="CODICE_RU" HeaderText="CODICE CONTRATTO" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="8%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INS." ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="4%" />
                                <ItemStyle HorizontalAlign="Center" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ItemStyle-HorizontalAlign="Left"
                                ItemStyle-Wrap="true">
                                <HeaderStyle Width="17%" />
                                <ItemStyle HorizontalAlign="Left" Wrap="True" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FILIALE" HeaderText="SEDE TERRITORIALE" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="NOTE_C" HeaderText="NOTA DI CHIUSURA" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FIGLI2" HeaderText="" Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="FIGLI" HeaderText="TICKET FIGLI" ItemStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_PERICOLO_SEGNALAZIONE" HeaderText="ID_PERICOLO_SEGNALAZIONE"
                                Visible="false">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="ID_SEGNALAZIONE_PADRE" HeaderText="N° SEGN. PADRE"
                                EmptyDataText="">
                                <HeaderStyle Width="5%" />
                                <ItemStyle HorizontalAlign="Left" />
                            </telerik:GridBoundColumn>
                        </Columns>
                        <PagerStyle AlwaysVisible="True"></PagerStyle>
                        <HeaderStyle Wrap="True" />
                    </MasterTableView>
                    <ClientSettings EnableRowHoverStyle="true" AllowDragToGroup="false" AllowAutoScrollOnDragDrop="false"
                        AllowRowsDragDrop="false">
                        <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                        <Selecting AllowRowSelect="True" />
                        <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                            AllowResizeToFit="true" />
                    </ClientSettings>
                    <PagerStyle AlwaysVisible="True" />
                </telerik:RadGrid>
            </div>
            <%--<asp:DataGrid runat="server" ID="DataGridSegnalaz" AutoGenerateColumns="False" CellPadding="1" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333" GridLines="None" CellSpacing="1" AllowPaging="True"
                PageSize="50" ShowFooter="True" Width="1970px" ClientIDMode="Static">
                <ItemStyle BackColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages" Position="TopAndBottom" />
                <AlternatingItemStyle BackColor="White" />
                <Columns>
                    <asp:HyperLinkColumn Text="+">
                        <ItemStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="10pt" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                        <HeaderStyle Width="10px" />
                    </asp:HyperLinkColumn>
                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="SELEZIONE" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# DataBinder.Eval(Container,"DataItem.CHECK1") %>' AutoPostBack="True" OnCheckedChanged="CheckBox1_CheckedChanged1" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle Width="50px" />
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="NUM" HeaderText="N°" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle Width="50px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPO" HeaderText="CATEGORIA SEGNALAZIONE" ItemStyle-HorizontalAlign="Left" Visible="false">
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPO_INT" HeaderText="CRITICITA'" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle Width="50px" />
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPO0" HeaderText="CATEGORIA" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPO1" HeaderText="CATEGORIA 1" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPO2" HeaderText="CATEGORIA 2" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPO3" HeaderText="CATEGORIA 3" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPO4" HeaderText="CATEGORIA 4" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="STATO" HeaderText="STATO" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="RICHIEDENTE" HeaderText="RICHIEDENTE" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="CODICE_RU" HeaderText="CODICE CONTRATTO" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle Width="150px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DATA_INSERIMENTO" HeaderText="DATA INS." ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle Width="460px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="FILIALE" HeaderText="SEDE TERRITORIALE" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="NOTE_C" HeaderText="NOTE DI CHIUSURA" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="FIGLI2" HeaderText="" ItemStyle-HorizontalAlign="Left" Visible="false">
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="FIGLI" HeaderText="TICKET FIGLI" ItemStyle-HorizontalAlign="Left">
                        <HeaderStyle Width="100px" />
                        <ItemStyle HorizontalAlign="Right" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="ID_PERICOLO_sEGNALAZIONE" HeaderText="ID_PERICOLO_sEGNALAZIONE" Visible="false"></asp:BoundColumn>
                </Columns>
                <EditItemStyle BackColor="White" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            </asp:DataGrid>--%>
            <asp:HiddenField runat="server" ID="idSegnalazione" ClientIDMode="Static" />
            <asp:HiddenField ID="HiddenFieldRigaSelezionata" runat="server" Value="0" ClientIDMode="Static" />
        </asp:View>
    </asp:MultiView>
    <asp:HiddenField ID="HiddenFieldVista" runat="server" Value="0" ClientIDMode="Static" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="CPFooter" runat="Server">
    <table border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td>
                <asp:MultiView ID="MultiViewBottoni" runat="server" ActiveViewIndex="0">
                    <asp:View ID="ViewBottoniRicerca" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:Button ID="btnCerca" runat="server" Text="Avvia Ricerca" CssClass="bottone"
                                        ToolTip="Avvia Ricerca" OnClientClick="document.getElementById('HiddenFieldVista').value='1'" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                    <asp:View ID="ViewBottoniRisultati" runat="server">
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <asp:Button ID="ButtonSelezionaTutti" runat="server" Text="Seleziona tutti" CssClass="bottone"
                                        ToolTip="Seleziona tutti" OnClientClick="document.getElementById('HiddenFieldVista').value='1';document.getElementById('MasterPage_CPFooter_RadButtonSelezionaTutti_input').click();return false;" />
                                    <telerik:RadButton ID="RadButtonSelezionaTutti" runat="server" Style="display: none">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <asp:Button ID="ButtonDeselezionaTutti" runat="server" Text="Deseleziona tutti" CssClass="bottone"
                                        ToolTip="Deseleziona tutti" OnClientClick="document.getElementById('HiddenFieldVista').value='1';document.getElementById('MasterPage_CPFooter_RadButtonDeselezionaTutti_input').click();return false;" />
                                    <telerik:RadButton ID="RadButtonDeselezionaTutti" runat="server" Style="display: none">
                                    </telerik:RadButton>
                                </td>
                                <td>
                                    <asp:Button ID="btnNuovaRicerca" runat="server" Text="Nuova Ricerca" CssClass="bottone"
                                        ToolTip="Effettua una nuova ricerca" OnClientClick="document.getElementById('HiddenFieldVista').value='0'" />
                                </td>
                                <td>
                                    <asp:Button ID="ButtonUnisci" runat="server" Text="Unisci segnalazioni" CssClass="bottone"
                                        ToolTip="Unisci segnalazioni" OnClientClick="document.getElementById('HiddenFieldVista').value='1'" />
                                </td>
                            </tr>
                        </table>
                    </asp:View>
                </asp:MultiView>
            </td>
            <td>
                <asp:Button ID="btnEsci" runat="server" Text="Esci" CssClass="bottone" ToolTip="Esci" />
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="AltezzaRadGrid" Value="0" />
    <asp:HiddenField runat="server" ClientIDMode="Static" ID="LarghezzaRadGrid" Value="0" />
    <asp:Button ID="ButtonUnisciSegnalazioni" runat="server" Text="Unisci" CssClass="bottone"
        ToolTip="Unisci segnalazioni" Style="visibility: hidden" ClientIDMode="Static" />
    <script type="text/javascript">
        validNavigation = false;
        $(document).ready(function () {
            var altezzaRad = $(window).height() - 280;
            var larghezzaRad = $(window).width() - 50;
            $("#MasterPage_CPContenuto_RadGridSegnalazioni").width(larghezzaRad);
            $("#MasterPage_CPContenuto_RadGridSegnalazioni").height(altezzaRad);
            document.getElementById('LarghezzaRadGrid').value = larghezzaRad;
            document.getElementById('AltezzaRadGrid').value = altezzaRad;

        });
        $(window).resize(function () {
            var altezzaRad = $(window).height() - 280;
            var larghezzaRad = $(window).width() - 50;
            $("#MasterPage_CPContenuto_RadGridSegnalazioni").width(larghezzaRad);
            $("#MasterPage_CPContenuto_RadGridSegnalazioni").height(altezzaRad);
            document.getElementById('LarghezzaRadGrid').value = larghezzaRad;
            document.getElementById('AltezzaRadGrid').value = altezzaRad;
        });
        $(function () {
            $("#CPContenuto_txtDal").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
            $("#CPContenuto_txtAl").datepicker({ autoSize: true, dateFormat: 'dd/mm/yy', showAnim: 'slide', beforeShow: function () { $(".ui-datepicker").css('font-size', 10); } });
        });
    </script>
</asp:Content>
