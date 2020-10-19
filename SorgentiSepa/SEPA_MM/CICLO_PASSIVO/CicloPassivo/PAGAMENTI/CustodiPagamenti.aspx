<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CustodiPagamenti.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_CustodiPagamenti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <title>Fatture Utenze</title>
    <style type="text/css">
        .bottone
        {
            /*background-color: #507cd1;     border-style: none;     color: White;     font-weight: bold;     font-size: 8pt;     height: 25px;     cursor: pointer;     */
            background-color: transparent;
            border-left: 8px solid #800000;
            border-right: 0px solid #800000;
            border-top: 0px solid #800000;
            border-bottom: 0px solid #800000;
            font-weight: bold;
            font-size: 9pt;
            cursor: pointer;
        }
        .divFullscreen
        {
            clear: both;
            height: 100%;
            border-top: 1px solid black;
        }
    </style>
    <script language="javascript" type="text/javascript">
        var Selezionato;
        function caricamentoincorso() {
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate();
                if (Page_IsValid) {
                    if (document.getElementById('caricamento') != null) {
                        document.getElementById('caricamento').style.display = 'block';
                    };
                }
                else {
                    apriAlert('ATTENZIONE! Ci sono delle incongruenze dati della pagina e/o eventuali TAB!', 300, 150, 'Attenzione', null, null);

                };
            }
            else {
                if (document.getElementById('caricamento') != null) {
                    document.getElementById('caricamento').style.display = 'block';
                };
            };
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
        function IsSelected(cb, id) {
            if (cb.checked == true) {
                document.getElementById("idSelezionati").value += id + ',';
            }
            else {

                document.getElementById("idSelezionati").value = document.getElementById("idSelezionati").value.replace(id + ',', '');
            };

        };
        function Apri() {
            window.showModalDialog('FatturePagaUt.aspx?TIPO=C', 'window', 'status:no;dialogWidth:800px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');
        };
        

    </script>
</head>
<body class="sfondo">
    <div id="caricamento" style="margin: 0px; background-color: #C0C0C0; width: 100%;
        height: 100%; position: fixed; top: 0px; left: 0px; filter: alpha(opacity='75');
        opacity: 0.75; background-color: #eeeeee; z-index: 500">
        <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
            margin-top: -48px; background-image: url('Immagini/sfondo2.png');">
            <table style="width: 100%; height: 100%">
                <tr>
                    <td valign="middle" align="center">
                        <img alt="Caricamento" src="Immagini/load.gif" />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="center">
                        Caricamento . . .
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <form id="form1" runat="server" defaultbutton="btnCerca" defaultfocus="btnCerca"
    onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
         <telerik:RadWindow ID="RadWindow1" runat="server" CenterIfModal="true" Modal="true" InitialBehaviors="Maximize" ClientIDMode="Static" 
            VisibleStatusbar="False" AutoSize="false" Height="500" Width="800" Behavior="Pin, Move, Resize" Skin="Web20">
        </telerik:RadWindow>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <div>
        <telerik:RadNotification ID="RadNotificationNote" runat="server" Height="140px" Animation="Fade"
            EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="3500" Position="BottomRight"
            OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
        </telerik:RadNotification>
    </div>
    <table style="width: 100%;" class="FontTelerik">
        <tr>
            <td class="TitoloModulo">
                <asp:Label ID="lblTitolo" runat="server" Text="Pagamento Custodi"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="font-size: 5pt">
                <table>
                    <tr>
                    </tr>
                    <tr>
                        <td style="text-align: right">
                            <telerik:RadButton ID="btnCreaCdp" runat="server" Text="Crea CDP" ToolTip="Crea il cdp per le righe selezionate"
                                Style="top: 0px; left: 0px" />
                        </td>
                        <td style="text-align: right">
                            <telerik:RadButton ID="btnEliminaFattura" runat="server" Text="Elimina record" ToolTip="Elimina i record selezionati"
                                Style="top: 0px; left: 0px" />
                        </td>
                        <td style="text-align: right">
                            <telerik:RadButton ID="btnEliminaFatturaTutte" runat="server" Text="Elimina tutto"
                                ToolTip="Elimina tutti i record in base ai filtri definiti" Style="top: 0px;
                                left: 0px" />
                        </td>
                        <td style="text-align: right">
                            <telerik:RadButton ID="btnAnnulla" runat="server" Text="Esci" ToolTip="Esci" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td width="50%">
                            <fieldset style="border: thin solid #99CCFF">
                                <legend><strong style="text-align: left">
                                    <table>
                                        <tr>
                                            <td>
                                                FILTRI di RICERCA
                                            </td>
                                        </tr>
                                    </table>
                                </strong></legend>
                                <table>
                                    <tr>
                                        <td>
                                            <table style="border: thin solid #3399FF;">
                                                <tr>
                                                    <td colspan="1">
                                                        COD. CUSTODE
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtCodCustode" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                            Width="150px" MaxLength="50"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table style="border: thin solid #FF6600;">
                                                <tr>
                                                    <td colspan="1">
                                                        ANNO
                                                    </td>
                                                    <td colspan="1">
                                                        MESE
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtAnno" runat="server" Font-Names="Arial" Font-Size="8pt" Width="60px"
                                                            MaxLength="4"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtMese" runat="server" Font-Names="Arial" Font-Size="8pt" Width="50px"
                                                            MaxLength="50"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/CICLO_PASSIVO/Immagini/search-icon.png"
                                                OnClientClick="caricamentoincorso()" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnPulisci" runat="server" ImageUrl="~/CICLO_PASSIVO/Immagini/broom-icon.png"
                                                OnClientClick="caricamentoincorso()" />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadButton ID="btnExportXls" runat="server" Text="Esporta XLS" ToolTip="Esporta in formato xls" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <table style="border: thin solid #FFFF66; width: 100%;">
                                                <tr>
                                                    <td style="text-align: center;">
                                                        Fornitore
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="text-align: center">
                                                        <telerik:RadComboBox ID="cmbFornitore" runat="server" AppendDataBoundItems="true"
                                                            AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                                            ResolvedRenderMode="Classic" Width="100%">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                        <td valign="top">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
     
    </table>
        <table>
               <tr>
            <td>
                <asp:Panel runat="server" ID="PanelRadGrid" Style="width: 100%; height: 100%">
                    <telerik:RadGrid ID="dgvFattUtenze" runat="server" ResolvedRenderMode="Classic" AutoGenerateColumns="False"
                        Culture="it-IT" RegisterWithScriptManager="False" PagerStyle-Visible="true" PagerStyle-AlwaysVisible="true"
                        AllowPaging="true" EnableLinqExpressions="False" Width="99%" AllowSorting="True" HeaderStyle-Width="15%"
                        PageSize="100" IsExporting="False">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            Width="200%">
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="ID" Visible="False" DataField="ID">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CHECKED" HeaderText="CHECKED" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelAll" runat="server" Text="SELEZIONE" AutoPostBack="True"
                                            OnCheckedChanged="chkSelAll_CheckedChanged" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSel" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.CHECKED") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="DATA_CARICAMENTO" HeaderText="CARICAMENTO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FORNITORE" HeaderText="FORNITORE">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PIANO" HeaderText="ESERCIZIO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" VerticalAlign="Middle" Wrap="False" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SIGLA" HeaderText="SIGLA">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ANNO" HeaderText="ANNO">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="MESE" HeaderText="MESE">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="COD_CUSTODE" HeaderText="COD. CUSTODE">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="VOCE_BP" HeaderText="VOCE BP">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IMPORTO" HeaderText="IMPORTO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                </telerik:GridBoundColumn>
                            </Columns>
                        </MasterTableView>
                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                            <Excel FileExtension="xls" Format="Xlsx" />
                        </ExportSettings>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true"
                            ClientEvents-OnCommand="onCommand">
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            <Selecting AllowRowSelect="True" />
                            <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                AllowResizeToFit="true" />
                        </ClientSettings>
                    </telerik:RadGrid>
                </asp:Panel>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp; &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                            <asp:Button ID="btnCrea" runat="server" Text="CREA CDP" ToolTip="Crea il cdp per le righe selezionate"
                                Style="visibility: hidden" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="idSelezionati" runat="server" />
                <asp:HiddenField ID="selAll" runat="server" Value="0" />
                <asp:HiddenField ID="flReload" runat="server" Value="0" />
                <asp:HiddenField ID="HFGriglia" runat="server" />
                 <asp:HiddenField ID="HFAltezzaSottratta" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        </table>
    </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('caricamento').style.display = 'none';

        window.onresize = setDimensioni;
        Sys.Application.add_load(setDimensioni);
    </script>
</body>
</html>
