<%@ Page Language="VB" AutoEventWireup="false" CodeFile="TempisticaDiffide.aspx.vb" Inherits="ANAUT_TempisticaDiffide" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Creazione XML</title>
</head>
<script type="text/javascript">
    function Attendi() {
        //var win=null;
        //LeftPosition=(screen.width) ? (screen.width-250)/2 :0 ;
        //TopPosition=(screen.height) ? (screen.height-150)/2 :0;
        //LeftPosition=LeftPosition;
        //TopPosition=TopPosition;
        //parent.funzioni.aa=window.open('../loadXML.htm','','height=150,top='+TopPosition+',left='+LeftPosition+',width=250');
    }
</script>
<body bgcolor="#f2f5f1">
    <form id="form1" runat="server">
   
        &nbsp;&nbsp;&nbsp;&nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px">
            <tr>
                <td>
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Tempistica Diffide</strong></span><br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:Label ID="Label1" runat="server" Text="Diffide per incompletezza" 
                        style="position:absolute; top: 94px; left: 15px;" Font-Names="arial" 
                        Font-Size="10pt"></asp:Label>
                        <asp:Label ID="Label4" runat="server" Text="Giorni" 
                        style="position:absolute; top: 94px; left: 306px;" Font-Names="arial" 
                        Font-Size="10pt"></asp:Label>
                        <asp:Label ID="Label2" runat="server" Text="Diffide per non presentazione" 
                        style="position:absolute; top: 140px; left: 15px;" Font-Names="arial" 
                        Font-Size="10pt"></asp:Label>
                        <asp:Label ID="Label3" runat="server" Text="Giorni" 
                        style="position:absolute; top: 140px; left: 307px;" Font-Names="arial" 
                        Font-Size="10pt"></asp:Label>
                        <asp:Label ID="Label5" runat="server" Text="Indicano il numero di giorni entro cui produrre la documentazione richiesta nelle specifiche tipologie di diffida. Il numero sarà stampato nella lettera se previsto." 
                        style="position:absolute; top: 189px; left: 15px; width: 645px;" Font-Names="arial" 
                        Font-Size="10pt" ForeColor="#0000CC"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:TextBox ID="TextBox1" runat="server" 
                        style="position:absolute; top: 91px; left: 207px; width: 75px;" Font-Names="arial" 
                        Font-Size="10pt" MaxLength="30"></asp:TextBox>
                        <asp:TextBox ID="TextBox2" runat="server" 
                        style="position:absolute; top: 135px; left: 207px; width: 75px;" Font-Names="arial" 
                        Font-Size="10pt" MaxLength="30"></asp:TextBox>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 101; left: 563px; position: absolute; top: 445px" 
            TabIndex="8" ToolTip="Home" />
            <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/NuoveImm/Img_SalvaGrande.png"
            Style="z-index: 101; left: 479px; position: absolute; top: 445px" 
            TabIndex="8" ToolTip="Home" />
        &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
    
    </form>
</body>
</html>
