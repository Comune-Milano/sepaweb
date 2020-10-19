<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TabMillesimali.aspx.vb" Inherits="CENSIMENTO_TabMillesimaliaspx" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Tipologie Tabelle Millesimali</title>
</head>
<body bgcolor="white">
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            
            Style="z-index: 100; left: 24px; position: absolute; top: 104px; height: 10px;" 
            Width="120px">Descrizione Tabella</asp:Label>
        <asp:TextBox ID="TxtDescTab" runat="server" MaxLength="30" Style="left: 24px;
            position: absolute; top: 120px" Width="360px" Height="32px" TextMode="MultiLine" TabIndex="3"></asp:TextBox>
        <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 24px; position: absolute; top: 16px" Width="160px">Tipologia Tabella Millesimale</asp:Label>
        <asp:DropDownList ID="DrLMillesimi" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 24px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 32px" TabIndex="1" Width="360px">
        </asp:DropDownList>
        &nbsp; &nbsp;&nbsp;
    
    </div>
        <asp:ImageButton ID="BtnADD" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
            Style="z-index: 103; left: 345px; position: absolute; top: 198px; height: 12px;" 
        ToolTip="Salva" TabIndex="5" Visible="False" />
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            Height="16px" Style="z-index: 100; left: 24px; position: absolute; top: 64px"
            Width="56px">Descrizione</asp:Label>
        <asp:TextBox ID="txtDescr" runat="server" MaxLength="20" Style="left: 24px;
            position: absolute; top: 80px" Width="216px" TabIndex="2"></asp:TextBox>
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
            Style="z-index: 103; left: 396px; position: absolute; top: 198px" ToolTip="ESCI" TabIndex="6" />
        <div style="border-top-width: thin; border-left-width: thin; border-left-color: gray;
            left: 23px; border-bottom-width: thin; border-bottom-color: gray; overflow: auto;
            width: 306px; border-top-color: gray; position: absolute; top: 175px; height: 72px;
            border-right-width: thin; border-right-color: gray">
            <asp:CheckBoxList ID="ListEdifci" runat="server" EnableTheming="False" Font-Names="Arial"
                Font-Size="8pt" Height="25px" RepeatLayout="Flow" Style="z-index: 1; left: 10px;
                top: 12px" Width="280px" TabIndex="4">
            </asp:CheckBoxList></div>
        <asp:Button ID="btnSelezionaTutto" runat="server" Font-Names="Arial" Font-Size="8pt"
            Style="left: 21px; position: absolute; top: 251px" Text="Seleziona/Deseleziona"
            Visible="False" Width="117px" />
        <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Style="left: 24px; position: absolute; top: 280px" Text="Label"
            Visible="False" Width="368px"></asp:Label>
        <asp:Label ID="lblEdifAssociati" runat="server" Font-Bold="False" Font-Names="Arial"
            Font-Size="8pt" Style="z-index: 100; left: 24px; position: absolute; top: 160px"
            Visible="False" Width="94px">Edifici Associati</asp:Label>
        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/Back.png"
            Style="z-index: 103; left: 89px; position: absolute; top: 287px" ToolTip="INDIETRO"
            Visible="False" />
   
    </form>
</body>
</html>
