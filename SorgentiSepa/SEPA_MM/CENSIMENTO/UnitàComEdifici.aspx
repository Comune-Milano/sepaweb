<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UnitàComEdifici.aspx.vb" Inherits="CENSIMENTO_UnitàComEdifici" %>

<%@ Register Src="Tab_AdDimens.ascx" TagName="Tab_AdDimens" TagPrefix="uc1" %>

<%@ Register src="Tab_ConsUnitaComuni.ascx" tagname="Tab_ConsUnitaComuni" tagprefix="uc3" %>

<%@ Register src="Tab_EdifAssociati.ascx" tagname="Tab_EdifAssociati" tagprefix="uc2" %>

<%@ Register src="Tab_Caratteristiche.ascx" tagname="Tab_Caratteristiche" tagprefix="uc4" %>

<%@ Register src="Tab_AnCantine.ascx" tagname="Tab_AnCantine" tagprefix="uc5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml" >
        <script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
        <script type="text/javascript" src="../Contratti/moo.fx.js"></script>
        <script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>

	<head runat="server">

		<title>Inserimento Unità Comuni</title>
		            		<script type="text/javascript">
		            		    document.write('<style type="text/css">.tabber{display:none;}<\/style>');

		            		    //var tabberOptions = {'onClick':function(){alert("clicky!");}};
		            		    var tabberOptions = {


		            		        /* Optional: code to run when the user clicks a tab. If this
		            		        function returns boolean false then the tab will not be changed
		            		        (the click is canceled). If you do not return a value or return
		            		        something that is not boolean false, */

		            		        'onClick': function(argsObj) {

		            		            var t = argsObj.tabber; /* Tabber object */
		            		            var id = t.id; /* ID of the main tabber DIV */
		            		            var i = argsObj.index; /* Which tab was clicked (0 is the first tab) */
		            		            var e = argsObj.event; /* Event object */

		            		            document.getElementById('txttab').value = i + 1;
		            		        },
		            		        'addLinkId': true



		            		    };
            </script>
    <script type="text/javascript" src="tabber.js"></script>
	<link rel="stylesheet" href="example.css" type="text/css"  media="screen" />

		    <style>
    .CssMaiuscolo { TEXT-TRANSFORM: uppercase}
		</style>
	        <script language="javascript" type="text/javascript">
//        window.onbeforeunload = confirmExit; 
        function ConfermaEsci(){
            if (document.getElementById('txtModificato').value=='1') {
            var chiediConferma
            chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Continuare l\'operazione senza aver salvato?");
            if (chiediConferma == false) {
                    document.getElementById('txtModificato').value='111';
        //document.getElementById('USCITA').value='0';
                                            }
                                                                        }
                                  } 

        </script>
        <script language="javascript" type="text/javascript">
            var Uscita;
            Uscita = 0;

            var r = {
                'special': /[\W]/g,
                'quotes': /['\''&'\"']/g,
                'notnumbers': /[^\d]/g
            }
            function valid(o, w) {
                o.value = o.value.replace(r[w], '');
            }
    
</script>
</head>
	<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat :no-repeat ">

    <form id="form1" runat="server">
   
            <asp:HiddenField ID="CENS_MANUT_SL" runat="server" Value="0" />

	            <script language="javascript" type="text/javascript">

	                function scDaAprire() {
	                
            var ins = <%=vId %> 
            if (ins == 0){
            alert('SALAVARE LA NUOVA UNITA\' PRIMA DI APRIRE UNA SCHEDA!');
            return ;
            }
	                    if (navigator.userAgent.toLowerCase().indexOf("msie") != -1) {
	                        var objSchede = document.getElementById("DrlSchede");
	                        var objPeriodo = document.getElementById("cmbPeriodo");
	                        var I = objSchede.options[objSchede.selectedIndex].value
	                        var D = objPeriodo.options[objPeriodo.selectedIndex].innerText
	                        if (D != '') {
	                            D = D.substring(6) + D.substring(3, 5) + D.substring(0, 2)
	                        }
	                        //            

	                    }
	                    else {
	                        var objSchede = document.getElementById("DrlSchede");
	                        var objPeriodo = document.getElementById("cmbPeriodo");
	                        var I = objSchede.options[objSchede.selectedIndex].value
	                        var D = objPeriodo.options[objPeriodo.selectedIndex].text
	                        if (D != '') {
	                            D = D.substring(6) + D.substring(3, 5) + D.substring(0, 2)
	                        }

	                    }
            switch (I) {
                case '0':
                   window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=UC&TSCHEDA=A&TITSCHEDA=RILIEVO STRUTTURE&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '1':
                    window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=UC&TSCHEDA=B&TITSCHEDA=SCHEDA RILIEVO CHIUSURE&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '2':
                    window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=UC&TSCHEDA=C&TITSCHEDA=SCHEDA RILIEVO PARTIZIONI INTERNE&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '3':
                    window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=UC&TSCHEDA=D&TITSCHEDA=SCHEDA RILIEVO PAVIMENTAZIONI INTERNE&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '4':
                     window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=UC&TSCHEDA=E&TITSCHEDA=SCHEDA RILIEVO PROTEZIONE E DELIMITAZIONI&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '5':
                    window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=UC&TSCHEDA=F&TITSCHEDA=SCHEDA RILIEVO ATTREZZATURE E SPAZI INTERNI&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '6':
                    window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=UC&TSCHEDA=G&TITSCHEDA=SCHEDA RILIEVO ATTREZZATURE ED ARREDI ESTERNI&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '7':
                     window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=UC&TSCHEDA=H&TITSCHEDA=SCHEDA RILIEVO IMPIANTI FISSI DI TRASPORTO&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '8':
                     window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=UC&TSCHEDA=I&TITSCHEDA=SCHEDA RILIEVO IMPIANTI RISCALDAMENTO E PRODUZIONE H2O CENTRALIZZATA&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '9':
                     window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=UC&TSCHEDA=L&TITSCHEDA=SCHEDA RILIEVO IMPIANTI IDRICO SANITARI&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '10':
                    window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=UCTSCHEDA=M&TITSCHEDA=SCHEDA RILIEVO IMPIANTI ANTINCENDIO&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '11':
                    window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=UC&TSCHEDA=N&TITSCHEDA=SCHEDA RILIEVO RETE SCARICO / FOGNARIA&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '12':
                    window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=UC&TSCHEDA=O&TITSCHEDA=SCHEDA RILIEVO IMPIANTI SMALTIMENTO AERIFORMI&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '13':
                     window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=UC&TSCHEDA=P&TITSCHEDA=SCHEDA RILIEVO INPIANTO DI DISTRIBUZIONE GAS&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '14':
                     window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=UC&TSCHEDA=Q&TITSCHEDA=SCHEDA RILIEVO IMPIANTI ELETTRICI&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '15':
                     window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=UC&TSCHEDA=R&TITSCHEDA=SCHEDA RILIEVO IMPIANTI TELEVISIVI&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '16':
                     window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=UCF&TSCHEDA=S&TITSCHEDA=SCHEDA RILIEVO IMPIANTI CITOFONI&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '17':
                    window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=UC&TSCHEDA=T&TITSCHEDA=SCHEDA RILIEVO IMPIANTI DI TELECOMUNICAZIONE&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
              
                default:
                    alert('Selezionare una scheda da aprire!')
            }
                            
        }
            </script>

        <asp:Label ID="Label42" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            
            
            Style="z-index: 100; left: 319px; position: absolute; top: 76px; height: 14px; width: 120px;">Denominazione - Codice</asp:Label>
        <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            
            Style="z-index: 100; left: 320px; position: absolute; top: 45px; height: 14px; width: 120px;">Denominazione - Codice</asp:Label>
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial">
        <asp:HiddenField ID="txtindietro" runat="server" Value="-1" />


        <asp:HiddenField ID="txtModificato" runat="server" Value="0" />

                        <asp:HiddenField ID="USCITA" runat="server" Value="0" />
        </span>

                                <asp:HiddenField ID="txttab" runat="server" Value="1" />
                                
            &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="imgStampa" runat="server" ImageUrl="~/NuoveImm/Img_Stampa.png"
                
                Style="z-index: 100; left: 108px; position: absolute; top: 27px; right: 518px;" 
                ToolTip="Stampa" OnClientClick="ConfermaEsci();" TabIndex="14" Visible="False" />
            &nbsp;
                        <br />
            <asp:Label ID="Label44" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                
            Style="z-index: 100; left: 250px; position: absolute; top: 165px; right: 894px; width: 47px;">Dest. Uso</asp:Label>
            <asp:Label ID="Label43" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 18px; position: absolute; top: 165px; right: 1101px; bottom: 459px;" 
                Width="60px">Ubicazione</asp:Label>
                        <br />
		<asp:DropDownList ID="CmbUbicazione" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="9pt" Height="20px" 
                Style="border: 1px solid black; z-index: 111; left: 101px; position: absolute; top: 165px" 
                TabIndex="5" Width="146px">
        </asp:DropDownList>
       
                        <br />
		<asp:DropDownList ID="CmbDestUso" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="9pt" Height="20px" 
                Style="border: 1px solid black; z-index: 111; left: 298px; position: absolute; top: 165px; width: 150px;" 
                TabIndex="5">
        </asp:DropDownList>
       
            <asp:Label ID="Label45" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                
            Style="z-index: 100; left: 449px; position: absolute; top: 165px; right: 695px;" Width="45px">St.Fisico</asp:Label>
                        <br />
		<asp:DropDownList ID="CmbStFisico" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="9pt" Height="20px" 
                Style="border: 1px solid black; z-index: 111; left: 492px; position: absolute; top: 165px;" 
                TabIndex="5" Width="269px">
        </asp:DropDownList>
       
                        <br />
        <asp:DropDownList ID="DrlSchede" runat="server" Enabled="False" Style="left: 98px;
            position: absolute; top: 243px; z-index: 143; height: 16px; width: 570px;" 
            TabIndex="5">
        </asp:DropDownList>
                        <br />
            <img border="0" alt="Apri Scheda Rilievo" id="imgSchede" 
            src="../NuoveImm/Img_SchedaRilievo.png" 
            style="cursor: pointer; left: 10px; position: absolute; top: 244px;" 
            
            onclick="scDaAprire();"/><br />
                        <br />
                        <asp:DropDownList ID="cmbPeriodo" runat="server" 
                style = "position : absolute; top: 243px; left: 670px; width: 90px;" 
            Enabled="False" >
            </asp:DropDownList>
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        
                        <div id= "MyTab" class="tabber" style="text-align: left;" >
                            <div class= "<%=classetab %> <%=tabdefault1%>" title ="DIMENSIONI">
                              <uc1:Tab_AdDimens ID="Tab_AdDimens1" runat="server" />
                            </div>
                            <div class= "<%=classetabED %>  <%=tabdefault2%>" title ="EDIFICI">
                                <uc2:Tab_EdifAssociati ID="Tab_EdifAssociati1" runat="server" />
                            </div>
                            <div class= "tabbertab <%=tabdefault3%>" title ="NOTE">
                                <asp:TextBox ID="txtNote" runat="server" Height="135px" TextMode="MultiLine" 
                                    Width="703px"></asp:TextBox>
                            </div>
                            <div class= "<%=classetab %> <%=tabdefault4%>" title ="CARATTERISTICHE">
                                
                                <uc4:Tab_Caratteristiche ID="Tab_Caratteristiche1" runat="server" />
                                
                            </div>
                            <div class= "<%=classetab %> <%=tabdefault5%>" title ="ANOMALIE">
                         
                                <uc5:Tab_AnCantine ID="Tab_AnCantine1" runat="server" />
                         
                            </div>

                        </div>
             <a href="javascript:" onclick="history.go(document.getElementById('txtindietro').value); return false"></a> 
            &nbsp; &nbsp;
            &nbsp; &nbsp;&nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 18px; position: absolute; top: 114px" 
                Width="56px">Tipologia *</asp:Label>
            &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 18px; position: absolute; top: 140px; right: 1073px;" 
                Width="60px">Disponibilità</asp:Label>
            <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                
            
            
            Style="z-index: 100; left: 18px; position: absolute; top: 193px; width: 74px;">Localizzazione*</asp:Label>
            &nbsp; &nbsp; &nbsp;
            <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                
            
            Style="z-index: 100; left: 454px; position: absolute; top: 190px; width: 85px;">N. Piani Ascens.</asp:Label>
            &nbsp;
            <asp:TextBox ID="TxtNumPianiAsc" runat="server" Style="left: 542px; position: absolute;
                top: 190px; z-index: 2;" Width="31px" MaxLength="4" 
            CssClass="CssMaiuscolo" TabIndex="9"></asp:TextBox>
            &nbsp;&nbsp;
            <asp:DropDownList ID="DrLTipoUnitComune" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="9pt" Height="20px" 
                Style="border: 1px solid black; z-index: 111; left: 101px; position: absolute; top: 114px; right: 865px;" 
                TabIndex="3" Width="347px">
            </asp:DropDownList>
            <asp:TextBox ID="TxtLocalUnita" runat="server" Style="left: 101px; position: absolute;
                top: 192px; z-index: 2;" MaxLength="100" 
            TextMode="MultiLine" CssClass="CssMaiuscolo" TabIndex="10" Height="45px" Width="341px"></asp:TextBox>
            &nbsp; &nbsp;&nbsp;
            <asp:Label ID="lbledificio" runat="server" Font-Bold="False" 
            Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 18px; position: absolute; top: 89px" 
                Width="56px">Edificio*</asp:Label>
            <asp:DropDownList ID="DrLEdificio" runat="server" AutoPostBack="True" BackColor="White"
                Font-Names="arial" Font-Size="9pt" Height="20px" 
                Style="border: 1px solid black; z-index: 111; left: 100px; position: absolute; top: 89px;" 
                TabIndex="2" Width="660px">
            </asp:DropDownList>
            <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 455px; position: absolute; top: 215px" 
            Width="68px">N. Piani Scale</asp:Label>
            <asp:TextBox ID="TxtNumPianiScale" runat="server" MaxLength="4" Style="left: 542px; position: absolute;
                top: 215px; z-index: 2;" Width="31px" CssClass="CssMaiuscolo" 
            TabIndex="7"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
                Style="left: 734px; position: absolute; top: 27px; z-index: 250;" 
            ToolTip="Esci" OnClientClick="ConfermaEsci();" TabIndex="17" />
		<asp:DropDownList ID="DrLDispUnitComune" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="9pt" Height="20px" 
                Style="border: 1px solid black; z-index: 111; left: 101px; position: absolute; top: 140px" 
                TabIndex="5" Width="146px">
        </asp:DropDownList>
       
        <asp:Label ID="Lblcomplesso" runat="server" Font-Bold="False" 
            Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 18px; position: absolute; top: 57px" Width="61px" 
                TabIndex="1">Complesso*</asp:Label>
            &nbsp;
         <asp:DropDownList ID="DLRComplessi" runat="server" AutoPostBack="True" BackColor="White"
                Font-Names="arial" Font-Size="9pt" Height="20px" 
                
            Style="border: 1px solid black; z-index: 111; left: 100px; position: absolute; top: 57px;" 
            TabIndex="1" Width="660px">
         </asp:DropDownList>
         
            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
                Style="z-index: 100; left: 59px; position: absolute; top: 27px; height: 12px;" 
                ToolTip="Salva" TabIndex="13" /><asp:ImageButton ID="BTNiNDIETRO" 
                runat="server" ImageUrl="~/NuoveImm/Img_Indietro.png"
                Style="z-index: 100; left: 10px; position: absolute; top: 27px" 
                ToolTip="Indietro" OnClientClick="ConfermaEsci();" TabIndex="22" />
            &nbsp;
            &nbsp;&nbsp;
            <asp:Label ID="LblErrore" runat="server" Style="left: 18px; position: absolute; top: 493px; z-index: 2;"
                Text="Label" Width="616px" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Red" Visible="False"></asp:Label>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" 
            runat="server" ControlToValidate="TxtNumPianiAsc"
                ErrorMessage="!" Font-Bold="True" Style="left: 579px; position: absolute;
                top: 193px; z-index: 2;" ValidationExpression="\d{1,50}" 
                ToolTip="E' possibile inserire solo numeri" Height="17px" Width="1px"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" 
            runat="server" ControlToValidate="TxtNumPianiScale"
                ErrorMessage="!" Font-Bold="True" Style="left: 579px; position: absolute;
                top: 217px; z-index: 2; width: 7px; height: 16px;" ValidationExpression="\d{1,50}" 
                ToolTip="E' possibile inserire solo numeri"></asp:RegularExpressionValidator>
            <asp:Label ID="Label41" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                
                Style="z-index: 100; left: 458px; position: absolute; top: 115px; height: 13px; width: 24px;" 
                ForeColor="Gray">Cod.</asp:Label>
            <asp:TextBox ID="txtCodUnitCom" runat="server" Enabled="False" 
            MaxLength="20" Style="left: 491px;
                position: absolute; top: 113px; z-index: 2;" 
            TabIndex="4" Width="172px"></asp:TextBox>
            <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 456px; position: absolute; top: 141px; right: 942px;" 
                Width="29px">Scala</asp:Label>
            <asp:DropDownList ID="DrlSc" runat="server" BackColor="White" Font-Names="arial"
                Font-Size="9pt" Height="20px" 
                Style="border: 1px solid black; z-index: 111; left: 492px; position: absolute; top: 141px; width: 170px;" 
                TabIndex="6">
            </asp:DropDownList>
            <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 249px; position: absolute; top: 140px" 
            Width="88px">Tipo Livello piano*</asp:Label>
            <asp:DropDownList ID="DrLTipoLivPiano" runat="server" BackColor="White" Font-Names="arial"
                Font-Size="9pt" Height="20px" 
            Style="border: 1px solid black; z-index: 111; left: 336px; position: absolute; top: 141px" 
            TabIndex="8" Width="112px">
            </asp:DropDownList>
            &nbsp;
            &nbsp; &nbsp;
                                <asp:HiddenField ID="txttab0" runat="server" Value="1" />
        <img id="ImgEventi" alt="Eventi" border="0" onclick="window.open('Eventi.aspx?ID=<%=vId %> &CHIAMA=UC','Eventi', '');"
            src="../NuoveImm/Img_Eventi.png" style="left: 672px; cursor: pointer; position: absolute;
            top: 27px; z-index: 500;" />
                                
	</form>
            <script  language="javascript" type="text/javascript">
                document.getElementById('dvvvPre').style.visibility = 'hidden';


    </script>
	</body>
</html> 
