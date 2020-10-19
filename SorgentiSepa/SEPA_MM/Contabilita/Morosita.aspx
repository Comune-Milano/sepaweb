<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Morosita.aspx.vb" Inherits="Contabilita_Morosita" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Dettagli</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;<asp:ImageButton ID="ImageButton1" runat="server" 
            ImageUrl="~/NuoveImm/Img_ExportExcel.png" /><br />
        <br />
        <asp:Label ID="Label1" runat="server"></asp:Label>
        <br />
    
        </div>
    </form>
    
    <script  language="javascript" type="text/javascript">
    document.getElementById('dvvvPre').style.visibility='hidden';
    
    </script>

    
</body>
</html>