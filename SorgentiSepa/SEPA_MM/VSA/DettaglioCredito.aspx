<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DettaglioCredito.aspx.vb" Inherits="VSA_DettaglioCredito" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Dettaglio Credito/Debito</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="left: 0px; 
            width: 100%; ">
            <tr>
                <td style="left: 0px;">
                    <div>
                        <strong>
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial; text-align: center;">
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                        <asp:Label ID="LblIntestazione"
                            runat="server"></asp:Label>
                        <br />
                        </span>
                        </span></strong></div>
                    <div style="left: 12px; width: 100%; height:100%">
                        <table width="100%">
                            <tr>
                                <td>
                                    &nbsp;<table width="100%">
                                        <tr>
                                            <td style="vertical-align: top; height: 41px;">
                                                <asp:Label ID="TBL_DETTAGLIO_SALDO" runat="server" Font-Names="ARIAL" 
                                                    Font-Size="8pt" TabIndex="24" Width="100%"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table width="100%">
                            <tr>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                        &nbsp;
                    </div>
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 10;width: 719px;"
                        Text="Label" Visible="False"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp;<br />
                    &nbsp;<br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp;
        
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
