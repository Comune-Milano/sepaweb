' REPORT FORNITORI 

Partial Class ReportFornitoriG
    Inherits PageSetIdMode
    Dim par As New CM.Global

    Dim vIdFornitore As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim StringaSql As String

        If Session.Item("OPERATORE") = "" Then
            Response.Redirect("~/AccessoNegato.htm", True)
            Exit Sub
        End If

        If Not IsPostBack Then


            vIdFornitore = Request.QueryString("ID_FORNITORE")

            If IsNumeric(vIdFornitore) Then

                Try
                    ' LEGGO FORNITORE
                    Label2.Text = "FORNITORE GIURIDICO"

                    par.OracleConn.Open()
                    par.SettaCommand(par)

                    par.cmd.CommandText = "select * from SISCOM_MI.FORNITORI where SISCOM_MI.FORNITORI.ID=" & vIdFornitore
                    Dim myReader1 As Oracle.DataAccess.Client.OracleDataReader
                    myReader1 = par.cmd.ExecuteReader()

                    If myReader1.Read Then
                        lblragione.Text = par.IfNull(myReader1("RAGIONE_SOCIALE"), "")
                        lbliva.Text = par.IfNull(myReader1("PARTITA_IVA"), "")
                        lblcognome.Text = par.IfNull(myReader1("COGNOME"), "")
                        lblnome.Text = par.IfNull(myReader1("NOME"), "")
                        lblCF.Text = par.IfNull(myReader1("COD_FISCALE"), "")
                        lblTel.Text = par.IfNull(myReader1("NUM_TELEFONO"), "")
                        lblcomune.Text = par.IfNull(myReader1("COMUNE_RESIDENZA"), "")
                        lblpr.Text = par.IfNull(myReader1("PR_RESIDENZA"), "")
                        lblindirizzo.Text = par.IfNull(myReader1("TIPO_INDIRIZZO_RESIDENZA"), "") & " " & par.IfNull(myReader1("INDIRIZZO_RESIDENZA"), "")
                        lblcivico.Text = par.IfNull(myReader1("CIVICO_RESIDENZA"), "")
                        lblcap.Text = par.IfNull(myReader1("CAP_RESIDENZA"), "")
                        lbliban.Text = par.IfNull(myReader1("IBAN"), "")
                        lblcfr.Text = par.IfNull(myReader1("COD_FISCALE_R"), "")
                        lblcomunea.Text = par.IfNull(myReader1("COMUNE_SEDE_A"), "")
                        lblcomuner.Text = par.IfNull(myReader1("COMUNE_RESIDENZA_R"), "")
                        lblpra.Text = par.IfNull(myReader1("PR_SEDE_A"), "")
                        lblprr.Text = par.IfNull(myReader1("PR_RESIDENZA_R"), "")
                        lblindirizzoa.Text = par.IfNull(myReader1("TIPO_INDIRIZZO_SEDE_A"), "") & " " & par.IfNull(myReader1("INDIRIZZO_SEDE_A"), "")
                        lblindirizzor.Text = par.IfNull(myReader1("TIPO_INDIRIZZO_RESIDENZA_R"), "") & par.IfNull(myReader1("INDIRIZZO_RESIDENZA_R"), "")
                        lblcivicoa.Text = par.IfNull(myReader1("CIVICO_SEDE_A"), "")
                        lblcivicor.Text = par.IfNull(myReader1("CIVICO_RESIDENZA_R"), "")
                        lblfax.Text = par.IfNull(myReader1("NUM_FAX"), "")
                        lblfaxa.Text = par.IfNull(myReader1("NUM_FAX_SEDE_A"), "")
                        lblcapa.Text = par.IfNull(myReader1("CAP_SEDE_A"), "")
                        lblcapr.Text = par.IfNull(myReader1("CAP_RESIDENZA_R"), "")
                        lbltela.Text = par.IfNull(myReader1("NUM_TELEFONO_SEDE_A"), "")
                        lbltelr.Text = par.IfNull(myReader1("TELEFONO_R"), "")
                        lblprocura.Text = par.IfNull(myReader1("NUM_PROCURA"), "")
                        lbldataprocura.Text = par.FormattaData(par.IfNull(myReader1("DATA_PROCURA"), ""))
                        If myReader1("TIPO_R") = "P" Then
                            lbltipor.Text = "PROCURATORE LEG."
                        Else
                            lbltipor.Text = "LEGALE RAPP."
                        End If
                        lblNote.Text = par.IfNull(myReader1("RIFERIMENTI"), "")

                    End If
                    myReader1.Close()

                    '*** ELENCO APPALTI
                    StringaSql = "  select  distinct(SISCOM_MI.APPALTI.ID), SISCOM_MI.APPALTI.ID_FORNITORE, SISCOM_MI.APPALTI.NUM_REPERTORIO, " _
              & " TO_CHAR(TO_DATE(SISCOM_MI.APPALTI.DATA_REPERTORIO,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_REPERTORIO"", (CASE (APPALTI.SAL) WHEN 0 THEN 'NO' ELSE 'SI' END) AS ""SAL"", " _
              & " SISCOM_MI.APPALTI.DESCRIZIONE, TO_CHAR(TO_DATE(SISCOM_MI.APPALTI.DATA_INIZIO,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_INIZIO"", TO_CHAR(TO_DATE(SISCOM_MI.APPALTI.DATA_FINE,'YYYYmmdd'),'DD/MM/YYYY') AS ""DATA_FINE"", SISCOM_MI.APPALTI.DURATA_MESI AS ""DURATA"", " _
              & " TRIM(TO_CHAR(SISCOM_MI.APPALTI.BASE_ASTA_CANONE,'9G999G999G999G999G990D99')) AS ""ASTA_CANONE"", " _
              & " TRIM(TO_CHAR(SISCOM_MI.APPALTI.BASE_ASTA_CONSUMO,'9G999G999G999G999G990D99')) AS ""ASTA_CONSUMO"", " _
              & " TRIM(TO_CHAR(SISCOM_MI.APPALTI.ONERI_SICUREZZA_CANONE,'9G999G999G999G999G990D99')) AS ""ONERI_CANONE"", " _
              & "TRIM(TO_CHAR(SISCOM_MI.APPALTI.ONERI_SICUREZZA_CONSUMO,'9G999G999G999G999G990D99')) AS ""ONERI_CONSUMO"", " _
              & "SISCOM_MI.APPALTI.PERC_ONERI_SIC_CAN, SISCOM_MI.APPALTI.PERC_ONERI_SIC_CON, SISCOM_MI.APPALTI.PENALI, '' AS ""RIFINIZIO"", " _
              & " '' AS ""RIFINE"", " _
              & " SISCOM_MI.LOTTI.DESCRIZIONE AS ""DESCRIZIONE_LOTTO"", SISCOM_MI.LOTTI.ID AS ""ID_LOTTO"" " _
              & " from SISCOM_MI.APPALTI, SISCOM_MI.APPALTI_LOTTI_SERVIZI, SISCOM_MI.LOTTI " _
              & " where SISCOM_MI.APPALTI.ID_FORNITORE = " & vIdFornitore & "" _
              & " AND SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_APPALTO=SISCOM_MI.APPALTI.ID (+) AND " _
              & "SISCOM_MI.APPALTI_LOTTI_SERVIZI.ID_LOTTO=SISCOM_MI.LOTTI.ID (+) " _
              & " ORDER BY SISCOM_MI.APPALTI.NUM_REPERTORIO ASC, DATA_REPERTORIO ASC"


                    par.cmd.CommandText = StringaSql
                    '***removed from db
                    'TRIM(TO_CHAR(SISCOM_MI.APPALTI.COSTO_GRADO_GIORNO,'9G999G999G999G999G990D99')) AS ""COSTO"",
                    Dim da As New Oracle.DataAccess.Client.OracleDataAdapter(par.cmd)
                    Dim ds As New Data.DataSet()

                    da.Fill(ds, "APPALTI")

                    DataGrid3.DataSource = ds
                    DataGrid3.DataBind()

                    ds.Dispose()
                    '*************************

                    '*** EVENTI_FORNITORI
                    InserisciEvento(par.cmd, vIdFornitore, Session.Item("ID_OPERATORE"), 90, "Stampa fornitori e appalti assegnati")


                    par.cmd.Dispose()
                    par.OracleConn.Close()
                    par.OracleConn.Dispose()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()

                Catch EX1 As Oracle.DataAccess.Client.OracleException
                    par.OracleConn.Close()
                    par.OracleConn.Dispose()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                Catch ex As Exception
                    par.OracleConn.Close()
                    par.OracleConn.Dispose()
                    Oracle.DataAccess.Client.OracleConnection.ClearAllPools()
                End Try
            End If
        End If

    End Sub

    Public Function InserisciEvento(ByVal MyPar As Oracle.DataAccess.Client.OracleCommand, ByVal vIdFornitore As Long, ByVal vIdOperatore As Long, ByVal Tab_Eventi As Integer, ByRef Motivazione As String) As Boolean

        Try

            InserisciEvento = False

            MyPar.Parameters.Clear()

            MyPar.CommandText = "insert into SISCOM_MI.EVENTI_FORNITORI (ID_FORNITORE,ID_OPERATORE,DATA_ORA,COD_EVENTO,MOTIVAZIONE) " _
                                & "VALUES (" & vIdFornitore & "," & vIdOperatore & ",'" & Format(Now, "yyyyMMddHHmmss") & "'," _
                                & "'F" & Tab_Eventi & "','" & par.PulisciStrSql(Motivazione) & "')"
            MyPar.ExecuteNonQuery()
            MyPar.CommandText = ""
            MyPar.Parameters.Clear()


        Catch ex As Exception
            InserisciEvento = False
            MyPar.Parameters.Clear()
        End Try

    End Function

End Class
