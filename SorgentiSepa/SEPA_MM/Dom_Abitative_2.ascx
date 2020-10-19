<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Dom_Abitative_2.ascx.vb" Inherits="Dom_Abitative_2" %>
<div id="abdue" style="border-right: lightsteelblue 1px solid; border-top: lightsteelblue 1px solid;
    left: 10px; border-left: lightsteelblue 1px solid; width: 641px; border-bottom: lightsteelblue 1px solid;
    position: absolute; top: 107px; height: 315px; background-color: #ffffff; z-index: 195;">
    <p>
        &nbsp; &nbsp;
        <table border="0" cellpadding="0" cellspacing="0" style="left: 0px; width: 630px;
            border-top-style: none; border-right-style: none; border-left-style: none; position: absolute;
            top: 10px; border-bottom-style: none; z-index: 115;">
            <tr>
                <td style="width: 10px">
                    <asp:Image ID="alert18" runat="server" ImageUrl="~/IMG/Alert.gif" Visible="False" /></td>
                <td style="width: 606px">
                    <asp:Label ID="Label7" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
                        Text="13) BARRIERE ARCHITETTONICHE - Richiedenti, di cui al precedente punto 2) che abitino con il proprio nucleo familiare in alloggio che non consenta una normale condizione abitativa"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 10px">
                </td>
                <td style="width: 606px">
                    <asp:DropDownList ID="cmbA6" runat="server" CssClass="CssFamiAbit" Font-Names="Times New Roman"
                        Font-Size="8pt" ForeColor="#0000C0" Width="600px" TabIndex="1">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 10px">
                    <asp:Image ID="alert19" runat="server" ImageUrl="~/IMG/Alert.gif" Visible="False" /></td>
                <td style="width: 606px">
                    <asp:Label ID="Label6" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
                        Text="14) CONDIZIONE DI ACCESSIBILITA' - Richiedenti, di cui ai precedenti punti 1) e 2), che abitino con il proprio nucleo familiare in alloggio che non è servito da ascensore ed è situato superiormente al primo piano"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 10px">
                </td>
                <td style="width: 606px">
                    <asp:DropDownList ID="cmbA7" runat="server" CssClass="CssFamiAbit" Font-Names="Times New Roman"
                        Font-Size="8pt" ForeColor="#0000C0" Width="600px" TabIndex="2">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 10px">
                    <asp:Image ID="alert20" runat="server" ImageUrl="~/IMG/Alert.gif" Visible="False" /></td>
                <td style="width: 606px">
                    <asp:Label ID="Label4" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
                        Text="15) LONTANANZA DALLA SEDE DI LAVORO - Richiedenti che impiegano più di 90 minuti per raggiungere la sede di lavoro, utilizzando gli ordinari mezzi pubblici, in comune diverso da quello di residenza."></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 10px; height: 20px;">
                </td>
                <td style="width: 606px; height: 20px;">
                    <asp:DropDownList ID="cmbA8" runat="server" CssClass="CssFamiAbit" Font-Names="Times New Roman"
                        Font-Size="8pt" ForeColor="#0000C0" Width="600px" TabIndex="3">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 10px; height: 17px;">
                    <asp:Image ID="alert21" runat="server" ImageUrl="~/IMG/Alert.gif" Visible="False" /></td>
                <td style="width: 606px; height: 17px;">
                    <asp:Label ID="Label9" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
                        Text="16) AFFITTO ONEROSO La condizione di affitto oneroso è riconosciuta  se il canone integrato è superiore di oltre il 5% al canone sopportabile e viene gestita automaticamente dal sistema se:" Width="597px" Height="28px"></asp:Label><br />
                    <asp:Label ID="Label11" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
                        Height="15px" Style="z-index: 100; left: 0px; position: static; top: 0px" Text="1)  viene compilato dall'operatore, per l'anno fiscale di riferimento, almeno il campo 'canone di locazione';"
                        Width="597px"></asp:Label><br />
                    <asp:Label ID="Label12" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
                        Height="15px" Style="z-index: 100; left: 0px; position: static; top: 0px" Text="2) viene indicato dall'operatore che l'onerosità  sussiste nei due anni reddituali pregressi;"
                        Width="597px"></asp:Label><br />
                    <asp:Label ID="Label13" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
                        ForeColor="Maroon" Height="15px" Style="z-index: 100; left: 0px; position: static;
                        top: 0px" Text="nb. La condizione deve sussistere da almeno tre anni e permanere al momento di presentazione/aggiornamento della domanda. Il contratto di affitto deve essere relativo all'abitazione principale regolarmente registrato."
                        Width="597px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 10px">
                </td>
                <td style="width: 606px">
                    <asp:DropDownList ID="cmbA9" runat="server" CssClass="CssFamiAbit" Font-Names="Times New Roman"
                        Font-Size="8pt" ForeColor="#0000C0" Width="600px" TabIndex="4">
                    </asp:DropDownList></td>
            </tr>
        </table>
        <br />
    &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp;
        <asp:CheckBox ID="chAO" runat="server" Font-Names="Times New Roman" Font-Size="8pt"
            Style="z-index: 100; left: 474px; position: absolute; top: 268px" TabIndex="2"
            Text="Affitto Oneroso nei due anni reddituali pregressi." Width="158px" />
        <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="arial" Font-Size="8pt"
            NavigateUrl="~/Trasparenza.aspx" Style="z-index: 101; left: 370px; position: absolute;
            top: 271px" Target="_blank">Modulo Trasparenza</asp:HyperLink>
        <asp:HyperLink ID="HyperLink2" runat="server" Font-Names="arial" Font-Size="8pt"
            ImageUrl="~/IMG/Aiuto.gif" NavigateUrl="~/help_domanda.htm#TA2" Style="z-index: 102;
            left: 621px; position: absolute; top: 3px" Target="_blank" Width="16px">Aiuto</asp:HyperLink>
        <asp:TextBox ID="txtAnnoCanone" runat="server" Columns="3" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            Height="20px" MaxLength="4" Style="z-index: 103; left: 165px;
            position: absolute; top: 267px" TabIndex="7" Width="35px"></asp:TextBox>
        <asp:Label ID="Label2" runat="server" CssClass="CssLabel" Font-Names="Times New Roman"
            Font-Size="8pt" Height="10px" Style="z-index: 104; left: 20px; position: absolute;
            top: 269px"> canone di locazione per l'anno</asp:Label>
        <asp:Label ID="Label3" runat="server" CssClass="CssLabel" Font-Names="Times New Roman"
            Font-Size="8pt" Height="10px" Style="z-index: 105; left: 207px; position: absolute;
            top: 269px">è di</asp:Label>
        <asp:TextBox ID="txtSpese" runat="server" Columns="5" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="20px" Style="z-index: 106;
            left: 245px; position: absolute; top: 267px" TabIndex="8"></asp:TextBox>
        <asp:Label ID="Label1" runat="server" CssClass="CssLabel" Font-Names="Times New Roman"
            Font-Size="8pt" Height="10px" Style="z-index: 107; left: 296px; position: absolute;
            top: 269px">,00 Euro</asp:Label>
        <asp:Label ID="Label5" runat="server" CssClass="CssLabel" Font-Names="Times New Roman"
            Font-Size="8pt" Height="14px" Style="z-index: 108; left: 20px; position: absolute;
            top: 291px">Spese accessorie per l'anno</asp:Label>
        <asp:TextBox ID="txtAnnoAcc" runat="server" Columns="2" CssClass="CssMaiuscolo" Font-Bold="False"
            Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue" Height="20px" MaxLength="4" Style="z-index: 109; left: 165px; position: absolute; top: 289px"
            TabIndex="9" Width="35px"></asp:TextBox>
        <asp:Label ID="Label8" runat="server" CssClass="CssLabel" Font-Names="Times New Roman"
            Font-Size="8pt" Height="10px" Style="z-index: 110; left: 207px; position: absolute;
            top: 291px">sono di</asp:Label>
        <asp:TextBox ID="txtSpeseAcc" runat="server" Columns="5" CssClass="CssMaiuscolo"
            Font-Bold="False" Font-Names="Times New Roman" Font-Size="8pt" ForeColor="Blue"
            Height="20px" Style="z-index: 111; left: 245px; position: absolute; top: 289px"
            TabIndex="10"></asp:TextBox>
        <asp:Label ID="Label10" runat="server" CssClass="CssLabel" Font-Names="Times New Roman"
            Font-Size="8pt" Height="10px" Style="z-index: 112; left: 296px; position: absolute;
            top: 291px">,00 Euro</asp:Label>
        <asp:Image ID="alert2" runat="server" ImageUrl="~/IMG/Alert.gif" Style="z-index: 113;
            left: 339px; position: absolute; top: 267px" ToolTip="ABITA IN ABITAZIONE PROPRIA!"
            Visible="False" />
        <asp:Image ID="alert3" runat="server" ImageUrl="~/IMG/Alert.gif" Style="z-index: 116;
            left: 340px; position: absolute; top: 290px" ToolTip="ABITA IN ABITAZIONE PROPRIA!"
            Visible="False" />
    </p>
</div>
