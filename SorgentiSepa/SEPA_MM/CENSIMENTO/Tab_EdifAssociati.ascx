<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_EdifAssociati.ascx.vb" Inherits="CENSIMENTO_Tab_EdifAssociati" %>
<style type="text/css">
    .style1
    {
        width: 542px;
    }
</style>
<table style="width:53%;">
    <tr>
        <td class="style1">
            <div style="border-color: gray; border-width: thin; left: 18px; overflow: auto;
                width: 703px; top: 253px; height: 135px;
                z-index: 2;">
            <asp:CheckBoxList ID="ListEdifci" runat="server" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 1; left: 10px; top: 12px" Width="97%" EnableTheming="False" 
                    Height="30px" RepeatLayout="Flow" TabIndex="11">
            </asp:CheckBoxList></div>
            </td>
    </tr>
    <tr>
        <td class="style1">
            <asp:Button ID="btnSelezionaTutto" runat="server" Font-Names="Arial" Font-Size="8pt"
                Style="left: 530px; top: 253px; z-index: 2;" Text="Seleziona/Deseleziona"
                Visible="False" Width="115px" TabIndex="12" />
                                </td>
    </tr>
    <tr>
        <td class="style1">
            &nbsp;</td>
    </tr>
</table>
