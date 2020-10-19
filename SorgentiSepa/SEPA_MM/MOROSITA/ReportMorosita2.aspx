<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportMorosita2.aspx.vb"
    Inherits="MOROSITA_ReportMorosita2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .font5
        {
            color: black;
            font-size: 8.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , serif;
        }
        .font0
        {
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , sans-serif;
        }
        .style1
        {
            height: 33.75pt;
            width: 100%;
            color: #69676D;
            font-size: 18.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: "Lucida Sans" , sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border-left: 1.0pt solid windowtext;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top: 1.0pt solid windowtext;
            border-bottom-style: none;
            border-bottom-color: inherit;
            border-bottom-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #D9D9D9;
        }
        .style2
        {
            height: 17.25pt;
            width: 100%;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: normal;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top: 1.0pt solid windowtext;
            border-bottom: 1.0pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .style3
        {
            height: 17.25pt;
            width: 100%;
            color: #262626;
            font-size: 11.0pt;
            font-weight: 700;
            font-style: italic;
            text-decoration: none;
            font-family: "Book Antiqua" , serif;
            text-align: left;
            vertical-align: bottom;
            white-space: normal;
            border-left: 1.0pt solid windowtext;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top: 1.0pt solid windowtext;
            border-bottom: 1.0pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #D9D9D9;
        }
        .style4
        {
            height: 20.1pt;
            color: #262626;
            font-size: 15.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left: 1.0pt solid windowtext;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top: 1.0pt solid windowtext;
            border-bottom: 1.0pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #9688A6;
        }
        .style5
        {
            height: 20.1pt;
            width: 100%;
            color: #262626;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border-left: 1.0pt solid windowtext;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top: 1.0pt solid windowtext;
            border-bottom: 1.0pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #DFDBE3;
        }
        .style6
        {
            height: 132.0pt;
            width: 376pt;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border-left: .5pt solid windowtext;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top-style: none;
            border-top-color: inherit;
            border-top-width: medium;
            border-bottom-style: none;
            border-bottom-color: inherit;
            border-bottom-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .style7
        {
            width: 376pt;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border-left: .5pt solid windowtext;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top-style: none;
            border-top-color: inherit;
            border-top-width: medium;
            border-bottom-style: none;
            border-bottom-color: inherit;
            border-bottom-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .style8
        {
            height: 82.5pt;
            width: 188pt;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border-left: .5pt solid windowtext;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top-style: none;
            border-top-color: inherit;
            border-top-width: medium;
            border-bottom-style: none;
            border-bottom-color: inherit;
            border-bottom-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .style9
        {
            width: 188pt;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , sans-serif;
            text-align: left;
            vertical-align: middle;
            white-space: normal;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .style10
        {
            height: 49.5pt;
            width: 94pt;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .style11
        {
            width: 94pt;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border-style: none;
            border-color: inherit;
            border-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .style12
        {
            height: 16.5pt;
            width: 376pt;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border-left: .5pt solid windowtext;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top: .5pt solid windowtext;
            border-bottom: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #EAE7ED;
        }
        .style13
        {
            width: 376pt;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border-left: .5pt solid windowtext;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top: .5pt solid windowtext;
            border-bottom: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #EAE7ED;
        }
        .style14
        {
            height: 99.75pt;
            width: 94pt;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border-left: .5pt solid windowtext;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top-style: none;
            border-top-color: inherit;
            border-top-width: medium;
            border-bottom-style: none;
            border-bottom-color: inherit;
            border-bottom-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .style15
        {
            width: 94pt;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top: .5pt solid windowtext;
            border-bottom-style: none;
            border-bottom-color: inherit;
            border-bottom-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .style16
        {
            width: 94pt;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border-left: .5pt solid windowtext;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top-style: none;
            border-top-color: inherit;
            border-top-width: medium;
            border-bottom-style: none;
            border-bottom-color: inherit;
            border-bottom-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .style17
        {
            width: 94pt;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , sans-serif;
            text-align: center;
            vertical-align: middle;
            white-space: normal;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right: .5pt solid windowtext;
            border-top-style: none;
            border-top-color: inherit;
            border-top-width: medium;
            border-bottom-style: none;
            border-bottom-color: inherit;
            border-bottom-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .style18
        {
            height: 16.5pt;
            width: 100%;
            color: black;
            font-size: 11.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , serif;
            text-align: center;
            vertical-align: bottom;
            white-space: normal;
            border-left: 1.0pt solid windowtext;
            border-right: .5pt solid windowtext;
            border-top: 1.0pt solid windowtext;
            border-bottom: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #EDE4F1;
        }
        .style19
        {
            height: 26.25pt;
            width: 94pt;
            color: windowtext;
            font-size: 10.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            vertical-align: middle;
            white-space: normal;
            border-left: 1.0pt solid windowtext;
            border-right: .5pt solid windowtext;
            border-top: .5pt solid windowtext;
            border-bottom: 1.0pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #EDE4F1;
        }
        .style20
        {
            width: 94pt;
            color: windowtext;
            font-size: 10.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            vertical-align: middle;
            white-space: normal;
            border-left: .5pt solid windowtext;
            border-right: .5pt solid windowtext;
            border-top: .5pt solid windowtext;
            border-bottom: 1.0pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #EDE4F1;
        }
        .style21
        {
            width: 94pt;
            color: windowtext;
            font-size: 10.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            text-align: right;
            vertical-align: middle;
            white-space: normal;
            border-left: .5pt solid windowtext;
            border-right: .5pt solid windowtext;
            border-top: .5pt solid windowtext;
            border-bottom: 1.0pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #EDE4F1;
        }
        .style22
        {
            width: 94pt;
            color: windowtext;
            font-size: 10.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            vertical-align: middle;
            white-space: normal;
            border-left: .5pt solid windowtext;
            border-right: .5pt solid windowtext;
            border-top: .5pt solid windowtext;
            border-bottom: 1.0pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #EDE4F1;
        }
        .style23
        {
            width: 94pt;
            color: windowtext;
            font-size: 10.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: Arial, sans-serif;
            vertical-align: middle;
            white-space: normal;
            border-left: .5pt solid windowtext;
            border-right: 1.0pt solid windowtext;
            border-top: .5pt solid windowtext;
            border-bottom: 1.0pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #EDE4F1;
        }
        .style24
        {
            height: 50.25pt;
            width: 100%;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: normal;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top: 1.0pt solid windowtext;
            border-bottom-style: none;
            border-bottom-color: inherit;
            border-bottom-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .style25
        {
            height: 20.25pt;
            color: #262626;
            font-size: 15.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: nowrap;
            border-left: 1.0pt solid windowtext;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top: 1.0pt solid windowtext;
            border-bottom: 1.0pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #C8AED7;
        }
        .style26
        {
            height: 53.25pt;
            width: 100%;
            color: black;
            font-size: 14.0pt;
            font-weight: 700;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , serif;
            text-align: left;
            vertical-align: top;
            white-space: normal;
            border-left: 1.0pt solid windowtext;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top: 1.0pt solid windowtext;
            border-bottom-style: none;
            border-bottom-color: inherit;
            border-bottom-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .style27
        {
            height: 33.75pt;
            width: 100%;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: normal;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top: 1.0pt solid windowtext;
            border-bottom-style: none;
            border-bottom-color: inherit;
            border-bottom-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .style28
        {
            height: 49.5pt;
            width: 100%;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , sans-serif;
            text-align: center;
            vertical-align: bottom;
            white-space: normal;
            border-left-style: none;
            border-left-color: inherit;
            border-left-width: medium;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top: 1.0pt solid windowtext;
            border-bottom-style: none;
            border-bottom-color: inherit;
            border-bottom-width: medium;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
        }
        .style38
        {
            height: 16.5pt;
            width: 100%;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , serif;
            text-align: left;
            vertical-align: bottom;
            white-space: normal;
            border-left: 1.0pt solid windowtext;
            border-right: .5pt solid windowtext;
            border-top: 1.0pt solid windowtext;
            border-bottom: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #ECE3C1;
        }
        .style39
        {
            height: 16.5pt;
            width: 100%;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , serif;
            text-align: left;
            vertical-align: bottom;
            white-space: normal;
            border-left: 1.0pt solid windowtext;
            border-right: .5pt solid windowtext;
            border-top: .5pt solid windowtext;
            border-bottom: .5pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #ECE3C1;
        }
        .style40
        {
            height: 16.5pt;
            width: 100%;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , serif;
            text-align: left;
            vertical-align: top;
            white-space: normal;
            border-left: 1.0pt solid windowtext;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top: .5pt solid windowtext;
            border-bottom: 1.0pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #ECE3C1;
        }
        .style41
        {
            height: 17.25pt;
            width: 100%;
            color: black;
            font-size: 11.0pt;
            font-weight: 400;
            font-style: normal;
            text-decoration: none;
            font-family: "Book Antiqua" , serif;
            text-align: left;
            vertical-align: top;
            white-space: normal;
            border-left: 1.0pt solid windowtext;
            border-right-style: none;
            border-right-color: inherit;
            border-right-width: medium;
            border-top: .5pt solid windowtext;
            border-bottom: 1.0pt solid windowtext;
            padding-left: 1px;
            padding-right: 1px;
            padding-top: 1px;
            background: #ECE3C1;
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="1" cellpadding="0" cellspacing="0" style="border-collapse: collapse;
            width: 100%" width="1000">
            <colgroup>
                <col span="8" style="width: 94pt" width="125" />
            </colgroup>
            <tr style="height: 16.5pt">
                <td class="style1" colspan="8" height="45" width="1000">
                    Report situazione morosità
                </td>
            </tr>
            <tr style="height: 17.25pt">
                <td class="style2" colspan="8" width="1000">
                    &nbsp;
                </td>
            </tr>
            <tr style="height: 17.25pt">
                <td class="style3" colspan="8" width="1000">
                    Riepilogo dei filtri di estrazione inseriti per ottenere il risultato (solo filtri
                    valorizzati)
                    <br />
                    <asp:Label ID="parametriDiRicerca" Text="" runat="server" />
                </td>
            </tr>
            <tr style="height: 17.25pt">
                <td class="style2" colspan="8" width="1000">
                    &nbsp;
                </td>
            </tr>
            <tr style="height: 20.1pt">
                <td class="style4" colspan="8">
                    Morosità inferiore a 3 mesi
                </td>
            </tr>
            <tr style="height: 20.1pt">
                <td class="style5" colspan="8" width="1000">
                    Assegnatari a debito
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style6" colspan="4" height="176" rowspan="3" width="500">
                    visualizza n° Assegnatari morosi
                    <asp:TextBox ID="NAssegnatari3Mesi" runat="server" Style="text-align: right;
                        border: 0px" Font-Names="Arial" Font-Size="10pt" Height="18px"></asp:TextBox>
                </td>
                <td class="style7" colspan="4" width="500">
                    Importo morosità totale
                    <asp:TextBox ID="ImportoMorositaTotale3Mesi" runat="server" Style="text-align: right;
                        border: 0px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style8" colspan="2" height="110" rowspan="2" width="250">
                    Importo morosità ex gestori <font class="font5">(3)</font>
                    <br />
                    <asp:TextBox ID="ImportoMorositaExGestori3Mesi" runat="server" Style="text-align: right;
                        border: 0px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
                </td>
                <td class="style9" colspan="2" width="250">
                    Importo morosità Gestore
                    <asp:TextBox ID="ImportoMorositaALER3Mesi" runat="server" Style="text-align: right;
                        border: 0px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style10" height="66" width="125">
                    Canoni <font class="font5">(1)</font>
                    <br />
                    <asp:TextBox ID="ImportoMorositaCanoni3Mesi" runat="server" Style="text-align: right;
                        border: 0px" Font-Names="Arial" Font-Size="10pt" Height="18px"></asp:TextBox>
                </td>
                <td class="style11" width="125">
                    Servizi <font class="font5">(2)</font>
                    <br />
                    <asp:TextBox ID="ImportoMorositaServizi3Mesi" runat="server" Style="text-align: right;
                        border: 0px" Font-Names="Arial" Font-Size="10pt"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style12" colspan="4" width="500">
                    Dettaglio Assegnatari morosi
                </td>
                <td class="style13" colspan="4" width="500">
                    Dettaglio Morosità
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style14" height="133" width="125">
                    Visualizza n° Assegnatari a cui è stata inviata M.M.
                    <br />
                    <asp:TextBox ID="NAssegnatariMM3Mesi" runat="server" Style="text-align: right;
                        border: 0px" Font-Names="Arial" Font-Size="10pt" Height="18px"></asp:TextBox>
                </td>
                <td class="style15" width="125">
                    Visualizza n° Assegnatari con richiesta di Contributo Solidarietà in essere
                    <br />
                    <asp:TextBox ID="NAssegnatariCS3Mesi" runat="server" Style="text-align: right;
                        border: 0px" Font-Names="Arial" Font-Size="10pt" Height="18px"></asp:TextBox>
                </td>
                <td class="style15" width="125">
                    Visualizza n° Assegnatari con Pratiche Legali in essere
                    <br />
                    <asp:TextBox ID="NAssegnatariPL3Mesi" runat="server" Style="text-align: right;
                        border: 0px" Font-Names="Arial" Font-Size="10pt" Height="18px"></asp:TextBox>
                </td>
                <td class="style11" width="125">
                    Visualizza n° altri assegnatari
                    <br />
                    <asp:TextBox ID="NAssegnatariAltri3Mesi" runat="server" Style="text-align: right;
                        border: 0px" Font-Names="Arial" Font-Size="10pt" Height="18px"></asp:TextBox>
                </td>
                <td class="style16" width="125">
                    Importo morosità assegnatari con M.M.
                    <br />
                    <asp:TextBox ID="ImportoMorositaMM3Mesi" runat="server" Style="text-align: right;
                        border: 0px" Font-Names="Arial" Font-Size="10pt" Height="18px"></asp:TextBox>
                </td>
                <td class="style11" width="125">
                    Importo morosità assegnatari con rich. C.S.
                    <br />
                    <asp:TextBox ID="ImportoMorositaCS3Mesi" runat="server" Style="text-align: right;
                        border: 0px" Font-Names="Arial" Font-Size="10pt" Height="18px"></asp:TextBox>
                </td>
                <td class="style11" width="125">
                    Importo morosità assegnatari con P.L.
                    <br />
                    <asp:TextBox ID="ImportoMorositaPL3Mesi" runat="server" Style="text-align: right;
                        border: 0px" Font-Names="Arial" Font-Size="10pt" Height="18px"></asp:TextBox>
                </td>
                <td class="style17" width="125">
                    Importo morosità altri assegnatari
                    <br />
                    <asp:TextBox ID="ImportoMorositaAtri3Mesi" runat="server" Style="text-align: right;
                        border: 0px" Font-Names="Arial" Font-Size="10pt" Height="18px"></asp:TextBox>
                </td>
            </tr>
            <tr style="height: 17.25pt">
                <td class="style2" colspan="8" width="1000">
                    &nbsp;
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style18" colspan="8" width="1000">
                    Dettaglio inquilini (*)
                </td>
            </tr>
            <tr>
                <td colspan="8">
                    <asp:DataGrid runat="server" ID="DataGridInquilini3Mesi" AutoGenerateColumns="False" Width="100%" BackColor="#EDE4F1">
                        <Columns>
                            <asp:BoundColumn HeaderText="Codice contrattuale" DataField="CODICE_CONTRATTO"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Intestatario" DataField="INTESTATARIO2"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Debito" DataField="DEBITO2"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Tipologia rapporto" DataField="COD_TIPOLOGIA_CONTR_LOC"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Posizione contrattuale" DataField="POSIZIONE_CONTRATTO"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Unità immobiliare" DataField="COD_UNITA_IMMOBILIARE"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Tipo UI" DataField="COD_TIPOLOGIA"></asp:BoundColumn>
                            <asp:BoundColumn HeaderText="Indirizzo completo" DataField="INDIRIZZO"></asp:BoundColumn>
                        </Columns>
                    </asp:DataGrid>
                </td>
            </tr>
            <tr style="height: 17.25pt">
                <td class="style2" colspan="8" width="1000">
                    &nbsp;
                </td>
            </tr>
            <tr style="height: 20.1pt">
                <td class="style4" colspan="8">
                    Morosità da 3 a 12 mesi
                </td>
            </tr>
            <tr style="height: 20.1pt">
                <td class="style5" colspan="8" width="1000">
                    Assegnatari a debito
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style6" colspan="4" height="176" rowspan="3" width="500">
                    visualizza n° Assegnatari morosi
                </td>
                <td class="style7" colspan="4" width="500">
                    Importo morosità totale
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style8" colspan="2" height="110" rowspan="2" width="250">
                    Importo morosità ex gestori <font class="font5">(3)</font>
                </td>
                <td class="style9" colspan="2" width="250">
                    Importo morosità Gestore
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style10" height="66" width="125">
                    Canoni <font class="font5">(1)</font>
                </td>
                <td class="style11" width="125">
                    Servizi <font class="font5">(2)</font>
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style12" colspan="4" width="500">
                    Dettaglio Assegnatari morosi
                </td>
                <td class="style13" colspan="4" width="500">
                    Dettaglio Morosità
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style14" height="133" width="125">
                    Visualizza n° Assegnatari a cui è stata inviata M.M.
                </td>
                <td class="style11" width="125">
                    Visualizza n° Assegnatari con richiesta di Contributo Solidarietà in essere
                </td>
                <td class="style11" width="125">
                    Visualizza n° Assegnatari con Pratiche Legali in essere
                </td>
                <td class="style11" width="125">
                    Visualizza n° altri assegnatari
                </td>
                <td class="style16" width="125">
                    Importo morosità assegnatari con M.M.
                </td>
                <td class="style11" width="125">
                    Importo morosità assegnatari con rich. C.S.
                </td>
                <td class="style11" width="125">
                    Importo morosità assegnatari con P.L.
                </td>
                <td class="style17" width="125">
                    Importo morosità altri assegnatari
                </td>
            </tr>
            <tr style="height: 17.25pt">
                <td class="style2" colspan="8" width="1000">
                    &nbsp;
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style18" colspan="8" width="1000">
                    Dettaglio inquilini (*)
                </td>
            </tr>
            <tr style="height: 26.25pt">
                <td class="style19" width="125">
                    Codice Contrattuale
                </td>
                <td class="style20" width="125">
                    Intestatario
                </td>
                <td class="style21" width="125">
                    Debito
                </td>
                <td class="style22" width="125">
                    Tipologia Rapporto
                </td>
                <td class="style20" width="125">
                    Posizione Contrattuale
                </td>
                <td class="style20" width="125">
                    Unità Immobiliare
                </td>
                <td class="style22" width="125">
                    Tipo UI
                </td>
                <td class="style23" width="125">
                    Indirizzo completo
                </td>
            </tr>
            <tr style="height: 17.25pt">
                <td class="style2" colspan="8" width="1000">
                    &nbsp;
                </td>
            </tr>
            <tr style="height: 20.1pt">
                <td class="style4" colspan="8">
                    Morosità superiore 12 mesi
                </td>
            </tr>
            <tr style="height: 20.1pt">
                <td class="style5" colspan="8" width="1000">
                    Assegnatari a debito
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style6" colspan="4" height="176" rowspan="3" width="500">
                    visualizza n° Assegnatari morosi
                </td>
                <td class="style7" colspan="4" width="500">
                    Importo morosità totale
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style8" colspan="2" height="110" rowspan="2" width="250">
                    Importo morosità ex gestori <font class="font5">(3)</font>
                </td>
                <td class="style9" colspan="2" width="250">
                    Importo morosità Gestore
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style10" height="66" width="125">
                    Canoni <font class="font5">(1)</font>
                </td>
                <td class="style11" width="125">
                    Servizi <font class="font5">(2)</font>
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style12" colspan="4" width="500">
                    Dettaglio Assegnatari morosi
                </td>
                <td class="style13" colspan="4" width="500">
                    Dettaglio Morosità
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style14" height="133" width="125">
                    Visualizza n° Assegnatari a cui è stata inviata M.M.
                </td>
                <td class="style11" width="125">
                    Visualizza n° Assegnatari con richiesta di Contributo Solidarietà in essere
                </td>
                <td class="style11" width="125">
                    Visualizza n° Assegnatari con Pratiche Legali in essere
                </td>
                <td class="style11" width="125">
                    Visualizza n° altri assegnatari
                </td>
                <td class="style16" width="125">
                    Importo morosità assegnatari con M.M.
                </td>
                <td class="style11" width="125">
                    Importo morosità assegnatari con rich. C.S.
                </td>
                <td class="style11" width="125">
                    Importo morosità assegnatari con P.L.
                </td>
                <td class="style17" width="125">
                    Importo morosità altri assegnatari
                </td>
            </tr>
            <tr style="height: 17.25pt">
                <td class="style2" colspan="8" width="1000">
                    &nbsp;
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style18" colspan="8" width="1000">
                    Dettaglio inquilini (*)
                </td>
            </tr>
            <tr style="height: 26.25pt">
                <td class="style19" width="125">
                    Codice Contrattuale
                </td>
                <td class="style20" width="125">
                    Intestatario
                </td>
                <td class="style21" width="125">
                    Debito
                </td>
                <td class="style22" width="125">
                    Tipologia Rapporto
                </td>
                <td class="style20" width="125">
                    Posizione Contrattuale
                </td>
                <td class="style20" width="125">
                    Unità Immobiliare
                </td>
                <td class="style22" width="125">
                    Tipo UI
                </td>
                <td class="style23" width="125">
                    Indirizzo completo
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style24" colspan="8" height="67" width="1000">
                    &nbsp;
                </td>
            </tr>
            <tr height="27" style="height: 20.25pt">
                <td class="style25" colspan="8" height="27">
                    Riepilogo Generale
                </td>
            </tr>
            <tr style="height: 17.25pt">
                <td class="style2" colspan="8" width="1000">
                    &nbsp;
                </td>
            </tr>
            <tr style="height: 19.5pt">
                <td class="style26" colspan="8" height="71" width="1000">
                    N° Assegnatari Totali nel Range di estrazione:&nbsp; ????????<br />
                    N° Assegnatari a Credito nel Range di estrazione:&nbsp; ???? per un importo a credito
                    totale di € ?????,??
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style27" colspan="8" height="45" width="1000">
                    &nbsp;
                </td>
            </tr>
            <tr style="height: 20.1pt">
                <td class="style4" colspan="8">
                    Riepilogo Morosità
                </td>
            </tr>
            <tr style="height: 20.1pt">
                <td class="style5" colspan="8" width="1000">
                    Assegnatari a debito
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style6" colspan="4" height="176" rowspan="3" width="500">
                    visualizza n° Assegnatari morosi
                </td>
                <td class="style7" colspan="4" width="500">
                    Importo morosità totale
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style8" colspan="2" height="110" rowspan="2" width="250">
                    Importo morosità ex gestori <font class="font5">(3)</font>
                </td>
                <td class="style9" colspan="2" width="250">
                    Importo morosità Gestore
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style10" height="66" width="125">
                    Canoni <font class="font5">(1)</font>
                </td>
                <td class="style11" width="125">
                    Servizi <font class="font5">(2)</font>
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style12" colspan="4" width="500">
                    Dettaglio Assegnatari morosi
                </td>
                <td class="style13" colspan="4" width="500">
                    Dettaglio Morosità
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style14" height="133" width="125">
                    Visualizza n° Assegnatari a cui è stata inviata M.M.
                </td>
                <td class="style11" width="125">
                    Visualizza n° Assegnatari con richiesta di Contributo Solidarietà in essere
                </td>
                <td class="style11" width="125">
                    Visualizza n° Assegnatari con Pratiche Legali in essere
                </td>
                <td class="style11" width="125">
                    Visualizza n° altri assegnatari
                </td>
                <td class="style16" width="125">
                    Importo morosità assegnatari con M.M.
                </td>
                <td class="style11" width="125">
                    Importo morosità assegnatari con rich. C.S.
                </td>
                <td class="style11" width="125">
                    Importo morosità assegnatari con P.L.
                </td>
                <td class="style17" width="125">
                    Importo morosità altri assegnatari
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style28" colspan="8" height="66" width="1000">
                    <br />
                </td>
            </tr>
            <tr>
                <td class="style4" colspan="8">
                    Riepilogo bollette di messa in mora comprese nel range di estrazione
                </td>
            </tr>
            <tr>
                <td class="style6" colspan="4" height="176" rowspan="3" width="500">
                    Visualizza n° mensilità messe in mora tra quelle estratte per morosità
                </td>
                <td class="style7" colspan="4" width="500">
                    Importo di messa in mora totale
                </td>
            </tr>
            <tr>
                <td class="style8" colspan="2" height="110" rowspan="2" width="250">
                    Importo messa in mora su morosità ex gestori <font class="font5">(3)</font>
                </td>
                <td class="style9" colspan="2" width="250">
                    Importo messa in mora su morosità Gestore
                </td>
            </tr>
            <tr>
                <td class="style10" height="66" width="125">
                    Canoni <font class="font5">(1)</font>
                </td>
                <td class="style11" width="125">
                    Servizi <font class="font5">(2)</font>
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style27" colspan="8" height="45" width="1000">
                    &nbsp;
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style38" colspan="8" width="1000">
                    <font class="font5">(1)</font><font class="font0"> Per Canoni intendiamo tutte le voci
                        che rientrano nella Macrocategoria &quot;Canone e Indennità di Occupazione&quot;</font>
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style39" colspan="8" width="1000">
                    <font class="font5">(2)</font><font class="font0"> Per Servizi intendiamo tutte le voci
                        che rientrano nella Macrocategoria &quot;Oneri Accessori&quot;</font>
                </td>
            </tr>
            <tr style="height: 16.5pt">
                <td class="style40" colspan="8" width="1000">
                    <font class="font5">(3)</font><font class="font0"> Se si riesce, gestire la suddivicione
                        in Canoni e Servizi anche per le morosità ex-gestori</font>
                </td>
            </tr>
            <tr style="height: 17.25pt">
                <td class="style41" colspan="8" width="1000">
                    (*) Se selezionato in fase di esecuzione report &quot;Stampa dettaglio inquilini&quot;
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>

