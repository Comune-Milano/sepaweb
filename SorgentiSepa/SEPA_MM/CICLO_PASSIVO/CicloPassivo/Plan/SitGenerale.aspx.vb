Imports System.IO
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.Checksums

Partial Class Contabilita_CicloPassivo_Plan_ConvalidaComune
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Public Property Tabella() As String
        Get
            If Not (ViewState("par_Tabella") Is Nothing) Then
                Return CStr(ViewState("par_Tabella"))
            Else
                Return ""
            End If
        End Get

        Set(ByVal value As String)
            ViewState("par_Tabella") = value
        End Set

    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If
        Response.Expires = 0

        If Not IsPostBack Then
            idPianoF.Value = Request.QueryString("IDP")
            per.Value = ""
            CaricaStato()
            CaricaTabella()
            'CaricaValori()

        End If
    End Sub

    Function CaricaTabella()
        Try
            Dim TestoPagina As String = ""
            Dim Buono As Boolean = True

            Dim Totale As Double = 0
            Dim totaleAssestamento As Double = 0
            par.OracleConn.Open()
            par.SettaCommand(par)

            TestoPagina = TestoPagina & "<table style='width: 900px;' cellpadding=0 cellspacing = 0'>"
            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='100px'>COD.</td><td width='300px' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>VOCE</td><td width='100px' align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO</td><td width='100px' align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;ASSESTAMENTO</td><td width='100px' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;CAPITOLO</td><td width='300px' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>DESCRIZIONE</td></tr>"
            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 8pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>1</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per il property management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"

            Buono = True
            par.cmd.CommandText = "select PF_VOCI.*,PF_CAPITOLI.COD,PF_CAPITOLI.DESCRIZIONE as dcap from SISCOM_MI.PF_CAPITOLI,siscom_mi.pf_voci where PF_VOCI.ID_CAPITOLO=PF_CAPITOLI.ID (+) AND id_piano_finanziario=" & idPianoF.Value & " order by codice asc,indice asc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            While myReader1.Read
                If myReader1("codice") = "2.01" Then
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE Spese per il property management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp" & Format(totaleAssestamento, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                    Totale = 0
                    totaleAssestamento = 0
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 9pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>2</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per il facility management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                End If
                If myReader1("codice") = "3.01" Then
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE Spese per il facility management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp" & Format(totaleAssestamento, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                    Totale = 0
                    totaleAssestamento = 0
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 8pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>3</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per contributi per sostegno agli inquilini</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                End If
                If myReader1("codice") = "4.01" Then
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE Spese per contributi per sostegno agli inquilini</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp" & Format(totaleAssestamento, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                    Totale = 0
                    totaleAssestamento = 0
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border: 2px dashed #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 8pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>4</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese diverse</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                End If

                If Len(par.IfNull(myReader1("codice"), "")) = 7 Then

                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI WHERE pf_voci.id_piano_finanziario=" & idPianoF.Value & " AND LENGTH(CODICE)=7 AND SUBSTR(CODICE,1,7)='" & myReader1("codice") & "'"
                    Dim myReader123456 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader123456.Read Then
                        par.cmd.CommandText = "select SUM(VALORE_LORDO), SUM(ASSESTAMENTO_VALORE_LORDO) from siscom_mi.pf_voci_STRUTTURA where ID_VOCE=" & myReader123456("id")
                        Dim myReader1234567 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader1234567.Read Then
                            Totale = Totale + par.IfNull(myReader1234567(0), 0)
                            totaleAssestamento = totaleAssestamento + par.IfNull(myReader1234567(1), 0)
                        End If
                        myReader1234567.Close()

                    End If
                    myReader123456.Close()

                End If


                If Len(par.IfNull(myReader1("CODice"), "--")) <> 4 Then ' <> "1.01" And par.IfNull(myReader1("CODice"), "--") <> "1.02" And par.IfNull(myReader1("CODice"), "--") <> "1.03" And par.IfNull(myReader1("CODice"), "--") <> "2.01" And par.IfNull(myReader1("CODice"), "--") <> "2.02" And par.IfNull(myReader1("CODice"), "--") <> "2.03" And par.IfNull(myReader1("CODice"), "--") <> "2.04" Then
                    par.cmd.CommandText = "select sum(valore_lordo), SUM(ASSESTAMENTO_VALORE_LORDO) from SISCOM_MI.PF_voci_struttura where PF_VOCI_struttura.id_voce=" & myReader1("id")
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader3.Read Then
                        'Totale = Totale + CDbl(par.IfNull(myReader3(0), "0"))
                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'><a href='DettagliSpesaAler.aspx?IDV=" & par.IfNull(myReader1("id"), "") & "&P=" & per.Value & "' target='_blank'>" & Format(CDbl(par.IfNull(myReader3(0), 0)), "##,##0.00") & "</a></td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;&nbsp;" & Format(CDbl(par.IfNull(myReader3(1), 0)), "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;&nbsp;" & par.IfNull(myReader1("COD"), "---") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("dcap"), "---") & "</td></tr>"
                    End If
                    myReader3.Close()
                Else
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td></tr>"

                End If
                If par.IfNull(myReader1("COD"), "--") = "--" Then
                    Buono = False
                End If
            End While
            myReader1.Close()

            If Buono = False Then
                convalidaok.Value = "0"
            Else
                convalidaok.Value = "1"

            End If
            TestoPagina = TestoPagina & "</table><br/><br/><p style='font-family: arial; font-size: 10pt; font-weight: bold'>Importi per Capitolo</p>"


            TestoPagina = TestoPagina & "<table style='width: 600px;' cellpadding=0 cellspacing = 0'>"
            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='100px'>CAPITOLO</td><td width='400px' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>DESCRIZIONE</td><td width='100px' align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO</td><td width='100px' align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;ASSESTAMENTO</td></tr>"
            Totale = 0


            'par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_CAPITOLI ORDER BY COD ASC"
            'myReader1 = par.cmd.ExecuteReader()

            'While myReader1.Read
            '    Totale = 0
            '    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI WHERE ID_CAPITOLO=" & myReader1("ID") & " and pf_voci.id_piano_finanziario=" & idPianoF.Value & " AND LENGTH(CODICE)=7 "
            '    Dim myReaderSomma As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '    Do While myReaderSomma.Read
            '        par.cmd.CommandText = "select SUM(VALORE_LORDO) from siscom_mi.pf_voci_STRUTTURA where ID_VOCE=" & myReaderSomma("id")
            '        Dim myReader123456 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            '        If myReader123456.Read Then
            '            Totale = Totale + par.IfNull(myReader123456(0), 0)
            '        End If
            '    Loop
            '    myReaderSomma.Close()
            '    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("cod"), "---") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "---") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(Totale, "##,##0.00") & "</td></tr>"

            'End While
            'myReader1.Close()


            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_CAPITOLI where id>0 ORDER BY COD ASC"
            myReader1 = par.cmd.ExecuteReader()
            Dim TotCapitoli As Double = 0
            Dim assestamentoCapitoli As Double = 0
            While myReader1.Read
                par.cmd.CommandText = "SELECT SUM(VALORE_LORDO), sum(ASSESTAMENTO_VALORE_LORDO) FROM SISCOM_MI.PF_VOCI_STRUTTURA,SISCOM_MI.PF_VOCI WHERE LENGTH(CODICE)=7 AND PF_VOCI_STRUTTURA.ID_VOCE=PF_VOCI.ID AND ID_CAPITOLO=" & myReader1("ID") & " AND id_piano_finanziario=" & idPianoF.Value
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("cod"), "---") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "---") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(CDbl(par.IfNull(myReader2(0), "0")), "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(CDbl(par.IfNull(myReader2(1), "0")), "##,##0.00") & "</td></tr>"
                    'TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("cod"), "---") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "---") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(Totale, "##,##0.00") & "</td></tr>"
                    TotCapitoli = TotCapitoli + CDbl(par.IfNull(myReader2(0), 0))
                    assestamentoCapitoli = assestamentoCapitoli + CDbl(par.IfNull(myReader2(1), 0))
                End If
                myReader2.Close()

            End While
            myReader1.Close()
            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>TOTALE PER CAPITOLI</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(TotCapitoli, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>&nbsp;&nbsp;" & Format(assestamentoCapitoli, "##,##0.00") & "</td></tr>"
            TestoPagina = TestoPagina & "</table>"

            Tabella = TestoPagina

            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Response.Write(ex.Message)
        End Try

    End Function

    Function CaricaStato()
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)


            par.cmd.CommandText = "select TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.INIZIO,'YYYYmmdd'),'DD/MM/YYYY') as INIZIO,TO_CHAR(TO_DATE(T_ESERCIZIO_FINANZIARIO.FINE,'YYYYmmdd'),'DD/MM/YYYY') AS FINE,PF_MAIN.*,PF_STATI.DESCRIZIONE AS STATO FROM SISCOM_MI.T_ESERCIZIO_FINANZIARIO,SISCOM_MI.PF_STATI, SISCOM_MI.PF_MAIN WHERE PF_MAIN.ID=" & idPianoF.Value & " and PF_STATI.ID=PF_MAIN.ID_STATO and t_esercizio_finanziario.id=pf_main.id_esercizio_finanziario"
            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader5.Read Then
                Label1.Text = myReader5("inizio") & "-" & myReader5("fine")
                per.Value = Label1.Text
                lblStato.Text = "STATO:" & par.IfNull(myReader5("stato"), "")
                stato.Value = par.IfNull(myReader5("id_stato"), "")

            End If
            myReader5.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()


        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Function

    Protected Sub imgEsportaXLS_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles imgEsportaXLS.Click
        Try
            par.OracleConn.Open()
            par.SettaCommand(par)
            Dim dtSitImporti, dtSitImportiCapitolo As New Data.DataTable
            creaDTExportSitImporti(dtSitImporti)
            creaDTExportSitImportiCapitolo(dtSitImportiCapitolo)
            Dim xls As New ExcelSiSol
            Dim nomeFile = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportSitImporti", "ExportSitImporti", dtSitImporti)
            Dim nomeFile1 = xls.EsportaExcelDaDT(ExcelSiSol.Estensione.Office2007_xlsx, "ExportSitImportiCapitolo", "ExportSitImportiCapitolo", dtSitImportiCapitolo)
            Dim strmZipOutputStream As ZipOutputStream = Nothing
            Dim nome As String = "ExportSitImporti-" & Format(Now, "yyyyMMddHHmmss")
            Dim objCrc32 As New Crc32()
            Dim zipfic As String
            zipfic = Server.MapPath("..\..\..\FileTemp\" & nome & ".zip")
            strmZipOutputStream = New ZipOutputStream(File.Create(zipfic))
            strmZipOutputStream.SetLevel(6)
            Dim strFile As String
            Dim strmFile As FileStream
            Dim theEntry As ZipEntry
            Dim contatore As Integer = 0
            Dim ElencoFile() As String = Nothing
            Dim fileexport As String = ""
            For i As Integer = 0 To 1
                If i = 0 Then
                    fileexport = nomeFile
                ElseIf i = 1 Then
                    fileexport = nomeFile1
                End If
                contatore += 1
                strFile = Server.MapPath("~\/FileTemp\/") & fileexport
                strmFile = File.OpenRead(strFile)
                Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
                strmFile.Read(abyBuffer, 0, abyBuffer.Length)
                Dim sFile As String = Path.GetFileName(strFile)
                theEntry = New ZipEntry(sFile)
                Dim fi As New FileInfo(strFile)
                theEntry.DateTime = fi.LastWriteTime
                theEntry.Size = strmFile.Length
                strmFile.Close()
                objCrc32.Reset()
                objCrc32.Update(abyBuffer)
                theEntry.Crc = objCrc32.Value
                strmZipOutputStream.PutNextEntry(theEntry)
                strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
                ReDim Preserve ElencoFile(i)
                ElencoFile(i) = "..\..\..\FileTemp\" & nomeFile
            Next
            strmZipOutputStream.Close()
            If Not String.IsNullOrEmpty(nome) Then
                If System.IO.File.Exists(Server.MapPath("~\/FileTemp\/") & nome & ".zip") Then
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "msg", "alert('File creato correttamente');", True)
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "downloadFile", "avvio(); function avvio() { var altezza = (screen.height/2)-50; var larghezza = (screen.width/2)-100;window.open('..\/..\/..\/FileTemp\/" & nome & ".zip','','top='+altezza+',left='+larghezza+',height=100,width=200');}", True)
                Else
                    ScriptManager.RegisterClientScriptBlock(Me.Page, Me.GetType(), "msg", "alert('Il file non è stato creato correttamente!\nRiprovare o contattare l\'amministratore di Sistema!');", True)
                End If
            End If
            par.cmd.Dispose()
            par.OracleConn.Close()
        Catch ex As Exception
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub creaDTExportSitImporti(ByRef dt As Data.DataTable)
        Try
         
            dt.Columns.Add("COD")
            dt.Columns.Add("VOCE")
            dt.Columns.Add("IMPORTO")
            dt.Columns.Add("ASSESTAMENTO")
            dt.Columns.Add("CAPITOLO")
            dt.Columns.Add("DESCRIZIONE")
            Dim Buono As Boolean = True
            Dim Totale As Double = 0
            Dim totaleAssestamento As Double = 0
            Dim riga As Data.DataRow = dt.NewRow
            riga.Item("COD") = "1"
            riga.Item("VOCE") = "Spese per il property management"
            dt.Rows.Add(riga)
            par.cmd.CommandText = "select PF_VOCI.*,PF_CAPITOLI.COD,PF_CAPITOLI.DESCRIZIONE as dcap from SISCOM_MI.PF_CAPITOLI,siscom_mi.pf_voci where PF_VOCI.ID_CAPITOLO=PF_CAPITOLI.ID (+) AND id_piano_finanziario=" & idPianoF.Value & " order by codice asc,indice asc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            While myReader1.Read
                If myReader1("codice") = "2.01" Then
                    riga = dt.NewRow
                    riga.Item("VOCE") = "TOTALE Spese per il property management"
                    riga.Item("IMPORTO") = Format(Totale, "##,##0.00")
                    riga.Item("ASSESTAMENTO") = Format(totaleAssestamento, "##,##0.00")
                    dt.Rows.Add(riga)
                    Totale = 0
                    totaleAssestamento = 0
                    riga = dt.NewRow
                    riga.Item("COD") = "2"
                    riga.Item("VOCE") = "Spese per il facility management"
                    dt.Rows.Add(riga)
                End If
                If myReader1("codice") = "3.01" Then
                    riga = dt.NewRow
                    riga.Item("VOCE") = "TOTALE Spese per il facility management"
                    riga.Item("IMPORTO") = Format(Totale, "##,##0.00")
                    riga.Item("ASSESTAMENTO") = Format(totaleAssestamento, "##,##0.00")
                    dt.Rows.Add(riga)
                    Totale = 0
                    totaleAssestamento = 0
                    riga = dt.NewRow
                    riga.Item("COD") = "3"
                    riga.Item("VOCE") = "Spese per contributi per sostegno agli inquilini"
                    dt.Rows.Add(riga)
                End If
                If myReader1("codice") = "4.01" Then
                    riga = dt.NewRow
                    dt.Rows.Add(riga)
                    riga.Item("VOCE") = "TOTALE Spese per contributi per sostegno agli inquilini"
                    riga.Item("IMPORTO") = Format(Totale, "##,##0.00")
                    riga.Item("ASSESTAMENTO") = Format(totaleAssestamento, "##,##0.00")
                    Totale = 0
                    totaleAssestamento = 0
                    riga = dt.NewRow
                    riga.Item("COD") = "4"
                    riga.Item("VOCE") = "Spese diverse"
                    dt.Rows.Add(riga)
                End If
                If Len(par.IfNull(myReader1("codice"), "")) = 7 Then
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI WHERE pf_voci.id_piano_finanziario=" & idPianoF.Value & " AND LENGTH(CODICE)=7 AND SUBSTR(CODICE,1,7)='" & myReader1("codice") & "'"
                    Dim myReader123456 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader123456.Read Then
                        par.cmd.CommandText = "select SUM(VALORE_LORDO), SUM(ASSESTAMENTO_VALORE_LORDO) from siscom_mi.pf_voci_STRUTTURA where ID_VOCE=" & myReader123456("id")
                        Dim myReader1234567 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader1234567.Read Then
                            Totale = Totale + par.IfNull(myReader1234567(0), 0)
                            totaleAssestamento = totaleAssestamento + par.IfNull(myReader1234567(1), 0)
                        End If
                        myReader1234567.Close()
                    End If
                    myReader123456.Close()
                End If
                If Len(par.IfNull(myReader1("CODice"), "--")) <> 4 Then ' <> "1.01" And par.IfNull(myReader1("CODice"), "--") <> "1.02" And par.IfNull(myReader1("CODice"), "--") <> "1.03" And par.IfNull(myReader1("CODice"), "--") <> "2.01" And par.IfNull(myReader1("CODice"), "--") <> "2.02" And par.IfNull(myReader1("CODice"), "--") <> "2.03" And par.IfNull(myReader1("CODice"), "--") <> "2.04" Then
                    par.cmd.CommandText = "select sum(valore_lordo), SUM(ASSESTAMENTO_VALORE_LORDO) from SISCOM_MI.PF_voci_struttura where PF_VOCI_struttura.id_voce=" & myReader1("id")
                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    riga = dt.NewRow
                    dt.Rows.Add(riga)
                    If myReader3.Read Then
                        Totale = Totale + CDbl(par.IfNull(myReader3(0), "0"))
                        riga.Item("COD") = par.IfNull(myReader1("codice"), "")
                        riga.Item("VOCE") = par.IfNull(myReader1("descrizione"), "")
                        riga.Item("IMPORTO") = Format(CDbl(par.IfNull(myReader3(0), 0)), "##,##0.00")
                        riga.Item("ASSESTAMENTO") = Format(CDbl(par.IfNull(myReader3(1), 0)), "##,##0.00")
                        riga.Item("CAPITOLO") = par.IfNull(myReader1("COD"), "---")
                        riga.Item("DESCRIZIONE") = par.IfNull(myReader1("dcap"), "---")
                    End If
                    myReader3.Close()
                Else
                    riga = dt.NewRow
                    dt.Rows.Add(riga)
                    riga.Item("COD") = par.IfNull(myReader1("codice"), "")
                    riga.Item("VOCE") = par.IfNull(myReader1("descrizione"), "")
                    'TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td></tr>"
                End If
                If par.IfNull(myReader1("COD"), "--") = "--" Then
                    Buono = False
                End If
            End While
            myReader1.Close()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

    Private Sub creaDTExportSitImportiCapitolo(ByRef dt As Data.DataTable)
        Try
            dt.Columns.Add("CAPITOLO")
            dt.Columns.Add("DESCRIZIONE")
            dt.Columns.Add("IMPORTO")
            dt.Columns.Add("ASSESTAMENTO")
            Dim riga As Data.DataRow = dt.NewRow
         
            Dim Totale As Double = 0
            Dim TotaleAssestamento As Double = 0


            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_CAPITOLI where id>0 ORDER BY COD ASC"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            Dim TotCapitoli As Double = 0
            Dim assestamentoCapitoli As Double = 0
            While myReader1.Read
                par.cmd.CommandText = "SELECT SUM(VALORE_LORDO), sum(ASSESTAMENTO_VALORE_LORDO) FROM SISCOM_MI.PF_VOCI_STRUTTURA,SISCOM_MI.PF_VOCI WHERE LENGTH(CODICE)=7 AND PF_VOCI_STRUTTURA.ID_VOCE=PF_VOCI.ID AND ID_CAPITOLO=" & myReader1("ID") & " AND id_piano_finanziario=" & idPianoF.Value
                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    riga = dt.NewRow
                    riga.Item("CAPITOLO") = par.IfNull(myReader1("cod"), "---")
                    riga.Item("DESCRIZIONE") = par.IfNull(myReader1("descrizione"), "---")
                    riga.Item("IMPORTO") = Format(CDbl(par.IfNull(myReader2(0), "0")), "##,##0.00")
                    riga.Item("ASSESTAMENTO") = Format(CDbl(par.IfNull(myReader2(1), "0")), "##,##0.00")
                    dt.Rows.Add(riga)
                    TotCapitoli = TotCapitoli + CDbl(par.IfNull(myReader2(0), 0))
                    assestamentoCapitoli = assestamentoCapitoli + CDbl(par.IfNull(myReader2(1), 0))
                End If
                myReader2.Close()
            End While
            myReader1.Close()
            riga = dt.NewRow
            riga.Item("DESCRIZIONE") = "TOTALE PER CAPITOLI"
            riga.Item("IMPORTO") = Format(TotCapitoli, "##,##0.00")
            riga.Item("ASSESTAMENTO") = Format(assestamentoCapitoli, "##,##0.00")
            dt.Rows.Add(riga)
            'riga = dt.NewRow
            'dt.Rows.Add(riga)
            'riga.Item("VOCE") = "Importi per Capitolo"
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
        End Try
    End Sub

End Class
