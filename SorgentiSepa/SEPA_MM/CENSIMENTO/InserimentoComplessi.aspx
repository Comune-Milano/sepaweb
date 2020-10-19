<%@ Page Language="VB" AutoEventWireup="false" CodeFile="InserimentoComplessi.aspx.vb" Inherits="CENSIMENTO_InserimentoComplessi" %>

<%@ Register Src="Tab_UtMillesimali.ascx" TagName="Tab_UtMillesimali" TagPrefix="uc2" %>

<%@ Register Src="Tab_Millesimali.ascx" TagName="Tab_Millesimali" TagPrefix="uc1" %>

<%@ Register src="Tab_ComEdifici.ascx" tagname="Tab_ComEdifici" tagprefix="uc3" %>

<%@ Register src="Tab_Impianti.ascx" tagname="Tab_Impianti" tagprefix="uc4" %>

<%@ Register src="Tab_UnComuni.ascx" tagname="Tab_UnComuni" tagprefix="uc5" %>

<%@ Register src="Tab_ImpComuni.ascx" tagname="Tab_ImpComuni" tagprefix="uc6" %>

<%@ Register src="Tab_Servizi.ascx" tagname="Tab_Servizi" tagprefix="uc7" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
   <script type="text/javascript" src="prototype.lite.js"></script>
   <script type="text/javascript" src="moo.fx.js"></script>
   <script type="text/javascript" src="moo.fx.pack.js"></script>

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



		    }
		    
</script>

<script language="javascript" type="text/javascript">
            var Uscita;
            Uscita=0;

            var r = {
                'special': /[\W]/g,
                'quotes': /['\''&'\"']/g,
                'notnumbers': /[^\d]/g
            }
            function valid(o, w) {
                o.value = o.value.replace(r[w], '');
            }
</script>
        <script language="javascript" type="text/javascript">
        function ConfermaEsci(){
            if (document.getElementById('txtModificato').value=='1') {
            var chiediConferma
            chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Continuare l\'operazione senza aver salvato?");
            if (chiediConferma == false) {
                    document.getElementById('txtModificato').value='111';
        document.getElementById('USCITA').value='0';
                                            }
                                                                        }
}


function ApriFotoPlan() {

    window.open('FotoImmobile.aspx?T=C&ID=<%=vId %>&I=<%=vIdIndirizzo %>', '');
}
        </script>
        <title>Inserimento Complessi</title>

		<script type="text/javascript" src="tabber.js"></script>
        <link rel="stylesheet" href="example.css" type="text/css" media="screen"/>

</head>   
	<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat :no-repeat">
            <script language="javascript" type="text/javascript">

            function scDaAprire() {
            var ins = <%=vId %> 
            if (ins == 0){
            alert('SALAVARE IL NUOVO COMPLESSO PRIMA DI APRIRE UNA SCHEDA!');
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
                   window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=COMP&TSCHEDA=A&TITSCHEDA=RILIEVO STRUTTURE&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '1':
                    window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=COMP&TSCHEDA=B&TITSCHEDA=SCHEDA RILIEVO CHIUSURE&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '2':
                    window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=COMP&TSCHEDA=C&TITSCHEDA=SCHEDA RILIEVO PARTIZIONI INTERNE&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '3':
                    window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=COMP&TSCHEDA=D&TITSCHEDA=SCHEDA RILIEVO PAVIMENTAZIONI INTERNE&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '4':
                     window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=COMP&TSCHEDA=E&TITSCHEDA=SCHEDA RILIEVO PROTEZIONE E DELIMITAZIONI&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '5':
                    window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=COMP&TSCHEDA=F&TITSCHEDA=SCHEDA RILIEVO ATTREZZATURE E SPAZI INTERNI&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '6':
                    window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=COMP&TSCHEDA=G&TITSCHEDA=SCHEDA RILIEVO ATTREZZATURE ED ARREDI ESTERNI&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '7':
                     window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=COMP&TSCHEDA=H&TITSCHEDA=SCHEDA RILIEVO IMPIANTI FISSI DI TRASPORTO&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '8':
                     window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=COMP&TSCHEDA=I&TITSCHEDA=SCHEDA RILIEVO IMPIANTI RISCALDAMENTO E PRODUZIONE H2O CENTRALIZZATA&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '9':
                     window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=COMP&TSCHEDA=L&TITSCHEDA=SCHEDA RILIEVO IMPIANTI IDRICO SANITARI&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '10':
                    window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=COMP&TSCHEDA=M&TITSCHEDA=SCHEDA RILIEVO IMPIANTI ANTINCENDIO&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '11':
                    window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=COMP&TSCHEDA=N&TITSCHEDA=SCHEDA RILIEVO RETE SCARICO / FOGNARIA&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '12':
                    window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=COMP&TSCHEDA=O&TITSCHEDA=SCHEDA RILIEVO IMPIANTI SMALTIMENTO AERIFORMI&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '13':
                     window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=COMP&TSCHEDA=P&TITSCHEDA=SCHEDA RILIEVO INPIANTO DI DISTRIBUZIONE GAS&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '14':
                     window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=COMP&TSCHEDA=Q&TITSCHEDA=SCHEDA RILIEVO IMPIANTI ELETTRICI&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '15':
                     window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=COMP&TSCHEDA=R&TITSCHEDA=SCHEDA RILIEVO IMPIANTI TELEVISIVI&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '16':
                     window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=COMP&TSCHEDA=S&TITSCHEDA=SCHEDA RILIEVO IMPIANTI CITOFONI&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
                case '17':
                    window.open("../Manutenzioni/ScProva.aspx?ID=<%=vId %>&TIPO=COMP&TSCHEDA=T&TITSCHEDA=SCHEDA RILIEVO IMPIANTI DI TELECOMUNICAZIONE&DATA=" + D +'&SL='+ document.getElementById("CENS_MANUT_SL").value , '', 'scrollbars=yes, resizable=yes');
                    break;
              
                default:
                    alert('Selezionare una scheda da aprire!')
            }
                            
        }
            </script>

    <form id="form1" runat="server" >
            <asp:HiddenField ID="txtVisibility" runat="server" Value="0" />
            <asp:HiddenField ID="txtInfoVisible" runat="server" Value="0" />
            <asp:HiddenField ID="txtLottoName" runat="server" Value="0" />

            &nbsp; &nbsp;&nbsp;
            <asp:HiddenField ID="CENS_MANUT_SL" runat="server" Value="0" />

            <asp:ImageButton ID="imgUscita" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
              Style="z-index: 102; left: 731px;
                position: absolute; top: 27px; cursor: pointer; " 
                ToolTip="Esci" OnClientClick="ConfermaEsci();"
                TabIndex="22" />
            <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
                
                Style="z-index: 101; left: 55px; position: absolute; top: 27px; cursor: pointer; height: 12px;" 
                ToolTip="Salva" TabIndex="18" />
            &nbsp;
            <asp:ImageButton ID="imgStampa" runat="server" ImageUrl="~/NuoveImm/Img_Stampa.png"
                
                Style="z-index: 102; left: 97px; position: absolute; top: 27px; cursor: pointer;" 
                ToolTip="Stampa" OnClientClick="ConfermaEsci();"  Visible="False" TabIndex="19" />
            &nbsp; &nbsp;<a href="javascript:" onclick="history.go(document.getElementById('txtindietro').value); return false"></a>
        
            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                
                Style="z-index: 100; left: 426px; position: absolute; top: 134px; width: 81px;" 
                Height="17px">Tipo Complesso*</asp:Label>
            <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                
                Style="z-index: 100; left: 10px; position: absolute; top: 159px; bottom: 669px;" 
                Width="96px">Codice Ubicazione legge 392/78*</asp:Label>
            <asp:DropDownList ID="DdLCodUbicaz" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="9pt" Height="20px" 
                Style="border: 1px solid black; z-index: 111; left: 114px; position: absolute; top: 165px;" 
                TabIndex="6" Width="308px">
            </asp:DropDownList>
            &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
            &nbsp;&nbsp;
            <asp:DropDownList ID="DdlLivelloPossesso" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="9pt" Height="20px" 
                Style="border: 1px solid black; z-index: 111; left: 114px; position: absolute; top: 135px;" 
                TabIndex="4" Width="308px">
            </asp:DropDownList>
            <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 10px; position: absolute; top: 135px" Width="98px">Livello di Possesso*</asp:Label>
                <asp:DropDownList ID="DrLTipoComplesso" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="9pt" Height="20px" 
                Style="border: 1px solid black; z-index: 111; left: 513px; position: absolute; top: 135px;" 
                TabIndex="5" Width="260px">
        </asp:DropDownList>
		
        <asp:Label ID="Lbl3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 76px; position: absolute; top: 72px" Width="32px">Lotto</asp:Label>
            &nbsp;
        <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 219px; position: absolute; top: 72px" Width="64px" ForeColor="Black">Cod.Complesso</asp:Label>
        <asp:TextBox ID="TxtCodComplesso" runat="server" MaxLength="7" Style="left: 299px;
            position: absolute; top: 72px; z-index: 5;" Width="117px" ReadOnly="True" TabIndex="2"></asp:TextBox>
        <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 489px; position: absolute; top: 22px" Width="48px" 
                Visible="False">Cod.GIMI</asp:Label>
        <asp:TextBox ID="TxtCodGimi" runat="server" MaxLength="50" Style="left: 546px; position: absolute;
            top: 27px; z-index: 5; height: 6px; width: 32px;" Visible="False"></asp:TextBox>
        <asp:Label ID="Label16" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            
                Style="z-index: 100; left: 427px; position: absolute; top: 106px; width: 82px;">N° Passi Carrabili</asp:Label>
        <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 11px; position: absolute; top: 106px" Width="96px">Den.Complesso*</asp:Label>
        <asp:TextBox ID="TxtDenComplesso" runat="server" Style="left: 113px;
            position: absolute; top: 105px; z-index: 5;" MaxLength="100" 
                CssClass="CssMaiuscolo" TabIndex="7" Width="300px"></asp:TextBox>
            &nbsp;&nbsp;
            <img border="0" alt="Apri Scheda Rilievo" id="imgSchede" 
            src="../NuoveImm/Img_SchedaRilievo.png" 
            style="cursor: pointer; left: 10px; position: absolute; top: 277px; right: 1095px;" 
            
            onclick="scDaAprire();"/>
        <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            Style="z-index: 100; left: 427px; position: absolute; top: 166px" Width="68px">Provenienza*</asp:Label>
            &nbsp; &nbsp;<br />
            &nbsp;
            <asp:DropDownList ID="DrLProvenienza" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="9pt" Height="20px" 
                Style="border: 1px solid black; z-index: 100; left: 513px; position: absolute; top: 165px" 
                TabIndex="8" Width="260px">
            </asp:DropDownList>
        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
            <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 433px; position: absolute; top: 72px" Width="40px">Gestore</asp:Label>
            <asp:DropDownList ID="DrLGestore" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="9pt" Height="20px" Style="border-right: black 1px solid;
                border-top: black 1px solid; z-index: 111; left: 481px; border-left: black 1px solid;
                border-bottom: black 1px solid; position: absolute; top: 72px" TabIndex="3" Width="200px">
            </asp:DropDownList>
            &nbsp; &nbsp;
        <asp:DropDownList ID="DrLLotto" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="9pt" Height="20px" Style="border-right: black 1px solid;
                border-top: black 1px solid; z-index: 10; left: 116px; border-left: black 1px solid;
                border-bottom: black 1px solid; position: absolute; top: 72px" Width="96px" TabIndex="1">
                </asp:DropDownList>
        &nbsp;&nbsp;
            &nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
            <asp:TextBox ID="txtindietro" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" MaxLength="100" Style="left: 41px; position: absolute;z-index :-1;  top: 352px; background-color: white;"
                Width="19px" ForeColor="White" TabIndex="-1">0</asp:TextBox>
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/NuoveImm/Img_Indietro.png"
                Style="left: 7px; position: absolute; top: 27px; z-index: 100; cursor: pointer;" ToolTip="Indietro" OnClientClick="ConfermaEsci();" TabIndex="17" />
            <asp:Label ID="LblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="Red" 
                Style="left: 263px; position: absolute; top: 23px; z-index: 102;" Text="Label"
                Visible="False" Width="485px"></asp:Label>
                        <br />
                           <img border="0" alt="Eventi" id="ImgEventi" 
            src="../NuoveImm/Img_Eventi.png" 
            style="cursor: pointer; left: 674px; position: absolute; top: 27px;" 
            
            onclick="window.open('Eventi.aspx?ID=<%=vId %> &CHIAMA=CO','Eventi', '');"/><asp:ImageButton 
                ID="btnFoto" runat="server" ImageUrl="~/CENSIMENTO/IMMCENSIMENTO/FotoEplan.gif"
                
                Style="z-index: 103; left: 155px; position: absolute; top: 27px; cursor: pointer; height: 12px;" 
                ToolTip="Foto e Planimetrie" TabIndex="45" Visible="False" 
                onclientclick="ApriFotoPlan();return false;" />
                        <br />
                           <img border="0" 
                alt="info" id="ImgInfo" 
            src="../NuoveImm/INFO.png" 
            style="cursor: pointer; left: 739px; position: absolute; top: 226px;" 
            
            onclick="window.open('Eventi.aspx?ID=<%=vId %> &CHIAMA=CO','Eventi', '');"/><br />
                        <br />
                        <asp:Label ID="Label17" runat="server" Font-Bold="False" 
                Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 674px; position: absolute; top: 194px; text-align: left"
                            Width="52px">SubFascia</asp:Label>
                        <br />
                        <br />
                        <asp:Label ID="Label18" runat="server" Font-Bold="False" 
                Font-Names="Arial" Font-Size="8pt" 
                Style="z-index: 100; left: 10px; position: absolute; top: 311px; text-align: left; width: 99px; right: 1076px;">Inizio Gest. Esterna</asp:Label>
            <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" 
                Font-Size="8pt" 
                Style="z-index: 100; left: 226px; position: absolute; top: 311px; text-align: left; width: 99px; right: 860px;">Fine Gest. Esterna</asp:Label>
            <asp:TextBox ID="TxtDataInizio" runat="server" MaxLength="10" Style="left: 112px; position: absolute;
        top: 306px; z-index: 2;" TabIndex="16" ToolTip="dd/Mm/YYYY" Width="75px"></asp:TextBox>
            <asp:TextBox ID="TxtDataFine" runat="server" MaxLength="10" Style="left: 321px; position: absolute;
        top: 306px; z-index: 2;" TabIndex="17" ToolTip="dd/Mm/YYYY" Width="75px"></asp:TextBox>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                ControlToValidate="TxtDataInizio" ErrorMessage="!" Font-Bold="True" 
                Height="1px" Style="left: 191px; position: absolute;
        top: 310px; z-index: 2;" ToolTip="Inserire una data valida" 
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))" 
                Width="1px"></asp:RegularExpressionValidator>
            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" 
                ControlToValidate="TxtDataFine" ErrorMessage="!" Font-Bold="True" Height="1px" Style="left: 401px; position: absolute;
        top: 310px; z-index: 2;" ToolTip="Inserire una data valida" 
                ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))" 
                Width="1px"></asp:RegularExpressionValidator>
                        <br />
                        <br />
        <asp:TextBox ID="txtPCarrabili" runat="server" MaxLength="7" Style="left: 514px;
            position: absolute; top: 104px; z-index: 5; width: 45px;" TabIndex="2"></asp:TextBox>
                        <br />
                        <br />
                        <br />
                        <asp:DropDownList ID="cmbPeriodo" runat="server" 
                style = "position : absolute; top: 276px; left: 684px; width: 90px;" 
                Enabled="False" >
            </asp:DropDownList>
                        <br />
        <asp:TextBox ID="txtSubFascia" runat="server" MaxLength="3" Style="left: 731px;
            position: absolute; top: 191px; z-index: 5; width: 41px;" TabIndex="2"></asp:TextBox>
                         <br />
                        &nbsp;<br />
                        <br />

                        <asp:Label ID="Label6" runat="server" Font-Bold="False" 
                Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 11px; position: absolute; top: 194px; text-align: left; right: 843px;"
                            Width="68px">Commissariato </asp:Label>
                        <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            
                            Style="z-index: 100; left: 10px; position: absolute; top: 224px; text-align: left; width: 88px;">Sede territoriale</asp:Label>
                            <asp:Label ID="Label20" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            
                            Style="z-index: 100; left: 10px; position: absolute; top: 250px; text-align: left; width: 88px;">Sede Terr. Amm.</asp:Label>
                        <asp:DropDownList ID="CmbCommissariati" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="9pt" Height="20px" 
                Style="border: 1px solid black; z-index: 111; left: 114px; position: absolute; top: 194px" 
                TabIndex="8" Width="308px">
                        </asp:DropDownList>
        <asp:DropDownList ID="DrlSchede" runat="server" Enabled="False" Style="z-index: 143;
            left: 112px; width: 570px; position: absolute; top: 276px;" 
                TabIndex="5">
        </asp:DropDownList>
                        <br /><asp:DropDownList ID="CmbFiliali" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="9pt" Height="20px" 
                Style="border: 1px solid black; z-index: 111; left: 113px; position: absolute; top: 224px; width: 619px;" 
                TabIndex="8">
                        </asp:DropDownList>
                        <asp:DropDownList ID="cmbFilialiAmministrative" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="9pt" Height="20px" 
                Style="border: 1px solid black; z-index: 111; left: 113px; position: absolute; top: 250px; width: 619px;" 
                TabIndex="8">
                        </asp:DropDownList>
                        <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            Style="z-index: 100; left: 427px; position: absolute; top: 194px; text-align: left"
                            Width="52px">Quartiere</asp:Label>
                        <asp:DropDownList ID="CmbQuartieri" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="9pt" Height="20px" 
                Style="border: 1px solid black; z-index: 111; left: 514px; position: absolute; top: 193px; width: 135px;" 
                TabIndex="8">
                        </asp:DropDownList>
                        <br />

<div class="tabber"style="text-align: left">
     
     <div class="tabbertab">
     	    <h2>INDIRIZZO</h2>
         <p>

            <table style="width: 645px; height: 95px;">
                <tr>
                    <td style="width: 100px">
                    <asp:DropDownList ID="DrLTipoInd" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="9pt" Height="20px" Style="border-right: black 1px solid;
                border-top: black 1px solid; z-index: 111; left: 11px; border-left: black 1px solid;
                border-bottom: black 1px solid; top: 296px" TabIndex="9" Width="97px">
                </asp:DropDownList></td>
                    <td>
            <asp:TextBox ID="TxtVia" runat="server" Style="left: 117px;
                top: 296px; z-index: 5;" Width="210px" MaxLength="30" CssClass="CssMaiuscolo" TabIndex="10"></asp:TextBox></td>
                    <td>
            <asp:Label ID="Label9" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 336px; top: 296px" Width="24px">Civico</asp:Label></td>
                    <td style="width: 2px">
            <asp:TextBox ID="TxtCivico" runat="server" Style="left: 369px;
                top: 296px; z-index: 5;" Width="59px" MaxLength="10" TabIndex="11"></asp:TextBox></td>
                    <td style="width: 2px">
            <asp:Label ID="Label11" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 438px; top: 296px" Width="40px">Comune</asp:Label></td>
                    <td style="width: 2px">
            <asp:DropDownList ID="DrLComune" runat="server" BackColor="White"
                Font-Names="arial" Font-Size="9pt" Height="20px" Style="border-right: black 1px solid;
                border-top: black 1px solid; z-index: 111; left: 481px; border-left: black 1px solid;
                border-bottom: black 1px solid; top: 295px" TabIndex="12" Width="168px">
            </asp:DropDownList></td>
                </tr>
                <tr>
                    <td style="width: 100px">
            <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 11px; top: 328px" Width="1px">Località</asp:Label></td>
                    <td>
            <asp:TextBox ID="TxtLocalita" runat="server" Style="left: 117px;
                top: 328px; z-index: 5;" Width="162px" MaxLength="20" CssClass="CssMaiuscolo" TabIndex="13"></asp:TextBox></td>
                    <td>
            <asp:Label ID="Label10" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                Style="z-index: 100; left: 331px; top: 328px" Width="24px">CAP*</asp:Label></td>
                    <td style="width: 2px">
            <asp:TextBox ID="TxtCap" runat="server" Style="left: 369px;
                top: 328px; z-index: 5;" Width="51px" MaxLength="5" TabIndex="14"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator10" runat="server"
                            ControlToValidate="TxtCap" ErrorMessage="!" Font-Bold="True" Height="1px" Style="z-index: 10;
                            left: 424px; top: 328px" ValidationExpression="\d{5}" 
                            Width="1px" ToolTip="E' ammesso un  CAP di 5 cifre" Display="Dynamic">!</asp:RegularExpressionValidator></td>
                    <td style="width: 2px">
                    </td>
                    <td style="width: 2px">
                    </td>
                </tr>
                <tr>
                    <td style="width: 100px">
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td style="width: 2px">
                    </td>
                    <td style="width: 2px">
                    </td>
                    <td style="width: 2px">
                    </td>
                </tr>
            </table>
            </p> 
     </div>

     <div class="<%=classetab %> <%=tabdefault2 %>" title ="MILLESIMALI">
            <uc1:Tab_Millesimali ID="Tab_Millesimali1" runat="server" />
     </div>
     
     <div class="<%=classetab %> <%=tabdefault3 %>" title ="UT.MILLESIMALI">
            <uc2:Tab_UtMillesimali ID="Tab_UtMillesimali1" runat="server" />
     </div>
     
     <div class="<%=classetab %> <%=tabdefault4 %>" title ="EDIFICI">
            <uc3:Tab_ComEdifici ID="Tab_ComEdifici1" runat="server" />
     </div>
     
     <div class="<%=classetab %> <%=tabdefault5 %>" title ="UN.COMUNI">   
            <uc5:Tab_UnComuni ID="Tab_UnComuni1" runat="server" />   
     </div>

     <div class="<%=classetab %> <%=tabdefault6 %>" title ="IMPIANTI">
            <uc4:Tab_Impianti ID="Tab_Impianti1" runat="server" />   
     </div>
     
     <div class="<%=classetab %> <%=tabdefault7 %>" title ="SERVIZI">
                           <uc7:Tab_Servizi ID="Tab_Servizi1" runat="server" />
     </div>

     <div class="<%=classetab %> <%=tabdefault8 %>" title ="IMP.COMUNI">
            <uc6:Tab_ImpComuni ID="Tab_ImpComuni1" runat="server" />
     </div>

     <div class="<%=classetab %> <%=tabdefault9 %>" title ="NOTE">
        <asp:TextBox ID="txtNote" runat="server" Height="113px" TextMode="MultiLine" Width="598px"></asp:TextBox>
     </div>
</div>
            <asp:TextBox ID="USCITA" runat="server" BorderColor="#F2F5F1" BorderStyle="None"
                ForeColor="#F2F5F1" Style="left: 37px; position: absolute; z-index :-1; top: 352px" TabIndex="-1"
                Width="25px">0</asp:TextBox>
            <asp:HiddenField ID="txttab" runat="server" Value="1" />
                
                <asp:HiddenField ID="txtModificato" runat="server" Value="0" />

</form>


    <script  language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';

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

        function PulisciCampiMillesimali() {
            document.getElementById('Tab_Millesimali1_txtDescr').value = ""
            document.getElementById('Tab_Millesimali1_txtDescTabella').value = ""
            document.getElementById('Tab_Millesimali1_HFtxtId').value = "0"
        }


        if (document.getElementById("txtVisibility").value != 0) {
            if (document.getElementById('Tab_Millesimali1_imgAddConv')) {
                document.getElementById('Tab_Millesimali1_imgAddConv').style.visibylity = 'hidden'
            }
            document.getElementById('Tab_Servizi1_imgAddConv').style.visibility = 'hidden'
            document.getElementById('Tab_ImpComuni1_imgAddConv').style.visibility = 'hidden'

        }
        if (document.getElementById("txtInfoVisible").value != 0) {

            document.getElementById('ImgInfo').style.visibility = 'visible'
            document.getElementById('ImgInfo').alt = 'Struttura bloccata perchè già presente nel lotto ' + document.getElementById('txtLottoName').value

        }
        else {
            document.getElementById('ImgInfo').style.visibility = 'hidden'
        }

    </script>
	</body>

</html> 
	