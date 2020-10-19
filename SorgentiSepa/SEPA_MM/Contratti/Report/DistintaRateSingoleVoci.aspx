<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DistintaRateSingoleVoci.aspx.vb" Inherits="Contratti_Report_DistintaRateSingoleVoci" %>

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
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                        Distinta Rate Emesse per Singole Voci<br />
                        </strong></span>
                        <br />
                        <br />
                        &nbsp;<asp:Label ID="Label21" runat="server" Font-Bold="True" 
                            Font-Names="Arial" Font-Size="8pt"
                            
                            
                            
                            Style="z-index: 102; left: 357px; position: absolute; top: 110px; width: 310px; height: 31px;">Esempio:  per il bimestre Ottobre-Novembre 2009 inserire Dal=01/10/2009 Al=30/11/2009</asp:Label>
                        <br />
                        <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 222px; position: absolute; top: 115px" 
                            Width="3px">al</asp:Label>
                        <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 45px; position: absolute; top: 115px" 
                            Width="3px">al</asp:Label>
                        <asp:TextBox ID="txtDataAl0" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 103; left: 246px; position: absolute; top: 110px"
                            Width="83px" TabIndex="4" ToolTip="GG/MM/YYYY"></asp:TextBox>
                        <asp:TextBox ID="txtDataAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 103; left: 68px; position: absolute; top: 110px"
                            Width="83px" TabIndex="2" ToolTip="GG/MM/YYYY"></asp:TextBox>
                        <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 222px; position: absolute; top: 86px" 
                            Width="19px">Dal</asp:Label>
                        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 44px; position: absolute; top: 86px" 
                            Width="19px">Dal</asp:Label>
                        &nbsp;&nbsp;<br />
                        &nbsp;&nbsp;
                        <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            
                            
                            Style="z-index: 102; left: 46px; position: absolute; top: 149px; width: 165px;">TIPOLOGIA BOLLETTE</asp:Label>
                        <br />
                        &nbsp; &nbsp;&nbsp;<asp:RadioButtonList ID="RdbTipologia" runat="server" 
                            Font-Names="Arial" Font-Size="8pt"
                            RepeatColumns="250" Style="z-index: 1; left: 45px; position: absolute; top: 167px; bottom: 379px;"
                            Width="350px" Height="20px" TabIndex="5" CausesValidation="True" 
                            RepeatLayout="Flow">
                            <asp:ListItem Value="Generale" Selected="True">Tutte</asp:ListItem>
                            <asp:ListItem Value="Attiva">Attivazione Contratto</asp:ListItem>
                            <asp:ListItem>Bollettazione</asp:ListItem>
                            <asp:ListItem>Virt.Manuale</asp:ListItem>
                        </asp:RadioButtonList>
                        <br />
                        <asp:CheckBox ID="ChComune" runat="server" 
                            style="position:absolute; top: 167px; left: 411px; height: 20px;" 
                            AutoPostBack="True" CausesValidation="True" Font-Bold="True" Font-Names="arial" 
                            Font-Size="10pt" Text="Report Comunale" 
                            ToolTip="valorizza per estrarre il report comunale senza considerare i depositi cauzionali ed i relativi bolli" />
                            <asp:CheckBox ID="ChSoloUsd" runat="server" 
                            style="position:absolute; top: 167px; left: 574px; height: 20px;" 
                            Font-Bold="True" Font-Names="arial" 
                            Font-Size="10pt" Text="Solo Usi Diversi" 
                            
                            ToolTip="Visualizza gli importi relativi alle sole unità diverse dall'abitativo" />
                        <br />
                        &nbsp;<br />
                        <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            
                            
                            Style="z-index: 102; left: 220px; position: absolute; top: 63px; width: 167px;">PERIODO DI RIFERIMENTO</asp:Label>
                        <asp:Label ID="Label13" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            
                            Style="z-index: 102; left: 44px; position: absolute; top: 63px; width: 109px;">DATA EMISSIONE</asp:Label>
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 44px; position: absolute; top: 196px" 
                            Width="88px">Voci Bolletta</asp:Label>
                        &nbsp;
                        &nbsp; &nbsp;<br />
                        &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp;&nbsp;<br />
                        &nbsp; &nbsp; &nbsp;
                        <div style="border-style: groove; border-color: inherit; border-width: thin; z-index: 1; left: 44px; width: 706px; position: absolute; top: 214px;
                            height: 215px; overflow: auto; ">
                            <asp:CheckBoxList ID="CheckVociBoll" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="678px" CausesValidation="True" RepeatColumns="3" TabIndex="6">
                            </asp:CheckBoxList></div>
                        <br />
                        <br />
                        <asp:TextBox ID="txtDataDal0" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" 
                            Style="z-index: 103; left: 246px; position: absolute; top: 83px" Width="83px" 
                            TabIndex="3" ToolTip="GG/MM/YYYY"></asp:TextBox>
                        <asp:TextBox ID="txtDataDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" 
                            Style="z-index: 103; left: 68px; position: absolute; top: 83px" Width="83px" 
                            TabIndex="1" ToolTip="GG/MM/YYYY"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;<br />
                        &nbsp; &nbsp;
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:Button ID="btnSelezionaTutto" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 5; left: 43px; position: absolute; top: 438px" Text="Seleziona/Deseleziona"
                            Width="119px" TabIndex="7" />
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
                TabIndex="9" ToolTip="Home" />
            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                
                Style="z-index: 101; left: 533px; position: absolute; top: 471px; height: 20px;" 
                TabIndex="8" ToolTip="Avvia Ricerca" />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDataDal0"
                ErrorMessage="!" Font-Bold="True" Style="left: 333px; position: absolute;
                top: 83px" 
                
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">!!!</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataDal"
                ErrorMessage="!" Font-Bold="True" Style="left: 158px; position: absolute;
                top: 83px" 
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">!!!</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtDataAl0"
                ErrorMessage="!" Font-Bold="True" Style="left: 334px; position: absolute;
                top: 110px" 
                
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">!!!</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataAl"
                ErrorMessage="!" Font-Bold="True" Style="left: 159px; position: absolute;
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
                        <asp:Label ID="Label20" runat="server" Font-Bold="True" 
                    Font-Names="Arial" Font-Size="8pt"
                            
                            
                            
                    Style="z-index: 102; left: 356px; position: absolute; top: 79px; width: 310px; height: 31px;">Esempio:  per il mese di Ottobre  2009 inserire Dal=01/10/2009 Al=31/10/2009</asp:Label>
                        </p>
		</form>
		</body>
</html>
