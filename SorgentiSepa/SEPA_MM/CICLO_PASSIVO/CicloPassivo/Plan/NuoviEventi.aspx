<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NuoviEventi.aspx.vb" Inherits="Contabilita_CicloPassivo_Plan_NuoviEventi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Eventi</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="font-family: arial; font-size: 12pt; font-weight: bold">
    
        <b>Elenco Eventi<br />
&nbsp;</b><br />
        <asp:CheckBoxList ID="CheckBoxList1" runat="server" Font-Names="Courier New" 
            Font-Size="8pt" Width="100%">
        </asp:CheckBoxList>
        <br />
        <br />
        <asp:Image ID="imgConferma" runat="server" 
            ImageUrl="~/NuoveImm/Img_Conferma.png" />
&nbsp;
        <asp:Image ID="imgConferma0" runat="server" 
            ImageUrl="~/NuoveImm/Img_Home.png" onclick="self.close();"/>
    
    </div>
    </form>
</body>
</html>
