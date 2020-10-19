<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatoIncompleti.aspx.vb" Inherits="ANAUT_RisultatoIncompleti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    var Selezionato;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="../Contratti/jquery-1.8.2.js"></script>
<script type="text/javascript" src="../Contratti/jquery-impromptu.4.0.min.js"></script>
<script type="text/javascript" src="../Contratti/jquery.corner.js"></script>

	<head>
    <link rel="stylesheet" type="text/css" href="impromptu.css" />
		<title>Incomplete</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
	    <style type="text/css">
            #contenitore
            {
                top: 53px;
            }
            .style1
            {
                font-family: Arial;
                font-size: xx-small;
            }
            .style2
            {
                font-family: Arial, Helvetica, sans-serif;
                font-size: xx-small;
            }
            .style3
            {
                font-family: Arial;
                font-size: xx-small;
                font-weight: bold;
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
	</head>
	<body bgcolor="#f2f5f1">
		<form id="Form1" method="post" runat="server" >
            &nbsp;&nbsp;
            <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td>
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; AU 
                        Sospese per Doc. Incompleta </strong>
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
                        <br />
                        <asp:HiddenField ID="LBLID" runat="server" Value="0" />
                        <asp:HiddenField ID="LBLIDmodello" runat="server" Value="0" />
                        <asp:HiddenField ID="npg" runat="server" Value="" />
                        
                    </td>
                </tr>
            </table>
            &nbsp;<asp:ImageButton ID="btnAnnulla" 
                runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                Style="z-index: 101; left: 598px; position: absolute; top: 506px; height: 20px;" 
                ToolTip="Home" />
                            <asp:ImageButton ID="btnSelezionaTutti" runat="server" ImageUrl="~/NuoveImm/Img_SelezionaTuttiGrande.png"
                Style="z-index: 102; left: 421px; position: absolute; top: 258px" 
                ToolTip="Seleziona Tutti" />
                <asp:label id="Label1" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True"                 
                style="z-index: 106; left: 14px; position: absolute; top: 261px; width: 130px; height: 16px; right: 1225px;" 
                ForeColor="Black">Elementi Selezionati:</asp:label>
                <asp:label id="Label13" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True"                 
                style="z-index: 106; left: 14px; position: absolute; top: 286px; width: 630px; height: 16px; right: 725px;" 
                ForeColor="#CC0000" BackColor="#FFFFCC">SCELTA MODELLO:</asp:label>
                <div id="Div1" 
                
                
                
                
                
                
                
                style="border: 1px ridge #0000FF; position: absolute; width: 629px; height: 97px; left: 16px; overflow: auto; top: 306px;">
                <asp:datagrid id="DataGrid2" runat="server" Font-Names="Arial" 
                        AutoGenerateColumns="False" Font-Size="8pt" PageSize="13" 
                        
                        style="z-index: 105; left: 1px; position: absolute; top: 0px; width: 614px;" 
                        Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" GridLines="Vertical" 
                        CellPadding="3" BackColor="White" BorderColor="#999999" BorderStyle="None" 
                        BorderWidth="1px">
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                                BackColor="#000084" ForeColor="White"></HeaderStyle>
							<AlternatingItemStyle BackColor="#DCDCDC" />
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
							<ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
							<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" 
                                Mode="NumericPages"></PagerStyle>
						    <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
						</asp:datagrid>
                        </div>
                        <asp:TextBox ID="TextBox3" runat="server" Font-Bold="True" 
                            Font-Names="ARIAL" Font-Size="8pt"
                                
                
                
                Style="border: 1px solid white; position:absolute; left: 14px; top: 410px; width: 41%;">Nessuna Selezione</asp:TextBox>
                <table style="width:627px; position: absolute; height: 39px; top: 438px; left: 17px;" 
                cellpadding="0" cellspacing="0">
                            <tr>
                                <td class="style1" colspan="0">
                                    Data Stampa:</td>
                                <td colspan="0">
                                    <asp:TextBox ID="txtDataStampa" runat="server" Width="70px" CssClass="style2"></asp:TextBox>
&nbsp;<asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtDataStampa"
                            Display="Dynamic" ErrorMessage="!!" Font-Bold="True" 
                Font-Names="arial" Font-Size="8pt"
                             
                            
                            
                
                
                                        
                                        ValidationExpression="(((0[1-9]|[12][0-9]|3[01])([/])(0[13578]|10|12)([/])(\d{4}))|(([0][1-9]|[12][0-9]|30)([/])(0[469]|11)([/])(\d{4}))|((0[1-9]|1[0-9]|2[0-8])([/])(02)([/])(\d{4}))|((29)(\.|-|\/)(02)([/])([02468][048]00))|((29)([/])(02)([/])([13579][26]00))|((29)([/])(02)([/])([0-9][0-9][0][48]))|((29)([/])(02)([/])([0-9][0-9][2468][048]))|((29)([/])(02)([/])([0-9][0-9][13579][26])))" 
                                        CssClass="style2"></asp:RegularExpressionValidator></td>
                                <td class="style1">
                                    Numero del protocollo</td>
                                <td>
                                    <asp:TextBox ID="txtNumPG" runat="server" Width="70px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td class="style3">
                                    Stai effettuando una simulazione?</td>
                                <td valign="middle" >
                                    <asp:DropDownList ID="cmbSimulazione" runat="server" Font-Names="arial" 
                                        Font-Size="8pt">
                                        <asp:ListItem>SI</asp:ListItem>
                                        <asp:ListItem>NO</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                                <td valign="middle" class="style1" >
                                    </td>
                                <td valign="middle" >
                                    &nbsp;</td>
                            </tr>
                        </table>
                <asp:label id="lblSelezionati" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                
                style="z-index: 106; left: 139px; position: absolute; top: 261px; width: 97px; height: 16px; right: 1133px;" 
                ForeColor="Black">0</asp:label>
                <asp:ImageButton ID="btnDeselezionaTutti" runat="server" ImageUrl="~/NuoveImm/Img_DeSelezionaTutti.png"
                Style="z-index: 102; left: 276px; position: absolute; top: 258px" 
                ToolTip="Deseleziona tutti" />
            <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
                Style="z-index: 103; left: 224px; position: absolute; top: 506px" 
                ToolTip="Nuova Ricerca" />
                <asp:ImageButton ID="imgExport" runat="server" ImageUrl="~/NuoveImm/Img_Export.png"
                Style="z-index: 103; left: 562px; position: absolute; top: 258px" 
                ToolTip="Export in excel" />
                <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                Style="z-index: 103; left: 277px; position: absolute; top: 518px" 
                ToolTip="Procedi con la stampa" />
            <asp:label id="LBLPROGR" 
                
                style="Z-INDEX: 104; LEFT: 102px; POSITION: absolute; TOP: 499px; width: 25px;" 
                runat="server" Height="23px" Visible="False">Label</asp:label>
                <div id="contenitore" 
                
                
                
                
                style="border: 1px ridge #0000FF; position: absolute; width: 633px; height: 199px; left: 14px; overflow: auto;">
                <asp:datagrid id="DataGrid1" runat="server" Font-Names="Arial" 
                        AutoGenerateColumns="False" Font-Size="8pt" PageSize="13" 
                        style="z-index: 105; left: 1px; position: absolute; top: 0px; width: 1050px; " 
                        BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" GridLines="Vertical" 
                        BorderColor="#999999" BorderStyle="None" BorderWidth="1px" CellPadding="0">
							<FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                                BackColor="#000084" ForeColor="White"></HeaderStyle>
							<AlternatingItemStyle BackColor="#DCDCDC" />
							<Columns>
								<asp:TemplateColumn>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="ChSelezionato" runat="server" onclick="Conta();"/>
                                        <asp:Label ID="Label1" runat="server"></asp:Label>
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
										<asp:Label ID="Label2" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COD_CONTRATTO") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="PG AU">
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INDIRIZZO">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.INDIRIZZO_UNITA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CIVICO">
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.CIVICO_UNITA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CAP">
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.CAP_UNITA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="SPORTELLO">
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.FILIALE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="COMUNE">
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COMUNE_UNITA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText="COGNOME">
                                    <ItemTemplate>
                                        <asp:Label ID="Label8" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="NOME">
                                    <ItemTemplate>
                                        <asp:Label ID="Label9" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="TIPOLOGIA">
                                    <ItemTemplate>
                                        <asp:Label ID="Label10" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.TIPOLOGIA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DECORRENZA">
                                    <ItemTemplate>
                                        <asp:Label ID="Label11" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.DECORRENZA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="SCADENZA">
                                    <ItemTemplate>
                                        <asp:Label ID="Label12" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.SLOGGIO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
							</Columns>
							<ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
							<PagerStyle Mode="NumericPages" BackColor="#999999" ForeColor="Black" 
                                HorizontalAlign="Center"></PagerStyle>
						    <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
						</asp:datagrid>
                        </div>
            </form>
                   <script  language="javascript" type="text/javascript">



                       document.getElementById('btnProcedi').style.visibility = 'hidden';
                       document.getElementById('btnProcedi').style.position = 'absolute';
                       document.getElementById('btnProcedi').style.left = '-100px';
                       document.getElementById('btnProcedi').style.display = 'none';


                       document.getElementById('dvvvPre').style.visibility = 'hidden';
                       

                       function Conferma() {
                           var chiediConferma
                           chiediConferma = window.confirm("Attenzione...Sei sicuro di voler procedere con la stampa delle diffide per incompletezza?");
                           if (chiediConferma == false) {
                               document.getElementById('LBLID').value = '0';
                           }
                           else {
                               document.getElementById('LBLID').value = '1';
                               document.getElementById('btnProcedi').click();

                           }
                       }

                       Conta();

    </script> 
	</body>
</html>

