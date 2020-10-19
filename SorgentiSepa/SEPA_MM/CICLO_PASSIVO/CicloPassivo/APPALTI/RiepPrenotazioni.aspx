<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RiepPrenotazioni.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_APPALTI_RiepPrenotazioni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <base target="_self" />
    <title>Prenotazioni a canone</title>
    <style type="text/css">
        .style1
        {
            width: 707px;
            text-align: right;
        }
        #form1
        {
            width: 795px;
            height: 365px;
        }
        .style3
        {
            width: 707px;
            text-align: right;
        }
        .style4
        {
            width: 707px;
            text-align: right;
            font-family: Arial;
            font-weight: bold;
            color: #0000FF;
        }
        .style5
        {
            font-family: Arial;
            font-size: 9pt;
            font-weight: bold;
        }
        .style6
        {
            font-family: Arial;
            font-size: 8pt;
        }
        .style7
        {
            width: 49px;
        }
    </style>
    <link rel="shortcut icon" href="../../../favicon.ico" type="image/x-icon" />
    <link rel="icon" href="../../../favicon.ico" type="image/x-icon" />
    <script type="text/javascript" language="javascript">
        window.name = "modal";


        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\-\,]/g
        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
            //        o.value = o.value.replace('.', ',');
            //document.getElementById('txtModificato').value = '1';

        }

        function AutoDecimal2(obj) {
            obj.value = obj.value.replace('.', '');
            if (obj.value.replace(',', '.') != 0) {
                var a = obj.value.replace(',', '.');
                a = parseFloat(a).toFixed(2)
                if (a.substring(a.length - 3, 0).length >= 4) {
                    var decimali = a.substring(a.length, a.length - 2);
                    var dascrivere = a.substring(a.length - 3, 0);
                    var risultato = '';
                    while (dascrivere.replace('-', '').length >= 4) {

                        risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato
                        dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
                    }
                    risultato = dascrivere + risultato + ',' + decimali
                    //document.getElementById(obj.id).value = a.replace('.', ',')
                    document.getElementById(obj.id).value = risultato
                }
                else {
                    document.getElementById(obj.id).value = a.replace('.', ',')
                }

            }
        }

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

        function ConfermElimina() {
            if (document.getElementById('idPrenotazione').value != 0) {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Verrà eliminata una scadenza!Continuare l\'operazione?");
                if (chiediConferma == true) {
                    document.getElementById('ConfElimina').value = '1';

                }
            }
            else { alert('Selezionare la prenotazione da eliminare!'); }
        }
        function ConfEsci() {
            if (document.getElementById('Modificato').value == '1') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Stai per uscire senza aver salvato le modifiche apportate!\nContinuare l\'operazione?");
                if (chiediConferma == true) {
                    self.close();
                    CancelEdit();
                }
            }
            else {
                if (document.getElementById('HiddenAttiva').value == 1) {
                    closeWinAndAttivaContratto('btnTerminaAttivazione');
                }
                else {
                    self.close();
                    CancelEdit();
                };
            }

        }

        function SostPuntVirg(e, obj) {

            var keyPressed;
            keypressed = (window.event) ? event.keyCode : e.which;
            if (keypressed == 110 || keypressed == 190) {
                if (navigator.appName == 'Microsoft Internet Explorer') {
                    event.keyCode = 0;
                }
                else {
                    e.preventDefault();
                };
                obj.value += ',';
                obj.value = obj.value.replace('.', '');
            };
        };
        //        function NuovaIva() {
        //            var data = ''
        //            data = document.getElementById('txtScadenza').value.substring(6, 10) + document.getElementById('txtScadenza').value.substring(3, 5) + document.getElementById('txtScadenza').value.substring(0, 2)
        //            if (data >= '20131001') {

        //                document.getElementById('txtIvaVoce').value = document.getElementById('txtIvaVoce').value + ' nuova aliquota 22%';
        //            }

        //            else {
        //                if (document.getElementById('txtIvaVoce').value.indexOf('nuova') > 0) {
        //                    document.getElementById('txtIvaVoce').value = document.getElementById('txtIvaVoce').value.substring(0, document.getElementById('txtIvaVoce').value.indexOf('nuova'));

        //                }

        //            }

        //        }
    </script>
</head>
<body class="sfondo">
    <!-- Da mettere subito dopo l'apertura del tag <body> -->
    <div id="splash" style="border: thin dashed #000066; position: absolute; z-index: 500;
        text-align: center; font-size: 10px; width: 760px; height: 300px; visibility: hidden;
        vertical-align: top; line-height: normal; top: 10px; left: 12px; background-color: #FFFFFF;">
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <img src='../../../CONDOMINI/Immagini/load.gif' alt='caricamento in corso' /><br />
        <br />
        caricamento in corso...<br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        &nbsp;
    </div>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
         <telerik:RadFormDecorator Skin="Web20" ID="FormDecorator1" runat="server" DecoratedControls="Buttons" />
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="dgvScadenze">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="dgvScadenze" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Web20">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <table style="width: 98%; position: absolute; top: 23px; left: 12px;">
        <tr>
            <td class="TitoloH1" colspan="2">
                Riepilogo prenotazione pagamenti a canone del contratto in base alla voce di servizio
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; text-align: left" width="100%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; text-align: left" width="100%">
                <table>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btnSalva" runat="server" Text="Conferma e salva" ToolTip="Conferma le prenotazioni e salva"
                                Style="top: 0px; left: 1px" />
                        </td>
                        <td>
                            <telerik:RadButton ID="imgAnnulla" runat="server" Text="Chiudi" OnClientClicking="function(sender, args){ConfEsci();}"
                                AutoPostBack="false" ToolTip="Chiudi la finestra" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; text-align: left" width="100%">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; text-align: left" width="100%">
                <telerik:RadComboBox ID="cmbvoceRiepilogo" runat="server" AppendDataBoundItems="true"
                    AutoPostBack="true" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                    ResolvedRenderMode="Classic" Width="100%">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; text-align: left" width="100%">
                <div>
                    <telerik:RadGrid ID="dgvScadenze" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                        AllowFilteringByColumn="false" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                        IsExporting="False" PagerStyle-AlwaysVisible="true">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            CommandItemDisplay="Top">
                            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="true" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID_PRENOTAZIONE" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="ID_APPALTO" HeaderText="ID_APPALTO" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="SCADENZA" HeaderText="SCADENZA">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="PERC_IVA" HeaderText="IVA %">
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn HeaderText="IMPORTO €">
                                    <ItemTemplate>
                                        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                            <asp:TextBox ID="txtImporto" runat="server" Font-Names="Arial" Font-Size="8pt" Style="text-align: right"
                                                Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO_PRENOTATO") %>' Width="85px"></asp:TextBox>
                                        </span></strong>
                                        <asp:Label ID="Label3" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                </telerik:GridTemplateColumn>
                                <telerik:GridBoundColumn DataField="IMPORTO_PRENOTATO" HeaderText="IMPORTO_PRENOTATO"
                                    Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IMPORTO_APPROVATO" HeaderText="IMP. CERTIFICATO €"
                                    DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="IMPORTO_LIQUIDATO" HeaderText="IMP. LIQUIDATO €"
                                    DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                </telerik:GridBoundColumn>
                                <telerik:GridButtonColumn HeaderStyle-Width="3%" CommandName="Delete" ImageUrl="../Immagini/Delete.gif"
                                    Text="Elimina" UniqueName="DeleteColumn" ButtonType="ImageButton" ButtonCssClass="rgDel">
                                    <ItemStyle HorizontalAlign="Center" />
                                    <HeaderStyle HorizontalAlign="Center" Width="24px" Height="24px" />
                                </telerik:GridButtonColumn>
                            </Columns>
                            <CommandItemTemplate>
                                <a id="addServizio" style="cursor: pointer" onclick="document.getElementById('DivInsScadenza').style.visibility='visible'">
                                    <img style="border: 0px" alt="" src="../../Immagini/addRecord.gif" />
                                    Aggiungi nuovo record</a>
                            </CommandItemTemplate>
                            <PagerStyle AlwaysVisible="True"></PagerStyle>
                        </MasterTableView>
                        <GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                            <Excel FileExtension="xls" Format="Xlsx" />
                        </ExportSettings>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true"
                            ClientEvents-OnCommand="onCommand">
                            <ClientEvents OnCommand="onCommand"></ClientEvents>
                            <Scrolling AllowScroll="True" UseStaticHeaders="True" />
                            <Selecting AllowRowSelect="True" />
                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                AllowResizeToFit="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="True"></PagerStyle>
                    </telerik:RadGrid>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table width="98%">
                    <tr>
                        <td align="right">
                            <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                ForeColor="Red" Height="18px" Style="z-index: 104;" Visible="False" Width="422px"></asp:Label>
                            <table>
                                <tr>
                                    <td class="style5">
                                        TOTALE €.
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTotale" runat="server" Width="85px" Font-Bold="True" Font-Names="Arial"
                                            Font-Size="8pt" ReadOnly="True" Style="text-align: right"></asp:TextBox>
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
                            <table width="100%">
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                                            Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" MaxLength="60"
                                            ReadOnly="True" Style="left: 13px; top: 197px" Width="524px">Nessuna Selezione</asp:TextBox>
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
                </table>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style3">
                &nbsp;
            </td>
            <td class="style3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style1">
                &nbsp;
            </td>
            <td class="style1">
                &nbsp;
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="HiddenAttiva" runat="server" Value="" />
    <asp:HiddenField ID="ScadSelected" runat="server" Value="0" />
    <asp:HiddenField ID="ConfElimina" runat="server" Value="0" />
    <asp:HiddenField ID="VisibilitaDiv" runat="server" Value="0" />
    <asp:HiddenField ID="ImpMassimo" runat="server" Value="0" />
    <asp:HiddenField ID="IdStato" runat="server" Value="0" />
    <asp:HiddenField ID="idPrenotazione" runat="server" Value="0" />
    <asp:HiddenField ID="Modificato" runat="server" Value="0" />
    <div id="DivInsScadenza" style="border: thin solid #0000FF; position: absolute; top: 50px;
        left: 9px; height: 242px; width: 768px; background-color: #FFFFFF; visibility: hidden;">
        <table style="width: 100%;">
            <tr>
                <td class="style6">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style6">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style6">
                    Voce di servizio
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadComboBox ID="cmbvoce" runat="server" AppendDataBoundItems="true" AutoPostBack="true"
                        Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                        ResolvedRenderMode="Classic" Width="100%">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="Label1" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Scadenza"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtScadenza" runat="server" Width="75px" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" Style="text-align: right"></asp:TextBox>
                            </td>
                            <td>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtScadenza"
                                    ErrorMessage="!" Font-Bold="True" Font-Names="arial" Font-Size="8pt" Style="z-index: 150;
                                    left: 604px; top: 53px" TabIndex="100" ToolTip="Inserire una data valida" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>
                            </td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Font-Names="Arial" Font-Size="8pt" Text="Importo (iva compresa) €."></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtImporto" runat="server" Width="85px" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" Style="text-align: right"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <table>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td class="style7">
                                <asp:Button ID="btn_InserisciAppalti" runat="server" text="Salva"
                                    Style="cursor: pointer" TabIndex="55" ToolTip="Salva" />
                            </td>
                            <td>
                                <asp:Button ID="btn_ChiudiAppalti" runat="server" text="Esci"
                                    OnClientClick="document.getElementById('DivInsScadenza').style.visibility='hidden'"
                                    Style="cursor: pointer" TabIndex="57" ToolTip="Esci senza inserire" />
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
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
        function GetRadWindow() {
            var oWindow = null;
            if (window.radWindow) {
                oWindow = window.radWindow;
            } else {
                if (window.frameElement) {
                    if (window.frameElement.radWindow) {
                        oWindow = window.frameElement.radWindow;
                    };
                };
            };
            return oWindow;
        };
        function CancelEdit() {
            if (GetRadWindow()) {
                GetRadWindow().close();
            };
        };
    </script>
</body>
<script type="text/javascript" language="javascript">
    if (document.getElementById("IdStato").value == 0) {

        if (document.getElementById("imgAddScadenza"))
            document.getElementById("imgAddScadenza").style.visibility = 'hidden'
    }
    document.getElementById('splash').style.visibility = 'hidden';

</script>
</html>
