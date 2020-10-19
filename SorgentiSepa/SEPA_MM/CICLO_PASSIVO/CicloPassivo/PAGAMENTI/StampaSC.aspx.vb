Imports System.IO
Imports ExpertPdf.HtmlToPdf
Imports System.Drawing

Partial Class StampaSC
    Inherits PageSetIdMode
    Dim par As New CM.Global
    Dim ID_EF As String = "0"
    Dim TestoPagina As String = ""


    Public sValoreData_Dal As String
    Public sValoreData_Al As String

    Public SommaBungetCapTOT As Decimal = 0
    Public SommaAssestatoCapTOT As Decimal = 0
    Public SommaPrenotatoCapTOT As Decimal = 0
    Public SommaConsuntivatoCapTOT As Decimal = 0
    Public SommaLiquidatoCapTOT As Decimal = 0
    Public SommaResiduoCapTOT As Decimal = 0
    Public SommaVariazioniCapTOT As Decimal = 0


    Public SommaBungetTOT As Decimal = 0
    Public SommaAssestatoTOT As Decimal = 0
    Public SommaPrenotatoTOT As Decimal = 0
    Public SommaConsuntivatoTOT As Decimal = 0
    Public SommaLiquidatoTOT As Decimal = 0
    Public SommaResiduoTOT As Decimal = 0


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        Response.Expires = 0


        If Not IsPostBack Then

            StampaSC()
        End If


    End Sub


    Public Sub StampaSC()

        Dim Contatore As Integer = 1
        Dim Capitolo1 As Boolean = False
        Dim Capitolo2 As Boolean = False
        Dim Capitolo3 As Boolean = False
        Dim Capitolo4 As Boolean = False
        Dim Capitolo5 As Boolean = False

        'Dim Id_Operatore As String
        Dim sStringaSql As String

        Dim SommaBudget As Decimal = 0
        Dim SommaAssestato As Decimal = 0
        Dim SommaPrenotato As Decimal = 0
        Dim SommaConsuntivato As Decimal = 0
        Dim SommaLiquidato As Decimal = 0
        Dim SommaResiduo As Decimal = 0
        Dim SommaVariazioni As Decimal = 0


        Dim sDataEsercizio As String = ""
        Dim sRisultato As String = ""

        Dim ElencoPF_VOCI_ID As String

        Dim FlagConnessione As Boolean



        Try
            Contatore = 1

            sValoreData_Dal = Request.QueryString("DAL")
            sValoreData_Al = Request.QueryString("AL")

            ID_EF = Request.QueryString("ID")


            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If

            sDataEsercizio = " DAL " & par.FormattaData(sValoreData_Dal) & " AL " & par.FormattaData(sValoreData_Al)


            'Id_Operatore = Session.Item("ID_OPERATORE") 

            TestoPagina = "<p style='font-family: ARIAL; font-size: 14pt; font-weight: bold; text-align: center;'>SITUAZIONE CONTABILE: " & sDataEsercizio & "</p></br>"

            TestoPagina = TestoPagina & "<table style='width: 100%;' cellpadding=0 cellspacing = 0'>"
            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                                      & "<td align='left'  style='border-bottom-style: dashed; width:7%;  border-bottom-width: 1px; border-bottom-color: #000000'>COD.</td>" _
                                      & "<td align='left'  style='border-bottom-style: dashed; width:33%; border-bottom-width: 1px; border-bottom-color: #000000'>VOCE</td>" _
                                      & "<td align='right' style='border-bottom-style: dashed; width:11%; border-bottom-width: 1px; border-bottom-color: #000000'>BUDGET INIZIALE</td>" _
                                      & "<td align='right' style='border-bottom-style: dashed; width:11%; border-bottom-width: 1px; border-bottom-color: #000000'>BUDGET ASSESTATO + VAR.</td>" _
                                      & "<td align='right' style='border-bottom-style: dashed; width:10%; border-bottom-width: 1px; border-bottom-color: #000000'>DISPONIBILITA' RESIDUA</td>" _
                                      & "<td align='right' style='border-bottom-style: dashed; width:10%; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE     PRENOTATO</td>" _
                                      & "<td align='right' style='border-bottom-style: dashed; width:10%; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE     CONSUNTIVATO</td>" _
                                      & "<td align='right' style='border-bottom-style: dashed; width:8%; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE             PAGATO</td>" _
                                      & "</tr>"


            sStringaSql = " select PF_VOCI.* " _
                        & " from SISCOM_MI.PF_VOCI " _
                        & " where PF_VOCI.ID_PIANO_FINANZIARIO in " _
                                    & " (select ID from SISCOM_MI.PF_MAIN where ID_STATO=5 and ID_ESERCIZIO_FINANZIARIO=" & ID_EF & ")"

            If Request.QueryString("VOCI") = "False" Then
                sStringaSql = sStringaSql & " and length(PF_VOCI.CODICE)<10 "
            End If

            sStringaSql = sStringaSql & " order by CODICE"

            par.cmd.CommandText = sStringaSql
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()

            While myReader1.Read

                '**** SOMMA IMPORTI x RIGA

                SommaBudget = 0
                SommaAssestato = 0
                SommaPrenotato = 0
                SommaConsuntivato = 0
                SommaLiquidato = 0
                SommaVariazioni = 0

                'ESTRAGGO per ogni VOCE, la sua VOVCE + tutte le VOCI figlie di eventuali sottofigli
                ElencoPF_VOCI_ID = ""
                par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI " _
                                  & "  where ID=" & myReader1("ID") _
                                  & "     or ID_VOCE_MADRE=" & myReader1("ID") _
                                  & "     or ID_VOCE_MADRE in (select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & myReader1("ID") & ") order by CODICE"

                Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                While myReader2.Read
                    If ElencoPF_VOCI_ID = "" Then
                        ElencoPF_VOCI_ID = par.IfNull(myReader2(0), "")
                    Else
                        ElencoPF_VOCI_ID = ElencoPF_VOCI_ID & "," & par.IfNull(myReader2(0), "")
                    End If

                End While
                myReader2.Close()

                'X RIGA *********************************************************
                'SOMMA VALORE LORDO + ASSESTATO 
                par.cmd.CommandText = "select ID_VOCE,to_char(VALORE_LORDO)  as VALORE_LORDO " _
                                   & " from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE in (" & ElencoPF_VOCI_ID & ")" ' myReader1("ID")

                SommaBudget = 0

                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read

                    par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & myReader2("ID_VOCE")

                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader3.Read Then

                    Else
                        sRisultato = par.IfNull(myReader2("VALORE_LORDO"), "0")
                        SommaBudget = SommaBudget + Decimal.Parse(sRisultato)
                    End If
                    myReader3.Close()

                End While
                myReader2.Close()


                'SOMMA VALORE ASSESSTATO LORDO
                par.cmd.CommandText = "select ID_VOCE,to_char(ASSESTAMENTO_VALORE_LORDO) as ASSESTAMENTO_VALORE_LORDO,DATA_ASSESTAMENTO from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE in (" & ElencoPF_VOCI_ID & ")" ' myReader1("ID")

                SommaAssestato = 0

                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read

                    par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & myReader2("ID_VOCE")

                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader3.Read Then

                    Else
                        If par.IfNull(myReader2("DATA_ASSESTAMENTO"), "") = "" Then
                            sRisultato = par.IfNull(myReader2("ASSESTAMENTO_VALORE_LORDO"), "0")
                            SommaAssestato = SommaAssestato + Decimal.Parse(sRisultato)
                        Else
                            If myReader2("DATA_ASSESTAMENTO") <= sValoreData_Al Then
                                sRisultato = par.IfNull(myReader2("ASSESTAMENTO_VALORE_LORDO"), "0")
                                SommaAssestato = SommaAssestato + Decimal.Parse(sRisultato)
                            End If
                        End If
                    End If
                    myReader3.Close()

                End While
                myReader2.Close()


                'SOMMA VARIAZIONI
                par.cmd.CommandText = "select ID_VOCE,to_char(VARIAZIONI)  as VARIAZIONI " _
                                   & " from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE in (" & ElencoPF_VOCI_ID & ")"

                SommaVariazioni = 0

                myReader2 = par.cmd.ExecuteReader()
                While myReader2.Read

                    par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & myReader2("ID_VOCE")

                    Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReader3.Read Then

                    Else
                        sRisultato = par.IfNull(myReader2("VARIAZIONI"), "0")
                        SommaVariazioni = SommaVariazioni + Decimal.Parse(sRisultato)
                    End If
                    myReader3.Close()

                End While
                myReader2.Close()


                SommaAssestato = SommaBudget + SommaAssestato + SommaVariazioni


                'SommaBungetCapTOT = SommaBungetCapTOT + SommaBudget 'par.IfNull(myReader1("valore"), 0)
                'SommaBungetTOT = SommaBungetTOT + SommaBudget 'par.IfNull(myReader1("valore"), 0)


                'If par.IfNull(myReader1("DATA_ASSESTAMENTO"), "") = "" Then
                '    SommaAssestato = par.IfNull(myReader1("valore"), "0")
                '    SommaAssestatoCapTOT = SommaAssestatoCapTOT + par.IfNull(myReader1("valore"), "0")
                '    SommaAssestatoTOT = SommaAssestatoTOT + par.IfNull(myReader1("valore"), 0)
                'Else
                '    If myReader1("DATA_ASSESTAMENTO") <= sValoreData_Al Then
                '        SommaAssestatoCapTOT = SommaAssestatoCapTOT + par.IfNull(myReader1("valore"), "0") + par.IfNull(myReader1("VALORE_ASSESTAMENTO"), "0")
                '        SommaAssestato = par.IfNull(myReader1("valore"), "0") + par.IfNull(myReader1("VALORE_ASSESTAMENTO"), "0")
                '        SommaAssestatoTOT = SommaAssestatoTOT + par.IfNull(myReader1("valore"), 0) + par.IfNull(myReader1("VALORE_ASSESTAMENTO"), "0")

                '    Else
                '        SommaAssestato = par.IfNull(myReader1("valore"), "0")
                '        SommaAssestatoCapTOT = SommaAssestatoCapTOT + par.IfNull(myReader1("valore"), "0")
                '        SommaAssestatoTOT = SommaAssestatoTOT + par.IfNull(myReader1("valore"), 0)
                '    End If
                'End If

                'IMPORTO PRENOTATO [BOZZA o DA APPROVARE]
                par.cmd.CommandText = " select to_char(SUM(IMPORTO_PRENOTATO)) " _
                                    & " from    SISCOM_MI.PRENOTAZIONI " _
                                    & " where  (ID_STATO=0 or ID_STATO=1) " _
                                    & "   and   ID_PAGAMENTO is null " _
                                    & "   and   DATA_PRENOTAZIONE<=" & sValoreData_Al _
                                    & "   and   ID_VOCE_PF in (" & ElencoPF_VOCI_ID & ")" ' myReader1("ID")

                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    sRisultato = par.IfNull(myReader2(0), "0")
                    SommaPrenotato = Decimal.Parse(sRisultato)
                End If
                myReader2.Close()
                '*************************

                'IMPORTO PRENOTATO [CONSUNTIVATO o APPROVATO]
                par.cmd.CommandText = " select to_char(SUM(IMPORTO_APPROVATO)) " _
                                    & " from    SISCOM_MI.PRENOTAZIONI " _
                                    & " where   ID_STATO>1 " _
                                    & "   and   ID_PAGAMENTO is NOT null " _
                                    & "   and   DATA_PRENOTAZIONE<=" & sValoreData_Al _
                                    & "   and  ID_VOCE_PF in (" & ElencoPF_VOCI_ID & ")" ' myReader1("ID")

                myReader2 = par.cmd.ExecuteReader()
                If myReader2.Read Then
                    sRisultato = par.IfNull(myReader2(0), "0")
                    SommaConsuntivato = Decimal.Parse(sRisultato)
                End If
                myReader2.Close()
                '****************************


                'IMPORTO LIQUIDATO
                'par.cmd.CommandText = "select to_char(IMPORTO_CONSUNTIVATO) as IMPORTO_CONSUNTIVATO,DATA_EMISSIONE " _
                '                   & " from SISCOM_MI.PAGAMENTI " _
                '                   & " where DATA_STAMPA is NOT null " _
                '                   & "   and ID_STATO=5 " _
                '                   & "   and PAGAMENTI.ID in (select ID_PAGAMENTO " _
                '                                           & " from   SISCOM_MI.PRENOTAZIONI " _
                '                                           & " where  ID_VOCE_PF in (" & ElencoPF_VOCI_ID & ")" _
                '                                           & "   and  DATA_PRENOTAZIONE<=" & sValoreData_Al _
                '                                           & "   and  ID_STATO>=2 )"


                par.cmd.CommandText = "select to_char(SUM(IMPORTO)) " _
                                   & " from SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                                   & " where ID_PAGAMENTO in (select distinct(ID_PAGAMENTO) " _
                                                          & " from   SISCOM_MI.PRENOTAZIONI " _
                                                          & " where  ID_VOCE_PF in (" & ElencoPF_VOCI_ID & ")" _
                                                          & "   and  DATA_PRENOTAZIONE<=" & sValoreData_Al _
                                                          & "   and  ID_STATO>=2 )"


                myReader2 = par.cmd.ExecuteReader()

                'While myReader2.Read
                '    If par.IfNull(myReader2("DATA_MANDATO"), 0) > 0 And par.IfNull(myReader2("DATA_MANDATO"), 0) <= sValoreData_Al Then

                '        sRisultato = par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), "0")
                '        SommaLiquidato = Decimal.Parse(sRisultato)
                '    End If
                'End While
                If myReader2.Read Then
                    sRisultato = par.IfNull(myReader2(0), "0")
                    SommaLiquidato = Decimal.Parse(sRisultato)
                End If
                myReader2.Close()


                myReader2.Close()

                SommaConsuntivato = SommaConsuntivato - SommaLiquidato
                'X RIGA FINE *********************************************************


                If Capitolo1 = False Then
                    If Strings.Left(myReader1("CODICE"), 2) = "1." Then

                        CalcoliTotale("1")

                        SommaResiduoCapTOT = SommaAssestatoCapTOT - (SommaPrenotatoCapTOT + SommaConsuntivatoCapTOT + SommaLiquidatoCapTOT)

                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                                                  & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>1</td>" _
                                                  & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>Spese per il property management</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaBungetCapTOT, "##,##0.00") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaAssestatoCapTOT, "##,##0.00") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaResiduoCapTOT, "##,##0.00") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaPrenotatoCapTOT, "##,##0.00") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaConsuntivatoCapTOT, "##,##0.00") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaLiquidatoCapTOT, "##,##0.00") & "</td>" _
                                                  & "</tr>"
                        Capitolo1 = True
                        Contatore = Contatore + 1
                    End If
                End If

                If Capitolo2 = False Then
                    If Strings.Left(myReader1("CODICE"), 2) = "2." Then

                        CalcoliTotale("2")

                        SommaResiduoCapTOT = SommaAssestatoCapTOT - (SommaPrenotatoCapTOT + SommaConsuntivatoCapTOT + SommaLiquidatoCapTOT)

                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                                                  & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>2</td>" _
                                                  & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>Spese per il facility management</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaBungetCapTOT, "##,##0.00") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaAssestatoCapTOT, "##,##0.00") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaResiduoCapTOT, "##,##0.00") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaPrenotatoCapTOT, "##,##0.00") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaConsuntivatoCapTOT, "##,##0.00") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaLiquidatoCapTOT, "##,##0.00") & "</td>" _
                                                  & "</tr>"

                        Contatore = Contatore + 1
                        Capitolo2 = True

                    End If
                End If


                If Capitolo3 = False Then
                    If Strings.Left(myReader1("CODICE"), 2) = "3." Then

                        CalcoliTotale("3")
                        SommaResiduoCapTOT = SommaAssestatoCapTOT - (SommaPrenotatoCapTOT + SommaConsuntivatoCapTOT + SommaLiquidatoCapTOT)


                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                                                  & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>3</td>" _
                                                  & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>Spese per contributi per sostegno agli inquilini</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaBungetCapTOT, "##,##0.00") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaAssestatoCapTOT, "##,##0.00") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaResiduoCapTOT, "##,##0.00") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaPrenotatoCapTOT, "##,##0.00") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaConsuntivatoCapTOT, "##,##0.00") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaLiquidatoCapTOT, "##,##0.00") & "</td>" _
                                                  & "</tr>"
                        Contatore = Contatore + 1
                        Capitolo3 = True

                    End If
                End If

                If Capitolo4 = False Then
                    If Strings.Left(myReader1("CODICE"), 2) = "4." Then

                        CalcoliTotale("4")

                        SommaResiduoCapTOT = SommaAssestatoCapTOT - (SommaPrenotatoCapTOT + SommaConsuntivatoCapTOT + SommaLiquidatoCapTOT)

                        TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                                                  & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>4</td>" _
                                                  & "<td align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>Spese diverse</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaBungetCapTOT, "##,##0.00") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaAssestatoCapTOT, "##,##0.00") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaResiduoCapTOT, "##,##0.00") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaPrenotatoCapTOT, "##,##0.00") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaConsuntivatoCapTOT, "##,##0.00") & "</td>" _
                                                  & "<td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaLiquidatoCapTOT, "##,##0.00") & "</td>" _
                                                  & "</tr>"

                        Contatore = Contatore + 1
                        Capitolo4 = True

                    End If
                End If



                SommaResiduo = SommaAssestato - (SommaPrenotato + SommaConsuntivato + SommaLiquidato)

                TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;'>" _
                                          & "<td align='left'  style='border-bottom-style: dashed;  width:7%; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("codice"), "") & "</td>" _
                                          & "<td align='left'  style='border-bottom-style: dashed;  width:33%; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & par.IfNull(myReader1("descrizione"), "") & "</td>" _
                                          & "<td align='right' style='border-bottom-style: dashed;  width:11%; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaBudget, "##,##0.00") & "</td>" _
                                          & "<td align='right' style='border-bottom-style: dashed;  width:11%; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaAssestato, "##,##0.00") & "</td>" _
                                          & "<td align='right' style='border-bottom-style: dashed;  width:10%; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaResiduo, "##,##0.00") & "</td>" _
                                          & "<td align='right' style='border-bottom-style: dashed;  width:10%; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaPrenotato, "##,##0.00") & "</td>" _
                                          & "<td align='right' style='border-bottom-style: dashed;  width:10%; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaConsuntivato, "##,##0.00") & "</td>" _
                                          & "<td align='right' style='border-bottom-style: dashed;  width:8%; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>" & Format(SommaLiquidato, "##,##0.00") & "</td>" _
                                          & "</tr>"

                Contatore = Contatore + 1

                If Contatore = 20 Then
                    Contatore = 1
                    TestoPagina = TestoPagina & "</table>"
                    TestoPagina = TestoPagina & "<p style='page-break-before: always'>&nbsp;</p>"

                    TestoPagina = TestoPagina & "<table style='width: 100%;' cellpadding=0 cellspacing = 0'>"
                    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'>" _
                                              & "<td align='left'  style='border-bottom-style: dashed; width:7%;  border-bottom-width: 1px; border-bottom-color: #000000'>COD.</td>" _
                                              & "<td align='left'  style='border-bottom-style: dashed; width:33%; border-bottom-width: 1px; border-bottom-color: #000000'>VOCE</td>" _
                                              & "<td align='right' style='border-bottom-style: dashed; width:11%; border-bottom-width: 1px; border-bottom-color: #000000'>BUDGET INIZIALE</td>" _
                                              & "<td align='right' style='border-bottom-style: dashed; width:11%; border-bottom-width: 1px; border-bottom-color: #000000'>BUDGET ASSESTATO + VAR.</td>" _
                                              & "<td align='right' style='border-bottom-style: dashed; width:10%; border-bottom-width: 1px; border-bottom-color: #000000'>DISPONIBILITA' RESIDUA</td>" _
                                              & "<td align='right' style='border-bottom-style: dashed; width:10%; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE     PRENOTATO</td>" _
                                              & "<td align='right' style='border-bottom-style: dashed; width:10%; border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE     CONSUNTIVATO</td>" _
                                              & "<td align='right' style='border-bottom-style: dashed; width:8%;  border-bottom-width: 1px; border-bottom-color: #000000'>TOTALE             PAGATO</td>" _
                                              & "</tr>"

                    'TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: arial; font-size: 9pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' width='2%'>COD.</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>VOCE</td><td align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>IMPORTO</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;&nbsp;PROSEGUI</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"

                End If

            End While


            'If Capitolo1 = False Then

            '    'TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>1</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per il property management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>&nbsp;</td></tr>"

            '    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>1</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>Spese per il property management</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'></td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'></td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'></td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'></td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'></td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'></td>" _
            '                              & "</tr>"

            '    Contatore = Contatore + 1
            'End If


            'If Capitolo2 = False Then

            '    'TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TOTALE Spese per il property management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td></tr>"
            '    SommaBungetCapTOT = SommaBungetCapTOT - SommaBudget
            '    SommaAssestatoCapTOT = SommaAssestatoCapTOT - SommaBudget - SommaAssestato

            '    SommaResiduoCapTOT = SommaAssestatoCapTOT - (SommaPrenotatoCapTOT + SommaConsuntivatoCapTOT + SommaLiquidatoCapTOT)

            '    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;&nbsp;&nbsp;TOTALE Spese per il property management</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(SommaBungetCapTOT, "##,##0.00") & "</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(SommaAssestatoCapTOT, "##,##0.00") & "</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(SommaResiduoCapTOT, "##,##0.00") & "</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(SommaPrenotatoCapTOT, "##,##0.00") & "</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(SommaConsuntivatoCapTOT, "##,##0.00") & "</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(SommaLiquidatoCapTOT, "##,##0.00") & "</td>" _
            '                              & "</tr>"
            '    Contatore = Contatore + 1

            '    SommaBungetCapTOT = 0
            '    SommaAssestatoCapTOT = 0


            '    SommaPrenotatoCapTOT = 0
            '    SommaConsuntivatoCapTOT = 0
            '    SommaLiquidatoCapTOT = 0

            '    'TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>2</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per il facility management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"

            '    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>2</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>Spese per il facility management</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'></td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'></td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'></td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'></td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'></td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'></td>" _
            '                              & "</tr>"

            '    Contatore = Contatore + 1
            '    Capitolo2 = True

            'End If


            'If Capitolo3 = False Then

            '    'TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TOTALE Spese per il facility management</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td></tr>"

            '    SommaBungetCapTOT = SommaBungetCapTOT - SommaBudget
            '    SommaAssestatoCapTOT = SommaAssestatoCapTOT - SommaBudget - SommaAssestato

            '    SommaResiduoCapTOT = SommaAssestatoCapTOT - (SommaPrenotatoCapTOT + SommaConsuntivatoCapTOT + SommaLiquidatoCapTOT)

            '    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;&nbsp;&nbsp;TOTALE Spese per il facility management</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(SommaBungetCapTOT, "##,##0.00") & "</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(SommaAssestatoCapTOT, "##,##0.00") & "</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(SommaResiduoCapTOT, "##,##0.00") & "</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(SommaPrenotatoCapTOT, "##,##0.00") & "</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(SommaConsuntivatoCapTOT, "##,##0.00") & "</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(SommaLiquidatoCapTOT, "##,##0.00") & "</td>" _
            '                              & "</tr>"
            '    Contatore = Contatore + 1

            '    SommaBungetCapTOT = 0
            '    SommaAssestatoCapTOT = 0

            '    SommaPrenotatoCapTOT = 0
            '    SommaConsuntivatoCapTOT = 0
            '    SommaLiquidatoCapTOT = 0

            '    'TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>3</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese per contributi per sostegno agli inquilini</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"

            '    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>3</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>Spese per contributi per sostegno agli inquilini</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'></td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'></td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'></td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'></td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'></td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'></td>" _
            '                              & "</tr>"

            '    Contatore = Contatore + 1
            '    Capitolo3 = True

            'End If

            'If Capitolo4 = False Then
            '    'TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TOTALE Spese per contributi per sostegno agli inquilini</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td></tr>"

            '    SommaBungetCapTOT = SommaBungetCapTOT - SommaBudget
            '    SommaAssestatoCapTOT = SommaAssestatoCapTOT - SommaBudget - SommaAssestato

            '    SommaResiduoCapTOT = SommaAssestatoCapTOT - (SommaPrenotatoCapTOT + SommaConsuntivatoCapTOT + SommaLiquidatoCapTOT)

            '    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;&nbsp;&nbsp;TOTALE Spese per contributi per sostegno agli inquilini</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(SommaBungetCapTOT, "##,##0.00") & "</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(SommaAssestatoCapTOT, "##,##0.00") & "</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(SommaResiduoCapTOT, "##,##0.00") & "</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(SommaPrenotatoCapTOT, "##,##0.00") & "</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(SommaConsuntivatoCapTOT, "##,##0.00") & "</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(SommaLiquidatoCapTOT, "##,##0.00") & "</td>" _
            '                              & "</tr>"

            '    Contatore = Contatore + 1

            '    SommaBungetCapTOT = 0
            '    SommaAssestatoCapTOT = 0

            '    SommaPrenotatoCapTOT = 0
            '    SommaConsuntivatoCapTOT = 0
            '    SommaLiquidatoCapTOT = 0

            '    'TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border: 2px dashed #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>4</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>Spese diverse</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td></tr>"

            '    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
            '          & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>4</td>" _
            '          & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>Spese diverse</td>" _
            '          & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'></td>" _
            '          & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'></td>" _
            '          & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'></td>" _
            '          & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'></td>" _
            '          & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'></td>" _
            '          & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'></td>" _
            '          & "</tr>"

            '    Contatore = Contatore + 1
            '    Capitolo4 = True

            'End If

            'If Capitolo5 = False Then
            '    'TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;TOTALE Spese diverse</td><td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000' align='right'>" & Format(Totale, "##,##0.00") & "</td></tr>"

            '    SommaBungetCapTOT = SommaBungetCapTOT - SommaBudget
            '    SommaAssestatoCapTOT = SommaAssestatoCapTOT - SommaBudget - SommaAssestato

            '    SommaResiduoCapTOT = SommaAssestatoCapTOT - (SommaPrenotatoCapTOT + SommaConsuntivatoCapTOT + SommaLiquidatoCapTOT)

            '    TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt'>&nbsp;&nbsp;&nbsp;TOTALE Spese diverse</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(SommaBungetCapTOT, "##,##0.00") & "</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(SommaAssestatoCapTOT, "##,##0.00") & "</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(SommaResiduoCapTOT, "##,##0.00") & "</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(SommaPrenotatoCapTOT, "##,##0.00") & "</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(SommaConsuntivatoCapTOT, "##,##0.00") & "</td>" _
            '                              & "<td style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000;font-family: ARIAL; font-size: 8pt' align='right'>" & Format(SommaLiquidatoCapTOT, "##,##0.00") & "</td>" _
            '                              & "</tr>"

            '    Contatore = Contatore + 1

            '    SommaBungetCapTOT = 0
            '    SommaAssestatoCapTOT = 0

            '    SommaPrenotatoCapTOT = 0
            '    SommaConsuntivatoCapTOT = 0
            '    SommaLiquidatoCapTOT = 0
            '    Capitolo5 = True
            'End If


            SommaResiduoTOT = SommaAssestatoTOT - (SommaPrenotatoTOT + SommaConsuntivatoTOT + SommaLiquidatoTOT)

            TestoPagina = TestoPagina & "<tr style='border-bottom-style: dashed; border-bottom-width: 2px; border-bottom-color: #000000;border-width: 1px; border-color: #000000; height: 30px; border-bottom-style: solid;font-family: ARIAL; font-size: 10pt; font-weight: bold'>" _
                                      & "<td  align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;</td>" _
                                      & "<td  align='left'  style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>&nbsp;TOTALE </td>" _
                                      & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & Format(SommaBungetTOT, "##,##0.00") & "</td>" _
                                      & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & Format(SommaAssestatoTOT, "##,##0.00") & "</td>" _
                                      & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & Format(SommaResiduoTOT, "##,##0.00") & "</td>" _
                                      & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & Format(SommaPrenotatoTOT, "##,##0.00") & "</td>" _
                                      & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & Format(SommaConsuntivatoTOT, "##,##0.00") & "</td>" _
                                      & "<td  align='right' style='border-bottom-style: dashed; border-bottom-width: 1px; border-bottom-color: #000000'>" & Format(SommaLiquidatoTOT, "##,##0.00") & "</td>" _
                                      & "</tr>"

            myReader1.Close()

            TestoPagina = TestoPagina & "</table>"



            Dim NomeFile As String = "SC_" & Format(Now, "yyyyMMddHHmmss")

            'x le prove
            'Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".htm", False, System.Text.Encoding.Default)
            '**************

            Dim sr As StreamWriter = New StreamWriter(Server.MapPath("..\FileTemp\") & NomeFile & ".htm", False, System.Text.Encoding.Default)

            sr.WriteLine(TestoPagina)
            sr.Close()

            Dim url As String = NomeFile
            Dim Licenza As String = Session.Item("LicenzaHtmlToPdf")

            Dim pdfConverter As PdfConverter = New PdfConverter

            If Licenza <> "" Then
                pdfConverter.LicenseKey = Licenza
            End If

            pdfConverter.PdfDocumentOptions.PdfPageSize = PdfPageSize.A4
            pdfConverter.PdfDocumentOptions.PdfPageOrientation = PDFPageOrientation.Landscape
            pdfConverter.PdfDocumentOptions.PdfCompressionLevel = PdfCompressionLevel.NoCompression
            pdfConverter.PdfDocumentOptions.ShowHeader = False
            pdfConverter.PdfDocumentOptions.ShowFooter = False
            pdfConverter.PdfDocumentOptions.LeftMargin = 20
            pdfConverter.PdfDocumentOptions.RightMargin = 20
            pdfConverter.PdfDocumentOptions.TopMargin = 5
            pdfConverter.PdfDocumentOptions.BottomMargin = 5
            pdfConverter.PdfDocumentOptions.GenerateSelectablePdf = True

            pdfConverter.PdfDocumentOptions.ShowHeader = False
            pdfConverter.PdfFooterOptions.FooterText = ("")
            pdfConverter.PdfFooterOptions.FooterTextColor = Color.Blue
            pdfConverter.PdfFooterOptions.DrawFooterLine = False
            pdfConverter.PdfFooterOptions.PageNumberText = ""
            pdfConverter.PdfFooterOptions.ShowPageNumber = False


            'x le prove 
            'pdfConverter.SavePdfFromUrlToFile(Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".htm", Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".pdf")
            'IO.File.Delete(Server.MapPath("..\..\..\FileTemp\") & NomeFile & ".htm")
            'Response.Redirect("..\..\..\FileTemp\" & NomeFile & ".pdf")
            '******************

            pdfConverter.SavePdfFromUrlToFile(Server.MapPath("..\FileTemp\") & NomeFile & ".htm", Server.MapPath("..\FileTemp\") & NomeFile & ".pdf")
            IO.File.Delete(Server.MapPath("..\FileTemp\") & NomeFile & ".htm")
            Response.Redirect("..\FileTemp\" & NomeFile & ".pdf")

            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Response.Write(ex.Message)
        End Try


    End Sub






    Public Sub CalcoliTotale(ByVal CODICE As String)
        Dim FlagConnessione As Boolean
        Dim sRisultato As String = ""
        Dim ElencoPF_VOCI_ID As String = ""

        'X CAPITOLO e TOTALE REPORT di TUTTO ****************************************

        Try

            '*******************APERURA CONNESSIONE*********************
            FlagConnessione = False
            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)

                FlagConnessione = True
            End If


            SommaBungetCapTOT = 0
            SommaAssestatoCapTOT = 0
            SommaVariazioniCapTOT = 0

            SommaPrenotatoCapTOT = 0
            SommaConsuntivatoCapTOT = 0
            SommaLiquidatoCapTOT = 0


            sValoreData_Dal = Request.QueryString("DAL")
            sValoreData_Al = Request.QueryString("AL")

            ID_EF = Request.QueryString("ID")

            par.cmd.CommandText = " select PF_VOCI.* " _
                                & " from SISCOM_MI.PF_VOCI " _
                                & " where PF_VOCI.ID_PIANO_FINANZIARIO in " _
                                            & " (select ID from SISCOM_MI.PF_MAIN where ID_STATO=5 and ID_ESERCIZIO_FINANZIARIO=" & ID_EF & ")"


            par.cmd.CommandText = par.cmd.CommandText & " and CODICE LIKE ('" & CODICE & ".%') order by CODICE"

            Dim myReader2 As Oracle.DataAccess.Client.OracleDataReader

            myReader2 = par.cmd.ExecuteReader()
            While myReader2.Read
                If ElencoPF_VOCI_ID = "" Then
                    ElencoPF_VOCI_ID = par.IfNull(myReader2(0), "")
                Else
                    ElencoPF_VOCI_ID = ElencoPF_VOCI_ID & "," & par.IfNull(myReader2(0), "")
                End If

            End While
            myReader2.Close()


            'SOMMA VALORE LORDO 
            par.cmd.CommandText = "select ID_VOCE,to_char(VALORE_LORDO)  as VALORE_LORDO " _
                               & " from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE in (" & ElencoPF_VOCI_ID & ")" ' myReader1("ID")

            myReader2 = par.cmd.ExecuteReader()
            While myReader2.Read

                par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & myReader2("ID_VOCE")

                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader3.Read Then

                Else
                    sRisultato = par.IfNull(myReader2("VALORE_LORDO"), "0")

                    SommaBungetCapTOT = SommaBungetCapTOT + Decimal.Parse(sRisultato)    'x CAPITOLO

                End If
                myReader3.Close()

            End While
            myReader2.Close()
            '----------------------------------


            'SOMMA VALORE ASSESSTATO LORDO
            par.cmd.CommandText = "select ID_VOCE,to_char(ASSESTAMENTO_VALORE_LORDO) as ASSESTAMENTO_VALORE_LORDO,DATA_ASSESTAMENTO from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE in (" & ElencoPF_VOCI_ID & ")" ' myReader1("ID")

            myReader2 = par.cmd.ExecuteReader()
            While myReader2.Read

                par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & myReader2("ID_VOCE")

                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader3.Read Then

                Else
                    If par.IfNull(myReader2("DATA_ASSESTAMENTO"), "") = "" Then
                        sRisultato = par.IfNull(myReader2("ASSESTAMENTO_VALORE_LORDO"), "0")
                        SommaAssestatoCapTOT = SommaAssestatoCapTOT + Decimal.Parse(sRisultato)
                    Else
                        If myReader2("DATA_ASSESTAMENTO") <= sValoreData_Al Then
                            sRisultato = par.IfNull(myReader2("ASSESTAMENTO_VALORE_LORDO"), "0")
                            SommaAssestatoCapTOT = SommaAssestatoCapTOT + Decimal.Parse(sRisultato)
                        End If
                    End If
                End If
                myReader3.Close()

            End While
            myReader2.Close()


            'SOMMA VARIAZIONI
            par.cmd.CommandText = "select ID_VOCE,to_char(VARIAZIONI)  as VARIAZIONI " _
                               & " from SISCOM_MI.PF_VOCI_STRUTTURA where ID_VOCE in (" & ElencoPF_VOCI_ID & ")" ' myReader1("ID")

            myReader2 = par.cmd.ExecuteReader()
            While myReader2.Read

                par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & myReader2("ID_VOCE")

                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader3.Read Then

                Else
                    sRisultato = par.IfNull(myReader2("VARIAZIONI"), "0")

                    SommaVariazioniCapTOT = SommaVariazioniCapTOT + Decimal.Parse(sRisultato)    'x CAPITOLO
                End If
                myReader3.Close()

            End While
            myReader2.Close()
            '----------------------------------


            SommaAssestatoCapTOT = SommaBungetCapTOT + SommaAssestatoCapTOT + SommaVariazioniCapTOT     'x CAPITOLO
            '*******************************

            'IMPORTO PRENOTATO [BOZZA o DA APPROVARE]
            par.cmd.CommandText = " select  ID_VOCE_PF,to_char(IMPORTO_PRENOTATO) as IMPORTO_PRENOTATO " _
                                & " from    SISCOM_MI.PRENOTAZIONI " _
                                & " where   (ID_STATO=0 or ID_STATO=1) " _
                                & "   and   ID_PAGAMENTO is null " _
                                & "   and   DATA_PRENOTAZIONE<=" & sValoreData_Al _
                                & "   and   ID_VOCE_PF in (" & ElencoPF_VOCI_ID & ")"


            myReader2 = par.cmd.ExecuteReader()
            While myReader2.Read

                par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & myReader2("ID_VOCE_PF")

                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader3.Read Then

                Else
                    sRisultato = par.IfNull(myReader2("IMPORTO_PRENOTATO"), "0")

                    SommaPrenotatoCapTOT = SommaPrenotatoCapTOT + Decimal.Parse(sRisultato) 'x CAPITOLO
                End If
                myReader3.Close()

            End While
            myReader2.Close()
            '********************************


            'IMPORTO CONSUNTIVATO (30/05/2011 abbiamo scoperto che non si puù prendere da PAGAMENTI, perchè un pagamento può contenere prenotazioni di voci diverse)
            par.cmd.CommandText = "select  ID_VOCE_PF,to_char(IMPORTO_APPROVATO) as IMPORTO_APPROVATO " _
                                & " from   SISCOM_MI.PRENOTAZIONI " _
                                & " where  ID_PAGAMENTO is NOT null " _
                                & "   and  ID_STATO>1 " _
                                & "   and   DATA_PRENOTAZIONE<=" & sValoreData_Al _
                                & "   and  ID_VOCE_PF in (" & ElencoPF_VOCI_ID & ")"

            myReader2 = par.cmd.ExecuteReader
            While myReader2.Read

                par.cmd.CommandText = "select ID from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE=" & myReader2("ID_VOCE_PF")

                Dim myReader3 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                If myReader3.Read Then

                Else
                    sRisultato = par.IfNull(myReader2("IMPORTO_APPROVATO"), "0")

                    SommaConsuntivatoCapTOT = SommaConsuntivatoCapTOT + Decimal.Parse(sRisultato)   'x CAPITOLO
                End If
                myReader3.Close()

            End While
            myReader2.Close()


            'IMPORTO LIQUIDATO
            par.cmd.Parameters.Clear()
            'par.cmd.CommandText = "select to_char(IMPORTO_CONSUNTIVATO) as IMPORTO_CONSUNTIVATO,DATA_EMISSIONE " _
            '                   & " from SISCOM_MI.PAGAMENTI " _
            '                   & "  where DATA_STAMPA is NOT null " _
            '                   & "    and ID_STATO=5 " _
            '                   & "    and PAGAMENTI.ID in (select ID_PAGAMENTO " _
            '                                            & " from   SISCOM_MI.PRENOTAZIONI " _
            '                                            & " where  ID_VOCE_PF in (" & ElencoPF_VOCI_ID & ")" _
            '                                            & "   and  DATA_PRENOTAZIONE<=" & sValoreData_Al _
            '                                            & "   and  ID_VOCE_PF NOT IN (select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE in (" & ElencoPF_VOCI_ID & "))" _
            '                                            & "   and  ID_STATO>=2)"


            par.cmd.CommandText = "select to_char(SUM(IMPORTO)) " _
                              & " from SISCOM_MI.PAGAMENTI_LIQUIDATI " _
                              & " where ID_PAGAMENTO in (select distinct(ID_PAGAMENTO) " _
                                                   & " from   SISCOM_MI.PRENOTAZIONI " _
                                                   & " where  ID_VOCE_PF in (" & ElencoPF_VOCI_ID & ")" _
                                                   & "   and  DATA_PRENOTAZIONE<=" & sValoreData_Al _
                                                   & "   and  ID_VOCE_PF NOT IN (select ID_VOCE_MADRE from SISCOM_MI.PF_VOCI where ID_VOCE_MADRE in (" & ElencoPF_VOCI_ID & "))" _
                                                   & "   and  ID_STATO>=2)"



            myReader2 = par.cmd.ExecuteReader
            If myReader2.Read Then

                'If par.IfNull(myReader2("DATA_MANDATO"), 0) > 0 And par.IfNull(myReader2("DATA_MANDATO"), 0) <= sValoreData_Al Then
                'sRisultato = par.IfNull(myReader2("IMPORTO_CONSUNTIVATO"), "0")
                sRisultato = par.IfNull(myReader2(0), "0")

                SommaLiquidatoCapTOT = SommaLiquidatoCapTOT + Decimal.Parse(sRisultato)
            End If
            myReader2.Close()

            SommaConsuntivatoCapTOT = SommaConsuntivatoCapTOT - SommaLiquidatoCapTOT

            'FINE X CAPITOLO 


            'TOTALE REPORT FINE *********************************************************
            SommaBungetTOT = SommaBungetTOT + SommaBungetCapTOT

            SommaAssestatoTOT = SommaAssestatoTOT + SommaAssestatoCapTOT

            SommaPrenotatoTOT = SommaPrenotatoTOT + SommaPrenotatoCapTOT
            SommaConsuntivatoTOT = SommaConsuntivatoTOT + SommaConsuntivatoCapTOT
            SommaLiquidatoTOT = SommaLiquidatoTOT + SommaLiquidatoCapTOT
            '***************************************************************************


            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

        Catch ex As Exception
            If FlagConnessione = True Then
                par.cmd.Dispose()
                par.OracleConn.Close()
                Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            End If

            Response.Write(ex.Message)
        End Try



    End Sub

End Class
