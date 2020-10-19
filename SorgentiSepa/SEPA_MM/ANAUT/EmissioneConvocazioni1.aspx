<%@ Page Language="VB" AutoEventWireup="false" CodeFile="EmissioneConvocazioni1.aspx.vb" Inherits="ANAUT_EmissioneConvocazioni1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Selezionato;
    var Uscita;
    Uscita = 1;
    var tempo;
    tempo = '';
</script>
<html xmlns="http://www.w3.org/1999/xhtml">

	<head id="Head1" runat="server" >
       <script type="text/javascript" src="js/jsfunzioni.js"></script>
       <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
		<title>RisultatoRicercaD</title>
				
	    <style type="text/css">
            #contenitore
            {
                top: 61px;
            }
            .style1
            {
                width: 129px;
            }
        </style>
        
	</head>
	<body bgcolor="#f2f5f1">

		<form id="Form1" method="post" runat="server" >
            
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td>
                        <br />
                   <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>Emissioni 
                        Convocazioni AU-Scelta Modello/Sportello <asp:Label
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
                            <asp:TextBox ID="TextBox3" runat="server" Font-Bold="True" 
                            Font-Names="ARIAL" Font-Size="12pt"
                                Style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                                border-bottom: white 1px solid; left: -1px; top: 45px;" Width="100%">Nessuna Selezione</asp:TextBox>
                        <br />
                        <br />
                        <div id="ContenitoreSp" 
                            
                            
                            style="border: 1px solid #0000FF; position:absolute; width: 640px; height: 92px; top: 241px; left: 14px; visibility: visible; overflow: auto;">
                        <asp:CheckBoxList style="position:absolute; top: 19px; left: 5px;" 
                        ID="ListaVoci" runat="server" Font-Names="arial" 
                        Font-Size="9pt" >
                    </asp:CheckBoxList>
                            <asp:Label ID="Label5" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="10pt" Text="Seleziona Uno o più Sportelli dalla lista"></asp:Label>
                        </div>
                                
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                        <br />
                            <br />
                        <br />
                            <asp:Label ID="Label6" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="8pt" 
                            Text="Attenzione, si consiglia di selezionare uno sportello alla volta per limitare i tempi di produzione delle lettere!" 
                            ForeColor="#CC0000"></asp:Label>
                            <asp:Label ID="lblErrore" runat="server" Font-Bold="True" Font-Names="arial" 
                                Font-Size="8pt" 
                            Text="Attenzione, si consiglia di selezionare uno sportello alla volta per limitare i tempi di produzione delle lettere!" 
                            ForeColor="#CC0000" 
                            style="position:absolute; top: 491px; left: 144px; width: 316px;" 
                            Visible="False"></asp:Label>
                        <br />
                        <br />
                        <br />
                        <br />
                        <table style="width:100%;">
                            <tr>
                                <td class="style1">
                                    Data Stampa:</td>
                                <td>
                                    <asp:TextBox ID="txtDataStampa" runat="server" Width="70px"></asp:TextBox>
&nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataStampa"
                            Display="Dynamic" ErrorMessage="!!" Font-Bold="True" 
                Font-Names="arial" Font-Size="8pt"
                             
                            
                            
                
                
                                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))"></asp:RegularExpressionValidator>&nbsp;(Assicurarsi che sul modello sia presente il segnaposto $datastampa$</td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    Numero del protocollo</td>
                                <td>
                                    <asp:TextBox ID="txtNumPG" runat="server" Width="70px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style1">
                                    Stampa 
                                    in gruppi da</td>
                                <td valign="middle" >
                                    <asp:TextBox ID="txtNumPagine" runat="server" Width="70px"></asp:TextBox>
                                &nbsp;lettere (non valorizzare se si intendono stampare tutte le lettere)</td>
                            </tr>
                        </table>
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
                         <asp:HiddenField ID="H1" runat="server" Value="0" />
                        <asp:HiddenField ID="LBLID" runat="server" Value="0" />
                        <asp:HiddenField ID="nlettere" runat="server" Value="0" />
                    </td>
                </tr>
            </table>
            
            &nbsp;
                <img id="img2" alt="Esci" src="../NuoveImm/Img_Home.png" 
                            onclick="Esci()" 
                            
                style="position:absolute;cursor:pointer; top: 506px; left: 583px;"/>
                <img id="img1" alt="Esci" src="../NuoveImm/Img_Marcatori.png" 
                            onclick="ApriM()" 
                            
                
                style="position:absolute;cursor:pointer; top: 506px; left: 16px; right: 1402px;"/>
                                 <asp:ImageButton ID="btnEliminaModello" runat="server" ImageUrl="~/NuoveImm/img_EliminaModello.png"
                                            ToolTip="" 
                                             
                 style="position:absolute; top: -100px; left: -100px; " 
                 />
                <div id="contenitore" 
                
                
                
                
                style="border: 1px ridge #0000FF; position: absolute; width: 640px; height: 136px; left: 14px; overflow: auto;">
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
                                
                                if (document.getElementById('LBLID').value != '-1' && document.getElementById('LBLID').value != '0') {
                                    document.getElementById('H1').value = '1';
                                }
                                else {
                                    document.getElementById('H1').value = '0';
                                    //alert('Selezionare un elemento dalla lista!');
                                }
                            }

                           
                        </script>
                            
            <p>
                    <asp:ImageButton ID="imgSalva" runat="server" 
                        style="position:absolute; top: 507px; left: 482px;" 
                        ImageUrl="~/NuoveImm/Img_Procedi.png" TabIndex="3" 
                        onclientclick="VerificaSelezionato();" />
                    </p>
                    <script type="text/javascript">
                        //document.onkeydown = $onkeydown;


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


</script>
            </form>
                   
	</body>
</html>


