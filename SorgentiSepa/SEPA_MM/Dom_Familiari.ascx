<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dom_Familiari.ascx.vb" Inherits="Dom_Familiari" %>
<div id="fam" style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid;
    left: 10px; border-left: lightsteelblue 1px solid; width: 641px; border-bottom: lightsteelblue 1px solid;
    position: absolute; top: 107px; height: 315px; background-color: #ffffff; z-index: 197;">
    <p>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 632px; border-top-style: none;
            border-right-style: none; border-left-style: none; border-bottom-style: none; left: 0px; position: absolute; top: 5px; z-index: 100;">
            <tr>
                <td style="width: 10px">
                    <asp:Image ID="alert4" runat="server" ImageUrl="~/IMG/Alert.gif" Visible="False" /></td>
                <td style="width: 606px">
                    <asp:Label ID="Label7" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
                        Text="01) ANZIANI - Nuclei familiari di non più di due componenti o persone singole"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 10px; height: 20px;">
                </td>
                <td style="width: 606px; height: 20px;">
                    <asp:DropDownList ID="cmbF1" runat="server" CssClass="CssFamiAbit" Font-Names="Times New Roman"
                        Font-Size="8pt" ForeColor="#0000C0" Width="600px" TabIndex="1">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 10px">
                    <asp:Image ID="alert5" runat="server" ImageUrl="~/IMG/Alert.gif" Visible="False" /></td>
                <td style="width: 606px">
                    <asp:Label ID="Label8" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
                        Text="02) DISABILI - Nuclei familiari con componenti, anche anagraficamente non conviventi, ma presenti nella domanda, che siano affetti da minorazioni o malattie invalidanti che comportino un handicap grave"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 10px">
                </td>
                <td style="width: 606px">
                    <asp:DropDownList ID="cmbF2" runat="server" CssClass="CssFamiAbit" Font-Names="Times New Roman"
                        Font-Size="8pt" ForeColor="#0000C0" Width="600px" TabIndex="2">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 10px">
                    <asp:Image ID="alert6" runat="server" ImageUrl="~/IMG/Alert.gif" Visible="False" /></td>
                <td style="width: 606px">
                    <asp:Label ID="Label1" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
                        Text="3) FAMIGLIA DI NUOVA FORMAZIONE - Nuclei familiari da costituirsi prima della consegna dell'alloggio, ovvero costituitisi entro i due anni precedenti alla data della domanda"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 10px">
                </td>
                <td style="width: 606px">
                    <asp:DropDownList ID="cmbF3" runat="server" CssClass="CssFamiAbit" Font-Names="Times New Roman"
                        Font-Size="8pt" ForeColor="#0000C0" Width="600px" TabIndex="3">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 10px">
                    <asp:Image ID="alert7" runat="server" ImageUrl="~/IMG/Alert.gif" Visible="False" /></td>
                <td style="width: 606px">
                    <asp:Label ID="Label2" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
                        Text="4) PERSONE SOLE, CON EVENTUALE MINORE A CARICO - Nuclei di un componente, con un eventuale minore o più a carico"
                        Width="597px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 10px">
                </td>
                <td style="width: 606px">
                    <asp:DropDownList ID="cmbF4" runat="server" CssClass="CssFamiAbit" Font-Names="Times New Roman"
                        Font-Size="8pt" ForeColor="#0000C0" Width="600px" TabIndex="4">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 10px">
                    <asp:Image ID="alert8" runat="server" ImageUrl="~/IMG/Alert.gif" Visible="False" /></td>
                <td style="width: 606px">
                    <asp:Label ID="Label3" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
                        Text="5) STATO DI DISOCCUPAZIONE - Stato di disoccupazione determinato da una caduta del reddito complessivo del nucleo familiare superiore al 50%"
                        Width="597px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 10px">
                </td>
                <td style="width: 606px">
                    <asp:DropDownList ID="cmbF5" runat="server" CssClass="CssFamiAbit" Font-Names="Times New Roman"
                        Font-Size="8pt" ForeColor="#0000C0" Width="600px" TabIndex="5">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 10px">
                    <asp:Image ID="alert9" runat="server" ImageUrl="~/IMG/Alert.gif" Visible="False" /></td>
                <td style="width: 606px">
                    <asp:Label ID="Label9" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
                        Text="6) RICONGIUNZIONE - Nucleo familiare che necessiti di alloggio idoneo per accogliervi parente disabile"
                        Width="597px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 10px">
                </td>
                <td style="width: 606px">
                    <asp:DropDownList ID="cmbF6" runat="server" CssClass="CssFamiAbit" Font-Names="Times New Roman"
                        Font-Size="8pt" ForeColor="#0000C0" Width="600px" TabIndex="6">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 10px">
                    <asp:Image ID="alert10" runat="server" ImageUrl="~/IMG/Alert.gif" Visible="False" /></td>
                <td style="width: 606px">
                    <asp:Label ID="Label4" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
                        Text="7) CASI PARTICOLARI" Width="597px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 10px">
                </td>
                <td style="width: 606px">
                    <asp:DropDownList ID="cmbF7" runat="server" CssClass="CssFamiAbit" Font-Names="Times New Roman"
                        Font-Size="8pt" ForeColor="#0000C0" Width="600px" TabIndex="7">
                    </asp:DropDownList></td>
            </tr>
        </table>
        <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="arial" Font-Size="8pt"
            ImageUrl="~/IMG/Aiuto.gif" NavigateUrl="~/help_domanda.htm#TF" Style="z-index: 102;
            left: 621px; position: absolute; top: 3px" Target="_blank" Width="16px">Aiuto</asp:HyperLink>
    </p>
</div>
&nbsp;
