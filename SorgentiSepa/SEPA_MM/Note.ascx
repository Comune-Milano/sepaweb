<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Note.ascx.vb" Inherits="Note" %>
<style type="text/css">
    .auto-style1 {
        z-index: 100;
        left: 0px;
        position: absolute;
        top: 13px;
        height: 140px;
    }

    .auto-style2 {
        position: absolute;
        left: 12px;
        top: 170px;
        height: 15px;
    }

    .auto-style3 {
        z-index: 100;
        left: 12px;
        position: absolute;
        top: 196px;
        height: 92px;
    }
</style>
<div id="not" style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid; left: 10px; border-left: lightsteelblue 1px solid; width: 641px; border-bottom: lightsteelblue 1px solid; position: absolute; top: 107px; height: 315px; background-color: #ffffff; z-index: 193;">
    <br />
    <table>
        <tr>
            <td>
                <asp:TextBox ID="txtNote"
                    runat="server" TextMode="MultiLine" Width="611px" Height="120px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Note Sepa Client" Font-Names="arial" Font-Size="10pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtNoteClient"
                    runat="server" TextMode="MultiLine" Width="611px" Enabled="False" Height="120px"></asp:TextBox>
            </td>
        </tr>
    </table>


</div>
