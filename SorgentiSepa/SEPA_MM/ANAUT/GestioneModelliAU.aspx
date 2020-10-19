<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestioneModelliAU.aspx.vb" Inherits="ANAUT_GestioneModelliAU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Selezionato;
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="../Contratti/jquery-1.8.2.js"></script>
<script type="text/javascript" src="../Contratti/jquery-impromptu.4.0.min.js"></script>
<script type="text/javascript" src="../Contratti/jquery.corner.js"></script>

	<head runat="server" >
    <link type="text/css" href="css/smoothness/jquery-ui-1.8.23.custom.css" rel="stylesheet" />
    <script type="text/javascript" src="js/jquery-1.8.0.min.js"></script>
    <script type="text/javascript" src="js/jquery-ui-1.8.23.custom.min.js"></script>
    <script type="text/javascript" src="js/jquery.ui.datepicker-it.js"></script>
    <script type="text/javascript" src="js/jsfunzioni.js"></script>
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
		<title>RisultatoRicercaD</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
	    <style type="text/css">
            #contenitore
            {
                top: 61px;
            }
        </style>
        
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
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                        Gestione Modelli Anagrafe Utenza <asp:Label
                                ID="Label4" runat="server" Text="DD"></asp:Label></strong>
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
                        <br />
                            <asp:TextBox ID="TextBox3" runat="server" Font-Bold="True" 
                            Font-Names="ARIAL" Font-Size="12pt"
                                Style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                border-bottom: white 1px solid; left: -1px; top: 45px;" Width="100%">Nessuna Selezione</asp:TextBox>
                        <br />
                        <asp:HiddenField ID="npg" runat="server" Value="" />
                         <br />
                         <asp:HiddenField ID="H1" runat="server" Value="0" />
                         <asp:HiddenField ID="LBLID" runat="server" Value="0" />
        
                    </td>
                </tr>
            </table>
            
            &nbsp;
                <img id="img2" alt="Esci" src="../NuoveImm/Img_Home.png" 
                            onclick="Esci()" 
                            
                style="position:absolute;cursor:pointer; top: 506px; left: 583px;"/>
                <img id="img1" alt="Esci" src="../NuoveImm/Img_Marcatori.png" 
                            onclick="ApriM()" 
                            
                style="position:absolute;cursor:pointer; top: 506px; left: 16px;"/>
                <asp:ImageButton ID="btnNuovo" runat="server" ImageUrl="~/NuoveImm/Img_NuovoModello.png"
                                            ToolTip="Aggiunge un nuovo modello" 
                                            OnClientClick="NuovoModello();" 
                 style="position:absolute; top: 505px; left: 167px;" />
                 <asp:ImageButton ID="btnElimina" runat="server" ImageUrl="~/NuoveImm/img_EliminaModello.png"
                                            ToolTip="Elimina il modello selezionato" 
                                             
                 style="position:absolute; top: 505px; left: 256px; right: 1085px;" 
                onclientclick="VerificaSelezionato();" />
                                 <asp:ImageButton ID="btnEliminaModello" runat="server" ImageUrl="~/NuoveImm/img_EliminaModello.png"
                                            ToolTip="" 
                                             
                 style="position:absolute; top: -100px; left: -100px; " 
                 />
                <div id="contenitore" 
                
                
                
                style="position: absolute; width: 640px; height: 331px; left: 14px; overflow: auto;">
                <asp:datagrid id="DataGrid1" runat="server" Font-Names="Arial" 
                        AutoGenerateColumns="False" Font-Size="8pt" PageSize="13" 
                        
                        style="z-index: 105; left: 1px; position: absolute; top: 0px; width: 614px;" 
                        Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" GridLines="None" 
                        CellPadding="4" ForeColor="#333333">
							<EditItemStyle BackColor="#2461BF" />
                            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                                BackColor="#507CD1" ForeColor="White"></HeaderStyle>
							<AlternatingItemStyle BackColor="White" />
							<Columns>
								<asp:BoundColumn DataField="ID" HeaderText="ID" 
                                    Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="DESCRIZIONE" HeaderText="DESCRIZIONE">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DESCR_BANDO" HeaderText="ANAGRAFE UTENZA">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NOTE" HeaderText="NOTE">
                                </asp:BoundColumn>
							    <asp:BoundColumn DataField="MODELLO1" HeaderText="MODELLO"></asp:BoundColumn>
							    <asp:BoundColumn DataField="TEST" HeaderText="TEST"></asp:BoundColumn>
							</Columns>
							<ItemStyle BackColor="#EFF3FB" />
							<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
						    <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
						</asp:datagrid>
                        </div>
                        <script language="javascript" type="text/javascript">
                            function Esci() {
                                document.location.href = 'pagina_home.aspx';
                            }

                            function ApriM() {
                                window.open('ElencoMarcatori.aspx', '', '');
                            }

                            function NuovoModello() {
                                window.showModalDialog('NuovoModelloAU.aspx?ID=-1', window, 'status:no;dialogWidth:600px;dialogHeight:480px;dialogHide:true;help:no;scroll:no');
                            }

                            function VerificaSelezionato() {

                                if (document.getElementById('LBLID').value != '-1' && document.getElementById('LBLID').value != '') {
                                    document.getElementById('H1').value = '1';
                                }
                                else {
                                    document.getElementById('H1').value = '0';
                                    
                                }
                            }

                           
                        </script>
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
            </form>
                   
	</body>
</html>

