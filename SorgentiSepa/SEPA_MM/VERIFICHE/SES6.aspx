<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SES6.aspx.vb" Inherits="AMMSEPA_Controllo_UploadFile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <asp:TextBox ID="TextBox1" runat="server" TextMode="Password"></asp:TextBox>
        &nbsp;<asp:Button ID="Button6" runat="server" style="height: 26px" Text="conferma" />
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="10pt" 
                Text="File da trasferire" Visible="False"></asp:Label>
            <br />
            <asp:FileUpload ID="FileUpload1" runat="server" Visible="False" />
&nbsp;&nbsp;
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Font-Names="arial" Font-Size="10pt" 
                Text="Cartella in cui copiare (la cartella dovrà trovarsi in ALLEGATI)" 
                Visible="False"></asp:Label>
            <br />
            <asp:TextBox ID="TextBox2" runat="server" Visible="False" Width="267px"></asp:TextBox>
&nbsp;<br />
            <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="10pt" 
                Text="Scegliere il file da trasferire e Inserire la directory in cui copiare il file (partendo da ALLEGATI. Eempio: cartella\cartella" 
                Visible="False"></asp:Label>
            <br />
            <br />
            <br />
            <br />
            <asp:Button ID="Button7" runat="server" Text="Copia" Visible="False" />
            <br />
    </div>
    </form>
</body>
</html>
