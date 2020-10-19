<%@ Control Language="VB" AutoEventWireup="false" CodeFile="TabDatiTecnici.ascx.vb" Inherits="Condomini_TabDatiTecnici" %>
<script type="text/javascript">
    if (navigator.appName == 'Microsoft Internet Explorer') {
        document.onkeydown = $onkeydown;
    }
    else {
        window.document.addEventListener("keydown", TastoInvio, true);
    }
</script>
<style type="text/css">
</style>

<table style="width: 90%; ">
    <tr>
        <td style="vertical-align: top; width: 80px; height: 81px; text-align: left">
            <table>
                <tr>
                    <td style="vertical-align: top; text-align: left; ">
                        &nbsp;</td>
                </tr>
                </table>
            <table>
                <tr>
                    <td style="vertical-align: top; text-align: left; ">
                        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 104; left: 5px; top: 25px">BUILDING MANAGER</asp:Label></td>
                    <td style="width: 334px">
                        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 104; left: 5px; top: 25px">FILIALE</asp:Label></td>
                </tr>
                <tr>
                    <td style="vertical-align: top; text-align: left; " >
                        <asp:DropDownList ID="cmbBuildingManager" runat="server" BackColor="White" Font-Names="Arial"
                            Font-Size="9pt" Style="right: 481px; left: 9px; top: 109px" TabIndex="11" Width="278px">
                        </asp:DropDownList></td>
                    <td style="width: 334px; vertical-align: top; height: 26px; text-align: left;">
                        <asp:DropDownList ID="cmbFiliale" runat="server" BackColor="White" Font-Names="Arial"
                            Font-Size="9pt" Style="right: 481px; left: 9px; top: 109px" TabIndex="12" Width="400px">
                        </asp:DropDownList></td>
                </tr>
            </table>
            <table style="width: 738px">
                <tr>
                    <td style="vertical-align: top; text-align: right;  ">
                        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 104; left: 5px; top: 25px">TIPO RISCALDAMENTO</asp:Label></td>
                    <td >
                        <asp:DropDownList ID="cmbTipoRisc" runat="server" BackColor="White" Font-Names="Arial"
                            Font-Size="9pt" Style="right: 481px; left: 9px; top: 109px" TabIndex="13" Width="400px">
                        </asp:DropDownList></td>
                    <td >
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top; text-align: right;  " >
                        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 104; left: 5px; top: 25px">CENTRALE TERMICA</asp:Label></td>
                    <td style="vertical-align: top; height: 26px; text-align: left;">
                        <asp:DropDownList ID="cmbDenRiscald" runat="server" BackColor="White" Font-Names="Arial"
                            Font-Size="9pt" Style="right: 481px; left: 9px; top: 109px" TabIndex="14" Width="400px">
                        </asp:DropDownList></td>
                    <td style="vertical-align: top;height: 26px; text-align: left">
                    </td>
                </tr>
                <tr>
                    <td style="vertical-align: top;text-align: right" >
                        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 104; left: 5px; top: 25px">SUPER CONDOMINIO</asp:Label></td>
                    <td style="vertical-align: top;height: 26px; text-align: left">
                            <asp:ListBox ID="LstSuperCond" runat="server" Width="91%" Font-Names="Arial" 
                                Font-Size="9pt" 
                                style="border-right: #cc0000 thin solid; border-top: #cc0000 thin solid; border-left: #cc0000 thin solid; border-bottom: #cc0000 thin solid" 
                                TabIndex="15"></asp:ListBox>
                        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    </td>
                    <td style="vertical-align: top; text-align: left">
                        <asp:Image ID="imgAddSuperCond" runat="server" ImageUrl="~/Condomini/Immagini/pencil-icon.png"
                            onclick="document.getElementById('ScegSuperCondVis').value!='1';myOpacitySuperCond.toggle();"
                            Style="z-index: 8; left: 778px; top: 175px; cursor :pointer;" ToolTip="Aggiungi un Super Condominio" TabIndex="16"  /></td>
                </tr>
                </table>
        </td>
    </tr>
</table>
<div id="SuperCond" style="border: thin solid #6699ff; z-index: 300; left: 324px; visibility: hidden; vertical-align: top; width: 399px; position: absolute; top: 416px;
    height: 169px; background-color: gainsboro; text-align: left">
    <table style="width: 99%; height: 99%">
        <tr>
            <td style="vertical-align: top; width: 426px; height: 98%; text-align: left">
                <div style="overflow: auto; width: 99%; height: 99%">
                    <asp:CheckBoxList ID="ListSuperCond" runat="server" Font-Names="Arial" Font-Size="9pt"
                        Style="left: 334px; top: 251px" Width="90%">
                    </asp:CheckBoxList></div>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; width: 426px; text-align: right">
                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" ImageUrl="~/Condomini/Immagini/Aggiungi.png"
                    Style="z-index: 103; left: 744px; cursor: pointer; top: 26px" ToolTip="Aggiorna" TabIndex="-1" /></td>
        </tr>
    </table>
</div>
