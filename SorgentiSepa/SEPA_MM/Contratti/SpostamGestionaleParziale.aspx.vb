
Partial Class Contratti_SpostamGestionaleParziale
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Protected Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If IsPostBack = False Then
            idContratto = Request.QueryString("IDCONTR")
            idBolletta = Request.QueryString("IDBOLL")

            If Request.QueryString("TIPO") = "DEB" Then
                ElaborazioneParzDebito(idContratto, idBolletta)
            Else
                'ElaborazioneParzCredito(idContratto, idBolletta)
            End If

        End If
    End Sub

    Public Property percentuale() As Integer
        Get
            If Not (ViewState("par_percentuale") Is Nothing) Then
                Return CInt(ViewState("par_percentuale"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Integer)
            ViewState("par_percentuale") = value
        End Set

    End Property

    Public Property idContratto() As Long
        Get
            If Not (ViewState("par_idContratto") Is Nothing) Then
                Return CLng(ViewState("par_idContratto"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idContratto") = value
        End Set

    End Property

    Public Property idBolletta() As Long
        Get
            If Not (ViewState("par_idBolletta") Is Nothing) Then
                Return CLng(ViewState("par_idBolletta"))
            Else
                Return 0
            End If
        End Get

        Set(ByVal value As Long)
            ViewState("par_idBolletta") = value
        End Set

    End Property

    Private Sub RipartizioneImporto()
        Try
            percentuale = Request.QueryString("PERC")
            Response.Write("<script>alert('RipartizioneDebito " & percentuale & "%')</script>")

        Catch ex As Exception

        End Try
    End Sub

    Private Sub ElaborazioneParzDebito(ByVal idContr As Long, ByVal idBollGest As Long)
        Try
            Dim importoIniziale As Decimal = 0
            Dim importoNewBolletta As Decimal = 0
            Dim num_rate As Integer = 0
            Dim importoBolletta As Integer = 0
            Dim IDUNITA As Long = 0
            Dim dataDecorr As String = ""

            If par.OracleConn.State = Data.ConnectionState.Closed Then
                par.OracleConn.Open()
                par.SettaCommand(par)
            End If

            par.myTrans = par.OracleConn.BeginTransaction()
            ‘‘par.cmd.Transaction = par.myTrans

            '*** 
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.RAPPORTI_UTENZA WHERE ID=" & idContr & ""
            Dim myReader0 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader0.Read Then
                importoIniziale = par.IfNull(myReader0("IMP_CANONE_INIZIALE"), 0)
                num_rate = par.IfNull(myReader0("NRO_RATE"), 0)
                dataDecorr = par.IfNull(myReader0("DATA_DECORRENZA"), "")
            End If
            myReader0.Close()

            par.cmd.CommandText = "SELECT ID_UNITA FROM SISCOM_MI.UNITA_CONTRATTUALE WHERE ID_CONTRATTO=" & idContr & ""
            Dim myReaderU As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderU.Read Then
                IDUNITA = par.IfNull(myReaderU("ID_UNITA"), 0)
            End If
            myReaderU.Close()

            par.cmd.CommandText = "SELECT to_char(BOL_BOLLETTE_GEST.IMPORTO_TOTALE,'9G999G990D99') as IMP_EMESSO FROM SISCOM_MI.BOL_BOLLETTE_GEST WHERE ID=" & idBollGest
            Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReader1.Read Then
                importoBolletta = CDec(par.IfNull(myReader1("IMP_EMESSO"), 0))
            End If
            myReader1.Close()

            Dim importoSpesa300 As Decimal = 0
            Dim importoSpesa301 As Decimal = 0
            Dim importoSpesa302 As Decimal = 0
            Dim importoSpesa303 As Decimal = 0
            Dim importoSpesaTOT As Decimal = 0

            Dim BOLLO_BOLLETTA As Decimal = 0
            Dim SPESEPOSTALI As Decimal = 0
            Dim SPMAV As Decimal = 0
            Dim vociAutomatiche As Decimal = 0

            Dim percentualeSost As Integer = Request.QueryString("PERC")
            Dim importoDaConfrontare As Decimal = 0
            Dim rate As Integer = 0
            Dim impRateizzato As Decimal = 0

            par.cmd.CommandText = "SELECT * from SISCOM_MI.BOL_SCHEMA WHERE ID_CONTRATTO=" & idContratto & " AND ANNO=" & Year(Now) & ""
            Dim daSpese As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dtSpese As New Data.DataTable
            daSpese = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            daSpese.Fill(dtSpese)
            daSpese.Dispose()
            For Each row As Data.DataRow In dtSpese.Rows
                Select Case row.Item("ID_VOCE")
                    Case "300"
                        importoSpesa300 = row.Item("IMPORTO_SINGOLA_RATA")
                    Case "301"
                        importoSpesa301 = row.Item("IMPORTO_SINGOLA_RATA")
                    Case "302"
                        importoSpesa302 = row.Item("IMPORTO_SINGOLA_RATA")
                    Case "303"
                        importoSpesa303 = row.Item("IMPORTO_SINGOLA_RATA")
                End Select
            Next

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=0"
            Dim myReaderPar As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
            If myReaderPar.Read Then
                BOLLO_BOLLETTA = CDbl(par.PuntiInVirgole(myReaderPar("VALORE")))
            End If
            myReaderPar.Close()

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=17"
            myReaderPar = par.cmd.ExecuteReader()
            If myReaderPar.Read Then
                SPESEPOSTALI = CDbl(par.PuntiInVirgole(myReaderPar("VALORE")))
            End If
            myReaderPar.Close()

            par.cmd.CommandText = "select VALORE FROM SISCOM_MI.PARAMETRI_BOLLETTA WHERE ID=26"
            myReaderPar = par.cmd.ExecuteReader()
            If myReaderPar.Read Then
                SPMAV = CDbl(par.PuntiInVirgole(myReaderPar("VALORE")))
            End If
            myReaderPar.Close()

            vociAutomatiche = BOLLO_BOLLETTA + SPESEPOSTALI + SPMAV

            Dim ESERCIZIOF As Long = 0
            Dim rataProssima As Integer = 0
            Dim dataProssimoPeriodo As Integer = 0
            'Dim annoDecorrenza As Integer = 0

            ESERCIZIOF = par.RicavaEsercizioCorrente

            importoSpesaTOT = importoSpesa300 + importoSpesa301 + importoSpesa302 + importoSpesa303 + vociAutomatiche
            importoNewBolletta = Format((importoIniziale / num_rate) + importoSpesaTOT, "##,##0.00")

            '***** CALCOLO PER RATA SOSTENIBILE *****
            importoDaConfrontare = (importoNewBolletta * percentualeSost) / 100

            'rataProssima = par.ProssimaRata(num_rate, dataDecorr, dataProssimoPeriodo)

            Dim prossimaBolletta As String = ""
            par.cmd.CommandText = "SELECT * FROM SISCOM_MI.rapporti_utenza_prossima_bol WHERE ID_CONTRATTO=" & idContr
            myReader0 = par.cmd.ExecuteReader()
            If myReader0.Read Then
                prossimaBolletta = par.IfNull(myReader0("PROSSIMA_BOLLETTA"), "")
                rataProssima = CInt(Mid(prossimaBolletta, 5, 2))
            End If
            myReader0.Close()

            'annoDecorrenza = Year(CDate(par.FormattaData(dataDecorr)))
            Dim annoBolletta As Integer = Mid(prossimaBolletta, 1, 4)

            Dim idVoce As Integer = 0
            Dim importoVoce As Decimal = 0
            Dim idBolSchema As Long = 0
            par.cmd.CommandText = "SELECT bol_bollette_voci_gest.* FROM SISCOM_MI.bol_bollette_voci_gest,siscom_mi.bol_bollette_gest WHERE bol_bollette_gest.ID=BOL_BOLLETTE_VOCI_gest.ID_BOLLETTA_GEST AND bol_bollette_gest.ID=" & idBollGest
            Dim daVoci As Oracle.DataAccess.Client.OracleDataAdapter
            Dim dtVoci As New Data.DataTable
            daVoci = New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)

            daVoci.Fill(dtVoci)
            daVoci.Dispose()
            Dim importoTettoMax As Decimal = 0
            For Each row As Data.DataRow In dtVoci.Rows
                importoVoce = row.Item("IMPORTO")
                idVoce = row.Item("ID_VOCE")
                importoTettoMax = importoDaConfrontare / dtVoci.Rows.Count
                If importoVoce > importoTettoMax Then
                    rate = Format(importoVoce / importoTettoMax, "##,##0.00")
                    rate = Format(rate, "0")
                    impRateizzato = Format(importoVoce / rate, "##,##0.00")

                    par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO,ID_VOCE_BOLLETTA_GEST) Values (siscom_mi.seq_bol_schema.nextval, " & idContr & ", " & IDUNITA & ", " & ESERCIZIOF & ", " & idVoce & ", " & (par.VirgoleInPunti(importoVoce)) & " , " & rataProssima & ", " & rate & ", " & (par.VirgoleInPunti(impRateizzato)) & " , " & annoBolletta & "," & row.Item("ID") & ")"
                    par.cmd.ExecuteNonQuery()

                    par.cmd.CommandText = "select SISCOM_MI.SEQ_BOL_SCHEMA.CURRVAL FROM DUAL"
                    Dim myReaderA As Oracle.DataAccess.Client.OracleDataReader = par.cmd.ExecuteReader()
                    If myReaderA.Read Then
                        idBolSchema = myReaderA(0)
                    End If
                    myReaderA.Close()
                Else
                    rate = 1

                    'bol_schema
                    par.cmd.CommandText = "Insert into siscom_mi.BOL_SCHEMA (ID, ID_CONTRATTO, ID_UNITA, ID_ESERCIZIO_F, ID_VOCE, IMPORTO, DA_RATA, PER_RATE, IMPORTO_SINGOLA_RATA, ANNO,ID_VOCE_BOLLETTA_GEST) Values (siscom_mi.seq_bol_schema.nextval, " & idContr & ", " & IDUNITA & ", " & ESERCIZIOF & ", " & idVoce & ", " & (par.VirgoleInPunti(importoVoce)) & " , " & rataProssima & ", " & rate & ", " & (par.VirgoleInPunti(importoVoce)) & " , " & annoBolletta & "," & row.Item("ID") & ")"
                    par.cmd.ExecuteNonQuery()
                End If
            Next

            'AGGIORNO IL DOCUMENTO COME CONTABILE E TIPO APPLICAZIONE = 1 (: spostamento parziale)
            par.cmd.CommandText = "UPDATE SISCOM_MI.BOL_BOLLETTE_GEST SET TIPO_APPLICAZIONE='P',DATA_APPLICAZIONE='" & Format(Now, "yyyyMMdd") & "',ID_OPERATORE_APPLICAZIONE=" & Session.Item("ID_OPERATORE") & " WHERE ID=" & idBollGest
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                        & "VALUES (" & idContr & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                        & "'F184','APPLICAZIONE PARZIALE DI IMPORTO A DEBITO GESTIONALE')"
            par.cmd.ExecuteNonQuery()

            par.cmd.CommandText = "INSERT INTO SISCOM_MI.EVENTI_CONTRATTI (ID_CONTRATTO,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                & "VALUES (" & idContr & "," & Session.Item("ID_OPERATORE") & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                & "'F207','IMPORTO ELABORATO: EURO " & par.VirgoleInPunti(importoBolletta) & "')"
            par.cmd.ExecuteNonQuery()

            par.myTrans.Commit()
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

            Response.Write("<script>self.close();opener.document.getElementById('imgSalva').click();</script>")

        Catch ex As Exception
            If Not IsNothing(par.myTrans) Then
                par.myTrans.Rollback()
            End If
            par.OracleConn.Close()
            par.OracleConn.Dispose()
            Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
            Session.Add("ERRORE", "Provenienza:" & Page.Title & " - " & ex.Message)
            Response.Write("<script>top.location.href='../Errore.aspx';</script>")
        End Try
    End Sub
End Class
