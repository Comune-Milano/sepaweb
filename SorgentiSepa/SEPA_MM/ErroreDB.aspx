<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ErroreDB.aspx.vb" Inherits="Errore" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Errore</title>
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
                        <img src="NuoveImm/DbError.png" alt="Errore"/><br />
                        <br />
                        ANOMALIA CONNESSIONE DATABASE<br />
                        
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
                                        ID="Label1" runat="server" Text="Label" Width="931px" style="text-align: left"></asp:Label>&nbsp;</td>
                            </tr>
                            <tr style="font-size: 12pt">
                                <td style="text-align: left">
                                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="javascript:window.close();">Chiudi finestra</asp:HyperLink></td>
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

