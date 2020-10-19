<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Errore.aspx.vb" Inherits="Errore" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Errore</title>
    <script src="AMMSEPA/js/jquery-1.7.2.min.js" type="text/javascript"></script>
    <script src="AMMSEPA/js/jquery-ui-1.8.19.custom.min.js" type="text/javascript"></script>
    <script src="StandardTelerik/Scripts/modalTelerik.js" type="text/javascript"></script>
    <script language="javascript" type="text/javascript">
        function Chiudi() {
            var oWindow = null;
            if (window.radWindow) {
                oWindow = window.radWindow;
            } else {
                if (window.frameElement) {
                    if (window.frameElement.radWindow) {
                        oWindow = window.frameElement.radWindow;
                    };
                };
            };
            if (oWindow != null) {
                CancelEdit();
            } else {
                if (parent.main == null) {
                    window.close();
                }
                else {
                    window.parent.close();
                };
            };
        };
        function CenterPage(pageURL, title, w, h) {
            var left = (screen.width / 2) - (w / 2);
            var top = (screen.height / 2) - (h / 2);
            var targetWin = window.open(pageURL, title, 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=no, copyhistory=no, width=' + w + ', height=' + h + ', top=' + top + ', left=' + left);
        };
        function TastoInvio(e) {
            sKeyPressed1 = e.which;
            if (sKeyPressed1 == 112 || sKeyPressed1 == 115 || sKeyPressed1 == 116 || sKeyPressed1 == 117 || sKeyPressed1 == 118 || sKeyPressed1 == 122 || sKeyPressed1 == 123 || sKeyPressed1 == 60 || sKeyPressed1 == 91 || sKeyPressed1 == 92) {
                sKeyPressed1 = 0;
                e.preventDefault();
                e.stopPropagation();
            };
            if (document.activeElement.isTextEdit == false && document.activeElement.isContentEditable == false) {
                if (sKeyPressed1 == 8 || sKeyPressed1 == 13) {
                    sKeyPressed1 = 0;
                    e.preventDefault();
                    e.stopPropagation();
                }
            }
            else {
                if (document.activeElement.isMultiLine == false) {
                    if (sKeyPressed1 == 13) {
                        sKeyPressed1 = 0;
                        e.preventDefault();
                        e.stopPropagation();
                    };
                };
            };
            var alt = window.event.altKey;
            if (alt && sKeyPressed1 == 115) {
                if (document.getElementById('noClose').value == 1) {
                    if (document.getElementById('btnEsci') != null) {
                        exitClick = 1;
                        document.getElementById('btnEsci').click();
                    }
                    else if (document.getElementById('MainContent_btnEsci') != null) {
                        exitClick = 1;
                        document.getElementById('MainContent_btnEsci').click();
                    };
                    alert('La finestra è stata chiusa in modo anomalo.Tutti i dati non salvati andranno persi');
                };
            };
        };
        function $onkeydown() {
            var alt = window.event.altKey;
            if (alt && event.keyCode == 115) {
                if (document.getElementById('noClose').value == 1) {
                    if (document.getElementById('btnEsci') != null) {
                        exitClick = 1;
                        document.getElementById('btnEsci').click();
                    }
                    else if (document.getElementById('MainContent_btnEsci') != null) {
                        exitClick = 1;
                        document.getElementById('MainContent_btnEsci').click();
                    };
                    alert('La finestra è stata chiusa in modo anomalo.Tutti i dati non salvati andranno persi');
                };
            };
            if (event.keyCode == 112 || event.keyCode == 115 || event.keyCode == 116 || event.keyCode == 117 || event.keyCode == 118 || event.keyCode == 122 || event.keyCode == 123 || event.keyCode == 60 || event.keyCode == 91 || event.keyCode == 92) {
                event.keyCode = 0;
                return false;
            };
            if (document.activeElement.isTextEdit == false && document.activeElement.isContentEditable == false) {
                if (event.keyCode == 8 || event.keyCode == 13) {
                    event.keyCode = 0;
                    return false;
                }
            }
            else {
                if (document.activeElement.isMultiLine == false) {
                    if (event.keyCode == 13) {
                        event.keyCode = 0;
                        return false;
                    };
                };
            };
        };
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $onkeydown;
        }
        else {
            window.document.addEventListener("keydown", TastoInvio, true);
        };
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                initialize();
            };
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td style="border-right: red 1px solid; width: 86px; border-bottom: red 1px solid">
                    <img src="Immagini/Milano.gif" /></td>
                <td style="border-bottom: red 1px solid" valign="top">
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="border-right: red 1px solid; width: 86px">
                    <img src="Immagini/LogoComune.gif" /></td>
                <td style="text-align: center" valign="middle">
                    <strong><span style="font-size: 16pt; font-family: Arial">
                        <img src="AutoCompilazione/IMG/Divieto.png" alt="Errore"/><br />
                        <br />
                        Si è verificato un errore.<br />
                        
                    </span></strong></td>
            </tr>
            <tr>
                <td style="border-right: red 1px solid; width: 86px">
                </td>
                <td style="text-align: center" valign="top">
                    <strong><span style="font-size: 16pt; font-family: Arial">
                        <table width="80%">
                            <tr style="font-size: 12pt">
                                <td style="text-align: left">
                                    Errore:<strong><span style="font-family: Arial"><br />
                                </span></strong></td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td style="text-align: left">
                                    <asp:Label
                                        ID="Label1" runat="server" Text="" Width="931px" style="text-align: left"></asp:Label>&nbsp;</td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td style="text-align: left">
                                    &nbsp;</td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td style="text-align: left">
                                    <%--<asp:HyperLink ID="HyperLink2" runat="server">Invia Errore allo staff tecnico</asp:HyperLink></td>--%>
                                <asp:Label Text="Invia Errore allo staff tecnico" runat="server" />
                            </tr>
                            <tr style="font-size: 12pt">
                                <td style="text-align: left">
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="javascript:Chiudi();">Chiudi finestra</asp:HyperLink></td>
                            </tr>
                        </table>
                    </span></strong>
                </td>
            </tr>
        </table>
        <br />
        <br />
    
    </div>
    </form>
</body>
</html>

