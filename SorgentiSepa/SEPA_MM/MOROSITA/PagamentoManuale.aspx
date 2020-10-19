<%@ Page Language="VB" AutoEventWireup="false" CodeFile="PagamentoManuale.aspx.vb" Inherits="MOROSITA_PagamentoManuale" %>

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
            event.keyCode=0;
              
            document.getElementById('USCITA').value = '0';
            document.getElementById('txtModificato').value = '111';   
        }  
    }

    var r = {
        'special': /[\W]/g,
        'quotes': /['\''&'\"']/g,
        'notnumbers': /[^\d\-\,]/g
    }
    
    
    function valid(o, w) {
        o.value = o.value.replace(r[w], '');
//        o.value = o.value.replace('.', ',');
        document.getElementById('txtModificato').value = '1';
    }
	
	
    function DelPointer(obj) {
	    obj.value = obj.value.replace('.', '');
	    document.getElementById(obj.id).value = obj.value;
	}

    function $onkeydown() {
        if (event.keyCode == 46) {
            event.keyCode = 0;
        }
    }
	
	function AutoDecimal2(obj) {
        obj.value = obj.value.replace('.', '');
        if (obj.value.replace(',', '.') != 0) 
        {
            var a = obj.value.replace(',', '.');
            a = parseFloat(a).toFixed(2)
            if (a.substring(a.length - 3, 0).length >= 4) 
            {
                var decimali = a.substring(a.length, a.length - 2);
                var dascrivere = a.substring(a.length - 3, 0);
                var risultato = '';
                while (dascrivere.replace('-', '').length >= 4) {

                risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato 
                dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
            }
                risultato = dascrivere + risultato + ',' + decimali
                //document.getElementById(obj.id).value = a.replace('.', ',')
                document.getElementById(obj.id).value = risultato
            }
            else 
            {
                document.getElementById(obj.id).value = a.replace('.', ',')
            }
        }
        }

function CompletaData(e,obj) {
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

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
	    <base target="_self"/>
		<title>Pagamento Manuale Bolletta Morosità</title>
		
		<script language="javascript" type="text/javascript">
            var Uscita;
            Uscita=0;
        </script>
		
		<script language="javascript" type="text/javascript">

            function ConfermaAnnulloConsuntivo() {
                var sicuro = confirm('Sei sicuro di voler cancellare questo consuntivo?');
                if (sicuro == true) {
                document.getElementById('txtannullo').value='1';
                }
                else
                {
                document.getElementById('txtannullo').value='0'; 
                }
            }


            function ConfermaEsci()
            {
             //alert('ciao');
             if (document.getElementById('txtModificato').value=='1') 
             {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
                if (chiediConferma == false) {
                    document.getElementById('txtModificato').value='111';
                    //document.getElementById('USCITA').value='0';
                }
             } 
            } 

   
</script>
		
	</head>
	<body bgcolor="#f2f5f1" onload="document.getElementById('btnSalva').focus()">
	<script type="text/javascript">
document.onkeydown=$onkeydown;
</script>

		<form id="Form1" method="post" runat="server">
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); width: 800px;
                position: absolute; top: 0px">
                <tr>
                    <td style="width: 800px">
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Inserimento
                            Manuale Pagamento</strong></span><br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            
                            Style="z-index: 102; left: 202px; position: absolute; top: 71px; width: 107px;" Visible="False">Bollettino MAV BPS</asp:Label>
                        <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 9px; position: absolute; top: 71px" Width="69px">Num. Bolletta</asp:Label>
                        <br />
                        &nbsp;&nbsp;
                        <hr style="left: 91px; width: 701px; position: absolute; top: 116px" />
                        <br />
                        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 9px; position: absolute; top: 107px" Width="80px">DATI BOLLETTA</asp:Label>
                        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 418px; position: absolute; top: 146px" 
                            Width="42px">Indirizzo</asp:Label>
                        <asp:Label ID="lblIndirizzo" runat="server" Font-Bold="True" Font-Names="Arial"
                            Font-Size="8pt" 
                            Style="z-index: 102; left: 461px; position: absolute; top: 146px; width: 325px;"></asp:Label>
                        <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 9px; position: absolute; top: 146px" Width="52px">Intestatario</asp:Label>
                        <br />
                        <br />
                        &nbsp;<br />
                        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            
                            Style="z-index: 102; left: 528px; position: absolute; top: 232px; width: 66px;" Visible="False">Num. Bolletta</asp:Label>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataPagamento"
                            ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 195px; position: absolute;
                            top: 229px" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                            Width="1px"></asp:RegularExpressionValidator>
                        <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            
                            Style="z-index: 102; left: 9px; position: absolute; top: 173px; width: 37px;">Periodo</asp:Label>
                        <asp:Label ID="lblPeriodo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            
                            Style="z-index: 102; left: 90px; position: absolute; top: 173px;" Height="14px" Width="320px"></asp:Label>
                        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 418px; position: absolute; top: 173px" 
                            Width="35px">Importo</asp:Label>
                        <asp:Label ID="lblImporto" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 461px; position: absolute; top: 173px" 
                            Width="106px"></asp:Label>
                        <asp:Label ID="lblNumero" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            
                            Style="z-index: 102; left: 608px; position: absolute; top: 232px; width: 89px; height: 14px;" Visible="False"></asp:Label>
                        <br />
                        <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 9px; position: absolute; top: 200px" Width="75px">Data Emissione</asp:Label>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator6" runat="server" ControlToValidate="txtImporto"
                            ErrorMessage="!" Font-Bold="True" Height="1px" Style="z-index: 1; left: 391px;
                            position: absolute; top: 230px" ValidationExpression="\d+(\.\d\d)?(,\d\d)?(\.\d)?(,\d)?"
                            Width="1px"></asp:RegularExpressionValidator>
                        <asp:Label ID="lblEmissione" runat="server" Font-Bold="True" Font-Names="Arial"
                            Font-Size="8pt" Style="z-index: 102; left: 87px; position: absolute; top: 200px"
                            Width="88px"></asp:Label>
                        <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 183px; position: absolute; top: 200px" 
                            Width="76px">Data Scadenza</asp:Label>
                        <asp:Label ID="lblScadenza" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 263px; position: absolute; top: 200px" 
                            Width="146px"></asp:Label>
                        <br />
                        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Blue" Style="z-index: 102; left: 9px; position: absolute; top: 228px"
                            Width="90px">Data Pagamento</asp:Label>
                        &nbsp; &nbsp;
                        <br />
                        <br />
                        <asp:TextBox ID="txtNumBolletta0" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="25" 
                            Style="z-index: 103; left: 302px; position: absolute; top: 69px; width: 157px;" 
                            TabIndex="2" Visible="False"></asp:TextBox>
                        <asp:TextBox ID="txtNumBolletta" runat="server" BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="10" 
                            Style="z-index: 103; left: 81px; position: absolute; top: 69px" Width="105px" 
                            TabIndex="1"></asp:TextBox>
                        <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 102; left: 9px; position: absolute; top: 279px" 
                            Width="25px">Note</asp:Label>
                            <asp:Label ID="Label12" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            
                            Style="z-index: 102; left: 102px; position: absolute; top: 250px; width: 299px;" 
                            Font-Italic="True">Attenzione...riportare la data effettiva del pagamento!</asp:Label>
                        <br />
                        <asp:Label ID="Label11" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Blue" Style="z-index: 102; left: 206px; position: absolute; top: 228px"
                            Width="88px">Importo Pagato</asp:Label>
                        &nbsp;
                        <asp:TextBox ID="txtNote" runat="server" BorderStyle="Solid" BorderWidth="1px" Height="57px"
                            MaxLength="500" Style="z-index: 103; left: 38px; position: absolute; top: 284px"
                            TabIndex="6" TextMode="MultiLine" Width="489px"></asp:TextBox>
                        <asp:TextBox ID="txtImporto" runat="server" BorderStyle="Solid" 
                            BorderWidth="1px" MaxLength="10"
                            Style="z-index: 103; left: 297px; position: absolute; top: 228px" TabIndex="5"
                            Width="88px"></asp:TextBox>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Red" Height="16px" Style="z-index: 104; left: 9px; position: absolute;
                            top: 395px" Visible="False" Width="525px"></asp:Label>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                    </td>
                </tr>
            </table>
            <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                Style="z-index: 101; left: 660px; position: absolute; top: 441px" 
                TabIndex="8" ToolTip="Home" OnClientClick="document.getElementById('USCITA').value='1';ConfermaEsci();" />
            <asp:ImageButton ID="btnSalva" 
                runat="server" ImageUrl="~/NuoveImm/Img_SalvaGrande.png"
                Style="z-index: 100; left: 575px; position: absolute; top: 441px; height: 20px;" 
                TabIndex="7" ToolTip="Salva" />
            <asp:ImageButton ID="btnCerca" runat="server" ImageUrl="~/NuoveImm/Img_AvviaRicerca.png"
                Style="z-index: 101; left: 471px; position: absolute; top: 69px" 
                TabIndex="3" ToolTip="Avvia Ricerca" Visible="False" />
									<asp:label id="lblIntestatario" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 102; left: 87px; position: absolute; top: 146px; width: 300px; height: 15px;"></asp:label>
									<asp:textbox id="txtDataPagamento" tabIndex="4" 
                runat="server" 
                style="z-index: 103; left: 102px; position: absolute; top: 228px; right: 888px;" 
                BorderStyle="Solid" BorderWidth="1px" MaxLength="10" ToolTip="dd/MM/yyyy" 
                Width="88px"></asp:textbox>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;


        <asp:TextBox ID="USCITA"            runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
	    <asp:TextBox ID="txtModificato"     runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>

		</form>

	</body>
</html>
