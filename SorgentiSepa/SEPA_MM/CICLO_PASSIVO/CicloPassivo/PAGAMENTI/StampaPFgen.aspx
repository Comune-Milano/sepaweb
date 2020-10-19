<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StampaPFgen.aspx.vb" Inherits="CicloPassivo_Pagamenti_StampaPFgen" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="../../CicloPassivo.css" rel="stylesheet" type="text/css" />
    <script src="../../CicloPassivo.js" type="text/javascript"></script>
    <title>Stampa</title>
    <script type="text/javascript">
        function ApriBlocco() {
            document.getElementById('Blocco').style.visibility = 'visible';
            document.body.style.overflow = 'hidden';
        }
    </script>
</head>
<body class="sfondo">
    <form id="form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <asp:HiddenField ID="capitoloAttuale" runat="server" Value="1." />
    <table width="100%">
        <tr>
            <td colspan="2" style="height: 7px; width: 100%">
            </td>
        </tr>
        <tr>
            <td style="text-align: left; height: 21px; width: 90%;">
                <table>
                    <tr>
                        <td>
                            <telerik:RadButton ID="btnExport" runat="server" Style="top: 0px; left: 0px" Text="Export XLS"
                                ToolTip="Esporta in Excel" />
                        </td>
                        <td>
                            <telerik:RadButton ID="btnStampaPDF" runat="server" Style="top: 0px; left: 0px" OnClientClicking="function(sender, args){ApriBlocco();}"
                                Text="Stampa" ToolTip="Stampa in PDF" />
                        </td>
                    </tr>
                </table>
            </td>
            <td style="text-align: right; height: 21px; width: 10%;">
                <telerik:RadButton ID="RadButton1" runat="server" Style="top: 0px; left: 0px" OnClientClicking="function(sender, args){self.close();}"
                    Text="Esci" ToolTip="Esci" />
            </td>
        </tr>
        <tr>
            <td style="width: 100%;" colspan="2">
                <asp:Label ID="TabellaCompleta" runat="server" Text="" Width="100%"></asp:Label>
            </td>
        </tr>
    </table>
    <asp:HiddenField runat="server" ID="FIN" Value="0" />
    <div align='center' id='Blocco' style='visibility: hidden; position: absolute; background-color: #ffffff;
        text-align: center; width: 99%; height: 99%; top: 10px; left: 10px; z-index: 10;
        border: 1px dashed #660000; font: verdana; font-size: 10px; vertical-align: middle;'>
        <table width="100%" style="height: 100%">
            <tr>
                <td style="height: 100%; width: 100%; text-align: center;">
                    <img src='../../../ASS/Immagini/load.gif' alt='caricamento in corso' />
                    <br />
                    <asp:Label ID="label27" runat="server" Font-Names="Arial" Font-Size="9pt">caricamento in corso...</asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
    <script type="text/javascript">
        if (document.getElementById('FIN').value == '0') {
            window.focus();
            self.focus();
        }
        document.getElementById('Blocco').style.visibility = 'hidden';
        document.body.style.overflow = 'auto';
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
</body>
</html>
