<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SituazioneVariazioni.aspx.vb"
    Inherits="SituazioneVariazioni" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1" />
    <title>Situazione Variazioni</title>
    <script type="text/javascript">
        /*function ApriRiepilogo1() {
            window.open('RiepilogoVariazioni.aspx?AN=' + document.getElementById('AnnoSelezionato').value, 'Variazioni', '');
        }
        function ApriRiepilogo2() {
            window.open('DettaglioRiepilogoVariazioni.aspx?AN=' + document.getElementById('AnnoSelezionato').value, 'Variazioni', '');
        }
        function ApriRiepilogo3() {
            window.open('DettaglioVariazioni.aspx?AN=' + document.getElementById('AnnoSelezionato').value, 'Variazioni', '');
        }*/
        function Home() {
            location.replace('../../pagina_home.aspx');
        }
    </script>
    <style type="text/css">
        .style1
        {
            height: 257px;
            text-align: center;
        }
        .style2
        {
            height: 400px;
            text-align: left;
            width: 624px;
        }
        .style3
        {
            height: 350px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <table width="100%">
        <tr>
            <td style="height: 5px;">
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td width="80%">
                            <asp:Label ID="lblTitolo" runat="server" Font-Bold="True" Font-Names="Arial" Font-Size="12pt"
                                ForeColor="Maroon" Width="100%">Situazione Variazioni</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td style="height: 10px">
            </td>
        </tr>
        <tr>
            <td>
                <table width="100%">
                    <tr>
                        <td style="width: 20%">
                            <asp:Label ID="Label1" runat="server" Text="Esercizio finanziario" Font-Names="Arial"
                                Font-Size="10pt"></asp:Label>
                        </td>
                        <td style="width: 80%">
                            <asp:DropDownList ID="ddlAnno" runat="server" AutoPostBack="True" Font-Names="Arial"
                                Font-Size="10pt" Width="200px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="style3">
                <table width="100%">
                    <tr>
                        <td style="text-align: left;" colspan="2">
                            &nbsp;
                            <asp:TreeView ID="T1" runat="server" Font-Names="arial" Font-Size="8pt" Height="124px"
                                Style="z-index: 110; left: 1px; position: absolute; top: 256px" Width="106px"
                                ImageSet="Arrows">
                                <LevelStyles>
                                    <asp:TreeNodeStyle Font-Bold="False" Font-Names="arial" Font-Size="8pt" Font-Underline="False"
                                        ForeColor="#721C1F" HorizontalPadding="1px" VerticalPadding="5px" />
                                    <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                                        HorizontalPadding="3px" />
                                    <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                                        HorizontalPadding="3px" />
                                    <asp:TreeNodeStyle Font-Names="arial" Font-Size="8pt" Font-Underline="False" ForeColor="#721C1F"
                                        HorizontalPadding="3px" />
                                </LevelStyles>
                                <Nodes>
                                    <asp:TreeNode Text="Riepilogo variazioni tra voci (raggruppamento per capitoli)"
                                        Value="RiepGen">
                                    </asp:TreeNode>
                                    <asp:TreeNode Text="Riepilogo variazioni tra voci e tra strutture (dettaglio per strutture)" 
                                        Value="RiepStrutt"></asp:TreeNode>
                                    <asp:TreeNode Text="Dettaglio variazioni tra voci" Value="RiepDett"></asp:TreeNode>
                                 </Nodes>
                                <NodeStyle BorderStyle="None" Font-Names="arial" Font-Size="8pt" ForeColor="Black"
                                    HorizontalPadding="1px" NodeSpacing="0px" VerticalPadding="0px" />
                                <ParentNodeStyle Font-Bold="False" />
                                <HoverNodeStyle Font-Underline="True" ForeColor="#5555DD" Font-Names="Arial" Font-Size="8pt" />
                                <SelectedNodeStyle Font-Underline="True" ForeColor="#5555DD" HorizontalPadding="0px"
                                    VerticalPadding="0px" />
                            </asp:TreeView>
                        </td>
                    </tr>
                    <tr>
                        <td align="left" class="style2" valign="bottom">
                            <asp:Label ID="lblErrore" runat="server" Font-Names="Arial" Font-Size="10pt" 
                                ForeColor="Red" style="text-align: left"></asp:Label>
                        </td>
                        <td class="style1" valign="bottom">
                            <asp:Image ID="btnHome" runat="server" ImageUrl="../../../NuoveImm/Img_HomeModelli.png"
                                onclick="Home();" style="cursor:pointer"
                                ToolTip="Torna alla Home" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <asp:HiddenField ID="AnnoSelezionato" runat="server" Value="0" />
    </form>
    <script type="text/javascript">
        window.focus();
        self.focus();
    </script>
</body>
<script language="javascript" type="text/javascript">
    document.getElementById('dvvvPre').style.visibility = 'hidden';
</script>
</html>
