<%@ Page Language="VB" AutoEventWireup="false" CodeFile="VerificaS.aspx.vb" Inherits="ANAUT_VerificaS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>

<head runat="server">
    <title>Verifica Simulazione</title>

    <script type="text/javascript">

      
        </script>

</head>
<body>
    <form id="form1" runat="server">
<table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
                position: absolute; top: 0px">
                <tr>
                    <td>
                        <br />
                        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                        Rapporti ABUSIVI in Corso con canone da Adeguare&nbsp; </strong>
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
                        
                    </td>
                </tr>
            </table>
                <img id="img1" alt="Esci" src="../NuoveImm/Img_Home.png" 
                            onclick="Esci()" 
                            
                style="position:absolute;cursor:pointer; top: 506px; left: 583px;"/>
                <img id="imgAvanti" alt="Procedi" src="../NuoveImm/Img_CreaNuovoGruppo.png" 
                            onclick="Conferma()" 
                            
                style="position:absolute;cursor:pointer; top: 506px; left: 401px;"/>
                <div id="contenitore" 
                
        
        style="position: absolute; width: 640px; height: 364px; left: 14px; overflow: auto; top: 65px;">
                <asp:datagrid id="DataGrid1" runat="server" Font-Names="Arial" 
                        AutoGenerateColumns="False" Font-Size="8pt" PageSize="15" 
                        
                        style="z-index: 105; left: 1px; position: absolute; top: 0px; width: 1009px;" 
                        Font-Bold="False" Font-Italic="False" Font-Overline="False" 
                        Font-Strikeout="False" Font-Underline="False" GridLines="Vertical" 
                        CellPadding="3" AllowPaging="True" BackColor="White" BorderColor="#999999" 
                        BorderStyle="None" BorderWidth="1px">
                            <FooterStyle BackColor="#CCCCCC" ForeColor="Black" />
							<HeaderStyle Font-Size="8pt" Font-Names="Arial" Font-Bold="True" 
                                BackColor="#000084" ForeColor="White"></HeaderStyle>
							<AlternatingItemStyle BackColor="#DCDCDC" />
							<Columns>
								<asp:BoundColumn DataField="IDC" HeaderText="IDAU" Visible="False">
                                </asp:BoundColumn>
								<asp:TemplateColumn HeaderText="COD.CONTRATTO">
									<ItemTemplate>
										<asp:Label ID="Label2" runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COD_CONTRATTO") %>'></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DECORRENZA">
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.DECORRENZA") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.DECORRENZA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="INDIRIZZO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.INDIRIZZO_UNITA") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.INDIRIZZO_UNITA") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CIVICO">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.CIVICO_UNITA") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.CIVICO_UNITA") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="CAP">
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.CAP_UNITA") %>'></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.CAP_UNITA") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
							    <asp:TemplateColumn HeaderText="COMUNE">
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COMUNE_UNITA") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COMUNE_UNITA") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="COGNOME">
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="NOME">
                                    <EditItemTemplate>
                                        <asp:TextBox runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'></asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label runat="server" 
                                            Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
							</Columns>
							<ItemStyle BackColor="#EEEEEE" ForeColor="Black" />
							<PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" 
                                Mode="NumericPages"></PagerStyle>
						    <SelectedItemStyle BackColor="#008A8C" Font-Bold="True" ForeColor="White" />
						</asp:datagrid>
                        </div>
                        <div id="divSalva" 
                
                
        
        
        
        
        style="position: absolute; z-index: 500; width: 678px; height: 528px; top: 2px; left: 0px; background-image: url('../ImmDiv/SfondoDim3.jpg'); background-repeat: no-repeat; visibility: visible;" >
                        
                            <table style="width:70%; position:absolute;top:129px; left: 90px; height: 131px;">
                                <tr>
                                    <td>
                                        <asp:Label ID="Label13" runat="server" Font-Names="arial" Font-Size="10pt" 
                                            Text="Si sta per creare un nuovo Gruppo di Lavorazione con le dichiarazioni selezionate. Inserire il nome del Gruppo."></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtNomeGruppo" runat="server" BorderStyle="Solid" 
                                            BorderWidth="1px" MaxLength="100" Width="401px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtSalvataggio" runat="server" BorderStyle="Solid" 
                                            BorderWidth="0px" MaxLength="100" Width="401px" ForeColor="Red">Attendere...Salvataggio in corso...</asp:TextBox></td>
                                </tr>
                                <tr align="right">
                                    <td>
                                        &nbsp;<asp:ImageButton 
                                            ID="ImageButton1" runat="server" ImageUrl="~/NuoveImm/Img_SalvaVal.png" style="cursor:pointer"
                                            onclientclick="Elabora();" />
                                        &nbsp;&nbsp;
                                        <img id="imgAnnulla" alt="" src="../NuoveImm/Img_AnnullaVal.png" 
                                            onclick="javascript:myOpacity.toggle();" style="cursor: pointer"/></td>
                                </tr>
                            </table>
                        
                        </div>
                        <script  language="javascript" type="text/javascript">

                            myOpacity = new fx.Opacity('divSalva', { duration: 200 });
                            myOpacity.hide();

                            document.getElementById('dvvvPre').style.visibility = 'hidden';
                            document.getElementById('txtSalvataggio').style.visibility = 'hidden';


                            function Esci() {
                                document.location.href = 'pagina_home.aspx';
                            }



                            function Conferma() {

                                    myOpacity.toggle();
                              

                            }

                            function Elabora() {
                                //myOpacity.toggle;
                                //document.getElementById('dvvvPre1').style.visibility = 'visible';
                                document.getElementById('ImageButton1').style.visibility = 'hidden';
                                document.getElementById('ImageButton1').style.position = 'absolute';
                                document.getElementById('ImageButton1').style.left = '-100px';
                                document.getElementById('ImageButton1').style.display = 'none';

                                document.getElementById('imgAnnulla').style.visibility = 'hidden';
                                document.getElementById('imgAnnulla').style.position = 'absolute';
                                document.getElementById('imgAnnulla').style.left = '-100px';
                                document.getElementById('imgAnnulla').style.display = 'none';

                                document.getElementById('txtSalvataggio').style.visibility = 'visible';                             
                            }

                            

                       

    </script> 
    </form>
</body>
</html>
