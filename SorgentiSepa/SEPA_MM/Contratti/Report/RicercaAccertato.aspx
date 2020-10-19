<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RicercaAccertato.aspx.vb" Inherits="Contratti_Report_RicercaAccertato" %>

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
                        Emesso Accertato<br />
                        </strong></span>
                        <br />
                        <br />
                        &nbsp;<br />
                            <asp:TextBox ID="txtAnno" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="4" 
                            Style="z-index: 103; left: 331px; position: absolute; top: 75px; width: 49px;" 
                            TabIndex="2" ToolTip="YYYY" Font-Names="arial" Font-Size="10pt"></asp:TextBox>
                        &nbsp;&nbsp;<br />
                        &nbsp;&nbsp;
                        <br />
                        &nbsp; &nbsp;&nbsp;<br />
                        <asp:CheckBox ID="ChComune" runat="server" 
                            style="position:absolute; top: 158px; left: 39px; height: 20px;" 
                            AutoPostBack="True" CausesValidation="True" Font-Bold="True" Font-Names="arial" 
                            Font-Size="10pt" Text="Report Comunale" 
                            
                            
                            ToolTip="valorizza per estrarre il report comunale senza considerare i depositi cauzionali ed i relativi bolli" 
                            TabIndex="3" />
                        <asp:DropDownList ID="cmbMensilita" runat="server" 
                            style="position:absolute; top: 75px; left: 177px;" Font-Names="arial" 
                            Font-Size="10pt" TabIndex="1">
                            <asp:ListItem Value="0">Gennaio-Febbraio</asp:ListItem>
                            <asp:ListItem Value="1">Marzo-Aprile</asp:ListItem>
                            <asp:ListItem Value="2">Maggio-Giugno</asp:ListItem>
                            <asp:ListItem Value="3">Luglio-Agosto</asp:ListItem>
                            <asp:ListItem Value="4">Settembre-Ottobre</asp:ListItem>
                            <asp:ListItem Value="5">Novembre-Dicembre</asp:ListItem>
                        </asp:DropDownList>
                        <br />
                        &nbsp;<br />
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
                                Width="678px" CausesValidation="True" RepeatColumns="3" TabIndex="4">
                            </asp:CheckBoxList></div>
                        <br />
                        <br />
                        &nbsp;&nbsp;&nbsp;<br />
                        &nbsp; &nbsp;
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:Button ID="btnSelezionaTutto" runat="server" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 5; left: 43px; position: absolute; top: 438px" Text="Seleziona/Deseleziona"
                            Width="119px" TabIndex="5" />
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
                TabIndex="7" ToolTip="Home" />
            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                
                Style="z-index: 101; left: 533px; position: absolute; top: 471px; height: 20px;" 
                TabIndex="6" ToolTip="Avvia Ricerca" 
                onclientclick="document.getElementById('dvvvPre').style.visibility = 'visible';" />
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
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
                            
                            
                            
                    
                            Style="z-index: 102; left: 44px; position: absolute; top: 79px; width: 151px; height: 31px;">Periodo Bollettazione</asp:Label>
                        </p>
        <script  language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
        </script>
		</form>
		</body>
</html>
