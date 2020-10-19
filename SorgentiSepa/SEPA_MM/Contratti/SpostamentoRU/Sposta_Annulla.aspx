<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Sposta_Annulla.aspx.vb"
    Inherits="Contratti_Sposta_Annulla" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Seleziona unità</title>
    <style type="text/css">
        .stile_tabella
        {
            width: 100%;
            margin-top: 5%;
            margin-left: 10px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="motiv" style="width: 800px; left: 0px; background-repeat: no-repeat; background-image: url('../../NuoveImm/SfondoMascheraContratti.jpg');
        z-index: 500; position: absolute; top: 0px; height: 457px;">
        <br />
        <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp Seleziona
            Unità per stipula rapporto </strong>
            <asp:Label ID="lblTipoContratto" runat="server" Font-Size="14pt" Font-Bold="True"></asp:Label></span><br />
        <br />
        <img id="imgCambiaAmm" alt="Ricerca Rapida" onclick="cerca();" src="../Immagini/Search_16x16.png"
            style="position: absolute; top: 49px; left: 759px; cursor: pointer" />
        <div id="contenitore" style="position: absolute; width: 770px; height: 250px; top: 77px;
            overflow: auto; left: 13px;">
            <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" Font-Names="Arial"
                Font-Size="8pt" HorizontalAlign="Left" Style="z-index: 121; left: 0px; top: 0px"
                Width="750px" TabIndex="2" BorderWidth="0px" CellPadding="1">
                <PagerStyle Mode="NumericPages" />
                <Columns>
                    <asp:BoundColumn DataField="ID" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="COD_ALLOGGIO" HeaderText="COD" Visible="False">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="PROPRIETA" HeaderText="PROPRIETA" ReadOnly="True" Visible="False">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="COD_ALLOGGIO" HeaderText="CODICE">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPOLOGIA" HeaderText="TIPO">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="ZONA" HeaderText="ZONA">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="TIPO_VIA" HeaderText="INDIRIZZO">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="NUM_CIVICO" HeaderText="CIV.">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="N_ALL" HeaderText="N.ALL">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="NUM_LOCALI" HeaderText="LOC.">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="PIANO" HeaderText="PIANO">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="ELEVATORE" HeaderText="ASC.">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="SUP" HeaderText="SUP.">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="DATA_DISPONIBILITA1" HeaderText="DISP.">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="PROPRIETA1" HeaderText="PROPR.">
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:BoundColumn>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton3" runat="server" CommandName="Cancel" ImageUrl="~/NuoveImm/Abbina_Foto.png"
                                ToolTip="Dettagli Unità, Foto e Planimetrie" />
                        </ItemTemplate>
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                    </asp:TemplateColumn>
                    
                </Columns>
                <HeaderStyle BackColor="PapayaWhip" Font-Bold="True" Font-Names="Arial" Font-Size="8pt" />
            </asp:DataGrid>
        </div>
        <table width="100%">
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td style="padding-top: 270px;">
                    &nbsp&nbsp<asp:TextBox ID="txtSelezione" runat="server" Style="border-right: white 1px solid;
                        border-top: white 1px solid; border-left: white 1px solid; border-bottom: white 1px solid;
                        left: -1px; top: 45px;" Font-Bold="True" Font-Names="arial" Font-Size="12pt"
                        Width="100%">Nessuna selezione</asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td style="padding-left: 580px;">
                    <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                        TabIndex="30" ToolTip="Procedi" OnClientClick="unitaSelezionata();"/>
                    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_EsciCorto.png"
                        TabIndex="31" ToolTip="Esci" OnClientClick="self.close();" />
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="LBLCODUI" runat="server" Value="0" />
        <asp:HiddenField ID="LBLIDUI" runat="server" Value="0" />
    </div>
    </form>
    <script type="text/javascript">
        var Selezionato;
        document.getElementById('dvvvPre').style.visibility = 'hidden';
        function cerca() {
            if (document.all) {
                finestra = showModelessDialog('Find.htm', window, 'dialogLeft:150px;dialogTop:150px;dialogWidth:385px; dialogHeight:165px; scroll:no; status:no; help:no;');
                finestra.focus
                finestra.document.close()
            }
            else if (document.getElementById) {
                self.find()
            }
            else window.alert('Il tuo browser non supporta questo metodo')
        }

        function unitaSelezionata() {
            if (document.getElementById('LBLIDUI') == '') {
                alert('Nessuna unità selezionata!')
            }
        }
    </script>
</body>
</html>
