<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatoDatiAE.aspx.vb" Inherits="Contratti_RisultatoDatiAE" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script type="text/javascript">
    var Uscita;
    var Selezionato;

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
<body bgcolor="White">
    <script type="text/javascript">
        document.onkeydown = $onkeydown;
    </script>
    <form id="Form1" method="post" runat="server">
    &nbsp;&nbsp;
    <table style="left: 0px; background-image: url(../NuoveImm/SfondoMascheraContratti.jpg);
        width: 800px; position: absolute; top: 0px">
        <tr>
            <td style="width: 800px">
                
                
                <br />
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>&nbsp; 
                Elenco Rapporti UI </strong>
                <asp:Label ID="Label4" runat="server" style="font-weight: 700" Text="-"></asp:Label>
                </span><br />
                <br />
                <table style="width:100%;">
                    <tr>
                        <td>
                         <div id="DivRoot" align="left">
    <div style="overflow: hidden;" id="DivHeaderRow">
    </div>
    <div style="width: 765px;overflow:scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent" >
    
        <asp:DataGrid ID="Datagrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
            PageSize="200" Style="z-index: 101; left: 9px; " TabIndex="1" 
            Width="2500px" CellPadding="4" ForeColor="#333333">
            <FooterStyle Font-Bold="True" Wrap="False" BackColor="#507CD1" 
                ForeColor="White" />
            <PagerStyle Wrap="False" BackColor="#2461BF" ForeColor="White" 
                HorizontalAlign="Center" />
            <AlternatingItemStyle Wrap="False" BackColor="White" />
            <Columns>
                <asp:BoundColumn DataField="ID" HeaderText="ID" Visible="False">
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="COD_CONTRATTO" HeaderText="CODICE" ReadOnly="True">
                    <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" Wrap="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CF_PIVA" HeaderText="COD.FISC. INTESTATARIO">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="COD_TIPOLOGIA_CONTR_LOC" HeaderText="TIPOLOGIA">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DECORRENZA" HeaderText="DATA DECORRENZA">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SLOGGIO" HeaderText="DATA SLOGGIO">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="COD_UNITA_IMMOBILIARE" HeaderText="CODICE UI">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="INDIRIZZO_UI" HeaderText="INDIRIZZO">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="CIVICO_UI" HeaderText="CIVICO"></asp:BoundColumn>
                <asp:BoundColumn DataField="SCALA_UI" HeaderText="SCALA"></asp:BoundColumn>
                <asp:BoundColumn DataField="PIANO_UI" HeaderText="PIANO"></asp:BoundColumn>
                <asp:BoundColumn DataField="INTERNO" HeaderText="INTERNO"></asp:BoundColumn>
                <asp:BoundColumn DataField="NUM_REGISTRAZIONE" HeaderText="NUM.REGISTRAZIONE">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="SERIE_REGISTRAZIONE" 
                    HeaderText="SERIE REGISTRAZIONE"></asp:BoundColumn>
                <asp:BoundColumn DataField="REGISTRAZIONE" HeaderText="DATA_REGISTRAZIONE">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TIPO_VERSAMENTO" HeaderText="MOD.VERSAMENTO">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="COD_UFFICIO_REG" HeaderText="COD.UFFICIO">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NRO_ASSEGNAZIONE_PG" HeaderText="NUM.PROTOCOLLO AE">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="DATA_PG" HeaderText="DATA ASS. PG">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="TIPO_POSIZIONE" HeaderText="TIPO_POSIZIONE">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="MODO_PAGAMENTO" HeaderText="MOD. PAGAMENTO">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="NOTE_REGISTRAZIONE" HeaderText="NOTE">
                </asp:BoundColumn>
            </Columns>
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                ForeColor="White" Wrap="False" />
            <EditItemStyle Wrap="False" BackColor="#2461BF" />
            <SelectedItemStyle Font-Bold="True" Wrap="False" BackColor="#D1DDF1" 
                ForeColor="#333333" />
            <ItemStyle Wrap="False" BackColor="#EFF3FB" />
        </asp:DataGrid>
    </div>

    <div id="DivFooterRow" style="overflow:hidden">
    </div>
</div>
                        </td>
                    </tr>
                    <tr>
                        <td>
                <asp:TextBox ID="TextBox3" runat="server" Font-Bold="True" Font-Names="ARIAL" Font-Size="12pt"
                    Style="border-right: white 1px solid; border-top: white 1px solid; border-left: white 1px solid;
                    border-bottom: white 1px solid; left: -1px; top: 45px;" Width="777px">Nessuna Selezione</asp:TextBox></td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp; &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width:100%;">
                                <tr>
                                    <td>
                <img onclick="ApriContratto();" alt="" src="../NuoveImm/Img_Visualizza.png" style="cursor: pointer;" id="Visualizza" /></td>
                                    <td>
    <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_Export_XLS.png"
        Style="z-index: 102;" ToolTip="Visualizza"
        OnClientClick="document.getElementById('H1').value='1';" Visible="False" />
                                    </td>
                                    <td>
                <img onclick="document.location.href='DatiRegistrazioneS1.aspx';" alt="" src="../NuoveImm/Img_NuovaRicerca.png"
                    style="cursor: pointer;" /></td>
                                    <td>
                <img onclick="document.location.href='pagina_home.aspx';" alt="" src="../NuoveImm/Img_Home.png"
                    style="cursor: pointer; height: 20px;" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
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
               
                <asp:ImageButton ID="ImageButton1" runat="server" CausesValidation="False" ImageUrl="~/NuoveImm/Falso.png" />
                <br />
                &nbsp;&nbsp;
                <asp:HiddenField ID="H1" runat="server" Value="0" />
                <asp:HiddenField ID="Label3" runat="server" />
                 <asp:HiddenField ID="LBLID" runat="server" />
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <br />
            </td>
        </tr>
    </table>
    &nbsp;
    <asp:Label ID="LBLPROGR" Style="z-index: 104; left: 404px; position: absolute; top: 500px"
        runat="server" Width="57px" Height="23px" Visible="False" TabIndex="-1">Label</asp:Label>
    &nbsp; &nbsp;&nbsp;&nbsp;
    &nbsp;
    </form>
    <script language="javascript" type="text/javascript">
        document.getElementById('dvvvPre').style.visibility = 'hidden';

        function ApriContratto() {
            if (document.getElementById('LBLID').value != '-1' && document.getElementById('LBLID').value != '') {
                today = new Date();
                var Titolo = 'Contratto' + today.getMinutes() + today.getSeconds();

                popupWindow = window.open('Contratto.aspx?ID=' + document.getElementById('LBLID').value + '&COD=' + document.getElementById('Label3').value, Titolo, 'height=780,width=1160');
                popupWindow.focus();
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

