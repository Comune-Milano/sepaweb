<%@ Page Language="VB" AutoEventWireup="false" CodeFile="CalendarioSportello.aspx.vb" Inherits="ANAUT_CalendarioSportello" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <title></title>
    <style type="text/css">
        #INTESTA
        {
            top: 209px;
            left: 11px;
        }
                   
        .style2
        {
            height: 26px;
        }

        .style3
        {
            height: 22px;
        }

        .style4
        {
            height: 14px;
        }

    </style>
</head>
<body onload="SitIniziale();">
    <form id="form1" runat="server" >
    
    <asp:ScriptManager ID="ScriptManager1" runat="server" ScriptMode="Release">
    </asp:ScriptManager>

     <script type="text/javascript">
         var prm = Sys.WebForms.PageRequestManager.getInstance();
         prm.add_endRequest(EndRequestHandler);
         function EndRequestHandler(sender, args) {

             window.scrollTo(document.getElementById('xPos').value, document.getElementById('yPos').value);
         }

    </script>


    <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
                    <ContentTemplate>

                         

                
    <table id="Principale" 
        style="width: 100%; background-color: #D6D6D6;" >
    <tr>
    <td>

    
    
        <div ID="Div2" 
            
            
            style="border: 1px solid #000000; background-color: #FFFFCC; height: 164px; width: 369px;">
            <table cellpadding="0" cellspacing="0" style="width:100%;">
                <tr>
                    <td style="text-align: left">
                        &nbsp;&nbsp; &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Label ID="Label3" runat="server" Font-Bold="True" Font-Names="Arial" 
                            Font-Size="8pt" style="z-index: 110; left: 9px;"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        &nbsp;&nbsp; &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        &nbsp;&nbsp; &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        &nbsp;&nbsp; &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        &nbsp;&nbsp; &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        &nbsp; &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        &nbsp; &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:Image ID="Image3" runat="server" alt="Esci" 
                            ImageUrl="~/NuoveImm/Img_EsciCorto.png" onclick="Esci()" 
                            style="cursor:pointer" />
                    </td>
                </tr>
            </table>
        </div>

    
    
    </td>
    <td>
    <div ID="Div4" 
            
            
            style="border: 1px solid #000000; background-color: #FFFFCC; height: 164px; width: 234px;">
            <table cellpadding="0" cellspacing="0" style="width:100%;">
                <tr style="font-family: arial, Helvetica, sans-serif; font-size: 8pt">
                    <td style="text-align: left">
                        <img alt="" src="img/SpostaAppuntamento.png" />
                    </td>
                    <td style="text-align: left">
                        Sposta Appuntamento</td>
                </tr>
                <tr style="font-family: arial, Helvetica, sans-serif; font-size: 8pt">
                    <td style="text-align: left">
                        <img alt="" src="img/ReimpostaAppuntamento.png" />
                    </td>
                    <td style="text-align: left">
                        Reimposta Appuntamento</td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <img alt="" src="img/AnnullaAppuntamento.png" />
                    </td>
                    <td style="font-family: arial, Helvetica, sans-serif; font-size: 8pt;">
                        Sospendi Appuntamento</td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <img alt="" src="img/RipristinaAppuntamento.png" />
                        </td>
                    <td style="font-family: arial, Helvetica, sans-serif; font-size: 8pt;">
                        Ripristina Appuntamento</td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <img alt="" src="img/CreaScheda.png" />
                    </td>
                    <td style="font-family: arial, Helvetica, sans-serif; font-size: 8pt;">
                        Crea Scheda AU</td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        &nbsp;&nbsp; &nbsp;</td>
                    <td style="text-align: left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        &nbsp; &nbsp;</td>
                    <td style="text-align: left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        &nbsp; &nbsp;</td>
                    <td style="text-align: left">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        &nbsp;</td>
                    <td style="text-align: right">
                        &nbsp;</td>
                </tr>
            </table>
        </div>
    </td>
    <td>
    <div id="Div5" 
             
        
        
        
            style="border: 1px solid #000000; background-color: #FFFFCC; width: 217px; height: 164px; ">
        <table style="width:100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label26" runat="server" Font-Bold="False" Font-Names="Arial" 
                        Font-Size="8pt" style="z-index: 110; left: 9px;">Cognome</asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:TextBox ID="txtCognome" runat="server" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Names="arial" Font-Size="8pt" TabIndex="1" Width="178px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left" class="style4">
                    <asp:Label ID="Label27" runat="server" Font-Bold="False" Font-Names="Arial" 
                        Font-Size="8pt" style="z-index: 110; left: 9px;">Nome</asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:TextBox ID="txtNome" runat="server" BorderStyle="Solid" BorderWidth="1px" 
                        Font-Names="arial" Font-Size="8pt" TabIndex="2" Width="178px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:Label ID="Label28" runat="server" Font-Bold="False" Font-Names="Arial" 
                        Font-Size="8pt" style="z-index: 110; left: 9px;">Cod.Contratto</asp:Label>
                </td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:TextBox ID="txtContratto" runat="server" BorderStyle="Solid" 
                        BorderWidth="1px" Font-Names="arial" Font-Size="8pt" TabIndex="3" Width="178px"></asp:TextBox>
                </td>
            </tr>
            <tr style="font-family: arial, Helvetica, sans-serif; font-size: 8pt; font-style: italic;">
                <td style="text-align: left">
                    l&#39;appuntamento selezionato sarà evidenziato in rosso nella lista.</td>
            </tr>
            <tr>
                <td style="text-align: right">
                    <asp:ImageButton ID="imgCerca" runat="server" 
                        ImageUrl="~/NuoveImm/Img_AvviaRicerca.png" onclientclick="CercaApp();" 
                        TabIndex="4" />
                </td>
            </tr>
        </table>
    </div>
    </td>
    <td >
     
    <div id="Div1" 
             
        
        
        
            style="border: 1px solid #000000; background-color: #FFFFCC; width: 217px; height: 164px; ">
        <table style="width:100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="text-align: left">
    <asp:CheckBox ID="chFuoriOrario" runat="server" CausesValidation="True"
        Font-Names="rial" Font-Size="9pt" Text="Abilita Fuori Orario*" 
        Checked="True" AutoPostBack="True" Font-Bold="True" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
    <asp:label id="Label2" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="False" 
                style="z-index: 110; left: 9px;">* 8.00-8.30 / 13.00-13.30 / 16.30-18.30</asp:label>
                &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td style="text-align: left">
                    <asp:CheckBox ID="ChAnnullati" runat="server" CausesValidation="True"
        Font-Names="rial" Font-Size="9pt" Text="Visualizza Sospesi" 
        Checked="False" AutoPostBack="True" Font-Bold="True" /></td>
            </tr>
        </table>
    </div>
    </td>
    <td >
    <asp:Calendar ID="Calendar1" runat="server" BackColor="White" 
        BorderColor="#999999" CellPadding="4" DayNameFormat="Shortest" 
        Font-Names="Verdana" Font-Size="8pt" ForeColor="Black" 
        style="height: 112px; width: 200px;" 
                            FirstDayOfWeek="Sunday">
                    <DayHeaderStyle BackColor="#CCCCCC" Font-Bold="True" Font-Size="7pt" />
                    <NextPrevStyle VerticalAlign="Bottom" />
                    <OtherMonthDayStyle ForeColor="#808080" />
                    <SelectedDayStyle BackColor="#666666" Font-Bold="True" ForeColor="White" />
                    <SelectorStyle BackColor="#CCCCCC" />
                    <TitleStyle BackColor="#999999" BorderColor="Black" Font-Bold="True" />
                    <TodayDayStyle BackColor="#CCCCCC" ForeColor="Black" />
                    <WeekendDayStyle BackColor="#FFFFCC" />
    </asp:Calendar>
    </td>
    </tr>
    <tr>
    <td>
        &nbsp;</td>
    <td>
        &nbsp;</td>
    <td>
        &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
            <img alt="Giorni Con disponibilità" src="LegendaDisp.png" 
        style=""/>
            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" 
                Font-Size="8pt" style="z-index: 110;">Giorni con disponibilità</asp:Label>
        </td>
    </tr>
    </table>

    
    <br />
    <table id="contenitore" style="width: 100%">
    <tr>
    <td>
    <div id="INTESTA" 
        
        
        style="background-color: #FFFFCC;">
        <table style="width:100%;">
            <tr>
                <td style="text-align: center">
    <asp:label id="lblFiliale" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 110; left: 9px;"></asp:label>
                &nbsp;&nbsp;
                <asp:label id="lblSportello" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 110; left: 120px;"></asp:label> 
                </td>
            </tr>
        </table>
    </div>
    </td>
    </tr>
    <tr>
    <td>
     
    <div id="Elenco"   
                            
                            style="border: 1px solid #000000; width: 100%; overflow: auto;">
        <table style="width:100%;">
            <%=TabellaGiorni %>
            
        </table>
   
    </div>
        
    
    </td>
    </tr>
    </table>
    
        
     

    <script type="text/javascript">
        function Verifica() {
//            alert(document.getElementById('OPERAZIONE').value);
//            if (document.getElementById('OPERAZIONE').value == '0') {
//                document.getElementById('imgAnnulla').style.visibility = 'hidden';
//            }
//            else {
//                document.getElementById('imgAnnulla').style.visibility = 'visible';
            //            }

        }

    </script>


                    <div id="DIVCONFERMA"                          
                            
        
                            
                            style="background-color: #c3c3bb; width: 100%; height: 100%; background-repeat: no-repeat; visibility: hidden; z-index: 1000; text-align: left; position:fixed; top: 0px; left: 0px;">
        <br />
        <br />
        <table style="background-position: center; width: 100%; height: 100%; background-repeat: no-repeat; z-index: 2000; text-align: left; background-image: url('../ImmDiv/SfondoDim1.jpg');">
        <tr style="text-align: center;font-family: arial, Helvetica, sans-serif; font-size: 12pt">
        <td>
        <table style="width: 100%; height: 100%;">
        <tr style="text-align: center;">
        <td class="style3">
            <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label></td>
        </tr>
            <tr style="text-align: center;">
                <td class="style3">
                    &nbsp;<asp:Label ID="Label25" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        <tr style="text-align: center;">
        <td class="style3">
            &nbsp;<asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
        </td>
        </tr>
                <tr>
        <td>
            
            &nbsp; &nbsp;</td>
        </tr>
                <tr style="text-align: center">
        <td>
            
            <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_SI.png" 
                        Style="cursor:pointer" 
                        TabIndex="8" 
                onclientclick="SitIniziale();document.getElementById('DIVCONFERMA').style.visibility = 'hidden';" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            <img alt="Annulla" src="../NuoveImm/Img_NO.png" 
        style="cursor:pointer" onclick="SitIniziale();alert('Operazione Annullata!');document.getElementById('DIVCONFERMA').style.visibility = 'hidden';"/></td>
        </tr>
        </table>
        </td>
        </tr>
        
        </table>
    </div>


    <asp:HiddenField ID="IDA" runat="server" />
    <asp:HiddenField ID="SPORTELLO" runat="server" />
    <asp:HiddenField ID="FILIALE" runat="server" />
    
    <asp:HiddenField ID="SLOTORIGINALE" runat="server" />
    <asp:HiddenField ID="TIPO" runat="server" />
    <asp:HiddenField ID="OPERAZIONE" runat="server" Value="0" />
    <asp:HiddenField ID="GIORNO_SLOT" runat="server" />
    <asp:HiddenField ID="ORARIO_SLOT" runat="server" />
    <asp:HiddenField ID="SLOTDESTINATARIO" runat="server" />
    <asp:HiddenField ID="yPos" runat="server" Value="0" />
    <asp:HiddenField ID="xPos" runat="server" Value="0" />


               
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
   
    <asp:HiddenField ID="opfatta" runat="server" />

    <script type="text/javascript">

        document.getElementById('DIVCONFERMA').style.visibility = 'hidden';

        function CercaApp() {
            window.showModalDialog('ElencoNomiApp.aspx?N=' + document.getElementById('txtNome').value + '&C=' + document.getElementById('txtCognome').value + '&CC=' + document.getElementById('txtContratto').value + '&S=' + document.getElementById('SPORTELLO').value + '&F=' + document.getElementById('FILIALE').value, window, 'status:no;dialogWidth:670px;dialogHeight:550px;dialogHide:true;help:no;scroll:no');
        }

        function Ripristina(indice, convocazione, contratto) {
            document.getElementById('SLOTORIGINALE').value = indice;
            document.getElementById('OPERAZIONE').value = '3';

            var chiediConferma
            chiediConferma = window.confirm("Attenzione...Sei sicuro di volere RIPRISTINARE questo appuntamento?");
            if (chiediConferma == true) {

                window.showModalDialog('AnnullaAppuntamento.aspx?T=1&IC=' + convocazione + '&IDA=' + indice + '&IDC=' + contratto, window, 'status:no;dialogWidth:400px;dialogHeight:350px;dialogHide:true;help:no;scroll:no');
                document.getElementById('ImgProcedi').click();
            }
            else {

                alert('Operazione annullata!');
            }
        }


        function Sposta(indice,cognome,nome,appuntamento) {
            document.getElementById('imgOp1').style.visibility = 'hidden';
            document.getElementById('imgOp2').style.visibility = 'visible';
            document.getElementById('SLOTORIGINALE').value = indice;
            document.getElementById('OPERAZIONE').value = '1';
            if (navigator.appName == 'Microsoft Internet Explorer') {
                document.getElementById('lblOperazione').innerText = "operazione in corso: SPOSTA APPUNTAMENTO";
                document.getElementById('lblOperazione0').innerText = 'Selezionare ora un orario libero in cui spostare...';
                document.getElementById('lblOperazione1').innerText = '';
                document.getElementById('testodomanda').value = 'Confermi di voler SPOSTARE l\'appuntamento selezionato al';
                document.getElementById('Label25').innerText = 'Confermi di voler SPOSTARE l\'appuntamento selezionato al';
                document.getElementById('testodomanda1').value =  cognome + ' ' + nome + ' ' + appuntamento ;
                document.getElementById('Label5').innerText = cognome + ' ' + nome + ' ' + appuntamento ;
            }
            else {
                document.getElementById('lblOperazione').textContent = "operazione in corso: SPOSTA APPUNTAMENTO";
                document.getElementById('lblOperazione0').textContent = 'Selezionare ora un orario libero in cui spostare...';
                document.getElementById('lblOperazione1').textContent = '';
                document.getElementById('testodomanda').value = 'Confermi di voler SPOSTARE l\'appuntamento selezionato al';
                document.getElementById('Label25').textContent = 'Confermi di voler SPOSTARE l\'appuntamento selezionato al';
                document.getElementById('testodomanda1').value = cognome + ' ' + nome + ' ' + appuntamento ;
                document.getElementById('Label5').textContent =  cognome + ' ' + nome + ' ' + appuntamento ;
            }
        }

        function Reimposta(indice, cognome, nome, appuntamento) {
            document.getElementById('imgOp1').style.visibility = 'hidden';
            document.getElementById('imgOp2').style.visibility = 'visible';
            document.getElementById('SLOTORIGINALE').value = indice;
            document.getElementById('OPERAZIONE').value = '2';
            if (navigator.appName == 'Microsoft Internet Explorer') {
                document.getElementById('lblOperazione').innerText = "operazione in corso: REIMPOSTA APPUNTAMENTO";
                document.getElementById('lblOperazione0').innerText = 'Selezionare ora un orario libero in cui reimpostare...';
                document.getElementById('lblOperazione1').innerText = '';
                document.getElementById('testodomanda').value = 'Confermi di voler REIMPOSTARE l\'appuntamento selezionato al';
                document.getElementById('Label25').innerText = 'Confermi di voler REIMPOSTARE l\'appuntamento selezionato al';
                document.getElementById('testodomanda1').value = cognome + ' ' + nome + ' ' + appuntamento;
                document.getElementById('Label5').innerText = cognome + ' ' + nome + ' ' + appuntamento;
            }
            else {
                document.getElementById('lblOperazione').textContent = "operazione in corso: REIMPOSTA APPUNTAMENTO";
                document.getElementById('lblOperazione0').textContent = 'Selezionare ora un orario libero in cui reimpostare...';
                document.getElementById('lblOperazione1').textContent = '';
                document.getElementById('testodomanda').value = 'Confermi di voler REIMPOSTARE l\'appuntamento selezionato al ';
                document.getElementById('Label25').textContent = 'Confermi di voler REIMPOSTARE l\'appuntamento selezionato al ';
                document.getElementById('testodomanda1').value = cognome + ' ' + nome + ' ' + appuntamento;
                document.getElementById('Label5').textContent = cognome + ' ' + nome + ' ' + appuntamento;
            }
        }

        function Annulla(indice,convocazione,contratto) {
            document.getElementById('SLOTORIGINALE').value = indice;
            document.getElementById('OPERAZIONE').value = '3';

            var chiediConferma
            chiediConferma = window.confirm("Attenzione...Sei sicuro di volere SOSPENDERE questo appuntamento?");
            if (chiediConferma == true) {

                window.showModalDialog('AnnullaAppuntamento.aspx?T=2&IC=' + convocazione + '&IDA=' + indice + '&IDC=' + contratto, window, 'status:no;dialogWidth:400px;dialogHeight:350px;dialogHide:true;help:no;scroll:no');
                document.getElementById('ImgProcedi').click();
            }
            else {

                alert('Operazione annullata!');
            }

        }

        function CreaAnagrafe(indice,convocazione,contratto) {
            var chiediConferma
            chiediConferma = window.confirm("Attenzione...Sei sicuro di volere creare una nuova scheda anagrafe utenza?");
            if (chiediConferma == true) {

                window.showModalDialog('CreaAU.aspx?IC=' + convocazione + '&IDA=' + indice + '&IDC=' + contratto, window, 'top=0,left=0,width=680,height=540,resizable=no,menubar=no,toolbar=no,scrollbars=no');
                document.getElementById('ImgProcedi').click();
            }
            else {

                alert('Operazione annullata!');
            }
        }

        function Assegna(indice,giorno,ora) {
            if (document.getElementById('SLOTORIGINALE').value == '') {
                alert('Selezionare una operazione da compiere, poi selezionare l\'appuntamento libero!');
            }
            else {
                document.getElementById('SLOTDESTINATARIO').value = indice;
                document.getElementById('GIORNO_SLOT').Value = giorno;
                document.getElementById('ORARIO_SLOT').value = ora;
                var miogiorno=String (giorno);

                if (navigator.appName == 'Microsoft Internet Explorer') {
                    document.getElementById('Label4').innerText = miogiorno.substr(6, 2) + '/' + miogiorno .substr(4, 2) + '/' + miogiorno .substr(0, 4) + ' ore ' + ora + ' ?';
                }
                else {
                    document.getElementById('Label4').textContent = miogiorno.substr(6, 2) + '/' + miogiorno.substr(4, 2) + '/' + miogiorno.substr(0, 4) + ' ore ' + ora + ' ?';
                }

                document.getElementById('DIVCONFERMA').style.visibility = 'visible';
            }
        }



        function Esci() {
            self.close();
        }


        function SitIniziale() {

            document.getElementById('imgOp1').style.visibility = 'visible';
            document.getElementById('imgOp2').style.visibility = 'hidden';

//            document.getElementById('SLOTDESTINATARIO').value = '';
//            document.getElementById('SLOTORIGINALE').value = '';

            if (navigator.appName == 'Microsoft Internet Explorer') {
                document.getElementById('lblOperazione').innerText = 'Scegli un operazione da compiere...';
                document.getElementById('lblOperazione0').innerText = '';
                document.getElementById('lblOperazione1').innerText = '';
            }
            else {
                document.getElementById('lblOperazione').textContent = 'Scegli un operazione da compiere...';
                document.getElementById('lblOperazione0').textContent = '';
                document.getElementById('lblOperazione1').textContent = '';
                
            }
        }



    </script>
                       </ContentTemplate>
                </asp:UpdatePanel> 
                
                <asp:HiddenField ID="testodomanda" runat="server" />
                <asp:HiddenField ID="testodomanda1" runat="server" />
                
    <div id="Div3"     
        
        
        style="position:absolute; top: 20px; left: 20px; width: 314px; background-color: #FFFFCC; height: 124px;">
        <table style="width:100%;" cellpadding="0" cellspacing="0">
            <tr>
                <td style="text-align: left">
                    </td>
                                    <td style="text-align: left">
                    </td>
            </tr>
            <tr>
            <td style="text-align: right">
                    <img alt="Operazione..." src="Arrow-right-icon.png" id="imgOp1" />
                    </td>
                <td style="text-align: left" class="style2" valign="middle">
    <asp:label id="lblOperazione" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 110; left: 9px;">Scegli un operazione da compiere...</asp:label>
                </td>
            </tr>
                        <tr>
                        <td style="text-align: right"><img alt="Operazione..." src="Arrow-right-icon.png" id="imgOp2" />
                    </td>
                            <td class="style2" style="text-align: left" valign="middle">
                                <asp:Label ID="lblOperazione0" runat="server" Font-Bold="True" 
                                    Font-Names="Arial" Font-Size="8pt" style="z-index: 110; left: 9px;"></asp:Label>
                            </td>
            </tr>
            <tr>
            <td style="text-align: left">&nbsp;</td>
                <td class="style2" style="text-align: left">
                    <asp:Label ID="lblOperazione1" runat="server" Font-Bold="True" 
                        Font-Names="Arial" Font-Size="8pt" style="z-index: 110; left: 9px;"></asp:Label>
                </td>
            </tr>
                        <tr>
                        <td style="text-align: left">
                    </td>
                <td style="text-align: right">
                    <asp:Image alt="Esci" ID="imgAnnulla" runat="server" 
                        ImageUrl="~/NuoveImm/Img_AnnullaVal.png" onclick="Esci()" 
                        style="cursor:pointer" />
                            </td>
            </tr>
        </table>
    </div>
    
    <asp:UpdateProgress ID="UpdateProgressGenerale" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div style="margin: 0px; background-color: #C0C0C0; width: 100%; height: 100%; position: fixed;
                top: 0px; left: 0px; filter: alpha(opacity='75'); opacity: 0.75; background-color: #eeeeee;">
                <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
                    margin-top: -48px; background-image: url('../NuoveImm/sfondo.png');">
                    <table style="width: 100%; height: 100%">
                        <tr>
                            <td valign="middle" align="center">
                                <asp:Image ID="Image2" runat="server" ImageUrl="../NuoveImm/load.gif" />
                                <br />
                                <br />
                                <asp:Label ID="Label24" runat="server" Text="caricamento in corso..." Font-Names="Arial"
                                    Font-Size="10pt"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>

        </ProgressTemplate>
    </asp:UpdateProgress>
    
    </form>
    
</body>
<script  language="javascript" type="text/javascript">

        </script>
</html>

