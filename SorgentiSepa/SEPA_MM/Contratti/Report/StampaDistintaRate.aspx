<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StampaDistintaRate.aspx.vb" Inherits="Contratti_Report_StampaDistintaRate" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Distinta Rate</title>
    <style type="text/css">
        .style1
        {
            font-family: Arial;
            font-size: xx-small;
            font-style: italic;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        &nbsp;&nbsp;
        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Height="16px" Style="z-index: 104; left: 19px; position: absolute;
            top: 584px" Visible="False" Width="525px"></asp:Label>
        &nbsp;&nbsp;
        <table width="100%">
            <tr>
                <td style="text-align: center">
                    &nbsp;</td>
            </tr>
        </table>
        <br />
    <asp:Image ID="imgExcel" runat="server" ImageUrl="~/NuoveImm/Img_ExportExcel.png"
        Style="cursor: pointer; height: 12px;" />
        <br />
        <br />
    <asp:Image ID="imgVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_VisualizzaOra.png"
        Style="cursor: pointer; height: 12px;" />
        <br />
        <span class="style1">La visualizzazione dei dati potrebbe richiedere alcuni 
        minuti</span><br />
    <br />
    <br />


    <br />
    <script  language="javascript" type="text/javascript">
    document.getElementById('dvvvPre').style.visibility='hidden';
    
    </script>
    
    </div>
    </form>
</body>
</html>
