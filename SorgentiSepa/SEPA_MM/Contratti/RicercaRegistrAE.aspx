<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaRegistrAE.aspx.vb"
    Inherits="Contratti_RicercaRegistrAE" %>

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
                                Report Situazione AE</strong></span>
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
                                    <legend>Parametri di Ricerca</legend>
                                    <table style="width: 500px;">
                                        <tr>
                                            <td>
                                                <table width="100%">


                                                    <tr>
                                                        <td>
                                                            <asp:Label ID="Label2" runat="server" Text="Anno:" Font-Names="arial" Font-Size="8" Width="80px"></asp:Label>
                                                            <telerik:RadComboBox ID="cmbAnno" runat="server" EnableLoadOnDemand="true" IsCaseSensitive="false"
                                                                Filter="Contains" AutoPostBack="False" Width="150px">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                        <td>
                                                            <asp:Label ID="Label1" runat="server" Text="Mese:" Font-Names="arial" Font-Size="8" Width="80px"></asp:Label>
                                                            <telerik:RadComboBox ID="cmbMese" runat="server" EnableLoadOnDemand="true" IsCaseSensitive="false"
                                                                Filter="Contains" AutoPostBack="False" Width="150px">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>

                                                    <tr>
                                                        <td>
                                                              &nbsp
                                                        </td>
                                                        <td>
                                                              &nbsp
                                                        </td>
                                                        <td>
                                                            <div id="Div1" >
                                                                <asp:Button ID="btnCerca" runat="server" Text="Cerca"  />
                                                            </div>
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
