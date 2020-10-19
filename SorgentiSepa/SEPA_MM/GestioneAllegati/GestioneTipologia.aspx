<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestioneTipologia.aspx.vb"
    Inherits="GestioneAllegati_GestioneTipologia" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestione Tipologia Allegati - Sep@Com</title>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <link href="Styles/jquery/jquery-ui-1.10.4.custom.css" rel="stylesheet" type="text/css" />
    <script src="Scripts/jquery-1.8.2.js" type="text/javascript"></script>
    <script src="Scripts/jquery-ui-1.9.0.custom.js" type="text/javascript"></script>
    <script src="Scripts/jsFunzioni.js" type="text/javascript"></script>
    <script src="Scripts/jsFunzioni.js" type="text/javascript"></script>
    <script type="text/javascript">
        function Close() {
            GetRadWindow().close();
            //GetRadWindow().BrowserWindow.document.getElementById('btnAddTipologia').click();
            GetRadWindow().BrowserWindow.refreshPage('btnAddTipologia');
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <telerik:RadFormDecorator ID="RadFormDecorator1" runat="server" />
    <div style="width: 97%; height: 195px;">
        <table style="width: 100%;">
            <tr>
                <td style="height: 35px; vertical-align: middle;">
                    &nbsp;&nbsp;<asp:Label ID="lblTitolo" runat="server" Text="" Font-Bold="True" Font-Names="Arial"
                        Font-Size="12pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;<asp:Button ID="btnSalva" runat="server" Text="Salva" ToolTip="Salva Tipologia" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                        ID="btnEsci" runat="server" Text="Esci" ToolTip="Esci" OnClientClick="Close();return false;" />
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;<strong>Descrizione*:</strong>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;&nbsp;<asp:TextBox ID="txtTipologia" runat="server" Width="97%"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div id="dialog" style="display: none;">
    </div>
    <div id="confirm" style="display: none;">
    </div>
    <div id="loading" style="display: none; text-align: center;">
    </div>
    <div id="divLoading" style="width: 0px; height: 0px; display: none;">
        <img src="Immagini/load.gif" id="imageLoading" alt="" />
    </div>
    <div id="divOscura" style="display: none; text-align: center; width: 100%; height: 100%;
        position: absolute; top: 0px; left: 0px; background-color: #cccccc; z-index: 2;">
    </div>
    <asp:HiddenField ID="tipoSubmit" runat="server" Value="0" ClientIDMode="Static" />
    <asp:HiddenField ID="HFOggetto" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="HFidTipologia" runat="server" Value="" ClientIDMode="Static" />
    <asp:HiddenField ID="idSelected" runat="server" Value="" ClientIDMode="Static" />
    </form>
</body>
</html>
