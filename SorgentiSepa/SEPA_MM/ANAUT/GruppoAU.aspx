<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GruppoAU.aspx.vb" Inherits="ANAUT_GruppoAU" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
   
    var Selezionato;

  
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Creazione XML</title>
    <style type="text/css">
        #Contenitore
        {
            top: 148px;
            left: 18px;
        }
        #aspetta
        {
            left: 23px;
        }
    </style>
</head>
<script type="text/javascript">
    

    function Attendi() {
        //var win=null;
        //LeftPosition=(screen.width) ? (screen.width-250)/2 :0 ;
        //TopPosition=(screen.height) ? (screen.height-150)/2 :0;
        //LeftPosition=LeftPosition;
        //TopPosition=TopPosition;
        //parent.funzioni.aa=window.open('../loadXML.htm','','height=150,top='+TopPosition+',left='+LeftPosition+',width=250');
    }
</script>
<body bgcolor="#f2f5f1">
    <form id="form1" runat="server">
   
        &nbsp;&nbsp;&nbsp;&nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px">
            <tr>
                <td>
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                    Gruppo di Lavoro A.U.</strong></span><br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
                    <br />
                    <br />
                                        
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                        <asp:label id="Label1" runat="server" 
                Font-Size="X-Small" Font-Names="Arial" Font-Bold="True" 
                style="z-index: 110; left: 18px; position: absolute; top: 128px">Elenco Nominativi</asp:label>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="../NuoveImm/Img_Aggiungi.png"
            Style="z-index: 101; left: 598px; position: absolute; top: 154px; height: 12px;" 
            TabIndex="8" ToolTip="Home" onclientclick="Inserimento();" />
            <asp:ImageButton ID="imgElimina" runat="server" ImageUrl="../NuoveImm/Img_Elimina.png"
            Style="z-index: 101; left: 598px; position: absolute; top: 182px" 
            TabIndex="8" ToolTip="Home" onclientclick="ConfermaCancella()" />
            <asp:ImageButton ID="imgRegistro" runat="server" ImageUrl="../NuoveImm/Img_SpostaRegistro.png"
            Style="z-index: 101; left: 598px; position: absolute; top: 232px" 
            TabIndex="8" ToolTip="Home" onclientclick="ConfermaRegistro()" />
            <asp:label id="lblErrore" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                
            style="z-index: 110; left: 18px; position: absolute; top: 489px; width: 637px; height: 14px;" 
            ForeColor="#CC0000" Visible="False"></asp:label>
                        <div id="Contenitore" 
                                
                                style="border: 1px solid #990033; position: absolute; width: 573px; height: 335px; overflow: auto;">
                            <asp:datagrid id="DataGrid1" runat="server" Font-Names="Arial" 
                        AutoGenerateColumns="False" Font-Size="8pt" PageSize="13" 
                        style="z-index: 105; left: 1px; position: absolute; top: 0px; width: 1768px;" 
                                Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" GridLines="None" CellPadding="4" 
                                ForeColor="#333333">
							    <EditItemStyle BackColor="#2461BF" />
                                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                                    BackColor="#507CD1" ForeColor="White"></HeaderStyle>
							    <AlternatingItemStyle BackColor="White" />
							<Columns>
								<asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="cod" ReadOnly="True" 
                                    Visible="False"></asp:BoundColumn>
								<asp:BoundColumn DataField="INDIRIZZO_UNITA" HeaderText="ind" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CIVICO_UNITA" HeaderText="CIV" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="CAP_UNITA" HeaderText="CAP" Visible="False">
                                </asp:BoundColumn>
								<asp:BoundColumn DataField="FILIALE" HeaderText="fil" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="COMUNE_UNITA" HeaderText="COM" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="COGNOME" HeaderText="COG" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="NOME" HeaderText="NOM" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIP" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="DECORRENZA" HeaderText="DEC" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="SCADENZA" HeaderText="SCAD" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="PREVALENTE" HeaderText="REDDITO PREV." 
                                    ReadOnly="True" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PRESENZA_15" HeaderText="PRESENZA&lt;15" 
                                    ReadOnly="True" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PRESENZA_65" HeaderText="PRESENZA&gt;65" 
                                    ReadOnly="True" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="N_INV_100_CON" HeaderText="INVALIDI 100% IND." 
                                    ReadOnly="True" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="N_INV_100_SENZA" HeaderText="INVALIDI 100%" 
                                    ReadOnly="True" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="N_INV_66_99" HeaderText="INVALIDI 66-99%" 
                                    ReadOnly="True" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="IDC" HeaderText="IDC" Visible="False">
                                </asp:BoundColumn>
								<asp:BoundColumn DataField="IDAU" HeaderText="IDAU" Visible="False">
                                </asp:BoundColumn>
								<asp:TemplateColumn HeaderText="COD.CONTRATTO">
									<ItemTemplate>
										<asp:Label ID="Label2" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COD_CONTRATTO") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="PG AU">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox1" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.PG_AU") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.PG_AU") %>'></asp:Label>
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
                                <asp:TemplateColumn HeaderText="SEDE T.">
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
                                            Text='<%# DataBinder.Eval(Container, "DataItem.SCADENZA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText="REDDITO PREV.">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox2" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.PREVALENTE") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.PREVALENTE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="PRESENZA&lt;15">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.PRESENZA_15") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label4" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.PRESENZA_15") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="PRESENZA&gt;65">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox4" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.PRESENZA_65") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.PRESENZA_65") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INVALIDI 100% IND.">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox5" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.N_INV_100_CON") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.N_INV_100_CON") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INVALIDI 100%">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox6" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.N_INV_100_SENZA") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.N_INV_100_SENZA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INVALIDI 66-99%">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox7" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.N_INV_66_99") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="Label8" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.N_INV_66_99") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
							</Columns>
							    <ItemStyle BackColor="#EFF3FB" />
							<PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center"></PagerStyle>
						        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
						</asp:datagrid>
                        </div>
                        
                        </ContentTemplate>

                    </asp:UpdatePanel>
                    
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
                    <asp:HiddenField ID="IDC" runat="server" />
                    <asp:HiddenField ID="conferma" runat="server" />
                    <asp:HiddenField ID="IDAU" runat="server" />
                    <asp:HiddenField ID="modificato" runat="server" />
                    <br />
                    <br />
                    
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <asp:label id="Label10" runat="server" 
                Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                style="left: 18px; position: absolute; top: 82px">Nome del Gruppo</asp:label>
                <asp:TextBox ID="txtNomeGruppo" runat="server" 
                BorderStyle="Solid" BorderWidth="1px"
                            MaxLength="100" Style="left: 125px; position: absolute; top: 79px; width: 465px;"
                            TabIndex="1" ToolTip="Nome del gruppo"></asp:TextBox>
                            &nbsp;<asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 101; left: 588px; position: absolute; top: 508px" 
            TabIndex="8" ToolTip="Home" onclientclick="ConfermaUscita()" />
            <asp:ImageButton ID="imgSalva" runat="server" ImageUrl="~/NuoveImm/Img_SalvaGrande.png"
            Style="z-index: 101; left: 494px; position: absolute; top: 508px" 
            TabIndex="8" ToolTip="Salva" />
            
            <asp:ImageButton ID="imgExport" runat="server" ImageUrl="~/NuoveImm/Img_Export_Grande.png"
            Style="z-index: 101; left: 17px; position: absolute; top: 508px" 
            TabIndex="8" ToolTip="Export Elenco in excel" onclientclick="VerificaModifiche()" />
            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                        <div id="aspetta" 
                                
                                
                                
                                
                                
                                
                                
                                
                                
                                
                                
                                
                                style="position: absolute; width: 668px; height: 546px; top: 0px; left: 0px; background-image: url('../NuoveImm/SfondoMaschere.jpg'); background-repeat: no-repeat; z-index: 1000; background-color: #C0C0C0;">
                                <table style="width: 100%; height: 100%; top: 0px; left: 0px;">
                                <tr valign="middle">
                                <td align="center" height="100%">
                                <asp:Image ID="Image1" runat="server" ImageUrl="~/loading.gif" Width="17px" />
                                </td>
                                </tr>
                                </table>
                        
                        </div>
                            
                        </ProgressTemplate>

                    </asp:UpdateProgress>
            
        &nbsp;&nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp; &nbsp;&nbsp;
   
    <script type="text/javascript">
        function Inserimento() {
            if (document.getElementById('modificato').value == '1') {
                var sicuro = window.confirm('Attenzione...sono state effettuate delle modifiche. Vuoi continuare senza salvare? Le modifiche potrebbero non essere visibili.');
                if (sicuro == true) {
                    window.showModalDialog('RicercaApplicazioneAU0.aspx', 'window', 'status:no;dialogWidth:670px;dialogHeight:550px;dialogHide:true;help:no;scroll:no');
                }
            }
            else {
                window.showModalDialog('RicercaApplicazioneAU0.aspx', 'window', 'status:no;dialogWidth:670px;dialogHeight:550px;dialogHide:true;help:no;scroll:no');
            }

        }

        function ConfermaCancella() {
            if (document.getElementById('IDC').value != '') {
                var sicuro = window.confirm('Sei sicuro di voler eliminare dal gruppo di lavoro l\'elemento selezionato?');
                if (sicuro == true) {
                    document.getElementById('conferma').value = '1';
                }
                else {
                    document.getElementById('conferma').value = '0';
                }
            }
            else {
                alert('Selezionare una riga!');
                document.getElementById('conferma').value = '0';
            }
        }




        function ConfermaRegistro() {
        if (document.getElementById('IDC').value != '') {
            var sicuro = window.confirm('Sei sicuro di voler spostare l\'elemento selezionato nel registro generale per future simulazioni e/o applicazione dell\'Anagrafe?');
            if (sicuro == true) {
                document.getElementById('conferma').value = '1';
            }
            else {
                document.getElementById('conferma').value = '0';
            }
        }
        else {
            alert('Selezionare una riga!');
            document.getElementById('conferma').value = '0';
        }
        }


        function VerificaModifiche() {
            if (document.getElementById('modificato').value == '1') {
                alert('Attenzione...sono state effettuate delle modifiche! Salvare prima di proseguire!');
                document.getElementById('conferma').value = '0';
            }
            else {
                document.getElementById('conferma').value = '1';
            }
        }

        function ConfermaUscita() {
            if (document.getElementById('modificato').value == '1') {
                var sicuro = window.confirm('Sono state effettuate delle modifiche. Sei sicuro di voler uscire senza salvare?');
                if (sicuro == true) {
                    document.getElementById('conferma').value = '1';
                }
                else {
                    document.getElementById('conferma').value = '0';
                }
            }
            else {
                document.getElementById('conferma').value = '1';
            }
        }


    </script>
        
     
   
    </form>
    </body>
</html>
