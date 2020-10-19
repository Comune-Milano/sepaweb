<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Solleciti.aspx.vb" Inherits="Contratti_Report_Solleciti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
var Uscita;
Uscita=1;

function $onkeydown() 
{  

if (event.keyCode==13) 
      {  
          var bt = document.getElementById('btnCerca');
          bt.click();

          return false; 
      }  
}

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Solleciti</title>
	</head>
	<body bgcolor="#f2f5f1" >
	<script type="text/javascript">
document.onkeydown=$onkeydown;
</script>
		<form id="Form1" method="post" runat="server">
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../../NuoveImm/SfondoMascheraContratti.jpg); width: 800px;
                position: absolute; top: 0px">
                <tr>
                    <td style="width: 800px">
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                        Solleciti Emessi<br />
                        </strong></span>
                        <br />
                        <br />
                        &nbsp;<asp:Label ID="Label21" runat="server" Font-Bold="True" 
                            Font-Names="Arial" Font-Size="8pt"
                            
                            
                            
                            
                            Style="z-index: 102; left: 466px; position: absolute; top: 110px; width: 310px; height: 31px;">Esempio:  per il bimestre Ottobre-Novembre 2009 inserire Dal=01/10/2009 Al=30/11/2009</asp:Label>
                        <br />
                        <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 309px; position: absolute; top: 115px" 
                            Width="3px">al</asp:Label>
                        <asp:Label ID="Label24" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 15px; position: absolute; top: 115px" 
                            Width="3px">al</asp:Label>
                        <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 172px; position: absolute; top: 115px" 
                            Width="3px">al</asp:Label>
                        <asp:TextBox ID="txtDataAl0" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 103; left: 337px; position: absolute; top: 110px"
                            Width="83px" TabIndex="6" ToolTip="GG/MM/YYYY"></asp:TextBox>
                        <asp:TextBox ID="txtDataAl1" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 103; left: 41px; position: absolute; top: 110px"
                            Width="83px" TabIndex="2" ToolTip="GG/MM/YYYY"></asp:TextBox>
                        <asp:TextBox ID="txtDataAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 103; left: 197px; position: absolute; top: 110px"
                            Width="83px" TabIndex="4" ToolTip="GG/MM/YYYY"></asp:TextBox>
                        <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 309px; position: absolute; top: 86px" 
                            Width="19px">Dal</asp:Label>
                        <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 15px; position: absolute; top: 86px" 
                            Width="19px">Dal</asp:Label>
                        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 172px; position: absolute; top: 86px" 
                            Width="19px">Dal</asp:Label>
                        &nbsp;&nbsp;<br />
                        &nbsp;&nbsp;
                        <br />
                        &nbsp; &nbsp;&nbsp;<br />
                        <br />
                        &nbsp;<br />
                        <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            
                            
                            
                            Style="z-index: 102; left: 309px; position: absolute; top: 63px; width: 167px;">PERIODO DI RIFERIMENTO</asp:Label>
                        <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            
                            
                            Style="z-index: 102; left: 14px; position: absolute; top: 63px; width: 109px;">DATA SOLLECITO</asp:Label>
                        <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            
                            
                            Style="z-index: 102; left: 172px; position: absolute; top: 63px; width: 109px;">DATA EMISSIONE</asp:Label>
                        &nbsp;
                        &nbsp; &nbsp;<br />
                        &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp;&nbsp;<br />
                        &nbsp; &nbsp; &nbsp;
                        <br />
                        <img style="position:absolute; top: 186px; left: 15px;" alt="" 
                            src="../../IMG/Alert.gif" /><br />
                        <asp:TextBox ID="txtDataDal0" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" 
                            Style="z-index: 103; left: 337px; position: absolute; top: 83px" Width="83px" 
                            TabIndex="5" ToolTip="GG/MM/YYYY"></asp:TextBox>
                        <asp:TextBox ID="txtDataDal1" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" 
                            Style="z-index: 103; left: 41px; position: absolute; top: 83px" Width="83px" 
                            TabIndex="1" ToolTip="GG/MM/YYYY"></asp:TextBox>
                        <asp:TextBox ID="txtDataDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" 
                            
                            Style="z-index: 103; left: 197px; position: absolute; top: 83px; right: 520px;" Width="83px" 
                            TabIndex="3" ToolTip="GG/MM/YYYY"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;<br />
                        &nbsp; &nbsp;
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Red" Height="16px" Style="z-index: 104; left: 9px; position: absolute;
                            top: 497px" Visible="False" Width="525px"></asp:Label>
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
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 102; left: 667px; position: absolute; top: 470px" 
                TabIndex="8" ToolTip="Home" />
            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                
                Style="z-index: 101; left: 533px; position: absolute; top: 471px; " 
                TabIndex="7" ToolTip="Avvia Ricerca" />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDataDal0"
                ErrorMessage="!" Font-Bold="True" Style="left: 431px; position: absolute;
                top: 83px" 
                
                
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">!!!</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtDataDal1"
                ErrorMessage="!" Font-Bold="True" Style="left: 130px; position: absolute;
                top: 83px" 
                
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">!!!</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataDal"
                ErrorMessage="!" Font-Bold="True" Style="left: 286px; position: absolute;
                top: 83px" 
                
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">!!!</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataAl0"
                ErrorMessage="!" Font-Bold="True" Style="left: 431px; position: absolute;
                top: 110px" 
                
                
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">!!!</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtDataAl1"
                ErrorMessage="!" Font-Bold="True" Style="left: 130px; position: absolute;
                top: 110px" 
                
                
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">!!!</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataAl"
                ErrorMessage="!" Font-Bold="True" Style="left: 286px; position: absolute;
                top: 110px" 
                
                
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">!!!</asp:RegularExpressionValidator>
		<script type="text/javascript">
            function CompletaData(e, obj) {
                // Check if the key is a number
                var sKeyPressed;

                sKeyPressed = (window.event) ? event.keyCode : e.which;

                if (sKeyPressed < 48 || sKeyPressed > 57) {
                    if (sKeyPressed != 8 && sKeyPressed != 0) {
                        // don't insert last non-numeric character
                        if (window.event) {
                            event.keyCode = 0;
                        }
                        else {
                            e.preventDefault();
                        }
                    }
                }
                else {
                    if (obj.value.length == 2) {
                        obj.value += "/";
                    }
                    else if (obj.value.length == 5) {
                        obj.value += "/";
                    }
                    else if (obj.value.length > 9) {
                        var selText = (document.all) ? document.selection.createRange().text : document.getSelection();
                        if (selText.length == 0) {
                            // make sure the field doesn't exceed the maximum length
                            if (window.event) {
                                event.keyCode = 0;
                            }
                            else {
                                e.preventDefault();
                            }
                        }
                    }
                }
            }
    </script>
	        <p>
                        <asp:Label ID="Label25" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="8pt"
                            
                            
                            
                    
                            
                            Style="z-index: 102; left: 38px; position: absolute; top: 189px; width: 712px; height: 31px;">SI CONSIGLIA DI EFFETTUARE RICERCHE SU BREVI INTERVALLI  DI TEMPO (max 2 mesi) PER EVITARE TEMPI DI ATTESA LUNGHI.</asp:Label>
                        <asp:Label ID="Label20" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="8pt"
                            
                            
                            
                    
                            Style="z-index: 102; left: 466px; position: absolute; top: 79px; width: 310px; height: 31px;">Esempio:  per il mese di Ottobre  2009 inserire Dal=01/10/2009 Al=31/10/2009</asp:Label>
                        </p>
		</form>
		</body>
</html>
