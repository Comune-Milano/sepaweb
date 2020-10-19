<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestCambioFornitura.aspx.vb"
    Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_GestCambioFornitura" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Duplicazione POD con nuovo fornitore</title>
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <script src="../../../StandardTelerik/Scripts/modalTelerik.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server" style="height: 450px; width: 370px; font-family: Arial; font-size: 10pt;">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server"></telerik:RadScriptManager>
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" DecoratedControls="Buttons"></telerik:RadFormDecorator>
        <telerik:RadWindowManager ID="RadWindowManager1" runat="server"></telerik:RadWindowManager>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="Panel1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Skin="Web20">
        </telerik:RadAjaxLoadingPanel>
        <asp:Panel runat="server" ID="Panel1" Width="450px">
            <table border="0" cellpadding="2" cellspacing="2" width="350px">
                <tr>
                    <td style="width: 370px">Fornitura
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadComboBox ID="RadComboBoxFornitura" runat="server" IsCaseSensitive="false"
                            Filter="Contains" AutoPostBack="true" Width="300px">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td>Fornitore da disattivare
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadComboBox ID="RadComboBoxFornitoreOld" runat="server" IsCaseSensitive="false"
                            Filter="Contains" AutoPostBack="true" Width="300px">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td>Fornitore da inserire
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadComboBox ID="RadComboBoxFornitoreNew" runat="server" IsCaseSensitive="false"
                            Filter="Contains" AutoPostBack="false" Width="300px">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <%--<tr>
                    <td>Data di decorrenza nuova fornitura
                    </td>
                </tr>
                <tr>
                    <td>
                        <telerik:RadDatePicker ID="TextBoxDataDecorrenza" runat="server" WrapperTableCaption=""
                            MaxDate="01/01/9999" DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}"
                            Width="110">
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
                        </telerik:RadDatePicker>
                    </td>
                </tr>--%>
                <tr>
                    <td style="height: 150px;">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right; width: 100%">
                        <table width="50%">
                            <tr>
                                <td>
                                    <asp:Button runat="server" ID="RadButtonModifica" Text="Modifica Fornitore"></asp:Button>
                                </td>
                                <td>
                                    <asp:Button runat="server" ID="RadButtonEsci" Text="Esci" OnClientClick="CancelEdit(); return false;"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </form>
</body>
</html>
