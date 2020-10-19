<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dom_Abitative_1.ascx.vb" Inherits="Dom_Abitative_1" %>
<div id="abuno" style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid;
    left: 10px; border-left: lightsteelblue 1px solid; width: 641px; border-bottom: lightsteelblue 1px solid;
    position: absolute; top: 107px; height: 315px; background-color: #ffffff; z-index: 196;">
    
        &nbsp; &nbsp;
        <table border="0" cellpadding="0" cellspacing="0" style="left: 0px; width: 630px;
            border-top-style: none; border-right-style: none; border-left-style: none; position: absolute;
            top: 10px; border-bottom-style: none; z-index: 100;">
            <tr>
                <td style="width: 10px; height: 17px;">
                    <asp:Image ID="alert12" runat="server" ImageUrl="~/IMG/Alert.gif" Visible="False" /></td>
                <td style="width: 606px; height: 17px;">
                    <asp:Label ID="Label7" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
                        Text="8) RILASCIO ALLOGGIO - Concorrenti che debbano rilasciare l'alloggio a seguito di ordinanza"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 10px">
                </td>
                <td style="width: 606px">
                    <asp:DropDownList ID="cmbA1" runat="server" CssClass="CssFamiAbit" Font-Names="Times New Roman"
                        Font-Size="8pt" ForeColor="#0000C0" Width="600px" TabIndex="1">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 10px">
                </td>
                <td style="width: 606px">
                    <asp:CheckBox ID="ChMorosita" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
                        Text="Non vi è provvedimento di rilascio per morosità" TabIndex="2" Checked="True" />
                    <asp:CheckBox ID="chMG" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
                        Text="La Morosità è Giustificata" TabIndex="2" style="z-index: 102; left: 475px; position: absolute; top: 35px" Width="141px" />
                    <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="arial" Font-Size="8pt"
                        NavigateUrl="~/Trasparenza.aspx" Style="z-index: 101; left: 340px; position: absolute;
                        top: 40px" Target="_blank">Modulo Trasparenza</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td style="width: 10px">
                </td>
                <td style="width: 606px">
    <asp:Label ID="Label5" runat="server" Font-Names="Times New Roman" Font-Size="8pt" Text="I valori di rilascio non sono riconosciuti nel caso di rilascio per morosità e se non sussiste la condizione di affitto oneroso."
        Width="592px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 10px">
                </td>
                <td style="width: 606px">
                </td>
            </tr>
            <tr>
                <td style="width: 10px">
                    <asp:Image ID="alert13" runat="server" ImageUrl="~/IMG/Alert.gif" Visible="False" /></td>
                <td style="width: 606px">
                    <asp:Label ID="Label6" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
                        Text="9) CONDIZIONE ABITATIVA IMPROPRIA"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 10px">
                </td>
                <td style="width: 606px">
                    <asp:DropDownList ID="cmbA2" runat="server" CssClass="CssFamiAbit" Font-Names="Times New Roman"
                        Font-Size="8pt" ForeColor="#0000C0" Width="600px" TabIndex="3">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 10px">
                    <asp:Image ID="alert14" runat="server" ImageUrl="~/IMG/Alert.gif" Visible="False" /></td>
                <td style="width: 606px">
                    <asp:Label ID="Label1" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
                        Text="10) COABITAZIONE - Richiedenti che abitino da almeno tre anni con il proprio nucleo familiare in uno stesso alloggio con altro o più nuclei familiari"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 10px">
                </td>
                <td style="width: 606px">
                    <asp:DropDownList ID="cmbA3" runat="server" CssClass="CssFamiAbit" Font-Names="Times New Roman"
                        Font-Size="8pt" ForeColor="#0000C0" Width="600px" TabIndex="4">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 10px">
                    <asp:Image ID="alert15" runat="server" ImageUrl="~/IMG/Alert.gif" Visible="False" /></td>
                <td style="width: 606px">
                    <asp:Label ID="Label2" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
                        Text="11) SOVRAFFOLLAMENTO - Richiedenti che abitino con il proprio nucleo familiare"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 10px; height: 20px;">
                </td>
                <td style="width: 606px; height: 20px;">
                    <asp:DropDownList ID="cmbA4" runat="server" CssClass="CssFamiAbit" Font-Names="Times New Roman"
                        Font-Size="8pt" ForeColor="#0000C0" Width="600px" TabIndex="5">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 10px">
                    <asp:Image ID="alert17" runat="server" ImageUrl="~/IMG/Alert.gif" Visible="False" /></td>
                <td style="width: 606px">
                    <asp:Label ID="Label9" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
                        Text="12) CONDIZIONE DELL'ALLOGGIO - Richiedenti che abitino con il proprio nucleo familiare"
                        Width="597px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 10px">
                </td>
                <td style="width: 606px">
                    <asp:DropDownList ID="cmbA5" runat="server" CssClass="CssFamiAbit" Font-Names="Times New Roman"
                        Font-Size="8pt" ForeColor="#0000C0" Width="600px" TabIndex="6">
                    </asp:DropDownList></td>
            </tr>
        </table>
    <asp:HyperLink ID="HyperLink2" runat="server" Font-Names="arial" Font-Size="8pt"
        ImageUrl="~/IMG/Aiuto.gif" NavigateUrl="~/help_domanda.htm#TA1" Style="z-index: 102;
        left: 621px; position: absolute; top: 3px" Target="_blank" Width="16px">Aiuto</asp:HyperLink>
        <br />
    &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
    
    </div>
