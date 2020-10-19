<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VisInteressi.aspx.vb" Inherits="Contratti_VisInteressi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Pagina senza titolo</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Image ID="imgExcel" runat="server" ImageUrl="~/NuoveImm/Img_ExportExcel.png"
        Style="cursor: pointer; left: 7px; position: absolute; top: 39px;" />
        <br />
        <br />
        &nbsp;&nbsp;
        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Height="16px" Style="z-index: 104; left: 19px; position: absolute;
            top: 584px" Visible="False" Width="525px"></asp:Label>
        &nbsp;
    
    </div>
    </form>


    &nbsp;&nbsp;<br />
    <br />
    <br />
    <br />


    <br />
    <script  language="javascript" type="text/javascript">
    document.getElementById('dvvvPre').style.visibility='hidden';
    
    </script>
                <script type="text/javascript">
                    window.focus();
                    self.focus();
</script>
</body>
</html>

