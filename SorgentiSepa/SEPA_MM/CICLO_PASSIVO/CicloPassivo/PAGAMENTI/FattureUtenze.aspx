<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FattureUtenze.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_FattureUtenze" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="../../CicloPassivo.js"></script>
    <link href="../../CicloPassivo.css" rel="stylesheet" />
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js"></script>
    <title>Fatture Utenze</title>
    <style type="text/css">
        .style1 {
            font-family: Arial;
            font-size: 8pt;
            text-align: left;
        }

        .style2 {
            font-family: Arial;
            font-size: 8pt;
        }

        .style4 {
            text-align: center;
            font-family: Arial;
            font-size: 8pt;
        }

        .bottone {
            /*background-color: #507cd1;     border-style: none;     color: White;     font-weight: bold;     font-size: 8pt;     height: 25px;     cursor: pointer;     */
            background-color: transparent;
            border-left: 8px solid #800000;
            border-right: 0px solid #800000;
            border-top: 0px solid #800000;
            border-bottom: 0px solid #800000;
            font-weight: bold;
            font-size: 9pt;
            height: 22px;
            cursor: pointer;
        }

        .divFullscreen {
            clear: both;
            height: 100%;
            border-top: 1px solid black;
        }

        .style5 {
            font-family: Arial;
            font-size: 10pt;
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
                    alert('ATTENZIONE! Ci sono delle incongruenze dati della pagina e/o eventuali TAB!');
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

        //function Apri() {
        //    var oWnd = $find('RadWindow1');
        //    oWnd.setUrl('FatturePagaUt.aspx?TIPO=U?';oWnd.show();
        //    //window.showModalDialog('FatturePagaUt.aspx?TIPO=U', 'window', 'status:no;dialogWidth:800px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');
        //};


    </script>
</head>
<body class="sfondo">
    <form id="form1" runat="server" onsubmit="caricamento();return true;" class="FontTelerik">
        <telerik:RadFormDecorator Skin="Web20" ID="FormDecorator1" runat="server" DecoratedControls="Buttons" />
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadWindow ID="RadWindow1" runat="server" CenterIfModal="true" Modal="true" InitialBehaviors="Maximize" ClientIDMode="Static"
            VisibleStatusbar="False" AutoSize="false" Height="500" Width="800" Behavior="Pin, Move, Resize" Skin="Web20">
        </telerik:RadWindow>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok" RestrictionZoneID="divGenerale"
            Localization-Cancel="Annulla">
        </telerik:RadWindowManager>
        <table style="width: 100%;">
            <tr>
                <td class="TitoloModulo">
                    <asp:Label ID="lblTitolo" runat="server" Text="FATTURE UTENZE"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td style="font-size: 5pt">
                    <table>

                        <tr>

                            <td style="text-align: right">
                                <asp:Button ID="btnCreaCdp" runat="server" Text="Crea CDP" ToolTip="Crea il cdp per le righe selezionate" Style="cursor: pointer" />
                            </td>
                            <td style="text-align: right">
                                <asp:Button ID="btnEliminaFattura" runat="server"
                                    Text="Elimina Fattura" ToolTip="Elimina il record selezionato" Style="cursor: pointer" />
                            </td>
                            <td style="text-align: right">
                                <asp:Button ID="btnEliminaFattura0" runat="server"
                                    Text="Elimina Tutto"
                                    ToolTip="Elimina tutti i record in base ai filtri definiti" Style="cursor: pointer" />
                            </td>
                            <td style="text-align: right">
                                <asp:Button ID="btnSalva0" runat="server" Text="Esci" ToolTip="Esci" Style="cursor: pointer" />
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
                                    <legend class="style1">FILTRI</legend>


                                    <table>
                                        <tr>
                                            <td>
                                                <table style="border: thin solid #009900;">
                                                    <tr>
                                                        <td colspan="3" class="style4">Data Emissione
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtEmDal" runat="server" Font-Names="Arial" Font-Size="8pt" Width="70px"
                                                                MaxLength="10"></asp:TextBox>
                                                        </td>
                                                        <td class="style2">al
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtEmAl" runat="server" Font-Names="Arial" Font-Size="8pt" Width="70px"
                                                                MaxLength="10"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table style="border: thin solid #CC3300;">
                                                    <tr>
                                                        <td colspan="3" class="style4">Data Scadenza
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtScadDal" runat="server" Font-Names="Arial" Font-Size="8pt" Width="70px"
                                                                MaxLength="10"></asp:TextBox>
                                                        </td>
                                                        <td class="style2">al
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtScadAl" runat="server" Font-Names="Arial" Font-Size="8pt" Width="70px"
                                                                MaxLength="10"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table style="border: thin solid #3399FF;">
                                                    <tr>
                                                        <td colspan="1" class="style4">POD
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtPOD" runat="server" Font-Names="Arial" Font-Size="8pt" Width="150px"
                                                                MaxLength="50"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table style="border: thin solid #000000;">
                                                    <tr>
                                                        <td colspan="1" class="style4">Fattura
                                                        </td>
                                                        <td class="style4">Anno
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtNumFattura" runat="server" Font-Names="Arial" Font-Size="8pt"
                                                                Width="100px" MaxLength="20"></asp:TextBox>
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtAnno" runat="server" Font-Names="Arial" Font-Size="8pt" Width="40px"
                                                                MaxLength="4"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/CICLO_PASSIVO/Immagini/search-icon.png"
                                                    OnClientClick="caricamentoincorso()"
                                                    ToolTip="Cerca in base ai filtri definiti" />
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="btnPulisci" runat="server" ImageUrl="~/CICLO_PASSIVO/Immagini/broom-icon.png"
                                                    OnClientClick="caricamentoincorso()" ToolTip="Azzera filtri di ricerca" />
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>
                                                <asp:Button ID="btnExportXls" runat="server" Text="Esporta Xls"
                                                    ToolTip="Esporta in formato xls" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table style="border: thin solid #CC33FF;">
                                                    <tr>
                                                        <td colspan="3" class="style4">Data caricamento</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtCaricDal" runat="server" Font-Names="Arial" Font-Size="8pt" Width="70px"
                                                                MaxLength="10"></asp:TextBox>
                                                        </td>
                                                        <td class="style2">al
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtCaricAl" runat="server" Font-Names="Arial" Font-Size="8pt" Width="70px"
                                                                MaxLength="10"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table style="border: thin solid #CCCC00;">
                                                    <tr>
                                                        <td colspan="3" class="style4">Data Inizio Periodo</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtInizioDal" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Width="70px"
                                                                MaxLength="10"></asp:TextBox>
                                                        </td>
                                                        <td class="style2">al
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtInizioAl" runat="server" Font-Names="Arial" Font-Size="8pt" Width="70px"
                                                                MaxLength="10"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>
                                                <table style="border: thin solid #FF9933;">
                                                    <tr>
                                                        <td colspan="3" class="style4">Data Fine Periodo</td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:TextBox ID="txtFinPerDal" runat="server" Font-Names="Arial"
                                                                Font-Size="8pt" Width="70px"
                                                                MaxLength="10"></asp:TextBox>
                                                        </td>
                                                        <td class="style2">al
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtFinPerAl" runat="server" Font-Names="Arial" Font-Size="8pt" Width="70px"
                                                                MaxLength="10"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <table style="border: thin solid #FFFF66; width: 100%;">
                                                    <tr>
                                                        <td style="text-align: center; font-family: Arial; font-size: 8pt">Fornitore
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="text-align: center">
                                                            <telerik:RadComboBox ID="cmbFornitore" runat="server" AppendDataBoundItems="true"
                                                                AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                                                ResolvedRenderMode="Classic" Width="650">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                            <td>&nbsp;</td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </td>
                            <td valign="top">&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="style5">&nbsp;</td>
            </tr>


            <tr>
                <td>&nbsp;
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td>

                    <telerik:RadGrid ID="dgvFattUtenze" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                        PagerStyle-Visible="true" PagerStyle-AlwaysVisible="true" AllowPaging="true" HeaderStyle-Width="15%"
                        AllowFilteringByColumn="True" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                        PageSize="100" IsExporting="False">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true" Width="200%"
                            CommandItemDisplay="Top">
                            <CommandItemSettings ShowExportToExcelButton="false" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="false" />

                            <Columns>
                                <telerik:GridBoundColumn HeaderText="ID" Visible="False" DataField="ID"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CHECKED" HeaderText="CHECKED" Visible="False"></telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelAll" runat="server" OnCheckedChanged="chkSelAll_CheckedChanged"
                                            Text="SELEZIONE" AutoPostBack="True" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSel" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.CHECKED") %>' />
                                    </ItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="DATA_CARICAMENTO" HeaderText="CARICAMENTO"></telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FORNITORE" HeaderText="FORNITORE">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DATA_EMISSIONE" HeaderText="EMISSIONE">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DATA_SCADENZA" HeaderText="SCADENZA">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NUMERO_FATTURA" HeaderText="N° FATTURA">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ANNO_FATTURA" HeaderText="ANNO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DATA_INIZIO_PERIODO" HeaderText="INIZIO PERIODO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DATA_FINE_PERIODO" HeaderText="FINE PERIODO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="POD" HeaderText="POD">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NOME_VIA_FORNITURA" HeaderText="VIA FORNITURA">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NUMERO_CIVICO_FORNITURA" HeaderText="CIVICO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="BARRATO_FORNITURA" HeaderText="BARRATO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CAP_FORNITURA" HeaderText="CAP">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="LOCALITA_FORNITURA" HeaderText="LOCALITA">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PROVINCIA_FORNITURA" HeaderText="PROVINCIA">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PIANO" HeaderText="ESERCIZIO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" VerticalAlign="Middle" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="VOCE_BP" HeaderText="VOCE BP">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center"
                                        Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                        Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FORM_TOTALE_ONERI_DIVERSI" HeaderText="ONERI DIVERSI">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FORM_BASE_IMPONIBILE" HeaderText="IMPONIBILE">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FORM_IVA" HeaderText="IVA">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FORM_TOTALE_BOLLETTA" HeaderText="TOT BOLLETTA">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FORM_TOTALE_BOLLETTINO" HeaderText="TOT BOLLETTINO">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" />
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

                            <Resizing AllowColumnResize="false" ResizeGridOnColumnResize="FALSE" EnableRealTimeResize="true" EnableNextColumnResize="true"
                                AllowResizeToFit="true" />
                        </ClientSettings>
                    </telerik:RadGrid>

                </td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="idSelezionati" runat="server" />
                    <asp:HiddenField ID="selAll" runat="server" Value="0" />
                    <asp:HiddenField ID="flReload" runat="server" Value="0" />
                    <asp:HiddenField ID="HFGriglia" runat="server" />
                    <asp:HiddenField ID="HFAltezzaSottratta" runat="server" Value="380" />

                </td>
            </tr>
        </table>
        <div style="display: none">
            <asp:Button ID="btnCrea" runat="server" Text="CREA CDP" ToolTip="Crea il cdp per le righe selezionate" />
        </div>
    </form>
    <script language="javascript" type="text/javascript">
        // document.getElementById('caricamento').style.display = 'none';
        window.onresize = setDimensioni;
        Sys.Application.add_load(setDimensioni);



    </script>
</body>
</html>
