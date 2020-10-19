<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AnnullaAssegnazioni.aspx.vb" Inherits="ASS_AnnullaAssegnazioni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
var Uscita;
Uscita=1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<script type="text/javascript" src="../Contratti/prototype.lite.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.js"></script>
<script type="text/javascript" src="../Contratti/moo.fx.pack.js"></script>
<head id="Head1" runat="server">
    <title>Annulla Assegnazioni</title>
    <style type="text/css">
        .style1
        {
            font-weight: bold;
        }
    </style>
</head>

<body bgcolor="#f2f5f1">

    <form id="form1" runat="server" 
    defaultfocus="DataGrid1">
    <div id="Motivazione"  
                
        style="border: 1px solid #000080; position:absolute; width: 675px; height: 538px; top: 0px; left: 0px; background-color: #E2E2E2; z-index: 300; text-align: center;">
                <br />
                <br />
                <br />
                <br />
                <br />
            <table width="50%">
                <tr>
                    <td class="style1">
                        ANNULLAMENTO ASSEGNAZIONE MOTIVATO DA</td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:RadioButton ID="RadioButton1" runat="server" Font-Names="ARIAL" 
                            Font-Size="8pt" GroupName="A" Text="OCCUPAZIONE ABUSIVA" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:RadioButton ID="RadioButton2" runat="server" Font-Names="ARIAL" 
                            Font-Size="8pt" GroupName="A" Text="ALTRO" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:RadioButton ID="RadioButton3" runat="server" Font-Names="ARIAL" 
                            Font-Size="8pt" GroupName="A" Text="ALLOGGIO INAGIBILE O NON IDONEO" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:RadioButton ID="RadioButton4" runat="server" Font-Names="ARIAL" 
                            Font-Size="8pt" GroupName="A" Text="CANONE NON SOPPORTABALE" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:RadioButton ID="RadioButton5" runat="server" Font-Names="ARIAL" 
                            Font-Size="8pt" GroupName="A" Text="ERRORE UFFICIO" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">
                        <asp:RadioButton ID="RadioButton6" runat="server" Font-Names="ARIAL" 
                            Font-Size="8pt" GroupName="A" Text="BARRIERE ARCHITETTONICHE" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;&nbsp;</td>
                </tr>
                <tr>
                    <td>
        <asp:Button ID="btnAccetta" runat="server" BackColor="Red" Font-Bold="True" ForeColor="White"
            Height="35px" Style="z-index: 100; "
            Text="CONFERMA" Width="181px" 
                        OnClientClick="AnnullaProposta();" 
                        TabIndex="1" />
                    &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="btnAccetta0" runat="server" BackColor="Red" Font-Bold="True" ForeColor="White"
            Height="35px" Style="z-index: 100; "
            Text="ANNULLA" Width="73px" 
                        TabIndex="1" />
                    </td>
                </tr>
            </table>
    </div>
    <div>
        <asp:Label ID="LBLID" runat="server" Height="21px" Style="z-index: 100; left: 13px;
            position: absolute; top: 300px" Visible="False" Width="78px">Label</asp:Label>
        &nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px">
            <tr>
                <td style="width: 670px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco
                        Assegnazioni&nbsp; </strong>
                    <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label>
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
                    <img alt="" src="../NuoveImm/Img_AnnullaAssegnazione.png" onclick="javascript:myOpacity.toggle();"/><br />
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    <asp:HiddenField ID="CodAlloggio" runat="server" />
                    <asp:HiddenField ID="IdDomanda" runat="server" />
                    <asp:HiddenField ID="idunita" runat="server" />
                    <asp:HiddenField ID="cf_piva" runat="server" />
                    <br />
                </td>
            </tr>
        </table>
        <asp:Label ID="LBLPROGR" runat="server" Height="23px" Style="z-index: 101; left: 17px;
            position: absolute; top: 206px" Visible="False" Width="57px">Label</asp:Label>
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" 
            Font-Names="Arial" 
            style="z-index: 104; left: 5px; position: absolute; top: 441px; text-align: left" 
            Width="317px" Font-Size="12pt">Nessuna selezione</asp:Label>
                    <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        Font-Names="Arial" Font-Size="8pt" Width="623px" Height="86px" 
            PageSize="12" style="z-index: 107; left: 4px; position: absolute; top: 77px" 
            BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
            Font-Strikeout="False" Font-Underline="False" GridLines="None" TabIndex="1">
                        <PagerStyle Mode="NumericPages" />
                        <HeaderStyle BackColor="#F2F5F1" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" />
                        <Columns>
                            <asp:BoundColumn DataField="N_OFFERTA" HeaderText="ID" Visible="False">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="COD_UNITA_IMMOBILIARE" ReadOnly="True" 
                                Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ID_DOMANDA" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="id_unita" Visible="False"></asp:BoundColumn>
                            <asp:BoundColumn DataField="cf_piva" Visible="False"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="NUM.OFFERTA">
                                <ItemTemplate>
                                    <asp:Label runat="server" 
                                        Text='<%# DataBinder.Eval(Container, "DataItem.N_OFFERTA") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="DATA ASSEGNAZIONE">
                                <ItemTemplate>
                                    <asp:Label runat="server" 
                                        Text='<%# DataBinder.Eval(Container, "DataItem.DATA_ASSEGNAZIONE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="COD.UNITA">
                                <ItemTemplate>
                                    <asp:Label runat="server" 
                                        Text='<%# DataBinder.Eval(Container, "DataItem.COD_UNITA_IMMOBILIARE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="COGNOME/R.S.">
                                <ItemTemplate>
                                    <asp:Label runat="server" 
                                        Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME_RS") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="NOME">
                                <ItemTemplate>
                                    <asp:Label runat="server" 
                                        Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="Update" Style="z-index: 100;
                                        left: 0px; position: static; top: 0px" Width="52px">Seleziona</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                        </Columns>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" />
                    </asp:DataGrid>
        <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
            Style="z-index: 108; left: 434px; position: absolute; top: 495px; right: 531px;" 
            ToolTip="Nuova Ricerca" TabIndex="3" />
        <asp:ImageButton ID="btAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
            Style="z-index: 111; left: 570px; position: absolute; top: 495px" 
            ToolTip="Home" TabIndex="4" />
    
    </div>
    </form>
        <script  language="javascript" type="text/javascript">
            document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
    <script type="text/javascript">
        function VerificaAnnullo() {

            if (document.getElementById('HiddenField2').value != '') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Effettuare questa scelta se si intende annullare l\'Assegnazione. Sarà possibile assegnare una nuova unità. Premere OK per confermare o ANNULLA per non procedere.");
                if (chiediConferma == false) {
                    document.getElementById('HiddenField1').value = '0';
                }
                else {
                    document.getElementById('HiddenField1').value = '1';
                }
            }
        }

        function VerificaRestituzione() {

            if (document.getElementById('HiddenField2').value != '') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Effettuare questa scelta per restituire la domanda al Comune per la revisione della stessa. Effettuando questa scelta la domanda tornerà di competenza comunale e non sarà più gestibile! Premere OK per confermare o ANNULLA per non procedere.");
                if (chiediConferma == false) {
                    document.getElementById('HiddenField1').value = '0';
                }
                else {
                    document.getElementById('HiddenField1').value = '1';
                }
            }
        }
</script>
</body>
        	    <script type="text/javascript">
        	        myOpacity = new fx.Opacity('Motivazione', { duration: 200 });
        	        myOpacity.hide();
        	        
        </script>
        <script type="text/javascript">
        function AnnullaProposta() {

            if (document.getElementById('HiddenField2').value != '') {
                var chiediConferma
                chiediConferma = window.confirm("Attenzione...Annullare l\'assegnazione? Premere OK per confermare o ANNULLA per non procedere.");
                if (chiediConferma == false) {
                    document.getElementById('HiddenField1').value = '0';
                }
                else {
                    document.getElementById('HiddenField1').value = '1';
                }
            }
        }
        </script>
</html>


