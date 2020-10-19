<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_CPI.ascx.vb" Inherits="NEW_CENSIMENTO_Tab_CPI" %>

<table style="width: 645px; height: 95px">
    <tr>
        <td style="vertical-align: top; text-align: left;" >
            <asp:Label ID="Label1" runat="server" Font-Names="arial" Font-Size="8pt" 
                Text="Data Rilascio"></asp:Label>
&nbsp;<asp:TextBox ID="TxtDataRilascio" runat="server" Style="left: 435px;
            top: 144px; z-index: 2;" Width="75px" ToolTip="dd/Mm/YYYY" MaxLength="10" 
            TabIndex="8"></asp:TextBox>
            <asp:Label ID="Label2" runat="server" Font-Names="arial" Font-Size="8pt" 
                Text="Data Scadenza"></asp:Label>
            <asp:TextBox ID="TxtDataScadenza" runat="server" Style="left: 435px;
            top: 144px; z-index: 2;" Width="75px" ToolTip="dd/Mm/YYYY" MaxLength="10" 
            TabIndex="8"></asp:TextBox>
        </td>
        <td style="vertical-align: top; text-align: left; " >
        </td>
    </tr>
    <tr>
        <td style="width: 80px; vertical-align: top; height: 81px; text-align: left;">
            <div 
                
                style="left: 0px; vertical-align: top; overflow: auto; width: 703px; top: 0px;
                height: 135px; text-align: left; border-right: #ccccff solid; border-top: #ccccff solid; border-left: #ccccff solid; border-bottom: #ccccff solid;">
            <asp:CheckBoxList ID="Attivita" runat="server" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 1; left: 10px; top: 12px" Width="99%" EnableTheming="False" 
                    RepeatLayout="Flow" TabIndex="11">
            </asp:CheckBoxList>
            </div>
        </td>
        <td style="vertical-align: top; width: 38px; text-align: left; height: 81px;">
            <br />
        </td>
    </tr>
</table>

 