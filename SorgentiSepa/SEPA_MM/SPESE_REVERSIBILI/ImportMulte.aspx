<%@ Page Title="" Language="VB" MasterPageFile="~/SPESE_REVERSIBILI/HomePage.master"
    AutoEventWireup="false" CodeFile="ImportMulte.aspx.vb" Inherits="SPESE_REVERSIBILI_ImportMulte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script language="javascript" type="text/javascript">
        function Conferma() {
            if (document.getElementById('cmbCriterioRipartizione').value == ''
                || document.getElementById('txtDataInizio').value == '' || document.getElementById('txtDataFine').value == '') {
                message('Attenzione', 'Valorizzare tutti i campi!');
                return false;
            }
            else {
                return true;
            };
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok" ClientIDMode="Static"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <table>
        <tr>
            <td>
                <table border="0" cellpadding="2" cellspacing="2">
                    <tr>
                        <td style="vertical-align: middle; text-align: center">
                            <img src="../Images/Telerik/Information-icon.png" alt="info" height="16" width="16" />
                        </td>
                        <td style="vertical-align: middle">
                            <asp:Label ID="LabelJolly" runat="server" class="TitoloH1"><i>L'operazione di importazione cancella preventivamente le precedenti multe importate nel prospetto</i></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td style="width: 10%">Criterio di ripartizione
                        </td>
                        <td>
                            <telerik:RadComboBox ID="cmbCriterioRipartizione" Width="30%" ClientIDMode="Static"
                                Filter="Contains" runat="server" AutoPostBack="false">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 10%">Data Inizio
                        </td>
                        <td>
                            <table cellpadding="0" cellspacing="0">
                                <tr>
                                    <td style="width: 10%">
                                        <telerik:RadDatePicker ID="txtDataInizio" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                            ClientIDMode="Static" DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}"
                                            Width="110">
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
                                    <td style="width: 6%">Data Fine
                                    </td>
                                    <td>
                                        <telerik:RadDatePicker ID="txtDataFine" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                            ClientIDMode="Static" DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}"
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
                                            <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                        </telerik:RadDatePicker>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <telerik:RadNotification ID="RadNotificationNote" runat="server" Height="140px" Width="400"
        Animation="Fade" EnableRoundedCorners="true" EnableShadow="true" AutoCloseDelay="3600"
        Position="BottomRight" OffsetX="-30" OffsetY="-70" ShowCloseButton="true">
    </telerik:RadNotification>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder2" runat="Server">
    <asp:Button ID="btnImporta" runat="server" Text="Importa" OnClientClick="if (!Conferma()){return false;};" />
    <asp:Button ID="Button1" runat="server" Text="Esci" OnClientClick="tornaHome();return false;" />
</asp:Content>
