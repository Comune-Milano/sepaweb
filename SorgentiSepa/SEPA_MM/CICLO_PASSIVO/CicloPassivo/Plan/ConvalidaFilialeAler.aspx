<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ConvalidaFilialeAler.aspx.vb" Inherits="Contabilita_CicloPassivo_Plan_ConvalidaAler" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<base target="_self"/>
<script type="text/javascript" src="prototype.lite.js"></script>
<script type="text/javascript" src="moo.fx.js"></script>
<script type="text/javascript" src="moo.fx.pack.js"></script>
    <title>Convalida</title>
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
                    
                    <asp:Image ID="imgStampa" runat="server" 
                        style="position:absolute; top: 518px; left: 567px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_Stampa_Grande.png" 
                        onclick="ConfermaStampa();"/>
                    <asp:Image ID="imgEsci" runat="server" 
                        style="position:absolute; top: 517px; left: 699px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_EsciCorto.png" 
                        onclick="ConfermaEsci();"/>
                    <br />
                    <br />
                    <br />
                    
                    <br />
                    <br />
                </td>
            </tr>
 
        </table>
                       <asp:HiddenField ID="idPianoF" runat="server" />
                     <asp:HiddenField ID="per" runat="server" />
                    <asp:HiddenField ID="modificato" runat="server" />
                    <asp:HiddenField ID="salvaok" runat="server" Value="0" />
                    <asp:HiddenField ID="stato" runat="server" Value="0" />
                    <asp:HiddenField ID="tuttocompleto" runat="server" Value="0" />
                    <asp:HiddenField ID="idfiliale" runat="server" Value="0" />

   

                    <asp:HiddenField ID="idvoce" runat="server" />
                    <asp:HiddenField ID="idvoce1" runat="server" />



        <div id="DivConferma"         
            
            
            style="position: absolute; z-index: 400; top: 0px; left: 0px; width: 800px; height: 600px; background-image: url('../../../NuoveImm/SfondoMascheraContratti.jpg'); background-repeat: no-repeat; visibility: hidden;">
       
           <img id="Img1" alt="" src="../../../ImmDiv/DivMGrande.png" 
               style="position: absolute; z-index: 401; top: 84px; left: 30px;" />
               <table 
               
               style="width: 669px; position: absolute; z-index: 402; top: 119px; background-color: #FFFFFF; left: 65px; ">
               <tr>
                   <td style="font-family: arial; font-size: 12pt; font-weight: bold; text-align: center;">
                       Approvare l'importo 
                       <asp:Label ID="lblRichiesto1" runat="server" Font-Bold="True" 
                           Font-Names="arial" Font-Size="12pt" Text="0,00"></asp:Label>
&nbsp;richiesto?
                       <br />
                       Premere 
                       CONFERMA per confermare.</td>
               </tr>
               <tr>
                   
                   <td>
                       &nbsp;
                       &nbsp;</td>
               </tr>
               <tr>
                   
                   <td align="center">
&nbsp;&nbsp;&nbsp;
                       </td>
               </tr>
               <tr>
                                  <td>
                       &nbsp;
                       &nbsp;</td>
               </tr>
               <tr>
                                  <td>
                       &nbsp;
                       &nbsp;                                  </td>
               </tr>
                              <tr>
                                  <td style="text-align: center">
                                      <asp:ImageButton ID="imgConfermare" runat="server" ImageUrl="~/NuoveImm/Img_Conferma1.png"
                        Style="height: 20px" 
                        TabIndex="4" onclientclick="Sicuro();" />
                                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                      <asp:Image ID="Image1" runat="server" 
                        style="cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_EsciCorto.png" 
                        onclick="myOpacity1.toggle();"/></td>
               </tr>
           </table>
        </div>

        <div id="Convalida" 
            style="position: absolute; z-index: 410; top: 0px; left: 0px; width: 800px; height: 600px; background-image: url('../../../NuoveImm/SfondoMascheraContratti.jpg'); background-repeat: no-repeat; visibility: hidden;" >
        <img id="immDiv1" alt="" src="../../../ImmDiv/DivMGrande.png" style="position: absolute; z-index: 401; top: 84px; left: 30px;" />
        <table style="width: 669px; position: absolute; z-index: 402; top: 119px; background-color: #FFFFFF; left: 65px; ">
               <tr>
                   <td style="font-family: arial; font-size: 12pt; font-weight: bold; text-align: center;">
                       Si è scelto di modificare l'importo richiesto. Digitare il nuovo importo e premere CONFERMA.<br />Gli operatori abilitati saranno automaticamente allertati.</td>
               </tr>
               <tr>                   
                   <td>
                       &nbsp;
                       &nbsp;</td>
               </tr>
                              <tr>                   
                   <td>
                       &nbsp;
                       &nbsp;<asp:Label ID="lblRichiesto" runat="server" Font-Bold="True" 
                           Font-Names="arial" Font-Size="10pt"></asp:Label>
                                  </td>
               </tr>
               <tr>                   
                   <td align="center" valign="middle">
                      
                       <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial" 
                           Font-Size="10pt" Text="Importo Approvato Euro"></asp:Label>
                      
                       <asp:TextBox ID="txtImporto" runat="server" Font-Bold="True" Font-Names="arial" 
                           Font-Size="10pt" Width="100px" style="text-align:right" 
                           ToolTip="Inserire un valore con decimale a precisione doppia">0,00</asp:TextBox>
                      
                       <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" 
                           ControlToValidate="txtImporto" ErrorMessage="Errore!" 
                           ValidationExpression="^\d{1,10}((,|)\d{1,2})?$"></asp:RegularExpressionValidator>
                      
                   </td>
               </tr>
               <tr>
                   <td>
                       &nbsp;
                       &nbsp;</td>
               </tr>
                              <tr>
                                  <td style="text-align: center">
                                      
                                      &nbsp;&nbsp;&nbsp;<asp:ImageButton 
                                          ID="ImageButton1" runat="server" ImageUrl="~/NuoveImm/Img_Conferma1.png" 
                                          onclientclick="Sicuro();" />
                                      &nbsp;&nbsp;&nbsp;&nbsp;
                                      <asp:Image ID="Image2" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png" style="cursor:pointer" onclick='myOpacity.toggle();'/>
                                     </td>
               </tr>
           </table>
        </div>

       
            <script type="text/javascript">

                myOpacity = new fx.Opacity('Convalida', { duration: 200 });
                myOpacity.hide();

                myOpacity1 = new fx.Opacity('DivConferma', { duration: 200 });
                myOpacity1.hide();

                               
        </script>
    </form>
    
    <script type="text/javascript">

        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\-\.\,]/g
        }
        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
            //document.getElementById('modificato').value = '1';
        }

//        function AutoDecimal2(obj) {
//            if (obj.value.replace(',', '.') > 0) {
//                var a = obj.value.replace(',', '.');
//                a = parseFloat(a).toFixed(2)
//                document.getElementById(obj.id).value = a.replace('.', ',')
//            }

        //                }

        function AutoDecimal2(obj) {

            obj = obj.toString();
           

            if (obj.replace(',', '.') != 0) {
                var a = obj.replace(',', '.');
                a = parseFloat(a).toFixed(2)
                if (a != 'NaN') {
                    if (a.substring(a.length - 3, 0).length >= 4) {
                        var decimali = a.substring(a.length, a.length - 2);
                        var dascrivere = a.substring(a.length - 3, 0);
                        var risultato = '';
                        while (dascrivere.replace('-', '').length >= 4) {
                            risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato
                            dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
                        }
                        risultato = dascrivere + risultato + ',' + decimali
                        if (document.all) {
                            document.getElementById('lblRichiesto1').innerText = 'Importo Richiesto Euro: ' + risultato;
                        }
                        else {
                            document.getElementById('lblRichiesto1').textContent = 'Importo Richiesto Euro: ' + risultato;
                        }
                    }
                    else {
                        //document.getElementById(obj.id) = a.replace('.', ',')
                        if (document.all) {
                            document.getElementById('lblRichiesto1').innerText = 'Importo Richiesto Euro: ' + a.replace('.', ',');
                        }
                        else {
                            document.getElementById('lblRichiesto1').textContent = 'Importo Richiesto Euro: ' + a.replace('.', ',');
                        }
                    }

                }
                else
                    //document.getElementById(obj.id).value = ''
                document.getElementById('lblRichiesto1').textContent = 'Importo Richiesto Euro: ';
            }
        };

         function AutoDecimal3(obj) {
             obj = obj.toString();
            obj = obj.replace('.', '');
            if (obj.replace(',', '.') != 0) {
                var a = obj.replace(',', '.');
                a = parseFloat(a).toFixed(2)
                if (a.substring(a.length - 3, 0).length >= 4) {
                    var decimali = a.substring(a.length, a.length - 2);
                    var dascrivere = a.substring(a.length - 3, 0);
                    var risultato = '';
                    while (dascrivere.replace('-', '').length >= 4) {
                        risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato
                        dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
                    }
                    risultato = dascrivere + risultato + ',' + decimali

                    
                    if (document.all) {
                        document.getElementById('lblRichiesto1').innerText = 'Importo Richiesto Euro: ' + risultato;
                    }
                    else {
                        document.getElementById('lblRichiesto1').textContent = 'Importo Richiesto Euro: ' + risultato;
                    }



                    
                }
                else {
                    
                    if (document.all) {
                        document.getElementById('lblRichiesto1').innerText = 'Importo Richiesto Euro: ' + a.replace('.', ',')
                    }
                    else {
                        document.getElementById('lblRichiesto1').textContent = 'Importo Richiesto Euro: ' + a.replace('.', ',')
                    }
                    
                }

            }
        }

        function AutoDecimal4(obj) {
            obj = obj.toString();
            obj = obj.replace('.', '');
            if (obj.replace(',', '.') != 0) {
                var a = obj.replace(',', '.');
                a = parseFloat(a).toFixed(2)
                if (a.substring(a.length - 3, 0).length >= 4) {
                    var decimali = a.substring(a.length, a.length - 2);
                    var dascrivere = a.substring(a.length - 3, 0);
                    var risultato = '';
                    while (dascrivere.replace('-', '').length >= 4) {
                        risultato = '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3) + risultato
                        dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
                    }
                    risultato = dascrivere + risultato + ',' + decimali
                    
                    if (document.all) {
                        document.getElementById('lblRichiesto').innerText = 'Importo Richiesto Euro: ' + risultato;
                    }
                    else {
                        document.getElementById('lblRichiesto').textContent = 'Importo Richiesto Euro: ' + risultato;
                    }
                    
                }
                else {
                    
                    if (document.all) {
                        document.getElementById('lblRichiesto').innerText = 'Importo Richiesto Euro: ' + a.replace('.', ',')
                    }
                    else {
                        document.getElementById('lblRichiesto').textContent = 'Importo Richiesto Euro: ' + a.replace('.', ',')
                    }
                    
                }

            }
        }
        function ConfermaImporto(indicevoce,importo) {
            document.getElementById('idvoce').value = indicevoce;
            AutoDecimal2(importo);
            //var a = AutoDecimal3(importo);
            //document.getElementById('lblRichiesto1').innerText = 'Iporto Richiesto Euro: ' + a;
            myOpacity1.toggle();
        }

        function NonConfermaImporto(indicevoce,importo) {
            document.getElementById('idvoce').value = indicevoce;
            AutoDecimal2(importo);
            //var a = importo.toString();
            //document.getElementById('lblRichiesto').innerText ='Iporto Richiesto Euro: ' + a.replace('.',',');
            myOpacity.toggle();
        }


        function ConfermaStampa() {
            if (document.getElementById('modificato').value == '1') {
                alert('Attenzione...Sono state apportate delle modifiche. Salvare prima di proseguire!');

            }
            else {
                window.open('StampaPF.aspx?IDF=' + document.getElementById('idfiliale').value  + '&ID=' + document.getElementById('idPianoF').value + '&P=' + document.getElementById('per').value, 'Stampa', '');
            }
        }


        function ConfermaEsci() {
            if (document.getElementById('modificato').value == '1') {


                var chiediConferma
                chiediConferma = window.confirm("Attenzione...sono state effettuate modifiche. Uscire senza salvare?");
                if (chiediConferma == true) {
                    self.close();
                }
            }
            else {
                self.close();
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


        
    </script>
    <p>
        &nbsp;</p>
</body>
</html>
