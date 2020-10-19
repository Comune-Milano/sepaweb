<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ProvaFornitoriMax.aspx.vb" Inherits="_Default" validateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Esempio di file xml Fornitori da inviare al webservice (in chiaro)<br />
    
        <asp:TextBox ID="TextBox1" runat="server" Height="322px" TextMode="MultiLine" 
            Width="669px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Invia File" />
        <br />
        <br />
        Il file da inviare al webservice in formato base64 sarà:<br />
    
        <asp:TextBox ID="TextBox4" runat="server" Height="322px" TextMode="MultiLine" 
            Width="669px"></asp:TextBox>
        <br />
        <br />
        <table style="width:100%;">
            <tr>
                <td>
                    Esito che resituirà il webservice (base64):      
                    <br />
                    <asp:TextBox ID="TextBox3" runat="server" Height="80px" TextMode="MultiLine" 
                        Width="669px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    Esito in chiaro</td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server" Height="80px" TextMode="MultiLine" 
                        Width="669px"></asp:TextBox>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
