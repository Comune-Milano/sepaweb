<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ParametriGestCredito.aspx.vb"
    Inherits="Contratti_ParametriGestCredito" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script src="../CICLO_PASSIVO/CicloPassivo.js" type="text/javascript"></script>
    <script src="../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <link href="../CICLO_PASSIVO/CicloPassivo.css" rel="stylesheet" />
    <script type="text/javascript">

        var Selezionato;
        var Selezionato1;
    </script>
    <style type="text/css">
        .CSSmaiuscolo {
            text-transform: uppercase;
        }

        .style1 {
            width: 400px;
        }

        .style2 {
            height: 24px;
        }
    </style>
</head>
<body class="sfondo">
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
        <telerik:RadFormDecorator Skin="Web20" ID="FormDecorator1" runat="server" DecoratedControls="Buttons" />
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
            <Windows>
                <telerik:RadWindow ID="RadWindowMesi" runat="server" CenterIfModal="true" Modal="true"
                    Title="Gestione mesi" AutoSize="true" VisibleStatusbar="false"
                    Behaviors="Pin, Maximize, Move, Resize">
                    <ContentTemplate>
                        <asp:Panel runat="server" ID="PanelRadWindowMesi" Style="height: 100%;" class="sfondo">
                            <table width="460px" style="height: 250px; text-align: center; margin-left: 10px; z-index: 400;"
                                align="center">
                                <tr>
                                    <td style="height: 19px; text-align: left" align="center" valign="middle" colspan="2">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td class="TitoloModulo">MODIFICA MENSILITA'
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <telerik:RadButton ID="btn_inserisci" runat="server" Text="Salva" ToolTip="Salva"
                                                        CausesValidation="False">
                                                    </telerik:RadButton>
                                                </td>
                                                <td>
                                                    <telerik:RadButton ID="btn_chiudi" runat="server" Text="Esci" ToolTip="Esci senza inserire" AutoPostBack="false"
                                                        CausesValidation="False" OnClientClicking="function(sender, args){closeWindow(sender, args, 'RadWindowMesi');}">
                                                    </telerik:RadButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <span style="font-size: 10pt; font-family: Arial">Num.mesi</span>
                                    </td>
                                    <td style="text-align: left;">
                                        <asp:TextBox ID="txtDurata" runat="server" Font-Names="Arial" Font-Size="10pt" Width="150px"
                                            MaxLength="400" TabIndex="1" Font-Bold="True" CssClass="CSSmaiuscolo"></asp:TextBox>
                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator15" runat="server"
                                            ControlToValidate="txtDurata" Display="Dynamic" ErrorMessage="!!" Font-Bold="True"
                                            Font-Names="arial" Font-Size="8pt" ValidationExpression="\d+" ForeColor="Red"></asp:RegularExpressionValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left" height="50px">&nbsp;
                                    </td>
                                    <td style="text-align: right"></td>
                                </tr>
                            </table>

                        </asp:Panel>
                    </ContentTemplate>
                </telerik:RadWindow>

                <telerik:RadWindow ID="RadWindowGestCrediti" runat="server" CenterIfModal="true" Modal="true"
                    Title="Gestione" AutoSize="true" VisibleStatusbar="false"
                    Behaviors="Pin, Maximize, Move, Resize">
                    <ContentTemplate>
                        <asp:Panel runat="server" ID="Panel1" Style="height: 100%;" class="sfondo">
                            <table border="0" cellpadding="1" cellspacing="1">
                                <tr>
                                    <td colspan="2">
                                        <table style="width: 100%">
                                            <tr>
                                                <td class="TitoloModulo">Gestione - Parametri - Gestione crediti
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                <telerik:RadButton ID="img_InserisciSchema" runat="server" Text="Salva" ToolTip="Salva"
                                                                    CausesValidation="False">
                                                                </telerik:RadButton>
                                                            </td>
                                                            <td>
                                                                <telerik:RadButton ID="img_ChiudiSchema" runat="server" Text="Esci" ToolTip="Esci senza inserire" AutoPostBack="false"
                                                                    CausesValidation="False" OnClientClicking="function(sender, args){closeWindow(sender, args, 'RadWindowGestCrediti');}">
                                                                </telerik:RadButton>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="text-align: left" class="style1">&nbsp;</td>
                                    <td style="width: 469px; height: 19px; text-align: left">&nbsp;</td>
                                </tr>
                                <tr>
                                    <td style="text-align: left" class="style1">
                                        <span style="font-size: 10pt; font-family: Arial">Struttura Competente</span></td>
                                    <td style="width: 469px; height: 19px; text-align: left">
                                        <telerik:RadComboBox ID="cmbstruttura" runat="server" AppendDataBoundItems="true"
                                            AutoPostBack="false" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                            ResolvedRenderMode="Classic" Width="380">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr style="font-size: 12pt; font-family: Times New Roman">
                                    <td style="text-align: left" class="style1">
                                        <span style="font-size: 10pt; font-family: Arial">Voce B.P. Dep. Cauz.</span></td>
                                    <td style="width: 469px; height: 19px; text-align: left">
                                        <telerik:RadComboBox ID="cmbVoceBP" runat="server" AppendDataBoundItems="true"
                                            AutoPostBack="false" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                            ResolvedRenderMode="Classic" Width="380">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr style="font-size: 12pt; font-family: Times New Roman">
                                    <td style="text-align: left" class="style1">
                                        <span style="font-size: 10pt; font-family: Arial">Voce B.P. Res.Crediti</span> </td>
                                    <td style="width: 469px; height: 19px; text-align: left">

                                        <telerik:RadComboBox ID="cmbVoceBP0" runat="server" AppendDataBoundItems="true"
                                            AutoPostBack="false" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                            ResolvedRenderMode="Classic" Width="380">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr style="font-size: 12pt; font-family: Times New Roman">
                                    <td style="text-align: left" class="style1" valign="top">
                                        <span style="font-size: 10pt; font-family: Arial">Doc.Contabile</span></td>
                                    <td style="width: 469px; height: 19px; text-align: left">
                                        <telerik:RadComboBox ID="cmbDocContabile" runat="server" AppendDataBoundItems="true"
                                            AutoPostBack="false" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                            ResolvedRenderMode="Classic" Width="380">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <span style="font-size: 10pt; font-family: Arial">Fornitore</span></td>
                                    <td style="width: 469px; height: 19px">

                                        <telerik:RadComboBox ID="cmbFornitore" runat="server" AppendDataBoundItems="true"
                                            AutoPostBack="false" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                            ResolvedRenderMode="Classic" Width="380">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <span style="font-size: 10pt; font-family: Arial">Inizio Validità E.F.</span></td>
                                    <td style="width: 469px; height: 19px">
                                        <asp:TextBox ID="txtInizio" runat="server" BorderStyle="Solid"
                                            BorderWidth="1px" MaxLength="10"
                                            Style="z-index: 200;" ToolTip="gg/mm/aaaa" Width="70px" TabIndex="104"></asp:TextBox>
                                        &nbsp;
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtInizio"
                        Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        TabIndex="-1"
                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        <span style="font-size: 10pt; font-family: Arial">Fine Validità E.F.</span></td>
                                    <td style="width: 469px; height: 19px">
                                        <asp:TextBox ID="txtFine" runat="server" BorderStyle="Solid"
                                            BorderWidth="1px" MaxLength="10"
                                            Style="z-index: 200;" ToolTip="gg/mm/aaaa" Width="70px" TabIndex="105"></asp:TextBox>
                                        &nbsp;
                    <asp:RegularExpressionValidator ID="RegularExpressionValidator16" runat="server" ControlToValidate="txtFine"
                        Display="Dynamic" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        TabIndex="-1"
                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator></td>
                                </tr>

                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                </telerik:RadWindow>
            </Windows>
        </telerik:RadWindowManager>
        <div>
            <table style="width: 96%; margin-left: 15px;">
                <tr>
                    <td class="TitoloModulo" colspan="2">Gestione - Parametri - Gestione crediti
                    </td>
                </tr>
                <tr>
                    <td style="height: 20px"></td>
                </tr>
                <tr>
                    <td style="width: 80%">
                        <div style="overflow: auto; width: 100%; height: 100%;">
                            <telerik:RadGrid ID="DataGridParam" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                                AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                                AllowFilteringByColumn="false" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                                IsExporting="False" PagerStyle-AlwaysVisible="true">
                                <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                    CommandItemDisplay="Top" Width="100%">
                                    <CommandItemSettings ShowExportToExcelButton="false" ShowExportToWordButton="false"
                                        ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                        ShowRefreshButton="false" />
                                    <Columns>
                                        <telerik:GridBoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Width="20%" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="N_MESI" HeaderText="NUM.MESI">
                                            <HeaderStyle Width="70%" />
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Width="30%" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn UniqueName="modificaServizio" HeaderText="" ButtonType="ImageButton"
                                            ShowInEditForm="true" CommandName="myEdit" ConfirmDialogType="RadWindow" ButtonCssClass="rgEdit">
                                            <HeaderStyle Width="3%" />
                                            <ItemStyle Width="24px" Height="24px" />
                                        </telerik:GridButtonColumn>
                                    </Columns>

                                </MasterTableView>
                                <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                                    <Excel FileExtension="xls" Format="Xlsx" />
                                </ExportSettings>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                    <Selecting AllowRowSelect="True" />
                                    <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                        AllowResizeToFit="true" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </div>



                    </td>
                    <telerik:RadButton ID="btnModifica" runat="server" Style="visibility: hidden">
                    </telerik:RadButton>
                    <td style="width: 20%" valign="top">

                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:TextBox ID="txtmia" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="11pt"
                            Width="624px" BackColor="#F2F5F1" BorderStyle="None" BorderWidth="0px">Nessuna Selezione</asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp
                    </td>
                </tr>
                <tr>
                    <td style="width: 100%;">
                        <div style="overflow: auto; width: 100%; height: 100%;">
                            <telerik:RadGrid ID="DataGridParam0" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                                AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                                AllowFilteringByColumn="false" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                                IsExporting="False" PagerStyle-AlwaysVisible="true">
                                <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                                    CommandItemDisplay="Top" Width="100%">
                                    <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                        ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                        ShowRefreshButton="true" />
                                    <Columns>
                                        <telerik:GridBoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Wrap="False" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="STRUTTURA" HeaderText="STRUTTURA">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Width="20%" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="VOCE_BP" HeaderText="VOCE B.P. DEP.CAUZIONALE">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Width="20%" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="VOCE_BP_CREDITI" HeaderText="VOCE B.P. CREDITO GEN.">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Width="20%" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DATA_INIZIO_VALIDITA" HeaderText="INIZIO VAL.">
                                            <HeaderStyle Width="70%" Font-Bold="True" />
                                            <HeaderStyle Font-Bold="true" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" Width="10%" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                Font-Underline="False" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="DATA_FINE_VALIDITA" HeaderText="FINE VAL.">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                                Font-Strikeout="False" Font-Underline="False" Width="10%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="TIPO_DOC_CONT" HeaderText="TIPO DOC. CONTABILE">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                                Font-Strikeout="False" Font-Underline="False" Width="30%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridBoundColumn DataField="FORNITORE" HeaderText="FORNITORE">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                                Font-Strikeout="False" Font-Underline="False" Width="30%" />
                                        </telerik:GridBoundColumn>
                                        <telerik:GridButtonColumn UniqueName="modificaServizio" HeaderText="" ButtonType="ImageButton"
                                            ShowInEditForm="true" CommandName="myEdit" ConfirmDialogType="RadWindow" ButtonCssClass="rgEdit">
                                            <HeaderStyle Width="3%" />
                                            <ItemStyle Width="24px" Height="24px" />
                                        </telerik:GridButtonColumn>
                                        <telerik:GridButtonColumn HeaderStyle-Width="3%" CommandName="Delete" ImageUrl="../Immagini/Delete.gif"
                                            Text="Elimina" UniqueName="DeleteColumn" ButtonType="ImageButton" ButtonCssClass="rgDel">
                                            <ItemStyle HorizontalAlign="Center" />
                                            <HeaderStyle HorizontalAlign="Center" Width="24px" Height="24px" />
                                        </telerik:GridButtonColumn>
                                    </Columns>
                                    <CommandItemTemplate>
                                        <a id="addServizio" style="cursor: pointer" onclick="openWindow(null, null, 'RadWindowGestCrediti');document.getElementById('TextBox1').value=1;document.getElementById('TextBox1').value=1;">
                                            <img style="border: 0px" alt="" src="../CICLO_PASSIVO/Immagini/AddRecord.gif" />
                                            Aggiungi nuovo record</a>
                                    </CommandItemTemplate>
                                </MasterTableView>
                                <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                                <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                                    <Excel FileExtension="xls" Format="Xlsx" />
                                </ExportSettings>
                                <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true">
                                    <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                                    <Selecting AllowRowSelect="True" />
                                    <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                        AllowResizeToFit="true" />
                                </ClientSettings>
                            </telerik:RadGrid>
                        </div>
                    </td>
                    <telerik:RadButton ID="btnModifica0" runat="server" Style="visibility: hidden">
                    </telerik:RadButton>
                </tr>
                <tr>
                    <td style="width: 80%;">
                        <asp:TextBox ID="txtmia0" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="11pt"
                            Width="624px" BackColor="#F2F5F1" BorderStyle="None" BorderWidth="0px">Nessuna Selezione</asp:TextBox>
                    </td>
                    <td style="width: 20%" valign="top">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 80%;">
                        <asp:Label ID="lblErrore" runat="server" ForeColor="#CC0000" Visible="False"></asp:Label>
                    </td>
                    <td style="width: 20%" valign="top">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 80%; height: 60px;" valign="bottom" align="right">
                        <asp:ImageButton ID="btnHome" runat="server" ImageUrl="../NuoveImm/Img_Home.png"
                            ToolTip="Home" />
                    </td>
                    <td style="width: 20%" valign="top">&nbsp;
                    </td>
                </tr>
            </table>
        </div>


        <asp:HiddenField ID="LBLID" runat="server" Value="0" />
        <asp:HiddenField ID="LBLID1" runat="server" Value="0" />
        <asp:HiddenField ID="Modificato" runat="server" />
        <asp:HiddenField ID="ConfElimina" runat="server" Value="0" />
        <asp:HiddenField ID="eliminato" runat="server" Value="0" />
        <asp:HiddenField ID="TextBox1" runat="server" />
        <script language="javascript" type="text/javascript">
            //        myOpacity = new fx.Opacity('InseriMotivazione', { duration: 200 });

            if ((document.getElementById('Modificato').value != '2') && (document.getElementById('Modificato').value != '1')) {
                //document.getElementById('InseriMotivazione').style.visibility = 'hidden';

            } else {

                // document.getElementById('InseriMotivazione').style.visibility = 'visible';

            }

            if ((document.getElementById('TextBox1').value != '2') && (document.getElementById('TextBox1').value != '1')) {
                //document.getElementById('InserimentoP').style.visibility = 'hidden';

            } else {

                // document.getElementById('InserimentoP').style.visibility = 'visible';

            }
            function Sicuro() {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione, Eliminare la voce selezionata?");
                if (chiediConferma == true) {
                    document.getElementById('eliminato').value = '1';
                }
                else {
                    document.getElementById('eliminato').value = '0';
                }
            }
        </script>
        <script type="text/javascript">
            //document.onkeydown = $onkeydown;


            function CompletaData(e, obj) {
                // Check if the key is a number
                var sKeyPressed;

                sKeyPressed = (window.event) ? event.keyCode : e.which;

                if (sKeyPressed < 48 || sKeyPressed > 57) {
                    if (sKeyPressed != 8 && sKeyPressed != 0) {
                        // don't insert last non-numeric character
                        if (window.event) {
                            event.keyCode = 0;
                        }
                        else {
                            e.preventDefault();
                        }
                    }
                }
                else {
                    if (obj.value.length == 2) {
                        obj.value += "/";
                    }
                    else if (obj.value.length == 5) {
                        obj.value += "/";
                    }
                    else if (obj.value.length > 9) {
                        var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                        if (selText.length == 0) {
                            // make sure the field doesn't exceed the maximum length
                            if (window.event) {
                                event.keyCode = 0;
                            }
                            else {
                                e.preventDefault();
                            }
                        }
                    }
                }
            }


        </script>

    </form>
</body>
</html>
