<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatoContratti.aspx.vb"
    Inherits="Contratti_RisultatoContratti" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Uscita;
    Uscita = 1;

    function $onkeydown() {

        if (event.keyCode == 13) {
            ApriContratto();
        }
    }
   
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Elenco Contratti</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <style type="text/css">
        #Form1
        {
            width: 800px;
        }
    </style>
</head>
<body bgcolor="White">
    <script type="text/javascript">
        document.onkeydown = $onkeydown;
    </script>
    <form id="Form1" method="post" runat="server">
    &nbsp;&nbsp;
    <table style="left: 0px; background-image: url(../../NuoveImm/SfondoMascheraContratti.jpg);
        width: 674px; position: absolute; top: 0px">
        <tr>
            <td style="width: 674px">
                <asp:HiddenField ID="Label3" runat="server" />
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Intestatari
                    <asp:Label ID="Label4" runat="server" Text="DD"></asp:Label>
                </strong></span>
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
                <asp:TextBox ID="TextBox3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
                    Style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                    border-bottom: white 1px solid; left: -1px; top: 45px;" Width="777px">Nessuna Selezione</asp:TextBox><br />
                <asp:HiddenField ID="LBLID" runat="server" />
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" ImageUrl="../../NuoveImm/Falso.png" />
                <br />
                <asp:HiddenField ID="LBLintest" runat="server" />
                <asp:HiddenField ID="LBLcodF" runat="server" />
                <br />
                <asp:ImageButton ID="btnIndietro" runat="server" ImageUrl="~/NuoveImm/Img_IndietroGrande.png"
                    Style="z-index: 101; left: 166px; position: absolute; top: 480px; right: 543px;"
                    TabIndex="25" ToolTip="Indietro" />
                <img onclick="document.location.href='../pagina_home.aspx';" alt="" src="../../NuoveImm/Img_Home.png"
                    
                    style="position: absolute; top: 480px; left: 560px; cursor: pointer; height: 20px;" />
                <img onclick="document.location.href='RicercaContratti.aspx';" alt="" src="../../NuoveImm/Img_NuovaRicerca.png"
                    style="position: absolute; top: 480px; cursor: pointer; right: 251px;" />
                <img onclick="ApriContratto();" alt="" src="../../NuoveImm/Img_Procedi.png" style="position: absolute;
                    top: 480px; left: 281px; cursor: pointer;" id="Visualizza" /><asp:HiddenField 
                    ID="ModRichiesta" runat="server" />
&nbsp;<asp:HiddenField ID="H1" runat="server" Value="0" />
                <br />
            </td>
        </tr>
    </table>
    &nbsp;
    <asp:Label ID="LBLPROGR" Style="z-index: 104; left: 404px; position: absolute; top: 500px"
        runat="server" Width="57px" Height="23px" Visible="False" TabIndex="-1">Label</asp:Label>
    &nbsp; &nbsp;&nbsp;&nbsp;
    <div id="ElContratti" style="overflow:auto; position: absolute; width: 650px; height: 350px;
        top: 64px; left: 14px;">
        <asp:DataGrid ID="Datagrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
            PageSize="200" Style="z-index: 101; left: 9px; width: 1300px;" TabIndex="1" BorderWidth="0px">
            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <PagerStyle Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" Wrap="False" />
            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" Wrap="False" />
            <Columns>
                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="CODICE" ReadOnly="True" Visible="False">
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="INTESTATARIO" HeaderText="INTESTATARIO" ReadOnly="True" Visible="False">
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="COD_FISCALE" HeaderText="COD_FISCALE" ReadOnly="True" Visible="False">
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:TemplateColumn HeaderText="CODICE">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_CONTRATTO") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="INTESTATARIO">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INTESTATARIO") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="STATO">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.STATO_DEL_CONTRATTO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="TIPO">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_TIPOLOGIA_CONTR_LOC") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="POSIZIONE">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.POSIZIONE_CONTRATTO") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="COD.UNITA">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_UNITA_IMMOBILIARE") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="TIPO UN.">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_TIPOLOGIA") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="INDIRIZZO">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.INDIRIZZO") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="CIV.">
                    <EditItemTemplate>
                        <asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CIVICO") %>'></asp:TextBox>
                    </EditItemTemplate>
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CIVICO") %>'></asp:Label>
                    </ItemTemplate>
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="COMUNE">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COMUNE_UNITA") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="ALLEGATI">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ALLEGATI_CONTRATTO") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <HeaderStyle BackColor="#F2F5F1" Font-Bold="False" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="Navy" Wrap="False" />
            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
        </asp:DataGrid>
    </div>
    &nbsp;
    </form>
    <script language="javascript" type="text/javascript">

        var Selezionato;
        document.getElementById('dvvvPre').style.visibility = 'hidden';

        function ApriContratto() {
            if (document.getElementById('LBLID').value != '-1' && document.getElementById('LBLID').value != '') {
                today = new Date();
                var Titolo = 'Contratto' + today.getMinutes() + today.getSeconds();
                //popupWindow = 
                location.replace('dichiarazione1.aspx?ID=' + document.getElementById('LBLID').value + '&COD=' + document.getElementById('Label3').value + '&INTEST=' + document.getElementById('LBLintest').value + '&ModR=' + document.getElementById('ModRichiesta').value + '&CODF=' + document.getElementById('LBLcodF').value);
                //popupWindow.focus();
            }
            else {
                alert('Nessun Contratto Selezionato!');
            }

        }

        //document.getElementById('Visualizza').focus();
    </script>
    <p>
        &nbsp;</p>
</body>
</html>
