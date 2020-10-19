<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SelezionaUnitaCON.aspx.vb" Inherits="Contratti_SelezionaUnitaCON" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var popupWindow;
</script>
<script type="text/javascript">
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Unità</title>
</head>
<body>
    <form id="form1" runat="server" defaultbutton="ImgProcedi" defaultfocus="ImgProcedi">
    <div>
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
            width: 800px; position: absolute; top: 0px">
            <tr>
                <td style="width: 800px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp;Seleziona
                        Unità per stipula rapporto a canone Convenzionato</strong></span><br />
                    <br />
                    <br />
                    <div id="contenitore" 
                        
                        style="position: absolute; width: 770px; height: 393px; top: 77px; overflow: auto">
                    <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="false" AutoGenerateColumns="False"
                        Font-Names="Arial" Font-Size="8pt" HorizontalAlign="Left" Style="z-index: 121;
                        left: 0px; position: absolute; top: 0px" Width="750px" TabIndex="2" 
                        BorderWidth="0px">
                        <PagerStyle Mode="NumericPages" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_ALLOGGIO" HeaderText="COD" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="PROPRIETA" HeaderText="PROPRIETA" ReadOnly="True" Visible="False">
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="CODICE">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_ALLOGGIO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ZONA">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ZONA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="INDIR.">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TIPO_VIA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText=" ">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INDIRIZZO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="CIV.">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NUM_CIVICO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="N.ALL">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.N_ALL") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="LOC.">
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NUM_LOCALI") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="PIANO">
                                <ItemTemplate>
                                    <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PIANO") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ASC.">
                                <ItemTemplate>
                                    <asp:Label ID="Label9" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ELEVATORE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="SUP.">
                                <ItemTemplate>
                                    <asp:Label ID="Label10" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SUP") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DISP.">
                                <ItemTemplate>
                                    <asp:Label ID="Label11" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_DISPONIBILITA1") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="PROPR.">
                                <ItemTemplate>
                                    <asp:Label ID="Label12" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PROPRIETA1") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton3" runat="server" CommandName="Cancel" ImageUrl="~/NuoveImm/Abbina_Foto.png"
                                        ToolTip="Dettagli Unità, Foto e Planimetrie" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton2" runat="server" CommandName="Edit" ImageUrl="~/NuoveImm/Abbina_Seleziona.png"
                                        ToolTip="Seleziona questa Unità Immobiliare" OnClientClick="Attendi();" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <HeaderStyle BackColor="PapayaWhip" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" />
                    </asp:DataGrid>
                    </div>
                    <br />
                    <br />
                    <br />
                    &nbsp;&nbsp;<br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp; &nbsp;
                    <br />
                    <br />
                    <br />
                    <br />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;<br />
                    &nbsp;
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <asp:ImageButton ID="ImgProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                        Style="left: 616px; position: absolute; top: 515px" />
                    <br />
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                        Style="z-index: 101; left: 712px; position: absolute; top: 515px" 
                        ToolTip="Home" />
                    <br />
                    <br />
                </td>
            </tr>
        </table>
        <asp:Label ID="LBLID" runat="server" Style="z-index: 122; left: 30px;
            position: absolute; top: 524px; height: 18px;" Visible="False" 
            Width="78px"></asp:Label>
        <asp:Label ID="Label9" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
            Style="z-index: 124; left: 11px; position: absolute; top: 481px; text-align: left"
            Text="Nessuna Selezione" Width="767px"></asp:Label>
    
    </div>

    <div id="splash"
        
        style="position :absolute; z-index :500; text-align:center; font-size:10px; width: 100%; height: 95%; vertical-align: top; line-height: normal; top: 0px; left: 0px; background-color:#FFFFFF; visibility: hidden;">
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
            <img src='../Immagini/load.gif' alt='Verifica in corso'/><br/><br/>
            Verifica in corso...<br />
            <br />
            <br />
            <br />
            <br />
            <br />
            <br />
            &nbsp;</div> 

   <img id="imgCambiaAmm" alt="Ricerca Rapida" onclick="cerca();" 
        src="Immagini/Search_16x16.png" 
        style="position: absolute; top: 49px; left: 759px; cursor: pointer" />
    
        <script type="text/javascript">
            function cerca() {
                if (document.all) {
                    finestra = showModelessDialog('Find.htm', window, 'dialogLeft:0px;dialogTop:0px;dialogWidth:385px; dialogHeight:165px; scroll:no; status:no; help:no;');
                    finestra.focus
                    finestra.document.close()
                }
                else if (document.getElementById) {
                    self.find()
                }
                else window.alert('Il tuo browser non supporta questo metodo')
            }
            //popupWindow.focus();
    </script>
        <script type="text/javascript">

            //popupWindow.focus();
    </script>
    <p>
                    <asp:ImageButton ID="btnAnnulla0" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
                        Style="z-index: 101; left: 516px; position: absolute; top: 515px; height: 20px;" 
                        ToolTip="Home" TabIndex="9" />
                    </p>
    
    </form>
     <script type="text/javascript">
         document.getElementById('splash').style.visibility = 'hidden';

         function Attendi() {
             document.getElementById('splash').style.visibility = 'visible';
         }
                </script>
        </body>
</html>