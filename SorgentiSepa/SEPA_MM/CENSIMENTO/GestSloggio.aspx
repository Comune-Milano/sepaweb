<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestSloggio.aspx.vb" Inherits="CENSIMENTO_GestSloggio" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Gestione Sloggio</title>
    <link href="../MANUTENZIONI/Styles/Site.css" rel="stylesheet" type="text/css" />
     
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
            function ConfermaEsci() {

                if (document.getElementById('Modificato').value == '1') {
                    var chiediConferma
                    chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
                    if (chiediConferma == true) {
                        self.close();
                        //document.getElementById('Modificato').value = '111';
                        //document.getElementById('USCITA').value='0';
                    }
                }
                else {
                    self.close();
                }
            }

  





       function AllegaFile() {
            var ins = <%=Inserimento %> 
            if (ins !='-1'){
                            window.open('InviaAllStatoManut.aspx?IDUNITA=' + +document.getElementById('idunita').value, 'Allegato', 'height=500px,width=900px,resizable=no');
}
else
{
alert('Salvare lo stato manutentivo prima di allegare un file!')}
            
            }

            function ElencoAllegati() {
                                        window.open('ElencoAllegati.aspx?IDUNITA=' + +document.getElementById('idunita').value, 'Allegato', 'height=500px,width=900px,resizable=no');

            
            }




            function ApriVerbaleSloggio() {
            
            
             window.open('VerbaleSloggio.aspx?ID=' + document.getElementById('idunita').value + '&IDSTATO= ' + document.getElementById('id_stato').value + '&IDSLOGGIO= ' + document.getElementById('id_sloggio').value  + '&STVERB= ' + document.getElementById('stato_verb').value, '');

            }










           function TastoInvio(e) {
            sKeyPressed1 = e.which;
            if (sKeyPressed1 == 13) {
                e.preventDefault();
                //document.getElementById('USCITA').value = '0';
                //document.getElementById('txtModificato').value = '111';
            }
        }


        function $onkeydown() {

            if (event.keyCode == 13) {
                event.keyCode = 0;
                // document.getElementById('USCITA').value = '0';
                //document.getElementById('txtModificato').value = '111';
            }
        }

        var r = {
            'special': /[\W]/g,
            'quotes': /['\''&'\"']/g,
            'notnumbers': /[^\d\-\,]/g
        }


        function valid(o, w) {
            o.value = o.value.replace(r[w], '');
            o.value = o.value.replace('.', ',');
            //document.getElementById('txtModificato').value = '1';
        }



        function DelPointer(obj) {
            obj.value = obj.value.replace('.', '');
            document.getElementById(obj.id).value = obj.value;

        }





        function $onkeydown() {
            if ((event.keyCode == 46) || (event.keyCode == 8) || (event.keyCode == 116)) {
                event.keyCode = 0;
            }
        }



           function AutoDecimal2(obj) {
            obj.value = obj.value.replace(/\./gi, '');
            if (obj.value.replace(',', '.') != 0) {
                var a = obj.value.replace(',', '.');
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
                    //document.getElementById(obj.id).value = a.replace('.', ',')
                    document.getElementById(obj.id).value = risultato
                }
                else {
                    document.getElementById(obj.id).value = a.replace('.', ',')
                }
            }
        }







        function SostPuntVirg(e, obj) {
            var keyPressed;

            keypressed = (window.event) ? event.keyCode : e.which;
            if (keypressed == 46) {

                if (navigator.appName == 'Microsoft Internet Explorer') {
                    event.keyCode = 0;
                }
                else {
                    e.preventDefault();
                }


                obj.value += ',';
                obj.value = obj.value.replace('.', '');

            }

        };

       
       function AbilitaDrop(status)
            {
               status=!status;
                 document.getElementById('Ddl_mano').disabled = status;
                 document.getElementById('Ddl_sopL').disabled = status;
                 }


        function AbilitaTxtLF(status)
            {
               status=!status;
                 document.getElementById('lastraF_txt').disabled = status;
                
                 }
          
           function AbilitaTxtLPF(status)
            {
               status=!status;
                 document.getElementById('lastraPF_txt').disabled = status;
                
                 }

           function AbilitaTxtSerr(status)
            {
               status=!status;
                 document.getElementById('serr_txt').disabled = status;
                
                 }

              
    </script>
    <style type="text/css">
       
 
    .style1
    {
        color: #0000CC;
        font-size: 8pt;
    }

       
        #form1
        {
            width: 1305px;
        }
        .style5
        {
            width: 12%;
        }
        .style6
        {
            width: 5%;
        }
        .style8
        {
            height: 55px;
        }
        .style9
        {
            height: 19px;
        }
        .style16
        {
            height: 34px;
            width: 43px;
        }
        .style22
        {
            width: 315px;
            height: 24px;
        }
        .style23
        {
            height: 34px;
            width: 36px;
        }
        .style24
        {
            height: 34px;
        }
        .style42
        {
            width: 315px;
            height: 22px;
        }
        .style48
        {
            height: 25px;
        }
        .style49
        {
            height: 29px;
            width: 36px;
        }
        .style50
        {
            height: 29px;
        }
        .style51
        {
            height: 29px;
            width: 905px;
        }
        .style52
        {
            height: 39px;
            width: 36px;
        }
        .style53
        {
            height: 39px;
        }
        .style54
        {
            height: 39px;
            width: 905px;
        }
        .style55
        {
            width: 315px;
            height: 15px;
        }
        .style56
        {
            height: 34px;
            width: 97px;
        }
        .style57
        {
            height: 34px;
            width: 77px;
        }
        .style59
        {
            width: 14%;
        }
        .style62
        {
            width: 40%;
        }
        .style64
        {
            width: 2%;
        }
        .style65
        {
            width: 66%;
        }
        .style66
        {
            width: 63%;
        }
        </style>
</head>
<body style="width: 99%; position: absolute; background-image: url('IMMCENSIMENTO/Sfondo.png'); background-repeat: repeat-x;"
     bgcolor="#fdfdfd">
    





    <form id="form1" runat="server"
    style="width: 100%">
   
     <script type="text/javascript">
         if (navigator.appName == 'Microsoft Internet Explorer') {
             document.onkeydown = $onkeydown;
         }
         else {
             window.document.addEventListener("keydown", TastoInvio, true);
         }
    </script>
  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    
  
       <asp:UpdateProgress ID="UpdateProgressEventi" runat="server" DisplayAfter="0">
        <ProgressTemplate>
            <div style="margin: 0px; background-color: #C0C0C0; width: 100%; height: 100%; position: fixed;
                top: 0px; left: 0px; filter: alpha(opacity='75'); opacity: 0.75; background-color: #eeeeee;
                z-index: 500">
                <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
                    margin-top: -48px; background-image: url('Immagini/sfondo2.png');">
                    <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                        <tr>
                            <td valign="middle" align="center">
                                <asp:Image ID="Image2" runat="server" ImageUrl="..\NuoveImm\load.gif" />
                                <br />
                                <br />
                                <asp:Label ID="lblcarica" runat="server" Text="caricamento in corso..." Font-Names="Arial"
                                    Font-Size="10pt"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ProgressTemplate>
    </asp:UpdateProgress>





    <table style="width: 100%; height: 54px;">
        <tr>
           <td class="style6">
                &nbsp;
            </td>
              <td class="style59">
            
           


            <asp:Menu ID="TStampe" runat="server" Font-Names="arial" Font-Size="8pt" ForeColor="Black"
                    Orientation="Horizontal" RenderingMode="Table">
                    <DynamicHoverStyle BackColor="#C0FFC0" BorderWidth="1px" Font-Bold="True" ForeColor="#0000C0" />
                    <DynamicMenuItemStyle BackColor="#E9F1F5" Height="20px" ItemSpacing="2px" BorderStyle="None"
                        ForeColor="#0066FF" Width="150px" />
                    <DynamicMenuStyle BackColor="White" BorderStyle="Solid" BorderWidth="1px" HorizontalPadding="1px"
                        VerticalPadding="1px" />
                    <Items>
                        <asp:MenuItem ImageUrl="~/NuoveImm/btnStampe.png" Selectable="False" Value="">
                            <asp:MenuItem Text="Modulo pre-sloggio" Value="1"></asp:MenuItem>
                            <asp:MenuItem Text="Ricevuta ritiro chiavi" Value="2"></asp:MenuItem>
                            <asp:MenuItem Text="Rapporto sloggio" Value="3"></asp:MenuItem>
                        </asp:MenuItem>
                    </Items>
                </asp:Menu>





            </td>
                <td class="style5">
                &nbsp;<asp:ImageButton ID="btn_salva" runat="server" Tooltip="Salva" ImageUrl="~/NuoveImm/Img_Salva.png" style="cursor: pointer;"
                  />

                   

            </td>
               <td class="style66">
              <asp:UpdatePanel ID="UpdatePanel2" runat="server">
               <ContentTemplate>
                   
                      &nbsp;<asp:ImageButton ID="btn_verbale" runat="server" onClientclick="ApriVerbaleSloggio();" Tooltip="Esci" ImageUrl="~/NuoveImm/Img_Verbale.png" style="cursor: pointer;"
                     />

                      </ContentTemplate>

          <Triggers>
         
                        <asp:AsyncPostBackTrigger ControlID="btn_salva" EventName="Click" />
                       

                        </Triggers>

     </asp:UpdatePanel>


            </td>
             <td class="style5">
                &nbsp;<asp:ImageButton ID="ImButEsci" runat="server" onClientclick="ConfermaEsci();" Tooltip="Esci" ImageUrl="~/NuoveImm/Img_Esci.png" style="cursor: pointer;"
                />

                   

            </td>
        </tr>

    </table>

    <table style="width: 98%; margin-left: 20px; margin-right: 10px; height: 46px;" 
        cellpadding="0">
   
        <tr>
            <td class="style9">
                <asp:Label ID="titScheda" runat="server" Style="font-size: 12pt; color: #801f1c;
                    font-family: Arial; margin-left: 10px; text-align: center; margin-top: 0px;"
                    Width="99%" Height="20px"><strong>GESTIONE PRE-SLOGGIO IN SEGUITO A DISDETTA</strong></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>




 
            <table style="width: 99%; margin-left:13px; height: 63px;">
               
               <tr>
    

                 <td class="style1" style="font-family: Arial">
            <strong>DATI DELL'UNITA' IMMOBILIARE</strong>
        </td>




               </tr>

             

                <tr>
                    <td style="border: 1px solid #0066FF">
                        <table width="100%" style="height: 70px">
                           
                           
                            <tr>
                <td style="text-align: left" valign="middle" class="style8">
                    <asp:Label ID="Label21" runat="server" Font-Bold="True" Font-Size="10pt" Font-Names="Arial"></asp:Label>
                    <br />
                    <br />
                    <asp:Label class="stLbDescrizione" ID="lblPianoVendita" runat="server"  Font-Bold="True" Font-Size="10pt" Font-Names="Arial"
                        Text="Zona Decentramento" Visible="False" ></asp:Label>
                </td>
            </tr>
                           
                           
                        
                        </table></td></tr>

                        </table>


<br />
<br />
<br />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <table style="width: 99%; margin-left:13px; height: 63px;">
               
               <tr>
    

                 <td class="style1" style="font-family: Arial">
            <strong>INTERVENTI DA EFFETTUARE</strong>
        </td>




               </tr>

                <tr>
                    <td style="border: 1px solid #0066FF">
                        
                            <table style="width: 100%; height: 271px;">
                         
                                     <tr>
                                    <td width= "99%"; style="text-align: left">
                                       

                                        <asp:CheckBox ID="Chk_PB" runat="server" Text="Porta Blindata" Font-Names="arial" Font-Size="8pt" onclick="AbilitaDrop(this.checked)" />


                                            <table style="height: 53px; margin-bottom: 0px; width:100%">
                                            <tr>
                                            <td class="style64">
                                            
                                            
                                            </td>
                                            
                                            <td class="style6" >
                                            
                                            <asp:Label ID="Label4" Font-Names="arial" TabIndex="33" Font-Size="8pt" runat="server" Text="Mano"></asp:Label>

                                            
                                            </td>

                                             <td class="style65">
                                            
                                                 <asp:DropDownList ID="Ddl_mano" runat="server" Height="20px" Width="62px" Font-Size="8" Font-Names="arial">
                                                     <asp:ListItem Value="-1">---</asp:ListItem>
                                                     <asp:ListItem Value="0">dx</asp:ListItem>
                                                     <asp:ListItem Value="1">sx</asp:ListItem>
                                                 </asp:DropDownList>

                                            
                                            </td>
                                   
                                              <td width="60%">
                                                       &nbsp;<asp:CheckBox ID="Chk_preSL" runat="server" Font-Names="arial" Font-Size="8pt" 
                                            TabIndex="34" Text="Pre-Sloggio Completo" />
                                            </td>

                                             <td width="60%">
                                            
                                            </td>

                                            </tr>
                                            <tr>

                                               <td class="style64">
                                            
                                      
                                            
                                            </td>
                                            <td class="style6">
                                            
                                            <asp:Label ID="Label5" Font-Names="arial" TabIndex="33" Font-Size="8pt" runat="server" Text="Sopraluce"></asp:Label>
                                            
                                            </td>

                                             <td class="style65">
                                            
                                            <asp:DropDownList ID="Ddl_sopL" runat="server" Height="20px" Width="62px" Font-Size="8" Font-Names="arial">
                                                 <asp:ListItem Value="-1">---</asp:ListItem>
                                                 <asp:ListItem Value="0">si</asp:ListItem>
                                                 <asp:ListItem Value="1">no</asp:ListItem>
                                                 </asp:DropDownList>

                                            
                                            </td>

                                            <td width="60%">
                                                       &nbsp;<asp:CheckBox ID="Chk_SL" runat="server" Font-Names="arial" Font-Size="8pt" 
                                            TabIndex="34" Text="Sloggio Completo" />
                                            </td>

                                               <td class="style62">
                                            
                                            </td>

                                            </tr>


                                    

                                            </table>





                                    </td>
                                </tr>
                                    <tr>
                                    <td class="style55" style="text-align: left">
                                         &nbsp;<asp:CheckBox ID="CheckBox4" runat="server" Font-Names="arial" Font-Size="8pt" 
                                            TabIndex="34" Text="Sostituzione serratura porta blindata" />
                                    </td>
                                </tr>
                                <tr>


                                <td class="style48">

                                <table>
                                <tr>
                                 
                                    <td class="style52" style="text-align: left">
                                        <asp:CheckBox ID="Chk_nLF" runat="server" Font-Names="arial" Font-Size="8pt" 
                                            TabIndex="33" Text="N°" onclick="AbilitaTxtLF(this.checked)" />
                                   
                                    </td>
                                     <td class="style53" style="text-align: left">
                                       
                                       &nbsp; <asp:TextBox ID="lastraF_txt" runat="server" Font-Names="arial" 
                                            Font-Size="8pt" TabIndex="33" Width="25px"></asp:TextBox>
                                     
                                    </td>
                                     <td class="style54" style="text-align: left">
                                      
                                       &nbsp; <asp:Label ID="Label3" Font-Names="arial" TabIndex="33" Font-Size="8pt" runat="server" Text=" lastra di protezione finestra"></asp:Label>
                                    </td>


                                    </tr>
                                    <tr>
                                    <td class="style49" style="text-align: left">
                                        <asp:CheckBox ID="Chk_nLPF" runat="server" Font-Names="arial" Font-Size="8pt" 
                                            TabIndex="33" Text="N°" onclick="AbilitaTxtLPF(this.checked)"/>
                                   
                                    </td>
                                     <td class="style50" style="text-align: left">
                                       
                                       &nbsp; <asp:TextBox ID="lastraPF_txt" runat="server" Font-Names="arial" 
                                            Font-Size="8pt" TabIndex="33" Width="25px"></asp:TextBox>
                                     
                                    </td>
                                     <td class="style51" style="text-align: left">
                                      
                                       &nbsp; <asp:Label ID="Label1" Font-Names="arial" TabIndex="33" Font-Size="8pt" runat="server" Text=" lastra di protezione porta finestra"></asp:Label>
                                    </td>


                                    </tr>

                                     </table>

                                  </td>
                                </tr>
                                <tr>
                                    <td class="style22" style="text-align: left">
                                         &nbsp;<asp:CheckBox ID="ChPB3" runat="server" Font-Names="arial" Font-Size="8pt" 
                                            TabIndex="34" Text="Sostituzione serratura serranda box" />
                                    </td>
                                </tr>


                                   <tr>
                                    <td class="style42" style="text-align: left">
                                   
                                    <table style="width: 486px; height: 13px;">
                                <tr>
                                    <td class="style23" style="text-align: left">
                                        <asp:CheckBox ID="Chk_nSerr" runat="server" Font-Names="arial" Font-Size="8pt" 
                                            TabIndex="33" Text="N°"   onclick="AbilitaTxtSerr(this.checked)"/>
                                   
                                    </td>
                                     <td class="style16" style="text-align: left">
                                       
                                       &nbsp; <asp:TextBox ID="serr_txt" runat="server" Font-Names="arial" 
                                            Font-Size="8pt" TabIndex="33" Width="25px" ></asp:TextBox>
                                     
                                    </td>
                                     <td class="style24">
                                      
                                       &nbsp; <asp:Label ID="Label2" Font-Names="arial" TabIndex="33" Font-Size="8pt" runat="server" Text=" sostituzione serratura serranda negozio"></asp:Label>
                                    </td>

                            
                                    </tr>

                                    </table>

                                      
                          


                                    </td>
                                </tr>

                              
       

                   <tr>
                                <td>
                                        <table style="width: 532px; height: 99%">
                                <tr>
                                  
                                     <td class="style56">
                                      
                                       &nbsp;<asp:Label ID="Label6" Font-Names="arial" TabIndex="33" Font-Size="8pt" runat="server" Text=" Data Pre-Sloggio"></asp:Label>
                                    </td>

                                      <td class="style24">
                                      
                                       &nbsp; 
                                          <asp:TextBox ID="datapreSL_txt" runat="server" Font-Names="arial" 
                                            Font-Size="8pt" TabIndex="33" Width="90px"></asp:TextBox>
                                    </td>

                                      <td class="style57">
                                      
                                       &nbsp; <asp:Label ID="Label7" Font-Names="arial" TabIndex="33" Font-Size="8pt" runat="server" Text=" Data Sloggio"></asp:Label>
                                    </td>

                                      <td class="style24">
                                      
                                       &nbsp; 
                                          <asp:TextBox ID="dataSL_txt" runat="server" Font-Names="arial" 
                                            Font-Size="8pt" TabIndex="33" Width="90px"></asp:TextBox>
                                    </td>

                                    </tr>


                                
                                    </table>
                                    </td>
                                    </tr>



                            </table>
                        </td>
                   
                </tr>

                    </table>








<br />
<br />
<br />
<br />








<br />
<br />




    <asp:HiddenField ID="Modificato" runat="server" />
    <asp:HiddenField ID="idunita" runat="server" />
    <asp:HiddenField ID="EVENTO" runat="server" />
    <asp:HiddenField ID="idcontratto" runat="server" />
    <asp:HiddenField ID="CODICE" runat="server" />
 
    <asp:HiddenField ID="chiamante" runat="server" Value="0" />
    <asp:HiddenField ID="statoc" runat="server" />
    <asp:HiddenField ID="lettura" runat="server" />
    <asp:HiddenField ID="tipounita" runat="server" />
    <asp:HiddenField ID="datasloggio" runat="server" />


    <asp:HiddenField ID="via_civico" runat="server" />
    <asp:HiddenField ID="scala" runat="server" />
     <asp:HiddenField ID="piano" runat="server" />
      <asp:HiddenField ID="quartiere" runat="server" />

      <asp:HiddenField ID="sup_mq" runat="server" />
      <asp:HiddenField ID="interno" runat="server" />
      <asp:HiddenField ID="provenienza" runat="server" />
        <asp:HiddenField ID="id_stato" runat="server" />
    <asp:HiddenField ID="id_sloggio" runat="server" />
     <asp:HiddenField ID="stato_verb" runat="server" />


   </ContentTemplate>

          <Triggers>
         
                        <asp:AsyncPostBackTrigger ControlID="btn_salva" EventName="Click" />
                          <asp:AsyncPostBackTrigger ControlID="btn_verbale" EventName="Click" />

                        </Triggers>





     </asp:UpdatePanel>




 <div id="caric" style="margin: 0px; background-color: #C0C0C0; width: 100%; height: 100%;
        position: fixed; top: 0px; left: 0px; filter: alpha(opacity='75'); opacity: 0.75;
        background-color: #eeeeee; z-index: 500">
        <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
            margin-top: -48px; background-image: url('Immagini/sfondo2.png');">
            <table style="width: 100%; height: 100%" cellpadding="0" cellspacing="0">
                <tr>
                    <td valign="middle" align="center">
                        <asp:Image ID="Image2222" runat="server" ImageUrl="..\NuoveImm\load.gif" />
                        <br />
                        <br />
                        <asp:Label ID="lblcarica222" runat="server" Text="caricamento in corso..." Font-Names="Arial"
                            Font-Size="10pt"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <%Response.Flush()%>


    <script language="javascript" type="text/javascript">
          document.getElementById('caric').style.visibility = 'hidden';
    </script>








    </form>


 


</body>
</html>
