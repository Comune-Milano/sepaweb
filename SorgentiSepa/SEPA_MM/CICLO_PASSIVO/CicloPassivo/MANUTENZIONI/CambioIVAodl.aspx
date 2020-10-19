<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CambioIVAodl.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_MANUTENZIONI_CambioIVAodl" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Ricalcolo ODL</title>
    <script src="../../../CICLO_PASSIVO/CicloPassivo.js" type="text/javascript"></script>
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <link href="../../../CICLO_PASSIVO/CicloPassivo.css" rel="stylesheet" />
    <style type="text/css">
        .moneta {
            text-align: right;
        }
    </style>
</head>
<body class="sfondo">
    <form id="form1" runat="server" style="font-family: Arial; font-size: 9pt">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" />
        <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server" Transparency="0">
        </telerik:RadAjaxLoadingPanel>
        <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
            <AjaxSettings>
                <telerik:AjaxSetting AjaxControlID="Panel1">
                    <UpdatedControls>
                        <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                    </UpdatedControls>
                </telerik:AjaxSetting>
            </AjaxSettings>
        </telerik:RadAjaxManager>
        <asp:Panel runat="server" ID="Panel1">
            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td colspan="5">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="5" class="TitoloModulo">
                        <asp:Label ID="Label1" Text="Cambio IVA per ODL" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="5">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <table>
                            <tr>
                                <td style="width: 20%">Numero ODL
                                </td>
                                <td style="width: 20%; text-align: right">
                                    <telerik:RadNumericTextBox ID="RadNumericTextBoxNumero" runat="server" Type="Number"
                                        MinValue="1" MaxValue="10000" Width="60px" NumberFormat-DecimalDigits="0">
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td style="width: 10%; text-align: center">&nbsp;/&nbsp;
                                </td>
                                <td style="width: 20%">
                                    <telerik:RadNumericTextBox ID="RadNumericTextBoxAnno" runat="server" Type="Number"
                                        MinValue="1990" MaxValue="2050" Width="60px" NumberFormat-DecimalDigits="0">
                                    </telerik:RadNumericTextBox>
                                </td>
                                <td style="width: 20%">
                                    <asp:Button runat="server" ID="ButtonCerca" Text="Cerca" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <fieldset style="border-color: Blue">
                            <legend><b>Info ODL</b></legend>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>Numero repertorio
                                    </td>
                                    <td>
                                        <asp:Label Text="" runat="server" ID="LabelRepertorio" Font-Bold="true" />
                                    </td>
                                    <td>Fornitore
                                    </td>
                                    <td>
                                        <asp:Label Text="" runat="server" ID="LabelFornitore" Font-Bold="true" />
                                    </td>
                                    <td>Patrimoniale
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelPatrimoniale" Font-Bold="True" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Descrizione
                                    </td>
                                    <td colspan="5">
                                        <asp:Label Text="" runat="server" ID="LabelDescrizione" />
                                    </td>

                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <fieldset style="border-color: Red">
                            <legend><b>Pre-modifica ODL</b></legend>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 50%">Sconto
                                    </td>
                                    <td style="width: 50%">
                                        <asp:Label runat="server" ID="LabelSconto" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Oneri
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelOneri" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>IVA
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelPercIVA" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>A lordo compresi oneri
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelLordoCompresiOneri" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Oneri di sicurezza
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelOneriDiSicurezza" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>A lordo esclusi oneri
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelLordoEsclusiOneri" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Ribasso d'asta
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelRibasso" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>A netto esclusi oneri
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelNettoEsclusiOneri" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Ritenuta di legge ivata
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelRitenutaIvata" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>A netto compresi oneri
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelNettoCompresiOneri" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>IVA
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelIVA" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Rimborsi
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelRimborsi" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>A netto compresi oneri e IVA
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelNettoCompresiOnerieIVA" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Penale
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelPenale" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Importo approvato
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelTotale" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td style="text-align: center;">
                        <table border="0" cellpadding="2" cellspacing="2">
                            <tr>
                                <td>
                                    <asp:Label ID="LabelStato" Text="" runat="server" ForeColor="Red" Font-Bold="true" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label2" Text="IVA precedente" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label Text="" runat="server" ID="IvaSbagliata" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" Text="IVA nuova" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:DropDownList runat="server" ID="IvaCorretta">
                                        <asp:ListItem Text="0" Value="0" />
                                        <asp:ListItem Text="4" Value="4" />
                                        <asp:ListItem Text="10" Value="10" />
                                        <asp:ListItem Text="22" Value="22" />
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button runat="server" ID="ButtonRicalcola" Text="Ricalcola" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td colspan="2">
                        <fieldset style="border-color: Green">
                            <legend><b>Post-modifica ODL</b></legend>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 50%">Sconto
                                    </td>
                                    <td style="width: 50%">
                                        <asp:Label runat="server" ID="LabelScontoC" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Oneri
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelOneriC" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>IVA
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelPercIVAC" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>A lordo compresi oneri
                                    </td>
                                    <td>
                                        <asp:Label Text="" runat="server" ID="LabelLordoCompresiOneriC" CssClass="moneta"
                                            Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Oneri di sicurezza
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelOneriSicurezzaC" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>A lordo esclusi oneri
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelLordoEsclusiOneriC" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Ribasso d'asta
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelRibassoC" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>A netto esclusi oneri
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelNettoEsclusiOneriC" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Ritenuta di legge ivata
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelRitenutaIvataC" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>A netto compresi oneri
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelNettoCompresiOneriC" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>IVA
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelIVAC" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Rimborsi
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelRimborsiC" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>A netto compresi oneri e IVA
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelNettoCompresiOneriIVAC" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Penale
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelPenaleC" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Importo approvato
                                    </td>
                                    <td>
                                        <asp:Label runat="server" ID="LabelTotaleC" CssClass="moneta" Width="100%" />
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
                    <td colspan="2">
                        <fieldset style="border-color: Red">
                            <legend><b>Pre-modifica Pagamento</b></legend>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 50%">
                                        <asp:Label ID="Label4" Text="Importo CDP" runat="server" />
                                    </td>
                                    <td style="width: 50%">
                                        <asp:Label runat="server" ID="LabelCDPold" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                    <td></td>
                    <td colspan="2">
                        <fieldset style="border-color: Green">
                            <legend><b>Post-modifica Pagamento</b></legend>
                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="width: 50%">
                                        <asp:Label ID="Label5" Text="Importo CDP" runat="server" />
                                    </td>
                                    <td style="width: 50%">
                                        <asp:Label runat="server" ID="LabelCDPnew" CssClass="moneta" Width="100%" />
                                    </td>
                                </tr>
                            </table>
                        </fieldset>
                    </td>
                </tr>
                <tr>
                    <td colspan="5">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:CheckBox runat="server" ID="CheckBoxPagamento" Checked="true" Text="Aggiorna CDP"
                            Enabled="false" />
                    </td>
                    <td colspan="3">&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="5">
                        <table>
                            <tr>
                                <td>Motivazione
                                </td>
                                <td colspan="3">
                                    <telerik:RadTextBox runat="server" ID="RadTextBoxMotivazione" Width="100%" MaxLength="100">
                                    </telerik:RadTextBox>
                                </td>
                                <td style="text-align: right">
                                    <asp:Button runat="server" ID="ButtonAggiorna" Text="Aggiorna" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:HiddenField runat="server" ID="HFidManutenzione" Value="0" />
            <asp:HiddenField runat="server" ID="HFRicalcolo" Value="0" />
        </asp:Panel>
    </form>
</body>
</html>
