<%@ Page Language="VB" AutoEventWireup="false" CodeFile="P_SingoleVoci.aspx.vb" Inherits="Contratti_P_SingoleVoci" %>

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
		<form id="Form1" method="post" runat="server">
            <p>
                        <asp:CheckBox ID="ChComune" runat="server" 
                    style="position:absolute; top: 76px; left: 468px; height: 20px; z-index: 103;" 
                    AutoPostBack="True" CausesValidation="True" Font-Bold="True" Font-Names="arial" 
                    Font-Size="10pt" TabIndex="2" Text="Report Comunale" 
                    ToolTip="valorizza per estrarre il report comunale senza considerare i depositi cauzionali ed i relativi bolli" />
                    <asp:CheckBox ID="chSoloUsd" runat="server" 
                    style="position:absolute; top: 76px; left: 615px; height: 20px; z-index: 103;" 
                            Font-Bold="True" Font-Names="arial" 
                    Font-Size="10pt" TabIndex="2" Text="Solo Usi Diversi" 
                    ToolTip="Visualizza gli importi relativi alle sole unità diverse dall'abitativo" />
                        </p>
	<script type="text/javascript">
document.onkeydown=$onkeydown;
</script>
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../../NuoveImm/SfondoMascheraContratti.jpg); width: 800px;
                position: absolute; top: 0px">
                <tr>
                    <td style="width: 800px">
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Pagamenti
                            Pervenuti per Singole Voci 
                        </strong></span>
                        <br />
                        <br />
                        <asp:RadioButtonList ID="RdbTipologia" runat="server" Font-Names="Arial" Font-Size="8pt"
                            RepeatColumns="250" Style="z-index: 1; left: 37px; position: absolute; top: 75px; bottom: 465px;"
                            Width="350px" Height="20px" TabIndex="1" CausesValidation="True" 
                            RepeatLayout="Flow">
                            <asp:ListItem Value="Generale">Tutte</asp:ListItem>
                            <asp:ListItem Value="Attiva">Attivazione Contratto</asp:ListItem>
                            <asp:ListItem>Bollettazione</asp:ListItem>
                            <asp:ListItem>Virt.Manuale</asp:ListItem>
                        </asp:RadioButtonList>
                        &nbsp;<br />
                        <asp:Label ID="Label24" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 289px; position: absolute; top: 151px">al</asp:Label>
                        <asp:Label ID="Label17" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 169px; position: absolute; top: 151px">al</asp:Label>
                        <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 44px; position: absolute; top: 151px">al</asp:Label>
                        <asp:TextBox ID="txtDataAl1" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 103; left: 308px; position: absolute; top: 148px"
                            Width="83px" TabIndex="8" ToolTip="GG/MM/YYYY"></asp:TextBox>
                        <asp:TextBox ID="txtDataAl0" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 103; left: 186px; position: absolute; top: 148px"
                            Width="83px" TabIndex="6" ToolTip="GG/MM/YYYY"></asp:TextBox>
                        <asp:TextBox ID="txtDataAl" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 103; left: 63px; position: absolute; top: 148px"
                            Width="83px" TabIndex="4" ToolTip="GG/MM/YYYY"></asp:TextBox>
                        <asp:Label ID="Label19" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            
                            
                            
                            Style="z-index: 102; left: 44px; position: absolute; top: 59px; width: 167px;">TIPOLOGIA BOLLETTAZIONE</asp:Label>
                        <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            
                            
                            
                            
                            Style="z-index: 102; left: 537px; position: absolute; top: 152px; width: 254px; height: 31px;">Esempio:  per il bimestre Ottobre-Novembre 2009 inserire Dal=01/10/2009 Al=30/11/2009</asp:Label>
                        <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            
                            
                            
                            
                            Style="z-index: 102; left: 536px; position: absolute; top: 122px; width: 255px; height: 31px;">Esempio:  per il mese di Ottobre  2009 inserire Dal=01/10/2009 Al=31/10/2009</asp:Label>
                        <asp:Label ID="Label18" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            
                            
                            
                            
                            Style="z-index: 102; left: 431px; position: absolute; top: 104px; width: 153px;">PERIODO DI RIFERIMENTO</asp:Label>
                        <asp:Label ID="Label22" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            
                            
                            
                            Style="z-index: 102; left: 288px; position: absolute; top: 104px; width: 113px;" 
                            ToolTip="DATA VALUTA">DATA DI ACCREDITO</asp:Label>
                        <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            
                            
                            
                            Style="z-index: 102; left: 181px; position: absolute; top: 104px; width: 94px;" 
                            ToolTip="DATA PAGAMENTO">DATA DI PAG.</asp:Label>
                        <asp:Label ID="Label14" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            
                            
                            Style="z-index: 102; left: 45px; position: absolute; top: 104px; width: 167px;">DATA DI EMISSIONE</asp:Label>
                        <br />
                        <asp:Label ID="Label23" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 288px; position: absolute; top: 125px" 
                            Width="19px">Dal</asp:Label>
                        <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 168px; position: absolute; top: 125px" 
                            Width="19px">Dal</asp:Label>
                        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 44px; position: absolute; top: 125px" 
                            Width="19px">Dal</asp:Label>
                        &nbsp;&nbsp;&nbsp;&nbsp;<br />
                        &nbsp;&nbsp;
                        <br />
                        &nbsp; &nbsp;&nbsp;<br />
                        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            
                            Style="z-index: 102; left: 411px; position: absolute; top: 149px; width: 10px;">al</asp:Label>
                        <asp:TextBox ID="txtRifAl" runat="server" BorderStyle="Solid" BorderWidth="1px" MaxLength="10"
                            Style="z-index: 103; left: 430px; position: absolute; top: 148px" TabIndex="10"
                            ToolTip="GG/MM/YYYY" Width="83px"></asp:TextBox>
                        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            
                            Style="z-index: 102; left: 411px; position: absolute; top: 125px; height: 15px; width: 24px;">Dal</asp:Label>
                        <asp:TextBox ID="txtRifDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" Style="z-index: 103; left: 430px; position: absolute; top: 122px"
                            TabIndex="9" ToolTip="GG/MM/YYYY" Width="83px"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtRifDal"
                            ErrorMessage="!" Font-Bold="True" Style="left: 518px; position: absolute;
                            top: 122px" 
                            
                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">!!!</asp:RegularExpressionValidator>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtRifAl"
                            ErrorMessage="!!!" Font-Bold="True" Style="left: 520px; position: absolute;
                            top: 148px" 
                            
                            
                            ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))" 
                            ToolTip="gg/mm/aaaa">!!!</asp:RegularExpressionValidator>
                        <br />
                        &nbsp;<br />
                        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 44px; position: absolute; top: 182px" 
                            Width="88px">Voci Bolletta</asp:Label>
                        &nbsp;
                        &nbsp; &nbsp;<br />
                        &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp;&nbsp;<br />
                        &nbsp; &nbsp; &nbsp;
                        <div style="border-style: groove; border-color: inherit; border-width: thin; z-index: 1; left: 43px; width: 706px; position: absolute; top: 198px;
                            height: 223px; overflow: auto; ">
                            <asp:CheckBoxList ID="CheckVociBoll" runat="server" Font-Names="Arial" Font-Size="8pt"
                                Width="678px" CausesValidation="True" RepeatColumns="3" TabIndex="11">
                            </asp:CheckBoxList></div>
                        <br />
                        <br />
                        <asp:TextBox ID="txtDataDal1" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" 
                            
                            
                            Style="z-index: 103; left: 308px; position: absolute; top: 122px; right: 409px;" Width="83px" 
                            TabIndex="7" ToolTip="GG/MM/YYYY"></asp:TextBox>
                        <asp:TextBox ID="txtDataDal0" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" 
                            
                            
                            Style="z-index: 103; left: 186px; position: absolute; top: 122px; right: 531px;" Width="83px" 
                            TabIndex="5" ToolTip="GG/MM/YYYY"></asp:TextBox>
                        <asp:TextBox ID="txtDataDal" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" 
                            Style="z-index: 103; left: 63px; position: absolute; top: 122px" Width="83px" 
                            TabIndex="3" ToolTip="GG/MM/YYYY"></asp:TextBox>
                        &nbsp;&nbsp;&nbsp;<br />
                        &nbsp; &nbsp;
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:Button ID="btnSelezionaTutto" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 5; left: 43px; position: absolute; top: 426px" Text="Seleziona/Deseleziona"
                            Width="119px" TabIndex="12" />
                        <br />
                        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Red" Height="16px" Style="z-index: 104; left: 9px; position: absolute;
                            top: 460px; width: 773px;" Visible="False"></asp:Label>
                        <br />
                        <br />
                        <br />
                        <asp:HiddenField ID="HiddenField1" runat="server" Value="0" />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 102; left: 660px; position: absolute; top: 485px" 
                TabIndex="14" ToolTip="Home" />
            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                Style="z-index: 101; left: 524px; position: absolute; top: 486px; height: 20px;" 
                TabIndex="13" ToolTip="Avvia Ricerca" />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            <asp:RegularExpressionValidator ID="RegularExpressionValidator7" runat="server" ControlToValidate="txtDataDal1"
                ErrorMessage="!" Font-Bold="True" Style="left: 393px; position: absolute;
                top: 122px" 
                
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">!!!</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txtDataDal0"
                ErrorMessage="!" Font-Bold="True" Style="left: 273px; position: absolute;
                top: 122px" 
                
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">!!!</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataDal"
                ErrorMessage="!" Font-Bold="True" Style="left: 149px; position: absolute;
                top: 122px" 
                
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">!!!</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtDataAl1"
                ErrorMessage="!!!" Font-Bold="True" Style="left: 394px; position: absolute;
                top: 148px" 
                
                
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))" 
                ToolTip="gg/mm/aaaa">!!!</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtDataAl0"
                ErrorMessage="!" Font-Bold="True" Style="left: 274px; position: absolute;
                top: 148px" 
                
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">!!!</asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtDataAl"
                ErrorMessage="!" Font-Bold="True" Style="left: 151px; position: absolute;
                top: 148px" 
                
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))">!!!</asp:RegularExpressionValidator>
		</form>
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
            olos</p>
	</body>
</html>
