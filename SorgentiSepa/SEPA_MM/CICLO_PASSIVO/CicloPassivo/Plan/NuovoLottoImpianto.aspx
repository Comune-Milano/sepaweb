<%@ Page Language="VB" AutoEventWireup="false" CodeFile="NuovoLottoImpianto.aspx.vb" Inherits="Contabilita_CicloPassivo_Plan_NuovoLotto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="prototype.lite.js"></script>
<script type="text/javascript" src="moo.fx.js"></script>
<script type="text/javascript" src="moo.fx.pack.js"></script>
<head id="Head1" runat="server">
<base target="_self"/>
    <title>Lotto IMPIANTI</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <table style="left: 0px; BACKGROUND-IMAGE: url('../../../NuoveImm/SfondoMascheraContratti.jpg'); WIDTH: 798px;
            position: absolute; top: 0px">
            <tr>
                <td style="width: 706px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                    Piano Finanziario-</strong>
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Text="Label"></asp:Label>
                    -
                    <asp:Label ID="lblStato" runat="server" style="font-weight: 700"></asp:Label>
                    </span><br />
                    <br />

                    <br />
                    <asp:Label ID="lblServizio1" runat="server" 
                        style="position:absolute; top: 149px; left: 14px;" Font-Bold="False" 
                        Font-Names="arial" Font-Size="10pt">Descrizione</asp:Label>
                        <asp:Label ID="Label3" runat="server" 
                        style="position:absolute; top: 179px; left: 14px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt">Impianto</asp:Label>
                    <asp:Label ID="lblLotto1" runat="server" 
                        style="position:absolute; top: 118px; left: 14px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt">Struttura</asp:Label>
                    <asp:Label ID="lblLotto" runat="server" 
                        style="position:absolute; top: 92px; left: 89px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt" Visible="False">Lotto</asp:Label>
                    <asp:Label ID="lblServizio" runat="server" 
                        style="position:absolute; top: 62px; left: 14px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt">Servizio</asp:Label>
                    <asp:Label ID="lblVoce" runat="server" 
                        style="position:absolute; top: 82px; left: 14px; width: 763px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />

                        
                    <br />
                    <asp:TextBox ID="txtDescrizione" runat="server" Font-Names="arial" Font-Size="10pt" 
                        MaxLength="100" ToolTip="Descrizione Lotto" 
                        style="position:absolute; top: 147px; left: 87px; width: 620px;" 
                        TabIndex="2"></asp:TextBox>
    
                    <asp:DropDownList ID="cmbFiliale" runat="server" 
                        style="position:absolute; top: 116px; left: 88px;" Font-Names="arial" 
                        Font-Size="10pt" Width="620px" TabIndex="1" AutoPostBack="True">
                    </asp:DropDownList>
    
                    <asp:DropDownList ID="cmbTipoImpianto" runat="server" 
                        style="position:absolute; top: 176px; left: 88px;" Font-Names="arial" 
                        Font-Size="10pt" Width="620px" TabIndex="3" AutoPostBack="True">
                        
                    </asp:DropDownList>
    
                    <br />
    
                    <br />
    
                    <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_SalvaGrande.png"
                        Style="left: 574px; position: absolute; top: 546px; height: 20px;" 
                        TabIndex="8" />
    
                    <br />
                    <br />
                    <div                 
                                                
                        style="border-color: #ccccff; border-style: solid; position: absolute; top: 248px; left: 14px; width: 659px; overflow: auto; right: 117px; height: 235px;">
        <asp:DataGrid ID="tabcomplessi" runat="server" AutoGenerateColumns="False"
            BackColor="White" BorderWidth="0px" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
            Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" Style="z-index: 101; left: 3px; top: 65px"
            Width="100%" GridLines="None" AllowSorting="True" Visible="False" Height="50px" 
                            TabIndex="4">
            <HeaderStyle BackColor="#F2F5F1" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="#0000C0" Wrap="False" />
            <Columns>
                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DENOMINAZIONE" HeaderText="DENOMINAZIONE">
                </asp:BoundColumn>
            </Columns>
            <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Mode="NumericPages" Wrap="False" Visible="False" />
            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
        </asp:DataGrid>
                    </div>
                        <asp:ImageButton ID="btnEliminaComplesso" runat="server" ImageUrl="Immagini/minus_icon.png"
                            Style="z-index: 102; left: 689px; top: 288px; position:absolute; height: 18px;cursor:pointer; width: 18px;" 
                ToolTip="Elimina Elemento Selezionato" onclientclick="Cancellare();" 
                        TabIndex="6" />
            <asp:Image ID="Img1" 
                alt="Aiuto Ricerca per Complessi" onclick="document.getElementById('txtcomplessi').value='1';myOpacitycom.toggle();"
                runat="server" 
                ImageUrl="Immagini/40px-Crystal_Clear_action_edit_add.png" 
                        ToolTip="Aggiungi Complesso" 
                        style="position:absolute; top: 248px; left: 689px;cursor:pointer; " 
                        TabIndex="5" />
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
                    <asp:Image ID="imgEsci" runat="server" 
                        style="position:absolute; top: 546px; left: 669px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_EsciCorto.png" 
                        onclick="ConfermaEsci();" TabIndex="9"/>
                    <br />
                    <br />
                    <br />
                    <asp:HiddenField ID="idPianoF" runat="server" />
            <asp:Image ID="Img2" 
                alt="Aiuto Ricerca per Edifici" onclick="VisualizzaComplesso();"
                runat="server" 
                ImageUrl="Immagini/home16.png" 
                        ToolTip="Visualizza dati dell'impianto" 
                        style="position:absolute; top: 335px; left: 689px;cursor:pointer; " 
                        TabIndex="7" />
                    <asp:HiddenField ID="idVoce" runat="server" />
                    <asp:HiddenField ID="idServizio" runat="server" />
                    <asp:HiddenField ID="idLotto" runat="server" />
                    <asp:HiddenField ID="txtcomplessi" runat="server" />
                    <asp:HiddenField ID="txtedifici" runat="server" />
                    <asp:HiddenField ID="txtModificato" runat="server" />
                    <asp:HiddenField ID="txtIdComponente" runat="server" />
                    <asp:HiddenField ID="USCITA" runat="server" />
                    <asp:HiddenField ID="lettura" runat="server" />
                    <asp:HiddenField ID="sicuro" runat="server" Value="0" />
                    <asp:HiddenField ID="idEsercizioF" runat="server" Value="0" />
                    <asp:HiddenField ID="tipostruttura" runat="server" Value="0" />
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Visible="False" 
                        style="position:absolute; top: 523px; left: 15px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                    <br />
                </td>
            </tr>
        </table>
    


                     
                    <asp:Label ID="Label24" runat="server" Font-Bold="True" 
            Font-Names="Arial" Font-Size="8pt"
                        
            Style="z-index: 104; left: 16px; position: absolute; top: 230px">Lista Impianti</asp:Label>



                     
    </div>
                <div id="RicercaEdifici" 
        
        
        
        
        
        
        style="z-index: 405; left: 0px; width: 800px; position: absolute; top: 0px; height: 600px; background-image: url('../../../NuoveImm/SfondoMascheraContratti.jpg'); background-repeat: no-repeat; visibility: hidden;">
    
                
                    <table style="width: 461px; height: 185px; background-color: #FFFFFF; z-index: 406; position: absolute; top: 196px; left: 175px;">
                        <tr>
                            <td style="vertical-align: top; text-align: left" class="style1">
                                <asp:Label ID="Label23" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 14px; top: 32px" 
                                    Width="238px">Inserire complessi per edifici</asp:Label>
                            </td>
                            <td style="vertical-align: baseline; text-align: left" class="style2">
                                </td>
                            <td style="vertical-align: baseline; text-align: left" class="style2">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; text-align: left" class="style3">
            <asp:DropDownList ID="cmbComplesso" runat="server" AutoPostBack="True" BackColor="White"
                Font-Names="arial" Font-Size="10pt" 
                                    Style="border: 1px solid black; z-index: 10; left: 2px; position: absolute; top: 28px; height: 14px; width: 356px;" 
                                    ToolTip="Seleziona complesso per edificio">
            </asp:DropDownList>
                            </td>
                            <td style="vertical-align: baseline; text-align: left" class="style4">
                                &nbsp;</td>
                            <td style="vertical-align: baseline; text-align: left" class="style4">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 352px; height: 104px; text-align: left">
                                <asp:Label
                                        ID="LblNoResulted" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Red" Style="z-index: 100; left: 14px; top: 73px" 
                                    Visible="False" Width="97px">Nessun Risultato</asp:Label><br />
                                <div style="left: 5px; overflow: auto; width: 360px; top: 87px; height: 101px">
                                    <asp:CheckBoxList ID="lstedifici" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="Black" Width="350px">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                            <td style="vertical-align: baseline; width: 27px; height: 104px; text-align: left">
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                <br />
                                </td>
                            <td style="vertical-align: bottom; width: 27px; height: 104px; text-align: left">
                                </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 352px;  text-align: left">
                                &nbsp;</td>
                            <td style="vertical-align: bottom; width: 27px;  text-align: left">
                                <asp:ImageButton ID="BtnConfermaedificio" runat="server" ImageUrl="Immagini/next.png"
                                    Style="z-index: 111; left: 268px; top: 190px" ToolTip="Conferma" 
                                    onclientclick="myOpacityed.toggle();" /></td>
                            <td style="vertical-align: bottom; width: 27px;  text-align: left">
                                <img id="ImgAnnulla1" alt="" src="Immagini/Annulla.png" onclick="myOpacityed.toggle();" style="cursor:pointer "/></td>
                        </tr>
                    </table>
                
                    <img alt="" src="../../../ImmDiv/SfondoDiv.png" 
                        style="position: absolute; z-index: 405; top: 0px; left: 0px;" /></div>
    <div id="RicercaComplessi" 
        
        
        
        style="z-index: 400; left: 0px; width: 800px; position: absolute; top: 0px; height: 600px; background-image: url('../../../ImmDiv/SfondoDivLotto.png'); background-repeat: no-repeat; visibility: hidden;">
                    <table style="width: 525px; height: 428px; background-color: #FFFFFF; margin-right: 0px; position:absolute; top: 99px; left: 134px; z-index: 402;">
                        <tr>
                            <td style="vertical-align: top; text-align: left" class="style1">
                                <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                    ForeColor="Black" Style="z-index: 100; left: 14px; top: 32px" 
                                    Width="238px">LISTA IMPIANTI</asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; width: 352px; height: 104px; text-align: left">
                                <asp:Label
                                        ID="LblNoResult" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                        ForeColor="Red" Style="z-index: 100; left: 14px; top: 73px" Visible="False" Width="97px">Nessun Risultato</asp:Label><br />
                                <div style="border: 2px solid #990000; left: 5px; overflow: auto; width: 500px; top: 87px; height: 348px">
                                    <asp:CheckBoxList ID="lstcomplessi" runat="server" Font-Names="Arial" 
                                        Font-Size="8pt" ForeColor="Black" Width="430px">
                                    </asp:CheckBoxList>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top; text-align: right" align="right">
                                <br />
                                    <asp:ImageButton ID="SelezionaTutto" runat="server" ImageUrl="~/CICLO_PASSIVO/CicloPassivo/Plan/Immagini/img_Sel_Tutti.png"
                                    Style="z-index: 111; left: 268px; top: 190px; height: 12px;" ToolTip="Seleziona Tutti" 
                                    onclientclick="myOpacitycom.toggle();" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:ImageButton ID="BtnConfermacomplesso" runat="server" ImageUrl="Immagini/next.png"
                                    Style="z-index: 111; left: 268px; top: 190px; height: 12px;" ToolTip="Conferma" 
                                    onclientclick="myOpacitycom.toggle();" />
                                    &nbsp;&nbsp; <img id="ImgAnnulla" alt="Annulla" src="Immagini/Annulla.png" onclick="myOpacitycom.toggle();" style="cursor:pointer "/></td>
                        </tr>
                    </table>
                    <img alt="" src="../../../ImmDiv/SfondoDivLotto.png" 
                        style="position:absolute; top: 0px; left: 0px; z-index: 401; visibility: hidden;"/>
            </div>
    <asp:TextBox ID="txtseledifici" runat="server" BackColor="#F2F5F1" BorderColor="White"
                BorderStyle="None" Font-Names="Arial" Font-Size="9pt" 
        Height="15px" MaxLength="100"
                ReadOnly="True" Style="left: 15px; top: 495px; position: absolute; width: 538px; z-index:100" 
                        TabIndex="-1"></asp:TextBox>
    <script type="text/javascript">

        myOpacitycom = new fx.Opacity('RicercaComplessi', { duration: 200 });
        if (document.getElementById('txtcomplessi').value != '2') {
            myOpacitycom.hide(); 
        }
                                        
        </script>
        
    </form>
    
        <script type="text/javascript">
            function Cancellare() {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione, Sei sicuro di voler eliminare il complesso dal lotto?");
                if (chiediConferma == true) {
                    document.getElementById('sicuro').value = '1';
                }
                else {
                    document.getElementById('sicuro').value = '0';
                }
            }
        
        function ConfermaEsci() {
            if (document.getElementById('txtmodificato').value == '1') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione, sono state fatte delle modifiche. Sei sicuro di voler uscire senza salvare?");
                if (chiediConferma == true) {
                    self.close();
                }
            }
            else {
                if (document.getElementById('lettura').value == '1') {
                    self.close();
                }
                else {
                    var chiediConferma
                    chiediConferma = window.confirm("Sei sicuro di voler uscire?");
                    if (chiediConferma == true) {
                        self.close();
                    }
                }
            }
        }

        function VisualizzaComplesso() {
            if (document.getElementById('txtIdComponente').value > 0) {
                //window.open('../../../CENSIMENTO/InserimentoComplessi.aspx?SLE=1&ID=' + document.getElementById('txtIdComponente').value, 'Dettagli', 'height=580,top=0,left=0,width=780');
                window.open('TrovaImpianto.aspx?ID=' + document.getElementById('txtIdComponente').value, 'Dettagli', 'height=580,top=0,left=0,width=800');
                
                //alert('Non Disponibile al momento!');
            }
            
        }
        
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
        
        if (document.getElementById('lettura').value == '1') {
            document.getElementById('Img1').style.visibility = 'hidden';
            document.getElementById('Img1').style.position = 'absolute';
            document.getElementById('Img1').style.left = '-100px';
            document.getElementById('Img1').style.display = 'none';

            document.getElementById('Img2').style.visibility = 'hidden';
            document.getElementById('Img2').style.position = 'absolute';
            document.getElementById('Img2').style.left = '-100px';
            document.getElementById('Img2').style.display = 'none';
        }
        
    </script>
</body>
</html>
