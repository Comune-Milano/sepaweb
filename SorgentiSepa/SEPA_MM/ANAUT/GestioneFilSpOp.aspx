<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestioneFilSpOp.aspx.vb" Inherits="ANAUT_GestioneFilSpOp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Selezionato;
    var Selezionato1;
    var Selezionato2;
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<link type="text/css" href="css/smoothness/jquery-ui-1.8.23.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.23.custom.min.js"></script>
    <script type="text/javascript" src="js/jquery.ui.datepicker-it.js"></script>
    <script type="text/javascript" src="js/jsfunzioni.js"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
    <title>Gestione</title>
    <style type="text/css">
        .style1
        {
            text-align: left;
            color: #FFFFFF;
            font-weight: bold;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12pt;
        }
        .style2
        {
            color: #801f1c;
            font-size: 14pt;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManagerBando" runat="server">
    </asp:ScriptManager>
    <div>
    
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    
        <table style="left: 0px; background-image: url('../NuoveImm/SfondoMaschere.jpg'); width: 674px;
                position: absolute; top: 0px">
            <tr>
                <td class="style1">
                    <br />
&nbsp; <span class="style2">Associazione Anagrafe Utenza/Strutture/Sportelli/Operatori</span></td>
            </tr>
            <tr>
                <td style="text-align: left">
                    &nbsp;
                    <asp:Label ID="lblDescrBando" runat="server" Font-Names="arial" 
                        Font-Size="10pt" style="font-weight: 700"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table style="width:100%;" cellpadding="1" cellspacing="0">
                        <tr bgcolor="#FFFFCC">
                            <td width="70%">
                    <asp:Label ID="lblDescrBando0" runat="server" Font-Names="arial" 
                        Font-Size="8pt" style="font-weight: 700" ForeColor="Black">STRUTTURE ASSOCIATE</asp:Label>
                            </td>
                            <td width="30%">
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td width="80%">
                <asp:datagrid id="DataGrid1" runat="server" Font-Names="Arial" 
                        AutoGenerateColumns="False" Font-Size="8pt" PageSize="4" 
                        Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" GridLines="Vertical" 
                        CellPadding="3" Width="100%" AllowPaging="True" BackColor="White" 
                                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px">
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                                BackColor="#000084" ForeColor="White" Height="1px"></HeaderStyle>
							<AlternatingItemStyle BackColor="#DCDCDC" />
							<Columns>
								<asp:BoundColumn DataField="ID" HeaderText="ID" 
                                    Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="ID_STRUTTURA" HeaderText="ID_STRUTTURA" 
                                    Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="DESCRIZIONE" HeaderText="NOME">
                                </asp:BoundColumn>
							    <asp:BoundColumn DataField="NUM_SPORTELLI" HeaderText="SPORTELLI">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NUM_OPERATORI" HeaderText="OPERATORI">
                                </asp:BoundColumn>
							</Columns>
							<ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
							<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" 
                                Mode="NumericPages"></PagerStyle>
						    <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
						</asp:datagrid>
                            </td>
                            <td width="20%" valign="middle">
                                &nbsp;&nbsp;&nbsp;
                                <asp:ImageButton ID="imgNuovaFiliale" runat="server" 
                                    ImageUrl="~/NuoveImm/Img_Aggiungi.png" onclientclick="NuovaFiliale();" />
                                    <br />
                                <br />
&nbsp;&nbsp;&nbsp;
                                    <img alt="Elimina Struttura selezionata" 
                        src="../NuoveImm/Img_Elimina.png" id="imgEliminafiliale" 
                        style="cursor:pointer" onclick="EliminaStruttura();"/>                                     <br />
                                <br />
&nbsp;&nbsp;&nbsp; <img alt="Calendario Festività Struttura" 
                        src="../NuoveImm/Img_CalendarioFStruttura.png" id="imgFStruttura" 
                        style="cursor:pointer" onclick="FesteStruttura();"/>
                            </td>
                        </tr>
                        <tr bgcolor="#FFFFCC">
                            <td width="70%">
                    <asp:Label ID="lblDescrBando1" runat="server" Font-Names="arial" 
                        Font-Size="8pt" style="font-weight: 700" ForeColor="Black">SPORTELLI/SEDI ASSOCIATI :</asp:Label>
                            </td>
                            <td width="30%" valign="top">
                                &nbsp;</td>
                        </tr>
                        <tr>
                       
                            <td width="80%">
                           
                <asp:datagrid id="DataGrid2" runat="server" Font-Names="Arial" 
                        AutoGenerateColumns="False" Font-Size="8pt" PageSize="4" 
                        Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" GridLines="Vertical" 
                        CellPadding="3" Width="100%" AllowPaging="True" BackColor="White" 
                                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px">
							<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                                BackColor="#000084" ForeColor="White"></HeaderStyle>
							<AlternatingItemStyle BackColor="#DCDCDC" />
							<Columns>
								<asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="ID_FILIALE" HeaderText="ID_FILIALE" Visible="False">
                                </asp:BoundColumn>
								<asp:BoundColumn DataField="DESCRIZIONE" HeaderText="NOME">
                                </asp:BoundColumn>
							    <asp:BoundColumn DataField="NUM_OPERATORI" HeaderText="OPERATORI">
                                </asp:BoundColumn>
							    <asp:BoundColumn DataField="PATRIMONIO" HeaderText="PATRIMONIO">
                                </asp:BoundColumn>
							</Columns>
							<ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" 
                                Mode="NumericPages" />
                            <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
						</asp:datagrid>
                            </td>
                            <td width="20%" valign="middle">
                                &nbsp;&nbsp;&nbsp;
                                <asp:ImageButton ID="imgNuovoSportello" runat="server" 
                                    ImageUrl="~/NuoveImm/Img_Aggiungi.png" onclientclick="NuovoSportello();" /><br />
                                <br />
                                &nbsp;&nbsp;&nbsp;
                                <asp:ImageButton ID="imgModificaSportello" runat="server" 
                                    ImageUrl="~/NuoveImm/Img_Modifica.png" onclientclick="ModificaSportello();" /><br />
                                <br />
&nbsp;&nbsp;&nbsp;
                                    <img alt="Elimina Sportello selezionato" 
                        src="../NuoveImm/Img_Elimina.png" id="imgEliminaSportello" 
                        style="cursor:pointer" onclick="EliminaSportello();"/>                                     <br />
                                <br />
&nbsp;&nbsp;&nbsp; <img alt="Calendario Festività Sportello" 
                        src="../NuoveImm/Img_CalendarioFSportello.png" id="imgFSportello" 
                        style="cursor:pointer" onclick="FesteSportello();"/>
                                                                <asp:ImageButton ID="btnCaricaSP" 
                                    runat="server" style="position:absolute; top: -100px; left: -100px;"
                        ImageUrl="~/NuoveImm/Img_Applica.png" />
                        <asp:ImageButton ID="btnEliminaFil" 
                                    runat="server" style="position:absolute; top: -100px; left: -100px;"
                        ImageUrl="~/NuoveImm/Img_Applica.png" />
                        <asp:ImageButton ID="btnCaricaOP1" 
                                    runat="server" style="position:absolute; top: -100px; left: -100px;"
                        ImageUrl="~/NuoveImm/Img_Applica.png" />
                        <asp:ImageButton ID="btnEliminaSportello" 
                                    runat="server" style="position:absolute; top: -100px; left: -100px;"
                        ImageUrl="~/NuoveImm/Img_Applica.png" />
                        <asp:ImageButton ID="btnEliminaOperatore" 
                                    runat="server" style="position:absolute; top: 517px; left: -100px;"
                        ImageUrl="~/NuoveImm/Img_Applica.png" />
                            </td>

                        </tr>
                        <tr bgcolor="#FFFFCC">
                       
                            <td width="80%">
                           
                    <asp:Label ID="lblDescrBando2" runat="server" Font-Names="arial" 
                        Font-Size="8pt" style="font-weight: 700" ForeColor="Black">OPERATORI ASSOCIATI :</asp:Label>
                            </td>
                            <td width="20%" valign="middle">
                                &nbsp;</td>

                        </tr>
                        <tr>
                       
                            <td width="80%">
                           
                <asp:datagrid id="DataGrid3" runat="server" Font-Names="Arial" 
                        AutoGenerateColumns="False" Font-Size="8pt" PageSize="4" 
                        Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" GridLines="Vertical" 
                        CellPadding="3" Width="100%" AllowPaging="True" BackColor="White" 
                                    BorderColor="#999999" BorderStyle="None" BorderWidth="1px">
							<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                                BackColor="#000084" ForeColor="White"></HeaderStyle>
							<AlternatingItemStyle BackColor="#DCDCDC" />
							<Columns>
								<asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                </asp:BoundColumn>
							    <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="NUMERO"></asp:BoundColumn>
							    <asp:BoundColumn DataField="PERIODO" HeaderText="PERIODO VALIDITA'">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="MATTINO" HeaderText="MATTINO">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="POMERIGGIO" HeaderText="POMERIGGIO">
                                </asp:BoundColumn>
							    <asp:BoundColumn DataField="GIORNI" HeaderText="GIORNI LAVORATI">
                                </asp:BoundColumn>
							</Columns>
							<ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
                            <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" 
                                Mode="NumericPages" />
                            <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
						</asp:datagrid>
                            </td>
                            <td width="20%" valign="middle">
                                &nbsp;&nbsp;&nbsp;
                                <asp:ImageButton ID="imgNuovoOperatore" runat="server" 
                                    ImageUrl="~/NuoveImm/Img_Aggiungi.png" onclientclick="NuovoOperatore();" /><br />
                                <br />
                                 &nbsp;&nbsp;&nbsp;
                                <asp:ImageButton ID="imgModificaOperatore" runat="server" 
                                    ImageUrl="~/NuoveImm/Img_Modifica.png" onclientclick="ModificaOperatore();" /><br />
                                <br />
&nbsp;&nbsp;&nbsp;
                                    <img alt="Elimina Operatore selezionato" 
                        src="../NuoveImm/Img_Elimina.png" id="imgEliminaOperatore" 
                        style="cursor:pointer" onclick="EliminaOperatore();"/>
                            </td>

                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblErrore" runat="server" Font-Names="arial" Font-Size="10pt" 
                        style="font-weight: 700" Visible="False"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                        <img alt="Calendario Festività Nazionale" 
                        src="../NuoveImm/Img_CalendarioNazionale.png" id="imgCalNazionale" 
                        style="position:absolute; top: 514px; left: 9px;cursor:pointer" onclick="CalendarioNazionale();"/>&nbsp;
                        <img alt="Calendario Festività Aziendale" 
                        src="../NuoveImm/Img_CalendarioAziendale.png" id="imgCalAziendale" 
                        style="position:absolute; top: 514px; left: 166px;cursor:pointer" onclick="CalendarioAziendale();" />
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
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        </div>
    <asp:HiddenField ID="LBLIDF" runat="server" Value="" />
    <asp:HiddenField ID="LBLIDS" runat="server" Value="" />
    <asp:HiddenField ID="LBLIDO" runat="server" Value="" />
    <asp:HiddenField ID="H1" runat="server" Value="" />
    <asp:HiddenField ID="H2" runat="server" Value="" />
    <asp:HiddenField ID="H3" runat="server" Value="" />
    <asp:HiddenField ID="idBando" runat="server" Value="" />
    <asp:HiddenField ID="lbldescrizione" runat="server" Value="" />
    <asp:HiddenField ID="lbldescrizione1" runat="server" Value="" />
    <asp:HiddenField ID="lbldescrizione2" runat="server" Value="" />

    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            
        </ContentTemplate>
        </asp:UpdatePanel>

                        <div id="ScriptMsg" title="Basic dialog" style="display: none; font-size: 10pt; font-family: Arial;
        width: 1000px">
    </div>
<div id="ScriptScelta" title="Finestra di Conferma" style="display: none; font-size: 10pt;
        font-family: Arial">
    </div>
    <script type="text/javascript" language="javascript">

        function EliminaOperatore() {
            if (document.getElementById('LBLIDO').value != '-1' && document.getElementById('LBLIDO').value != '') {
                document.getElementById('H3').value = '1';
                SceltaFunzioneOP('Eliminare l\'Operatore selezionato? Non sarà più possibile annullare questa operazione.');
            }
            else {
                document.getElementById('H3').value = '0';
                Messaggio('Selezionare uno Operatore dalla lista!');
            }
        }

        function ModificaOperatore() {
            if (document.getElementById('LBLIDO').value != '-1' && document.getElementById('LBLIDO').value != '') {
                document.getElementById('H3').value = '1';
                window.showModalDialog('NuovoOperatoreAU.aspx?T=1&IDO=' + document.getElementById('LBLIDO').value  + '&IDS=' + document.getElementById('LBLIDS').value, window, 'status:no;dialogWidth:750px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');
            }
            else {
                document.getElementById('H3').value = '0';
                alert('Selezionare un Operatore dalla lista!');
            }
        }

        function ModificaSportello() {
            if (document.getElementById('LBLIDS')) {
                if (document.getElementById('LBLIDS').value != '-1' && document.getElementById('LBLIDS').value != '') {
                    document.getElementById('H2').value = '1';
                    window.showModalDialog('NuovoSportello.aspx?T=1&IDS=' + document.getElementById('LBLIDS').value + '&IDF=' + document.getElementById('LBLIDF').value + '&AU=' + document.getElementById('idBando').value, window, 'status:no;dialogWidth:750px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');
                }
                else {
                    document.getElementById('H2').value = '0';
                    alert('Selezionare uno Sportello dalla lista!');
                }
            }
        }

        function EliminaSportello() {
            if (document.getElementById('LBLIDS').value != '-1' && document.getElementById('LBLIDS').value != '') {
                document.getElementById('H2').value = '1';
                SceltaFunzioneSP('Eliminare lo Sportello selezionato? Saranno eliminati anche i relativi operatori. Non sarà più possibile annullare questa operazione.');
            }
            else {
                document.getElementById('H2').value = '0';
                Messaggio('Selezionare uno Sportello dalla lista!');
            }
        }

        function EliminaStruttura() {
            if (document.getElementById('LBLIDF').value != '-1' && document.getElementById('LBLIDF').value != '') {
                document.getElementById('H1').value = '1';
                SceltaFunzione('Eliminare la Struttura selezionata? Saranno eliminati anche i relativi sportelli e operatori. Non sarà più possibile annullare questa operazione.');
            }
            else {
                document.getElementById('H1').value = '0';
                Messaggio('Selezionare una Struttura dalla lista!');
            }
        }

        function FesteStruttura() {
            if (document.getElementById('LBLIDF').value != '-1' && document.getElementById('LBLIDF').value != '') {
                window.showModalDialog('CalendarioF.aspx?IDS=0&IDO=0&IDF=' + document.getElementById('LBLIDF').value, window, 'status:no;dialogWidth:600px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');
            }
            else {
                Messaggio('Selezionare una Struttura dalla lista!');
            }
        }

        function FesteSportello() {
            if (document.getElementById('LBLIDS').value != '-1' && document.getElementById('LBLIDS').value != '') {
                window.showModalDialog('CalendarioF.aspx?IDF=0&IDO=0&IDS=' + document.getElementById('LBLIDS').value, window, 'status:no;dialogWidth:600px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');
            }
            else {
                Messaggio('Selezionare uno Sportello dalla lista!');
            }
        }

        function NuovaFiliale() {

            if (document.getElementById('idBando').value != '0') {
                document.getElementById('H1').value = '1';
                window.showModalDialog('NuovaFilialeAU.aspx', window, 'status:no;dialogWidth:600px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');
            }
            else {
                document.getElementById('H1').value = '0';
                alert('Nessuna Anagrafe Utenza Aperta!');
            }
        }

        function NuovoSportello() {
            if (document.getElementById('LBLIDF').value != '-1' && document.getElementById('LBLIDF').value != '') {
                window.showModalDialog('NuovoSportello.aspx?IDF=' + document.getElementById('LBLIDF').value + '&AU=' + document.getElementById('idBando').value, window, 'status:no;dialogWidth:750px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');
            }
            else {
                alert('Selezionare una Struttura dalla lista prima di inserire nuovi sportelli ed operatori!');
                
            }
        }

        function NuovoOperatore() {
        if (document.getElementById('LBLIDS').value != '-1' && document.getElementById('LBLIDS').value != '') {
            window.showModalDialog('NuovoOperatoreAU.aspx?IDS=' + document.getElementById('LBLIDS').value, window, 'status:no;dialogWidth:750px;dialogHeight:500px;dialogHide:true;help:no;scroll:no');
        }
        else {
            alert('Selezionare uno Sportello dalla lista prima di inserire nuovi operatori!');
            
        }
        }

        function CalendarioNazionale() {
            window.showModalDialog('CalendarioFesteNazionale.aspx', window, 'status:no;dialogWidth:600px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');
        }

        function CalendarioAziendale() {
            window.showModalDialog('CalendarioFesteAziendale.aspx', window, 'status:no;dialogWidth:600px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');
        }

        function SceltaFunzione(TestoMessaggio) {
            $(document).ready(function () {
                $('#ScriptScelta').text(TestoMessaggio);
                $('#ScriptScelta').dialog({ autoOpen: true, modal: true, show: 'blind', hide: 'explode', title: 'Attenzione', buttons: { 'Si': function () { __doPostBack('btnEliminaFil', ''); { $(this).dialog('close'); } }, 'No': function () { $(this).dialog('close'); " & Funzione2 & " } } });
            });
        }

        function SceltaFunzioneSP(TestoMessaggio) {
            $(document).ready(function () {
                $('#ScriptScelta').text(TestoMessaggio);
                $('#ScriptScelta').dialog({ autoOpen: true, modal: true, show: 'blind', hide: 'explode', title: 'Attenzione', buttons: { 'Si': function () { __doPostBack('btnEliminaSportello', ''); { $(this).dialog('close'); } }, 'No': function () { $(this).dialog('close'); " & Funzione2 & " } } });
            });
        }

        function SceltaFunzioneOP(TestoMessaggio) {
            $(document).ready(function () {
                $('#ScriptScelta').text(TestoMessaggio);
                $('#ScriptScelta').dialog({ autoOpen: true, modal: true, show: 'blind', hide: 'explode', title: 'Attenzione', buttons: { 'Si': function () { __doPostBack('btnEliminaOperatore', ''); { $(this).dialog('close'); } }, 'No': function () { $(this).dialog('close'); " & Funzione2 & " } } });
            });
        }

        function Messaggio(TestoMessaggio) {

            $(document).ready(function () {
                $('#ScriptMsg').text(TestoMessaggio);
                $('#ScriptMsg').dialog({ autoOpen: true, modal: true, show: 'blind', hide: 'explode', title: 'Attenzione...', buttons: { 'Ok': function () { $(this).dialog('close'); } } });
            });
        }
    </script>
    </form>
</body>
</html>
