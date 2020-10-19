<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NuovoTipoAllegato.aspx.vb" Inherits="NuovoTipoAllegato" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self"/>
    <title>Inserimento Tipologia</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <br />
    
    </div>
    <table style="width:100%;">
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="8pt" Text="Tipologia Documento"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtDescrizione" runat="server" Width="373px" 
                    Font-Names="ARIAL" Font-Size="8pt"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblAvviso" runat="server" Font-Bold="True" Font-Names="arial" 
                    Font-Size="8pt" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp; &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp; &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp; &nbsp;</td>
        </tr>
        <tr>
            <td style="text-align: right">



                     
            
                                <asp:ImageButton ID="BtnConfermacomplesso" runat="server" ImageUrl="~/CICLO_PASSIVO/CicloPassivo/Plan/Immagini/Next.png"
                                    Style="z-index: 111; left: 268px; top: 190px; height: 12px;" 
                                    ToolTip="Conferma" />&nbsp;&nbsp; 
                                <img id="ImgAnnulla" alt="Annulla" 
                                    src="CICLO_PASSIVO/CicloPassivo/Plan/Immagini/Annulla.png" onclick="self.close();" 
                                    style="cursor:pointer "/>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="tipo" runat="server" />
    </form>
</body>
</html>
