<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ElaboraPosteAler.aspx.vb" Inherits="MOROSITA_ElaboraPosteAler" %>


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
    
    


            

</script>

<html xmlns="http://www.w3.org/1999/xhtml" >


<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1"/>
<title>MODULO GESTIONE MOROSITA ELABORA POSTALER</title>

<script language="javascript" type="text/javascript">
    var Uscita;
Uscita=0;
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
    chiediConferma = window.confirm("Attenzione...E' stato elaborato il file PosteAler. Uscire ugualmente?");
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
    event.returnValue = "Attenzione...Uscire dalla scheda morosità premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
    }
    else
    {
    return "Attenzione...Uscire dalla scheda morosità premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
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

<body bgcolor="#f2f5f1" text="#ede0c0">
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
        <br />
        <br />
        <br />
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
            </tr>
                <td style="width: 800px;" id="TD_Principale">
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"></span>
                    <br />
                    <table style="width: 760px" id="TABLE1" >
                        <tr>
                            <td>
                                <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                                    ForeColor="Blue" Style="z-index: 100; left: 24px; top: 32px" Width="424px">Gestione acquisizione del file Excel di esito</asp:Label></td>
                            <td style="width: 60px">
                            </td>
                            <td>
                                </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td style="width: 60px">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="arial" Font-Size="8pt"
                                    Style="width: 600px" /></td>
                            <td style="width: 60px">
                            </td>
                            <td>
                                <asp:ImageButton ID="btnElabora" runat="server" ImageUrl="~/NuoveImm/Img_Elabora.png"
                                    OnClientClick="document.getElementById('USCITA').value='1';" Style="cursor: pointer"
                                    TabIndex="1" ToolTip="Elabora il file Excel" /></td>
                        </tr>
                    </table>
                    <table style="width: 368px">
                        <tr>
                            <td style="height: 26px">
                    <asp:Label ID="Label30" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" Style="z-index: 106; left: 24px; top: 368px" Width="480px">Elenco Contenuto file Excel</asp:Label></td>
                            <td style="height: 26px">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <div style="overflow: auto; width: 760px; height: 230px; border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;">
                                    <asp:DataGrid ID="DataGridExcel" runat="server" AllowSorting="True" AutoGenerateColumns="False"
                                        BackColor="White" BorderColor="#000099" BorderWidth="1px" Font-Bold="False" Font-Italic="False"
                                        Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                                        Font-Underline="False" ForeColor="Black" Height="1px" PageSize="1" Style="table-layout: auto;
                                        z-index: 101; left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px;
                                        border-collapse: separate" TabIndex="18" Width="1360px">
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
                                            <asp:BoundColumn DataField="DATA" HeaderText="DATA">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" Width="5%" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="QUANTITA" HeaderText="Q.t&#224;">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Width="3%" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="ARTICOLO" HeaderText="ARTICOLO">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" Width="5%" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="VUOTO1" HeaderText="VUOTO1" Visible="False">
                                                <HeaderStyle Width="0%" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="VUOTO2" HeaderText="VUOTO2" Visible="False">
                                                <HeaderStyle Width="0%" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPOLOGIA">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" Width="10%" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="VETTORE" HeaderText="VETTORE">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" Width="5%" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="RIF1" HeaderText="RIF1">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" Width="5%" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="VUOTO3" HeaderText="VUOTO3" Visible="False">
                                                <HeaderStyle Width="0%" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="RIF2" HeaderText="RIF2">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" Width="5%" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="RIF3" HeaderText="RIF3">
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" />
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" Width="5%" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="DESTINATARIO" HeaderText="DESTINATARIO">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" Width="15%" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="LOCALITA" HeaderText="LOCALITA'">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" Width="10%" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="BARCODE" HeaderText="BARCODE">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" Width="5%" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="LOTTO" HeaderText="LOTTO">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" Width="5%" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="COD_ESITO" HeaderText="COD. ESITO">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Right" Width="2%" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" Width="15%" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="DATA_ESITO" HeaderText="DATA ESITO">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Width="3%" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="IA" HeaderText="IA">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Right" Width="2%" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
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
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 760px">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNote" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="100px">Esito Elaborazione:</asp:Label><br />
                                            <br />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="9pt" Height="60px"
                                                MaxLength="500" Style="left: 80px; top: 88px" TabIndex="1" TextMode="MultiLine"
                                                Width="650px" ReadOnly="True"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 20px">
                                            </td>
                                        <td style="height: 20px">
                                            &nbsp; &nbsp; &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <br />
                                            <br />
                                        </td>
                                        <td>
                                            <table border="0" cellpadding="1" cellspacing="1" style="width: 90%">
                                                <tr>
                                                    <td align="right" style="vertical-align: top; text-align: center">
                                                        &nbsp;<asp:ImageButton ID="btn_Salva" runat="server" ImageUrl="~/NuoveImm/Img_SalvaContinua.png"
                                                            OnClientClick="document.getElementById('USCITA').value='1';" Style="cursor: pointer"
                                                            TabIndex="2" ToolTip="Salva le modifiche apportate" />
                                                        &nbsp; &nbsp;<asp:ImageButton ID="btn_Chiudi" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                                                            OnClientClick="document.getElementById('USCITA').value='1';ConfermaEsci();"
                                                            Style="cursor: pointer" TabIndex="3" ToolTip="Esci senza inserire o modificare" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td>
                            </td>
                        </tr>
                    </table>
                    </table>
        <br />
<br />
        <asp:TextBox ID="USCITA"         runat="server" Style="left: 0px; position: absolute; top: 200px; z-index: -1;" TabIndex="-1">0</asp:TextBox>
        <asp:TextBox ID="SOLO_LETTURA"   runat="server" Style="z-index: -1; left: 0px; position: absolute; top: 415px" TabIndex="-1" Width="24px">0</asp:TextBox>        

        <asp:TextBox ID="txtModificato"  runat="server" Style="left: 0px; position: absolute; top: 200px; z-index: -1;" TabIndex="-1"></asp:TextBox>

        <asp:HiddenField ID="txtVisualizza" runat="server" />    
       
        
        <br />
    
    </div>
        
    </form>

<script type="text/javascript">
window.focus();
self.focus();

    if (document.getElementById('txtVisualizza').value == '1') {
        document.getElementById('btn_Salva').style.visibility = 'visible';
    }

    if (document.getElementById('txtVisualizza').value == '0') {
        document.getElementById('btn_Salva').style.visibility = 'hidden';
    }

</script>

</body>

</html>
