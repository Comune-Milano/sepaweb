<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_OccupazioneAbusiva.ascx.vb" Inherits="Contratti_Tab_OccupazioneAbusiva" %>
<div style="width: 1130px; position: absolute; top: 168px; height: 520px;">
    <fieldset style="border: 1px solid">
        <legend>&nbsp&nbsp;&nbsp;<asp:Label ID="lblInfoOccupazione" runat="server" Font-Bold="true" Font-Names="Arial" Font-Size="8pt"
            Text="Info occupazione senza titolo"></asp:Label></legend>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblTipoOccupazione" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Text="Tipo occupazione*"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblTipologiaAttoRilevazione" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Text="Tipologia atto rilevazione*"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblIdentificativoRilevazione" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Text="Identificativo atto rilevazione*"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDataAttoRilevazione" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Text="Data atto rilevazione*"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadComboBox ID="cmbTipoOccupazione" Width="300px" AppendDataBoundItems="true"
                        Filter="Contains" runat="server" AutoPostBack="false"
                        HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                    </telerik:RadComboBox>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtTipologiaAttoRilevazione" runat="server" Width="300px" MaxLength="500"></telerik:RadTextBox>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtIdentificativoAtto" runat="server" Width="300px" MaxLength="500"></telerik:RadTextBox>
                </td>
                <td>
                    <telerik:RadDatePicker ID="txtDataAttoRilevazione" runat="server" WrapperTableCaption=""
                        DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}"
                        Width="110" MinDate="01/01/1000" MaxDate="01/01/9999">
                        <DateInput ID="DateInput5" runat="server" EmptyMessage="gg/mm/aaaa">
                            <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                        </DateInput>
                        <Calendar ID="Calendar3" runat="server">
                            <SpecialDays>
                                <telerik:RadCalendarDay Repeatable="Today">
                                    <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                </telerik:RadCalendarDay>
                            </SpecialDays>
                        </Calendar>
                    </telerik:RadDatePicker>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblTipoAttoLegittimante" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Text="Tipo atto legittimante"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblIdentificativoAttoLegittimante" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Text="Identificativo atto legittimante"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDataAttoLegittimante" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Text="Data atto legittimante"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadComboBox ID="cmbTipoAttoLegittimante" Width="300px" AppendDataBoundItems="true"
                        Filter="Contains" runat="server" AutoPostBack="false"
                        HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                    </telerik:RadComboBox>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtIdentificativoAttoLegittimante" runat="server" Width="300px" MaxLength="500"></telerik:RadTextBox>
                </td>
                <td>
                    <telerik:RadDatePicker ID="txtDataAttoLegittimante" runat="server" WrapperTableCaption=""
                        DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}"
                        Width="110" MinDate="01/01/1000" MaxDate="01/01/9999">
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
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblIdentificativoAttoRilascio" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Text="Identificativo atto rilascio"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblProtocolloAttoRilascio" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Text="Protocollo atto rilascio"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDataAttoRilascio" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Text="Data atto rilascio"></asp:Label>
                </td>

            </tr>
            <tr>
                <td>
                    <telerik:RadTextBox ID="txtIdentificativoAttoRilascio" runat="server" Width="300px" MaxLength="500"></telerik:RadTextBox>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtProtocolloAttoRilascio" runat="server" Width="300px" MaxLength="500"></telerik:RadTextBox>
                </td>
                <td>
                    <telerik:RadDatePicker ID="txtDataAttoRilascio" runat="server" WrapperTableCaption=""
                        DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}"
                        Width="110" MinDate="01/01/1000" MaxDate="01/01/9999">
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
                    </telerik:RadDatePicker>
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset style="border: 1px solid">
        <legend>&nbsp&nbsp;&nbsp;<asp:Label ID="lblInfoDebiti" runat="server" Font-Bold="true" Font-Names="Arial" Font-Size="8pt"
            Text="Info eventuali debiti"></asp:Label></legend>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblPresenzaDebito" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Text="Presenza debito"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblTipoDebito" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Text="Tipo debito" Visible="false"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblEstinzioneDebito" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Text="Estinzione debito" Visible="false"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDataEstinzioneDebito" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Text="Data estinzione debito*" Visible="false"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadComboBox ID="cmbPresenzaDebito" Width="300px" AppendDataBoundItems="true"
                        Filter="Contains" runat="server" AutoPostBack="true"
                        HighlightTemplatedItems="true" LoadingMessage="Caricamento...">
                    </telerik:RadComboBox>
                </td>
                <td>
                    <telerik:RadComboBox ID="cmbTipoDebito" Width="300px" AppendDataBoundItems="true"
                        Filter="Contains" runat="server" AutoPostBack="false" CheckBoxes="true" EnableCheckAllItemsCheckBox="true"
                        HighlightTemplatedItems="true" LoadingMessage="Caricamento..." Visible="false">
                    </telerik:RadComboBox>
                </td>
                <td>
                    <telerik:RadComboBox ID="cmbEstinzioneDebito" Width="110px" AppendDataBoundItems="true"
                        Filter="Contains" runat="server" AutoPostBack="true"
                        HighlightTemplatedItems="true" LoadingMessage="Caricamento..." Visible="false">
                    </telerik:RadComboBox>
                </td>
                <td>
                    <telerik:RadDatePicker ID="txtDataEstinzioneDebito" runat="server" WrapperTableCaption=""
                        DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}" Visible="false"
                        Width="110" MinDate="01/01/1000" MaxDate="01/01/9999">
                        <DateInput ID="DateInput3" runat="server" EmptyMessage="gg/mm/aaaa">
                            <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                        </DateInput>
                        <Calendar ID="Calendar4" runat="server">
                            <SpecialDays>
                                <telerik:RadCalendarDay Repeatable="Today">
                                    <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                </telerik:RadCalendarDay>
                            </SpecialDays>
                        </Calendar>
                    </telerik:RadDatePicker>
                </td>
            </tr>
        </table>
    </fieldset>
    <fieldset style="border: 1px solid">
        <legend>&nbsp&nbsp;&nbsp;<asp:Label ID="lblInfoCessazione" runat="server" Font-Bold="true" Font-Names="Arial" Font-Size="8pt"
            Text="Info cessazione"></asp:Label></legend>
        <table>
            <tr>
                <td>
                    <asp:Label ID="lblIdentificativoProvvedimentoCessazione" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Text="Identificativo provvedimento cessazione"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblProtocolloProvvedimentoCessazione" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Text="Protocollo provvedimento cessazione"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblDataProvvedimentoCessazione" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        Text="Data provvedimento cessazione"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <telerik:RadTextBox ID="txtIdentificativoProvvedimentoCessazione" runat="server" Width="300px" MaxLength="500"></telerik:RadTextBox>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtProtocolloProvvedimentoCessazione" runat="server" Width="300px" MaxLength="500"></telerik:RadTextBox>
                </td>
                <td>
                    <telerik:RadDatePicker ID="txtDataProvvedimentoCessazione" runat="server" WrapperTableCaption=""
                        DatePopupButton-Visible="true" DataFormatString="{0:dd/MM/yyyy}"
                        Width="110" MinDate="01/01/1000" MaxDate="01/01/9999">
                        <DateInput ID="DateInput4" runat="server" EmptyMessage="gg/mm/aaaa">
                            <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                        </DateInput>
                        <Calendar ID="Calendar5" runat="server">
                            <SpecialDays>
                                <telerik:RadCalendarDay Repeatable="Today">
                                    <ItemStyle Font-Bold="True" BackColor="#FFFF99" />
                                </telerik:RadCalendarDay>
                            </SpecialDays>
                        </Calendar>
                    </telerik:RadDatePicker>
                </td>
            </tr>
        </table>
    </fieldset>
</div>
