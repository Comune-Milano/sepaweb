<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Prelievi.aspx.vb" Inherits="Contabilita_Flussi_Prelievi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
    
    </div>
    
    <asp:Label ID="lblTabella" runat="server" Font-Names="arial" Font-Size="10pt"></asp:Label>
                <script  language="javascript" type="text/javascript">
                    document.getElementById('dvvvPre').style.visibility = 'hidden';
    
    </script>
    
   
    
    <br />
    <br />
    <br />
    <asp:ImageButton ID="imgStampa" runat="server" 
        ImageUrl="~/NuoveImm/Img_StampaContratto.png" style="height: 20px" 
        ToolTip="Stampa in pdf" />
&nbsp;
    <asp:ImageButton ID="imgStampa0" runat="server" 
        ImageUrl="~/NuoveImm/Img_Export_XLS.png" style="height: 20px" 
        ToolTip="Export in xls" />
&nbsp;
    </form>
                    
   
    
    
                    
   
    
</body>
</html>
