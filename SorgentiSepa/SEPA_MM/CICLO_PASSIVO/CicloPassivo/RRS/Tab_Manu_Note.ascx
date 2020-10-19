<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Manu_Note.ascx.vb" Inherits="Tab_Manu_Note" %>
<style type="text/css">
    .style1
    {
        width: 153px;
    }
    .style3
    {
        width: 80px;
    }
</style>
<table style="width:100%">
    <tr>
        <td>
            &nbsp;&nbsp;
            <asp:Label ID="Label4" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="7pt"
                ForeColor="#0000C0" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1"
                Width="755px">NOTE..............................................................................................................................................................................................................................................</asp:Label><br />
            &nbsp; &nbsp;<asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="9pt"
                Height="300px" MaxLength="2000" ReadOnly="True" Style="left: 80px; top: 88px"
                TabIndex="8" TextMode="MultiLine" Width="100%"></asp:TextBox>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        </td>
    </tr>
</table>