<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DeleteMorosita.aspx.vb" Inherits="Condomini_DeleteMorosita" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self"/>
   <script type="text/javascript" language="javascript" >
        window.name="modal";
   </script>

    <title>Eliminazione Morosità</title>
    <style type="text/css">
        #form1
        {
            width: 500px;
            height: 200px;
        }
    </style>
</head>
<body style="background-color:#EBE9ED">
    <form id="form1" runat="server" target ="modal">
    <table style="width:100%;">
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblAttention" runat="server" Font-Bold="True" Font-Names="Arial" 
                    
                    Text="ATTENZIONE&lt;br/&gt;I mav per questa morosità sono stati già emessi. Procedendo, questi ultimi, verranno annullati.Procedere?"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="text-align: left">
                <div id="divTabel" style="overflow: auto">
                <asp:Label ID="lblTabella" runat="server" Text="Label" Width="97%"></asp:Label>
                </div>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; word-spacing: normal; text-align: right;" 
                align="right">
                <asp:ImageButton ID="btnConferma" runat="server" 
                    ImageUrl="~/NuoveImm/Img_Conferma1.png" ToolTip="Conferma Cancellazione" />
                &nbsp;<asp:ImageButton ID="btnAnnulla" runat="server" 
                    ImageUrl="~/NuoveImm/Img_AnnullaVal.png" ToolTip="Annula" />
            </td>
        </tr>
    </table>
    <asp:Label ID="lblerrore" runat="server" Text="Label" Visible="False" 
        Font-Names="Arial" Font-Size="8pt" ForeColor="#FF3300"></asp:Label>
    </form>
</body>
</html>
