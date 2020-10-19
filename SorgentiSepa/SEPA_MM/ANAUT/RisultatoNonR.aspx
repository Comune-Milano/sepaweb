<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatoNonR.aspx.vb" Inherits="ANAUT_RisultatoNonR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="../Contratti/jquery-1.8.2.js"></script>
<script type="text/javascript" src="../Contratti/jquery-impromptu.4.0.min.js"></script>
<script type="text/javascript" src="../Contratti/jquery.corner.js"></script>

	<head>
    <link rel="stylesheet" type="text/css" href="impromptu.css" />
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
        <script type="text/javascript">
            function Conta() {
                var contatore;
                contatore = 0;
                re = new RegExp(':' + document.getElementById('ChSelezionato') + '$')  //generated control
                for (i = 0; i < document.forms[0].elements.length; i++) {
                    elm = document.forms[0].elements[i]
                    if (elm.type == 'checkbox') {
                        if (elm.checked == true) {
                            contatore = contatore + 1;
                        }
                    }
                }
                if (document.all) {
                    document.getElementById('lblSelezionati').innerText = contatore;
                }
                else {
                    document.getElementById('lblSelezionati').textContent = contatore;
                }
               
            }
        </script>
	</head>
	<body bgColor="#f2f5f1">
		<form id="Form1" method="post" runat="server" >
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td>
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco
                            Non Rispondenti Un.Reali </strong>
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
                        <img id="imgAvanti" alt="Procedi" src="../NuoveImm/Img_Procedi.png" 
                            onclick="Conferma()" 
                            style="position:absolute;cursor:pointer; top: 506px; left: 364px;"/><br />
                        <br />
                        <br />
                        <asp:HiddenField ID="LBLID" runat="server" Value="0" />
                        <asp:HiddenField ID="npg" runat="server" Value="" />
                        
                    </td>
                </tr>
            </table>
            &nbsp;<asp:ImageButton ID="btnAnnulla" 
                runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 101; left: 598px; position: absolute; top: 506px; height: 20px;" 
                ToolTip="Home" />
                            <asp:ImageButton ID="btnSelezionaTutti" runat="server" ImageUrl="~/NuoveImm/Img_SelezionaTuttiGrande.png"
                Style="z-index: 102; left: 155px; position: absolute; top: 506px" 
                ToolTip="Seleziona Tutti" />
                <asp:label id="Label1" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True"                 
                style="z-index: 106; left: 14px; position: absolute; top: 436px; width: 130px; height: 16px; right: 1426px;" 
                ForeColor="Black">Elementi Selezionati:</asp:label>
                <asp:label id="lblSelezionati" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                
                style="z-index: 106; left: 139px; position: absolute; top: 436px; width: 97px; height: 16px; right: 1301px;" 
                ForeColor="Black">0</asp:label>
                <asp:label id="lblAvviso" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                
                style="z-index: 106; left: 15px; position: absolute; top: 465px; width: 635px; height: 31px; right: 887px;" 
                ForeColor="#CC0000">ATTENZIONE...si consiglia di effettuare la ricerca e la creazione delle diffide su un numero non elevato di soggetti per evitare lunghe attese durante la fase di generazione delle lettere.</asp:label>
                <asp:ImageButton ID="btnDeselezionaTutti" runat="server" ImageUrl="~/NuoveImm/Img_DeSelezionaTutti.png"
                Style="z-index: 102; left: 14px; position: absolute; top: 506px" 
                ToolTip="Deseleziona tutti" />
            <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
                Style="z-index: 103; left: 460px; position: absolute; top: 506px" 
                ToolTip="Nuova Ricerca" />
                <asp:ImageButton ID="imgExport" runat="server" ImageUrl="~/NuoveImm/Img_Export.png"
                Style="z-index: 103; left: 277px; position: absolute; top: 506px" 
                ToolTip="Export in excel" />
                <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                Style="z-index: 103; left: 277px; position: absolute; top: 518px" 
                ToolTip="Procedi con la stampa" />
            <asp:label id="LBLPROGR" 
                
                style="Z-INDEX: 104; LEFT: 102px; POSITION: absolute; TOP: 499px; width: 25px;" 
                runat="server" Height="23px" Visible="False">Label</asp:label>
                <div id="contenitore" 
                
                
                style="position: absolute; width: 640px; height: 364px; left: 14px; overflow: auto;">
                <asp:datagrid id="DataGrid1" runat="server" Font-Names="Arial" 
                        AutoGenerateColumns="False" Font-Size="8pt" PageSize="13" 
                        style="z-index: 105; left: 1px; position: absolute; top: 0px; width: 1050px;" 
                        BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" GridLines="None">
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" BackColor="#F2F5F1" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChSelezionato" runat="server" onclick="Conta();"/>
                                        <asp:Label runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
								<asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="cod" ReadOnly="True" 
                                    Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="FILIALE" HeaderText="fil" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="IDC" HeaderText="IDC" Visible="False">
                                </asp:BoundColumn>
								<asp:TemplateColumn HeaderText="COD.CONTRATTO">
									<ItemTemplate>
										<asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COD_CONTRATTO") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INDIRIZZO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.INDIRIZZO_UNITA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CIVICO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.CIVICO_UNITA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CAP">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.CAP_UNITA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="SEDE T.">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.FILIALE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="COMUNE">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COMUNE_UNITA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText="COGNOME">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="NOME">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="TIPOLOGIA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.TIPOLOGIA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DECORRENZA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.DECORRENZA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="SCADENZA">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.SLOGGIO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
							</Columns>
							<PagerStyle Mode="NumericPages"></PagerStyle>
						</asp:datagrid>
                        </div>
            </form>
                   <script  language="javascript" type="text/javascript">



                       document.getElementById('btnProcedi').style.visibility = 'hidden';
                       document.getElementById('btnProcedi').style.position = 'absolute';
                       document.getElementById('btnProcedi').style.left = '-100px';
                       document.getElementById('btnProcedi').style.display = 'none';


                   document.getElementById('dvvvPre').style.visibility = 'hidden';

                   var txt = 'Inserisci il numero di Protocollo:<br /><input type="text" id="alertName" name="alertName" value="" />';
	


                    function mycallbackform(e,v,m,f){
                        if (v != undefined)
                            document.getElementById('npg').value = f.alertName;
                        document.getElementById('LBLID').value = '1';
                        document.getElementById('btnProcedi').click();
                        return true;
                    }

                    

                       function Conferma() {
                           var chiediConferma
                           chiediConferma = window.confirm("Attenzione...Sei sicuro di voler procedere con la stampa delle diffide?");
                           if (chiediConferma == false) {
                               document.getElementById('LBLID').value = '0';
                           }
                           else {
                               jQuery.prompt(txt, {
                                   callback: mycallbackform,
                                   buttons: { Conferma: 'Conferma', Annulla: 'Annulla' }
                               });
                               
                           }
                       }

                       Conta();

    </script> 
	</body>
</html>
