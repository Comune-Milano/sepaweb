<%@ Page Language="VB" AutoEventWireup="false" CodeFile="MorositaLegale.aspx.vb" Inherits="MOROSITA_MorositaLegale" %>


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
<title>CREA LETTERA MOROSITA</title>

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
 //alert('ciao');
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
    event.returnValue = "Attenzione...Uscire dalla scheda premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
    }
    else
    {
    return "Attenzione...Uscire dalla scheda premendo il pulsante ESCI. In caso contrario non sara più possibile accedere alla scheda per un determinato periodo di tempo!";
    }
    }
}


function SalvaMorosita() {
    var sicuro = confirm('Attenzione...Confermi di voler affidare le pratiche al legale visualizzato?');
    if (sicuro == true) {
    document.getElementById('txtAnnullo').value='1';
    }
    else
    {
    document.getElementById('txtAnnullo').value='0'; 
    }
}


function StampaMorosita() {
    var sicuro = confirm('Attenzione...Confermi di voler Ristampare i MAV?');
    if (sicuro == true) {
    document.getElementById('txtAnnullo').value='1';
    }
    else
    {
    document.getElementById('txtAnnullo').value='0'; 
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
                <td style="width: 800px; height: 1px;" id="TD_Principale">
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"></span>
                    <br />
                    <table style="width: 760px" id="TABLE1" >
                        <tr>
                            <td style="width: 76px">
                                &nbsp;<asp:ImageButton ID="btnINDIETRO" runat="server" ImageUrl="~/NuoveImm/Img_Indietro.png"
                                    OnClientClick="document.getElementById('USCITA').value='1';ConfermaEsci();" Style="z-index: 100;
                                    left: 16px; position: static; top: 29px" TabIndex="-1" ToolTip="Indietro" /></td>
                            <td style="width: 76px">
                                <asp:ImageButton ID="btnSalva" runat="server" ImageUrl="~/NuoveImm/Img_Salva.png"
                                    OnClientClick="document.getElementById('USCITA').value='1'; SalvaMorosita();" Style="z-index: 100;
                                    left: 584px; position: static; top: 32px" TabIndex="-1" ToolTip="Salva" /></td>
                            <td style="width: 76px">
                                </td>
                            <td style="width: 76px"><asp:ImageButton ID="imgExcel" runat="server" ImageUrl="~/MOROSITA/Immagini/Img_ElencoCF.png"
                                    OnClientClick="document.getElementById('USCITA').value='1'; "
                                    Style="z-index: 100; left: 584px; position: static; top: 32px" TabIndex="-1"
                                    ToolTip="Visualizza un file Excel con l'elenco degli inquilini aventi C.F. errato." Visible="False" /></td>
                            <td style="width: 76px">
                            </td>
                            <td style="width: 76px">
                            </td>
                            <td style="width: 76px">
                            </td>
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
                    <asp:Label ID="lblElencoInquilini" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Black" Style="z-index: 106; left: 24px; top: 368px" Width="200px">RISULTATO RICERCA INTESTATARI : </asp:Label>&nbsp;
                    <asp:Label ID="lblNumIntestatari" runat="server" Font-Bold="True" Font-Names="Arial"
                        Font-Size="10pt" ForeColor="Red" Style="z-index: 106; left: 24px; top: 368px"
                        Width="136px">0</asp:Label><br />
                    <table style="width: 368px">
                        <tr>
                            <td>
                                <div style="overflow: auto; width: 760px; height: 280px; border-right: black 1px solid; border-top: black 1px solid; border-left: black 1px solid; border-bottom: black 1px solid;">
                                    <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                                        BackColor="White" BorderColor="#000099" BorderWidth="1px" Font-Bold="False" Font-Italic="False"
                                        Font-Names="Arial" Font-Overline="False" Font-Size="8pt" Font-Strikeout="False"
                                        Font-Underline="False" ForeColor="Black" PageSize="300" Style="z-index: 101;
                                        left: 9px; border-collapse:separate" Width="1087px">
                                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                        <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                        <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                        <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Mode="NumericPages" Position="TopAndBottom" Wrap="False" />
                                        <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                            Font-Underline="False" Wrap="False" />
                                        <Columns>
                                            <asp:BoundColumn DataField="ID_MOROSITA_LETTERE" HeaderText="ID" Visible="False">
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="CODICE" Visible="False">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_CONTRATTO") %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="CODICE">
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_CONTRATTO") %>'></asp:Label>&nbsp;
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="INTESTATARIO">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INTESTATARIO") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="DEBITO2" HeaderText="DEBITO">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:BoundColumn DataField="TIPO_MAV" HeaderText="Tipo MAV">
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            </asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="TIPO">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_TIPOLOGIA_CONTR_LOC") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="POSIZIONE">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.POSIZIONE_CONTRATTO") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="COD.UNITA'">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_UNITA_IMMOBILIARE") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="TIPO UN.">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_TIPOLOGIA") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="INDIRIZZO">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INDIRIZZO") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="CIV.">
                                                <EditItemTemplate>
                                                    <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CIVICO") %>'></asp:TextBox>
                                                </EditItemTemplate>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CIVICO") %>'></asp:Label>
                                                </ItemTemplate>
                                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" Wrap="False" />
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" Wrap="False" />
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="COMUNE">
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COMUNE_UNITA") %>'></asp:Label>
                                                </ItemTemplate>
                                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" />
                                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                                    Font-Underline="False" HorizontalAlign="Left" />
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="ID_MOROSITA" HeaderText="ID_MOROSITA" Visible="False"></asp:BoundColumn>
                                        </Columns>
                                        <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                            ForeColor="#0000C0" Wrap="False" />
                                    </asp:DataGrid></div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table style="width: 760px">
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblLegale" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="100px">Legale</asp:Label></td>
                                        <td>
                                            <asp:TextBox ID="txtLegale" runat="server" MaxLength="50" Style="z-index: 10; left: 408px;
                                                top: 171px" TabIndex="1" Width="650px"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td style="height: 26px">
                                            <asp:Label ID="lblProtocollo" runat="server" Font-Bold="False" Font-Names="Arial"
                                                Font-Size="8pt" ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px"
                                                TabIndex="-1" Width="100px">Protocollo: *</asp:Label></td>
                                        <td style="height: 26px">
                                            <asp:TextBox ID="txtProtocollo" runat="server" MaxLength="50" Style="z-index: 10;
                                                left: 408px; top: 171px" Width="200px" TabIndex="1"></asp:TextBox>
                                            &nbsp; &nbsp;
                                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="Black" Style="z-index: 100; left: 24px; top: 32px" TabIndex="-1" Width="130px">Data Affidamento: *</asp:Label>
                                            <asp:TextBox ID="txtDataProtocollo" runat="server" Font-Names="Arial" Font-Size="9pt"
                                                MaxLength="10" Style="left: 144px; top: 192px" TabIndex="3" ToolTip="gg/mm/aaaa"
                                                Width="70px"></asp:TextBox><asp:RegularExpressionValidator ID="RegularExpressionValidator1"
                                                    runat="server" ControlToValidate="txtDataProtocollo" Display="Dynamic" ErrorMessage="Inserire la data (gg/mm/aaaa)"
                                                    Font-Bold="False" Font-Names="arial" Font-Size="8pt" TabIndex="-1" ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"
                                                    Width="192px"></asp:RegularExpressionValidator></td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblNote" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                                ForeColor="Black" Style="z-index: 100; left: 8px; top: 88px" TabIndex="-1" Width="100px">Note:</asp:Label><br />
                                            <br />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNote" runat="server" Font-Names="Arial" Font-Size="9pt" Height="80px"
                                                MaxLength="500" Style="left: 80px; top: 88px" TabIndex="2" TextMode="MultiLine"
                                                Width="650px"></asp:TextBox></td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                        </tr>
                    </table>
                    <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                        ForeColor="Red" Style="z-index: 104; left: 17px; width: 858px; top: 588px; height: 30px"
                        Visible="False" TabIndex="-1"></asp:Label></table>
        <br />
<br />
        <asp:TextBox ID="USCITA"         runat="server" Style="left: 0px; position: absolute; top: 200px; z-index: -1;" TabIndex="-1">0</asp:TextBox>
        <asp:TextBox ID="txtModificato"  runat="server" BackColor="White" BorderStyle="None" ForeColor="White" Style="left: 0px; position: absolute; top: 200px; z-index: -1;">0</asp:TextBox>
        
        <asp:TextBox ID="txtindietro"    runat="server" BackColor="#F2F5F1" BorderColor="White" BorderStyle="None" MaxLength="100" Style="z-index: -1; left: 0px; position: absolute; top: 200px" TabIndex="-1" Width="48px">0</asp:TextBox>
        <asp:TextBox ID="txtConnessione" runat="server" Style="left: 0px; position: absolute; top: 200px; z-index: -1;" TabIndex="-1"></asp:TextBox>
        <asp:TextBox ID="SOLO_LETTURA"   runat="server" Style="z-index: -1; left: 0px; position: absolute; top: 415px" TabIndex="-1" Width="24px">0</asp:TextBox>        

        <asp:HiddenField ID="txtAnnullo" runat="server" />    
        
        <br />
    
    </div>
        
    </form>

<script type="text/javascript">
    window.focus();
    self.focus();
</script>


     <script  language="javascript" type="text/javascript">
         document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>

</body>

</html>
