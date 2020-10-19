<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Locali.aspx.vb" Inherits="MANUTENZIONI_Locali" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>LOCALI</title>
<script language="javascript" type="text/javascript">
// <!CDATA[

function HR1_onclick() {

}

// ]]>
</script>
</head>
<body bgcolor="#ffffff" text="#ede0c0">
    <form id="form1" runat="server">
    <div>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 32px; position: absolute; top: 56px">LOCALE</asp:Label>
        <asp:DropDownList ID="cmbLocali" runat="server" AutoPostBack="True" BackColor="White"
            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
            border-top: black 1px solid; z-index: 111; left: 80px; border-left: black 1px solid;
            border-bottom: black 1px solid; position: absolute; top: 56px"
            Width="208px">
        </asp:DropDownList>
        &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;&nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
            Style="left: 187px; position: absolute; top: 147px" ToolTip="Salva" TabIndex="1" />
        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
            Style="left: 257px; position: absolute; top: 147px" ToolTip="Esci" TabIndex="2" />
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Black" Style="z-index: 100; left: 32px; position: absolute; top: 24px">IMPOSSIBILITA' ACCESSO</asp:Label>
    
    </div>
    </form>
</body>
</html>