<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Contabilita.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_PAGAMENTI_Contabilita" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>RICERCA SIT. CONTABILE</title>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <script src="../../../StandardTelerik/Scripts/jsFunzioni.js" type="text/javascript"></script>
    <script type="text/javascript" src="Funzioni.js">
        var Uscita1;
        Uscita1 = 1;
    </script>
    <script language="javascript" type="text/javascript">

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
    </script>
    <style type="text/css">
.RadComboBox_Default{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif}.RadComboBox{text-align:left;display:inline-block;vertical-align:middle;white-space:nowrap;*display:inline;*zoom:1}.RadComboBox .rcbReadOnly .rcbInputCellLeft{background-position:0 -88px}.RadComboBox_Default .rcbInputCell{background-image:url('mvwres://Telerik.Web.UI, Version=2016.3.1027.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbInputCellLeft{background-position:0 0}.RadComboBox .rcbInputCell{padding-right:4px;padding-left:5px;width:100%;height:20px;line-height:20px;text-align:left;vertical-align:middle}.RadComboBox .rcbInputCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}.RadComboBox_Default .rcbReadOnly .rcbInput{color:#333}.RadComboBox .rcbReadOnly .rcbInput{cursor:default}.RadComboBox_Default .rcbInput{color:#333;font-size:12px;font-family:"Segoe UI",Arial,Helvetica,sans-serif;line-height:16px}.RadComboBox .rcbInput{margin:0;padding:2px 0 1px;height:auto;width:100%;border-width:0;outline:0;color:inherit;background-color:transparent;vertical-align:top;opacity:1}.RadComboBox .rcbReadOnly .rcbArrowCellRight{background-position:-162px -176px}.RadComboBox_Default .rcbArrowCell{background-image:url('mvwres://Telerik.Web.UI, Version=2016.3.1027.40, Culture=neutral, PublicKeyToken=121fae78165ba3d4/Telerik.Web.UI.Skins.Default.Common.radFormSprite.png')}.RadComboBox .rcbArrowCellRight{background-position:-18px -176px}.RadComboBox .rcbArrowCell{width:18px}.RadComboBox .rcbArrowCell{padding:0;border-width:0;border-style:solid;background-color:transparent;background-repeat:no-repeat}
        </style>
</head>
<body class="sfondo">
    <form id="form1" runat="server" defaultbutton="btnStampa" onsubmit="caricamento();return true;">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadWindowManager ID="RadWindowManager1" runat="server" Localization-OK="Ok"
        Localization-Cancel="Annulla">
    </telerik:RadWindowManager>
    <asp:HiddenField ID="txtDATAfinePredefinita" runat="server" />
    <asp:HiddenField ID="txtDATAinizioPredefinita" runat="server" />
    <table width="100%" class="FontTelerik">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="TitoloModulo">
                Report - Situazione contabile - Contabilità
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btnStampa" runat="server" Style="top: 0px; left: 0px" Text="Stampa"
                                ToolTip="Stampa" />
                        </td>
                        <td>
                            <telerik:RadButton ID="btnAnnulla" runat="server" Style="top: 0px; left: 0px" Text="Esci"
                                ToolTip="Home" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 7%">
                            <asp:Label ID="Label4" runat="server" Width="110px">Esercizio Finanziario</asp:Label>
                        </td>
                        <td style="width: 80%">
                            <telerik:RadComboBox ID="cmbEsercizio" runat="server" AppendDataBoundItems="true"
                                AutoPostBack="TRUE" Filter="Contains" HighlightTemplatedItems="true" LoadingMessage="Caricamento..."
                                ResolvedRenderMode="Classic" Width="40%">
                            </telerik:RadComboBox>
                        </td>
                    </tr>
                     <tr>
                        <td style="width: 7%">
                            <asp:Label ID="lblDataP1" runat="server" Width="120px">Data inizio*</asp:Label>
                        </td>
                        <td style="width: 15%">
                            <telerik:RadDatePicker ID="txtdataDAL" runat="server" DataFormatString="{0:dd/MM/yyyy}"
                                DatePopupButton-Visible="true" MaxDate="01/01/9999" Width="110" WrapperTableCaption="">
                                <DateInput ID="DateInput5" runat="server" EmptyMessage="gg/mm/aaaa">
                                    <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                </DateInput>
                                <Calendar ID="Calendar3" runat="server">
                                    <SpecialDays>
                                        <telerik:RadCalendarDay Repeatable="Today">
                                            <ItemStyle BackColor="#FFFF99" Font-Bold="True" />
                                        </telerik:RadCalendarDay>
                                    </SpecialDays>
                                </Calendar>
                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 7%">
                            <asp:Label ID="lblDataP2" runat="server" Width="100px">Data fine*</asp:Label>
                        </td>
                        <td style="width: 15%">
                            <telerik:RadDatePicker ID="txtDataAL" runat="server" DataFormatString="{0:dd/MM/yyyy}"
                                DatePopupButton-Visible="true" MaxDate="01/01/9999" Width="110" WrapperTableCaption="">
                                <DateInput ID="DateInput1" runat="server" EmptyMessage="gg/mm/aaaa">
                                    <ClientEvents OnFocus="CalendarDatePicker" OnKeyPress="CompletaDataTelerik" />
                                </DateInput>
                                <Calendar ID="Calendar1" runat="server">
                                    <SpecialDays>
                                        <telerik:RadCalendarDay Repeatable="Today">
                                            <ItemStyle BackColor="#FFFF99" Font-Bold="True" />
                                        </telerik:RadCalendarDay>
                                    </SpecialDays>
                                </Calendar>
                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                            </telerik:RadDatePicker>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
       
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td class="TitoloH1" style="text-align: left">
                            <asp:CheckBox ID="ChkStampa" runat="server" Font-Bold="True" Text="Stampa con sottovoci"
                                Width="200px" TabIndex="3" />
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
    </form>
</body>
</html>
