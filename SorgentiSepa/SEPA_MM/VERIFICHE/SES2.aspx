<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SES2.aspx.vb" Inherits="AMMSEPA_Controllo_Select" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Select</title>
        <script type="text/javascript" language="javascript">
        function TastoInvio(e) {
            sKeyPressed1 = e.which;
            if (sKeyPressed1 == 120) {
                document.getElementById('Button2').click();
//                e.preventDefault();
            }
        }

        function $onkeydown() {
            if (event.keyCode == 120) {
//                event.keyCode = '9';
                document.getElementById('Button2').click();

            }
        }
        </script>
</head>
<body>
    <script type="text/javascript">
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $onkeydown;
        }
        else {
            window.document.addEventListener("keydown", TastoInvio, true);
        }
    </script>
    <form id="form1" runat="server" defaultbutton="Button1" defaultfocus="TextBox1">
    <div>
    
        <asp:TextBox ID="TextBox1" runat="server" TextMode="Password"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="CONFERMA" />
        &nbsp;<br />
    
    </div>
    <asp:TextBox ID="TextBox2" runat="server" Height="230px" TextMode="MultiLine" 
        Visible="False" Width="100%"></asp:TextBox>
    <br />
    <asp:Button ID="Button2" runat="server" Text="ESEGUI" Visible="False" />
    &nbsp;<asp:Button ID="btnExport" runat="server" Text="EXPORT XLS" 
        Visible="False" />
        &nbsp;<asp:Button ID="btnExport0" runat="server" Text="XLS (numerico)" 
        Visible="False" Width="107px" />
    <br />
    <asp:Label ID="lblNumResult" runat="server" Visible="False"></asp:Label>
    <br />
    <br />
    <asp:GridView ID="GridView1" runat="server" Visible="False" Width="100%">
    </asp:GridView>
    </form>
</body>
</html>
