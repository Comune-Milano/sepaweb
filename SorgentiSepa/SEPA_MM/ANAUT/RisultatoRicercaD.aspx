<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatoRicercaD.aspx.vb"
    Inherits="ANAUT_RisultatoRicercaD" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<script type="text/javascript">
    var Uscita;
    Uscita = 1;
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>RisultatoRicercaD</title>
    <style type="text/css">
        #contenitore
        {
            left: 13px;
        }
    </style>
    <script language="javascript" type="text/javascript">
        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
            var tbl = document.getElementById(gridId);
            if (tbl) {
                var DivHR = document.getElementById('DivHeaderRow');
                var DivMC = document.getElementById('DivMainContent');
                var DivFR = document.getElementById('DivFooterRow');

                //*** Set divheaderRow Properties ****
                DivHR.style.height = headerHeight + 'px';
                DivHR.style.width = (parseInt(width) - 16) + 'px';
                DivHR.style.position = 'relative';
                DivHR.style.top = '0px';
                DivHR.style.zIndex = '10';
                DivHR.style.verticalAlign = 'top';

                //*** Set divMainContent Properties ****
                DivMC.style.width = width + 'px';
                DivMC.style.height = height + 'px';
                DivMC.style.position = 'relative';
                DivMC.style.top = -headerHeight + 'px';
                DivMC.style.zIndex = '1';

                //*** Set divFooterRow Properties ****
                DivFR.style.width = (parseInt(width) - 16) + 'px';
                DivFR.style.position = 'relative';
                DivFR.style.top = -headerHeight + 'px';
                DivFR.style.verticalAlign = 'top';
                DivFR.style.paddingtop = '2px';

                if (isFooter) {
                    var tblfr = tbl.cloneNode(true);
                    tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
                    var tblBody = document.createElement('tbody');
                    tblfr.style.width = '100%';
                    tblfr.cellSpacing = "0";
                    tblfr.border = "0px";
                    tblfr.rules = "none";
                    //*****In the case of Footer Row *******
                    tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
                    tblfr.appendChild(tblBody);
                    DivFR.appendChild(tblfr);
                }
                //****
                DivHR.appendChild(tbl.cloneNode(true));
            }
        }



        function OnScrollDiv(Scrollablediv) {
            document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
            document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
        }


    </script>
</head>
<body bgcolor="#f2f5f1">
    <form id="Form1" method="post" runat="server" defaultbutton="btnVisualizza">
    &nbsp;&nbsp;
    <table style="left: 0px; background-image: url(../NuoveImm/SfondoMaschere.jpg); width: 674px;
        position: absolute; top: 0px">
        <tr>
            <td>
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; Elenco
                    Dichiarazioni Trovate </strong>
                    <asp:Label ID="Label9" runat="server" Text="Label"></asp:Label>
                    <img id="imgCambiaAmm" alt="Ricerca Rapida" onclick="cerca();" src="../Contratti/Immagini/Search_16x16.png"
                        style="position: absolute; top: 24px; left: 643px; cursor: pointer" />
                </span>
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
                <asp:TextBox ID="TextBox7" runat="server" BorderWidth="0px" Font-Bold="True" Font-Names="arial"
                    Font-Size="12pt" ReadOnly="True" Width="657px">Nessuna Selezione</asp:TextBox>
                <br />
                <br />
                <br />
                <br />
                <asp:HiddenField ID="LBLID" runat="server" Value="-1" />
                <asp:HiddenField ID="pgd" runat="server" Value="" />
                <asp:HiddenField ID="XX" runat="server" Value="0" />
            </td>
        </tr>
    </table>
    &nbsp;
    <asp:ImageButton ID="btnAnnulla" runat="server" ImageUrl="~/NuoveImm/Img_Home.png"
        Style="z-index: 101; left: 533px; position: absolute; top: 494px; height: 20px;"
        ToolTip="Home" />
    <asp:ImageButton ID="btnVisualizza" runat="server" ImageUrl="~/NuoveImm/Img_Visualizza.png"
        Style="z-index: 102; left: 229px; position: absolute; top: 496px" ToolTip="Visualizza" />
    <asp:ImageButton ID="btnRicerca" runat="server" ImageUrl="~/NuoveImm/Img_NuovaRicerca.png"
        Style="z-index: 103; left: 399px; position: absolute; top: 495px" ToolTip="Nuova Ricerca" />
    <asp:Label ID="LBLPROGR" Style="z-index: 104; left: 163px; position: absolute; top: 308px"
        runat="server" Width="57px" Height="23px" Visible="False">Label</asp:Label>
    <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_Export_Grande.png"
        Style="z-index: 102; left: 13px; position: absolute; top: 496px" ToolTip="Export" />
    <br />
    <br />
    <table style="width: 100%;">
        <tr>
            <td>
                <div id="DivRoot" align="left">
                    <div style="overflow: hidden;" id="DivHeaderRow">
                    </div>
                    <div style="width: 636px; overflow: scroll; position: absolute; height: 354px; top: 53px;
                        left: 0px;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                        <asp:DataGrid ID="DataGrid1" runat="server" Font-Names="Arial" AutoGenerateColumns="False"
                            AllowPaging="True" Font-Size="8pt" PageSize="200" Style="z-index: 105; left: 0px;
                            position: absolute; top: 0px; width: 971px;" BackColor="White" Font-Bold="False"
                            Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False"
                            GridLines="None">
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="ID" HeaderText="ID">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="RAPPORTO" HeaderText="COD.CONTRATTO"></asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="COGNOME">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Font-Names="ARIAL" Font-Size="8pt" Text='<%# DataBinder.Eval(Container, "DataItem.COGNOME") %>'>
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
                                        <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.NOME") %>'>
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
                                <asp:TemplateColumn HeaderText="COD. FISCALE">
                                    <ItemTemplate>
                                        <asp:Label ID="Label5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_FISCALE") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COD_FISCALE") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="A.U.">
                                    <ItemTemplate>
                                        <asp:Label ID="Label3" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BANDO") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn Visible="False" DataField="PG" HeaderText="PG">
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:BoundColumn>
                                <asp:TemplateColumn HeaderText="NUMERO">
                                    <ItemTemplate>
                                        <asp:Label ID="Label6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox4" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PG") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="DATA PR.">
                                    <ItemTemplate>
                                        <asp:Label ID="Label7" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_PG") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox5" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.DATA_PG") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="STATO">
                                    <ItemTemplate>
                                        <asp:Label ID="Label8" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.STATO") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="TextBox6" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.STATO") %>'>
                                        </asp:TextBox>
                                    </EditItemTemplate>
                                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                        Font-Underline="False" Wrap="False" />
                                </asp:TemplateColumn>
                                <asp:BoundColumn DataField="ISE_ERP" HeaderText="ISE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="VSE" HeaderText="VSE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ISEE" HeaderText="ISEE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="VERIFICA" HeaderText="MOT. VERIFICA"></asp:BoundColumn>
                                <asp:BoundColumn DataField="OPERATORE" HeaderText="OPERATORE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="CAMBIO"></asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="#3366FF" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                                ForeColor="White" Wrap="False" Height="25px" />
                            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" Height="25px" />
                            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" Wrap="False" />
                            <PagerStyle Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                    <div id="DivFooterRow" style="overflow: hidden">
                    </div>
                </div>
            </td>
        </tr>
    </table>
    </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';
    </script>
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

        function ApriDichiarazione() {
            //btnVisualizza_Click
            document.getElementById('btnVisualizza').click();
        }

    </script>
</body>
</html>
