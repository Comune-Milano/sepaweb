<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ListeConvocabili.aspx.vb" Inherits="ANAUT_ListeConvocabili" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    Uscita = 1;
    var Selezionato;
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
	<head>
    <link type="text/css" href="css/smoothness/jquery-ui-1.8.23.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.23.custom.min.js"></script>
    <script type="text/javascript" src="js/jquery.ui.datepicker-it.js"></script>
    <script type="text/javascript" src="js/jsfunzioni.js"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
		
	    <style type="text/css">
            #contenitore
            {
                top: 61px;
            }
        </style>
        <title>RisultatoRicercaD</title>
	</head>
	<body bgcolor="#f2f5f1">
		<form id="Form1" method="post" runat="server" >
            <asp:ScriptManager ID="ScriptManagerBando" runat="server">
    </asp:ScriptManager>
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td>
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco
                            Liste Convocabili </strong>
                        <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                        </span><br />
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
                        <asp:HiddenField ID="LBLID" runat="server" Value="0" />
                        <asp:HiddenField ID="npg" runat="server" Value="" />
                        <asp:HiddenField ID="H1" runat="server" Value="" />
                    </td>
                </tr>
            </table>
            &nbsp;<img alt="Torna alla pagina principale" 
                        src="../NuoveImm/Img_Home.png" id="imgEliminafiliale" 
                        
                style="cursor:pointer;left: 598px; position: absolute; top: 484px; height: 20px;" 
                onclick="PaginaHome();"/>
                <img alt="Conferma la lista selezionata" 
                        src="../NuoveImm/Img_Conferma.png" id="img1" 
                        
                style="cursor:pointer;left: 43px; position: absolute; top: 484px; height: 20px;" 
                onclick="Verifica();"/>
                <img alt="Elimina la lista selezionata" 
                        src="../NuoveImm/Img_Elimina_Grande.png" id="img2" 
                        
                style="cursor:pointer;left: 149px; position: absolute; top: 484px;" 
                onclick="Elimina();"/>
            <img alt="Escludi uno o più elemento dalla lista confermata" 
                        src="../NuoveImm/Img_Escludi.png" id="img3" 
                        
                style="cursor:pointer;left: 263px; position: absolute; top: 484px;" 
                onclick="Escludi();"/>
                <img alt="Includi uno o più elementi precedentemente esclusi" 
                        src="../NuoveImm/Img_Includi.png" id="img4" 
                        
                style="cursor:pointer;left: 361px; position: absolute; top: 484px;" 
                onclick="Includi();"/>
                <div id="contenitore" 
                
                
                
                
                style="position: absolute; width: 643px; height: 364px; left: 14px; overflow: auto;">
                <asp:datagrid id="DataGrid1" runat="server" Font-Names="Arial" 
                        AutoGenerateColumns="False" Font-Size="8pt" PageSize="13" 
                        
                        style="z-index: 105; left: 1px; position: absolute; top: 0px; width: 1082px;" 
                        Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" GridLines="None" 
                        CellPadding="4" ForeColor="#333333">
							<EditItemStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                                BackColor="#507CD1" ForeColor="White"></HeaderStyle>
							<AlternatingItemStyle BackColor="White" />
							<Columns>
								<asp:BoundColumn DataField="ID" HeaderText="id" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="VISUALIZZA"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DSTATO" HeaderText="STATO">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_ORA1" HeaderText="DATA ORA">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="AU" HeaderText="ANAGRAFE U."></asp:BoundColumn>
                                <asp:BoundColumn DataField="OPERATORE" HeaderText="OPERATORE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CRITERI" HeaderText="CRITERI"></asp:BoundColumn>
							</Columns>
							<ItemStyle BackColor="#EFF3FB" />
							<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
						    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
						</asp:datagrid>
                        </div>

                         <asp:ImageButton ID="btnEliminaLista" runat="server" 
                style="position:absolute; top: -100px; left: -100px;"/>
                <asp:ImageButton ID="btnConferma" runat="server" 
                style="position:absolute; top: -100px; left: -100px;"/>
            
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
        function PaginaHome() {
            document.location.href = 'pagina_home.aspx';
        }

        function Elimina() {
            if (document.getElementById('LBLID').value != '-1' && document.getElementById('LBLID').value != '') {
                document.getElementById('H1').value = '1';
                SceltaFunzioneOP('Sicuri di non voler confermare la lista selezionata?');
            }
            else {
                document.getElementById('H1').value = '0';
                Messaggio('Selezionare una lista!');

            }
        }

        function SceltaFunzioneOP(TestoMessaggio) {
            $(document).ready(function () {
                $('#ScriptScelta').text(TestoMessaggio);
                $('#ScriptScelta').dialog({ autoOpen: true, modal: true, show: 'blind', hide: 'explode', title: 'Attenzione', buttons: { 'Si': function () { __doPostBack('btnEliminaLista', ''); { $(this).dialog('close'); } }, 'No': function () { $(this).dialog('close'); " & Funzione2 & " } } });
            });
        }


        function Verifica() {
            if (document.getElementById('LBLID').value != '-1' && document.getElementById('LBLID').value != '') {
                document.getElementById('H1').value = '1';
                SceltaFunzioneOP1('Sicuri di voler confermare la lista selezionata?');
            }
            else {
                document.getElementById('H1').value = '0';
                Messaggio('Selezionare una lista!');

            }
        }

        function SceltaFunzioneOP1(TestoMessaggio) {
            $(document).ready(function () {
                $('#ScriptScelta').text(TestoMessaggio);
                $('#ScriptScelta').dialog({ autoOpen: true, modal: true, show: 'blind', hide: 'explode', title: 'Attenzione', buttons: { 'Si': function () { __doPostBack('btnConferma', ''); { $(this).dialog('close'); } }, 'No': function () { $(this).dialog('close'); " & Funzione2 & " } } });
            });
        }


        function Messaggio(TestoMessaggio) {

            $(document).ready(function () {
                $('#ScriptMsg').text(TestoMessaggio);
                $('#ScriptMsg').dialog({ autoOpen: true, modal: true, show: 'blind', hide: 'explode', title: 'Attenzione...', buttons: { 'Ok': function () { $(this).dialog('close'); } } });
            });
        }

        function Includi() {
            if (document.getElementById('LBLID').value != '-1' && document.getElementById('LBLID').value != '') {
                document.getElementById('H1').value = '1';
                window.open('DettagliListaEsclusi.aspx?ID=' + document.getElementById('LBLID').value, '', '');
            }
            else {
                document.getElementById('H1').value = '0';
                Messaggio('Selezionare una lista!');

            }
        }

        function Escludi() {
            if (document.getElementById('LBLID').value != '-1' && document.getElementById('LBLID').value != '') {
                document.getElementById('H1').value = '1';
                window.open('DettagliListaEscludibili.aspx?ID=' + document.getElementById('LBLID').value, '', '');
            }
            else {
                document.getElementById('H1').value = '0';
                Messaggio('Selezionare una lista!');

            }
        }

    </script>    
           
    </form>    

	</body>
    <script  language="javascript" type="text/javascript">
        //document.getElementById('dvvvPre').style.visibility = 'hidden';
        </script>
</html>
