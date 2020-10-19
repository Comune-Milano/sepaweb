<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PreHome.aspx.vb" Inherits="MANUTENZIONI_PreHome" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>MENU DI SCELTA</title>
</head>
<script type="text/javascript" src="funzioni.js">function IMG1_onclick() {

}

</script>
<body leftmargin="0" topmargin="0" style="text-align: center; background-attachment: fixed; background-image: url(../NuoveImm/SfondoManutenzioni.jpg); background-repeat: no-repeat;">
    <form id="form1" runat="server">
        &nbsp;
                   <asp:ImageButton ID="ImgConsRilievi" runat="server" ImageUrl="~/MANUTENZIONI/Immagini/documento.jpg" style="left: 490px; position: absolute; top: 242px" TabIndex="2" />
                   <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="AvantGarde Bk BT"
                       Font-Size="10pt" Text="SERVIZI E MANUTENZIONI" style="left: 165px; position: absolute; top: 354px" TabIndex="-1"></asp:Label>
        &nbsp; &nbsp;&nbsp;
                   <asp:ImageButton ID="ImgManutenz" runat="server" ImageUrl="~/MANUTENZIONI/Immagini/manutenzioneORIGINA.jpg" style="left: 183px; position: absolute; top: 243px" TabIndex="1" />
        &nbsp;
                   <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="AvantGarde Bk BT"
                       Font-Size="10pt" Text="CENSIMENTO DELLO STATO MANUTENTIVO" style="left: 417px; position: absolute; top: 354px" TabIndex="-1"></asp:Label>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/NuoveImm/Titolo_MANUTENZIONI.png"
            Style="left: 13px; position: absolute; top: 16px" />
        <asp:Image ID="Image2" runat="server" Height="14px" ImageUrl="~/NuoveImm/Albero_1.gif"
            Style="left: 727px; position: absolute; top: 21px" Width="4px" />
        <asp:LinkButton ID="LinkButton1" runat="server" Font-Names="arial" Font-Size="9pt"
            ForeColor="#721C1F" Style="left: 738px; position: absolute; top: 21px" ToolTip="Chiudi Applicazione" TabIndex="-1">Chiudi</asp:LinkButton>
    </form>
</body>
</html>