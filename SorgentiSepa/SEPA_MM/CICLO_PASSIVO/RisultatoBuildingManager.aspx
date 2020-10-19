<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatoBuildingManager.aspx.vb"
    Inherits="AMMSEPA_RisultatoBuildingManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Risultato Building Manager</title>
    <link href="CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="CicloPassivo.js" type="text/javascript"></script>
    <script src="../StandardTelerik/Scripts/jsFunzioni.js"></script>
</head>
<body class="sfondo">
    <form id="form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadFormDecorator Skin="Web20" ID="FormDecorator1" runat="server" DecoratedControls="Buttons" />
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="DataGridBM">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="DataGridBM" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Web20">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
            Localization-Cancel="Annulla">
            <Windows>
            </Windows>
        </telerik:RadWindowManager>
        <telerik:RadWindow ID="RadWindowBM" runat="server" CenterIfModal="true" Modal="true"
            Title="Gestione building manager" Width="680px" Height="330px" VisibleStatusbar="false"
            Behaviors="Pin, Maximize, Move, Resize">
            <ContentTemplate>
                <telerik:RadWindowManager ID="RadWindowManager2" runat="server" Localization-OK="Ok"
                    Localization-Cancel="Annulla">
                </telerik:RadWindowManager>
                <asp:Panel runat="server" ID="PanelServiziVoci" Style="height: 100%;" class="sfondo">
                    <table>

                        <tr>
                            <td class="TitoloModulo" colspan="3">Building Manager
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnSalvaBM" runat="server" Text="Salva" Style="cursor: pointer" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnChiudi" runat="server" Text="Esci" Style="cursor: pointer"
                                                OnClientClick="document.getElementById('TextBox1').value='0';closeWindow(null, null, 'RadWindowBM');" CausesValidation="false" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr style="text-align: left">
                            <td>
                                <asp:Label ID="lblCodice" runat="server" Text="Codice" Font-Names="arial" Font-Size="8pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <asp:TextBox ID="txtCodice" runat="server" MaxLength="100" Width="300px" CssClass="CssMaiuscolo"></asp:TextBox>
                            </td>
                        </tr>
                        <tr style="text-align: left">
                            <td>
                                <asp:Label ID="lblStruttura" runat="server" Text="Struttura" Font-Names="arial" Font-Size="8pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <telerik:RadComboBox ID="cmbfiliale" runat="server" AppendDataBoundItems="true"
                                    AutoPostBack="false" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                    ResolvedRenderMode="Classic" Width="300px">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr style="text-align: left">
                            <td>
                                <asp:Label ID="Label3" runat="server" Text="Operatore 1" Font-Names="arial" Font-Size="8pt"></asp:Label>
                            </td>
                            <td style="padding-left: 15px;">
                                <asp:Label ID="Label2" runat="server" Text="Inizio Val." Font-Names="arial" Font-Size="8pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">

                                <telerik:RadComboBox ID="cmbOperatore1" runat="server" AppendDataBoundItems="true"
                                    AutoPostBack="false" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                    ResolvedRenderMode="Classic" Width="300px">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtInizio1" runat="server" MaxLength="100" Width="100px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtInizio1"
                                    Display="Static" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                    Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"> </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr style="text-align: left">
                            <td>
                                <asp:Label ID="Label4" runat="server" Text="Operatore 2" Font-Names="arial" Font-Size="8pt"></asp:Label>
                            </td>
                            <td style="padding-left: 15px;">
                                <asp:Label ID="Label6" runat="server" Text="Inizio Val." Font-Names="arial" Font-Size="8pt"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="text-align: left">
                                <telerik:RadComboBox ID="cmbOperatore2" runat="server" AppendDataBoundItems="true"
                                    AutoPostBack="false" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                    ResolvedRenderMode="Classic" Width="300px">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <asp:TextBox ID="txtInizio2" runat="server" MaxLength="100" Width="100px"></asp:TextBox>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtInizio2"
                                    Display="Static" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                                    Style="height: 14px;" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"> </asp:RegularExpressionValidator>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp
                            </td>
                        </tr>

                    </table>

                    </table>
                </asp:Panel>
            </ContentTemplate>
        </telerik:RadWindow>
        <table style="width: 100%">
            <tr>
                <td class="TitoloModulo">Gestione - Building manager
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnModifica" runat="server" Text="Modifica" Style="cursor: pointer"
                                    ToolTip="Modifica" />
                            </td>
                            <td>
                                <asp:Button ID="btnEliminaBM" runat="server" Text="Elimina" Style="cursor: pointer"
                                    ToolTip="Elimina" />
                            </td>
                            <td>
                                <asp:Button ID="btnAnnulla" runat="server" Text="Esci" Style="cursor: pointer"
                                    ToolTip="Esci" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>

                <td>


                    <telerik:RadGrid ID="DataGridBM" runat="server" GroupPanelPosition="Top"
                        ResolvedRenderMode="Classic" AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                        PageSize="100" AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="99%"
                        AllowSorting="True" IsExporting="False" AllowPaging="True" PagerStyle-AlwaysVisible="true">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            CommandItemDisplay="Top">
                            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="true" />
                            <Columns>

                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" ReadOnly="True" Visible="False"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CODICE" HeaderText="CODICE"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FILIALE" HeaderText="SEDE TERRITORIALE"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="OPERATORE1" HeaderText="OPERATORE 1"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="OPERATORE2" HeaderText="OPERATORE 2"></telerik:GridBoundColumn>
                            </Columns>
                            <CommandItemTemplate>
                                <a id="addServizio" style="cursor: pointer" onclick="document.getElementById('TextBox1').value = '1';MostraDiv();openWindow(null, null, 'RadWindowBM')">
                                    <img style="border: 0px" alt="" src="Immagini/addRecord.gif" />
                                    Aggiungi nuovo record</a>
                            </CommandItemTemplate>
                        </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
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

                </td>

            </tr>


            <tr>
                <td>
                    <asp:Label runat="server" ID="txtmia"></asp:Label>

                </td>
            </tr>

            <tr>
                <td>
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Red" Height="16px" Visible="False" Width="100%"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        </td>
            </tr>
        </table>

      
            <asp:HiddenField ID="TextBox1" runat="server" />
        <asp:HiddenField ID="hiddenID" runat="server" />
        <asp:HiddenField ID="err" runat="server" Value="0" />
        </div>

        <div style="display: none">
            <table>
                <tr>
                    <td style="width: 5%; vertical-align: top;">
                        <table cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <img alt="Aggiungi BM" onclick="MostraDiv();document.getElementById('TextBox1').value = '1';"
                                        src="../NuoveImm/Img_Aggiungi.png" style="cursor: pointer;" id="imgAggiungiBM"
                                        title="Aggiungi BM" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="ImgModificaBM" OnClientClick="if (document.getElementById('txtmia').value != '') { document.getElementById('TextBox1').value='2';} else{  message('Attenzione',Messaggio.Elemento_No_Selezione); document.getElementById('TextBox1').value='0';}"
                                        runat="server" ImageUrl="~/NuoveImm/Img_Modifica.png" ToolTip="Modifica BM" />
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="~/NuoveImm/Img_Elimina.png"
                                        ToolTip="Elimina BM" OnClientClick="EliminaElemento();return false;" />
                                    <asp:Button ID="btnEliminaElemento" runat="server" Text="Button" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </form>
    <script language="javascript" type="text/javascript">
        //document.getElementById('dvvvPre').style.visibility = 'hidden';
        var Selezionato;
        var OldColor;
        var SelColo;
        //if (document.getElementById('CPContenuto_TextBox1')) {
        //    if ((document.getElementById('CPContenuto_TextBox1').value != '2') && (document.getElementById('CPContenuto_TextBox1').value != '1')) {
        //        NascondiDiv();
        //    } else {
        //        MostraDiv();
        //    }
        //    document.getElementById('CPContenuto_btnEliminaElemento').style.visibility = 'hidden';

        //    document.getElementById('CPContenuto_btnEliminaElemento').style.left = '-100px';
        //    document.getElementById('CPContenuto_btnEliminaElemento').style.display = 'none';
        //}
        //else {
        //    if (document.getElementById('TextBox1')) {
        //        if ((document.getElementById('TextBox1').value != '2') && (document.getElementById('TextBox1').value != '1')) {
        //            NascondiDiv();
        //        } else {
        //            MostraDiv();
        //        }
        //        document.getElementById('btnEliminaElemento').style.visibility = 'hidden';

        //        document.getElementById('btnEliminaElemento').style.left = '-100px';
        //        document.getElementById('btnEliminaElemento').style.display = 'none';
        //    }
        //};


        function MostraDiv() {




            var v = $find('<%=cmbfiliale.ClientID %>');
            if (v)
                v.clearSelection();
            v = $find('<%=cmbOperatore1.ClientID %>');
            if (v)
                v.clearSelection();
            v = $find('<%=cmbOperatore2.ClientID %>');
            if (v)
                v.clearSelection();
            if (document.getElementById('RadWindowBM_C_txtCodice'))
                document.getElementById('RadWindowBM_C_txtCodice').value = '';
            if (document.getElementById('RadWindowBM_C_txtInizio1'))
                document.getElementById('RadWindowBM_C_txtInizio1').value = '';
            if (document.getElementById('RadWindowBM_C_txtInizio2'))
                document.getElementById('RadWindowBM_C_txtInizio2').value = '';



        };

        function NascondiDiv() {
            document.getElementById('err').value = '0';

        };
        function EliminaElemento() {
            if (document.getElementById('txtmia')) {
                if (document.getElementById('txtmia').value != '') {
                    var chiediConferma;
                    chiediConferma = window.confirm("Eliminare l\'elemento selezionato?");
                    if (chiediConferma == true) {
                        document.getElementById('btnEliminaElemento').click();
                    }
                    else {

                    }
                }
                else {
                    alert('Nessun elemento selezionato!');
                }
            }
        };

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
</body>
</html>
