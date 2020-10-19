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


    <style type="text/css">
        #form1
        {
            width: 793px;
            height: 489px;
        }
        .style1
        {
            width: 787px;
        }
    </style>


</head>

<body  style="background-attachment: fixed; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat:no-repeat;">
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
    <form id="form1" runat="server" 
    style="width: 792px; vertical-align: top; text-align: left;">
    <table style="width: 100%;">
        <tr>
            <td>
                                <asp:Label ID="Label17" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                                    ForeColor="Blue" Style="z-index: 100; left: 24px; top: 32px" Width="424px">Gestione acquisizione del file Excel di esito</asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                                &nbsp;</td>
        </tr>
        <tr>
            <td class="style1">
                    <table style="width: 98%" id="TABLE1" >
                        <tr>
                            <td>
                                <asp:FileUpload ID="FileUpload1" runat="server" Font-Names="arial" Font-Size="8pt"
                                    Style="width: 600px" Width="99%" /></td>
                            <td>
                                <asp:Image ID="Image1" runat="server" 
                                    ImageUrl="~/Contabilita/IMMCONTABILITA/info-icon.png" 
                                    ToolTip="Ordine dei campi per il file excel: DESTINATARIO colonna L; LOCALITA colonna M; COD.ESITO colonna P; DESCRIZIONE colonna Q; DATA ESITO colonna R." />
                            </td>
                            <td>
                                <asp:ImageButton ID="btnElabora" runat="server" ImageUrl="Immagini/Img_Elabora.png"
                                    OnClientClick="document.getElementById('USCITA').value='1';" Style="cursor: pointer"
                                    TabIndex="1" ToolTip="Elabora il file Excel" /></td>
                        </tr>
                    </table>
            </td>
        </tr>
        <tr>
            <td >
                <table style="width: 100%">
                    <tr>
                        <td style="height: 26px">
                            <asp:Label ID="Label30" runat="server" Font-Bold="True" Font-Names="Arial" 
                                Font-Size="8pt" ForeColor="Black" Style="z-index: 106; left: 24px; top: 368px" 
                                Width="480px">Elenco Contenuto file Excel</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div style="overflow: auto; width: 98%; height: 230px;">
                                <asp:DataGrid ID="DataGridExcel" runat="server" AllowSorting="True" 
                                    AutoGenerateColumns="False" BackColor="White" BorderColor="#000099" 
                                    BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Names="Arial" 
                                    Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                                    Font-Underline="False" ForeColor="Black" Height="1px" PageSize="1" Style="table-layout: auto;
                                        z-index: 101; left: 8px; clip: rect(auto auto auto auto); direction: ltr; top: 40px;
                                        border-collapse: separate" TabIndex="18" Width="98%" Visible="False">
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Position="TopAndBottom" 
                                        Visible="False" Wrap="False" />
                                    <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" 
                                        Font-Italic="False" Font-Overline="False" Font-Strikeout="False" 
                                        Font-Underline="False" Wrap="False" />
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                        Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                    <Columns>
                                        <asp:BoundColumn DataField="DESTINATARIO" HeaderText="DESTINATARIO">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                                                Width="15%" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="LOCALITA" HeaderText="LOCALITA'">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                                                Width="10%" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="COD_ESITO" HeaderText="COD. ESITO" Visible="False">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                                Width="2%" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Right" 
                                                Wrap="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" 
                                                Width="15%" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="DATA_ESITO" HeaderText="DATA ESITO">
                                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" 
                                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                                Width="3%" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" 
                                                Wrap="False" />
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="IA" HeaderText="IA" Visible="False">
                                        </asp:BoundColumn>
                                        <asp:BoundColumn DataField="COD_ESITO" HeaderText="COD_ESITO" Visible="False">
                                        </asp:BoundColumn>
                                        <asp:TemplateColumn HeaderText="Selezione" Visible="False">
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="LinkButton3" runat="server" CommandName="Update" 
                                                    Text="Aggiorna"></asp:LinkButton>
                                                <asp:LinkButton ID="LinkButton2" runat="server" CausesValidation="false" 
                                                    CommandName="Cancel" Text="Annulla"></asp:LinkButton>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="false" 
                                                    CommandName="Edit" Text="Modifica">Seleziona</asp:LinkButton>
                                            </ItemTemplate>
                                            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                                                Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" 
                                        Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" 
                                        Font-Underline="False" ForeColor="#0000C0" Wrap="False" />
                                </asp:DataGrid>
                            </div>
                        </td>
                    </tr>
                </table>
            </td>

        </tr>
        <tr>
            <td >
                <table style="width:100%;">
                    <tr>
                        <td>
                            <asp:Label ID="lblNote" runat="server" Font-Bold="False" Font-Names="Arial" 
                                Font-Size="8pt" ForeColor="Black" Visible="False">Esito Elaborazione:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="9pt" 
                                MaxLength="500" ReadOnly="True" Style="left: 80px; top: 88px" 
                                TabIndex="1" TextMode="MultiLine" Width="645px" Height="61px" 
                                Visible="False"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>

        </tr>
        <tr>
            <td >
                &nbsp;</td>

        </tr>
        <tr>
            <td align="right" >
                <table border="0" cellpadding="1" cellspacing="1">
                    <tr>
                        <td align="right" style="vertical-align: top; text-align:right">
                            <asp:ImageButton ID="btn_Salva" runat="server" 
                                ImageUrl="~/NuoveImm/Img_SalvaVal.png" 
                                OnClientClick="document.getElementById('USCITA').value='1';" 
                                Style="cursor: pointer" TabIndex="2" 
                                ToolTip="Salva le modifiche apportate" />
                        </td>
                        <td align="right" style="vertical-align: top; text-align:right">
                            <asp:ImageButton ID="btn_Chiudi" runat="server" 
                                ImageUrl="~/NuoveImm/Img_AnnullaVal.png" 
                                OnClientClick="document.getElementById('USCITA').value='1';ConfermaEsci();" 
                                Style="cursor: pointer; text-align: right;" TabIndex="3" 
                                ToolTip="Esci senza inserire o modificare" />
                        </td>
                    </tr>
                </table>
            </td>

        </tr>
    </table>
            <asp:HiddenField ID="USCITA" runat="server" Value="0" />    
                        <asp:HiddenField ID="SOLO_LETTURA" runat="server" Value="0" />    
                        <asp:HiddenField ID="txtModificato" runat="server" Value="0" />    

        <asp:HiddenField ID="txtVisualizza" runat="server" />    
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
