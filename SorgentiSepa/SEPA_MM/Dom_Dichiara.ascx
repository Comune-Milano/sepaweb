<%@ Control Language="vb" AutoEventWireup="false" Inherits="CM.Dom_Dichiara" CodeFile="Dom_Dichiara.ascx.vb" %>
<DIV id="dic" style="BORDER-RIGHT: lightsteelblue 1px solid; BORDER-TOP: lightsteelblue 1px solid; LEFT: 10px; BORDER-LEFT: lightsteelblue 1px solid; WIDTH: 641px; BORDER-BOTTOM: lightsteelblue 1px solid; POSITION: absolute; TOP: 107px; HEIGHT: 315px; BACKGROUND-COLOR: #ffffff; z-index: 199;">
	<P>&nbsp;<asp:Image ID="alert1" runat="server" ImageUrl="~/IMG/Alert.gif" ToolTip="IL NUCLEO FAMILIARE E' COMPOSTO DA UN SOLO COMPONENTE!" Visible="False" style="z-index: 100; left: 6px; position: absolute; top: 4px" /></P>
	<asp:Label id="Label13" style="Z-INDEX: 101; LEFT: 38px; POSITION: absolute; TOP: 148px" Font-Size="8pt" Font-Names="Times New Roman" Height="18px" Width="160px" runat="server">Di presentare domanda in quanto</asp:Label>
    &nbsp;
	<asp:DropDownList id="cmbPresentaD" style="Z-INDEX: 103; LEFT: 208px; POSITION: absolute; TOP: 145px" ForeColor="#0000C0" Font-Size="8pt" Font-Names="Times New Roman" CssClass="CssPresenta" Font-Bold="False" Height="20px" runat="server" Width="417px" TabIndex="5">
	</asp:DropDownList>
	<asp:CheckBox id="CF1" style="Z-INDEX: 104; LEFT: 33px; POSITION: absolute; TOP: 3px" runat="server" Height="12px" CssClass="CssLabel" Font-Names="Times New Roman" Font-Size="8pt" Text="Che trattasi di nucleo familiare di nuova formazione costituito con atto di matrimonio o per convivenza more         uxorio entro due anni dalla data della domanda con/senza figli minorenni o minori anche legalmente affidati" TabIndex="1" Width="573px"></asp:CheckBox>
	<asp:CheckBox id="CF2" style="Z-INDEX: 105; LEFT: 34px; POSITION: absolute; TOP: 46px" runat="server" Height="12px" CssClass="CssLabel" Font-Names="Times New Roman" Font-Size="8pt" Text="Che trattasi di nucleo familiare composto da uno o entrambi i coniugi e dai figli legittimi, naturali, o adottivi con loro conviventi, ovvero costituito da una persona sola. In questo caso:" TabIndex="2" Width="567px"></asp:CheckBox>
	<asp:CheckBox id="CF3" style="Z-INDEX: 106; LEFT: 53px; POSITION: absolute; TOP: 87px" runat="server" Height="12px" CssClass="CssLabel" Font-Names="Times New Roman" Font-Size="8pt" Text="Che nel nucleo familiari sono presenti conviventi anche more uxorio, la convivenza dei quali dura da almeno un anno." TabIndex="3" Width="570px"></asp:CheckBox>
	<asp:CheckBox id="CF4" style="Z-INDEX: 107; LEFT: 53px; POSITION: absolute; TOP: 114px" runat="server" Width="501px" Height="12px" Font-Names="Times New Roman" Font-Size="8pt" Text="Che nel nucleo familiare sono presenti altri conviventi non legati da vincoli di parentela o affinità." TabIndex="4"></asp:CheckBox>
	<asp:CheckBox id="cfProfugo" style="Z-INDEX: 108; LEFT: 32px; POSITION: absolute; TOP: 175px" runat="server" Width="411px" Height="12px" Font-Names="Times New Roman" Font-Size="8pt" Text="di essere nelle condizioni di profugo rimpatriato da non oltre un quinquennio." TabIndex="6"></asp:CheckBox>
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
	<asp:Label id="Label8" style="Z-INDEX: 119; LEFT: 26px; POSITION: absolute; TOP: 2px" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="Red">-</asp:Label>
	<asp:Label id="Label9" style="Z-INDEX: 120; LEFT: 27px; POSITION: absolute; TOP: 45px" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="Red">-</asp:Label>
	<asp:Label id="Label10" style="Z-INDEX: 121; LEFT: 28px; POSITION: absolute; TOP: 144px" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="Red">-</asp:Label>
	<asp:Label id="Label11" style="Z-INDEX: 122; LEFT: 28px; POSITION: absolute; TOP: 174px" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="Red">-</asp:Label>
    &nbsp; &nbsp;
    &nbsp;&nbsp;&nbsp;&nbsp;
    <asp:HyperLink ID="HyperLink1" runat="server" Font-Names="arial" Font-Size="8pt"
        ImageUrl="~/IMG/Aiuto.gif" NavigateUrl="~/help_domanda.htm#TD" Style="z-index: 128;
        left: 621px; position: absolute; top: 3px" Target="_blank" Width="16px">Aiuto</asp:HyperLink>
    &nbsp;<br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    &nbsp;<br />
    <br />
    <br />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp;<br />
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
    &nbsp;</DIV>
