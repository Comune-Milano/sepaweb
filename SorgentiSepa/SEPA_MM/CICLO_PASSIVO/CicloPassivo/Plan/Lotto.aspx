<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Lotto.aspx.vb" Inherits="Contabilita_CicloPassivo_Plan_Lotto" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<script src="jquery-1.8.2.js" type="text/javascript"></script>
<script src="jquery-impromptu.4.0.min.js" type="text/javascript"></script>
<head id="Head1" runat="server">
<base target="_self"/>
<link rel="stylesheet" type="text/css" href="impromptu.css" />
    <title>Scelta Servizio</title>
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
                        style="position:absolute; top: 179px; left: 14px; font-style: italic;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt">Servizi Associati al lotto selezionato</asp:Label>
                    <asp:Label ID="lblServizio0" runat="server" 
                        style="position:absolute; top: 125px; left: 14px; font-style: italic;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt">Elenco LOTTI - E=Lotto su Edificio   I=Lotto su Impianto</asp:Label>
                    <asp:Label ID="lblServizio" runat="server" 
                        style="position:absolute; top: 64px; left: 14px; font-style: italic;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt">Servizio DGR</asp:Label>
                    <asp:Label ID="lblServiziAssociati" runat="server" 
                        style="position:absolute; top: 200px; left: 14px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="9pt"></asp:Label>
                    <asp:Label ID="lblVoce" runat="server" 
                        style="position:absolute; top: 84px; left: 14px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="10pt"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
    
                    <asp:ImageButton ID="ImgEliminaLotto" runat="server" ImageUrl="Immagini/Elimina.png"
                        Style="left: 602px; position: absolute; top: 146px; height: 16px; width: 16px;" 
                        TabIndex="4" 
                        
                        onclientclick="ConfermaElimina();" 
                        ToolTip="Elimina Lotto" />
    
                    <asp:ImageButton ID="ImgModificaLotto" runat="server" ImageUrl="Immagini/Pencil-Icon.png"
                        Style="left: 565px; position: absolute; top: 146px; " 
                        TabIndex="4" 
                        ToolTip="Modifica Lotto" onclientclick="ChiamaFinestra();" />
    
                    <asp:ImageButton ID="ImgNuovoLotto" runat="server" ImageUrl="Immagini/40px-Crystal_Clear_action_edit_add.png"
                        Style="left: 888px; position: absolute; top: 235px; width: 18px; height: 18px;" 
                        TabIndex="4" 
                        
                        onclientclick="ApriLotto();" 
                        ToolTip="Aggiungi Nuovo Lotto" />
    
                    <asp:DropDownList ID="cmbLotti" runat="server" 
                        style="position:absolute; top: 146px; left: 14px;" Font-Names="arial" 
                        Font-Size="10pt" Width="500px" TabIndex="1" AutoPostBack="True" 
                        CausesValidation="True">
                    </asp:DropDownList>
                    <br />
    
                    <asp:ImageButton ID="ImgProcedi0" runat="server" ImageUrl="~/NuoveImm/Img_Precedente.png"
                        Style="left: 447px; position: absolute; top: 546px; height: 20px;" 
                        TabIndex="4" />
    
                    <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                        Style="left: 574px; position: absolute; top: 546px; height: 20px;" 
                        TabIndex="4" onclientclick="ConfermaProcedi()" />
    
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <img alt="" src="Immagini/40px-Crystal_Clear_action_edit_add.png" 
                        style="position:absolute;cursor:pointer; top: 146px; left: 527px;" 
                        onclick="ApriLotto();"/><br />
                    <br />
                    <br />
                    <br />
                    <asp:Image ID="imgEsci" runat="server" 
                        style="position:absolute; top: 546px; left: 669px; cursor:pointer" 
                        ImageUrl="~/NuoveImm/Img_EsciCorto.png" 
                        onclick="ConfermaEsci();"/>
                    <br />
                    <br />
                    <br />
                    <asp:HiddenField ID="idPianoF" runat="server" />
                    <asp:HiddenField ID="idVoce" runat="server" />
                    <asp:HiddenField ID="idServizio" runat="server" />
                    <asp:HiddenField ID="elimina" runat="server" Value="0" />
                    <asp:HiddenField ID="conferma" runat="server" Value="0" />
                    <asp:HiddenField ID="cista" runat="server" Value="0" />
                    <asp:HiddenField ID="tipolotto" runat="server" Value="0" />
                    <asp:HiddenField ID="selezionato" runat="server" Value="0" />
                    <br />
                    <asp:Label ID="lblErrore" runat="server" Visible="False" 
                        style="position:absolute; top: 430px; left: 19px;" Font-Bold="True" 
                        Font-Names="arial" Font-Size="8pt" ForeColor="Red"></asp:Label>
                    <br />
                </td>
            </tr>
        </table>
    
    </div>
    
    </form>
    
        <script type="text/javascript">


          

        function ConfermaEsci() {

         
                var chiediConferma
                chiediConferma = window.confirm("Sei sicuro di voler uscire?");
                if (chiediConferma == true) {
                    self.close();
                }

            }


            function ConfermaProcedi() {


                var chiediConferma

                if (document.getElementById('cista').value == '1') {
                    chiediConferma = window.confirm("Sei sicuro di voler procedere?");
                }
                else {
                    chiediConferma = window.confirm("Il lotto selezionato non è attualmente associato al servizio scelto. Sei sicuro di voler procedere?");
                }
                                
                if (chiediConferma == true) {
                    document.getElementById('conferma').value = '1';
                }

            }


            function ConfermaElimina() {
                var chiediConferma
                chiediConferma = window.confirm("Sei sicuro di voler ELIMINARE il lotto selezionato? Si ricorda che saranno eliminati tutti i valori e importi caricati relativi al lotto selezionato.");
                if (chiediConferma == true) {
                    document.getElementById('elimina').value = '1';
                }
                else {
                    document.getElementById('elimina').value = '0';
                }
            }

            function ChiamaFinestra() {
                var SelectedVal = document.getElementById('cmbLotti').value;
                if (document.getElementById('tipolotto').value == 'E') {
                    window.showModalDialog('NuovoLotto.aspx?ID=' + SelectedVal + '&IDV=' + document.getElementById('idVoce').value + '&IDP=' + document.getElementById('idPianoF').value + '&IDS=' + document.getElementById('idServizio').value, window, 'status:no;dialogWidth:800px;dialogHeight:600px;dialogHide:true;help:no;scroll:no');
                }
                else {
                    window.showModalDialog('NuovoLottoImpianto.aspx?ID=' + SelectedVal + '&IDV=' + document.getElementById('idVoce').value + '&IDP=' + document.getElementById('idPianoF').value + '&IDS=' + document.getElementById('idServizio').value, window, 'status:no;dialogWidth:800px;dialogHeight:600px;dialogHide:true;help:no;scroll:no');
                }

            }


            function mycallbackform(e,v, m, f) {
                if (v != undefined)
                    if (v == 1) {
                        window.showModalDialog('NuovoLotto.aspx?ID=-1&IDV=' + document.getElementById('idVoce').value + '&IDP=' + document.getElementById('idPianoF').value + '&IDS=' + document.getElementById('idServizio').value, window, 'status:no;dialogWidth:800px;dialogHeight:600px;dialogHide:true;help:no;scroll:no');
                        document.getElementById('ImgNuovoLotto').click();
                    }
                    if (v == 2) {
                        window.showModalDialog('NuovoLottoImpianto.aspx?ID=-1&IDV=' + document.getElementById('idVoce').value + '&IDP=' + document.getElementById('idPianoF').value + '&IDS=' + document.getElementById('idServizio').value, window, 'status:no;dialogWidth:800px;dialogHeight:600px;dialogHide:true;help:no;scroll:no');
                        document.getElementById('ImgNuovoLotto').click();
                    }
                   
            }



            function ApriLotto() {
                var txt = 'Il nuovo LOTTO deve essere basato su:<br />';
                $.prompt(txt, {
                    callback: mycallbackform,
                    buttons: { Edifici: '1', Impianti: '2' },
                    show: 'slideDown' });

                
                //var chiediConferma
                //chiediConferma = window.confirm("Vuoi creare un nuovo Lotto basato su EDIFICI?");
                //if (chiediConferma == true) {
                //    window.showModalDialog('NuovoLotto.aspx?ID=-1&IDV=' + document.getElementById('idVoce').value + '&IDP=' + document.getElementById('idPianoF').value + '&IDS=' + document.getElementById('idServizio').value, window, 'status:no;dialogWidth:800px;dialogHeight:600px;dialogHide:true;help:no;scroll:no');
                //}
                //else {
                //    document.getElementById('elimina').value = '0';
                //}
            }

    </script>
</body>
</html>
