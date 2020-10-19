<%@ Page Language="VB" AutoEventWireup="false" CodeFile="AnnullaRestituisci.aspx.vb" Inherits="ASS_AnnullaRestituisci" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
var Uscita;
Uscita=1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Inviti</title>
</head>

<body bgcolor="#f2f5f1">

    <form id="form1" runat="server" defaultbutton="btnVisualizza" 
    defaultfocus="DataGrid1">
    <div>
        <asp:Label ID="LBLID" runat="server" Height="21px" Style="z-index: 100; left: 13px;
            position: absolute; top: 300px" Visible="False" Width="78px">Label</asp:Label>
                    <asp:DataGrid ID="DataGrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        Font-Names="Arial" Font-Size="8pt" Height="86px" 
            PageSize="12" style="z-index: 108; left: 4px; position: absolute; top: 77px; width: 658px;" 
            BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
            Font-Strikeout="False" Font-Underline="False" GridLines="None" 
            TabIndex="1" Visible="False">
                        <PagerStyle Mode="NumericPages" />
                        <HeaderStyle BackColor="#F2F5F1" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PG" HeaderText="PG" Visible="False">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="PG">
                                <ItemTemplate>
                                    <asp:Label runat="server" 
                                        Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'></asp:Label>
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
                            <asp:TemplateColumn HeaderText="AI">
                                <ItemTemplate>
                                    <asp:Label runat="server" 
                                        Text='<%# DataBinder.Eval(Container, "DataItem.AI") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="RU">
                                <ItemTemplate>
                                    <asp:Label runat="server" 
                                        Text='<%# DataBinder.Eval(Container, "DataItem.RU") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="RI">
                                <ItemTemplate>
                                    <asp:Label runat="server" 
                                        Text='<%# DataBinder.Eval(Container, "DataItem.AI") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Label runat="server" 
                                        Text='<%# DataBinder.Eval(Container, "DataItem.RI") %>'></asp:Label>
                                    <asp:LinkButton ID="LinkButton4" runat="server" 
                                        CommandName="Update" Style="z-index: 100;
                                        left: 0px; position: static; top: 0px" Width="52px">Seleziona</asp:LinkButton>
                                
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton5" runat="server" CommandName="Update" Style="z-index: 100;
                                        left: 0px; position: static; top: 0px" Width="52px">Seleziona</asp:LinkButton>
                                </ItemTemplate>
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                        </Columns>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" />
                    </asp:DataGrid>
        &nbsp;
        <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
            position: absolute; top: 0px">
            <tr>
                <td style="width: 670px">
                    <br />
                    <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco
                        Domande Invitate&nbsp; </strong>
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
                    <br />
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="HiddenField2" runat="server" />
                    <br />
                </td>
            </tr>
        </table>
        <asp:Label ID="LBLPROGR" runat="server" Height="23px" Style="z-index: 101; left: 17px;
            position: absolute; top: 206px" Visible="False" Width="57px">Label</asp:Label>
                    <asp:Label ID="lbl1" runat="server" BorderColor="Black" BorderStyle="None" Font-Bold="True"
                        Font-Names="Arial" Font-Size="XX-Small" ForeColor="Blue" style="z-index: 102; left: 4px; position: absolute; top: 410px">Selezionare la domanda  e poi scegliere quale operazione compiere.</asp:Label>
                    <asp:Label ID="Label5" runat="server" BorderColor="Black" BorderStyle="None" Font-Bold="True"
                        Font-Names="Arial" Font-Size="9pt" ForeColor="Red" Style="z-index: 103; left: 4px;
                        position: absolute; top: 56px">SONO VISUALIZZATE SOLO LE DOMANDE INVITATE E SENZA PROPOSTE/ACCETTAZIONI IN CORSO</asp:Label>
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" 
            Font-Names="Arial" 
            style="z-index: 104; left: 5px; position: absolute; top: 441px; text-align: left" 
            Width="317px" Font-Size="12pt">Nessuna selezione</asp:Label>
                    <asp:Button ID="btnVisualizza0" runat="server" BackColor="Red" 
            Font-Bold="True" ForeColor="White"
                        Height="32px" TabIndex="2" Text="ANNULLA E RESTITUISCI DOMANDA" 
            
            style="z-index: 105; left: 157px; position: absolute; top: 465px; width: 248px;" 
            onclientclick="VerificaRestituzione();" 
            ToolTip="Effettuare questa scelta per restituire la domanda al Comune per la revisione della stessa." />
                    <asp:Button ID="btnVisualizza" runat="server" BackColor="Red" 
            Font-Bold="True" ForeColor="White"
                        Height="32px" TabIndex="2" Text="ANNULLA INVITO" 
            Width="140px" 
            style="z-index: 105; left: 6px; position: absolute; top: 465px; right: 1141px;" 
            onclientclick="VerificaAnnullo();" 
            
            
            
            ToolTip="Effettuare questa scelta se la domanda deve tornare nel gruppo delle domande invitabili per invito errato o altri motivi." />
                    <asp:DataGrid ID="DataGrid1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        Font-Names="Arial" Font-Size="8pt" Width="623px" Height="86px" 
            PageSize="12" style="z-index: 107; left: 4px; position: absolute; top: 77px" 
            BackColor="White" Font-Bold="False" Font-Italic="False" Font-Overline="False" 
            Font-Strikeout="False" Font-Underline="False" GridLines="None" TabIndex="1">
                        <PagerStyle Mode="NumericPages" />
                        <HeaderStyle BackColor="#F2F5F1" Font-Bold="False" Font-Names="Arial" Font-Size="8pt" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Blue" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PG" HeaderText="PG" Visible="False">
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="POS">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.POSIZIONE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="PROTOCOLLO">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'>
										</asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'>
										</asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="COGNOME">
                                <ItemTemplate>
                                    <asp:Label ID="Label111" runat="server" Font-Names="ARIAL" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'>
										</asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'>
										</asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="NOME">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'>
										</asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="TextBox2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'>
										</asp:TextBox>
                                </EditItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ISBARC/R">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ISBARC_R") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="ISEE">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.REDDITO_ISEE") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="N.C.">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.N_COMP_NUCLEO") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Art.">
                                <ItemTemplate>
                                    <asp:Label ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.art") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="Edit" Style="z-index: 100;
                                        left: 0px; position: static; top: 0px" Width="51px" ToolTip="Visualizza i Dati, le Preferenze e gli alloggi disponibili per questa domanda">Verifica</asp:LinkButton>
                                </ItemTemplate>
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
                                <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" Wrap="False" />
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
                chiediConferma = window.confirm("Attenzione...Effettuare questa scelta se la domanda deve tornare nel gruppo delle domande invitabili per invito errato o altri motivi. Premere OK per confermare o ANNULLA per non procedere.");
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
                chiediConferma = window.confirm("Attenzione...Effettuare questa scelta per ricollocare la domanda in Graduatoria per la modifica della stessa da parte dell\'ufficio preposto. La domanda sarà eventualmente ancora gestibile da questo modulo solo al termine delle conseguneti attività istruttoria. Premere OK per confermare o ANNULLA per non procedere.");
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

</html>

