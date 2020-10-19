<%@ Page Language="VB" AutoEventWireup="false" CodeFile="LiberiAbusivi.aspx.vb" Inherits="Condomini_LiberiAbusivi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<base target="_self"/>
   <script type="text/javascript" src="prototype.lite.js"></script>
   <script type="text/javascript" src="moo.fx.js"></script>
   <script type="text/javascript" src="moo.fx.pack.js"></script>
   
   
   <script type="text/javascript" language="javascript" >
        window.name="modal";
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
    function TastoInvio(e) {
        sKeyPressed1 = e.which;
        if (sKeyPressed1 == 13) {

            e.preventDefault();
        }
    }
    function $onkeydown() {
        if (event.keyCode == 13) {
            //alert('ATTENZIONE!E\'stato premuto erroneamente il tasto invio! Utilizzare il mouse o il tasto TAB per spostarsi nei campi di testo!');
            //history.go(0);
            document.getElementById('txtModificato').value = '111'
            event.keyCode = 9;
        }
    }

    function SostPuntVirg(e, obj) {
        var keyPressed;
        keypressed = (window.event) ? event.keyCode : e.which;
        if (keypressed == 46) {
            event.keyCode = 0;
            obj.value += ',';
            obj.value = obj.value.replace('.', '');
        }

    };

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
    
//    function AutoDecimal2(obj) {
//        if (obj.value.replace(',', '.') != 0) {
//            var a = obj.value.replace(',', '.');
//            a = parseFloat(a).toFixed(2)
//            document.getElementById(obj.id).value = a.replace('.', ',')
//        }
//    }
    function AutoDecimal2(obj) {
        obj.value = obj.value.replace('.', '');
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
                document.getElementById(obj.id).value = risultato
            }
            else {
                document.getElementById(obj.id).value = a.replace('.', ',')
            }

        }
    }
    function ChiamaReport() {
       if (document.getElementById('txtModificato').value != '1') {
           window.open('RptLiberiAbusivi.aspx?IDGESTIONE=<%=vIdGestione %>&CHIAMA=<%=vChiama %>&IDCON=<%=vIdConnModale %>', 'RptLibeAbus', '');
       }
       else { 
       alert ('Salvare le modifiche prima di stampare!')
       }
   }
   function ConfermaUscita() {
       if (document.getElementById('txtModificato').value == 1) {
           var chiediConferma
           chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche.Uscire senza salvare?");
           if (chiediConferma == true) {
               document.getElementById('txtesci').value = '1';
               //                '<%=Session("MODIFYMODAL")%>' = '1';
               //  
           }

       }
       else {
           document.getElementById('txtesci').value = '1';
       }

   }
   
//   function SommaDaServ(obj) {
//       var risultato
//       var servizi = parseFloat(obj.value.replace(',', '.'));
//       var ascensore = parseFloat(document.getElementById(obj.id.replace('txtImporto', 'txtImportoAsc')).value.replace(',', '.'));
//       var riscaldamento = parseFloat(document.getElementById(obj.id.replace('txtImporto', 'txtImportoRisc')).value.replace(',', '.'));

//            if (isNaN (servizi)){
//            servizi = 0
//            }
//            if (isNaN (ascensore)){
//            ascensore=0
//            }
//            if (isNaN (riscaldamento)){
//            riscaldamento=0
//            }
//            
//           risultato = parseFloat(servizi)+ parseFloat(ascensore)+ parseFloat(riscaldamento)
//           risultato = parseFloat(risultato).toFixed(2)
//           document.getElementById(obj.id.replace('txtImporto', 'txtTOT')).value = risultato.replace('.', ',')
//       }
   //
   function SommaDaServ(obj) {
       var a
       var servizi = obj.value.replace('.', '');
       servizi = parseFloat(servizi.replace(',', '.'));
       var ascensore = document.getElementById(obj.id.replace('txtImporto', 'txtImportoAsc')).value.replace('.', '');
       ascensore = parseFloat (ascensore .replace(',','.'));
       var riscaldamento = document.getElementById(obj.id.replace('txtImporto', 'txtImportoRisc')).value.replace('.', '');
       riscaldamento = parseFloat(riscaldamento .replace(',','.'));
       
       if (isNaN(servizi)) {
           servizi = 0
       }
       if (isNaN(ascensore)) {
           ascensore = 0
       }
       if (isNaN(riscaldamento)) {
           riscaldamento = 0
       }

       a = parseFloat(servizi) + parseFloat(ascensore) + parseFloat(riscaldamento)
       a = parseFloat(a).toFixed(2)
       if (a.substring(a.length - 3, 0).length >= 4) {
           var decimali = a.substring(a.length, a.length - 2);
           var dascrivere = a.substring(a.length - 3, 0);
           var risultato = '';
           while (dascrivere.replace('-', '').length >= 4) {
               risultato = risultato + '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3)
               dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
           }
           risultato = dascrivere + risultato + ',' + decimali
           document.getElementById(obj.id.replace('txtImporto', 'txtTOT')).value = risultato
       }
       else {
           document.getElementById(obj.id.replace('txtImporto', 'txtTOT')).value = a.replace('.', ',')
       }

   }
       function SommaDaAsc(obj) {
           var a
           var servizi = document.getElementById(obj.id.replace('txtImportoAsc', 'txtImporto')).value.replace('.', '');
           servizi = parseFloat(servizi.replace(',', '.'));
           var ascensore = obj.value.replace('.', '');
           ascensore = parseFloat(ascensore.replace(',', '.'));
           var riscaldamento = document.getElementById(obj.id.replace('txtImportoAsc', 'txtImportoRisc')).value.replace('.', '');
           riscaldamento = parseFloat(riscaldamento.replace(',', '.'));

           if (isNaN(servizi)) {
               servizi = 0
           }
           if (isNaN(ascensore)) {
               ascensore = 0
           }
           if (isNaN(riscaldamento)) {
               riscaldamento = 0
           }

           a = parseFloat(servizi) + parseFloat(ascensore) + parseFloat(riscaldamento)
           a = parseFloat(a).toFixed(2)
           if (a.substring(a.length - 3, 0).length >= 4) {
               var decimali = a.substring(a.length, a.length - 2);
               var dascrivere = a.substring(a.length - 3, 0);
               var risultato = '';
               while (dascrivere.replace('-', '').length >= 4) {
                   risultato = risultato + '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3)
                   dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
               }
               risultato = dascrivere + risultato + ',' + decimali
               document.getElementById(obj.id.replace('txtImportoAsc', 'txtTOT')).value = risultato
           }
           else {
               document.getElementById(obj.id.replace('txtImportoAsc', 'txtTOT')).value = a.replace('.', ',')
           }
       }

       function SommaDaRisc(obj) {

           var a
           var servizi = document.getElementById(obj.id.replace('txtImportoRisc', 'txtImporto')).value.replace('.', '');
           servizi = parseFloat(servizi.replace(',', '.'));

           var ascensore = document.getElementById(obj.id.replace('txtImportoRisc', 'txtImportoAsc')).value.replace('.', '');
           ascensore = parseFloat(ascensore.replace(',', '.'));

           var riscaldamento = obj.value.replace('.', '');
           riscaldamento = parseFloat(riscaldamento.replace(',', '.'));

           if (isNaN(servizi)) {
               servizi = 0
           }
           if (isNaN(ascensore)) {
               ascensore = 0
           }
           if (isNaN(riscaldamento)) {
               riscaldamento = 0
           }

           a = parseFloat(servizi) + parseFloat(ascensore) + parseFloat(riscaldamento)
           a = parseFloat(a).toFixed(2)
           if (a.substring(a.length - 3, 0).length >= 4) {
               var decimali = a.substring(a.length, a.length - 2);
               var dascrivere = a.substring(a.length - 3, 0);
               var risultato = '';
               while (dascrivere.replace('-', '').length >= 4) {
                   risultato = risultato + '.' + dascrivere.substring(dascrivere.length, dascrivere.length - 3)
                   dascrivere = dascrivere.substring(dascrivere.length - 3, 0)
               }
               risultato = dascrivere + risultato + ',' + decimali
               document.getElementById(obj.id.replace('txtImportoRisc', 'txtTOT')).value  = risultato
           }
           else {
               document.getElementById(obj.id.replace('txtImportoRisc', 'txtTOT')).value  = a.replace('.', ',')
           }
       }
    </script>
    <title>Liberi/Abusivi</title>




    <style type="text/css">
        #form1
        {
            width: 884px;
        }
    </style>




    </head>
<body style="background-attachment: fixed; background-image: url(Immagini/SfondoContratto.png); background-repeat: no-repeat">
    <script type="text/javascript">
        if (navigator.appName == 'Microsoft Internet Explorer') {
            document.onkeydown = $onkeydown;
        }
        else {
            window.document.addEventListener("keydown", TastoInvio, true);
        }
    </script>

    <form id="form1" runat="server" target ="modal" >
    
        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
        <asp:HiddenField ID="txtIdUi" runat="server" Value="0" />

            <asp:Label ID="lblTitolo" runat="server" style="position:absolute; top: 22px; left: 7px;" 
            Text="Unità Libere Condominio: NameCond" ></asp:Label>
        </span></strong>

        <br />
        <br />
    

        <table style="width:98%; position: absolute; top: 47px; left: 17px;">
            <tr>
                <td>
                <br/>

                </td>
                <td class="style1">
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                <div style="overflow: auto; width: 99%; height: 285px;" id="DivMorositaInquilini">
        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                <asp:DataGrid ID="DataGridLiberiAbus" runat="server" AutoGenerateColumns="False"
                    BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" 
                    Style="z-index: 105; left: 193px; top: 54px" Width="98%" TabIndex="10" 
                        BorderColor="#000033" BorderWidth="1px" CellPadding="1" CellSpacing="1">
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <ItemStyle ForeColor="Black" />
                    <HeaderStyle BackColor="WhiteSmoke" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" HorizontalAlign="Center" />
                    <Columns>
                        <asp:BoundColumn DataField="ID_UI" HeaderText="ID_UI" Visible="False"></asp:BoundColumn>
                        <asp:BoundColumn DataField="POSIZIONE_BILANCIO" 
                            HeaderText="POSIZIONE DI BILANCIO">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="STATO" HeaderText="STATO OCCUPAZ."></asp:BoundColumn>
                        <asp:BoundColumn DataField="IMPORTO" HeaderText="IMPORTO" 
                            Visible="False"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="TOTALE">
                            <EditItemTemplate>
                                <asp:TextBox runat="server"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                                <asp:TextBox ID="txtTOT" runat="server" Font-Names="Arial" Font-Size="8pt" 
                                    Style="text-align: right" 
                                    Text='<%# DataBinder.Eval(Container, "DataItem.IMPORTO") %>' 
                                    Width="80px"></asp:TextBox>
                                </span></strong>
                                <asp:Label runat="server"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" />
                        </asp:TemplateColumn>
                    </Columns>
                </asp:DataGrid>
        </span></strong>
    
                </div>
                </td>
                <td style="text-align: left; vertical-align: top" class="style1">
                    <img alt="Aggiungi Unita Immobiliare Libera o Abusiva" src="Immagini/40px-Crystal_Clear_action_edit_add.png" 
                        onclick="OpInquil.toggle();" id="AddInquilini" style="cursor: pointer" /><br />
                    <br />
                        <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="~/Condomini/Immagini/minus_icon.png"
                            Style="z-index: 102; left: 392px; top: 387px; width: 18px;" OnClientClick="DeleteConfirm()"
                ToolTip="Elimina Elemento Selezionato" TabIndex="9" />
                    <br />
                    <br />
                    <img alt="Stampa le informazioni" src="Immagini/print-icon.png" 
                        onclick="ChiamaReport();" id="imgStamap" style="cursor: hand" /></td>
            </tr>
            <tr>
                <td style="text-align: left; vertical-align: top">
            
            <asp:TextBox ID="txtmia" runat="server" BackColor="White" BorderColor="White" BorderStyle="None"
                Font-Bold="True" Font-Names="Arial" Font-Size="8pt" ForeColor="Black" MaxLength="100"
                ReadOnly="True" Style="left: 13px; top: 197px" Width="487px">Nessuna Selezione</asp:TextBox>

            <div id="DivAddUiLibAbus" 
            
            style="top: -44px; left: 7px; width: 844px; height: 494px; position: absolute;">
               <asp:Image ID="Image1" runat="server" BackColor="White" ImageUrl="~/ImmDiv/DivMGrande.png"
                    
                    
                    
                    
                    
                    
                    Style="z-index: 100; left: -2px; position: absolute; top: 8px; height: 465px; width: 847px; margin-right: 0px;" />
                <table style="width: 91%; position: absolute; z-index: 202; height: 76%; top: 54px; left: 50px;">
                    <tr>
                        <td style="text-align: left; vertical-align: top">
                <div style="overflow: auto; width: 100%; height: 301px;" id="DivInquilini">
        <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                <asp:DataGrid ID="DataGridElencoUiLibereAbusive" runat="server" AutoGenerateColumns="False"
                    BackColor="White" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                    Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" 
                    Style="z-index: 105; left: 193px; top: 54px" Width="95%" TabIndex="10" 
                        BorderColor="#000033" BorderWidth="1px" CellPadding="1" CellSpacing="1">
                    <PagerStyle Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                        Font-Overline="False" Font-Strikeout="False" Font-Underline="False" />
                    <ItemStyle ForeColor="Black" />
                    <HeaderStyle BackColor="WhiteSmoke" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                        Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                        ForeColor="#0000C0" HorizontalAlign="Center" />
                    <Columns>
                        <asp:BoundColumn DataField="ID_UI" HeaderText="ID_UI" Visible="False"></asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="SELEZIONA">
                            <ItemTemplate>
                                &nbsp;
                                <asp:CheckBox ID="ChkSeleziona" runat="server" TabIndex="-1" />
                                <asp:Label runat="server"></asp:Label>
                            </ItemTemplate>
                            <HeaderStyle Width="20px" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" />
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="POSIZIONE_BILANCIO" HeaderText="Posizione Bilancio" 
                            ReadOnly="True"></asp:BoundColumn>
                        <asp:BoundColumn DataField="COD_UNITA_IMMOBILIARE" HeaderText="COD. UNIA IMM.">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="STATO" HeaderText="STATO"></asp:BoundColumn>
                    </Columns>
                </asp:DataGrid>
        </span></strong>
    
                </div>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: top">
                            <asp:ImageButton ID="Aggiungi" runat="server" 
                                ImageUrl="~/Condomini/Immagini/Aggiungi.png" TabIndex="11" />
                    <img alt="Esci" src="../NuoveImm/Img_Esci.png" 
                        onclick="OpInquil.toggle();" id="ImgEsci" style="cursor: hand" /></td>
                    </tr>
                </table>
            </div>

                            </td>
                <td style="text-align: left; vertical-align: top" class="style1">
            
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: left; vertical-align: top">
            
                <table  style="width: 98%; vertical-align: bottom; text-align: left; z-index: 200; top: 449px; left: 95px; " 
                    id="pepp" cellpadding =0 cellspacing = 0>
                    <tr>
                        <td style="width: 26%; text-align: center;">
                            <asp:ImageButton ID="btnSommatoria" runat="server" 
                            ImageUrl="~/Condomini/Immagini/Img_Totale.png" TabIndex="11" 
                    ToolTip="Somma delle colonne" /></td>
                        <td style="width: 9%; text-align: center;">
                            &nbsp;</td>
                        <td style="width: 9%; text-align: center;">
                            &nbsp;</td>
                        <td style="width: 9%; text-align: center;">
                            &nbsp;</td>
                        <td style="width: 9%; text-align: center;">
                            &nbsp;</td>
                        <td style="width: 9%; text-align: center;">
                            &nbsp;</td>
                        <td style="width: 9%; text-align: center;">
                            &nbsp;</td>
                        <td style="width: 9%; text-align: center;">
                            &nbsp;</td>
                        <td style="width: 9%; text-align: center;">
                            <asp:TextBox ID="txtSommaTot" runat="server" BackColor="White" Enabled="False" Font-Names="Arial"
                                Font-Size="9pt" MaxLength="50" Style="text-align: right" TabIndex="-1" 
                                Width="80px"></asp:TextBox></td>
                    </tr>
                    </table>

                            </td>
                <td style="text-align: left; vertical-align: top" class="style1">
            
                    &nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: right; vertical-align: top">
            
                    <table style="width:100%;">
                        <tr>
                            <td width="30%">
            
        <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Height="18px" Style="z-index: 104; left: 9px;
            top: 222px" Visible="False" Width="426px"></asp:Label>
            
                            </td>
                            <td width="30%">
            
                                &nbsp;</td>
                            <td width="30%">
            
                <asp:ImageButton ID="btnSalvaCambioAmm" runat="server" 
                            ImageUrl="~/NuoveImm/Img_SalvaVal.png" TabIndex="11" 
                    ToolTip="Salva" style="height: 16px" />
                            </td>
                            <td width="30%">
            
                <asp:ImageButton ID="btnAnnulla" runat="server" 
                            ImageUrl="~/NuoveImm/Img_Esci_AMM.png" TabIndex="11" 
                    ToolTip="Annulla l'operazione" style="height: 16px" OnClientClick="ConfermaUscita();" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="text-align: right; vertical-align: top" class="style1">
            
                    &nbsp;</td>
            </tr>
        </table>
        
        <script type="text/javascript">
            OpInquil = new fx.Opacity('DivAddUiLibAbus', { duration: 200 });
            OpInquil.hide(); ;


            if (document.getElementById('btnDelete').disabled) {
                document.getElementById("AddInquilini").style.visibility = 'hidden';
            } else {
                document.getElementById("AddInquilini").style.visibility = 'visible';
            }

            function DeleteConfirm() {
                if (document.getElementById('txtIdUi').value != 0) {
                    var Conferma
                    Conferma = window.confirm("Attenzione...Confermi di voler eliminare il dato selezionato?");
                    if (Conferma == false) {
                        document.getElementById('txtConfElimina').value = '0';
                    }
                    else {
                        document.getElementById('txtConfElimina').value = '1';

                    }
                }
            }
            
            
    </script>

            <asp:HiddenField ID="txtConfElimina" runat="server" Value="0" />

            <asp:HiddenField ID="txtModificato" runat="server" Value="0" />
            <asp:HiddenField ID="txtesci" runat="server" Value="0" />
            <asp:HiddenField ID="txtSalvato" runat="server" Value="0" />

            
            
    </form>
        </body>

</html>
