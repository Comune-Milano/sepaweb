<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MarcaContrattiRegistrAE.aspx.vb"
    Inherits="Contratti_MarcaContrattiRegistrAE" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Caricamento massivo voci</title>
    <link href="../Standard/Style/Site.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .bottone2
        {
            background-color: transparent;
            border-left: 8px solid #800000;
            border-right: 0px solid #800000;
            border-top: 0px solid #800000;
            border-bottom: 0px solid #800000;
            font-weight: bold;
            font-size: 9pt;
            height: 19px;
            cursor: pointer;
            padding-left: 3px;
        }
        
        .bottone2:hover
        {
            background-color: #FFF5D3;
            border-left: 8px solid #800000;
            border-right: 0px solid #800000;
            border-top: 0px solid #800000;
            border-bottom: 0px solid #800000;
            font-weight: bold;
            font-size: 9pt;
            height: 19px;
            cursor: pointer;
            padding-left: 3px;
        }
        
        .RadUploadProgressArea
        {
            position: relative;
            top: 150px;
            left: 150px;
            width: 100%;
        }
    </style>
</head>
<body style="background-repeat: no-repeat; background-image: url('../NuoveImm/SfondoMascheraContratti.jpg');">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        <Scripts>
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.Core.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQuery.js" />
            <asp:ScriptReference Assembly="Telerik.Web.UI" Name="Telerik.Web.UI.Common.jQueryInclude.js" />
            <asp:ScriptReference Path="../Standard/Scripts/jsFunzioni.js" />
        </Scripts>
    </telerik:RadScriptManager>
    <telerik:RadFormDecorator Skin="Web20" ID="FormDecorator1" runat="server" DecoratedControls="Buttons"
        ControlsToSkip="Zone" />
    <telerik:RadStyleSheetManager ID="RadStyleSheetManager1" runat="server">
        <StyleSheets>
            <telerik:StyleSheetReference Name="Telerik.Web.UI.Skins.Calendar.css" Assembly="Telerik.Web.UI" />
            <telerik:StyleSheetReference Name="Telerik.Web.UI.Skins.Web20.Calendar.Web20.css"
                Assembly="Telerik.Web.UI" />
        </StyleSheets>
    </telerik:RadStyleSheetManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="btnAllega">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="Panel1" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <telerik:RadAjaxLoadingPanel ID="RadAjaxLoadingPanel1" runat="server">
    </telerik:RadAjaxLoadingPanel>
    <asp:Panel runat="server" ID="Panel1">
        <div style="padding-left: 5px;">
            <div>
                <table>
                    <tr>
                        <td>
                            <br />
                            <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>Aggiornamento
                                Contratti Data Decorrenza AE</strong></span>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table>
                    <tr>
                        <td>
                            <div>
                                <fieldset style="width: 760px;">
                                    <legend>Caricamento Dati</legend>
                                    <table style="width: 500px;">
                                        <tr>
                                            <td>
                                                <table width="100%">
                                                    <tr>
                                                        <td width="400px" style="vertical-align: top;">
                                                            Selezionare Elenco RU (.txt)
                                                        </td>
                                                        <td id="sfoglia">
                                                            <telerik:RadAsyncUpload runat="server" ID="FileUpload1" AllowedFileExtensions=".txt"
                                                                Width="250px" UploadedFilesRendering="BelowFileInput" OnClientFileSelected="visibleBottone"
                                                                OnClientFileUploadRemoved="noVisibleBottone" />
                                                        </td>
                                                        <td style="vertical-align: top;">
                                                        <div id="btnUpload" style="display: none;">
                                                            <asp:Button ID="btnAllega" runat="server" Text="Aggiorna"  />
                                                            </div>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            Data Decorrenza AE
                                                        </td>
                                                        <td>
                                                            <telerik:RadDatePicker ID="txtDataInvio" runat="server" WrapperTableCaption="" MaxDate="01/01/9999"
                                                                Skin="Web20" DataFormatString="{0:dd/MM/yyyy}">
                                                                <DateInput ID="DateInput7" runat="server">
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
                                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDataInvio"
                                                                Display="Static" ErrorMessage="!!" Font-Bold="True" Font-Names="arial" Font-Size="8pt"></asp:RequiredFieldValidator>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </fieldset>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtNote" runat="server" Width="780px" Height="250px" TextMode="MultiLine"
                                Visible="True" MaxLength="5000" ReadOnly="true"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <div>
                                <asp:Button ID="btnHome" runat="server" CssClass="bottone2" Text="Home" CausesValidation="false"
                                    ToolTip="Home" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            
        </div>
    </asp:Panel>
    <asp:HiddenField ID="confermaProcedi" runat="server" Value="0" />
    </form>
</body>
<script type="text/javascript">
    if (document.getElementById('dvvvPre')) {
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    };

    function visibleBottone(sender, args) {
        document.getElementById('btnUpload').style.display = 'block';
    }
    function noVisibleBottone(sender, args) {
        document.getElementById('btnUpload').style.display = 'none';
    }
    function ConfermaProcedi() {
        //        chiediConferma = window.confirm("Attenzione...procedendo verranno aggiornati i contratti presenti nel file TXT. Continuare?");
        //        if (chiediConferma == false) {
        //            document.getElementById('confermaProcedi').value = '0';
        //        }
        //        else {
        //            document.getElementById('confermaProcedi').value = '1';
        //        }

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
    };
</script>
</html>
