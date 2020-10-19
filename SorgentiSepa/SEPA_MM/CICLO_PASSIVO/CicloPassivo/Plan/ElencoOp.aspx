<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoOp.aspx.vb" Inherits="CICLO_PASSIVO_CicloPassivo_Plan_ElencoOp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Elenco Operatori</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="ARIAL" 
            Font-Size="10pt" Text="Elenco Operatori assocciati *"></asp:Label>
        <br />
        <br />
    
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="ARIAL" 
            Font-Size="10pt" Text="VOCE"></asp:Label>
        <br />
        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="ARIAL" 
            Font-Size="9pt"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="ARIAL" 
            Font-Size="10pt" Text="OPERATORI"></asp:Label>
        <br />
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="ARIAL" 
            Font-Size="9pt"></asp:Label>
    
        <br />
        <br />
    
        <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="ARIAL" 
            Font-Size="10pt" 
            Text="* Salvare le eventuali modifiche apportate, premendo il pulsante &quot;Salva&quot; per rendere effettivo questo elenco." 
            ForeColor="Red"></asp:Label>
    
    </div>
    </form>
</body>
</html>
