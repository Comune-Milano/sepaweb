<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ImportSTR.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_MANUTENZIONI_ImportSTR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Import STR</title>
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <style type="text/css">
        .RadUpload .ruFakeInput
        {
            width: 600px;
        }
    </style>
</head>
<body class="sfondo">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <table border="0" cellpadding="0" cellspacing="0" width="100%" class="FontTelerik">
        <tr>
            <td style="height: 7px;" colspan="2">
            </td>
        </tr>
        <tr>
            <td colspan="2" class="TitoloModulo">
                Manutenzioni e servizi / STR / Import
                <br />
                <br />
            </td>
        </tr>
        <tr>
           
            <td colspan="2">
                <telerik:RadButton ID="btnCaricaFile" runat="server" Text="Elabora">
                </telerik:RadButton>
                  <br />
                <br />
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top">
                <asp:Label ID="Label1" Text="File da importare" runat="server" />
                <br />
                <br />
            </td>
        </tr>
        <tr>
            <td colspan="2" style="height: 400px; overflow: auto; vertical-align: top; text-align: left">
                <telerik:RadAsyncUpload runat="server" ID="rdpUpload" MaxFileInputsCount="100" MultipleFileSelection="Automatic"
                    Style="text-align: center" Width="250px" AllowedFileExtensions="xlsx" Height="400px" />
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
