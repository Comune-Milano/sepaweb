﻿<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InviaXML.aspx.vb" Inherits="ANAUT_InviaXML" %>

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
   // var win=null;
   // LeftPosition=(screen.width) ? (screen.width-250)/2 :0 ;
   // TopPosition=(screen.height) ? (screen.height-150)/2 :0;
   // LeftPosition=LeftPosition;
   // TopPosition=TopPosition;
   // parent.funzioni.aa=window.open('../loadXML.htm','','height=150,top='+TopPosition+',left='+LeftPosition+',width=250');
    }
</script>
<body bgcolor="#f2f5f1">
    <form id="form1" runat="server">
   
        &nbsp;&nbsp;&nbsp; &nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px">
            <tr>
                <td>
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Import
                        XML Dichiarazioni</strong></span><br />
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
                </td>
            </tr>
        </table>
        &nbsp;
        <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 100; left: 538px; position: absolute; top: 74px" TabIndex="8" ToolTip="Home" />
        &nbsp;&nbsp;&nbsp; &nbsp; &nbsp;
        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/NuoveImm/Img_InviaNuovoFile.png"
            Style="z-index: 101; left: 19px; position: absolute; top: 74px" TabIndex="8" ToolTip="Invia Nuovo File" />
        &nbsp;&nbsp;
        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/NuoveImm/Img_ElencoFile.png"
            Style="z-index: 104; left: 196px; position: absolute; top: 74px" TabIndex="8" ToolTip="Elenco File Inviati" />
    
    </form>
</body>
</html>

