<%@ Page Language="VB" AutoEventWireup="false" CodeFile="GestIncassi.aspx.vb" Inherits="Contratti_Bollettazione_GestIncassi" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style1
        {
            text-align: center;
            font-family: Arial;
            font-size: 14pt;
            color: #CC3300;
        }
        .style2
        {
            font-family: Arial;
            font-size: 10pt;
        }
        /*BOTTONI*/
        .bottone
        {
            /*background-color: #507cd1;     border-style: none;     color: White;     font-weight: bold;     font-size: 8pt;     height: 25px;     cursor: pointer;     */
            background-color: transparent;
            border-left: 8px solid #800000;
            border-right: 0px solid #800000;
            border-top: 0px solid #800000;
            border-bottom: 0px solid #800000;
            font-weight: bold;
            font-size: 10pt;
            height: 20px;
            cursor: pointer;
        }
        
        /*-------------------------------------------*/
        .CssMaiuscolo
        {
            text-transform: uppercase;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function Conferma() {
            var chiediConferma;
            chiediConferma = window.confirm("Attenzione...Procedere con il caricamento del file sulla base di dati?");
            if (chiediConferma == true) {
                document.getElementById("conferma").value = 1;
            }
            else {
                return false;

            }

        }
    
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <table style="width: 100%;">
        <tr>
            <td class="style1">
                <strong>GESTIONE ED ELABORAZION FILE DEGLI INCASSI POSTALI</strong>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td class="style2">
                <strong>&nbsp;Upload del file da elaborare</strong>
            </td>
        </tr>
        <tr>
            <td>
                <table style="border: thin solid #3399FF;">
                    <tr>
                        <td>
                            <asp:FileUpload ID="FileUpload" runat="server" Font-Names="Arial" Font-Size="10pt"
                                CssClass="CssMaiuscolo" Height="20" Width="500px" size="60" />
                        </td>
                        <td>
                            <asp:Button ID="btnElabora" runat="server" Text="Elabora" CssClass="bottone" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td style="text-align: center">
                <asp:Label ID="lblImport" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="10pt"
                    Text="BOLLETTE OTTENUTE DALLA LETTURA DEL FILE" Visible="False"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <strong><span style="font-size: 14pt; color: #801f1c; font-family: Arial">
                    <asp:DataGrid ID="dgvImport" runat="server" AutoGenerateColumns="False" CellPadding="4"
                        Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
                        PageSize="24" Style="z-index: 105; left: 193px; top: 54px" Width="100%" ForeColor="#333333">
                        <ItemStyle BackColor="#EFF3FB" />
                        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                        <AlternatingItemStyle BackColor="White" />
                        <Columns>
                            <asp:BoundColumn DataField="ID_BOLLETTA" HeaderText="ID_BOLLETTA" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="COGNOME_NOME" HeaderText="INTESTATARIO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="INDIRIZZO" HeaderText="INDIRIZZO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="RESIDENZA" HeaderText="RESIDENZA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SCADENZA" HeaderText="SCADENZA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="DA_PAGARE" HeaderText="TOT. EMESSO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="PAGATO" HeaderText="TOT. PAGATO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DATA_PAGAMENTO" HeaderText="DATA PAGAMENTO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="RITARDO" HeaderText="GG. RITARDO">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Right" Wrap="False" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="NOTE" HeaderText="NOTE">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                    Font-Underline="False" HorizontalAlign="Center" />
                            </asp:BoundColumn>
                        </Columns>
                        <EditItemStyle BackColor="#2461BF" />
                        <FooterStyle BackColor="#507CD1" ForeColor="White" Font-Bold="True" />
                        <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                            ForeColor="White" />
                        <SelectedItemStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                    </asp:DataGrid>
                </span></strong>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center">
                            <asp:Button ID="btnAnnulla" runat="server" Text="Annulla" OnClientClick="self.close(); return false;"
                                Visible="False" CssClass="bottone" />
                        </td>
                        <td style="text-align: center">
                            &nbsp;
                        </td>
                        <td style="text-align: center">
                            <asp:Button ID="btnConferma" runat="server" Text="Conferma" Visible="False" CssClass="bottone"
                                OnClientClick="Conferma();" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="nomeFile" runat="server" />
                <asp:HiddenField ID="conferma" runat="server" Value="0" />
            </td>
        </tr>
    </table>
    </form>
        <script language="javascript" type="text/javascript">
            document.getElementById('dvvvPre').style.visibility = 'hidden';
        </script>
        

</body>
</html>
