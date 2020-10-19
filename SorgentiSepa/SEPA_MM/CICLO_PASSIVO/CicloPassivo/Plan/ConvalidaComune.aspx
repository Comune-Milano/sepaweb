<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ConvalidaComune.aspx.vb" Inherits="Contabilita_CicloPassivo_Plan_ConvalidaComune" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<script type="text/javascript" src="prototype.lite.js"></script>
<script type="text/javascript" src="moo.fx.js"></script>
<script type="text/javascript" src="moo.fx.pack.js"></script>
    <title>Convalida Comune</title>
    <style type="text/css">
        #contenitore
        {
            height: 224px;
        }
        .style1
        {
            width: 600px;
            position: absolute;
            z-index: 402;
            top: 108px;
            left: 63px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table style="left: 0px; BACKGROUND-IMAGE: url('../../../NuoveImm/SfondoMascheraContratti.jpg'); WIDTH: 798px;
            position: absolute; top: 0px; height: 596px;">
            <tr>
                <td style="width: 706px">
                   <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                    Piano Finanziario-</strong>
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Label"></asp:Label>
                    -
                    <asp:Label ID="lblStato" runat="server" style="font-weight: 700"></asp:Label>
                    <br />
                    </span><br />
                    <br />
                    <br />
                                        <div id="cont" 
                        
                        style="position: absolute; width: 747px; height: 447px; top: 55px; left: 26px; overflow: scroll;"><%=Tabella%>
                    </div>
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
                    <asp:Label ID="lblErrore" runat="server" Visible="False" 
                        style="position:absolute; top: 540px; left: 18px;"></asp:Label>
                    <br />
                    <br />
                    <br />
                    
                    <asp:Image ID="imgEventi" runat="server" 
                        style="position:absolute; top: 518px; left: 25px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_Eventi_Grande.png" 
                        onclick="ConfermaEventi();"/>
                    <asp:Image ID="imgStampa" runat="server" 
                        style="position:absolute; top: 518px; left: 116px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_Stampa_Grande.png" 
                        onclick="ConfermaStampa();"/>
                    <asp:Image ID="imgConvalida" runat="server" 
                        style="position:absolute; top: 518px; left: 476px; cursor:pointer; right: 222px;" 
                        ImageUrl="Immagini/img_Modifica.png" 
                        onclick="Convalida();"/>
                    <asp:Image ID="imgEsci" runat="server" 
                        style="position:absolute; top: 517px; left: 699px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_EsciCorto.png" 
                        onclick="ConfermaEsci();"/>
                    <br />
                    <br />
                    <br />
                    
                    <asp:ImageButton ID="btnConvalida" runat="server" style="position:absolute; top: 518px; left: 354px;" 
                        ImageUrl="Immagini/img_Convalida.png" 
                        onclientclick="ConfermaConvalida();"/>
                    
                    <br />
                    <br />

   

                    <asp:HiddenField ID="idPianoF" runat="server" />

   

                    <asp:HiddenField ID="per" runat="server" />
                    <asp:HiddenField ID="modificato" runat="server" />
                    <asp:HiddenField ID="salvaok" runat="server" Value="0" />
                    <asp:HiddenField ID="stato" runat="server" Value="0" />

   

                    <asp:HiddenField ID="convalidaok" runat="server" />

   

                </td>
            </tr>
 
        </table>
   

<div id="Convalida" 
            
            
            
            
            style="position: absolute; z-index: 400; top: 0px; left: 0px; width: 800px; height: 600px; background-image: url('../../../NuoveImm/SfondoMascheraContratti.jpg'); background-repeat: no-repeat; ">
       
           <img id="immDiv" alt="" src="../../../ImmDiv/DivMGrande.png" 
               style="position: absolute; z-index: 401; top: 75px; left: 40px;" />
               <table style="background-color: #FFFFFF; " class="style1">
               <tr>
                   <td style="font-family: arial; font-size: 12pt; font-weight: bold; text-align: center;">
                       Indicare i motivi per cui il Piano Finanziario dovrà essere modificato</td>
               </tr>
               <tr>
                   
                   <td align="center" style="font-family: arial; font-size: 8pt; font-weight: normal; text-align: center;">
                       Il piano finanziario dovrà essere nuovamente modificato dal Gestore
                   </td>
               </tr>
               <tr>
                                  <td align="center">
                                      &nbsp;</td>
               </tr>
               <tr>
                                  <td style="font-family: arial; font-size: 10pt; font-weight: bold">
                                      Note</td>
               </tr>
               <tr>
                                  <td>
                       <asp:TextBox ID="TxtNote" runat="server" Height="225px" MaxLength="4000" 
                                          TextMode="MultiLine" Width="671px"></asp:TextBox>
                                  </td>
               </tr>
                              <tr>
                                  <td style="text-align: center">
                                      <asp:ImageButton ID="imgConferma" runat="server" ImageUrl="~/NuoveImm/Img_SalvaGrande.png"
                        Style="height: 20px" 
                        TabIndex="4" onclientclick="Sicuro();" />
                                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                      <asp:Image ID="imgAnnulla" runat="server" 
                        style="cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_EsciCorto.png" 
                        onclick="myOpacity.toggle();"/></td>
               </tr>
           </table>
        </div>
       
        
            <script type="text/javascript">

                myOpacity = new fx.Opacity('Convalida', { duration: 200 });
                myOpacity.hide();
                                
        </script>
    </form>
    
    <script type="text/javascript">

        function ConfermaConvalida() {
            if (document.getElementById('convalidaok').value == '1') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Sicuro di voler procedere con la convalida del Piano Finanziario?");
                if (chiediConferma == true) {
                    document.getElementById('salvaok').value = '1';
                }
                else {
                    document.getElementById('salvaok').value = '0';
                }
            }
            else {
                alert('Attenzione...Non tutti le voci hanno un capitolo di spesa! Non è possibile convalidare!');
            }
        }

        function Convalida() {
            if (document.getElementById('convalidaok').value == '1') {
                myOpacity.toggle();
            }
            else {
                alert('Attenzione...Non tutti le voci hanno un capitolo di spesa! Non è possibile convalidare!');
            }
        }
    
        function ConfermaEventi() {
            if (document.getElementById('modificato').value == '1') {
                alert('Attenzione...Sono state apportate delle modifiche. Salvare prima di proseguire!');

            }
            else { 
                window.open('EventiPF.aspx?ID=' + document.getElementById('idPianoF').value + '&P=' + document.getElementById('per').value, 'Eventi', '');
            }
        }

        function Annotazioni() {

                window.open('Annotazioni.aspx?IDP=' + document.getElementById('idPianoF').value + '&P='+ document.getElementById('per').value,'Annotazioni', '');
            
        }
        

        function ConfermaChiusura() {
            if (document.getElementById('modificato').value == '1') {
                alert('Attenzione...Sono state apportate delle modifiche. Salvare prima di proseguire!');

            }
            else {
                              
                    var chiediConferma
                    chiediConferma = window.confirm("Attenzione...Sei sicuro di voler concludere la fase di inserimento dello schema Piano Finanziario?\nIn caso affermativo, gli operatori abilitati potranno iniziare a insererire gli importi.");
                    if (chiediConferma == false) {
                        document.getElementById('salvaok').value == '0';
                    }
                    else {
                        document.getElementById('salvaok').value == '1';
                    }
            }
        }


        function ConfermaStampa() {
            if (document.getElementById('modificato').value == '1') {
                alert('Attenzione...Sono state apportate delle modifiche. Salvare prima di proseguire!');

            }
            else {
                window.open('StampaPF.aspx?T=1&ID=' + document.getElementById('idPianoF').value + '&P=' + document.getElementById('per').value, 'Stampa', '');
            }
        }


        function ConfermaEsci() {
            if (document.getElementById('modificato').value == '1') {


                var chiediConferma
                chiediConferma = window.confirm("Attenzione...sono state effettuate modifiche. Uscire senza salvare?");
                if (chiediConferma == true) {
                    document.location.href = '../../pagina_home.aspx';
                }
            }
            else {
                document.location.href = '../../pagina_home.aspx';
            }
        }

        function Sicuro() {
            var chiediConferma
            chiediConferma = window.confirm("Attenzione...Sicuro di voler procedere?");
            if (chiediConferma == true) {
                document.getElementById('salvaok').value = '1';
            }
            else {
                document.getElementById('salvaok').value = '0';
            }
        }

        if (document.getElementById('stato').value != '3') {
            document.getElementById('imgConvalida').style.visibility = 'hidden';
            document.getElementById('imgConvalida').style.position = 'absolute';
            document.getElementById('imgConvalida').style.left = '-100px';
            document.getElementById('imgConvalida').style.display = 'none';



        }
        
    </script>
</body>
</html>
