<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SES11.aspx.vb" Inherits="AMMSEPA_Controllo_CVarie" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:TextBox ID="TextBox1" runat="server" TextMode="Password"></asp:TextBox>
        <asp:Button ID="Button6" runat="server" style="height: 26px" Text="conferma" />
    
        <br />
        <b>
        <br />
    
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
        FILETEMP</b><asp:CheckBoxList ID="CheckBoxList1" runat="server">
        </asp:CheckBoxList>
        <br />
    
    </div>
&nbsp;<asp:Button ID="Button2" runat="server" Text="Procedi" />
    <br />
    <br />
    <asp:Label ID="Label1" runat="server"></asp:Label>
    <p>
        <b>EXPORT AU</b></p>
    
        <asp:CheckBoxList ID="CheckBoxList2" runat="server">
        </asp:CheckBoxList>
        <br />
    <asp:Button ID="Button3" runat="server" Text="Procedi" />
    <br />
    <br />
        <b>STAMPE CONTRATTI<br />
    
        <asp:CheckBoxList ID="CheckBoxList3" runat="server">
        </asp:CheckBoxList>
        <br />
    <asp:Button ID="Button4" runat="server" Text="Procedi" />
    <br />
    <br />
    <br />
        STAMPE LETTERE<br />
    
        <asp:CheckBoxList ID="CheckBoxList4" runat="server">
        </asp:CheckBoxList>
        <br />
    <asp:Button ID="Button5" runat="server" Text="Procedi" />
    <br />
    <br />
    ELENCO MAV<br />
    <br />
    <asp:Label ID="Label2" runat="server"></asp:Label>
    <br />
    <br />
    MOROSITA_CONTRATTI<br />
    <asp:CheckBoxList ID="CheckBoxList5" runat="server">
        </asp:CheckBoxList>
        <br />
    <asp:Button ID="Button7" runat="server" Text="CANCELLA MOROSITA CONTRATTI" />
    <br />
    <br />
    MOROSITA_CONDOMINI<br />
    <asp:Label ID="Label4" runat="server"></asp:Label>
    <br />
    <br />
    ALLEGATI CONDOMINI<br />
    <asp:Label ID="Label5" runat="server"></asp:Label>
    </b>
    </form>
</body>
</html>
