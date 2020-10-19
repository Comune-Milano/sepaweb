<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AnnullaAppuntamento.aspx.vb" Inherits="ANAUT_AnnullaAppuntamento" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self" />
    <title></title>
    <style type="text/css">
        .style1
        {
            height: 23px;
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="width:100%;">
            <tr>
                <td style="text-align: center">
                    <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="arial" 
                        Font-Size="10pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    &nbsp;<asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" 
                        Font-Size="10pt"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    &nbsp;<asp:DropDownList id="cmbLista" tabIndex="1" 
                runat="server" Height="20px"              
                style="border: 1px solid black; z-index: 111; left: 164px; " 
                Width="300px"></asp:DropDownList></td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp; &nbsp;</td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;<asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial" 
                        Font-Size="10pt">Note</asp:Label></td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;<asp:TextBox ID="TextBox1" runat="server" Height="109px" TabIndex="2" 
                        TextMode="MultiLine" Width="300px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="style1">
                    &nbsp;&nbsp; &nbsp;<asp:Label 
                        ID="Label4" runat="server" Font-Names="arial" Font-Size="8pt" 
                        ForeColor="#FF3300" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:ImageButton ID="ImageButton1" runat="server" 
                                    ImageUrl="~/NuoveImm/Img_Procedi.png" 
                        onclientclick="Nascondi();" TabIndex="3" style="height: 20px" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                    <asp:ImageButton ID="ImageButton2" runat="server" 
                                    ImageUrl="~/NuoveImm/Img_EsciCorto.png" 
                        onclientclick="self.close();" TabIndex="4" /></td>
            </tr>
        </table>
    
    </div>
     <asp:HiddenField ID="idc" runat="server" />
    <asp:HiddenField ID="t" runat="server" />
    <asp:HiddenField ID="fase" runat="server" />
    <asp:HiddenField ID="procedi" runat="server" />
    <asp:HiddenField ID="iddich" runat="server" />
    <asp:HiddenField ID="tipo" runat="server" />
    <asp:HiddenField ID="IDA" runat="server" />

    <script  language="javascript" type="text/javascript">
        function Nascondi() {
            document.getElementById('ImageButton1').style.visibility = 'hidden';
            document.getElementById('ImageButton1').style.position = 'absolute';
            document.getElementById('ImageButton1').style.left = '-100px';
            document.getElementById('ImageButton1').style.display = 'none';
        }

    </script>
    </form>
</body>
</html>
