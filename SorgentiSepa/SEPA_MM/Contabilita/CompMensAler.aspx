<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CompMensAler.aspx.vb" Inherits="Contabilita_CompMensAler" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8"/> 
    <title>Calcolo Compensi Gestore</title>
</head>
<body>
    <form id="form1" runat="server">
    <div style="overflow: auto;">
    
        <table style="width:95%;">
            <tr>
                <td style="text-align: center; border-bottom-style: dashed; border-bottom-width: thin; border-bottom-color: #C0C0C0;">
                    <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Names="Arial" 
                        Font-Size="12pt" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                <asp:Label ID="TBL_COMPENSO_MENSILE" runat="server" Font-Names="ARIAL" 
                                                    Font-Size="8pt" TabIndex="24" Width="100%"></asp:Label>
                                            </td>
            </tr>
            <tr>
                <td>
                    <asp:ImageButton ID="ImgPDF" runat="server" 
                        ImageUrl="~/NuoveImm/Img_Stampa_Grande.png" style="height: 20px" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 10; left: 13px; top: 598px; height: 13px; width: 719px;"
                        Text="Label" Visible="False" Width="100%"></asp:Label>
                    </td>
            </tr>
            </table>
    
    </div>
    </form>
    <script  language="javascript" type="text/javascript">
            document.getElementById('dvvvPre').style.visibility='hidden';
    </script>

</body>
</html>
