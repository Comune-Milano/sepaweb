<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PagamentiPervenuti.aspx.vb" Inherits="Contratti_Report_PagamentiPervenuti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
var Uscita;
Uscita=1;

function $onkeydown() 
{  

if (event.keyCode==13) 
      {  
      //alert('Usare il tasto <Avvia Ricerca>');
      //history.go(0);
      //event.keyCode = 0;
          var bt = document.getElementById('btnCerca');
          bt.click();

          return false; 


      }  
}

</script>
<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
		<title>Ricerca Contratti</title>
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
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;<asp:Label ID="lblTipo" runat="server" Text="Label" Width="729px"></asp:Label></strong></span><br />
                        <br />
                        <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 345px; position: absolute; top: 107px" 
                            Width="3px">al</asp:Label>
                        <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 195px; position: absolute; top: 107px" 
                            Width="3px">al</asp:Label>
                        <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 44px; position: absolute; top: 107px" 
                            Width="3px">al</asp:Label>
                        <asp:TextBox ID="txtDataAl1" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 103; left: 366px; position: absolute; top: 104px"
                            Width="83px" TabIndex="6" ToolTip="GG/MM/YYYY"></asp:TextBox>
                        <asp:TextBox ID="txtDataAl0" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 103; left: 217px; position: absolute; top: 104px"
                            Width="83px" TabIndex="4" ToolTip="GG/MM/YYYY"></asp:TextBox>
                        <asp:TextBox ID="txtDataAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 103; left: 69px; position: absolute; top: 104px"
                            Width="83px" TabIndex="2" ToolTip="GG/MM/YYYY"></asp:TextBox>
                        <br />
                        <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 345px; position: absolute; top: 83px" 
                            Width="19px">Dal</asp:Label>
                        <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 194px; position: absolute; top: 83px" 
                            Width="19px">Dal</asp:Label>
                        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 44px; position: absolute; top: 83px" 
                            Width="19px">Dal</asp:Label>
                        &nbsp;&nbsp;<br />
                        &nbsp;&nbsp;
                        <br />
                        &nbsp; &nbsp;&nbsp;<br />
                        <br />
                        &nbsp;<br />
                        &nbsp; &nbsp;<br />
                        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 45px; position: absolute; top: 185px">Edificio</asp:Label>
                        <asp:DropDownList ID="cmbEdificio" runat="server" AutoPostBack="True" BackColor="White"
                            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
                            border-top: black 1px solid; z-index: 111; left: 106px; border-left: black 1px solid;
                            border-bottom: black 1px solid; position: absolute; top: 184px" TabIndex="8"
                            Width="488px">
                        </asp:DropDownList>
                        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 44px; position: absolute; top: 147px">Complesso</asp:Label>
                        <asp:DropDownList ID="CmbComplesso" runat="server" AutoPostBack="True" BackColor="White"
                            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
                            border-top: black 1px solid; z-index: 111; left: 106px; border-left: black 1px solid;
                            border-bottom: black 1px solid; position: absolute; top: 147px" TabIndex="7"
                            Width="488px">
                        </asp:DropDownList>
                        &nbsp;
                        &nbsp; &nbsp; &nbsp;&nbsp;<br />
                        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 45px; position: absolute; top: 221px">Cod. Unità</asp:Label>
                        <asp:DropDownList ID="cmbCodUnita" runat="server" BackColor="White"
                            Font-Names="arial" Font-Size="10pt" Height="20px" Style="border-right: black 1px solid;
                            border-top: black 1px solid; z-index: 111; left: 106px; border-left: black 1px solid;
                            border-bottom: black 1px solid; position: absolute; top: 220px" TabIndex="9"
                            Width="247px">
                        </asp:DropDownList>
                        &nbsp;
                        &nbsp; &nbsp;
                        <br />
                        <br />
                        <asp:TextBox ID="txtDataDal1" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" 
                            Style="z-index: 103; left: 366px; position: absolute; top: 80px; " 
                            Width="83px" TabIndex="5" ToolTip="GG/MM/YYYY"></asp:TextBox>
                        <asp:TextBox ID="txtDataDal0" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" 
                            Style="z-index: 103; left: 217px; position: absolute; top: 80px; " 
                            Width="83px" TabIndex="3" ToolTip="GG/MM/YYYY"></asp:TextBox>
                        <asp:TextBox ID="txtDataDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" 
                            Style="z-index: 103; left: 69px; position: absolute; top: 80px; " 
                            Width="83px" TabIndex="1" ToolTip="GG/MM/YYYY"></asp:TextBox>
                        &nbsp;&nbsp;<br />
                        &nbsp; &nbsp;
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Red" Height="16px" Style="z-index: 104; left: 9px; position: absolute;
                            top: 334px" Visible="False" Width="525px"></asp:Label>
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
                Style="z-index: 102; left: 660px; position: absolute; top: 441px" 
                TabIndex="11" ToolTip="Home" />
            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                Style="z-index: 101; left: 533px; position: absolute; top: 441px; height: 20px;" 
                TabIndex="10" ToolTip="Avvia Ricerca" />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtDataDal1"
                ErrorMessage="!" Font-Bold="True" Style="left: 455px; position: absolute;
                top: 80px" 
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">!!!</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDataDal0"
                ErrorMessage="!" Font-Bold="True" Style="left: 306px; position: absolute;
                top: 80px" 
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">!!!</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataDal"
                ErrorMessage="!" Font-Bold="True" Style="left: 161px; position: absolute;
                top: 80px" 
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">!!!</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtDataAl1"
                ErrorMessage="!" Font-Bold="True" Style="left: 456px; position: absolute;
                top: 105px" 
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">!!!</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataAl0"
                ErrorMessage="!" Font-Bold="True" Style="left: 306px; position: absolute;
                top: 105px" 
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">!!!</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataAl"
                ErrorMessage="!" Font-Bold="True" Style="left: 162px; position: absolute;
                top: 105px" 
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

            //document.getElementById('txtDataDal').focus();
    </script>
	        <p>
                        <asp:Label ID="Label13" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="8pt"
                            
                            
                    Style="z-index: 102; left: 44px; position: absolute; top: 61px; width: 109px;">DATA EMISSIONE</asp:Label>
                        <asp:Label ID="Label18" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="8pt"
                            
                            
                            
                    Style="z-index: 102; left: 345px; position: absolute; top: 61px; width: 167px;">PERIODO DI RIFERIMENTO</asp:Label>
                        <asp:Label ID="Label15" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="8pt"
                            
                            
                            
                    Style="z-index: 102; left: 193px; position: absolute; top: 61px; width: 167px;">DATA DI PAGAMENTO</asp:Label>
                        <asp:Label ID="Label22" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="8pt"
                            
                            
                            
                    Style="z-index: 102; left: 475px; position: absolute; top: 106px; width: 310px; height: 31px;">Esempio:  per il bimestre Ottobre-Novembre 2009 inserire Dal=01/10/2009 Al=30/11/2009</asp:Label>
                        <asp:Label ID="Label21" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="8pt"
                            
                            
                            
                    Style="z-index: 102; left: 475px; position: absolute; top: 74px; width: 310px; height: 31px;">Esempio:  per il mese di Ottobre  2009 inserire Dal=01/10/2009 Al=31/10/2009</asp:Label>
                        </p>
		</form>
		</body>
</html>
