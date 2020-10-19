<%@ Page Language="VB" AutoEventWireup="false" CodeFile="StampaVerbaleSL.aspx.vb"
    Inherits="CENSIMENTO_StampaVerbaleSL" EnableEventValidation="False" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Rapporto Sloggio</title>
    <link href="../MANUTENZIONI/Styles/Site.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="function.js">
    </script>
    <script language="javascript" type="text/javascript">
        var Uscita;
        Uscita = 0;
    </script>
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

                       if((obj.value.substring(0,2)) <= 31 )
                       {
                       obj.value += "/";
                       
                       }
                       else {
                          if (window.event) {
                                event.keyCode = 0;
                            }
                            else {
                                e.preventDefault();
                            }
                       
                       }


                        
                    }
                    else if (obj.value.length == 5) {
                        if((obj.value.substring(3,5)) <= 12 )
                       {
                        obj.value += "/";
                        }else{
                        
                         if (window.event) {
                                event.keyCode = 0;
                            }
                            else {
                                e.preventDefault();
                            }
                        
                        
                        
                        }



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

  

            function StampaModulo() {
                window.open('ModuloPresloggio.aspx?', 'Modulo', '');

            }


            
            function StampaModuloRitiroChiavi() {
                window.open('ModuloRitChiavi.aspx?', 'Modulo', '');

            }



              function StampaModuloRapportoSloggio() {
                window.open('ModuloRappSloggio.aspx?', 'Modulo', '');

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




  function VisualizzaSloggio() {
              if (document.getElementById('idunita').value != '-1') 
                {
                    window.open('Sloggio.aspx?A=' + document.getElementById('chiamante').value + '&ID=' + document.getElementById('idunita').value, '');
   
            }

            
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
            'notnumbers': /[^\d\-\,]/g,
            'notnumbersint': /[^\d]/g

           
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



      




          function CalcolaAddebito(obj, val, addebito) {


            var qt = document.getElementById('' + obj.id + '').value.replace(",",".");

           
            var csu = val;
          


            var adTot;

            adTot = qt * csu;

           adTot=parseFloat(adTot).toFixed(2);

            document.getElementById('' + addebito.id + '').value = adTot

//            if (document.getElementById('' + addebito.id + '').value.indexOf('.') !=-1){



            document.getElementById('' + addebito.id + '').value = document.getElementById('' + addebito.id + '').value.replace(".",",");
       
//            }
        }

       

       function CalcolaTotale(obj) {
       
        document.getElementById('calcolaTot_btn').click();
       //document.getElementById('' + obj.id + '').focus();
       
       }



       function SettaTestoOncosto(obj) {
       
       
         var txt = document.getElementById('' + obj.id + '').value;

       

         if ((txt=='0') || ( txt=='0,00')) {
         
          txt = '';
         document.getElementById('' + obj.id + '').value= txt;
         
         }
       
      
      

       
       }





       function SettaTestoOutcosto(obj) {
       
       
         var txt = document.getElementById('' + obj.id + '').value;

       

         if ((txt=='') || (txt=='0')) {
         
          txt = '0,00';
          document.getElementById('' + obj.id + '').value= txt;
        
         
         }
       
      
      

       
       }


        function SettaTestoOnquant(obj) {
       
       
         var txt = document.getElementById('' + obj.id + '').value;

       

         if ((txt=='0') || ( txt=='0,00')) {
         
          txt = '';
         document.getElementById('' + obj.id + '').value= txt;
         
         }
       
      
      

       
       }





       function SettaTestoOutquant(obj) {
       
       
         var txt = document.getElementById('' + obj.id + '').value;

       

         if (txt=='') {
         
          txt = '0';
          document.getElementById('' + obj.id + '').value= txt;
        
         
         }
       
      
      

       
       }









    </script>
    <style type="text/css">
        .style1
        {
            color: #0000CC;
            font-size: 7pt;
        }
        
        
        #form1
        {
            width: 1305px;
        }
       
     
     

    
   
       
    
      
      
     
       
        </style>
</head>
<body style="font-size: 8pt; font-family: Arial; width: 99%; position: absolute;
    top: -12px; background-image: url('../MANUTENZIONI/Immagini/Sfondo.png'); background-repeat: repeat-x;"
    bgcolor="#fdfdfd">
    <form runat="server" style="width: 100%">
    <table style="width: 100%; height: 70px;">
        <tr>
            <td width= "5%">
                &nbsp;
            </td>
            <td  width= "10%">
                &nbsp;<asp:ImageButton ID="salva_btn" runat="server" ToolTip="Salva" ImageUrl="~/NuoveImm/Img_Salva.png"
                    Style="cursor: pointer;" ImageAlign="Top" />
            </td>
            <td style="width: 78%; height: 15px;">
                &nbsp;<asp:ImageButton ID="stampa_btn" runat="server" ToolTip="Stampa verbale completo"
                    OnClientClick="StampaVerbCompl();" ImageUrl="~/NuoveImm/Img_Stampa.png" Style="cursor: pointer;"
                    ImageAlign="Top" />
            </td>
            <td width= "12%">
                &nbsp;<asp:ImageButton ID="ImButEsci" runat="server" OnClientClick="ConfermaEsci();"
                    ToolTip="Esci" ImageUrl="~/NuoveImm/Img_Esci.png" Style="cursor: pointer;" ImageAlign="Top" />
            </td>
        </tr>
    </table>
    <table style="width: 98%; margin-left: 20px; margin-right: 10px; height: 38px;" cellpadding="0">
        <tr>
            <td  height= "19px">
                <asp:Label ID="titScheda" runat="server" Style="font-size: 12pt; color: #801f1c;
                    font-family: Arial; margin-left: 10px; text-align: center; margin-top: 0px;"
                    Width="99%" Height="21px"><strong>RAPPORTO DI SLOGGIO</strong Font-Names="Arial" Font-Size="8pt"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
            </td>
        </tr>
    </table>
    <table id="tutto" runat="server" style="width: 99%; margin-left: 13px; height: 63px;">
        <tr>
            <td>
                <table id="datiIntest1" runat="server" style="width: 100%;">
                    <tr>
                        <td class="style1" style="font-family: Arial" align="left">
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="100%" style="height: 124px; border: 1px solid #000000">
                                <tr>
                                    <td  width= "658px">
                                        <table style="border: 1px ridge #000000; width: 100%; height: 100%;">
                                            <tr>
                                                <td class="style4">
                                                    <asp:Label ID="Label7" runat="server" Text="SETTORE MANUTENTIVO:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="Label9" runat="server" Text="SEDE T.:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="Label11" runat="server" Text="PROPRIETA':" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="style4">
                                        <table style="border: 1px ridge #000000; width: 100%; height: 100%;">
                                            <tr>
                                                <td width= "71px">
                                                    <asp:Label ID="Label8" runat="server" Text="CODICE U.I.:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="txt_codiceUI1" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td width= "658px">
                                        <table style="border: 1px ridge #000000; width: 100%; height: 100%;">
                                            <tr>
                                                <td width= "67px">
                                                    <asp:Label ID="Label12" runat="server" Text="QUARTIERE:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td width= "221px">
                                                    <asp:Label ID="txt_quartiere1" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td width= "91px">
                                                    <asp:Label ID="Label13" runat="server" Text="VIA E N° CIVICO:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="txt_via1" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="style4">
                                        <table style="border: 1px ridge #000000; width: 100%; height: 100%;">
                                            <tr>
                                                <td width= "45px">
                                                    <asp:Label ID="Label14" runat="server" Text="SCALA:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="txt_scala1" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td width= "42px">
                                                    <asp:Label ID="Label15" runat="server" Text="PIANO:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="txt_piano1" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td width= "43px">
                                                    <asp:Label ID="Label16" runat="server" Text="N° U.I.:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="txt_nUI1" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td width= "53px">
                                                    <asp:Label ID="Label17" runat="server" Text="SUP MQ:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="txt_supmq1" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td width= "658px">
                                        <table style="border: 1px ridge #000000; width: 100%; height: 100%;">
                                            <tr>
                                                <td class="style4">
                                                    <asp:Label ID="Label18" runat="server" Text="COGNOME E NOME:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="style4">
                                        <table style="border: 1px ridge #000000; width: 100%; height: 100%;">
                                            <tr>
                                                <td class="style4">
                                                    <asp:Label ID="Label21" runat="server" Text="VIA E N° CIVICO NUOVA ABITAZIONE:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td width= "658px">
                                        <table style="border: 1px ridge #000000; width: 100%; height: 100%;">
                                            <tr>
                                                <td class="style4">
                                                    <asp:Label ID="Label19" runat="server" Text="DOCUMENTO DI RICONOSCIMENTO:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="Label20" runat="server" Text="TELEFONO:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td class="style4">
                                        <table style="border: 1px ridge #000000; width: 100%; height: 100%;">
                                            <tr>
                                                <td  width= "85px">
                                                    <asp:Label ID="Label22" runat="server" Text="DATA DISDETTA:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="txt_dataDisd1" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td  width= "107px">
                                                    <asp:Label ID="Label23" runat="server" Text="DATA PRE-SLOGGIO:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="txt_dataPreSL1" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td width= "85px">
                                                    <asp:Label ID="Label24" runat="server" Text="DATA SLOGGIO:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="txt_dataSL1" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td class="style206">
                            <table style="border: 1px ridge #000000; width: 100%; height: 100%;" width="100%">
                                <tr>
                                    <td class="style4" width="99%">
                                        <table>
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label25" runat="server" Text="MOTIVO:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <br />
            </td>
        </tr>
        <tr>
            <td>
                <table id="titoloDatiUI" runat="server" width="99%">
                    <tr>
                        <td class="style1" style="font-family: Arial;" width="99%">
                            DATI GENERALI DELL&#39;UI
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="99%">
                    <tr>
                        <td>
                            <table id="table1" align="center" runat="server" width="100%" cellpadding="0">
                                <tr>
                                    <td>
                                        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                            <asp:DataGrid ID="dgDatiUI" runat="server" AutoGenerateColumns="False" Style="page-break-after: always;"
                                                CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                                GridLines="None" Width="100%" BorderColor="Black" BorderStyle="Solid" BorderWidth="1px">
                                                <ItemStyle BackColor="#EFF3FB" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                                    Position="Top" VerticalAlign="Top" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <EditItemStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    ForeColor="White" />
                                                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="ID_TIPO" HeaderText="ID_TIPO" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ID_STATO_MAN" HeaderText="ID_STATO_MAN" Visible="False">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ID_MANUT1" HeaderText="ID_MANUT1" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ID_MANUT2" HeaderText="ID_MANUT2" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ID_MANUT3" HeaderText="ID_MANUT3" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ID_MANUT4" HeaderText="ID_MANUT4" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="STATO" HeaderText="DESCRIZIONE"></asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="STATO1">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="stato1" Text='<%#DataBinder.Eval(Container, "DataItem.MANUT1")%>'
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="STATO2">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="stato2" Text='<%#DataBinder.Eval(Container, "DataItem.MANUT2")%>'
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="STATO3">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="stato3" Text='<%#DataBinder.Eval(Container, "DataItem.MANUT3")%>'
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="STATO4">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="stato4" Text='<%#DataBinder.Eval(Container, "DataItem.MANUT4")%>'
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="UM" HeaderText="U.M."></asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="QTA'">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="quantita_txt" runat="server" Width="40px" Style="text-align: right;"
                                                                TabIndex="5"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="COSTO" HeaderText="COSTO UNITARIO"></asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="ADDEBITO">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="addebito_txt" runat="server" Width="55px" Style="text-align: right;"
                                                                TabIndex="6"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </span></strong>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="datiIntest2" runat="server" style="width: 100%;">
                    <tr>
                        <td class="style1" style="font-family: Arial" align="left">
                        </td>
                    </tr>
                    <tr>
                        <td height= "102px">
                            <table width="100%" style="border: 1px solid #000000">
                                <tr>
                                    <td  style="width: 658px; height: 32px;">
                                        <table style="border: 1px ridge #000000; width: 100%;">
                                            <tr>
                                                <td class="style4">
                                                    <asp:Label ID="Label26" runat="server" Text="SETTORE MANUTENTIVO:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="Label27" runat="server" Text="SEDE T.:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="Label28" runat="server" Text="PROPRIETA':" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td height= "32px">
                                        <table style="border: 1px ridge #000000; width: 100%;">
                                            <tr>
                                                <td width= "71px">
                                                    <asp:Label ID="Label29" runat="server" Text="CODICE U.I.:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="txt_codiceUI2" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style= "width: 658px; height: 30px;">
                                        <table style="border: 1px ridge #000000; width: 100%;">
                                            <tr>
                                                <td width= "67px">
                                                    <asp:Label ID="Label31" runat="server" Text="QUARTIERE:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td width= "221px">
                                                    <asp:Label ID="txt_quartiere2" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td width= "91px">
                                                    <asp:Label ID="Label33" runat="server" Text="VIA E N° CIVICO:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="txt_via2" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td  height= "30px">
                                        <table style="border: 1px ridge #000000; width: 100%;">
                                            <tr>
                                                <td  width= "45px">
                                                    <asp:Label ID="Label35" runat="server" Text="SCALA:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="txt_scala2" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td width= "42px">
                                                    <asp:Label ID="Label37" runat="server" Text="PIANO:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="txt_piano2" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td width= "43px">
                                                    <asp:Label ID="Label39" runat="server" Text="N° U.I.:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="txt_nUI2" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td width= "53px">
                                                    <asp:Label ID="Label41" runat="server" Text="SUP MQ:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="txt_supmq2" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="titoloSanit" runat="server" width="99%">
                    <tr>
                        <td class="style1" style="font-family: Arial;">
                            <strong>SANITARI E RUBINETTERIE</strong>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="99%">
                    <tr>
                        <td>
                            <table id="table2" align="center" runat="server" width="100%" cellpadding="0">
                                <tr>
                                    <td>
                                        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                            <asp:DataGrid ID="dgSanit" runat="server" AutoGenerateColumns="False" Style="page-break-after: always;"
                                                CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                                GridLines="None" Width="100%" BorderColor="Black" BorderWidth="1px">
                                                <ItemStyle BackColor="#EFF3FB" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                                    Position="Top" VerticalAlign="Top" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <EditItemStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    ForeColor="White" />
                                                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="ID_TIPO" HeaderText="ID_TIPO" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ID_STATO_MAN" HeaderText="ID_STATO_MAN" Visible="False">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ID_MANUT1" HeaderText="ID_MANUT1" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ID_MANUT2" HeaderText="ID_MANUT2" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ID_MANUT3" HeaderText="ID_MANUT3" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ID_MANUT4" HeaderText="ID_MANUT4" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="STATO" HeaderText="DESCRIZIONE"></asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="STATO1">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="stato1" Text='<%#DataBinder.Eval(Container, "DataItem.MANUT1")%>'
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="STATO2">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="stato2" Text='<%#DataBinder.Eval(Container, "DataItem.MANUT2")%>'
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="STATO3">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="stato3" Text='<%#DataBinder.Eval(Container, "DataItem.MANUT3")%>'
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="STATO4">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="stato4" Text='<%#DataBinder.Eval(Container, "DataItem.MANUT4")%>'
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="UM" HeaderText="U.M."></asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="QTA'">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="quantita_txt" runat="server" Width="40px" Style="text-align: right;"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="COSTO" HeaderText="COSTO UNITARIO"></asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="ADDEBITO">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="addebito_txt" runat="server" Width="55px" Style="text-align: right;"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </span></strong>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table id="datiIntest3" runat="server" style="width: 100%;">
                    <tr>
                        <td class="style1" style="font-family: Arial" align="left">
                        </td>
                    </tr>
                    <tr>
                        <td height= "102px">
                            <table width="100%" style="border: 1px solid #000000">
                                <tr>
                                    <td style="width: 658px; height: 32px;">
                                        <table style="border: 1px ridge #000000; width: 100%;">
                                            <tr>
                                                <td class="style4">
                                                    <asp:Label ID="Label6" runat="server" Text="SETTORE MANUTENTIVO:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="Label43" runat="server" Text="SEDE T.:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="Label44" runat="server" Text="PROPRIETA':" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td height= "32px">
                                        <table style="border: 1px ridge #000000; width: 100%;">
                                            <tr>
                                                <td width= "71px">
                                                    <asp:Label ID="Label45" runat="server" Text="CODICE U.I.:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="txt_codiceUI3" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style= "width: 658px; height: 30px;">
                                        <table style="border: 1px ridge #000000; width: 100%;">
                                            <tr>
                                                <td width= "67px">
                                                    <asp:Label ID="Label47" runat="server" Text="QUARTIERE:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td width= "221px">
                                                    <asp:Label ID="txt_quartiere3" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td width= "91px">
                                                    <asp:Label ID="Label49" runat="server" Text="VIA E N° CIVICO:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="txt_via3" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td height= "30px">
                                        <table style="border: 1px ridge #000000; width: 100%;">
                                            <tr>
                                                <td  width= "45px">
                                                    <asp:Label ID="Label51" runat="server" Text="SCALA:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="txt_scala3" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td width= "42px">
                                                    <asp:Label ID="Label53" runat="server" Text="PIANO:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="txt_piano3" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td width= "43px">
                                                    <asp:Label ID="Label55" runat="server" Text="N° U.I.:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="txt_nUI3" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td width= "53px">
                                                    <asp:Label ID="Label57" runat="server" Text="SUP MQ:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td class="style4">
                                                    <asp:Label ID="txt_supmq3" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="99%">
                    <tr>
                        <td class="style1" style="font-family: Arial;">
                            <strong>SERRAMENTI INTERNI ED ESTERNI</strong>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table width="99%">
                    <tr>
                        <td>
                            <table id="table3" width= "98%" align="center" runat="server" cellpadding="0">
                                <tr>
                                    <td>
                                        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                            <asp:DataGrid ID="dgSerram" runat="server" AutoGenerateColumns="False" Style="page-break-after: always;"
                                                CellPadding="4" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                                Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                                GridLines="None" Width="100%" BorderColor="Black" BorderWidth="1px">
                                                <ItemStyle BackColor="#EFF3FB" />
                                                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                                    Position="Top" VerticalAlign="Top" />
                                                <AlternatingItemStyle BackColor="White" />
                                                <EditItemStyle BackColor="#2461BF" />
                                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                    ForeColor="White" />
                                                <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                <Columns>
                                                    <asp:BoundColumn DataField="ID_TIPO" HeaderText="ID_TIPO" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ID_STATO_MAN" HeaderText="ID_STATO_MAN" Visible="False">
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ID_MANUT1" HeaderText="ID_MANUT1" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ID_MANUT2" HeaderText="ID_MANUT2" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ID_MANUT3" HeaderText="ID_MANUT3" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="ID_MANUT4" HeaderText="ID_MANUT4" Visible="False"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA"></asp:BoundColumn>
                                                    <asp:BoundColumn DataField="STATO" HeaderText="DESCRIZIONE"></asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="STATO1">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="stato1" Text='<%#DataBinder.Eval(Container, "DataItem.MANUT1")%>'
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="STATO2">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="stato2" Text='<%#DataBinder.Eval(Container, "DataItem.MANUT2")%>'
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="STATO3">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="stato3" Text='<%#DataBinder.Eval(Container, "DataItem.MANUT3")%>'
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="STATO4">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="stato4" Text='<%#DataBinder.Eval(Container, "DataItem.MANUT4")%>'
                                                                runat="server" />
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="UM" HeaderText="U.M."></asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="QTA'">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="quantita_txt" runat="server" Width="40px" Style="text-align: right;"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn DataField="COSTO" HeaderText="COSTO UNITARIO"></asp:BoundColumn>
                                                    <asp:TemplateColumn HeaderText="ADDEBITO">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="addebito_txt" runat="server" Width="55px" Style="text-align: right;"></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                </Columns>
                                            </asp:DataGrid>
                                        </span></strong>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <%--</table>--%>
                    <tr>
                        <td>
                            <table id="datiIntest4" runat="server" style="width: 100%;">
                                <tr>
                                    <td class="style1" style="font-family: Arial" align="left">
                                    </td>
                                </tr>
                                <tr>
                                    <td height= "102px">
                                        <table width="100%" style="border: 1px solid #000000">
                                            <tr>
                                                <td style="width: 658px; height: 32px;">
                                                    <table style="border: 1px ridge #000000; width: 100%;">
                                                        <tr>
                                                            <td class="style4">
                                                                <asp:Label ID="Label59" runat="server" Text="SETTORE MANUTENTIVO:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td class="style4">
                                                                <asp:Label ID="Label60" runat="server" Text="SEDE T.:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td class="style4">
                                                                <asp:Label ID="Label61" runat="server" Text="PROPRIETA':" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td height= "32px">
                                                    <table style="border: 1px ridge #000000; width: 100%;">
                                                        <tr>
                                                            <td width= "71px">
                                                                <asp:Label ID="Label62" runat="server" Text="CODICE U.I.:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td class="style4">
                                                                <asp:Label ID="txt_codiceUI4" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style= "width: 658px; height: 30px;">
                                                    <table style="border: 1px ridge #000000; width: 100%;">
                                                        <tr>
                                                            <td width= "67px">
                                                                <asp:Label ID="Label64" runat="server" Text="QUARTIERE:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td width= "221px">
                                                                <asp:Label ID="txt_quartiere4" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td width= "91px">
                                                                <asp:Label ID="Label66" runat="server" Text="VIA E N° CIVICO:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td class="style4">
                                                                <asp:Label ID="txt_via4" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                                <td height= "30px">
                                                    <table style="border: 1px ridge #000000; width: 100%;">
                                                        <tr>
                                                            <td  width= "45px">
                                                                <asp:Label ID="Label68" runat="server" Text="SCALA:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td class="style4">
                                                                <asp:Label ID="txt_scala4" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td width= "42px">
                                                                <asp:Label ID="Label70" runat="server" Text="PIANO:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td class="style4">
                                                                <asp:Label ID="txt_piano4" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td width= "43px">
                                                                <asp:Label ID="Label72" runat="server" Text="N° U.I.:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td class="style4">
                                                                <asp:Label ID="txt_nUI4" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td width= "53px">
                                                                <asp:Label ID="Label74" runat="server" Text="SUP MQ:" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                            </td>
                                                            <td class="style4">
                                                                <asp:Label ID="txt_supmq4" runat="server" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table id="titoloPavim" runat="server" width="99%">
                                <tr>
                                    <td class="style1" style="font-family: Arial">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table width="99%">
                                <tr>
                                    <td>
                                        <table id="table4" align="center" runat="server" width="100%" cellpadding="0">
                                            <tr>
                                                <td>
                                                    <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                                        <asp:DataGrid ID="dgPavim" runat="server" AutoGenerateColumns="False" CellPadding="4"
                                                            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                                                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#333333"
                                                            GridLines="None" Width="100%" BorderColor="Black" BorderWidth="1px">
                                                            <ItemStyle BackColor="#EFF3FB" />
                                                            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" Mode="NumericPages"
                                                                Position="Top" VerticalAlign="Top" />
                                                            <AlternatingItemStyle BackColor="White" />
                                                            <EditItemStyle BackColor="#2461BF" />
                                                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                                                            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                                                ForeColor="White" />
                                                            <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                                                            <Columns>
                                                                <asp:BoundColumn DataField="ID_TIPO" HeaderText="ID_TIPO" Visible="False"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="ID_STATO_MAN" HeaderText="ID_STATO_MAN" Visible="False">
                                                                </asp:BoundColumn>
                                                                <asp:BoundColumn DataField="ID_MANUT1" HeaderText="ID_MANUT1" Visible="False"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="ID_MANUT2" HeaderText="ID_MANUT2" Visible="False"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="ID_MANUT3" HeaderText="ID_MANUT3" Visible="False"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="ID_MANUT4" HeaderText="ID_MANUT4" Visible="False"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="STATO" HeaderText="DESCRIZIONE"></asp:BoundColumn>
                                                                <asp:TemplateColumn HeaderText="STATO1">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="stato1" Text='<%#DataBinder.Eval(Container, "DataItem.MANUT1")%>'
                                                                            runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="STATO2">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="stato2" Text='<%#DataBinder.Eval(Container, "DataItem.MANUT2")%>'
                                                                            runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="STATO3">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="stato3" Text='<%#DataBinder.Eval(Container, "DataItem.MANUT3")%>'
                                                                            runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="STATO4">
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="stato4" Text='<%#DataBinder.Eval(Container, "DataItem.MANUT4")%>'
                                                                            runat="server" />
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:BoundColumn DataField="UM" HeaderText="U.M."></asp:BoundColumn>
                                                                <asp:TemplateColumn HeaderText="QTA'">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="quantita_txt" runat="server" Width="40px" Style="text-align: right;"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:BoundColumn DataField="COSTO" HeaderText="COSTO UNITARIO"></asp:BoundColumn>
                                                                <asp:TemplateColumn HeaderText="ADDEBITO">
                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="addebito_txt" runat="server" Width="55px" Style="text-align: right;"></asp:TextBox>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </span></strong>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table id="titoloNote" runat="server" width="99%">
                                <tr>
                                    <td class="style1" style="font-family: Arial">
                                        <strong>NOTE DEL TECNICO</strong>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 100%">
                            <table id="note" runat="server" width="100%">
                                <tr>
                                    <td style="border: 1px solid #000000; width: 100%">
                                        <table style="width: 100%; height: 94px;">
                                            <tr>
                                                <td width="10%">
                                                    <asp:Label ID="Label86" runat="server" Text="NOTE" Width="61px" Height="19px" Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td width="90%">
                                                    <asp:TextBox ID="txtNote" CssClass="CssMaiuscolo" Style="margin-left: 10px" runat="server"
                                                        Font-Names="ARIAL" Font-Size="10pt" MaxLength="500" Height="72px" TextMode="MultiLine"
                                                        TabIndex="25" Width="99%"></asp:TextBox>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="margin-left: 13px;" width="99%">
                            <table id="tbl_totale" runat="server" style="width: 100%;">
                                <tr>
                                    <td style="text-align: left; border: 1px solid #000000; margin-left: 40px" valign="middle">
                                        <table width="100%">
                                            <tr>
                                                <td style="text-align: right" valign="middle" align="right" width= "54%">
                                                    <asp:Label ID="Label1" runat="server" Text="TOTALE ADDEBITI ESCLUSO IVA:" Width="669px"
                                                         Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                    <br />
                                                </td>
                                                <td align="right" width= "35%">
                                                    <asp:Label ID="Label4" runat="server" Text="€" Width="265px" Height="16px" 
                                                        Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td style="text-align: right" valign="middle" align="right" width= "12%">
                                                    <asp:TextBox ID="totAdd_txt" Style="text-align: right" runat="server">0</asp:TextBox>
                                                    <br />
                                                </td>
                                                <td style="text-align: left" valign="middle">
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="margin-left: 13px;" width="99%">
                            <table id="tbl_generalitaUI" runat="server" style="width: 100%;">
                                <tr>
                                    <td style="border: 1px solid #000000; margin-left: 40px" valign="middle">
                                        <table width="100%" style="height: 29px">
                                            <tr>
                                                <td width="95%">
            <table style=" width: 98%;">
                <tr>
                    <td>
                     <div style="overflow: auto; height: 250px;">
                        <table style="width: 100%; height:96%;">
                            <tr>
                                <td width="20%" colspan="2">
                                    <table width="100%">
                                        <tr>
                                            <td width= "50%">
                                    <asp:Label ID="Label87" Font-Names="arial" Font-Size="8pt" runat="server" Text="Ascensore"></asp:Label>
                                            </td>
                                            <td>
                                    <asp:DropDownList ID="ddl_ascensore" runat="server" Height="20px" Width="62px" Font-Size="8pt"
                                        TabIndex="10" Font-Names="arial">
                                       
                                        <asp:ListItem Value="0">NO</asp:ListItem>
                                        <asp:ListItem Value="1">SI</asp:ListItem>
                                    </asp:DropDownList>
                                            </td>
                                        </tr>
                                        </table>
                                </td>
                                <td width= "67%">
                                    &nbsp;</td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td  style= "width: 14%;height: 70px;">
                                    <asp:Label ID="Label65" Font-Names="arial" Font-Size="8pt" runat="server" Text="Accessibile Carrozzina"></asp:Label>
                                </td>
                                <td  style= "width: 29%;height: 70px;" colspan="2">
                                    <table style="width:100%; height:100%">
                                        <tr>
                                            <td  style="width: 12%; height: 24px;">
                                                <asp:CheckBox ID="chk_scivoli" runat="server" Height="20px" Width="82px" Text="Scivoli"
                                                    Font-Size="8pt" TabIndex="10" Font-Names="arial"></asp:CheckBox>
                                            </td>
                                            <td style="width: 829px; height: 24px;">
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td style="width: 12%; height: 24px;">
                                                <asp:CheckBox ID="chk_montascale" runat="server" Height="20px" Width="78px" Text="Montascale"
                                                    Font-Size="8pt" TabIndex="10" Font-Names="arial" 
                                                    ></asp:CheckBox>
                                            </td>
                                            <td  style="width: 829px; height: 24px;">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </td>
                                <td style="width: 4px;height: 70px;">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td width= "14%">
                                    <asp:Label ID="Label88" Font-Names="arial" Font-Size="8pt" runat="server" 
                                        Text="Foro di Areazione"></asp:Label>
                                </td>
                                <td width= "29%" colspan="2">
                                    <table style="width:100%; height:100%;" >
                                        <tr>
                                            <td  width= "12%">
                                                <asp:CheckBox ID="chk_esistente" runat="server" Height="20px" Width="65px" Text="Esistente"
                                                    Font-Size="8pt" TabIndex="10" Font-Names="arial" ></asp:CheckBox>
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td width= "12%">
                                                <asp:CheckBox ID="chk_locale" runat="server" Height="20px" Width="80px" Text="Locale"
                                                    Font-Size="8pt" TabIndex="10" Font-Names="arial" onclick="AbilitaTxt(this.checked)"></asp:CheckBox>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="txt_locale" runat="server" Width="250px"></asp:TextBox>


                                            </td>
                                        </tr>
                                    </table>
        </td>
                                <td  width= "4px"> &nbsp; </td>
    </tr>


    <tr>
    <td  width= "14%">
                                    <asp:Label ID="Label89" Font-Names="arial" Font-Size="8pt" runat="server" 
                                        Text="Condizioni Unità Immobiliare" Width="147px"></asp:Label>
                                </td>
                                <td width= "29%" colspan="2">
                                    <table width="100%">
                                        <tr>
                                            <td  width= "14%">
                                               <asp:Label ID="Label90" Font-Names="arial" Font-Size="8pt" runat="server" 
                                                    Text="Stato di Conservazione" Width="117px" ForeColor="Black"></asp:Label>
                                            </td>
                                            <td width= "18%">
                                                <asp:DropDownList ID="ddl_statocons" runat="server" Height="20px" 
                                                    Width="111px" Font-Size="8pt"
                                                    TabIndex="10" Font-Names="arial">
                                                    <asp:ListItem Value="-1">---</asp:ListItem>
                                                    <asp:ListItem Value="0">NORMALE</asp:ListItem>
                                                    <asp:ListItem Value="1">MEDIOCRE</asp:ListItem>
                                                    <asp:ListItem Value="2">SCADENTE</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td  width= "5%">
                                               <asp:Label ID="Label91" Font-Names="arial" Font-Size="8pt" runat="server" 
                                                    Text="Livello" Width="41px"></asp:Label>
                                            </td>
                                            <td width= "18%">
                                                <asp:DropDownList ID="ddl_livello" runat="server" Height="20px" 
                                                    Width="110px" Font-Size="8pt"
                                                    TabIndex="10" Font-Names="arial">
                                                    <asp:ListItem Value="-1">---</asp:ListItem>
                                                    <asp:ListItem Value="0">BASSO</asp:ListItem>
                                                    <asp:ListItem Value="1">MEDIO</asp:ListItem>
                                                    <asp:ListItem Value="2">ALTO</asp:ListItem>
                                                </asp:DropDownList>
                                            </td>
                                            <td  width= "11%">
                                               <asp:Label ID="Label67" Font-Names="arial" Font-Size="8pt" runat="server" 
                                                    Text="U.I. Recuperabile" Width="88px"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:DropDownList ID="ddl_UIRecuperabile" runat="server" Height="20px" Width="62px" Font-Size="8pt"
                                                    TabIndex="10" Font-Names="arial">
                                
                                                    <asp:ListItem Value="0">NO</asp:ListItem>
                                                    <asp:ListItem Value="1">SI</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        </table>
        </td>
        <td> &nbsp;</td>
        </tr>











    <tr>
    <td  width= "14%"">
                                   </td>
                                <td width= "29%" colspan="2">
                                    &nbsp;</td>
        <td> &nbsp;</td>
        </tr>











    </table>  </div> </td> </tr> </table> 
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="margin-left: 13px;" width="99%">
                            <table id="tbl_stima" runat="server" style="width: 100%;">
                                <tr>
                                    <td style="text-align: left; border: 1px solid #000000; margin-left: 40px" valign="middle">
                                        <table width="100%" style="height: 33px">
                                            <tr>
                                                <td style="text-align: right" valign="middle" align="right" width= "54%">
                                                    <asp:Label ID="Label2" runat="server" Text="STIMA DEI COSTI DI RIATTAMENTO:" Width="672px"
                                                         Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                    <br />
                                                </td>
                                                <td align="right" width= "35%">
                                                    <asp:Label ID="Label5" runat="server" Text="€" Width="265px" Height="16px" 
                                                        Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td style="text-align: right" valign="middle" align="right" width= "12%">
                                                    <asp:TextBox ID="stimaCosti_txt" Style="text-align: right" runat="server">0</asp:TextBox>
                                                    <br />
                                                </td>
                                                <td style="text-align: left" valign="middle">
                                                    <br />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="margin-left: 13px;" width="99%">
                            <table id="tbl_adeguamento" runat="server" style="width: 100%;">
                                <tr>
                                    <td style="text-align: left; border: 1px solid #000000; margin-left: 40px" valign="middle">
                                        <table style="height: 33px; width: 100%;">
                                            <tr>
                                                <td style="text-align: right" valign="middle" align="right" width= "54%">
                                                    <asp:Label ID="Label3" runat="server" Text="ADEGUAMENTO NORMATIVO:" Width="678px"
                                                        Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td align="right" width= "35%">
                                                    <asp:Label ID="Label76" runat="server" Text="€" Width="265px" Height="16px" 
                                                        Font-Names="Arial" Font-Size="8pt"></asp:Label>
                                                </td>
                                                <td style="text-align: right" valign="middle" align="right" width= "12%">
                                                    <asp:TextBox ID="adNormativo_txt" Style="text-align: right" runat="server">0</asp:TextBox>
                                                    <br />
                                                </td>
                                                <td style="text-align: left" valign="middle">
                                                    <br />
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
            </td>
        </tr>
    </table>
    <div>
        <asp:Button ID="calcolaTot_btn" runat="server" Text="Button" Style="visibility: hidden;" />
        <asp:Label ID="Label10" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="10pt"
            ForeColor="Red" Visible="False"></asp:Label>
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
        <asp:HiddenField ID="tabSelect" runat="server" Value="-1" />
        <asp:HiddenField ID="id_stato" runat="server" />
        <asp:HiddenField ID="id_sloggio" runat="server" />
        <asp:HiddenField ID="st_1" runat="server" />
        <asp:HiddenField ID="st_2" runat="server" />
        <asp:HiddenField ID="st_3" runat="server" />
        <asp:HiddenField ID="st_4" runat="server" />
        <asp:HiddenField ID="stato_verb" runat="server" />
        <asp:HiddenField ID="sola_lettura" runat="server" Value="0" />
    </div>
    <script type="text/javascript" language="javascript">
        var Selezionato;
        var OldColor;
        var SelColo;




    </script>
    </form>
</body>
</html>
