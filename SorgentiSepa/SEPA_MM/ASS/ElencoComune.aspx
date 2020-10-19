<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElencoComune.aspx.vb" Inherits="ASS_ElencoComune" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
		    var Uscita;
		    Uscita = 1;
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Elenco Disponibilità Comune</title>
    <style type="text/css">
        .style1
        {
            width: 1415px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server" >
    <div>
        <asp:Label ID="LBLID" runat="server" Height="21px" Style="z-index: 100; left: 108px;
            position: absolute; top: 529px" Visible="False" Width="78px">Label</asp:Label>
        <asp:Label ID="LBLPROGR" runat="server" Height="23px" Style="z-index: 102; left: 195px;
            position: absolute; top: 550px" Visible="False" Width="57px">Label</asp:Label>
        &nbsp;
        <table style="left: 0px; height: 806px; width: auto">
            <tr>
                <td class="style1" style="width: 1415px">
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;Elenco
                        Unità Imm. messe in Disponibilità 
                    <asp:Label ID="Label8" runat="server" Text="Label"></asp:Label>&nbsp;</strong></span><br />
                    <br />
                    <asp:Label ID="Label9" runat="server" Font-Names="ARIAL" Font-Size="8pt"></asp:Label><br />
                    <br />
                    <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True"
                        BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                        PageSize="20" Style="z-index: 101; left: 16px; position: absolute; top: 104px"
                        Width="100%" Height="80%">
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <PagerStyle Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="White" />
                        <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Wrap="False" ForeColor="#0000C0" />
                    </asp:DataGrid>
                    &nbsp;<br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="Label10" runat="server" Text="Label" Visible="False" Height="24px" Width="56px"></asp:Label><br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
            <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_Export_XLS.png"
                Style="z-index: 102; left: 8px; position: absolute; top: 72px; right: 28px;" 
                ToolTip="Esporta in Excel" TabIndex="2" />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 107; left: 1048px; position: absolute; top: 832px" 
            ToolTip="Home" Visible="False" />
    
    </div>
    </form>
    <script  language="javascript" type="text/javascript">
    document.getElementById('dvvvPre').style.visibility='hidden';
    </script>
</body>
</html>

