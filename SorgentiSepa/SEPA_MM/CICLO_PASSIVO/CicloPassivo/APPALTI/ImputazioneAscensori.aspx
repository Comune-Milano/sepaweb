<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ImputazioneAscensori.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_APPALTI_ImputazioneAscensori" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <script src="../../../StandardTelerik/Scripts/modalTelerik.js" type="text/javascript"></script>
    <title>Imputazione ascensori</title>
    <style type="text/css">
        .style1 {
            width: 707px;
            text-align: right;
        }

        #form1 {
            width: 795px;
            height: 365px;
        }

        .style3 {
            width: 707px;
            text-align: right;
        }

        .style4 {
            width: 707px;
            text-align: right;
            font-family: Arial;
            font-weight: bold;
            color: #0000FF;
        }

        .style5 {
            font-family: Arial;
            font-size: 9pt;
            font-weight: bold;
        }

        .style6 {
            font-family: Arial;
            font-size: 8pt;
        }

        .style7 {
            width: 49px;
        }
    </style>
    <link rel="shortcut icon" href="../../../favicon.ico" type="image/x-icon" />
    <link rel="icon" href="../../../favicon.ico" type="image/x-icon" />
    <script type="text/javascript" language="javascript">


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


        function VisSchedaImputazione() {
            // window.open( 'Scheda di imputazione', 'height=700,top=0,left=0,width=1300,scrollbars=no,resizable=yes');
            if (document.getElementById('idPrenotazione').value == '0') {
                apriAlert('Selezionare una rata!', 300, 150, 'Attenzione', null, null);

            } else {
                openModalInRad('RadWindowSchedeImputazione', '../../../SPESE_REVERSIBILI/ImputazioneAscensore.aspx?HiddenIdContratto=' + document.getElementById("HiddenIdContratto").value + '&IDPRENOTAZIONE=' + document.getElementById("idPrenotazione").value + '&HIDDENPIANO=' + document.getElementById("HiddenEsercizio").value, 500, 500, null, null, null, 1);
            };
        };


    </script>
</head>
<body class="sfondo">
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
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="btnAggiorna">
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
        <telerik:RadWindow ID="RadWindow1" runat="server" CenterIfModal="true" Modal="True" RestrictionZoneID="divRest"
            VisibleStatusbar="False" AutoSize="True" Behavior="Pin, Move, Resize" Skin="Web20">
        </telerik:RadWindow>
        <telerik:RadWindow ID="RadWindowSchedeImputazione" runat="server" CenterIfModal="true" Modal="True"
            VisibleStatusbar="False" Behavior="Pin, Move, Resize, Maximize" Width="800px" Title="Imputazione ascensori"
            Height="600px" ShowContentDuringLoad="false">
        </telerik:RadWindow>

        <table style="width: 98%; position: absolute; top: 23px; left: 12px;">
            <tr>
                <td class="TitoloH1" colspan="2">Riepilogo prenotazione pagamenti a canone del contratto in base alla voce di servizio
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: left" width="100%">&nbsp;
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: left" width="100%">
                    <table>
                        <tr>
                            <td>
                                <telerik:RadButton ID="btnSchedaImputazione" runat="server" Text="Scheda di imputazione" ToolTip="Scheda di imputazione" AutoPostBack="false"
                                    Style="top: 0px; left: 1px" OnClientClicking="function(sender,argas){VisSchedaImputazione()}" />
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
                <td style="vertical-align: top; text-align: left" width="100%">&nbsp;
                </td>
            </tr>
            <tr>
                <td style="vertical-align: top; text-align: left" width="100%">
                    <div>
                        <telerik:RadGrid ID="dgvScadenze" runat="server" GroupPanelPosition="Top"
                            AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False" ShowFooter="True" FooterStyle-Font-Bold="true"
                            FooterStyle-HorizontalAlign="Right"
                            AllowFilteringByColumn="false" EnableLinqExpressions="False" Width="99%" AllowSorting="True"
                            IsExporting="False" PagerStyle-AlwaysVisible="true">
                            <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true">
                                <ColumnGroups>
                                    <telerik:GridColumnGroup Name="Manutenzioni" HeaderText="Manutenzioni in abbonamento"
                                        HeaderStyle-HorizontalAlign="Center" />
                                    <telerik:GridColumnGroup Name="Scadenza" HeaderText="Rata"
                                        HeaderStyle-HorizontalAlign="Center" />
                                </ColumnGroups>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="ID_APPALTO" HeaderText="ID_APPALTO" Visible="False" ColumnGroupName="Scadenza">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="TIPO_PAGAMENTO" HeaderText="ID_APPALTO" Visible="False" ColumnGroupName="Scadenza">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="rownum" HeaderText="#" ColumnGroupName="Scadenza">
                                    </telerik:GridBoundColumn>

                                    <telerik:GridDateTimeColumn DataField="SCADENZA" HeaderText="SCADENZA" ColumnGroupName="Scadenza"
                                        DataFormatString="{0:dd/MM/yyyy}"
                                        AutoPostBackOnFilter="true" Visible="true">
                                        <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                        <ItemStyle HorizontalAlign="Center" Wrap="false" />
                                    </telerik:GridDateTimeColumn>

                                    <telerik:GridBoundColumn DataField="IMPORTO_PRENOTATO1" HeaderText="PRENOTATO" Aggregate="Sum"
                                        DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" ColumnGroupName="Manutenzioni">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ID1"
                                        Visible="False" ColumnGroupName="Manutenzioni">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IMPORTO_APPROVATO1" HeaderText="CERTIFICATO" Aggregate="Sum"
                                        DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" ColumnGroupName="Manutenzioni">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IMPORTO_LIQUIDATO1" HeaderText="LIQUIDATO" Aggregate="Sum"
                                        DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" ColumnGroupName="Manutenzioni">
                                    </telerik:GridBoundColumn>


                                    <%--<telerik:GridBoundColumn DataField="IMPORTO_PRENOTATO2" HeaderText="PRENOTATO" Aggregate="Sum"
                                        DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ID2"
                                        Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IMPORTO_APPROVATO2" HeaderText="IMP. CERTIFICATO" Aggregate="Sum"
                                        DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IMPORTO_LIQUIDATO2" HeaderText="IMP. LIQUIDATO" Aggregate="Sum"
                                        DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                    </telerik:GridBoundColumn>--%>

                                    <%--                                    <telerik:GridBoundColumn DataField="IMPORTO_PRENOTATO3" HeaderText="PRENOTATO" Aggregate="Sum"
                                        DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ID3"
                                        Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IMPORTO_APPROVATO3" HeaderText="IMP. CERTIFICATO" Aggregate="Sum"
                                        DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IMPORTO_LIQUIDATO3" HeaderText="IMP. LIQUIDATO" Aggregate="Sum"
                                        DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                    </telerik:GridBoundColumn>


                                    <telerik:GridBoundColumn DataField="IMPORTO_PRENOTATO4" HeaderText="PRENOTATO" Aggregate="Sum"
                                        DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ID4"
                                        Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IMPORTO_APPROVATO4" HeaderText="IMP. CERTIFICATO" Aggregate="Sum"
                                        DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IMPORTO_LIQUIDATO4" HeaderText="IMP. LIQUIDATO" Aggregate="Sum"
                                        DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                    </telerik:GridBoundColumn>


                                    <telerik:GridBoundColumn DataField="IMPORTO_PRENOTATO5" HeaderText="PRENOTATO" Aggregate="Sum"
                                        DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="ID5"
                                        Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IMPORTO_APPROVATO5" HeaderText="IMP. CERTIFICATO" Aggregate="Sum"
                                        DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="IMPORTO_LIQUIDATO5" HeaderText="IMP. LIQUIDATO" Aggregate="Sum"
                                        DataFormatString="{0:C2}" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridButtonColumn DataTextField="SCHEDA_IMPUTAZIONE" DataTextFormatString="Scheda di imputazione" ButtonType="LinkButton"
                                        UniqueName="SchedaImputazione" HeaderText="SCHEDA DI IMPUTAZIONE" CommandName="SchedaImputazione" HeaderStyle-HorizontalAlign="Center"
                                        ItemStyle-HorizontalAlign="Center">
                                    </telerik:GridButtonColumn>  --%>
                                </Columns>
                                <SortExpressions>
                                    <telerik:GridSortExpression FieldName="SCADENZA" SortOrder="ASCENDING" />
                                </SortExpressions>
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
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td class="style3">&nbsp;
                </td>
                <td class="style3">&nbsp;
                </td>
            </tr>
            <tr>
                <td class="style1">&nbsp;
                </td>
                <td class="style1">&nbsp;
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
        <asp:HiddenField ID="HiddenEsercizio" runat="server" ClientIDMode="Static" Value="" />
        <asp:HiddenField ID="Modificato" runat="server" Value="0" />


        <asp:HiddenField ID="HiddenIdContratto" runat="server" ClientIDMode="Static" Value="" />
        <asp:HiddenField ID="HFGriglia" runat="server" />
        <asp:Button ID="btnAggiorna" runat="server" ClientIDMode="Static" CssClass="nascondi" />
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


        window.onresize = setDimensioni;
        Sys.Application.add_load(setDimensioni);
</script>
</html>
