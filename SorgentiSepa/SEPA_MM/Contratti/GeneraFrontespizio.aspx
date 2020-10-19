<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GeneraFrontespizio.aspx.vb"
    Inherits="Contratti_GeneraFrontespizio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
    </style>
    <script type="text/javascript">
        function Frontespizio() {

            window.open('../VSA/StampeDoc.aspx?IDDICHIARAZ=' + document.getElementById('idDichiarazione').value + '&CODK=' + document.getElementById('cod_kofax').value + '&CODUNITA=' +
                    document.getElementById('codUnita').value + '&NUMCONT=' + document.getElementById('TextBoxCodRU').value + '&TIPO=Frontespizio', '');

        }
    </script>
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
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="TextBoxCodRU">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="cmbPGDomanda" LoadingPanelID="RadAjaxLoadingPanel1" />
                    <telerik:AjaxUpdatedControl ControlID="PanelHidden" LoadingPanelID="RadAjaxLoadingPanel1" />
                </UpdatedControls>
            </telerik:AjaxSetting>
            <telerik:AjaxSetting AjaxControlID="cmbPGDomanda">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="idDichiarazione" LoadingPanelID="RadAjaxLoadingPanel1" />
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
                            <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>Genera Frontespizio</strong></span>
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <table>
                    <tr>
                        <td>
                            <div style="padding-left: 15px;">
                                <fieldset style="width: 720px; height: 150px;">
                                    <legend>Ricerca istanze</legend>
                                    <table style="width: 670px;">
                                        <tr>
                                            <td>
                                                <table width="100%" cellpadding="4" cellspacing="4">
                                                    <tr>
                                                        <td>
                                                            Tipologia domanda
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cmbTipoDomanda" runat="server" Culture="it-IT" ResolvedRenderMode="Classic"
                                                                HighlightTemplatedItems="true" EnableLoadOnDemand="true" Filter="Contains" LoadingMessage="Caricamento..."
                                                                Width="350px" AutoPostBack="True">
                                                            </telerik:RadComboBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td width="400px" style="vertical-align: top;">
                                                            Codice contratto
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="TextBoxCodRU" runat="server" Width="350px" AutoPostBack="True"></asp:TextBox>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            PG domanda
                                                        </td>
                                                        <td>
                                                            <telerik:RadComboBox ID="cmbPGDomanda" runat="server" Culture="it-IT" ResolvedRenderMode="Classic"
                                                                HighlightTemplatedItems="true" EnableLoadOnDemand="true" Filter="Contains" LoadingMessage="Caricamento..."
                                                                AutoPostBack="True" Width="100px">
                                                            </telerik:RadComboBox>
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
                        <td align="right">
                            <div style="padding-top: 180px">
                                <asp:Button ID="btnStampa" runat="server" CssClass="bottone2" Text="Stampa" CausesValidation="false"
                                    ToolTip="Stampa" />
                                <asp:Button ID="btnHome" runat="server" CssClass="bottone2" Text="Home" CausesValidation="false"
                                    ToolTip="Home" />
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="PanelHidden">
        <asp:HiddenField ID="idDichiarazione" runat="server" Value="0" />
        <asp:HiddenField ID="codUnita" runat="server" Value="0" />
        <asp:HiddenField ID="cod_kofax" runat="server" Value="" />
    </asp:Panel>
    </form>
</body>
</html>
