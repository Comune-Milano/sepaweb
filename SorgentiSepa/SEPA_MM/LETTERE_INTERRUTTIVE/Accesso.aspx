<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Accesso.aspx.vb" Inherits="LETTERE_INTERRUTTIVE_Accesso" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            color: #FFFFFF;
            font-family: Arial;
            font-weight: bold;
            font-size: x-large;
        }
    </style>
</head>
<body bgcolor="#e4e4e4">
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td bgcolor="#6699FF" class="style1" style="text-align: center">
                    GESTIONE LETTERE INTERRUTTIVE 2009</td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <table style="width:100%;">
                        <tr align="center">
                            <td>
                                <table style="border: 2px solid #000080; width: 298px;">
                                    <tr>
                                        <td style="text-align: left">
                                            &nbsp;&nbsp;</td>
                                        <td>
                                            &nbsp;&nbsp;</td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="10pt" 
                                                Text="Utente"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUtente" runat="server" Font-Bold="True" Font-Names="arial" 
                                                Font-Size="10pt"></asp:TextBox>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: left">
                                            <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="10pt" 
                                                Text="Password" style="text-align: left"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtUtente0" runat="server" Font-Bold="True" Font-Names="arial" 
                                                Font-Size="10pt" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td>
                                            &nbsp;<asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt" ForeColor="Red" 
                                                Text="Accesso negato..." Visible="False"></asp:Label>
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            &nbsp;</td>
                                        <td style="text-align: right">
                                            <asp:Button ID="Button1" runat="server" BackColor="#6699FF" 
                                                BorderStyle="Outset" ForeColor="White" Text="Accedi" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;</td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
