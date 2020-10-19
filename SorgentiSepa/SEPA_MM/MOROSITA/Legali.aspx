<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Legali.aspx.vb" Inherits="MOROSITA_Legali" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">



<script type="text/javascript">



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



</script>

<html xmlns="http://www.w3.org/1999/xhtml" >


<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1"/>
<title>MODULO GESTIONE MOROSITA LEGALI</title>

<script language="javascript" type="text/javascript">
    var Uscita;
Uscita=0;
</script>

<script type="text/javascript" src="tabber.js"></script>
<link rel="stylesheet" href="example.css" type="text/css" media="screen"/>

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


    
<script language="javascript" type="text/javascript">

//window.onbeforeunload = confirmExit; 

function EliminaLegale() {
    var sicuro = confirm('Sei sicuro di voler eliminare questo Legale? Tutti i dati andranno persi.');
    if (sicuro == true) {
    document.getElementById('txtElimina').value='1';
    }
    else
    {
    document.getElementById('txtElimina').value='0'; 
    }
}



function ConfermaEsci()
{
    if (document.getElementById('txtModificato').value=='1') 
    {
        var chiediConferma
        if (document.getElementById('txtVisualizza').value<2)
        {
            chiediConferma = window.confirm("Attenzione...Sono state apportate delle modifiche. Uscire ugualmente?");
            if (chiediConferma == false) 
            {
                document.getElementById('txtModificato').value='111';
                //document.getElementById('USCITA').value='0';
            }
        }
    } 
} 



function confirmExit(){
 if (document.getElementById("USCITA").value=='0') {
 if (navigator.appName == 'Microsoft Internet Explorer') 
    {
    event.returnValue = "Attenzione...Uscire dalla scheda del legale premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
    }
    else
    {
    return "Attenzione...Uscire dalla scheda del legale premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
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


</head>

<body style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat:no-repeat;">
<script type="text/javascript">
	    if (navigator.appName == 'Microsoft Internet Explorer') 
	    {
	        document.onkeydown = $onkeydown;
	    }
	    else 
	    {
	        window.document.addEventListener("keydown", TastoInvio , true);
	    }
</script>
    <form id="form1" runat="server">
    <div>
        <table style="left: 0px; top: 0px">
                <td style="width: 800px; height: 1px;" id="TD_Principale">
                    <table style="width: 760px">
                        <tr>
                            <td style="width: 76px">
                                &nbsp;<asp:ImageButton ID="btnINDIETRO" runat="server" ImageUrl="~/NuoveImm/Img_Indietro.png"
                                    OnClientClick="document.getElementById('USCITA').value='1';ConfermaEsci();" Style="z-index: 100;
                                    left: 16px; position: static; top: 29px" TabIndex="-1" ToolTip="Indietro" /></td>
                            <td style="width: 76px">
                                <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
                                    OnClientClick="document.getElementById('USCITA').value='1'" Style="z-index: 100;
                                    left: 584px; position: static; top: 32px" TabIndex="-1" ToolTip="Salva" /></td>
                            <td style="width: 76px;">
                                <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="~/NuoveImm/Img_Elimina.png"
                                    OnClientClick="document.getElementById('USCITA').value='1';  EliminaLegale();"
                                    Style="z-index: 100; left: 584px; position: static; top: 32px" TabIndex="-1"
                                    ToolTip="Elimina il Legale visualizzato" /></td>
                            <td style="width: 76px">
                                </td>
                            <td style="width: 76px">
                                </td>
                            <td style="width: 76px"></td>
                            <td style="width: 76px">
                                <img id="ImgEventi" alt="Eventi Scheda Legale" border="0" onclick="window.open('Report/Eventi.aspx?ID=<%=vIdLegale%>','Report', '');"
                                    src="../NuoveImm/Img_Eventi.png" style="left: 616px; cursor: pointer; top: 32px" /></td>
                            <td style="width: 76px">
                                </td>
                            <td style="width: 76px">
                                </td>
                            <td style="width: 76px">
                            </td>
                            <td style="width: 76px">
                                <asp:ImageButton ID="imgUscita" runat="server" ImageUrl="~/NuoveImm/Img_Esci.png"
                                    OnClientClick="document.getElementById('USCITA').value='1';ConfermaEsci();" Style="z-index: 125;
                                    left: 600px; position: static; top: 29px" TabIndex="-1" ToolTip="Esci" /></td>
                        </tr>
                    </table>
                    &nbsp;<asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="#0000C0" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1"
                        Width="760px">DATI ANAGRAFICI.....................................................................................................................................................................................................................</asp:Label>
            <table>
                <tr>
                    <td>
                        <asp:Label ID="lblCognome" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                            TabIndex="-1" Width="80px">Cognome *</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtCognome" runat="server" MaxLength="50" Style="z-index: 10; left: 408px;
                            top: 171px; text-transform: uppercase;" Width="290px" Font-Bold="True" Font-Size="9pt" TabIndex="1"></asp:TextBox></td>
                    <td style="width: 20px">
                    </td>
                    <td>
                        <asp:Label ID="lblNome" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" TabIndex="-1" Width="60px">Nome *</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtNome" runat="server" MaxLength="50" Style="z-index: 10;
                            left: 408px; top: 171px; text-transform: uppercase;" TabIndex="2" Width="290px" Font-Bold="True" Font-Size="9pt"></asp:TextBox></td>
                </tr>
            </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Label ID="lbTipoIndirizzo" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="80px">Tipo Indirizzo *</asp:Label></td>
                            <td><asp:DropDownList ID="cmdTipoIndirizzo" runat="server" BackColor="White" Font-Names="arial"
                                    Font-Size="9pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                    z-index: 111; left: 88px; border-left: black 1px solid; border-bottom: black 1px solid;
                                    top: 56px" TabIndex="3" Width="140px">
                            </asp:DropDownList></td>
                            <td style="width: 20px">
                            </td>
                            <td>
                                <asp:Label ID="lblIndirizzo" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Indirizzo</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtIndirizzo" runat="server" Font-Bold="False" Font-Size="9pt"
                                    MaxLength="50" Style="z-index: 10; left: 408px; top: 171px; text-transform: uppercase;" TabIndex="4" Width="290px"></asp:TextBox></td>
                            <td style="width: 20px">
                                </td>
                            <td>
                                <asp:Label ID="lblCivico" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Civico</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtCivico" runat="server" Font-Bold="False" Font-Size="9pt"
                                    MaxLength="15" Style="z-index: 10; left: 408px; top: 171px; text-transform: uppercase;" TabIndex="5" Width="50px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCAP" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="80px">CAP</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtCAP" runat="server" Font-Bold="False" Font-Size="9pt" MaxLength="5"
                                    Style="z-index: 10; left: 408px; text-transform: uppercase; top: 171px" TabIndex="6"
                                    Width="50px"></asp:TextBox></td>
                            <td style="width: 20px">
                            </td>
                            <td>
                                <asp:Label ID="lblComune" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="60px">Comune</asp:Label></td>
                            <td>
                                <asp:DropDownList ID="cmbComune" runat="server" BackColor="White" Font-Names="arial"
                                    Font-Size="9pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                    z-index: 111; left: 88px; border-left: black 1px solid; border-bottom: black 1px solid;
                                    top: 56px" TabIndex="7" Width="290px" AutoPostBack="True">
                                </asp:DropDownList></td>
                            <td style="width: 20px">
                            </td>
                            <td>
                                <asp:Label ID="lblProvincia" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                    TabIndex="-1" Width="60px">Provincia</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtProvincia" runat="server" Font-Bold="False" Font-Size="9pt" MaxLength="15"
                                    ReadOnly="True" Style="z-index: 10; left: 408px; text-transform: uppercase; top: 171px"
                                    TabIndex="-1" Width="50px"></asp:TextBox></td>
                        </tr>
                    </table><table>
                        <tr>
                            <td>
                                <asp:Label ID="lblTel1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" TabIndex="-1" Width="80px">Telefono 1</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtTelefono1" runat="server" Font-Bold="False" Font-Size="9pt" MaxLength="50"
                                    Style="z-index: 10; left: 408px; text-transform: uppercase; top: 171px" Width="290px" TabIndex="8"></asp:TextBox></td>
                            <td style="width: 20px">
                            </td>
                            <td>
                                <asp:Label ID="lblTelefono2" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                    TabIndex="-1" Width="60px">Telefono 2</asp:Label></td>
                            <td>
                                <asp:TextBox ID="txtTelefono2" runat="server" Font-Bold="False" Font-Size="9pt" MaxLength="50"
                                    Style="z-index: 10; left: 408px; text-transform: uppercase; top: 171px" Width="290px" TabIndex="9"></asp:TextBox></td>
                        </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCellulare" runat="server" Font-Bold="False" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                            TabIndex="-1" Width="80px">Cellulare</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtCell" runat="server" Font-Bold="False" Font-Size="9pt" MaxLength="50"
                            Style="z-index: 10; left: 408px; text-transform: uppercase; top: 171px" Width="290px" TabIndex="10"></asp:TextBox></td>
                    <td style="width: 20px">
                    </td>
                    <td>
                        <asp:Label ID="lblFax" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" TabIndex="-1" Width="60px">Fax</asp:Label></td>
                    <td>
                        <asp:TextBox ID="txtFax" runat="server" Font-Bold="False" Font-Size="9pt" MaxLength="50"
                            Style="z-index: 10; left: 408px; text-transform: uppercase; top: 171px" Width="290px" TabIndex="11"></asp:TextBox></td>
                </tr>
                        <tr>
                            <td>
                                </td>
                            <td>
                                <asp:TextBox ID="txtMail" runat="server" Font-Bold="False" Font-Size="9pt" MaxLength="50"
                                    Style="z-index: 10; left: 408px; text-transform: uppercase; top: 171px" Width="290px" TabIndex="12"></asp:TextBox></td>
                            <td style="width: 20px">
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="height: 30px">
                                <asp:Label ID="lblTribunale" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" TabIndex="-1" Width="80px">Tribunale di Competenza *</asp:Label></td>
                            <td style="height: 30px">
                                <asp:DropDownList ID="cmbTribunali" runat="server" BackColor="White" Font-Names="arial"
                                    Font-Size="9pt" Style="border-right: black 1px solid; border-top: black 1px solid;
                                    z-index: 111; left: 88px; border-left: black 1px solid; border-bottom: black 1px solid;
                                    top: 56px" TabIndex="13" Width="500px">
                                </asp:DropDownList></td>
                            <td style="height: 30px">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblNote" runat="server" Font-Bold="False" Font-Names="Arial"
                                    Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px"
                                    TabIndex="-1" Width="80px">Note</asp:Label><br />
                            </td>
                            <td>
                                <asp:TextBox ID="txtNote" runat="server"
                            Font-Names="Arial" Font-Size="9pt" Height="50px" MaxLength="500" Style="left: 80px;
                            top: 88px" TabIndex="14" TextMode="MultiLine" Width="500px"></asp:TextBox></td>
                            <td>
                                <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="90px">Pratiche in Corso:</asp:Label>
                                <asp:TextBox ID="txtInCorso" runat="server" Font-Bold="True" Font-Size="9pt" MaxLength="15"
                                    ReadOnly="True" Style="z-index: 10; left: 408px; text-transform: uppercase; top: 171px"
                                    TabIndex="-1" Width="50px"></asp:TextBox>&nbsp;<br />
                                <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="90px">Pratiche Chiuse:</asp:Label>
                                <asp:TextBox ID="txtChiuse" runat="server" Font-Bold="True" Font-Size="9pt" MaxLength="15"
                                    ReadOnly="True" Style="z-index: 10; left: 408px; text-transform: uppercase; top: 171px"
                                    TabIndex="-1" Width="50px"></asp:TextBox></td>
                        </tr>
                    </table>
        <asp:Label style="z-index: 100; left: 8px; top: 88px;" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" ForeColor="#0000C0" ID="Label1" runat="server" TabIndex="-1" Width="760px">ELENCO PRATICHE ........................................................................................................................................................................................................................</asp:Label><br />
                    <table style="width: 760px">
                        <tr>
                            <td style="height: 43px">
                                <div style="border-right: black 1px solid; border-top: black 1px solid; overflow: auto;
                                    border-left: black 1px solid; width: 760px; border-bottom: black 1px solid; height: 170px">
                                    <asp:DataGrid ID="DataGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        BackColor="White" BorderColor="#000099" BorderWidth="1px" Font-Bold="False" Font-Italic="False"
                                        Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                                        Font-Underline="False" ForeColor="Black" PageSize="1" Style="table-layout: auto;
                                        z-index: 101; left: 0px; width: 100%; clip: rect(auto auto auto auto); direction: ltr;
                                        top: 8px; border-collapse: separate" Width="1px">
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
                                            <asp:BoundColumn DataField="ID_ANAGRAFICA" HeaderText="ID_ANAGRAFICA" Visible="False">
                                                <HeaderStyle Width="0%" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ID_MOROSITA" HeaderText="ID_MOROSITA" Visible="False">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" Width="10%" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ID_CONTRATTO" HeaderText="ID_CONTRATTO" Visible="False">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Width="5%" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ID_MOROSITA_LETTERA1" HeaderText="ID_MOROSITA_LETTERA1"
                                                Visible="False">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Width="5%" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Right" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ID_MOROSITA_LETTERA2" HeaderText="ID_MOROSITA_LETTERA2"
                                                Visible="False">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ID_TIPO_AZIONE_LEGALE" HeaderText="ID_TIPO_AZIONE_LEGALE"
                                                Visible="False">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="CODICE">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="COD_TIPOLOGIA_CONTR_LOC" HeaderText="TIPO"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="CIVICO" HeaderText="CIV."></asp:BoundColumn>
                                            <asp:BoundColumn DataField="COMUNE_UNITA" HeaderText="COMUNE"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="PROTOCOLLO" HeaderText="PROTOCOLLO"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DATA_APERTURA" HeaderText="DATA APERTURA"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="DATA_CHIUSURA" HeaderText="DATA CHIUSURA"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="EVENTO" HeaderText="AZIONE INTRAPRESA"></asp:BoundColumn>
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
        </table>
        <br />
     
        <asp:HiddenField id="USCITA"            runat="server"></asp:HiddenField>
        <asp:HiddenField id="txtModificato"     runat="server"></asp:HiddenField>
        <asp:HiddenField id="txtElimina"        runat="server"></asp:HiddenField>
        <asp:HiddenField id="txtindietro"       runat="server"></asp:HiddenField>
        <asp:HiddenField id="txtConnessione"    runat="server"></asp:HiddenField>

        <asp:HiddenField id="txtVisualizza"     runat="server"></asp:HiddenField>
        
        <asp:HiddenField ID="x"                 runat="server" Value="0"></asp:HiddenField>
        
        <asp:HiddenField id="txtStatoPagamento" runat="server"></asp:HiddenField>
        <asp:HiddenField id="txtSTATO"          runat="server"></asp:HiddenField>
        
        
        <asp:HiddenField id="txtID_STRUTTURA"   runat="server"></asp:HiddenField>
        
    </div>
        
    </form>

<script type="text/javascript">
window.focus();
self.focus();


</script>

</body>

</html>
