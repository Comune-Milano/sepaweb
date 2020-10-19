<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SelezionaUnitafuoriMI.aspx.vb"
    Inherits="ASS_SelezionaUnitafuoriMI" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        //        function MakeStaticHeader(gridId, height, width, headerHeight, isFooter) {
        //            var tbl = document.getElementById(gridId);
        //            if (tbl) {
        //                var DivHR = document.getElementById('DivHeaderRow');
        //                var DivMC = document.getElementById('DivMainContent');
        //                var DivFR = document.getElementById('DivFooterRow');

        //                DivHR.style.height = headerHeight + 'px';
        //                DivHR.style.width = (parseInt(width) - 0.4) + '%';
        //                DivHR.style.position = 'relative';
        //                DivHR.style.top = '0px';
        //                DivHR.style.zIndex = '10';
        //                DivHR.style.verticalAlign = 'top';

        //                //*** Set divMainContent Properties ****
        //                DivMC.style.width = width + '%';
        //                DivMC.style.height = height + 'px';
        //                DivMC.style.position = 'relative';
        //                DivMC.style.top = -headerHeight + 'px';
        //                DivMC.style.zIndex = '1';

        //                //*** Set divFooterRow Properties ****
        //                DivFR.style.width = (parseInt(width) - 1) + '%';
        //                DivFR.style.position = 'relative';
        //                DivFR.style.top = -headerHeight + 'px';
        //                DivFR.style.verticalAlign = 'top';
        //                DivFR.style.paddingtop = '2px';

        //                if (isFooter) {
        //                    var tblfr = tbl.cloneNode(true);
        //                    tblfr.removeChild(tblfr.getElementsByTagName('tbody')[0]);
        //                    var tblBody = document.createElement('tbody');
        //                    tblfr.style.width = '100%';
        //                    tblfr.cellSpacing = "0";
        //                    tblfr.border = "0px";
        //                    tblfr.rules = "none";
        //                    //*****In the case of Footer Row *******
        //                    tblBody.appendChild(tbl.rows[tbl.rows.length - 1]);
        //                    tblfr.appendChild(tblBody);
        //                    DivFR.appendChild(tblfr);
        //                }
        //                //****
        //                DivHR.appendChild(tbl.cloneNode(true));
        //            }
        //        }



        //        function OnScrollDiv(Scrollablediv) {
        //            document.getElementById('DivHeaderRow').scrollLeft = Scrollablediv.scrollLeft;
        //            document.getElementById('DivFooterRow').scrollLeft = Scrollablediv.scrollLeft;
        //        }
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
<body style="background-image: url(../NuoveImm/SfondoMascheraContratti.jpg); background-repeat: no-repeat;
    vertical-align: top;">
    <div id="caricamento" style="margin: 0px; background-color: #C0C0C0; width: 100%;
        height: 100%; position: fixed; top: 0px; left: 0px; filter: alpha(opacity='75');
        opacity: 0.75; background-color: #eeeeee; z-index: 500">
        <div style="position: fixed; top: 50%; left: 50%; width: 234px; height: 97px; margin-left: -117px;
            margin-top: -48px; background-image: url('Immagini/sfondo2.png');">
            <table style="width: 100%; height: 100%">
                <tr>
                    <td valign="middle" align="center">
                        <img alt="Caricamento" src="Immagini/load.gif" />
                        <br />
                        <br />
                    </td>
                </tr>
                <tr>
                    <td valign="middle" align="center">
                        Caricamento . . .
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%">
        <tr style="height: 35px; vertical-align: bottom">
            <td colspan="2">
                <span style="font-size: 14pt; color: #801f1c; font-family: Arial"><strong>Trovate
                    <asp:Label ID="lblNumTot" runat="server"></asp:Label>
                    unità per abbin. fuori Milano
                    <asp:Label ID="lblNumFuoriMi" runat="server"></asp:Label>
                </strong></span>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <div id="DivRoot" align="left">
                    <div style="overflow: hidden;" id="DivHeaderRow">
                    </div>
                    <div style="width: 776px; overflow: scroll;" onscroll="OnScrollDiv(this)" id="DivMainContent">
                        <asp:DataGrid ID="dgvAlloggi" runat="server" AutoGenerateColumns="False" Font-Names="Arial"
                            Font-Size="8pt" HorizontalAlign="Left" Width="930px" BorderWidth="0px">
                            <PagerStyle Mode="NumericPages" />
                            <Columns>
                                <asp:BoundColumn DataField="ID_UI" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COD_ALLOGGIO" HeaderText="COD" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="PROPRIETA" HeaderText="PROPRIETA" ReadOnly="True" Visible="False">
                                </asp:BoundColumn>
                                <asp:BoundColumn DataField="COD_ALLOGGIO" HeaderText="CODICE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="TIPO" HeaderText="TIPO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ZONA" HeaderText="ZONA"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COMUNEASS" HeaderText="COMUNE"></asp:BoundColumn>
                                <asp:BoundColumn DataField="VIA" HeaderText="INDIRIZ."></asp:BoundColumn>
                                <asp:BoundColumn DataField="NUM_CIVICO" HeaderText="CIV."></asp:BoundColumn>
                                <asp:BoundColumn DataField="N_ALL" HeaderText="N.ALL"></asp:BoundColumn>
                                <asp:BoundColumn DataField="NUM_LOCALI" HeaderText="LOC."></asp:BoundColumn>
                                <asp:BoundColumn DataField="PIANO" HeaderText="PIANO"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ELEVATORE" HeaderText="ASC."></asp:BoundColumn>
                                <asp:BoundColumn DataField="NETTA" HeaderText="SUP. NETTA"></asp:BoundColumn>
                                <asp:BoundColumn DataField="DATA_DISPONIBILITA1" HeaderText="DISP."></asp:BoundColumn>
                                <asp:BoundColumn DataField="PROPRIETA1" HeaderText="PROPR."></asp:BoundColumn>
                                <asp:BoundColumn DataField="Visualizza"></asp:BoundColumn>
                                <asp:BoundColumn DataField="ORDINAM" Visible="False"></asp:BoundColumn>
                                <asp:BoundColumn DataField="COD_COMUNE_ASS" Visible="False"></asp:BoundColumn>
                            </Columns>
                            <HeaderStyle BackColor="PapayaWhip" Font-Bold="True" Font-Names="Arial" Font-Size="8pt"
                                Height="25px" />
                        </asp:DataGrid>
                    </div>
                    <div id="DivFooterRow" style="overflow: hidden">
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:TextBox ID="txtmia" runat="server" BorderColor="White" BorderStyle="None" Font-Bold="True"
                    Font-Names="Arial" Font-Size="10pt" MaxLength="500" ReadOnly="True" Style="z-index: 500;
                    background-color: transparent;" Width="100%">Nessuna Selezione</asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>
                            <asp:Label ID="Label3" runat="server" Text="*Alloggio appartenente al comune scelto per l'assegnaz."
                                ForeColor="Black" Font-Names="Arial" Font-Size="8pt" Font-Bold="True"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Label2" runat="server" Text="*Alloggio con rapporto num.componenti/dimensione valido"
                                ForeColor="black" Font-Names="Arial" Font-Size="8pt" BackColor="#CEFFCE"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 98%">
                    <tr>
                        <td style="width: 80%">
                            &nbsp;
                        </td>
                        <td>
                            <asp:ImageButton ID="btnProcedi" runat="server" ImageUrl="~/NuoveImm/Img_Procedi.png"
                                OnClientClick="caricamentoincorso();" ToolTip="Procedi" />
                        </td>
                        <td>
                            <img alt="Esci" title="Esci dalla Maschera" src="../NuoveImm/Img_Esci_AMM.png" onclick="self.close();"
                                style="cursor: pointer" />
                        </td>
                    </tr>
                </table>
                <asp:HiddenField ID="idSelected" runat="server" Value="0" />
                <asp:HiddenField ID="cod_comune_ass" runat="server" Value="0" />
                <asp:HiddenField ID="idDich" runat="server" Value="0" />
            </td>
        </tr>
    </table>
    </form>
    <script type="text/javascript" language="javascript">
        var Selezionato;
        var OldColor;
        var SelColo;

        function caricamentoincorso() {
            if (typeof (Page_ClientValidate) == 'function') {
                Page_ClientValidate();
                if (Page_IsValid) {
                    if (document.getElementById('caricamento') != null) {
                        document.getElementById('caricamento').style.visibility = 'visible';
                    };
                }
                else {
                    alert('ATTENZIONE! Ci sono delle incongruenze nei dati della pagina!');
                };
            }
            else {
                if (document.getElementById('caricamento') != null) {
                    document.getElementById('caricamento').style.visibility = 'visible';
                };
            };
        };
        initialize();
        function initialize() {
            window.focus();
            document.getElementById('caricamento').style.visibility = 'hidden';
        };
    </script>
</body>
</html>
