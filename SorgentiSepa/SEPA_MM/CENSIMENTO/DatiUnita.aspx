<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DatiUnita.aspx.vb" Inherits="PED_DatiUnita" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Scheda Dati</title>
    <script type="text/javascript" src="tabber.js"></script>
    <link rel="stylesheet" type="text/css" media="screen"/>
    <script type="text/javascript">
    document.write('<style type="text/css">.tabber{display:none;}<\/style>');
    
    if (navigator.appName=='Microsoft Internet Explorer') {
        window.resizeTo(760,600);
    }
    else
    {
        window.innerWidth=760;
        window.innerHeight=600;
    }
    </script>

    <link href="example.css" rel="stylesheet" type="text/css" />
</head>
<script type="text/javascript" src="function.js"></script>
<body  bgcolor="#f5f5f5">
    <form id="form1" runat="server">
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="15%" style="height: 19px">
                    <span style="font-size: 8pt; font-family: Arial">Codice U.I.</span></td>
                <td width="25%" style="height: 19px">
                    <asp:Label ID="lblUI" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="position: static"></asp:Label></td>
                <td width="10%" style="height: 19px">
                    <span style="font-size: 8pt; font-family: Arial">Unità Princ.</span></td>
                <td width="20%" style="height: 19px">
                    <span style="font-size: 8pt; font-family: Arial">
                        <asp:Label ID="lblPrincipale" runat="server" Font-Bold="True" Font-Names="arial"
                            Font-Size="8pt" Style="position: static"></asp:Label></span></td>
                <td width="10%" style="height: 19px">
                    <span style="font-size: 8pt; font-family: Arial">Unità Corr.</span></td>
                <td width="20%" style="height: 19px">
                    <asp:Label ID="lblPertinenze" runat="server" Font-Bold="True" Font-Names="arial"
                        Font-Size="8pt" Style="position: static"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <span style="font-size: 8pt; font-family: Arial">Foglio</span></td>
                <td>
                    <asp:Label ID="lbFoglio" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="position: static"></asp:Label></td>
                <td>
                    <span style="font-size: 8pt; font-family: Arial">Particella</span></td>
                <td>
                    <asp:Label ID="lblParticella" runat="server" Font-Bold="True" Font-Names="arial"
                        Font-Size="8pt" Style="position: static"></asp:Label></td>
                <td>
                    <span style="font-size: 8pt; font-family: Arial">Sub</span></td>
                <td>
                    <asp:Label ID="lblSub" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="position: static"></asp:Label></td>
            </tr>
            <tr>
                <td style="height: 19px">
                    <span style="font-size: 8pt; font-family: Arial">Tipo Immobile</span></td>
                <td style="height: 19px">
                    <asp:Label ID="lblTipoImm" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="position: static"></asp:Label></td>
                <td style="height: 19px">
                    <span style="font-size: 8pt; font-family: Arial">Sup. Conv.</span></td>
                <td style="height: 19px">
                    <asp:Label ID="lblSup" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="position: static"></asp:Label></td>
                <td style="height: 19px">
                    <span style="font-size: 8pt; font-family: Arial">Millesimi</span></td>
                <td style="height: 19px">
                    <asp:Label ID="lblMillesimi" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="position: static"></asp:Label></td>
            </tr>
            <tr>
                <td style="height: 19px">
                    <span style="font-size: 8pt; font-family: Arial">Provenienza</span></td>
                <td style="height: 19px">
                    <asp:Label ID="lblProv" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="position: static"></asp:Label></td>
                <td style="height: 19px">
                    <span style="font-size: 8pt; font-family: Arial">Gestore</span></td>
                <td style="height: 19px">
                    <asp:Label ID="lblGestore" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="position: static"></asp:Label></td>
                <td style="height: 19px">
                    <span style="font-size: 8pt; font-family: Arial">Piano</span></td>
                <td style="height: 19px">
                    <asp:Label ID="lblPiano" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="position: static"></asp:Label></td>
            </tr>
            <tr>
                <td style="height: 19px">
                    <span style="font-size: 8pt; font-family: Arial">Indirizzo</span></td>
                <td style="height: 19px">
                    <asp:Label ID="lblIndirizzo" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="position: static"></asp:Label></td>
                <td style="height: 19px">
                    <span style="font-size: 8pt; font-family: Arial">C.A.P./Città</span></td>
                <td style="height: 19px">
                    <asp:Label ID="lblCAPCitta" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="position: static"></asp:Label></td>
                <td style="height: 19px">
                    <span style="font-size: 8pt; font-family: Arial">Scala</span></td>
                <td style="height: 19px">
                    <asp:Label ID="lblScala" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="position: static"></asp:Label></td>
            </tr>
            <tr>
                <td style="height: 19px">
                    <span style="font-size: 8pt; font-family: Arial">Tipo Disponibilità</span></td>
                <td style="height: 19px">
                    <asp:Label ID="lblDisp" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="position: static"></asp:Label></td>
                <td style="height: 19px">
                    <span style="font-size: 8pt; font-family: Arial">Censimento</span></td>
                <td style="height: 19px">
                    <asp:Label ID="lblCensimento" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="position: static"></asp:Label></td>
                <td style="height: 19px">
                </td>
                <td style="height: 19px">
                </td>
            </tr>
        </table>
        <span style="font-size: 8pt; font-family: Arial"></span>
        <table cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td width="15%" style="height: 21px">
                    <span style="font-size: 8pt; font-family: Arial">Dati Contrattuali</span></td>
                <td width="35%" style="height: 21px">
                    <asp:Label ID="lblOccupante" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="position: static"></asp:Label></td>
                <td style="height: 21px" width="50%">
                    <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="position: static"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <span style="font-size: 8pt; font-family: Arial">Complesso</span></td>
                <td style="height: 19px">
                    <asp:Label ID="lblComplesso" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="position: static"></asp:Label></td>
                <td style="height: 19px">
                </td>
            </tr>
            <tr>
                <td>
                    <span style="font-size: 8pt; font-family: Arial">Tipo Complesso</span></td>
                <td style="height: 19px">
                    <asp:Label ID="lblTipoComplesso" runat="server" Font-Bold="True" Font-Names="arial"
                        Font-Size="8pt" Style="position: static"></asp:Label></td>
                <td style="height: 19px">
                </td>
            </tr>
            <tr>
                <td>
                    <span style="font-size: 8pt; font-family: Arial">Edificio</span></td>
                <td style="height: 19px">
                    <asp:Label ID="lblEdificio" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="position: static"></asp:Label></td>
                <td style="height: 19px">
                    <asp:Label ID="Label2" runat="server" Font-Bold="True" Font-Names="arial" Font-Size="8pt"
                        Style="position: static"></asp:Label></td>
            </tr>
        </table>
            
                <table cellpadding="0" cellspacing="0" style="left: 9px; position: absolute; top: 400px"
                    width="100%" id="TABLE1">
                    <tr>
                        <td width="50%" style="height: 19px">
        <asp:Label ID="lblAvviso" runat="server" Style="z-index: 100; left: 16px; position: static;
            top: 374px" Text="Label" Visible="False" Width="246px"></asp:Label>
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/immagini/googleMaps.gif" style="cursor: pointer" /></td>
                        <td width="50%" style="height: 19px" align="right">
                            <asp:Button ID="btnStampa" runat="server" Style="z-index: 106; left: 667px; position: static;
            top: 369px" Text="Versione Stampabile" OnClientClick="Stampabile()" />
                            &nbsp;&nbsp; &nbsp;<asp:Button ID="btnChiudi" runat="server" Style="z-index: 106; left: 667px; position: static;
            top: 369px" Text="Chiudi" /></td>
                    </tr>
                </table>
    <div class="tabber" style="width:auto">
        <% TabFotoComplesso()%><% TabFotoEdificio()%><% TabFotoDWG()%>
        &nbsp;</div>
    </form>
</body>
</html>