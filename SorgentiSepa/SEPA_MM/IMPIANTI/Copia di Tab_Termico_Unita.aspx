<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Tab_Termico_Unita.aspx.vb" Inherits="Tab_Termico_Unita" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">


<script type="text/javascript">
    var Uscita;
    Uscita=0;

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


          


</script>


<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1"/>
<base target="_self"/>
<title>MODULO UNITA IMMOBILIARI</title>

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

<body bgcolor="#f2f5f1" text="#ede0c0">
    <form id="form1" runat="server" target ="modal">
    <div id="DIV1" style="display: block; left: 0px; background-image: url(../../../NuoveImm/SfondoMascheraContratti.jpg); position: absolute; top: 0px; background-color: whitesmoke; z-index: 102;">
            <table>
                <tr>
                    <td style="width: 515px; height: 21px;">
                        &nbsp;
                        <asp:Label ID="lblELENCO_INTERVENTI" runat="server" Font-Bold="True" Font-Names="Arial"
                            Font-Size="8pt" ForeColor="#8080FF" TabIndex="-1" Text="ELENCO UNITA' IMMOBILIARI" Width="300px"></asp:Label></td>
                </tr>
            </table>
            <table id="TABBLE_LISTA">
                <tr>
                    <td>
                        </td>
                    <td>
                        <div style="border-right: #0000cc thin solid; border-top: #0000cc thin solid; visibility: visible;
                            overflow: auto; border-left: #0000cc thin solid; width: 760px; border-bottom: #0000cc thin solid;
                            height: 450px">
                            <asp:DataGrid ID="DataGrid1" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="Black" PageSize="1" Style="table-layout: auto; z-index: 101; left: 8px;
                                clip: rect(auto auto auto auto); direction: ltr; top: 40px; border-collapse: separate"
                                TabIndex="8" Width="992px">
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
                                    <asp:TemplateColumn HeaderText="SCALA">
                                        <EditItemTemplate>
                                            <asp:TextBox runat="server" 
                                                Text='<%# DataBinder.Eval(Container, "DataItem.SCALA") %>'></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            &nbsp;<asp:CheckBox ID="CheckBox1" runat="server" />
                                            <asp:Label runat="server" 
                                                Text='<%# DataBinder.Eval(Container, "DataItem.SCALA") %>'></asp:Label>
                                        </ItemTemplate>
                                        <HeaderStyle Width="10%" />
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn DataField="PIANO" HeaderText="PIANO">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" Width="10%" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="INTERNO" HeaderText="INTERNO">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="MQ" HeaderText="MQ">
                                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" />
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Width="10%" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Width="20%" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                    </asp:BoundColumn>
                                    <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO">
                                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" Width="40%" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
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
                <tr>
                    <td>
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td style="width: 150px;">
                        <asp:ImageButton
                                ID="btnSelTutti" runat="server" ImageUrl="~/IMPIANTI/Immagini/Img_SelezionaTutti.png"
                                Style="z-index: 102; left: 16px; top: 392px" ToolTip="Seleziona tutte le righe della pagina" /></td>
                                <td style="width: 150px;">
                                    <asp:ImageButton ID="btnDeselTutti" runat="server" ImageUrl="~/IMPIANTI/Immagini/Img_DeSelezionaTutti.png"
                            Style="z-index: 102; left: 160px; top: 392px" ToolTip="Deseleziona tutte le righe della pagina" /></td>
                                <td style="width: 100px;">
                                </td>
                                <td style="width: 150px;">
                                    <asp:ImageButton ID="imgProcedi" runat="server" ImageUrl="~/IMPIANTI/Immagini/Img_SalvaContinua_Grande.png"
                            Style="z-index: 102; left: 160px; top: 392px" /></td>
                                <td style="width: 150px;">
                                    <asp:ImageButton ID="imgUscita" runat="server" ImageUrl="~/IMPIANTI/Immagini/Img_Esci_Grande.png"
                            Style="z-index: 102; left: 160px; top: 392px" ToolTip="Deseleziona tutte le righe della pagina" OnClientClick="document.getElementById('USCITA').value='1';" /></td>
                            </tr>
                        </table>
                        </td>
                </tr>
            </table>
        </div>
    <asp:TextBox ID="USCITA"            runat="server" Style="left: 32px; position: absolute;top: 32px; z-index: 100;" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>
    <asp:TextBox ID="txtModificato"     runat="server" Style="left: 8px; position: absolute;top: 32px; z-index: 99;" TabIndex="-1" Width="0px" Height="0px"></asp:TextBox>

    <asp:HiddenField ID="txtTipo"    runat="server" Value="0" />

 </form>
 

<script type="text/javascript">
window.focus();
self.focus();
</script>
 
</body>
        
</html>
