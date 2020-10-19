<%@ Control Language="VB" AutoEventWireup="false" CodeFile="Tab_Generale.ascx.vb"
    Inherits="Contratti_Generale" %>
<style type="text/css">
    .style1
    {
        font-family: Arial;
        font-size: xx-small;
    }
</style>
<script language="javascript" type="text/javascript">
// <!CDATA[

function VisDichiarazione_onclick() {
    if (document.getElementById('idDichOrigine').value !='0') {
if (<%=Provenienza %>=='1') {
    window.open('../max.aspx?ID=' + document.getElementById('idDichOrigine').value + '&LE=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');

}
else
{
if (<%=Provenienza %>=='8') {
window.open('../VSA/max.aspx?ID=<%=IndiceDichiarazione%>&LE=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');
}
else
{
if (<%=Provenienza %>=='10') {
window.open('../VSA/max.aspx?ID=<%=IndiceDichiarazione%>&LE=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');
}
else
{
    window.open('../CAMBI/max.aspx?ID=' + document.getElementById('idDichOrigine').value + '&LE=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');
}
}
}
}
else
{
alert('Non disponibile in questo momento!');
}
}

function VisDomanda_onclick() {
    if (document.getElementById('idDomandaOrigine').value !='0') {
if (<%=Provenienza %>=='1') {
    window.open('../domanda.aspx?ID=' + document.getElementById('idDomandaOrigine').value + '&ID1=-1&PROGR=-1&LE=1&APP=1&US=1','','top=0,left=0,width=670,height=450,resizable=no,menubar=no,toolbar=no,scrollbars=no');
}
else
{ 
if (<%=Provenienza %>=='8') {
//window.open('../VSA/domanda.aspx?ID=<%=IndiceDomanda%>&LE=1&US=1','','top=0,left=0,width=670,height=550,resizable=no,menubar=no,toolbar=no,scrollbars=no');
window.open('../VSA/NuovaDomandaVSA/domandaNuova.aspx?ID=<%=IndiceDomanda%>&LE=1&US=1','');
}
else
{
    window.open('../CAMBI/domanda.aspx?ID=' + document.getElementById('idDomandaOrigine').value + '&LE=1&US=1','','top=0,left=0,width=670,height=550,resizable=no,menubar=no,toolbar=no,scrollbars=no');
}
}
}
else
{
alert('Non disponibile in questo momento!');
}
}

function imgAnagrafe_onclick() {
//if (<%=IndiceAnagrafe %>=='-1') {
//alert('Non disponibile in questo momento! Verificare la presenza della Dichiarazione accedendo all\'apposito modulo!');
//}
//else
//{
//window.open('../ANAUT/max.aspx?CHIUDI=1&LE=1&ID=<%=IndiceAnagrafe%>','Dichiarazione','height=540,top=0,left=0,width=680,scrollbars=no');
window.open('../ANAUT/RisultatoRicercaD.aspx?F=1&XX=1&ENTE=ALTRI ENTI&CO='+document.getElementById('Tab_Contratto1_txtCodContratto').value,'','top=0,left=0,width=680,height=540,resizable=no,menubar=no,toolbar=no,scrollbars=no');
//}
}
function elenco_dich() {


window.open('ElencoDich.aspx?COD='+document.getElementById('Tab_Contratto1_txtCodContratto').value,'Elenco_dichiarazioni','height=520,top=0,left=0,width=670,scrollbars=no');
}

function elenco_conduttori() {

window.open('ElencoEXIntestatari.aspx?COD='+document.getElementById('Tab_Contratto1_txtCodContratto').value,'ElencoExCond','height=400,top=200,left=410,width=580,scrollbars=no');

}

function CreaAnagrafe() {
 var chiediConferma
    chiediConferma = window.confirm("Attenzione...Sei sicuro di volere creare una nuova scheda anagrafe utenza?");
    if (chiediConferma == true) {
        document.getElementById('USCITA').value='1';
        //window.open('CreaAU.aspx?T=<%=lIdConnessione %>&IDC='+document.getElementById('txtIdContratto').value,'Dichiarazione','height=400,top=0,left=0,width=350,scrollbars=no');
        window.showModalDialog('CreaAU.aspx?T=<%=lIdConnessione %>&IDC='+document.getElementById('txtIdContratto').value,window ,'status:no;dialogWidth:400px;dialogHeight:350px;dialogHide:true;help:no;scroll:no');
        }
        else {
        document.getElementById('USCITA').value='1';
        alert('Operazione annullata!');
    }
}
function CreaDomandaLocatari() {
 var chiediConferma
    chiediConferma = window.confirm("Attenzione...Sei sicuro di volere creare una nuova domanda?");
    if (chiediConferma == true) {
        document.getElementById('USCITA').value='1';
        window.open('../VSA/Locatari/nuova_domanda.aspx?ID='+document.getElementById('txtIdContratto').value+ '&COD='+ document.getElementById('Tab_Contratto1_txtCodContratto').value +'&INTEST=' + document.getElementById('LBLintest').value,'Nuova_domanda','height=570,top=0,left=0,width=820,scrollbars=no');
        }
        else {
        document.getElementById('USCITA').value='1';
        alert('Operazione annullata!');
    }
}



// ]]>
</script>
<div style="width: 1130px; position: absolute; top: 168px; height: 520px; left: 11px;
    border-top-width: thin; border-left-width: thin; border-left-color: gray; border-bottom-width: thin;
    border-bottom-color: gray; border-top-color: gray; border-right-width: thin;
    border-right-color: gray;">
    <table style="border-top-width: 3px; border-left-width: 3px; border-left-color: #DCDCDC;
        border-bottom-width: 3px; border-bottom-color: #DCDCDC; border-top-color: #DCDCDC;
        border-right-width: 3px; border-right-color: #DCDCDC;" width="100%">
        <tr>
            <td style="width: 182px; border-bottom: 3px solid; height: 80px; text-align: center;
                border-right-width: 3px">
                <img alt="Contratto" src="Immagini/Contratto.png" />&nbsp;<br />
                <asp:Label ID="lblContratto" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    Text="CONTRATTO"></asp:Label>
            </td>
            <td style="border-bottom: #DCDCDC 3px solid; height: 80px; width: 655px;">
            <table style="width: 100%">
            <tr>
            <td width="90%">
                            <table style="width: 90%">
                    <tr>
                        <td style="width: 4px">
                            <asp:Label ID="Label1" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Tipologia"></asp:Label>
                        </td>
                        <td style="width: 435px">
                            <asp:Label ID="Label2" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Stato"></asp:Label>
                        </td>
                        <td style="width: 435px">
                            <asp:Label ID="lblGIMI" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Cod. Contratto GIMI"></asp:Label>
                        </td>
                        <td style="width: 435px">
                            <asp:Label ID="Label20" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                Text="Saldo Attuale" ForeColor="#CC0000"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 4px">
                            <asp:Label ID="lblTipologia" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                                Width="197px"></asp:Label>
                        </td>
                        <td style="width: 435px">
                            <asp:Label ID="lblStato" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                                Width="197px"></asp:Label>
                        </td>
                        <td style="width: 435px">
                            <asp:Label ID="lblCodiceGimi" runat="server" Font-Bold="True" Font-Names="arial"
                                Font-Size="10pt" Width="197px"></asp:Label>
                        </td>
                        <td style="width: 435px">
                            <asp:Label ID="lblSaldoAttuale" runat="server" Font-Bold="True" Font-Names="arial"
                                Font-Size="10pt" Width="150px" ForeColor="#CC0000"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 4px">
                            <asp:Label ID="Label3" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Data decorrenza" Width="152px"></asp:Label>
                        </td>
                        <td style="width: 435px">
                            <asp:Label ID="Label4" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Data scadenza"></asp:Label>
                        </td>
                        <td style="width: 435px">
                            <asp:Label ID="Label8" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Data Disdetta"></asp:Label>
                        </td>
                        <td style="width: 435px">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 4px">
                            <asp:Label ID="lblDecorrenza" runat="server" Font-Bold="True" Font-Names="arial"
                                Font-Size="10pt" Width="197px"></asp:Label>
                        </td>
                        <td style="width: 435px">
                            <asp:Label ID="lblScadenza" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                                Width="197px"></asp:Label>
                        </td>
                        <td style="width: 435px">
                            <asp:Label ID="lblDisdetta" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                                Width="197px"></asp:Label>
                        </td>
                        <td style="width: 435px">
                            <div id="ContrColl" runat="server" style="cursor: pointer; width: 120px; display: none;"
                                onclick="ApriDettRifContratto();">
                                <img alt="Contratto Collegato" src="../CALL_CENTER/Immagini/alertSegn.gif" height="20px" />
                                <asp:Label ID="Label9" Text="Altro Contratto" runat="server" Font-Names="arial" Font-Size="9pt"
                                    Font-Italic="True"></asp:Label>
                            </div>
                        </td>
                    </tr>
                </table>

            </td>
            <td width="10%" align="center" valign="top"  >
            <table style="width:100%;" cellpadding="0" cellspacing="1">
                    <tr>
                        <td>
                            <asp:Image ID="btnArchivio" runat="server" 
                                ImageUrl="~/NuoveImm/Img_DocumentiArchivio.png" style="cursor:pointer"/>
                        </td>  
                        <td>&nbsp;&nbsp;</td>
                           <td>
                            <asp:Image ID="Image1" runat="server" 
                                ImageUrl="~/NuoveImm/Img_ElencoAllegatiContratti.png" style="cursor:pointer" 
                                   onclick="javascript:AllegatiContratto();"/>
                        </td>                         
                    </tr>
                    
                    <tr>
                        <td class="style1" align="center">
                            ARCHIVIO CARTACEO</td>  
                            <td>&nbsp;&nbsp;</td>
                           <td class="style1" align="center">
                               ELENCO ALLEGATI
                        </td>                         
                    </tr>
                </table>
                </td>
            </tr>
            </table>
            </td>
        </tr>
        <tr>
            <td style="width: 182px; border-bottom: 3px solid; text-align: center; border-right-width: 3px;
                height: 158px;">
                <img src="../Contratti/Immagini/home-red-128x128.png" height="55" />&nbsp;<br />
                <asp:Label ID="lblImmobile" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                    Text="IMMOBILE LOCATO"></asp:Label>
            </td>
            <td style="border-bottom: #DCDCDC 3px solid; width: 655px; height: 158px;">
                <table width="100%">
                    <tr>
                        <td style="width: 174px">
                            <asp:Label ID="Label5" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Tipo"></asp:Label>
                        </td>
                        <td style="width: 188px">
                            <asp:Label ID="Label6" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Cod. Unità"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label7" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Ubicazione"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 174px">
                            <asp:Label ID="lblTipoImmobile" runat="server" Font-Bold="True" Font-Names="arial"
                                Font-Size="10pt" Width="197px"></asp:Label>
                        </td>
                        <td style="width: 188px">
                            <asp:Label ID="lblCodUnita" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                                Width="197px"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblIndirizzo" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                                Width="288px"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 174px; height: 21px">
                            <table width="100%">
                                <tr>
                                    <td style="width: 64px">
                                        <asp:Label ID="Label12" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            Text="Sup. Convenzionale" Width="124px"></asp:Label>
                                    </td>
                                    <td style="width: 3px">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 64px">
                                        <asp:Label ID="lblConvenzionale" runat="server" Font-Bold="True" Font-Names="arial"
                                            Font-Size="10pt" Width="77px"></asp:Label>
                                    </td>
                                    <td style="width: 3px">
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="width: 188px; height: 21px">
                            <table width="100%">
                                <tr>
                                    <td style="width: 64px">
                                        <asp:Label ID="Label15" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                            Text="Complesso/Edificio" Width="124px"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 64px">
                                        <asp:Label ID="lblComplessoEdificio" runat="server" Font-Bold="True" Font-Names="arial"
                                            Font-Size="10pt" Width="180px" Style="cursor: pointer">clicca qui per visualizzare</asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="height: 21px">
                            <table width="100%">
                                <tr>
                                    <td style="width: 64px">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 64px">
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 174px; height: 21px">
                        </td>
                        <td style="width: 188px; height: 21px">
                        </td>
                        <td style="height: 21px">
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="width: 182px; height: 21px; text-align: center; border-bottom-width: 3px;
                border-bottom-color: #DCDCDC;">
                <img src="../Contratti/Immagini/User_48x48.png" /><br />
                <asp:Label ID="lblConduttore22" runat="server" Font-Bold="True" Font-Names="Arial"
                    Font-Size="8pt" Text="CONDUTTORE"></asp:Label>
            </td>
            <td style="height: 21px; border-bottom-width: 3px; width: 655px;">
                <table style="width: 100%">
                    <tr>
                        <td style="width: 300px">
                            <asp:Label ID="Label13" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Conduttore"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Conduttore precedente" Width="200px"></asp:Label>
                        </td>
                        <td style="width: 283px;">
                            <asp:Label ID="Label21" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Storico Cond." Width="86px"></asp:Label>
                        </td>
                        <td style="width: 320px;">
                            <asp:Label ID="Label14" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Altri Contratti"></asp:Label>
                        </td>
                        <%--                        <td style="width: 283px">
                           <asp:Label ID="Label19" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
                                Text="Domande in corso" Visible="False"></asp:Label></td>
                        --%>
                    </tr>
                    <tr>
                        <td style="width: 270px">
                            <asp:Label ID="lblConduttore" runat="server" Font-Bold="True" Font-Names="arial"
                                Font-Size="10pt" Width="340px" Height="40px"></asp:Label>
                        </td>
                        <td style="vertical-align: top;" valign="top">
                            <asp:Label ID="lblExcondutt" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="10pt"
                                Height="16px"></asp:Label>
                        </td>
                        <td style="vertical-align: top; text-align: center;">
                            &nbsp;<img id="imgCrono" alt="Cronistoria conduttori" src="../NuoveImm/Img_History.png"
                                onclick="return elenco_conduttori()" style="cursor: pointer" title="Visualizza elenco conduttori" />
                        </td>
                        <td style="vertical-align: top;">
                            <asp:Label ID="lblAltriAttivi" runat="server" Font-Bold="True" Font-Names="arial"
                                Font-Size="7pt" Width="140px" Height="16px"></asp:Label>
                        </td>
                        <%--                        <td style="width: 283px">
                            <asp:Label ID="lblDomandeInCorso" runat="server" Font-Bold="True" Font-Names="arial"
                                Font-Size="7pt" Width="122px" Height="16px" Visible="False"></asp:Label>
                        </td>
                        --%>
                    </tr>
                    <tr>
                        <td style="width: 300px; font-family: arial; font-size: 9px;">
                            clicca sul nome per visualizzare estr.Conto o &nbsp;anagrafica
                            <br />
                            <span style='color: Red'>* Intestatario del rapporto</span>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="tblBando" style="border-top: #DCDCDC 3px solid; height: 110px; border-collapse: separate;
        border-spacing: 0px;" width="100%">
        <tr>
            <td id="rigaImgBando" style="text-align: center; border-right: #DCDCDC 3px solid;">
                <table id="iconeBando" style="height: 50px;" align="center">
                    <tr>
                        <td colspan="2" align="center">
                            <span style="font-family: Arial; font-size: 10pt"><strong>BANDO</strong></span>
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            <img id="VisDichiarazione" alt="Clicca qui per visualizzare la Dichiarazione ISEE"
                                src="../NuoveImm/Dichirazione.png" onclick="return VisDichiarazione_onclick()"
                                style="cursor: pointer" />
                        </td>
                        <td>
                            <img id="VisDomanda" alt="Clicca qui per visualizzare la Domanda di Bando" src="../NuoveImm/Domanda.png"
                                onclick="return VisDomanda_onclick()" style="cursor: pointer" />
                        </td>
                    </tr>
                </table>
            </td>
            <td id="rigaImgAnagr" style="text-align: center; border-right: #DCDCDC 3px solid;">
                <table id="iconeAU" style="height: 50px" align="center">
                    <tr>
                        <td colspan="2" align="center">
                            <span style="font-family: Arial; font-size: 10pt"><strong>ANAGRAFE DELL&#39;UTENZA</strong></span>
                        </td>
                    </tr>
                    <tr align="center">
                        <td>
                            <asp:ImageButton ID="imgCreaAU" runat="server" ImageUrl="~/NuoveImm/AnagrafeUtenzaNuova.png"
                                OnClientClick="CreaAnagrafe();" ToolTip="Crea una nuova scheda anagrafe utenza" />
                            <img id="imgScegliAU" src="../NuoveImm/AnagrafeUtenzaNuova.png" onclick="return CreaAUabusivi()"
                                alt="Scelta bando per AU" style="border: 1px solid #0000FF; cursor: pointer"
                                title="Crea una nuova scheda anagrafe utenza" />
                        </td>
                        <td>
                            <img id="imgAnagrafe" alt="Clicca qui per visualizzare l'Anagrafe Utenza" src="../NuoveImm/AnagrafeUtenza.png"
                                onclick="return imgAnagrafe_onclick()" style="cursor: pointer" />
                        </td>
                    </tr>
                </table>
            </td>
            <td id="tblLocatari" style="text-align: center;">
                <table id="iconeVSA" style="height: 50px;" align="center">
                    <tr>
                        <td colspan="3" align="center">
                            <span style="font-family: Arial; font-size: 10pt"><strong>GESTIONE LOCATARI</strong></span>
                        </td>
                    </tr>
                    <tr align="center">
                        <td class="style3">
                            <asp:ImageButton ID="btnNewIstanza" runat="server" ImageUrl="~/NuoveImm/NuovaIstanza.png"
                                ToolTip="Crea una nuova istanza" OnClientClick="apriPaginaScelta();return false;" />
                        </td>
                        
                        <td class="style3"  style="border-left: #DCDCDC 3px solid;">
                       &nbsp;<img id="img2" alt="Elenco istanze" src="../NuoveImm/ElencoIstanze.png" onclick="return elenco_istanze()"
                                style="cursor: pointer" /></td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Image ID="img_ALERT" runat="server" ImageUrl="../IMG/Alert.gif" Style="z-index: 404;
                                height: 17px;" Visible="False" /><asp:Label ID="LBLDECADENZA" runat="server" Font-Bold="True"
                                    Font-Names="ARIAL" Font-Size="9pt" Text="Proposta di decadenza" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table style="border-top: #DCDCDC 3px solid; border-collapse: separate; border-spacing: 0px;"
        width="100%">
        <tr>
            <td style="text-align: center; vertical-align: top;">
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center;">
                            <asp:Label ID="lblAlertCauz" runat="server" Text="ATTENZIONE! Restituire il Deposito alla Chiusura Contrattuale!"
                                Visible="False" Style="font-weight: 700; text-decoration: blink; font-family: Arial;
                                font-size: 11pt; color: White; background-color: Red; text-align: center;"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</div>
<div id="InfoUtente" align="center" style="border: 1px solid #0000FF; width: 1180px;
    height: 900px; position: absolute; top: 0px; left: 0px; background-color: #c3c3bb;
    visibility: hidden;">
    <table style="position: absolute; width: 35%; z-index: 200; background-color: #FFFFFF;
        top: 185px; left: 242px; height: 260px;">
        <tr>
            <td>
                <asp:Label ID="Label16" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
                    ForeColor="Black" Text="Cosa Vuoi Vedere?"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label17" runat="server" Font-Names="arial" Font-Size="10pt" ForeColor="#0000CC"
                    Text="Scheda Anagrafica" Style="cursor: pointer" onclick="if (document.getElementById('lettura').value == '1') {window.open('anagrafica/Inserimento.aspx?LT=1&DAC=1&ID='+document.getElementById('Generale1_hAnagrafica').value,'Anagrafe','height=500,top=0,left=0,width=500,scroll=no,status=no');} else { window.open('anagrafica/Inserimento.aspx?DAC=1&ID='+document.getElementById('Generale1_hAnagrafica').value,'Anagrafe','height=500,top=0,left=0,width=500,scroll=no,status=no');}"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label18" runat="server" Font-Names="arial" Font-Size="10pt" ForeColor="#0000CC"
                    Text="Estratto Conto" Style="cursor: pointer" onclick="window.open('../Contabilita/DatiUtenza.aspx?C=RisUtenza&IDANA='+document.getElementById('Generale1_hAnagrafica').value + '&IDCONT=' + document.getElementById('txtIdContratto').value ,'EstrattoConto','');"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td align="right">
                <img style="cursor: pointer" alt="Annulla" src="../NuoveImm/Img_AnnullaVal.png" onclick="myOpacity10.toggle();" />
            </td>
        </tr>
    </table>
    <asp:Image ID="ImgSfondoGenerale" runat="server" ImageUrl="~/ImmDiv/SfondoDim1.jpg"
        Style="z-index: 199; position: absolute; top: 155px; left: 213px;" BackColor="White" />
    <asp:HiddenField ID="hAnagrafica" runat="server" />
</div>
<script type="text/javascript">
    // document.getElementById('Info').style.visibility = 'hidden';

    function CosaFare() {
        document.getElementById('USCITA').value = '1';
        myOpacity10.toggle();


    }
</script>
