
Partial Class Contabilita_CicloPassivo_Plan_ConvalidaAler
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

        End If


    End Sub

    Function CaricaTabella()
        Try
            Dim TestoPagina As String = ""

            par.OracleConn.Open()
            par.SettaCommand(par)

            TestoPagina = TestoPagina & "<table style='width: 95%;' cellpadding=0 cellspacing = 0'>"
            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='2%'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='6%'>COD.</td><td align='left' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>VOCE</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>RICHIESTO</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;&nbsp;COMP.&nbsp;&nbsp;</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>APPROVAZIONE</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>APPROVATO</td></tr>"

            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 8pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='2%'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>1</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per il property management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"

            Dim COMPLETO As String = ""
            Dim COMPLETO_COMUNE As String = "0"
            Dim immagine As String = ""
            Dim importovoce As String = ""
            Dim Approvazione_aler As String = ""
            Dim Totale As Double = 0
            Dim TotaleGenerale As Double = 0
            Dim ImportoVoceRichiesto As String = "0,00"

            Dim VALORE_VOCE_RICHIESTO As Double = 0
            Dim VALORE_VOCE_APPROVATO As Double = 0

            Dim SOMMA As Double = 0
            Dim SOMMA1 As Double = 0


            par.cmd.CommandText = "select * from siscom_mi.pf_voci where id_piano_finanziario=" & idPianoF.Value & " order by codice asc"
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            tuttocompleto.value = "0"
            While myReader1.Read
                If myReader1("codice") = "2.01" Then
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='2%'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE Spese per il property management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                    Totale = 0
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 9pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='2%'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>2</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per il facility management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                End If
                If myReader1("codice") = "3.01" Then

                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='2%'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE Spese per il facility management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                    Totale = 0
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 8pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='2%'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>3</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per contributi per sostegno agli inquilini</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                End If
                If myReader1("codice") = "4.01" Then
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='2%'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE Spese per contributi per sostegno agli inquilini</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                    Totale = 0
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border: 2px dashed #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 8pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='2%'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>4</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese diverse</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
                End If

                COMPLETO = "&nbsp;"

                If myReader1("codice") = "2.04.01" Then
                    COMPLETO = "&nbsp;"
                End If

                If Len(par.IfNull(myReader1("codice"), "0")) <= 7 And Len(par.IfNull(myReader1("codice"), "0")) > 4 Then
                    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI_STRUTTURA WHERE VALORE_LORDO<>0 AND COMPLETO='0' AND ID_VOCE=" & myReader1("ID")
                    Dim myReaderC As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderC.HasRows = True Then
                        COMPLETO = "NO"
                    Else
                        'If myReaderC.Read Then
                        COMPLETO = "SI"
                        'Else
                        'COMPLETO = "NO"
                        'End If
                    End If
                myReaderC.Close()
                End If


                'If myReader1("codice") <> "1.01" And myReader1("codice") <> "1.02" And myReader1("codice") <> "1.03" And myReader1("codice") <> "2.02" And myReader1("codice") <> "2.03" And myReader1("codice") <> "2.04" Then
                '    tuttocompleto.Value = "1"
                'Else
                '    tuttocompleto.Value = "0"
                'End If

                If Len(myReader1("codice")) <> 4 And Len(myReader1("codice")) <> 7 Then
                    par.cmd.CommandText = "select * from siscom_mi.pf_voci_struttura where VALORE_LORDO<>0 AND completo=0 and id_voce=" & myReader1("id")
                    Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderComp.HasRows = False Then
                        tuttocompleto.Value = "1"
                    Else
                        tuttocompleto.Value = "0"
                    End If
                    myReaderComp.Close()
                Else
                    tuttocompleto.Value = "1"
                End If

                If Len(myReader1("codice")) = 7 Then
                    par.cmd.CommandText = "select * from siscom_mi.pf_voci_struttura where VALORE_LORDO<>0 AND completo=0 and id_voce=" & myReader1("id")
                    Dim myReaderComp As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderComp.HasRows = False Then
                        tuttocompleto.Value = "1"
                    Else
                        tuttocompleto.Value = "0"
                    End If
                    myReaderComp.Close()
                Else
                    tuttocompleto.Value = "1"
                End If

                VALORE_VOCE_RICHIESTO = 0
                VALORE_VOCE_APPROVATO = 0
                VALORE_VOCE_RICHIESTO = 0
                VALORE_VOCE_APPROVATO = 0

                SOMMA = 0
                SOMMA1 = 0

                par.cmd.CommandText = "select * from siscom_mi.pf_voci where id_voce_madre=" & myReader1("id")
                Dim myReader123 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader123.HasRows = False Then

                    par.cmd.CommandText = "SELECT SUM(VALORE_LORDO),SUM(VALORE_LORDO_ALER) FROM SISCOM_MI.PF_VOCI_STRUTTURA WHERE ID_VOCE=" & myReader1("ID")
                    Dim myReaderD As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderD.Read Then
                        ImportoVoceRichiesto = par.IfNull(myReaderD(0), "0")
                        importovoce = "<a href='DettagliSpesaAler.aspx?IDV=" & par.IfNull(myReader1("id"), "") & "&P=" & per.Value & "' target='_blank'>" & Format(CDbl(par.IfNull(myReaderD(0), "0")), "##,##0.00") & "</a>"

                        Totale = Totale + CDbl(par.IfNull(myReaderD(0), "0"))
                        TotaleGenerale = TotaleGenerale + CDbl(par.IfNull(myReaderD(0), "0"))

                        VALORE_VOCE_RICHIESTO = CDbl(par.IfNull(myReaderD(0), "0"))
                        VALORE_VOCE_APPROVATO = CDbl(par.IfNull(myReaderD(1), "0"))


                        If VALORE_VOCE_RICHIESTO = 0 Then
                            importovoce = "0,00"
                        End If
                    End If
                    myReaderD.Close()

                Else
                    importovoce = "0,00"
  
                    importovoce = "&nbsp;"
                    'If Len(par.IfNull(myReader1("codice"), "")) = 4 Then
                    '    par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI WHERE pf_voci.id_piano_finanziario=" & idPianoF.Value & " AND LENGTH(CODICE)=7 AND SUBSTR(CODICE,1,4)='" & par.IfNull(myReader1("codice"), "") & "'"
                    '    Dim myReader12345 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    '    Do While myReader12345.Read
                    '        par.cmd.CommandText = "select SUM(VALORE_LORDO),SUM(VALORE_LORDO_ALER) from siscom_mi.pf_voci_STRUTTURA where ID_VOCE=" & myReader12345("id")
                    '        Dim myReader1234 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    '        If myReader1234.Read Then
                    '            SOMMA = SOMMA + par.IfNull(myReader1234(0), 0)
                    '            SOMMA1 = SOMMA1 + par.IfNull(myReader1234(1), 0)
                    '        End If
                    '        myReader1234.Close()
                    '    Loop
                    '    myReader12345.Close()
                    '    importovoce = "<span class='style2'>" & Format(SOMMA, "##,##0.00") & "</span>"
                    '    VALORE_VOCE_RICHIESTO = SOMMA
                    '    VALORE_VOCE_APPROVATO = SOMMA1
                    'End If

                    If Len(par.IfNull(myReader1("codice"), "")) = 7 Then

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI WHERE pf_voci.id_piano_finanziario=" & idPianoF.Value & " AND LENGTH(CODICE)=7 AND SUBSTR(CODICE,1,7)='" & myReader1("codice") & "'"
                        Dim myReader123456 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        If myReader123456.Read Then
                            par.cmd.CommandText = "select SUM(VALORE_LORDO_ALER) from siscom_mi.pf_voci_STRUTTURA where ID_VOCE=" & myReader123456("id")
                            Dim myReader1234567 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader1234567.Read Then
                                SOMMA1 = SOMMA1 + par.IfNull(myReader1234567(0), 0)
                            End If
                            myReader1234567.Close()

                        End If
                        myReader123456.Close()

                        par.cmd.CommandText = "SELECT * FROM SISCOM_MI.PF_VOCI WHERE pf_voci.id_piano_finanziario=" & idPianoF.Value & " AND LENGTH(CODICE)=10 AND SUBSTR(CODICE,1,7)='" & par.IfNull(myReader1("codice"), "") & "'"
                        Dim myReader12345 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                        Do While myReader12345.Read
                            par.cmd.CommandText = "select SUM(VALORE_LORDO),SUM(VALORE_LORDO_ALER) from siscom_mi.pf_voci_STRUTTURA where ID_VOCE=" & myReader12345("id")
                            Dim myReader1234 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                            If myReader1234.Read Then
                                SOMMA = SOMMA + par.IfNull(myReader1234(0), 0)
                                ' SOMMA1 = SOMMA1 + par.IfNull(myReader1234(1), 0)

                            End If
                            myReader1234.Close()
                        Loop
                        myReader12345.Close()
                        importovoce = "<span class='style2'>" & Format(SOMMA, "##,##0.00") & "</span>"
                        VALORE_VOCE_RICHIESTO = SOMMA
                        VALORE_VOCE_APPROVATO = SOMMA1
                    End If


                    End If
                    myReader123.Close()


                    par.cmd.CommandText = "SELECT COMPLETO_COMUNE FROM SISCOM_MI.PF_VOCI_STRUTTURA WHERE COMPLETO_COMUNE=1 AND ID_VOCE=" & myReader1("ID")
                    Dim myReaderE As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderE.HasRows = True Then
                        COMPLETO_COMUNE = "<img src='..\..\..\IMG\alert.gif' alt='Importo non approvato dal Comune di Milano'/>"
                    Else
                        COMPLETO_COMUNE = "&nbsp;"
                    End If
                    myReaderE.Close()

                    If Len(par.IfNull(myReader1("codice"), "")) > 7 Then
                        COMPLETO = "&nbsp;"
                    End If

                    If COMPLETO = "SI" Then
                        If tuttocompleto.Value = "1" Then
                            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='2%'>" & COMPLETO_COMUNE & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & importovoce & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='center'>" & COMPLETO & "</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img alt='Visualizza Dettagli Voce' src='Immagini/Edita.png' style='cursor: pointer' onclick='EditaVoce(" & par.IfNull(myReader1("id"), "0") & "," & par.VirgoleInPunti(ImportoVoceRichiesto) & ");'/></td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(VALORE_VOCE_APPROVATO, "##,##0.00") & "</td></tr>"
                        Else
                            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='2%'>" & COMPLETO_COMUNE & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='center'>&nbsp;</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td></tr>"
                        End If
                    Else

                        If tuttocompleto.Value = "1" Then
                            If VALORE_VOCE_RICHIESTO <> 0 And COMPLETO = "NO" Then
                            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='2%'>" & COMPLETO_COMUNE & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & importovoce & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='center'>" & COMPLETO & "</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(VALORE_VOCE_APPROVATO, "##,##0.00") & "</td></tr>"
                            Else
                                TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='2%'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & importovoce & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='center'>" & COMPLETO & "</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>0,00</td></tr>"
                            End If
                        Else

                            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='2%'>" & COMPLETO_COMUNE & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & importovoce & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='center'>" & COMPLETO & "</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td></tr>"

                        End If
                    End If
                    'End If



                    'If COMPLETO = "SI" And importovoce <> "&nbsp;" Then
                    '    If VALORE_VOCE_APPROVATO <> VALORE_VOCE_RICHIESTO Then
                    '        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & importovoce & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='center'>" & COMPLETO & "</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'><img alt='Approva Importo' src='Immagini/Approva.png' style='cursor: pointer' onclick='ConfermaImporto(" & par.IfNull(myReader1("id"), "0") & "," & par.VirgoleInPunti(ImportoVoceRichiesto) & ");'/>&nbsp;&nbsp;&nbsp;<img alt='Imposta altro Importo' src='Immagini/Non_Approva.png' style='cursor: pointer' onclick='NonConfermaImporto(" & par.IfNull(myReader1("id"), "0") & "," & par.VirgoleInPunti(ImportoVoceRichiesto) & ");'/></td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(VALORE_VOCE_APPROVATO, "##,##0.00") & "</td></tr>"
                    '    Else
                    '        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & importovoce & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='center'>" & COMPLETO & "</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<img alt='Imposta altro Importo' src='Immagini/Non_Approva.png' style='cursor: pointer' onclick='NonConfermaImporto(" & par.IfNull(myReader1("id"), "0") & "," & par.VirgoleInPunti(ImportoVoceRichiesto) & ");'/></td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(VALORE_VOCE_APPROVATO, "##,##0.00") & "</td></tr>"
                    '    End If
                    'Else
                    '    If importovoce <> "&nbsp;" Then
                    '        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & importovoce & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='center'>" & COMPLETO & "</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(VALORE_VOCE_APPROVATO, "##,##0.00") & "</td></tr>"
                    '    Else
                    '        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & importovoce & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='center'>&nbsp;</td><td align='center' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td></tr>"
                    '    End If
                    'End If

            End While
            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='2%'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE Spese Diverse</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
            TestoPagina = TestoPagina & "<tr></tr><tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='2%'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE GENERALE</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(TotaleGenerale, "##,##0.00") & "</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"
            myReader1.Close()
            TestoPagina = TestoPagina & "</table>"

            Tabella = TestoPagina



            par.cmd.CommandText = "select * from operatori where id=" & Session.Item("ID_OPERATORE")
            myReader1 = par.cmd.ExecuteReader()
            If myReader1.Read Then
                If par.IfNull(myReader1("BP_CONV_ALER_L"), "1") = "1" Then
                    imgConvalida.Visible = False
                End If
            End If
            myReader1.Close()


            par.cmd.Dispose()
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
        Catch ex As Exception
            par.OracleConn.Close()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            lblErrore.Visible = True
            lblErrore.Text = ex.Message
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

    'Protected Sub imgConfermare_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgConfermare.Click
    '    If salvaok.Value = "1" Then
    '        Try
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)

    '            par.cmd.CommandText = "update siscom_mi.pf_voci set completo_aler='1',valore_lordo_aler=valore_lordo where id=" & idvoce.Value
    '            par.cmd.ExecuteNonQuery()

    '            par.cmd.CommandText = "select * from siscom_mi.pf_voci where id=" & idvoce.Value
    '            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '            If myReader5.Read Then
    '                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_EVENTI (ID_PIANO_FINANZIARIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
    '                                    & "VALUES (" & idPianoF.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
    '                                    & "'F86','Voce " & par.PulisciStrSql(par.IfNull(myReader5("codice"), "")) & "-" & par.IfNull(myReader5("descrizione"), "") & " - Euro " & Format(par.IfNull(myReader5("valore_lordo"), "0"), "##,##0.00") & "')"
    '                par.cmd.ExecuteNonQuery()
    '            End If
    '            myReader5.Close()



    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    '            CaricaTabella()

    '        Catch ex As Exception
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    '            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
    '        End Try
    '    End If
    'End Sub

    'Protected Sub ImageButton1_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImageButton1.Click
    '    If salvaok.Value = "1" Then
    '        Try
    '            par.OracleConn.Open()
    '            par.SettaCommand(par)

    '            par.cmd.CommandText = "update siscom_mi.pf_voci set completo='0',completo_aler='1',valore_lordo_aler=" & par.VirgoleInPunti(txtImporto.Text) & " where id=" & idvoce.Value
    '            par.cmd.ExecuteNonQuery()

    '            par.cmd.CommandText = "select * from siscom_mi.pf_voci where id=" & idvoce.Value
    '            Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
    '            If myReader5.Read Then
    '                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_EVENTI (ID_PIANO_FINANZIARIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
    '                                    & "VALUES (" & idPianoF.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
    '                                    & "'F87','Voce " & par.PulisciStrSql(par.IfNull(myReader5("codice"), "")) & "-" & par.IfNull(myReader5("descrizione"), "") & " - Richiesto " & Format(par.IfNull(myReader5("valore_lordo"), "0"), "##,##0.00") & " - Approvato " & Format(CDbl(txtImporto.Text), "##,##0.00") & "')"
    '                par.cmd.ExecuteNonQuery()
    '            End If
    '            myReader5.Close()

    '            txtImporto.Text = "0,00"

    '            par.cmd.Dispose()
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    '            CaricaTabella()

    '        Catch ex As Exception
    '            par.OracleConn.Close()
    '            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

    '            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
    '            Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
    '        End Try
    '    End If
    'End Sub

    Protected Sub ImgConvalidaTutto_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ImgConvalidaTutto.Click
        If salvaok.Value = "1" Then
            Try
                par.OracleConn.Open()
                par.SettaCommand(par)

                par.cmd.CommandText = "select * from siscom_mi.pf_voci_STRUTTURA where ID_VOCE IN (SELECT ID FROM SISCOM_MI.PF_VOCI WHERE ID_VOCE_MADRE IS NOT NULL AND LENGTH(CODICE)<10) AND ((valore_lordo-valore_lordo_aler)>1 or (valore_lordo_aler-valore_lordo)>1) and valore_lordo<>valore_lordo_aler and (valore_netto<>0 or valore_netto is not null) and (valore_lordo<>0 or valore_lordo is not null) AND id_voce IN (SELECT ID FROM siscom_mi.PF_VOCI WHERE id_piano_finanziario=" & idPianoF.Value & ")  " '"select * from siscom_mi.pf_voci_STRUTTURA where (valore_lordo_aler<>0 and valore_netto<>0) and valore_lordo<>valore_lordo_aler "
                Dim myReader5 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader5.HasRows = True Then
                    Response.Write("<script>alert('Il piano non può essere covalidato ora. Gli importi richiesti potrebbero non coincidere con quelli approvati!');</script>")
                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                    Exit Sub
                End If

                par.cmd.CommandText = "update siscom_mi.pf_main set id_stato=3 where id=" & idPianoF.Value
                par.cmd.ExecuteNonQuery()

                par.cmd.CommandText = "INSERT INTO SISCOM_MI.PF_EVENTI (ID_PIANO_FINANZIARIO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE,ID_STRUTTURA) " _
                                    & "VALUES (" & idPianoF.Value & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                    & "'F86','Piano Finanziario Approvato dal Gestore',-1)"
                par.cmd.ExecuteNonQuery()

                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                CaricaStato()
             
            Catch ex As Exception
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
                Response.Write("<script>top.location.href='../../../Errore.aspx';</script>")
            End Try
        End If
    End Sub

    Protected Sub imgAggiorna_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles imgAggiorna.Click
        CaricaStato()
        CaricaTabella()
    End Sub
End Class
