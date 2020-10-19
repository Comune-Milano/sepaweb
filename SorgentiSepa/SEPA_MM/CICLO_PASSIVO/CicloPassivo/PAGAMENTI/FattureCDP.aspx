<%@ Page Language="VB" AutoEventWireup="false" CodeFile="FattureCDP.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_FattureCDP" %>

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
            height: 22px;
            cursor: pointer;
        }
    </style>
    <script language="javascript" type="text/javascript">
        var Selezionato;
       
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
            window.showModalDialog('FatturePagaUt.aspx', 'window', 'status:no;dialogWidth:800px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');
        };


        window.onresize = setDimensioni;
        

    </script>
</head>
<body class="sfondo">
   
    <form id="form1" runat="server" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <table style="width: 100%;" class="FontTelerik">
        <tr>
            <td class="TitoloModulo">
                <asp:Label ID="lblTitolo" runat="server" Text="Fatture Utenze"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadButton ID="btnAnnulla" runat="server" Text="Esci" ToolTip="Home" />
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td width="70%&gt;">
                            <fieldset style="border: thin solid #99CCFF">
                                <legend><strong style="text-align: left">
                                    <table>
                                        <tr>
                                            <td>
                                                FILTRI
                                            </td>
                                        </tr>
                                    </table>
                                </strong></legend>
                                <table>
                                    <tr>
                                        <td>
                                            <table style="border: thin solid #009900;">
                                                <tr>
                                                    <td colspan="3">
                                                        Data Emissione
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtEmDal" runat="server" Width="70px" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        al
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtEmAl" runat="server" Width="70px" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table style="border: thin solid #CC3300;">
                                                <tr>
                                                    <td colspan="3">
                                                        Data Scadenza
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtScadDal" runat="server" Width="70px" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        al
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtScadAl" runat="server" Width="70px" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table style="border: thin solid #3399FF;">
                                                <tr>
                                                    <td colspan="1">
                                                        POD
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtPOD" runat="server" Width="150px" MaxLength="50"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table style="border: thin solid #000000;">
                                                <tr>
                                                    <td colspan="1">
                                                        Fattura
                                                    </td>
                                                    <td>
                                                        Anno
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtNumFattura" runat="server" Width="100px" MaxLength="20"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtAnno" runat="server" Width="40px" MaxLength="4"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/CICLO_PASSIVO/Immagini/search-icon.png"
                                                 />
                                        </td>
                                        <td class="style5">
                                            &nbsp;
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="btnPulisci" runat="server" ImageUrl="~/CICLO_PASSIVO/Immagini/broom-icon.png"
                                                />
                                        </td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td>
                                            <telerik:RadButton ID="btnExportXls" runat="server" Text="Esporta XLS" ToolTip="Esporta in formato xls"
                                                Style="top: 0px; left: 0px" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table style="border: thin solid #CC33FF;">
                                                <tr>
                                                    <td colspan="3">
                                                        Data caricamento
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtCaricDal" runat="server" Width="70px" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        al
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCaricAl" runat="server" Width="70px" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table style="border: thin solid #CCCC00;">
                                                <tr>
                                                    <td colspan="3">
                                                        Data Inizio Periodo
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtInizioDal" runat="server" Width="70px" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        al
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtInizioAl" runat="server" Width="70px" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <table style="border: thin solid #FF9933;">
                                                <tr>
                                                    <td colspan="3">
                                                        Data Fine Periodo
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtFinPerDal" runat="server" Width="70px" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        al
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFinPerAl" runat="server" Width="70px" MaxLength="10"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                         <td>
                                            <table style="border: thin solid #FF9933; width:100%">
                                                <tr>
                                                    <td colspan="3" class="style7">
                                                        Esercizio Finanziario</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                       <telerik:RadComboBox ID="EsercizioFinanziario" runat="server" AppendDataBoundItems="true"
                                                             Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                                             Width="100%">
                                                        </telerik:RadComboBox>
                                                    </td>
                                                </tr>
                                            </table></td>
                                        <td>
                                            &nbsp;
                                        </td>
                                        <td class="style5">
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
                                    <tr>
                                        <td colspan="4">
                                            <table style="border: thin solid #FFFF66; width: 100%;">
                                                <tr>
                                                    <td style="text-align: center; font-family: Arial; font-size: 8pt">
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
                                        <td class="style5">
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
                                        <td>
                                            &nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
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
      
    
              <tr>
            <td>
                
               

                <telerik:RadGrid ID="dgvFattUtenze" runat="server" ResolvedRenderMode="Classic" AutoGenerateColumns="False"
                        Culture="it-IT" RegisterWithScriptManager="False" PagerStyle-Visible="true" PagerStyle-AlwaysVisible="true"
                        AllowPaging="true" EnableLinqExpressions="False" Width="99%" AllowSorting="True" HeaderStyle-Width="15%"
                        PageSize="100" IsExporting="False">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            Width="250%">
                            <Columns>
                                <telerik:GridBoundColumn HeaderText="ID" Visible="False" DataField="ID">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DATA_CARICAMENTO" HeaderText="CARICAMENTO">
                                    <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center"  />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left"  />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="RAG_SOCIALE" HeaderText="FORNITORE">
                                    <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center"  />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left"  />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CDP" HeaderText="CDP">
                                    <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center"  />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Left"  />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DATA_EMISSIONE" HeaderText="EMISSIONE">
                                    <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DATA_SCADENZA" HeaderText="SCADENZA">
                                    <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NUMERO_FATTURA" HeaderText="N° FATTURA">
                                    <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ANNO_FATTURA" HeaderText="ANNO">
                                    <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DATA_INIZIO_PERIODO" HeaderText="INIZIO PERIODO">
                                    <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DATA_FINE_PERIODO" HeaderText="FINE PERIODO">
                                    <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="POD" HeaderText="POD">
                                    <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center"  />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NOME_VIA_FORNITURA" HeaderText="VIA FORNITURA">
                                    <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="NUMERO_CIVICO_FORNITURA" HeaderText="CIVICO">
                                    <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="BARRATO_FORNITURA" HeaderText="BARRATO">
                                    <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CAP_FORNITURA" HeaderText="CAP">
                                    <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="LOCALITA_FORNITURA" HeaderText="LOCALITA">
                                    <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PROVINCIA_FORNITURA" HeaderText="PROVINCIA">
                                    <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PIANO" HeaderText="ESERCIZIO">
                                    <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center"  />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" VerticalAlign="Middle"  />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="VOCE_BP" HeaderText="VOCE BP">
                                    <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FORM_TOTALE_ONERI_DIVERSI" HeaderText="ONERI DIVERSI">
                                    <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right"  />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FORM_BASE_IMPONIBILE" HeaderText="IMPONIBILE">
                                    <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right"  />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FORM_IVA" HeaderText="IVA">
                                    <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right"  />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FORM_TOTALE_BOLLETTA" HeaderText="TOTALE BOLLETTA">
                                    <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right"  />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="FORM_TOTALE_BOLLETTINO" HeaderText="TOTALE BOLLETTINO">
                                    <HeaderStyle  Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False"  />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right"  />
                                </telerik:GridBoundColumn>
                            </Columns>
                            <HeaderStyle Wrap="true" HorizontalAlign="Center"  />
                            <ItemStyle Wrap="true" />
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
                
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
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
       

        window.onresize = setDimensioni;
        Sys.Application.add_load(setDimensioni);
    </script>
</body>
</html>
