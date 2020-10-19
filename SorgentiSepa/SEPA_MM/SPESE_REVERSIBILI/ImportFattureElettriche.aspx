<%@ Page Title="" Language="VB" MasterPageFile="~/SPESE_REVERSIBILI/HomePage.master" AutoEventWireup="false" CodeFile="ImportFattureElettriche.aspx.vb" Inherits="SPESE_REVERSIBILI_ImportFattureElettriche" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" language="javascript">
        window.onresize = setDimensioni;

        function selezionaCheckSingolo(id) {
            //alert(itemindex + " flag:" + flanalisicorpo.toString() + " perc:" + perccorpo.toString());
            var s = document.getElementById("HiddenIdEdificio").value;

            var idConfronto = "ID = " + id.toString();
            if (s.indexOf(idConfronto) !== -1) {
                s = s.replace("(ID = " + id.toString() + ") OR ", "");
            } else {
                s += "(ID = " + id + ") OR ";
            }
            //stringa dovrebbe esse 'ID = 1 OR ID = 2 OR etc' 
            document.getElementById("HiddenIdEdificio").value = s;
            //alert(s);
        };

        function selezionaTutti(a) {
            //            if (sender._checked)
            //                document.getElementById('hiddenSelTutti').value = "1";
            //            else
            //                document.getElementById('hiddenSelTutti').value = "0";
            if (a.checked)
                document.getElementById('hiddenSelTutti').value = "1"
            else
                document.getElementById('hiddenSelTutti').value = "0"
            document.getElementById('btnSelTutti').click();
        };


        function Conferma() {
            if (document.getElementById('cmbCriterioRipartizioneForzaMotrice').value == ''
                || document.getElementById('txtDataInizioForzaMotrice').value == '' || document.getElementById('txtDataFineForzaMotrice').value == ''
                || document.getElementById('cmbCriterioRipartizioneLuce').value == ''
                || document.getElementById('txtDataInizioLuce').value == '' || document.getElementById('txtDataFineLuce').value == ''
                || document.getElementById('txtDivisioneForzaMotrice').value == '' || document.getElementById('txtDivisioneLuce').value == '') {
                message('Attenzione', 'Valorizzare tutti i campi!');
                return false;
            }
            else {
                if (parseInt(document.getElementById('txtDivisioneForzaMotrice').value) + parseInt(document.getElementById('txtDivisioneLuce').value) != 100) {
                    message('Attenzione', 'La somma delle percentuali deve essere pari a 100!');
                    return false;
                } else
                    return true;
            };
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok" ClientIDMode="Static"
        Localization-Cancel="Annulla" />
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0">
    </telerik:RadAjaxLoadingPanel>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="PanelEdifici">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="PanelEdifici" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <table>
    <tr>
            <td colspan="2">
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td style="vertical-align: middle;text-align:center">
                            <img src="../Images/Telerik/Information-icon.png" alt="info" height="16" width="16" />
                        </td>
                        <td style="vertical-align: middle">
                            <asp:Label ID="LabelJolly" runat="server" class="TitoloH1"><i>L'operazione di importazione cancella preventivamente le precedenti fatture elettriche importate nel prospetto</i></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 50%">
                <fieldset>
                    <legend>&nbsp;&nbsp;<strong>Forza motrice ascensori</strong>&nbsp;&nbsp;</legend>
                    <table>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td style="width: 20%">Criterio di ripartizione
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbCriterioRipartizioneForzaMotrice" Width="50%" AppendDataBoundItems="true"
                                                ClientIDMode="Static" Filter="Contains" runat="server" AutoPostBack="false" ResolvedRenderMode="Classic"
                                                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%">Data Inizio</td>
                                        <td>
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>

                                                    <td style="width: 10%">
                                                        <telerik:RadDatePicker ID="txtDataInizioForzaMotrice" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                                            DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}" Width="110" ClientIDMode="Static">
                                                            <DateInput ID="DateInput2" runat="server" EmptyMessage="gg/mm/aaaa">
                                                                <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                                            </DateInput>
                                                            <Calendar ID="Calendar2" runat="server">
                                                                <SpecialDays>
                                                                    <telerik:RadCalendarDay Repeatable="Today">
                                                                        <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                                                    </telerik:RadCalendarDay>
                                                                </SpecialDays>
                                                            </Calendar>
                                                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                        </telerik:RadDatePicker>
                                                    </td>
                                                    <td style="width: 13%">Data Fine</td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="txtDataFineForzaMotrice" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                                            DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}" Width="110" ClientIDMode="Static">
                                                            <DateInput ID="DateInput1" runat="server" EmptyMessage="gg/mm/aaaa">
                                                                <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                                            </DateInput>
                                                            <Calendar ID="Calendar1" runat="server">
                                                                <SpecialDays>
                                                                    <telerik:RadCalendarDay Repeatable="Today">
                                                                        <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                                                    </telerik:RadCalendarDay>
                                                                </SpecialDays>
                                                            </Calendar>
                                                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                        </telerik:RadDatePicker>
                                                    </td>
                                                </tr>

                                            </table>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>% di suddivisione</td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtDivisioneForzaMotrice" ClientIDMode="Static" runat="server" DisabledStyle-HorizontalAlign="Right" DisplayText="30" Enabled="false" EnabledStyle-HorizontalAlign="Right" NumberFormat-DecimalDigits="0" MinValue="0" Width="50"></telerik:RadNumericTextBox></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>

                    </table>
                </fieldset>
            </td>

            <td style="width: 50%">
                <fieldset>
                    <legend>&nbsp;&nbsp;<strong>Luce parti comuni</strong>&nbsp;&nbsp;</legend>
                    <table>
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td style="width: 20%">Criterio di ripartizione
                                        </td>
                                        <td>
                                            <telerik:RadComboBox ID="cmbCriterioRipartizioneLuce" Width="50%" AppendDataBoundItems="true"
                                                ClientIDMode="Static" Filter="Contains" runat="server" AutoPostBack="false" ResolvedRenderMode="Classic"
                                                HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                                            </telerik:RadComboBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 10%">Data Inizio</td>
                                        <td>
                                            <table cellpadding="0" cellspacing="0">
                                                <tr>

                                                    <td style="width: 10%">
                                                        <telerik:RadDatePicker ID="txtDataInizioLuce" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                                            DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}" Width="110" ClientIDMode="Static">
                                                            <DateInput ID="DateInput3" runat="server" EmptyMessage="gg/mm/aaaa">
                                                                <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                                            </DateInput>
                                                            <Calendar ID="Calendar3" runat="server">
                                                                <SpecialDays>
                                                                    <telerik:RadCalendarDay Repeatable="Today">
                                                                        <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                                                    </telerik:RadCalendarDay>
                                                                </SpecialDays>
                                                            </Calendar>
                                                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                        </telerik:RadDatePicker>
                                                    </td>
                                                    <td style="width: 13%">Data Fine</td>
                                                    <td>
                                                        <telerik:RadDatePicker ID="txtDataFineLuce" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                                            DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}" Width="110" ClientIDMode="Static">
                                                            <DateInput ID="DateInput4" runat="server" EmptyMessage="gg/mm/aaaa">
                                                                <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                                            </DateInput>
                                                            <Calendar ID="Calendar4" runat="server">
                                                                <SpecialDays>
                                                                    <telerik:RadCalendarDay Repeatable="Today">
                                                                        <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                                                    </telerik:RadCalendarDay>
                                                                </SpecialDays>
                                                            </Calendar>
                                                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                                        </telerik:RadDatePicker>
                                                    </td>
                                                </tr>

                                            </table>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td>% di suddivisione</td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="txtDivisioneLuce" runat="server" ClientIDMode="Static" DisabledStyle-HorizontalAlign="Right" DisplayText="70" Enabled="false" EnabledStyle-HorizontalAlign="Right" NumberFormat-DecimalDigits="0" MinValue="0" Width="50"></telerik:RadNumericTextBox></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </fieldset>
            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2" style="font-size: 10pt; text-align: center">
                <strong>ELENCO COMPLESSI CON SUDDIVISIONE 90-10</strong>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="font-size: 10pt">
                <asp:Panel runat="server" ID="PanelEdifici">
                    <telerik:RadGrid ID="dgvEdifici" runat="server" GroupPanelPosition="Top" ResolvedRenderMode="Classic"
                        AutoGenerateColumns="False" Culture="it-IT" RegisterWithScriptManager="False"
                        PagerStyle-AlwaysVisible="true" AllowPaging="true" AllowFilteringByColumn="true"
                        EnableLinqExpressions="False" Width="97%" AllowSorting="True" IsExporting="False"
                        PageSize="100">
                        <MasterTableView NoMasterRecordsText="Nessun elemento da visualizzare." ShowHeadersWhenNoRecords="true"
                            DataKeyNames="ID" CommandItemDisplay="Top">
                            <CommandItemSettings ShowExportToExcelButton="true" ShowExportToWordButton="false"
                                ShowExportToPdfButton="false" ShowExportToCsvButton="false" ShowAddNewRecordButton="false"
                                ShowRefreshButton="true" />
                            <Columns>
                                <telerik:GridBoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="DENOMINAZIONE" HeaderText="DENOMINAZIONE" AutoPostBackOnFilter="true"
                                    FilterControlWidth="90%" CurrentFilterFunction="Contains" DataFormatString="{0:@}">
                                    <HeaderStyle Font-Bold="false" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" HorizontalAlign="Center" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="CHECK1" HeaderText="CHECK1" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridTemplateColumn DataField="CHECK1" HeaderText="90-10" AllowFiltering="false">
                                    <HeaderStyle Width="50px" HorizontalAlign="Center" />
                                    <ItemStyle Width="50px" HorizontalAlign="Center" />
                                    <ItemTemplate>
                                        <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# DataBinder.Eval(Container, "DataItem.CHECK1") %>' />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="headerChkbox" runat="server" OnCheckedChanged="headerChkbox_CheckedChanged" AutoPostBack="true" />
                                    </HeaderTemplate>
                                </telerik:GridTemplateColumn>
                            </Columns>
                            <PagerStyle AlwaysVisible="True" />
                            <CommandItemTemplate>
                                <div style="display: inline-block; width: 100%;">
                                    <table style="width: 100%">
                                        <tr>
                                            <td>
                                                <div style="float: right; padding: 4px;">
                                                    <asp:Button ID="ButtonRefresh" runat="server" OnClick="Refresh_Click" CommandName="Refresh" OnClientClick="caricamento(2);"
                                                        CssClass="rgRefresh" /><asp:Button ID="ButtonExportExcel" Text="text" runat="server" OnClientClick="caricamento(2);"
                                                            OnClick="Esporta_Click" CommandName="ExportToExcel" CssClass="rgExpXLS" />
                                                </div>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </CommandItemTemplate>
                        </MasterTableView><GroupingSettings CollapseAllTooltip="Collapse all groups" />
                        <ExportSettings FileName="Export" IgnorePaging="True" OpenInNewWindow="True">
                            <Excel FileExtension="xls" Format="Xlsx" />
                        </ExportSettings>
                        <ClientSettings EnableRowHoverStyle="true" AllowColumnsReorder="false" ReorderColumnsOnClient="true" ClientEvents-OnCommand="onCommand">
                            <Scrolling AllowScroll="true" UseStaticHeaders="True" />
                            <Selecting AllowRowSelect="True" />
                            <Resizing AllowColumnResize="true" ResizeGridOnColumnResize="false" EnableRealTimeResize="false"
                                AllowResizeToFit="true" />
                        </ClientSettings>
                        <PagerStyle AlwaysVisible="True" />
                    </telerik:RadGrid>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <telerik:RadNotification ID="RadNotificationNote" runat="server" Height="140px" Width="400" Animation="Fade"
        EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="3600" Position="BottomRight"
        OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
    </telerik:RadNotification>
    <asp:HiddenField ID="HFGriglia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFHeightGriglia" runat="server" Value="380" ClientIDMode="Static" />
    <asp:HiddenField ID="HiddenIdEdificio" runat="server" ClientIDMode="Static" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:Button ID="btnImporta" runat="server" Text="Importa" OnClientClick="if (!Conferma()){return false;};" />
    <asp:Button ID="Button1" runat="server" Text="Esci" OnClientClick="tornaHome();return false;" />
</asp:Content>

