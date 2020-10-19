<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DocNecessari.aspx.vb" Inherits="ANAUT_DocNecessari" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title></title>
    <script type="text/javascript" src="prototype.lite.js"></script>
    <script type="text/javascript" src="moo.fx.js"></script>
    <script type="text/javascript" src="moo.fx.pack.js"></script>
</head>
<body bgcolor="#f2f5f1">
    <script type="text/javascript">
        //document.onkeydown=$onkeydown;
    </script>
    <form id="Form1" method="post" runat="server">
    &nbsp;&nbsp;
    <table style="left: 0px; background-image: url(../../NuoveImm/SfondoMaschere.jpg); width: 674px;
        position: absolute; top: 0px">
        <tr>
            <td style="width: 670px">
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco
                    Documentazione Necessaria</strong></span><br /><br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <br />
                <img alt="Aggiungi Documento" onclick="document.getElementById('TextBox1').value='1';myOpacity.toggle();"
                    src="../../NuoveImm/Img_Aggiungi.png" style="cursor: pointer; left: 584px; position: absolute;
                    top: 133px; width: 60px;" id="IMG1" />
                <br />
                <br />
                <br />
                <br />
                <div style="left: 20px; overflow: auto; width: 550px; position: absolute; top: 117px;
                    height: 250px">
                    <asp:DataGrid ID="DataGridIntLegali" runat="server" AutoGenerateColumns="False" BorderWidth="0px"
                        Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" meta:resourcekey="DataGrid1Resource1"
                        PageSize="100" Style="z-index: 101; left: 483px; top: 68px" Width="550px">
                        <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <PagerStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Mode="NumericPages" Wrap="False" />
                        <AlternatingItemStyle BackColor="Gainsboro" Font-Bold="False" Font-Italic="False"
                            Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Wrap="False" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" Wrap="False" />
                        <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
                            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" ForeColor="#0000C0"
                            Wrap="False" />
                        <Columns>
                            <asp:BoundColumn DataField="ID" HeaderText="ID" ReadOnly="True" Visible="False">
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="DESCRIZIONE" HeaderText="QUARTIERE" ReadOnly="True" Visible="False">
                            </asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="TIPO DOCUMENTO">
                                <ItemTemplate>
                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DESCRIZIONE") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>
                </div>
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
            </td>
        </tr>
    </table>
    <div id="InserimentoLegali" style="display: block; left: 0px; width: 100%; position: absolute;
        top: 0px; height: 100%; text-align: left; background-repeat: no-repeat; background-image: url('../../ImmDiv/SfondoDim3.jpg');
        z-index: 500;">
        <span style="font-family: Arial"></span>
        <br />
        <br />
        <table border="0" cellpadding="1" cellspacing="1" style="left: 126px; width: 435px;
            position: absolute; top: 129px; background-color: #FFFFFF; z-index: 200; height: 208px;">
            <tr>
                <td style="width: 52px; height: 19px; text-align: left">
                    &nbsp;
                </td>
                <td style="width: 274px; height: 19px; text-align: left">
                    <strong><span style="font-family: Arial">Documento</span></strong>
                </td>
            </tr>
            <tr style="font-size: 12pt; font-family: Times New Roman">
                <td style="width: 52px; height: 19px; text-align: left">
                    <span style="font-size: 10pt; font-family: Arial">Descrizione</span>
                </td>
                <td style="width: 274px; height: 19px; text-align: left">
                &nbsp&nbsp
                    <asp:TextBox ID="txtQuartiere" runat="server" Font-Names="Arial" Font-Size="9pt"
                        Width="293px" MaxLength="400" TabIndex="1" Height="53px" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="width: 52px; height: 19px">
                </td>
                <td align="right" style="width: 274px; height: 19px; text-align: right">
                    <table border="0" cellpadding="1" cellspacing="1" style="width: 110%">
                        <tr>
                            <td style="text-align: right">
                                <asp:ImageButton ID="img_InserisciSchema" runat="server" ImageUrl="~/NuoveImm/Img_InserisciVal.png"
                                    TabIndex="2" />&nbsp;<asp:ImageButton ID="img_ChiudiSchema" runat="server" ImageUrl="~/NuoveImm/Img_AnnullaVal.png"
                                        OnClientClick="document.getElementById('InserimentoLegali').style.visibility='hidden';document.getElementById('TextBox1').value='0';document.getElementById('txtQuartiere').value='';" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:HiddenField ID="txtid" runat="server" />
        <asp:HiddenField ID="sololettura" runat="server" />
    </div>
    &nbsp;
    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
        Style="z-index: 100; left: 560px; position: absolute; top: 432px" TabIndex="11"
        ToolTip="Home" />
    <asp:Label ID="lblBando" runat="server" Font-Size="12pt" Font-Names="Arial" Font-Bold="True"
        
        Style="z-index: 102; left: 20px; position: absolute; top: 70px; width: 618px;">BANDO</asp:Label>
    <p>
        <asp:ImageButton ID="ImgModifica" OnClientClick="document.getElementById('TextBox1').value='2'"
            runat="server" ImageUrl="~/NuoveImm/Img_Modifica.png" Style="left: 584px; position: absolute;
            top: 156px;" ToolTip="Modifica Documento" EnableTheming="True" 
            CausesValidation="False" />
        <asp:ImageButton ID="ImgBtnAggiungi" runat="server" ImageUrl="~/NuoveImm/Img_Elimina.png"
            Style="left: 584px; position: absolute; top: 178px" 
            ToolTip="Elimina il documento selezionato" />
        <asp:TextBox ID="txtmia" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
            meta:resourcekey="TextBox3Resource1" Style="border: 1px solid white; left: 17px;
            position: absolute; top: 385px; width: 619px;" Text="Nessuna Selezione" BackColor="#F2F5F1"
            BorderWidth="0px"></asp:TextBox>
        <asp:Label ID="lblErrore" runat="server" Font-Bold="False" Font-Names="Arial" Font-Size="8pt"
            ForeColor="Red" Height="16px" Style="z-index: 104; left: 9px; position: absolute;
            top: 498px" Visible="False" Width="525px"></asp:Label>
    </p>
    <asp:HiddenField ID="TextBox1" runat="server" />
    <script type="text/javascript">

        var Selezionato;
        myOpacity = new fx.Opacity('InserimentoLegali', { duration: 200 });
        //myOpacity.hide();

        if (document.getElementById('TextBox1').value != '2') {
            myOpacity.hide();
        }

        if (document.getElementById('sololettura').value == '1') {
            document.getElementById('IMG1').style.visibility = 'hidden';
            document.getElementById('IMG1').style.position = 'absolute';
            document.getElementById('IMG1').style.left = '-100px';
            document.getElementById('IMG1').style.display = 'none';
        }

    </script>
    </form>
</body>
</html>
