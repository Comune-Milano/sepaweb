<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Tab_Manu_Consuntivo.aspx.vb" Inherits="Tab_Manu_Consuntivo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<script type="text/javascript">
    window.name = "modal";

    function TastoInvio(e) {
        sKeyPressed1 = e.which;
        if (sKeyPressed1 == 13) 
        {
            e.preventDefault();
            document.getElementById('USCITA').value = '0';
            document.getElementById('txtModificato').value = '111';
        }
    }

    
    function $onkeydown() {
 
        if (event.keyCode == 13) 
        {
            event.keyCode = 0;
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


 function CalcolaPrezzoTotale(obj,quantita,prezzo) {
        var risultato;
        
        //alert("pippo");
        quantita=quantita.replace('.', '');
        quantita=quantita.replace(',', '.');
        
        prezzo=prezzo.replace('.', '');
        prezzo=prezzo.replace(',', '.');
        
        risultato=quantita*prezzo;
        risultato = risultato.toFixed(2);
        //alert(risultato);
        document.getElementById('txtTotale').value = risultato.replace('.', ',');
        document.getElementById('txtTotale2').value = risultato.replace('.', ',');

        AutoDecimal2(document.getElementById('txtTotale'))
        AutoDecimal2(document.getElementById('txtTotale2'))
   }




</script>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1"/>
<base target="_self"/>
<title>MODULO GESTIONE CONSUNTIVO MANUTENZIONI</title>

<script language="javascript" type="text/javascript">
    var Uscita;
Uscita=0;
</script>

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

    document.getElementById('txttab').value=i+1;
  },
  'addLinkId': true
};

</script>


<script type="text/javascript" src="tabber.js"></script>
<link rel="stylesheet" href="example.css" type="text/css" media="screen"/>
    
<script language="javascript" type="text/javascript">

//window.onbeforeunload = confirmExit; 


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

function confirmExit(){
 if (document.getElementById("USCITA").value=='0') {
 if (navigator.appName == 'Microsoft Internet Explorer') 
    {
    event.returnValue = "Attenzione...Uscire dalla scheda consuntivo premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
    }
    else
    {
    return "Attenzione...Uscire dalla scheda consuntivo premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
    }
    }
}


   
</script>


</head>

<body >
    <form id="form1" runat="server" target ="modal">
    <div id="DIV1" >
            &nbsp;&nbsp;
            <table>
                <tr>
                    <td style="width: 740px">
                        &nbsp;
                        <asp:Label ID="lblELENCO_INTERVENTI" runat="server" Font-Bold="True" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="#8080FF" TabIndex="-1" Text="ELENCO CONSUNTIVI  e/o RIMBORSI" Width="300px"></asp:Label></td>
                    <td style="width: 60px">
                        <asp:ImageButton ID="imgUscita" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
                            Style="z-index: 125; left: 600px; position: static; top: 29px" TabIndex="12"
                            ToolTip="Esce dalla gestione dei consuntivi" OnClientClick="document.getElementById('USCITA').value='1';ConfermaEsci();" /></td>
                </tr>
            </table>
            <table id="TABBLE_LISTA">
                <tr>
                    <td>
                        </td>
                    <td>
                        <div style="border-right: #0000cc thin solid; border-top: #0000cc thin solid; visibility: visible;
                            overflow: auto; border-left: #0000cc thin solid; width: 710px; border-bottom: #0000cc thin solid;
                            height: 420px">
                            <asp:DataGrid ID="DataGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="Black" PageSize="1" Style="table-layout: auto; z-index: 101; left: 8px;
                                clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                                TabIndex="8" Width="710px">
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Position="TopAndBottom" Visible="False" Wrap="False" />
                                <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <Columns>
                                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                        <HeaderStyle Width="0%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="COD_ARTICOLO" HeaderText="Cod. ARTICOLO">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" Width="20%" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" Width="40%" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="UM" HeaderText="U.M.">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Width="5%" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="QUANTITA" HeaderText="Q.t&#224;">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Width="5%" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="PREZZO_UNITARIO" HeaderText="P.zzo UNI">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Width="15%" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="PREZZO_TOTALE" HeaderText="P.zzo TOT.">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Width="15%" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" />
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="Selezione" Visible="False">
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna"></asp:LinkButton><asp:LinkButton
                                                ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel"
                                                Text="Annulla"></asp:LinkButton>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                                                Text="Modifica">Seleziona</asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                    </asp:TemplateColumn>
                                </Columns>
                                <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                    ForeColor="#0000C0" Wrap="False" />
                            </asp:DataGrid></div>
                        <asp:Label ID="lbl_Consuntivi" runat="server" Font-Bold="True" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="#8080FF" TabIndex="-1" Text="TOTALE CONSUNTIVI:" Width="600px"></asp:Label>
                        <asp:Label ID="lbl_Tot_Cons" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="#0000C0" Style="text-align: right" TabIndex="-1" Text="0" Width="100px"></asp:Label></td>
                    <td>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td style="height: 16px">
                        <asp:ImageButton ID="btnAgg1" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Img_Aggiungi.png"
                            TabIndex="9" ToolTip="Aggiunge un nuovo consuntivo" OnClientClick="document.getElementById('txtAppare2').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_C').style.visibility='visible';" Width="60px" /></td>
                            </tr>
                            <tr>
                                <td style="height: 16px">
                                    <asp:ImageButton ID="btnAggRimborso" runat="server" CausesValidation="False" ImageUrl="~/CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/Img_Aggiungi_Rimborsi.png"
                            TabIndex="9" ToolTip="Aggiunge un nuovo rimborso per opera specialistica" OnClientClick="document.getElementById('txtAppare2').value='1';document.getElementById('USCITA').value='1'; document.getElementById('DIV_C').style.visibility='visible';" Width="60px" /></td>
                            </tr>
                            <tr>
                                <td style="height: 16px">
                                </td>
                            </tr>
                            <tr>
                                <td>
                        <asp:ImageButton ID="btnElimina1" runat="server" Height="12px" ImageUrl="~/NuoveImm/Img_Elimina.png"
                            TabIndex="11" ToolTip="Elimina il consuntivo selezionato" Width="60px" OnClientClick="document.getElementById('USCITA').value='1';ConfermaAnnulloConsuntivo();" /></td>
                            </tr>
                            <tr>
                                <td style="height: 14px">
                        <asp:ImageButton ID="btnApri1" runat="server" CausesValidation="False" Height="12px"
                            ImageUrl="~/NuoveImm/Img_Modifica.png" TabIndex="10" ToolTip="Modifica il consuntivo selezionato"
                            Width="60px" OnClientClick="document.getElementById('txtAppare2').value='2';document.getElementById('USCITA').value='1'; document.getElementById('DIV_C').style.visibility='visible';" /></td>
                            </tr>
                        </table>
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
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lbl_Rimborsi" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="#8080FF" TabIndex="-1" Text="TOTALE RIMBORSI:" Width="600px"></asp:Label>
                        <asp:Label ID="lbl_Tot_Rimborsi" runat="server" Font-Bold="True" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="#0000C0" Style="text-align: right" TabIndex="-1" Text="0"
                            Width="100px"></asp:Label></td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:TextBox ID="txtSel1" runat="server" BackColor="#F2F5F1" BorderColor="White"
                            BorderStyle="None" Font-Names="Arial" Font-Size="9pt" Height="15px" MaxLength="100"
                            ReadOnly="True" Style="left: 40px; top: 200px" TabIndex="-1" Width="700px"></asp:TextBox></td>
                    <td>
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
            
            
        </div>
        
    <div id="DIV_C" 
        style="left: 0px;
            width: 800px; position: absolute; top: 0px; height: 544px; background-color: whitesmoke;">
            &nbsp;
            <table id="TABLE1" style="border-right: blue 2px; border-top: blue 2px; z-index: 102;
                left: 30px; border-left: blue 2px; border-bottom: blue 2px; position: absolute;
                top: 30px; background-color: #ffffff">
                <tr>
                    <td>
                        <strong><span style="color: #0000ff; font-family: Arial">
                            <asp:Label ID="lblTitolo1" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                                ForeColor="Blue" Style="z-index: 100; left: 24px; top: 32px" Width="300px">Gestione Consuntivo</asp:Label></span></strong></td>
                </tr>
                <tr>
                    <td>
                        <table id="Table5">
                            <tr>
                                <td>
                                    <asp:Label ID="lblCodArticolo" runat="server" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                        Width="100px">Cod. Articolo *</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtCodArticolo" runat="server" Font-Names="Arial" Font-Size="9pt"
                                        MaxLength="30" TabIndex="1" ToolTip="Codice Articolo" Width="300px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="100px">Descrizione *</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtDescrizione" runat="server" Font-Names="Arial" Font-Size="9pt"
                                        Height="40px" MaxLength="300" Style="left: 80px; top: 88px" TabIndex="2" TextMode="MultiLine"
                                        Width="620px"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblQuantita" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="100px">Quantità *</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtQuantita" runat="server" Font-Names="arial" Font-Size="9pt" MaxLength="10"
                                        Style="z-index: 102; left: 688px; top: 192px; text-align: right" TabIndex="3"
                                        Width="80px"></asp:TextBox>
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDettaglio" runat="server" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                        Width="100px">Unità di Misura *</asp:Label></td>
                                <td>
                                    <asp:DropDownList ID="cmbUnitaMisura" runat="server" BackColor="White" Font-Names="arial"
                                        Font-Size="10pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                        z-index: 111; left: 88px; border-left: black 1px solid; border-bottom: black 1px solid;
                                        top: 56px" TabIndex="5" Width="310px">
                                    </asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblPrezzoUNI" runat="server" Font-Bold="False" Font-Names="Arial"
                                        Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                        Width="100px">Prezzo Unitario *</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtPrezzo" runat="server" MaxLength="14" Style="z-index: 10; left: 408px;
                                        top: 171px; text-align: right" TabIndex="4" Width="120px"></asp:TextBox>
                                    <asp:Label ID="lblEU1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€" Width="16px"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblTotale" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" Width="100px">Prezzo Totale *</asp:Label></td>
                                <td>
                                    <asp:TextBox ID="txtTotale" runat="server" MaxLength="14" ReadOnly="True" Style="z-index: 10;
                                        left: 408px; top: 171px; text-align: right" TabIndex="6" Width="120px"></asp:TextBox>
                                    <asp:Label ID="lblEU2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Black" Style="text-align: right" TabIndex="-1" Text="€" Width="16px"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:TextBox ID="txtID1" runat="server" Height="16px" Style="left: 640px; top: 200px"
                                        TabIndex="-1" Visible="False" Width="32px"></asp:TextBox></td>
                                <td>
                                    <table border="0" cellpadding="1" cellspacing="1" style="width: 90%">
                                        <tr>
                                            <td align="right" style="vertical-align: top; text-align: center"><asp:ImageButton ID="btn_Modifica1" runat="server" ImageUrl="~/CICLO_PASSIVO/CicloPassivo/MANUTENZIONI/Immagini/img_Modifica.png"
                                                    OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('txtAppare2').value='0';"
                                                    Style="cursor: pointer" TabIndex="7" ToolTip="Salva le modifiche apportate" />
                                                <asp:ImageButton ID="btn_Inserisci1" runat="server" ImageUrl="~/NuoveImm/Img_SalvaContinua.png"
                                                    OnClientClick="document.getElementById('USCITA').value='1';"
                                                    Style="cursor: pointer" TabIndex="7" ToolTip="Salva le modifiche apportate e pulisce la maschera per un inserimento nuovo" />
                                                &nbsp; &nbsp;<asp:ImageButton ID="btn_Chiudi1" runat="server" ImageUrl="~/NuoveImm/Img_Esci_AMM.png"
                                                    OnClientClick="document.getElementById('USCITA').value='1';document.getElementById('DIV_C').style.visibility='hidden';document.getElementById('txtAppare2').value='0';"
                                                    Style="cursor: pointer" TabIndex="8" ToolTip="Esci senza inserire o modificare" /></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                        <table style="width: 456px">
                            <tr>
                                <td>
                                    <asp:Label ID="lblTitolo2" runat="server" Font-Bold="True" Font-Italic="True" Font-Names="Arial"
                                        Font-Size="11pt" ForeColor="Blue" Style="z-index: 100; left: 24px; top: 32px"
                                        Width="300px">Elenco Consuntivi Inseriti</asp:Label></td>
                            </tr>
                            <tr>
                                <td>
                                    <div style="border-right: #0000cc thin solid; border-top: #0000cc thin solid; visibility: visible;
                            overflow: auto; border-left: #0000cc thin solid; width: 720px; border-bottom: #0000cc thin solid;
                            height: 200px" id="T_DataGrid2">
                            <asp:DataGrid ID="DataGrid2" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="Black" PageSize="1" Style="table-layout: auto; z-index: 101; left: 8px;
                                clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                                TabIndex="8" Width="730px">
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Position="TopAndBottom" Visible="False" Wrap="False" />
                                <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                    Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <Columns>
                                    <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                        <HeaderStyle Width="0%" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="COD_ARTICOLO" HeaderText="Cod. ARTICOLO">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" Width="20%" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" Width="40%" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="UM" HeaderText="U.M.">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Width="5%" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="QUANTITA" HeaderText="Q.t&#224;">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Width="5%" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="PREZZO_UNITARIO" HeaderText="P.zzo UNI">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Width="15%" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="PREZZO_TOTALE" HeaderText="P.zzo TOT.">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Width="15%" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" />
                                    </asp:BoundColumn>
                                    <asp:TemplateColumn HeaderText="Selezione" Visible="False">
                                        <EditItemTemplate>
                                            <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" Text="Aggiorna"></asp:LinkButton><asp:LinkButton
                                                ID="LinkButton2" runat="server" CausesValidation="false" CommandName="Cancel"
                                                Text="Annulla"></asp:LinkButton>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" CommandName="Edit"
                                                Text="Modifica">Seleziona</asp:LinkButton>
                                        </ItemTemplate>
                                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                    </asp:TemplateColumn>
                                </Columns>
                                <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                    ForeColor="#0000C0" Wrap="False" />
                            </asp:DataGrid></div>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            <asp:Image ID="Image1" runat="server" BackColor="White" Height="580px" ImageUrl="../../../ImmDiv/DivMGrande.png"
                Style="z-index: 101; left: 0px; position: absolute; top: 0px;" Width="800px" />
        <asp:HiddenField ID="txtTotale2" runat="server" />
                                    <asp:HiddenField ID="txtImportoODL" runat="server" />
        </div>
       
    <asp:TextBox ID="USCITA"            runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
    <asp:TextBox ID="txtIdComponente"   runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
    <asp:TextBox ID="txtannullo"        runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
    <asp:TextBox ID="txtModificato"     runat="server" Style="left: 800px; position: absolute;top: 320px" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
    <asp:HiddenField ID="txtResiduoConsumo" runat="server" />

    <asp:HiddenField ID="txtAppare2" runat="server" Value="0" />

    <asp:HiddenField ID="txtPercIVA"        runat="server" Value="0" />
    <asp:HiddenField ID="txtPercOneri"      runat="server" Value="0" />
    <asp:HiddenField ID="txtScontoConsumo"  runat="server" Value="0" />
    <asp:HiddenField ID="HiddenField4"      runat="server" Value="0" />
    <asp:HiddenField ID="txtFL_RIT_LEGGE"   runat="server" Value="0" />

 </form>
 
 <script type="text/javascript">

if (document.getElementById('txtAppare2').value == 0 ) {
    document.getElementById('DIV_C').style.visibility = 'hidden';
    document.getElementById('T_DataGrid2').style.visibility = 'hidden';

}

if (document.getElementById('txtAppare2').value == 1 ) {
    document.getElementById('DIV_C').style.visibility = 'visible';
    document.getElementById('T_DataGrid2').style.visibility = 'visible';
}

if (document.getElementById('txtAppare2').value == 2 ) {
    document.getElementById('DIV_C').style.visibility = 'visible';
    document.getElementById('T_DataGrid2').style.visibility = 'hidden';
}

</script>

<script type="text/javascript">
window.focus();
self.focus();
</script>
 
</body>
        
</html>
