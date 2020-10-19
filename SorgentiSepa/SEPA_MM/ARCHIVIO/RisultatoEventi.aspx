<%@ Page Language="VB" AutoEventWireup="false" CodeFile="RisultatoEventi.aspx.vb" Inherits="ARCHIVIO_RisultatoEventi" %>

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
                Elenco Operazioni
                    <asp:Label ID="Label4" runat="server" Text="DD"></asp:Label>
                </strong></span>
                <br />
                <br />
                <table style="width:100%;">
                    <tr>
                        <td>
                        <div id="DivRoot" align="left">
    <div style="overflow: hidden;" id="DivHeaderRow">
    </div>
    <div style="width: 776px;height:350px;overflow:scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent" >
       
     <asp:DataGrid ID="Datagrid2" runat="server" AllowPaging="True" AutoGenerateColumns="False"
            Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False"
            Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" GridLines="None"
            PageSize="200" Style="z-index: 101; left: 9px; width: 800px;" TabIndex="1" BorderWidth="0px">
            <FooterStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <PagerStyle Mode="NumericPages" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" Wrap="False" />
            <AlternatingItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False"
                Font-Strikeout="False" Font-Underline="False" Wrap="False" />
            <Columns>
                <asp:BoundColumn DataField="DATA_ORA" HeaderText="DATA/ORA">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="OPERATORE" HeaderText="OPERATORE">
                </asp:BoundColumn>
                <asp:BoundColumn DataField="EVENTO" HeaderText="EVENTO">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="MOTIVAZIONE" HeaderText="NOTE">
                    <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" />
                </asp:BoundColumn>
            </Columns>
            <HeaderStyle BackColor="#3366FF" Font-Bold="True" Font-Italic="False" Font-Names="Arial"
                Font-Overline="False" Font-Size="8pt" Font-Strikeout="False" Font-Underline="False"
                ForeColor="White" Wrap="False" Height="25px" />
            <EditItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <SelectedItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
            <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                Font-Underline="False" Wrap="False" />
        </asp:DataGrid>
      
    </div>

    <div id="DivFooterRow" style="overflow:hidden">
    </div>
</div>
   
                        </td>
                    </tr>
                    <tr>
                        <td height="10">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <table style="width:100%;">
                                <tr>
                                    <td>
                                        &nbsp;</td>
                                    <td>
    <asp:ImageButton ID="btnExport" runat="server" ImageUrl="~/NuoveImm/Img_Export_XLS.png"
        Style="z-index: 102;" ToolTip="Visualizza"
        OnClientClick="document.getElementById('H1').value='1';" />
                                    </td>
                                    <td>
                <img onclick="document.location.href='RicercaEventi.aspx';" alt="" src="../NuoveImm/Img_NuovaRicerca.png"
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

        function ApriScheda() {
            if (document.getElementById('LBLID').value != '-1' && document.getElementById('LBLID').value != '') {
                today = new Date();
                var Titolo = 'Contratto' + today.getMinutes() + today.getSeconds();

                popupWindow = window.open('SchedaEustorgio.aspx?ID=' + document.getElementById('LBLID').value + '&COD=' + document.getElementById('Label3').value, Titolo, 'height=500,width=600');
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

