<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ModificaRLI.aspx.vb" Inherits="Contratti_ModificaRLI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Modifica file RLI</title>
    <link href="../Standard/Style/Site.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
        function requestStart(sender, args) {
            if (args.get_eventTarget().indexOf("btnScaricaXML") >= 0) {
                args.set_enableAjax(false);
            }
        };
       
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            <asp:ScriptReference Path="../Standard/Scripts/notify.js" />
            <asp:ScriptReference Path="../Standard/Scripts/jsFunzioni.js" />
            <asp:ScriptReference Path="../Standard/Scripts/jsMessage.js" />
        </Scripts>
    </telerik:RadScriptManager>
    <telerik:RadFormDecorator RenderMode="Classic" Skin="Web20" ID="FormDecorator1" runat="server"
        DecoratedControls="Buttons" />
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <ClientEvents OnRequestStart="requestStart" />
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="RadGridRLI">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="RadGridRLI" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <table border="0" cellpadding="2" cellspacing="2" width="100%">
        <tr>
            <td colspan="2" style="text-align: center">
                <asp:Label ID="lblTitolo" runat="server" Text="Modifica file RLI" CssClass="testoGrassettoMaiuscoloBlu"
                    Font-Size="20px"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td width="90%" style="padding-left: 10px;">
                <asp:Panel runat="server" ID="PanelRadGrid">
                    <telerik:RadGrid ID="RadGridRLI" runat="server" AutoGenerateColumns="False" Culture="it-IT"
                        AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="100%" AllowSorting="True"
                        AllowPaging="true" PageSize="20">
                        <MasterTableView AllowFilteringByColumn="True" AllowSorting="True" CommandItemDisplay="Top"
                            DataKeyNames="ID" GridLines="None" CommandItemSettings-ShowAddNewRecordButton="False"
                            HierarchyLoadMode="ServerOnDemand" HierarchyDefaultExpanded="false" EnableHierarchyExpandAll="true">
                            <DetailTables>
                                <telerik:GridTableView Name="Dettagli" Width="100%" AllowPaging="false" BackColor="Azure"
                                    AllowFilteringByColumn="false" HierarchyDefaultExpanded="false" EnableHierarchyExpandAll="true">
                                    <Columns>
                                        <telerik:GridBoundColumn DataField="NUM_PROGRESSIVO" HeaderText="NUM. PROGRESSIVO">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TIPOIMMOB" HeaderText="TIPO IMMOBILE">
                                        </telerik:GridBoundColumn>
                                        <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn">
                                        </telerik:GridEditCommandColumn>
                                    </Columns>
                                    <EditFormSettings EditFormType="Template" InsertCaption="Modifica RLI" PopUpSettings-CloseButtonToolTip="Chiudi"
                                        PopUpSettings-Height="300px" PopUpSettings-Modal="True" PopUpSettings-ShowCaptionInEditForm="True"
                                        PopUpSettings-Width="500px">
                                        <EditColumn FilterControlAltText="Filter EditCommandColumn1 column" UniqueName="EditCommandColumn1">
                                        </EditColumn>
                                        <FormTemplate>
                                            <table id="table1" border="0" cellpadding="1" cellspacing="2" rules="none" style="border-collapse: collapse;"
                                                width="100%">
                                                <tr valign="top">
                                                    <td>
                                                        Codice comune catastale:
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="codice_comune_Catastale" runat="server" Text='<%# Bind("codice_comune_Catastale") %>'
                                                            Font-Names="arial" Font-Size="8" CssClass="CssMaiuscolo">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td>
                                                        Tipo catasto:
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="tipo_catasto" runat="server" Text='<%# Bind("tipo_catasto") %>'
                                                            Font-Names="arial" Font-Size="8" CssClass="CssMaiuscolo">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td>
                                                        Porzione immobile:
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="porzione_immobile" runat="server" Text='<%# Bind("porzione_immobile") %>'
                                                            Font-Names="arial" Font-Size="8" CssClass="CssMaiuscolo">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr valign="top">
                                                    <td>
                                                        Foglio:
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="foglio" runat="server" Text='<%# Bind("foglio") %>' Font-Names="arial"
                                                            Font-Size="8" CssClass="CssMaiuscolo">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td>
                                                        Particella:
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="particella" runat="server" Text='<%# Bind("particella") %>'
                                                            Font-Names="arial" Font-Size="8" CssClass="CssMaiuscolo">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td>
                                                        Particella denominatore:
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="particella_denominatore" runat="server" Text='<%# Bind("particella_denominatore") %>'
                                                            Font-Names="arial" Font-Size="8" CssClass="CssMaiuscolo">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr valign="top">
                                                    <td>
                                                        Subalterno:
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="subalterno" runat="server" Text='<%# Bind("subalterno") %>'
                                                            Font-Names="arial" Font-Size="8" CssClass="CssMaiuscolo">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td>
                                                        Comune:
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="comune" runat="server" Text='<%# Bind("comune") %>' Font-Names="arial"
                                                            Font-Size="8" CssClass="CssMaiuscolo">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td>
                                                        Provincia:
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="provincia" runat="server" Text='<%# Bind("provincia") %>'
                                                            Font-Names="arial" Font-Size="8" CssClass="CssMaiuscolo">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr valign="top">
                                                    <td>
                                                        Categ. catastale:
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="categoria_Catastale" runat="server" Text='<%# Bind("categoria_Catastale") %>'
                                                            Font-Names="arial" Font-Size="8" CssClass="CssMaiuscolo">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td>
                                                        Rendita catastale:
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="rendita_Catastale" runat="server" Text='<%# Bind("rendita_Catastale") %>'
                                                            Font-Names="arial" Font-Size="8" CssClass="CssMaiuscolo">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td>
                                                        Tipologia indirizzo:
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="tipologia_indirizzo" runat="server" Text='<%# Bind("tipologia_indirizzo") %>'
                                                            Font-Names="arial" Font-Size="8" CssClass="CssMaiuscolo">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                </tr>
                                                <tr valign="top">
                                                    <td>
                                                        Indirizzo:
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="indirizzo" runat="server" Text='<%# Bind("indirizzo") %>'
                                                            Font-Names="arial" Font-Size="8" CssClass="CssMaiuscolo">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td>
                                                        Num. civico:
                                                    </td>
                                                    <td>
                                                        <telerik:RadTextBox ID="num_civico" runat="server" Text='<%# Bind("num_civico") %>'
                                                            Font-Names="arial" Font-Size="8" CssClass="CssMaiuscolo">
                                                        </telerik:RadTextBox>
                                                    </td>
                                                    <td>
                                                        &nbsp
                                                    </td>
                                                    <td>
                                                        <asp:HiddenField runat="server" ID="idImmob" Value='<%# Bind("idi") %>' />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="6" align="right">
                                                        <asp:Button ID="btnUpdateOp2" runat="server" CommandName='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "PerformInsert", "Update")%>'
                                                            Text='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "Aggiungi", "Aggiorna") %>' />
                                                        &nbsp<asp:Button ID="btnCancelOp2" runat="server" CausesValidation="False" CommandName="Cancel"
                                                            Text="Chiudi" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </FormTemplate>
                                        <PopUpSettings CloseButtonToolTip="Chiudi" Height="300px" Modal="True" Width="500px" />
                                    </EditFormSettings>
                                </telerik:GridTableView>
                            </DetailTables>
                            <CommandItemSettings ShowAddNewRecordButton="False" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="TIPOLOGIA_ADEMPIMENTO" HeaderText="TIPOLOGIA_ADEMPIMENTO"
                                    Visible="false">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataField="COD_CONTRATTO" HeaderText="COD. CONTRATTO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataField="DATA_CREAZIONE" HeaderText="DATA CREAZIONE FILE">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataField="TIPO_ADEMPIMENTO" HeaderText="TIPO FILE">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn AutoPostBackOnFilter="true" CurrentFilterFunction="Contains"
                                    DataField="NOME_FILE_XML" HeaderText="NOME FILE">
                                </telerik:GridBoundColumn>
                                <telerik:GridEditCommandColumn ButtonType="ImageButton" UniqueName="EditCommandColumn"
                                    ItemStyle-Width="20">
                                </telerik:GridEditCommandColumn>
                                <telerik:GridTemplateColumn AllowFiltering="False" ItemStyle-Width="20">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btnScaricaXML" runat="server" ImageUrl="Immagini/xml.png" OnClick="btnScaricaXML_Click"
                                            Width="30" AlternateText="Scarica XML" />
                                    </ItemTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <EditFormSettings EditFormType="Template" InsertCaption="Modifica RLI" PopUpSettings-CloseButtonToolTip="Chiudi"
                                PopUpSettings-Height="300px" PopUpSettings-Modal="True" PopUpSettings-ShowCaptionInEditForm="True"
                                PopUpSettings-Width="500px">
                                <EditColumn FilterControlAltText="Filter EditCommandColumn1 column" UniqueName="EditCommandColumn1">
                                </EditColumn>
                                <FormTemplate>
                                    <table id="table1" border="0" cellpadding="1" cellspacing="2" rules="none" style="border-collapse: collapse;"
                                        width="100%">
                                        <tr valign="top">
                                            <td>
                                                Cod. fiscale fornitore:
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="COD_FISCALE_FORNITORE" runat="server" CssClass="CssMaiuscolo"
                                                    Font-Names="arial" Font-Size="8" Text='<%# Bind("COD_FISCALE_FORNITORE") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                Ufficio Territoriale:
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="UFFICIO_TERRITORIALE" runat="server" CssClass="CssMaiuscolo"
                                                    Font-Names="arial" Font-Size="8" Text='<%# Bind("UFFICIO_TERRITORIALE") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                Codice ABI:
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="CODICE_ABI" runat="server" CssClass="CssMaiuscolo" Font-Names="arial"
                                                    Font-Size="8" Text='<%# Bind("CODICE_ABI") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                Codice CAB:
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="CODICE_CAB" runat="server" CssClass="CssMaiuscolo" Font-Names="arial"
                                                    Font-Size="8" Text='<%# Bind("CODICE_CAB") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Num. Conto Corrente:
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="NUMERO_CONTO_CORRENTE" runat="server" CssClass="CssMaiuscolo"
                                                    Font-Names="arial" Font-Size="8" Text='<%# Bind("NUMERO_CONTO_CORRENTE") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                CIN:
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="CIN" runat="server" CssClass="CssMaiuscolo" Font-Names="arial"
                                                    Font-Size="8" Text='<%# Bind("CIN") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                Cod. fiscale titolare CC:
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="COD_FISCALE_TITOLARE_CC" runat="server" CssClass="CssMaiuscolo"
                                                    Font-Names="arial" Font-Size="8" Text='<%# Bind("COD_FISCALE_TITOLARE_CC") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                Importo da versare:
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="IMPORTO_DA_VERSARE" runat="server" CssClass="CssMaiuscolo"
                                                    Font-Names="arial" Font-Size="8" Text='<%# Bind("IMPORTO_DA_VERSARE") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Cod. fiscale richiedente:
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="COD_FISCALE_RICHIEDENTE" runat="server" CssClass="CssMaiuscolo"
                                                    Font-Names="arial" Font-Size="8" Text='<%# Bind("COD_FISCALE_RICHIEDENTE") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                Denominazione richiedente:
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="DENOMINAZIONE_RICHIEDENTE" runat="server" CssClass="CssMaiuscolo"
                                                    Font-Names="arial" Font-Size="8" Text='<%# Bind("DENOMINAZIONE_RICHIEDENTE") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                Cod. fiscale rappresentante:
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="COD_FISCALE_RAPPRESENTANTE" runat="server" CssClass="CssMaiuscolo"
                                                    Font-Names="arial" Font-Size="8" Text='<%# Bind("COD_FISCALE_RAPPRESENTANTE") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                Cognome rappresentante:
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="COGNOME_RAPPRESENTANTE" runat="server" CssClass="CssMaiuscolo"
                                                    Font-Names="arial" Font-Size="8" Text='<%# Bind("COGNOME_RAPPRESENTANTE") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Nome rappresentante:
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="NOME_RAPPRESENTANTE" runat="server" CssClass="CssMaiuscolo"
                                                    Font-Names="arial" Font-Size="8" Text='<%# Bind("NOME_RAPPRESENTANTE") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                Imposta di registro:
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="IMPOSTA_REGISTRO" runat="server" CssClass="CssMaiuscolo"
                                                    Font-Names="arial" Font-Size="8" Text='<%# Bind("IMPOSTA_REGISTRO") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                Imposta di bollo:
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="IMPOSTA_BOLLO" runat="server" CssClass="CssMaiuscolo" Font-Names="arial"
                                                    Font-Size="8" Text='<%# Bind("IMPOSTA_BOLLO") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                Sanzione imposta di registro:
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="SANZIONE_IMP_REGISTRO" runat="server" CssClass="CssMaiuscolo"
                                                    Font-Names="arial" Font-Size="8" Text='<%# Bind("SANZIONE_IMP_REGISTRO") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Sanzione imposta di bollo:
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="SANZIONE_IMP_BOLLO" runat="server" CssClass="CssMaiuscolo"
                                                    Font-Names="arial" Font-Size="8" Text='<%# Bind("SANZIONE_IMP_BOLLO") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="LBL_DATA_INIZIO_CONTRATTO" runat="server" Text="Data inizio contratto"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="DATA_INIZIO_CONTRATTO" runat="server" CssClass="CssMaiuscolo"
                                                    Font-Names="arial" Font-Size="8" Text='<%# Bind("DATA_INIZIO_CONTRATTO1") %>'>
                                                </telerik:RadTextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="DATA_INIZIO_CONTRATTO"
                                                    Display="Static" ErrorMessage="?" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                            </td>
                                            <td>
                                                <asp:Label ID="LBL_DATA_FINE_CONTRATTO" runat="server" Text="Data fine contratto"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="DATA_FINE_CONTRATTO" runat="server" CssClass="CssMaiuscolo"
                                                    Font-Names="arial" Font-Size="8" Text='<%# Bind("DATA_FINE_CONTRATTO1") %>'>
                                                </telerik:RadTextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="DATA_FINE_CONTRATTO"
                                                    Display="Static" ErrorMessage="?" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                            </td>
                                            <td>
                                                <asp:Label ID="LBL_IMPORTO_CANONE" runat="server" Text="Importo canone"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="IMPORTO_CANONE" runat="server" CssClass="CssMaiuscolo" Font-Names="arial"
                                                    Font-Size="8" Text='<%# Bind("IMPORTO_CANONE") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LBL_DATA_STIPULA" runat="server" Text="Data stipula"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="DATA_STIPULA" runat="server" CssClass="CssMaiuscolo" Font-Names="arial"
                                                    Font-Size="8" Text='<%# Bind("DATA_STIPULA1") %>'>
                                                </telerik:RadTextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="DATA_STIPULA"
                                                    Display="Static" ErrorMessage="?" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                            </td>
                                            <td>
                                                <asp:Label ID="LBL_NUMERO_PAGINE" runat="server" Text="Numero pagine"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="NUMERO_PAGINE" runat="server" CssClass="CssMaiuscolo" Font-Names="arial"
                                                    Font-Size="8" Text='<%# Bind("NUMERO_PAGINE") %>'>
                                                </telerik:RadTextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                                                    ControlToValidate="NUMERO_PAGINE" Display="Static" ErrorMessage="?" Font-Bold="True"
                                                    Font-Names="arial" Font-Size="8pt" ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                            </td>
                                            <td>
                                                <asp:Label ID="LBL_NUMERO_COPIE" runat="server" Text="Numero copie"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="NUMERO_COPIE" runat="server" CssClass="CssMaiuscolo" Font-Names="arial"
                                                    Font-Size="8" Text='<%# Bind("NUMERO_COPIE") %>'>
                                                </telerik:RadTextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="NUMERO_COPIE"
                                                    Display="Static" ErrorMessage="?" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                                    ValidationExpression="\d+"></asp:RegularExpressionValidator>
                                            </td>
                                            <td>
                                                Cod. fiscale intermediario:
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="CF_INTERMEDIARIO" runat="server" CssClass="CssMaiuscolo"
                                                    Font-Names="arial" Font-Size="8" Text='<%# Bind("CF_INTERMEDIARIO") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblAnnualita" runat="server" Text="Annualità"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="ANNUALITA" runat="server" CssClass="CssMaiuscolo" Font-Names="arial"
                                                    Font-Size="8" Text='<%# Bind("ANNUALITA") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblDataFine" runat="server" Text="Data fine"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="DATA_FINE" runat="server" CssClass="CssMaiuscolo" Font-Names="arial"
                                                    Font-Size="8" Text='<%# Bind("DATA_FINE1") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblUffRegistr" runat="server" Text="Uff.registrazione"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="UFFICIO_REGISTRAZIONE" runat="server" CssClass="CssMaiuscolo"
                                                    Font-Names="arial" Font-Size="8" Text='<%# Bind("UFFICIO_REGISTRAZIONE") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblAnnoRegistr" runat="server" Text="Anno registrazione"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="ANNO_REGISTRAZIONE" runat="server" CssClass="CssMaiuscolo"
                                                    Font-Names="arial" Font-Size="8" Text='<%# Bind("ANNO_REGISTRAZIONE") %>'>
                                                </telerik:RadTextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="ANNO_REGISTRAZIONE"
                                                    ErrorMessage="?" Font-Bold="True" Font-Names="ARIAL" Font-Size="9pt" ToolTip="Formato errato"
                                                    ValidationExpression="\d{4}"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblSerieRegistr" runat="server" Text="Serie registrazione"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="SERIE_REGISTRAZIONE" runat="server" CssClass="CssMaiuscolo"
                                                    Font-Names="arial" Font-Size="8" Text='<%# Bind("SERIE_REGISTRAZIONE") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNumRegistr" runat="server" Text="Num. registrazione"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="NUMERO_REGISTRAZIONE" runat="server" CssClass="CssMaiuscolo"
                                                    Font-Names="arial" Font-Size="8" Text='<%# Bind("NUMERO_REGISTRAZIONE") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="LBL_COD_FISCALE_LOCATORE" runat="server" Text="Cod. fiscale locatore:"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="COD_FISCALE_LOCATORE" runat="server" CssClass="CssMaiuscolo"
                                                    Font-Names="arial" Font-Size="8" Text='<%# Bind("COD_FISCALE_LOCATORE") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="LBL_DENOMINAZIONE_LOCATORE" runat="server" Text=" Denominazione locatore:"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="DENOMINAZIONE_LOCATORE" runat="server" CssClass="CssMaiuscolo"
                                                    Font-Names="arial" Font-Size="8" Text='<%# Bind("DENOMINAZIONE_LOCATORE") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LBL_COD_FISCALE_CONDUTTORE" runat="server" Text="Cod. fiscale conduttore"
                                                    Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="COD_FISCALE_CONDUTTORE" runat="server" CssClass="CssMaiuscolo"
                                                    Visible="False" Font-Names="arial" Font-Size="8" Text='<%# Bind("COD_FISCALE_CONDUTTORE") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="LBL_COGNOME_CONDUTTORE" runat="server" Text="Cognome conduttore" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="COGNOME_CONDUTTORE" runat="server" CssClass="CssMaiuscolo"
                                                    Visible="False" Font-Names="arial" Font-Size="8" Text='<%# Bind("COGNOME_CONDUTTORE") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="LBL_NOME_CONDUTTORE" runat="server" Text="Nome conduttore" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="NOME_CONDUTTORE" runat="server" CssClass="CssMaiuscolo" Font-Names="arial"
                                                    Visible="False" Font-Size="8" Text='<%# Bind("NOME_CONDUTTORE") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="LBL_DENOMINAZIONE_CONDUTTORE" runat="server" Text="Denominazione conduttore"
                                                    Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="DENOMINAZIONE_CONDUTTORE" runat="server" CssClass="CssMaiuscolo"
                                                    Visible="False" Font-Names="arial" Font-Size="8" Text='<%# Bind("DENOMINAZIONE_CONDUTTORE") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LBL_SESSO_CONDUTTORE" runat="server" Text="Sesso conduttore" Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="SESSO_CONDUTTORE" runat="server" CssClass="CssMaiuscolo"
                                                    Visible="False" Font-Names="arial" Font-Size="8" Text='<%# Bind("SESSO_CONDUTTORE") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="LBL_DATA_NASCITA_CONDUTTORE" runat="server" Text="Data nascita conduttore"
                                                    Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="DATA_NASCITA_CONDUTTORE" runat="server" CssClass="CssMaiuscolo"
                                                    Visible="False" Font-Names="arial" Font-Size="8" Text='<%# Bind("DATA_NASCITA_CONDUTTORE1") %>'>
                                                </telerik:RadTextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="DATA_NASCITA_CONDUTTORE"
                                                    Display="Static" ErrorMessage="?" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                            </td>
                                            <td>
                                                <asp:Label ID="LBL_COMUNE_NASCITA_CONDUTTORE" runat="server" Text="Comune nascita conduttore"
                                                    Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="COMUNE_NASCITA_CONDUTTORE" runat="server" CssClass="CssMaiuscolo"
                                                    Visible="False" Font-Names="arial" Font-Size="8" Text='<%# Bind("COMUNE_NASCITA_CONDUTTORE") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="LBL_PROVINCIA_NASCITA_CONDUTTORE" runat="server" Text="Provincia nascita conduttore"
                                                    Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="PROVINCIA_NASCITA_CONDUTTORE" runat="server" CssClass="CssMaiuscolo"
                                                    Visible="False" Font-Names="arial" Font-Size="8" Text='<%# Bind("PROVINCIA_NASCITA_CONDUTTORE") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LBL_COD_FISCALE_CONDUTTORE2" runat="server" Text="Cod. fiscale nuovo conduttore"
                                                    Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="COD_FISCALE_CONDUTTORE2" runat="server" CssClass="CssMaiuscolo"
                                                    Visible="False" Font-Names="arial" Font-Size="8" Text='<%# Bind("COD_FISCALE_CONDUTTORE_2") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="LBL_COGNOME_CONDUTTORE2" runat="server" Text="Cognome nuovo conduttore"
                                                    Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="COGNOME_CONDUTTORE2" runat="server" CssClass="CssMaiuscolo"
                                                    Visible="False" Font-Names="arial" Font-Size="8" Text='<%# Bind("COGNOME_CONDUTTORE_2") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="LBL_NOME_CONDUTTORE2" runat="server" Text="Nome nuovo conduttore"
                                                    Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="NOME_CONDUTTORE2" runat="server" CssClass="CssMaiuscolo"
                                                    Visible="False" Font-Names="arial" Font-Size="8" Text='<%# Bind("NOME_CONDUTTORE_2") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="LBL_DENOMINAZIONE_CONDUTTORE2" runat="server" Text="Denominazione nuovo conduttore"
                                                    Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="DENOMINAZIONE_CONDUTTORE2" runat="server" CssClass="CssMaiuscolo"
                                                    Visible="False" Font-Names="arial" Font-Size="8" Text='<%# Bind("DENOMINAZIONE_CONDUTTORE_2") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:Label ID="LBL_SESSO_CONDUTTORE2" runat="server" Text="Sesso nuovo conduttore"
                                                    Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="SESSO_CONDUTTORE2" runat="server" CssClass="CssMaiuscolo"
                                                    Visible="False" Font-Names="arial" Font-Size="8" Text='<%# Bind("SESSO_CONDUTTORE_2") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="LBL_DATA_NASCITA_CONDUTTORE2" runat="server" Text="Data nascita nuovo conduttore"
                                                    Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="DATA_NASCITA_CONDUTTORE2" runat="server" CssClass="CssMaiuscolo"
                                                    Visible="False" Font-Names="arial" Font-Size="8" Text='<%# Bind("DATA_NASCITA_CONDUTTORE2") %>'>
                                                </telerik:RadTextBox>
                                                <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="DATA_NASCITA_CONDUTTORE2"
                                                    Display="Static" ErrorMessage="?" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                                    ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                                            </td>
                                            <td>
                                                <asp:Label ID="LBL_COMUNE_NASCITA_CONDUTTORE2" runat="server" Text="Comune nascita nuovo conduttore"
                                                    Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="COMUNE_NASCITA_CONDUTTORE2" runat="server" CssClass="CssMaiuscolo"
                                                    Visible="False" Font-Names="arial" Font-Size="8" Text='<%# Bind("COMUNE_NASCITA_CONDUTTORE_2") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                            <td>
                                                <asp:Label ID="LBL_PROVINCIA_NASCITA_CONDUTTORE2" runat="server" Text="Provincia nascita nuovo conduttore"
                                                    Visible="False"></asp:Label>
                                            </td>
                                            <td>
                                                <telerik:RadTextBox ID="PROVINCIA_NASCITA_CONDUTTORE2" runat="server" CssClass="CssMaiuscolo"
                                                    Visible="False" Font-Names="arial" Font-Size="8" Text='<%# Bind("PROVINCIA_NASCITA_CONDUTTORE_2") %>'>
                                                </telerik:RadTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="8">
                                                <asp:Button ID="btnUpdateOp1" runat="server" CommandName='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "PerformInsert", "Update")%>'
                                                    Text='<%# IIf((TypeOf(Container) is GridEditFormInsertItem), "Aggiungi", "Aggiorna") %>' />
                                                &nbsp;<asp:Button ID="btnCancelOp1" runat="server" CausesValidation="False" CommandName="Cancel"
                                                    Text="Chiudi" />
                                            </td>
                                        </tr>
                                    </table>
                                </FormTemplate>
                                <PopUpSettings CloseButtonToolTip="Chiudi" Height="300px" Modal="True" Width="500px" />
                            </EditFormSettings>
                        </MasterTableView>
                        <FilterMenu>
                        </FilterMenu>
                        <HeaderContextMenu>
                        </HeaderContextMenu>
                        <%--<ClientSettings>
                            <Selecting AllowRowSelect="True" />
                        </ClientSettings>--%>
                    </telerik:RadGrid>
                </asp:Panel>
                <asp:HiddenField ID="idFornitura" runat="server" Value="0" />
                <asp:HiddenField ID="tipoAdempimento" runat="server" Value="0" />
                <asp:HiddenField ID="nome_file_xml" runat="server" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
<script language="javascript" type="text/javascript">
    var r = {
        'special': /[\W]/g,
        'quotes': /['\''&'\"']/g,
        'notnumbers': /[^\d]/g,
        'onlynumbers': /[^\d\-\,\.]/g,
        'numbers': /[^\d]/g
    };

   function AutoDecimal(obj, numdec) {
        if (numdec == null) numdec = 2;
        obj.value = obj.value.replace(/\./g, '');
        if (obj.value.replace(',', '.') != 0) {
            var a = obj.value.replace(',', '.');
            a = parseFloat(a).toFixed(numdec);

            if (a != 'NaN') {
                if (numdec > 0) {
                    if (a.substring(a.length - (numdec + 1), 0).length >= 4) {
                        var decimali = a.substring(a.length, a.length - numdec);
                        var dascrivere = a.substring(a.length - (numdec + 1), 0);
                        var risultato = '';
//                        while (dascrivere.replace('-', '').length >= 4) {
//                            risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato;
//                            dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
//                        };
                        risultato = dascrivere + risultato + ',' + decimali;
                        document.getElementById(obj.id).value = risultato;
                    }
                    else {
                        document.getElementById(obj.id).value = a.replace(/\./g, ',');
                    };
                }
                else {
                    if (a.substring(a.length - (numdec + 1), 0).length >= 3) {
                        var dascrivere = a.substring(a.length, 0);
                        var risultato = '';
//                        while (dascrivere.replace('-', '').length >= 4) {
//                            risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato;
//                            dascrivere = dascrivere.substring(dascrivere.length - 3, 0);
//                        };
                        risultato = dascrivere + risultato;
                        document.getElementById(obj.id).value = risultato;
                    }
                    else {
                        document.getElementById(obj.id).value = a.replace(/\./g, ',');
                    };

                };
            }
            else {
                document.getElementById(obj.id).value = '';
            };
        };
    };
    function SostPuntVirg(e, obj) {
        var keyPressed;
        keypressed = (window.event) ? event.keyCode : e.which;
        if (keypressed == 46) {
            if (navigator.appName == 'Microsoft Internet Explorer') {
                event.keyCode = 0;
            }
            else {
                e.preventDefault();
            }
            obj.value += ',';
            obj.value = obj.value.replace('.', '');
        }

    }
</script>
