<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AnnullaDiffida.aspx.vb" Inherits="ANAUT_AnnullaDiffida" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <base target="_self"/>
    <title>Annulla Diffide</title>
    <style type="text/css">
        .style1
        {
            font-size: medium;
            text-align: left;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr bgcolor="#990000">
                <td style="text-align: center">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="ARIAL" 
                        Font-Size="12pt" ForeColor="White" Text="ANNULLO DIFFIDE"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    Sei sicuro di voler annullare la diffida selezionata? Tutti gli eventi creati, 
                    il file contenente le lettere, etc saranno definitivamente cancellati.<br />
                    Hai selezionato:</td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="lblDiffida0" runat="server" style="font-weight: 700">File Diffida:</asp:Label>
                    <asp:Label ID="lblFile" runat="server" style="font-weight: 700"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="lblDiffida" runat="server" style="font-weight: 700"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    <asp:Label ID="lblDataCreazione" runat="server" style="font-weight: 700" 
                        Visible="False"></asp:Label>
                    <asp:Label ID="lblTipo" runat="server" style="font-weight: 700" Visible="False"></asp:Label>
                    <asp:Label ID="lblAU" runat="server" style="font-weight: 700" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr valign="top">
                <td class="style1">
                    Inserisci una motivazione:</td>
            </tr>
            <tr valign="top">
                <td class="style1">
                    <asp:TextBox ID="txtNote" runat="server" Height="49px" TextMode="MultiLine" 
                        Width="307px"></asp:TextBox>
                </td>
            </tr>
            <tr valign="top">
                <td class="style1">
                    <asp:Label ID="lblnote" runat="server" style="font-weight: 700" 
                        Font-Bold="False" ForeColor="#CC3300"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:ImageButton ID="ImageButton1" runat="server" 
                        ImageUrl="~/NuoveImm/Img_Conferma.png" />
&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="ImageButton2" runat="server" 
                        ImageUrl="~/NuoveImm/Img_EsciCorto.png" onclientclick="self.close();" />
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
